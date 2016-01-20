using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using TrainerEvaluate.BLL;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// SPSchoolInfo 的摘要说明
    /// </summary>
    public class SPSchoolInfo : IHttpHandler
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
                    ExportSchoolInfo(context);
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
            var shBll = new BLL.SPSchool();

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "SchoolName" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = shBll.GetRecordCount(" Status = 1 ");
            ds = shBll.GetListByPage(" Status = 1 ", sort, startIndex, endIndex, order);

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
            var schoolname = context.Request["schoolname"].Trim();
            var schdisname = context.Request["schdisname"].Trim();
            var runnature = context.Request["runnature"].Trim();
            var schooltype = context.Request["schooltype"].Trim();
            var legalname = context.Request["legalname"].Trim();
            var ds = new DataSet();
            var shBll = new BLL.SPSchool();
            var strWhere = " Status = 1 ";
            if (!string.IsNullOrEmpty(schoolname))
            {
                strWhere += string.Format(" and SchoolName like '%" + schoolname + "%' ");
            }
            if (!string.IsNullOrEmpty(schdisname))
            {
                strWhere += string.Format(" and  SchDisName like '%" + schdisname + "%' ");
            }

            if (!string.IsNullOrEmpty(legalname))
            {
                strWhere += string.Format(" and  LegalName like '%" + legalname + "%' ");
            }
            if (!string.IsNullOrEmpty(runnature))
            {
                strWhere += string.Format(" and  RunNatureCode = '" + runnature + "' ");
            }

            if (!string.IsNullOrEmpty(schooltype))
            {
                strWhere += string.Format(" and  SchoolTypeCode = '" + schooltype + "' ");
            }            

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "SchoolName" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = shBll.GetRecordCount(strWhere);
            ds = shBll.GetListByPage(strWhere, sort, startIndex, endIndex, order);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }

        private void ExportSchoolInfo(HttpContext context)
        {
            var exXls = new ExportXls();
            var fieldsNames = new List<string>();
            fieldsNames.Add("名称");
            fieldsNames.Add("所属学区");
            fieldsNames.Add("办学性质");
            fieldsNames.Add("学校类型");
            fieldsNames.Add("校址数");
            fieldsNames.Add("班级数");
            fieldsNames.Add("学生数");
            fieldsNames.Add("教师数");
            fieldsNames.Add("党员数");
            fieldsNames.Add("法人名称");
            fieldsNames.Add("联系电话");
            fieldsNames.Add("描述");

            var ds = QueryDataResultForExp(context);
            var filename = DateTime.Now.ToString("yyyy-MM-dd") + "-学校信息.xls";

            if (ds != null && ds.Tables.Count > 0)
            {
                exXls.ExportToXls(context.Response, fieldsNames, ds.Tables[0], filename);
            }
        }


        private DataSet QueryDataResultForExp(HttpContext context)
        {
            var name = context.Request["name"].Trim();
            var sdn = context.Request["sdn"].Trim();
            var rnn = context.Request["rnn"].Trim();
            var lln = context.Request["lln"].Trim();
            var sht = context.Request["sht"].Trim();
            var ds = new DataSet();
            var shBll = new BLL.SPSchool();
            var strWhere = "";
            if (!string.IsNullOrEmpty(name))
            {
                strWhere = string.Format(" SchoolName like '%" + name + "%' ");
            }
            if (!string.IsNullOrEmpty(sdn))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  SchDisName like '%" + sdn + "%' ");
                }
                else
                {
                    strWhere = string.Format(" SchDisName like '%" + sdn + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(rnn))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  RunNatureCode = '" + rnn + "' ");
                }
                else
                {
                    strWhere = string.Format(" RunNatureCode = '" + rnn + "' ");
                }
            }
            if (!string.IsNullOrEmpty(lln))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  LegalName like '%" + lln + "%' ");
                }
                else
                {
                    strWhere = string.Format(" LegalName '%" + lln + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(sht))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  SchoolTypeCode = '" + sht + "' ");
                }
                else
                {
                    strWhere = string.Format(" SchoolTypeCode = '" + sht + "' ");
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

            ds = shBll.GetDataForExport(strWhere);
            return ds;
        }

        private void AddData(HttpContext context)
        {
            var shModel = new Models.SPSchool();

            shModel.SchoolId = Guid.NewGuid();
            SetModelValue(shModel, context);
            shModel.CreatedDate = DateTime.Now;
            shModel.LastModifyTime = DateTime.Now;
            shModel.Status = 1;
            var shBll = new BLL.SPSchool();
            var result = false;
            var msg = "";
            try
            {
                result = shBll.Add(shModel);

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
            var shBll = new BLL.SPSchool();
            var shModel = shBll.GetModel(new Guid(id));
            shModel.LastModifyTime = DateTime.Now;
            var result = false;
            var msg = "";
            try
            {
                SetModelValue(shModel, context);
                result = shBll.Update(shModel);
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

        private void SetModelValue(Models.SPSchool shModel, HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request["RunNatureCode"]))
            {
                shModel.RunNatureCode = context.Request["RunNatureCode"];
            }
            if (!string.IsNullOrEmpty(context.Request["RunNatureName"]))
            {
                shModel.RunNatureName = context.Request["RunNatureName"];
            }
            
            if (!string.IsNullOrEmpty(context.Request["SchoolTypeCode"]))
            {
                shModel.SchoolTypeCode = context.Request["SchoolTypeCode"];
            }
            if (!string.IsNullOrEmpty(context.Request["SchoolTypeName"]))
            {
                shModel.SchoolTypeName = context.Request["SchoolTypeName"];
            }

            if (!string.IsNullOrEmpty(context.Request["SchDisId"]))
            {
                shModel.SchDisId = context.Request["SchDisId"];
            }
            if (!string.IsNullOrEmpty(context.Request["SchDisName"]))
            {
                shModel.SchDisName = context.Request["SchDisName"];
            }

            shModel.SchoolName = context.Request["SchoolName"];
            shModel.AddrNum = context.Request["AddrNum"];
            shModel.ClassNum = context.Request["ClassNum"];
            shModel.StudentNum = context.Request["StudentNum"];
            shModel.TeacherNum = context.Request["TeacherNum"];

            shModel.PartyNum = context.Request["PartyNum"];
            shModel.LegalName = context.Request["LegalName"];
            shModel.LinkTel = context.Request["LinkTel"];

            shModel.Description = context.Request["Description"];
        }

        private void DelData(string id, HttpContext context)
        {
            var shBll = new BLL.SPSchool();
            var result = false;
            var msg = "";
            try
            {
                result = shBll.Delete(new Guid(id));

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