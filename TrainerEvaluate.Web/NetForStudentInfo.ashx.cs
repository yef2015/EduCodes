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
    /// NetForStudentInfo 的摘要说明
    /// </summary>
    public class NetForStudentInfo : IHttpHandler
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
            var dsBll = new BLL.SPSchoolDistrict();

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "SchDisName" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = dsBll.GetRecordCount(" Status = 1 ");
            ds = dsBll.GetListByPage(" Status = 1 ", sort, startIndex, endIndex, order);
            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }



        private void Query(HttpContext context)
        {
            var name = context.Request["name"].Trim();
            var desp = context.Request["desp"].Trim();
            var ds = new DataSet();
            var sdBll = new BLL.SPSchoolDistrict();
            var strWhere = "";
            if (!string.IsNullOrEmpty(name))
            {
                strWhere = string.Format(" SchDisName like '%" + name + "%' ");
            }
            if (!string.IsNullOrEmpty(desp))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Description like '%" + desp + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Description like '%" + desp + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere += string.Format(" and  Status = 1 ");
            }
            else
            {
                strWhere = string.Format(" Status = 1 ");
            }

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "SchDisName" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = sdBll.GetRecordCount(strWhere);
            ds = sdBll.GetListByPage(strWhere, sort, startIndex, endIndex, order);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }



        private DataSet QueryDataResultForExp(HttpContext context)
        {
            var name = context.Request["name"].Trim();
            var desp = context.Request["desp"].Trim();
            var ds = new DataSet();
            var sdBll = new BLL.SPSchoolDistrict();
            var strWhere = "";
            if (!string.IsNullOrEmpty(name))
            {
                strWhere = string.Format(" SchDisName like '%" + name + "%' ");
            }
            if (!string.IsNullOrEmpty(desp))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Description like '%" + desp + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Description like '%" + desp + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere += string.Format(" and  Status = 1 ");
            }
            else
            {
                strWhere = string.Format(" Status = 1 ");
            }

            ds = sdBll.GetDataForExport(strWhere);
            return ds;
        }

        private void AddData(HttpContext context)
        {
            var sdModel = new Models.SPSchoolDistrict();
            sdModel.SchDisId = Guid.NewGuid();
            SetModelValue(sdModel, context);
            sdModel.CreatedDate = DateTime.Now;
            sdModel.LastModifyTime = DateTime.Now;
            sdModel.Status = 1;
            var sdBll = new BLL.SPSchoolDistrict();
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
            var sdBll = new BLL.SPSchoolDistrict();
            var sdModel = sdBll.GetModel(new Guid(id));
            sdModel.LastModifyTime = DateTime.Now;
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

        private void SetModelValue(Models.SPSchoolDistrict teaModel, HttpContext context)
        {
            teaModel.SchDisName = context.Request["SchDisName"];
            teaModel.Description = context.Request["Description"];
        }

        private void DelData(string id, HttpContext context)
        {
            var sdBll = new BLL.SPSchoolDistrict();
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