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
    /// SysParameters1 的摘要说明
    /// </summary>
    public class SysParameters1 : IHttpHandler,IRequiresSessionState
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        private string GetData(HttpContext context)
        {
            var ds = new DataSet();
            var sysparamBll = new BLL.SysParameters();
            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "Parameter" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];


            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = sysparamBll.GetRecordCount(" Pstatus !=0 ");
            ds = sysparamBll.GetListByPage(" Pstatus !=0  ", sort, startIndex, endIndex, order);
            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }


        private void SetModelValue(Models.SysParameters sparaModel, HttpContext context)
        {
            sparaModel.Parameter = context.Request["Parameter"];
            if (!string.IsNullOrEmpty(context.Request["StartTime"]))
            { 
                sparaModel.StartTime = Convert.ToDateTime(context.Request["StartTime"]);
            } 
            if (!string.IsNullOrEmpty(context.Request["Ptype"]))
            {
                sparaModel.Ptype = Convert.ToInt32(context.Request["Ptype"]);
            }
            if (!string.IsNullOrEmpty(context.Request["EndTime"]))
            {
                sparaModel.EndTime = Convert.ToDateTime(context.Request["EndTime"]);
            }
            if (!string.IsNullOrEmpty(context.Request["Pstatus"]))
            {
                sparaModel.Pstatus = Convert.ToInt32(context.Request["Pstatus"]);
            }
            else
            {
                sparaModel.Pstatus = 1;
            }
        }


        private void Add(HttpContext context)
        {
            var sparaModel = new Models.SysParameters();
            sparaModel.ID = Guid.NewGuid();
            sparaModel.CreateId = Profile.CurrentUser.UserId;
            sparaModel.CreateName = Profile.CurrentUser.UserName;
            sparaModel.CreateTime = System.DateTime.Now;
            SetModelValue(sparaModel, context);

            var sparaBll = new BLL.SysParameters();
            var result = false;
            var msg = "";
            try
            {
                result = sparaBll.Add(sparaModel);
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
            var sparaBll = new BLL.SysParameters();
            var sparaModel = sparaBll.GetModel(new Guid(id));

            sparaModel.LastModifyId = Profile.CurrentUser.UserId;
            sparaModel.LastModifyName = Profile.CurrentUser.UserName;
            sparaModel.LastModifyTime = System.DateTime.Now;
            SetModelValue(sparaModel, context);

            var result = false;
            var msg = "";
            try
            {
                result = sparaBll.Update(sparaModel);
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



        private void Del(string id, HttpContext context)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var result = false;
                var msg = "";
                var sparaBll = new BLL.SysParameters();
                try
                {
                    var sparaModel = sparaBll.GetModel(new Guid(id));

                    sparaModel.LastModifyId = Profile.CurrentUser.UserId;
                    sparaModel.LastModifyName = Profile.CurrentUser.UserName;
                    sparaModel.LastModifyTime = System.DateTime.Now;
                    sparaModel.Pstatus = 0;
                    result = sparaBll.Update(sparaModel);
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
            var Parameter = string.IsNullOrEmpty(context.Request["Parameter"]) ? "" : context.Request["Parameter"].Trim();
            var StartTime = string.IsNullOrEmpty(context.Request["StartTime"]) ? "" : context.Request["StartTime"].Trim();
            var Ptype = string.IsNullOrEmpty(context.Request["Ptype"]) ? "" : context.Request["Ptype"].Trim();
            var EndTime = string.IsNullOrEmpty(context.Request["EndTime"]) ? "" : context.Request["EndTime"].Trim();
            var Pstatus = string.IsNullOrEmpty(context.Request["Pstatus"]) ? "" : context.Request["Pstatus"].Trim();

            var ds = new DataSet();
            var sparaBll = new BLL.SysParameters();
            var strWhere = "";
            if (!string.IsNullOrEmpty(Parameter))
            {
                strWhere = string.Format(" Parameter like '%" + Parameter + "%' ");
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  StartTime like '%" + StartTime + "%' ");
                }
                else
                {
                    strWhere = string.Format(" StartTime  like '%" + StartTime + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(Ptype))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Ptype  like '%" + Ptype + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Ptype  like '%" + Ptype + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  EndTime  like '%" + EndTime + "%' ");
                }
                else
                {
                    strWhere = string.Format(" EndTime  like '%" + EndTime + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(Pstatus) && Pstatus != "0")
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Pstatus  like '%" + Pstatus + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Pstatus  like '%" + Pstatus + "%' ");
                }
            }
            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "StartTime" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = sparaBll.GetRecordCount(strWhere);
            ds = sparaBll.GetListByPage(strWhere, sort, startIndex, endIndex, order);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);

        }





    }
}