using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using TrainerEvaluate.BLL;
using TrainerEvaluate.Utility;
using System.Web.SessionState;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// ClassInfo 的摘要说明
    /// </summary>
    public class ClassInfo : IHttpHandler, IRequiresSessionState //就是这样显示的实现一下，不用实现什么方法
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
                case "c":
                    GetDataForCombobox(context);
                    break;
                case "ex":
                    ExportCourseInfo(context);
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

        private void GetDataForCombobox(HttpContext context)
        {
            var ds = new DataSet();
            var couBll = new BLL.Class();
            ds = couBll.GetAllList();

            //  [{"SUBITEM_VALUE":"1","SUBITEM_NAME":"男"},{"SUBITEM_VALUE":"2","SUBITEM_NAME":"女"}]  

            if (ds != null && ds.Tables.Count > 0)
            {
                var str = new StringBuilder("[");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    str.Append("{\"CourseId\": \"" + row["CourseId"] + "\",");
                    str.Append("\"CourseName\": \"" + row["CourseName"] + "\"},");
                }
                str.Remove(str.Length - 1, 1);
                str.Append("]");

                context.Response.Write(str.ToString());
            }
            // var str = JsonConvert.SerializeObject(new { total = ds.Tables[0].Rows.Count, rows = ds.Tables[0] }); 

        }

        private string GetData(HttpContext context)
        {
            var classYear = System.DateTime.Now.Year.ToString();
            if(context.Session["ClassYear"] != null)
            {
                classYear = context.Session["ClassYear"].ToString();
            }

            var ds = new DataSet();
            var classBll = new BLL.Class();

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);

            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "Name" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "desc" : context.Request["order"];
            
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = classBll.GetRecordCount("Status!=0 and YearLevel = '" + classYear + "' ");
            ds = classBll.GetListByPage("Status!=0  and YearLevel = '" + classYear + "' ", sort, startIndex, endIndex, order);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }

        private void Query(HttpContext context)
        {
            var name = context.Request["Name"];
            var description = context.Request["Description"];
            var area = context.Request["Area"];
            var type = context.Request["Type"];
            var ds = new DataSet();
            var classBll = new BLL.Class();
            var strWhere = "";
            if (!string.IsNullOrEmpty(name))
            {
                strWhere = string.Format(" Name like '%" + name.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(description))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Description  like '%" + description.Trim() + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Description like '%" + description.Trim() + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(area))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Area =" + area.Trim() + " ");
                }
                else
                {
                    strWhere = string.Format(" Area =" + area.Trim() + " ");
                }
            }
            if (!string.IsNullOrEmpty(type))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Type =" + type.Trim() + " ");
                }
                else
                {
                    strWhere = string.Format(" Type =" + type.Trim() + " ");
                }
            }

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);

            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "Name" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = classBll.GetRecordCount(strWhere);
            ds = classBll.GetListByPage(strWhere, sort, startIndex, endIndex, order);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }

        private void ExportCourseInfo(HttpContext context)
        {
            var exXls = new ExportXls();
            var fieldsNames = new List<string>();
            fieldsNames.Add("班级名称");
            fieldsNames.Add("培训对象");
            fieldsNames.Add("培训内容");
            fieldsNames.Add("开始日期");
            fieldsNames.Add("结束日期");
            fieldsNames.Add("学员人数");
            fieldsNames.Add("学时");
            fieldsNames.Add("学时类型");
            fieldsNames.Add("班主任");
            fieldsNames.Add("培训范围");
            fieldsNames.Add("培训级别");
            fieldsNames.Add("培训类型");

            var ds = QueryDataResultForExp(context);
            var filename = DateTime.Now.ToString("yyyy-MM-dd") + "-班级信息.xls";

            if (ds != null && ds.Tables.Count > 0)
            {
                exXls.ExportToXls(context.Response, fieldsNames, ds.Tables[0], filename);
            }
        }

        private DataSet QueryDataResultForExp(HttpContext context)
        {
            var name = context.Request["Name"].Trim();
            var description = context.Request["Description"].Trim();
            var area = context.Request["Area"].Trim();
            var type = context.Request["Type"].Trim();
            var ds = new DataSet();
            var classBll = new BLL.Class();
            var strWhere = "";
            if (!string.IsNullOrEmpty(name))
            {
                strWhere = string.Format(" Name like '%" + name + "%' ");
            }
            if (!string.IsNullOrEmpty(description))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Description  like '%" + description + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Description like '%" + description + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(area))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Area =" + area + " ");
                }
                else
                {
                    strWhere = string.Format(" Area =" + area + " ");
                }
            }
            if (!string.IsNullOrEmpty(type))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Type= " + type + "  ");
                }
                else
                {
                    strWhere = string.Format(" Type="  + type + " ");
                }
            }

            ds = classBll.GetDataForExport(strWhere);
            return ds;
        }

        private void AddData(HttpContext context)
        {
            var classModel = new Models.Class();
            SetModelValue(classModel, context);

            var classBll = new BLL.Class();
            if (classBll.GetId() == 1)
            {               
                classModel.ID = 2015000 + classBll.GetId();
            }
            else
            {
                classModel.ID = classBll.GetId();
            }

            classModel.Status = 1; //进行中，2--结束，0--删除
            classModel.CreatedTime = DateTime.Now;
            var result = false;
            var msg = "";
            try
            {
                result = classBll.Add(classModel);

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
            //  var str = JsonConvert.SerializeObject(new { success = result, errorMsg = msg});
            context.Response.Write(msg);
        }

        private void EditData(string id, HttpContext context)
        {
            var classBll = new BLL.Class();
            var classModel = classBll.GetModel(int.Parse(id));
            var result = false;
            var msg = "";
            try
            {
                SetModelValue(classModel, context);
                result = classBll.Update(classModel);
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

            //  var str = JsonConvert.SerializeObject(new { success = result, errorMsg = msg});
            context.Response.Write(msg);
        }

        private void SetModelValue(Models.Class classModel, HttpContext context)
        {
            classModel.Name = context.Request["Name"];
            classModel.Object = context.Request["Object"];
            classModel.Description = context.Request["Description"];
            if (!string.IsNullOrEmpty(context.Request["StartDate"]))
            {
                classModel.StartDate = DateTime.Parse(context.Request["StartDate"]);
            }
            if (!string.IsNullOrEmpty(context.Request["FinishDate"]))
            {
                classModel.FinishDate = DateTime.Parse(context.Request["FinishDate"]);
            }
            if (!string.IsNullOrEmpty(context.Request["Point"]))
            {
                classModel.Point = int.Parse(context.Request["Point"]);
            } 

            if (!string.IsNullOrEmpty(context.Request["PointType"]))
            {
                classModel.PointType = int.Parse(context.Request["PointType"]);
            }
            if (!string.IsNullOrEmpty(context.Request["Level"]))
            {
                classModel.Level = Convert.ToInt32(context.Request["Level"]);
            }
            if (!string.IsNullOrEmpty(context.Request["Type"]))
            {
                classModel.Type = int.Parse(context.Request["Type"]);
            }
            if (!string.IsNullOrEmpty(context.Request["Area"]))
            {
                classModel.Area = int.Parse(context.Request["Area"]);
            }
            if (context.Session["ClassYear"] != null)
            {
                classModel.YearLevel = context.Session["ClassYear"].ToString();
            }
        }


        private void DelData(string id, HttpContext context)
        {
            var courBll = new BLL.Class();
            var result = false;
            var msg = "";
            try
            {
                result = courBll.Delete(int.Parse(id));

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
            //  var str = JsonConvert.SerializeObject(new { success = result, errorMsg = msg});
            context.Response.Write(msg);
        }
    }
}