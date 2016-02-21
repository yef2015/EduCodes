using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using TrainerEvaluate.BLL;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// NetForAdminInfo 的摘要说明
    /// </summary>
    public class NetForAdminInfo : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var opType = context.Request["t"];
            var id = context.Request["id"];
            switch (opType)
            {
                case "n":
                    AddData(context);
                    break;
                case "e":
                    EditData(id, context);
                    break;
                case "d":
                    DelData(id, context);
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
            var dsBll = new BLL.NetEnterFor();

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "LastUpdateTime" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "desc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = dsBll.GetRecordCount(" IsDelete = 0 ");
            ds = dsBll.GetListByPage(" IsDelete = 0 ", sort, startIndex, endIndex, order);
            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }

        private void Query(HttpContext context)
        {
            var name = context.Request["trainName"].Trim();
            var explain = context.Request["explain"].Trim();
            var ds = new DataSet();
            var sdBll = new BLL.NetEnterFor();
            var strWhere = " IsDelete = 0 ";
            if (!string.IsNullOrEmpty(name))
            {
                strWhere += string.Format(" and TrainName like '%" + name + "%' ");
            }
            if (!string.IsNullOrEmpty(explain))
            {
                strWhere += string.Format(" and  explain like '%" + explain + "%' ");
            }

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "LastUpdateTime" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "desc" : context.Request["order"];
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = sdBll.GetRecordCount(strWhere);
            ds = sdBll.GetListByPage(strWhere, sort, startIndex, endIndex, order);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }

        private void AddData(HttpContext context)
        {
            var sdModel = new Models.NetEnterFor();
            sdModel.Guid = Guid.NewGuid();
            SetModelValue(sdModel, context);
            sdModel.CreateId = "";
            sdModel.CreateName = "";
            sdModel.EnterForNum = 0;
            var sdBll = new BLL.NetEnterFor();
            var result = false;
            var msg = "";
            try
            {
                result = sdBll.Add(sdModel);

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

        private void EditData(string id, HttpContext context)
        {
            var sdBll = new BLL.NetEnterFor();
            var sdModel = sdBll.GetModel(new Guid(id));
            sdModel.LastUpdateTime = DateTime.Now;
            var result = false;
            var msg = "";
            try
            {
                SetModelValue(sdModel, context);
                result = sdBll.Update(sdModel);
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

        private void SetModelValue(Models.NetEnterFor model, HttpContext context)
        {
            model.TrainName = context.Request["TrainName"];
            model.explain = context.Request["Explain"];
            if (!string.IsNullOrEmpty(context.Request["BeginTime"]))
            {
                model.BeginTime = DateTime.Parse(context.Request["BeginTime"]);
            }
            if (!string.IsNullOrEmpty(context.Request["EndTime"]))
            {
                model.EndTime = DateTime.Parse(context.Request["EndTime"]);
            }
            if (!string.IsNullOrEmpty(context.Request["PersonMax"]))
            {
                model.PersonMax = int.Parse(context.Request["PersonMax"]);
            }
            else
            {
                model.PersonMax = 0;
            }
        }

        private void DelData(string id, HttpContext context)
        {
            var sdBll = new BLL.NetEnterFor();
            var result = false;
            var msg = "";
            try
            {
                result = sdBll.Delete(new Guid(id));

                if (!result)
                {
                    msg = "删除失败！";
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
}