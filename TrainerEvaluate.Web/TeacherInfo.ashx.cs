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
    /// TeacherInfo 的摘要说明
    /// </summary>
    public class TeacherInfo : IHttpHandler
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
                    ExportTeacherInfo(context);
                    break;   
                case "gt":
                    GetTeacherInfoClass(context);
                    break;
                case "pah":
                    var strph = GetPersonArchive(context);
                    context.Response.Write(strph);
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
            //  ds = questionbll.GetCourseStudentState(courseId);
            ds = teacherbll.GetCourseTeacherStateListByPage(courseId, "ck", startIndex, endIndex);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }


        private string GetData(HttpContext context)
        {
            var ds = new DataSet();
            var teaBll = new BLL.Teacher();

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "TeacherName" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];

            if (sort == "GenderName")
            {
                sort = "Gender";
            }
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = teaBll.GetRecordCount(" Status = 1 ");
            ds = teaBll.GetListByPage(" Status = 1 ", sort, startIndex, endIndex, order);



            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }


        private void GetDataForCombobox(HttpContext context)
        {
            var ds = new DataSet();
            var teaBll = new BLL.Teacher();
            ds = teaBll.GetAllList();

            //  [{"SUBITEM_VALUE":"1","SUBITEM_NAME":"男"},{"SUBITEM_VALUE":"2","SUBITEM_NAME":"女"}]  

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

            // var str = JsonConvert.SerializeObject(new { total = ds.Tables[0].Rows.Count, rows = ds.Tables[0] }); 

        }


        private void Query(HttpContext context)
        {
            var teachName = context.Request["name"].Trim();
            var school = context.Request["sch"].Trim();
            var gender = context.Request["gender"].Trim();
            var title = context.Request["title"].Trim();
            var idno = context.Request["idno"].Trim();
            var ds = new DataSet();
            var teaBll = new BLL.Teacher();
            var strWhere = "  Status = 1 ";
            if (!string.IsNullOrEmpty(teachName))
            {
                strWhere += string.Format(" and TeacherName like '%" + teachName + "%' ");
            }
            if (!string.IsNullOrEmpty(school))
            {
                strWhere += string.Format(" and  Dept like '%" + school + "%' ");
            }
            if (!string.IsNullOrEmpty(gender) && gender != "0")
            {
                strWhere += string.Format(" and  Gender = '" + gender + "' ");
            }
            if (!string.IsNullOrEmpty(title))
            {
                strWhere += string.Format(" and  Title = '" + title + "' ");
            }
            if (!string.IsNullOrEmpty(idno))
            {
                strWhere += string.Format(" and  IdentityNo like '%" + idno + "%' ");
            }

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "TeacherName" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];
            if (sort == "GenderName")
            {
                sort = "Gender";
            }
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = teaBll.GetRecordCount(strWhere);
            ds = teaBll.GetListByPage(strWhere, sort, startIndex, endIndex, order);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }



        private void ExportTeacherInfo(HttpContext context)
        {
            var exXls = new ExportXls();
            var fieldsNames = new List<string>();
            fieldsNames.Add("姓名");
            fieldsNames.Add("性别");
            fieldsNames.Add("身份证号");
            fieldsNames.Add("所在单位");
            fieldsNames.Add("职称");
            fieldsNames.Add("职务");
            fieldsNames.Add("研究方向");
            fieldsNames.Add("手机号");
            fieldsNames.Add("描述");

            var ds = QueryDataResultForExp(context);
            var filename = DateTime.Now.ToString("yyyy-MM-dd") + "-教师信息.xls";

            if (ds != null && ds.Tables.Count > 0)
            {
                exXls.ExportToXls(context.Response, fieldsNames, ds.Tables[0], filename);
            }
        }


        private DataSet QueryDataResultForExp(HttpContext context)
        {
            var teachName = context.Request["name"].Trim();
            var school = context.Request["sch"].Trim();
            var gender = context.Request["gender"].Trim();
            var title = context.Request["title"].Trim();
            var idno = context.Request["idno"].Trim();

            var ds = new DataSet();
            var teaBll = new BLL.Teacher();
            var strWhere = " Status = 1 ";
            if (!string.IsNullOrEmpty(teachName))
            {
                strWhere += string.Format(" and TeacherName like '%" + teachName + "%' ");
            }
            if (!string.IsNullOrEmpty(school))
            {
                strWhere += string.Format(" and  Dept like '%" + school + "%' ");
            }
            if (!string.IsNullOrEmpty(gender) && gender != "0")
            {
                strWhere += string.Format(" and  Gender = '" + gender + "' ");
            }
            if (!string.IsNullOrEmpty(title))
            {
                strWhere += string.Format(" and  Title = '" + title + "' ");
            }
            if (!string.IsNullOrEmpty(idno))
            {
                strWhere += string.Format(" and  IdentityNo like '%" + idno + "%' ");
            }

            ds = teaBll.GetDataForExport(strWhere);
            return ds;
        }



        private void AddData(HttpContext context)
        {
            var teaModel = new Models.Teacher();
            teaModel.TeacherId = Guid.NewGuid();
            SetModelValue(teaModel, context);
            teaModel.CreateTime = DateTime.Now;
            teaModel.LastModifyTime = DateTime.Now;
            teaModel.Status = 1;
            var teaBll = new BLL.Teacher();
            var result = false;
            var msg = "";
            try
            {
                //var sysUserMo = new Models.SysUser();
                //var sysuserbll = new BLL.SysUser();
                //sysUserMo.UserRole = (int)EnumUserRole.Student;
                //sysUserMo.UserName = teaModel.TeacherName;
                //sysUserMo.UserId = teaModel.TeacherId;
                //sysUserMo.UserPassWord = Common.defaultPwd;
                //sysUserMo.UserAccount = teaModel.IdentityNo;

                //sysuserbll.Add(sysUserMo);



                // 添加内容到sysuser表中
                var sysUserMo = new Models.SysUser();
                var sysuserbll = new BLL.SysUser();
                sysUserMo.UserRole = (int)EnumUserRole.Teacher;
                sysUserMo.UserName = teaModel.TeacherName;
                sysUserMo.UserId = teaModel.TeacherId;
                sysUserMo.UserPassWord = teaBll.GetPwd();
                sysUserMo.UserAccount = teaBll.GetTeacherAccount();
                sysUserMo.IdentityNo = teaModel.IdentityNo;

                sysuserbll.Add(sysUserMo);

                // 添加内容到Teacher表中
                result = teaBll.Add(teaModel);

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
            var teaBll = new BLL.Teacher();
            var teaModel = teaBll.GetModel(new Guid(id));
            teaModel.LastModifyTime = DateTime.Now;
            var result = false;
            var msg = "";
            try
            {
                SetModelValue(teaModel, context);

                var sysuserbll = new BLL.SysUser();
                var sysUserMo = sysuserbll.GetModel(new Guid(id));
                sysUserMo.UserName = teaModel.TeacherName;
                sysUserMo.UserId = teaModel.TeacherId;
                sysUserMo.IdentityNo = teaModel.IdentityNo;

                sysuserbll.Update(sysUserMo);


                result = teaBll.Update(teaModel);
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




        private void SetModelValue(Models.Teacher teaModel, HttpContext context)
        {
            teaModel.Gender = Convert.ToInt32(context.Request["Gender"]);
            if (!string.IsNullOrEmpty(context.Request["Title"]))
            {
                teaModel.Title = Convert.ToInt32(context.Request["Title"]);
            }
            teaModel.IdentityNo = context.Request["IdentityNo"];
            teaModel.Dept = context.Request["Dept"];
            teaModel.TeacherName = context.Request["TeacherName"];
            teaModel.Post = context.Request["Post"];
            teaModel.ResearchBigName = context.Request["ResearchBigName"];
            teaModel.Research = context.Request["Research"];
            teaModel.Mobile = context.Request["Mobile"];
            teaModel.Description = context.Request["Description"];
        }



        private void DelData(string id, HttpContext context)
        {
            var teaBll = new BLL.Teacher();
            var result = false;
            var msg = "";
            try
            {
                result = teaBll.Delete(new Guid(id));
                var sysuserbll = new BLL.SysUser();
                sysuserbll.Delete(new Guid(id));

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

        private string GetPersonArchive(HttpContext context)
        {
            var ds = new DataSet();
            var teaBll = new BLL.Teacher();

            var teaId = context.Request["uid"];

            ds = teaBll.GetListByPage(" Status = 1   and TeacherId = '" + teaId + "' ", "TeacherName", 1, 5, "asc");
            var str = string.Empty;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                str = JsonConvert.SerializeObject(new { rows = ds.Tables[0] });
            }
            return str;
        }
       
    }
}