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
    /// SPSchoolDistrictInfo 的摘要说明
    /// </summary>
    public class SPSchoolDistrictInfo : IHttpHandler
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
                case "g":
                    GetDataTeacher(context);
                    break;
                case "s":
                    SaveCourseTeacher(context);
                    break;     
                case "ct":
                    SaveClassTeacher(context);
                    break;
                case "ex":
                    ExportSchoolDistrictInfo(context); 
                    break;   
                case "gt":
                    GetTeacherInfoClass(context);
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


        private void SaveClassTeacher(HttpContext context)
        {
            var msg = "";
            var courseId = context.Request["ClassId"];
            var isAll = context.Request["IsAll"];
            if (!string.IsNullOrEmpty(isAll)) //全选
            {
                if (isAll == "1")
                {
                    var teacherBll = new BLL.Teacher();
                    var result = teacherBll.SaveChooseForAllofClass(courseId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                else //全不选
                {
                    var teacherBll = new BLL.Teacher();
                    var result = teacherBll.DeletTeacherofClass(courseId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
            }
            else
            {
                var teaids = context.Request["TeacherIds"];
                var unteaids = context.Request["UnTeacherIds"];
                if (!string.IsNullOrEmpty(teaids))
                {
                    if (!string.IsNullOrEmpty(unteaids))
                    {
                        var unid = unteaids.Split('|');
                        if (unid.Length > 0)
                        {
                            foreach (var uid in unid)
                            {
                                if (!string.IsNullOrEmpty(uid))
                                {
                                    teaids = teaids.Replace(uid, "");
                                }
                            }
                        }
                    }
                    var tea = teaids.Split('|');
                    var teaList = new List<string>();
                    foreach (var sid in tea)
                    {
                        if (!string.IsNullOrEmpty(sid))
                        {
                            if (!teaList.Contains(sid))
                            {
                                teaList.Add(sid);
                            }
                        }
                    }  

                    var teaBll = new BLL.Teacher();
                    var result = teaBll.SaveChooseTeacherofClass(teaList.ToArray(), courseId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                else
                {
                    var teaBll = new BLL.Teacher();
                    var result = teaBll.SaveChooseTeacherofClass(null, courseId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
            }
            context.Response.Write(msg);

        }

        private void SaveCourseTeacher(HttpContext context)
        {
            var msg = "";
            var courseId = context.Request["CourseId"];
            var isAll = context.Request["IsAll"];
            if (!string.IsNullOrEmpty(isAll)) //全选
            {
                if (isAll == "1")
                {
                    var teacherBll = new BLL.Teacher();
                    var result = teacherBll.SaveChooseForAll(courseId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                else //全不选
                {
                    var teacherBll = new BLL.Teacher();
                    var result = teacherBll.DeletCourseTeacherbyCourseId(courseId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
            }
            else
            {
                var teaids = context.Request["TeaIds"];
                var unteaids = context.Request["UnTeaIds"];
                if (!string.IsNullOrEmpty(teaids))
                {
                    if (!string.IsNullOrEmpty(unteaids))
                    {
                        var unid = unteaids.Split('|');
                        if (unid.Length > 0)
                        {
                            foreach (var uid in unid)
                            {
                                if (!string.IsNullOrEmpty(uid))
                                {
                                    teaids = teaids.Replace(uid, "");
                                }
                            }
                        }
                    }
                    var tea = teaids.Split('|');
                    var teaList = new List<string>();
                    foreach (var sid in tea)
                    {
                        if (!string.IsNullOrEmpty(sid))
                        {
                            if (!teaList.Contains(sid))
                            {
                                teaList.Add(sid);
                            }
                        }
                    }
                    var teaBll = new BLL.Teacher();
                    var result = teaBll.SaveChooseTeacher(teaList.ToArray(), courseId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                else
                {
                    var teaBll = new BLL.Teacher();
                    var result = teaBll.SaveChooseTeacher(null, courseId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
            }
            context.Response.Write(msg);
        }

        private void GetTeacherInfoClass(HttpContext context)
        {
            var ds = new DataSet();
            var classId = context.Request["cId"];
            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var teaBll = new BLL.Teacher();
            var teacherbll = new BLL.Teacher();
            var num = teacherbll.GetRecordCount("Status=1");
            ds = teacherbll.GetClassTeacherListByPage(classId, "ck", startIndex, endIndex); 
            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }

        private void GetDataTeacher(HttpContext context)
        {
            var ds = new DataSet();
            var courseId = context.Request["coId"];
            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1; 
     
            var teacherbll = new BLL.Teacher();
            var num = teacherbll.GetRecordCount("Status=1");
            ds = teacherbll.GetCourseTeacherStateListByPage(courseId, "ck", startIndex, endIndex);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
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

        private void GetDataForCombobox(HttpContext context)
        {
            var ds = new DataSet();
            var teaBll = new BLL.Teacher();
            ds = teaBll.GetAllList();

            if (ds != null && ds.Tables.Count > 0)
            {
                var str = new StringBuilder("[");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    str.Append("{\"TeacherId\": \"" + row["TeacherId"] + "\",");
                    str.Append("\"TeacherName\": \"" + row["TeacherName"] + "\"},");
                }
                str.Remove(str.Length - 1, 1);
                str.Append("]");

                context.Response.Write(str.ToString());
            }
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

        private void ExportSchoolDistrictInfo(HttpContext context)
        {
            var exXls = new ExportXls();
            var fieldsNames = new List<string>();
            fieldsNames.Add("名称");
            fieldsNames.Add("描述");

            var ds = QueryDataResultForExp(context);
            var filename = DateTime.Now.ToString("yyyy-MM-dd") + "-学区信息.xls";

            if (ds != null && ds.Tables.Count > 0)
            {
                exXls.ExportToXls(context.Response, fieldsNames, ds.Tables[0], filename);
            }
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
}