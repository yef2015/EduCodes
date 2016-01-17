using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// Dictionaries1 的摘要说明
    /// </summary>
    public class Dictionaries1 : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            var opType = context.Request["t"];
            var id = context.Request["id"];
            switch (opType)
            {
                case "add":
                    Add(context);
                    break;
                case "edit": 
                    Edit(id, context);
                    break;
                case "del":
                    Del(id, context);
                    break;
                case "q":
                    Query(context);
                    break;
                default:
                    var str = GetData(context);
                    context.Response.Write(str);
                    break;
            }
        }





        private string GetData(HttpContext context)
        {
            var ds = new DataSet();
            var dictBll = new BLL.Dictionaries();
            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "Name" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];


            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = dictBll.GetRecordCount(" Dstatus =1 ");
            ds = dictBll.GetListByPage(" Dstatus = 1 ",sort, startIndex, endIndex, order);
            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }


        private void SetModelValue(Models.Dictionaries dictModel, HttpContext context)
        {
            dictModel.Name = context.Request["Name"];
            dictModel.DType = context.Request["DType"];

        }

        private void Add(HttpContext context)
        {
            var dictBll = new BLL.Dictionaries();
            var dictModel = new Models.Dictionaries();
            dictModel.ID = dictBll.GetMaxId();
            dictModel.Dstatus = 1;
            dictModel.CreateId = Profile.CurrentUser.UserId;
            dictModel.CreateName = Profile.CurrentUser.UserName;
            dictModel.CreateTime = System.DateTime.Now;
            SetModelValue(dictModel, context);

       
            var result = false;
            var msg = "";
            try
            {
                result = dictBll.Add(dictModel);
                if (!result)
                {
                    msg = "保存失败！";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                result = false;
                msg = ex.Message;
            }
            context.Response.Write(msg);
        }


        private void Edit(string id, HttpContext context)
        {
            var msg = "";
            if (string.IsNullOrEmpty(id))
            {
                msg = "参数错误！";
            }
            else
            {
                var id1 = 0;
                try
                {
                    id1 = Convert.ToInt32(id);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteFileLog("Edit id wrong: id"+id+"|" + ex.Message);
                    msg = "参数错误！";
                }
                var dicBll = new BLL.Dictionaries();
                var dicModel = dicBll.GetModel(id1); 
                dicModel.LastModifyId = Profile.CurrentUser.UserId;
                dicModel.LastModifyName = Profile.CurrentUser.UserName;
                dicModel.LastModifyTime = System.DateTime.Now;
                SetModelValue(dicModel, context);

                var result = false;

                try
                {
                    result = dicBll.Update(dicModel);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLogofExceptioin(ex);
                    result = false;
                    msg = ex.Message;
                }
            } 
            context.Response.Write(msg);
        }



        private void Del(string id, HttpContext context)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var result = false;
                var msg = "";
                var dicBll = new BLL.Dictionaries();
                try
                {
                    var id1 = Convert.ToInt32(id);
                    var dicModel = dicBll.GetModel(id1);

                    // dicModel.LastModifyId = Profile.CurrentUser.UserId;
                    // dicModel.LastModifyName = Profile.CurrentUser.UserName;
                    dicModel.LastModifyTime = System.DateTime.Now;
                    dicModel.Dstatus = 0;
                    result = dicBll.Update(dicModel);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLogofExceptioin(ex);
                    result = false;
                    msg = ex.Message;
                }
                context.Response.Write(msg); 
            }
        }

        private void Query(HttpContext context)
        {
            var dname = string.IsNullOrEmpty(context.Request["Name"]) ? "" : context.Request["Name"].Trim();
            var dtype = string.IsNullOrEmpty(context.Request["Ptype"]) ? "" : context.Request["Ptype"].Trim();

            var ds = new DataSet();
            var dictBll = new BLL.Dictionaries();
            var strWhere = " Dstatus =1 ";
            if (!string.IsNullOrEmpty(dname))
            {
                strWhere += string.Format(" and Name like '%" + dname + "%' ");
            }
            if (!string.IsNullOrEmpty(dtype))
            {
                strWhere += string.Format(" and  DType like '%" + dtype + "%' ");
            }

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "CreateTime" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = dictBll.GetRecordCount(strWhere);
            ds = dictBll.GetListByPage(strWhere, sort, startIndex, endIndex, order);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);

        }











        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}