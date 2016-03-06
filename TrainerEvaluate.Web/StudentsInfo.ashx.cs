using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TrainerEvaluate.BLL;
using TrainerEvaluate.Utility;
using System.Web.SessionState;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// StudentsInfo 的摘要说明
    /// </summary>
    public class StudentsInfo : IHttpHandler, IRequiresSessionState
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
                case "ex":
                    ExportStuInfo(context);
                    break;
                case "cs":
                    GetClassStu(context);
                    break;
                case "csq":
                    GetClassStuByConditon(context);
                    break;
                case "sc":
                    SaveClassStuData(context);
                    break;
                case "pah":
                    var strph = GetPersonArchive(context);
                    context.Response.Write(strph);
                    break;
                case "psdif":
                    var strinfo = GetPersonStudentInfo(context);
                    context.Response.Write(strinfo);
                    break;
                case "esif":
                    EditDataPerson(id, context);
                    break;
                case "stree":
                    Guid dui = Guid.Empty;
                    var stree = GetStudentTree();
                    context.Response.Write(stree);
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
            string strWhere = "Status=1";
            var schoolname = "all";
            if (context.Session["SchoolName"] != null)
            {
                schoolname = context.Session["SchoolName"].ToString();
            }
            if (schoolname != "all")
            {
                strWhere += " and School in(select SchoolName from School where (SchDisName = '" + schoolname + "' or SchoolName = '" + schoolname + "') and Status = 1) ";
            }

            var ds = new DataSet();
            var stuBll = new BLL.Student();
            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "StuName" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];

            if (sort == "GenderName")
            {
                sort = "Gender";
            }

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = stuBll.GetRecordCount(strWhere);
            ds = stuBll.GetListByPage(strWhere, sort, startIndex, endIndex, order);
            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }


        private void GetClassStu(HttpContext context)
        {
            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var coId = context.Request["coId"]; //班级id
            //var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "StuName" : context.Request["sort"];
            //var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];

            var stuBll = new BLL.Student();

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = stuBll.GetRecordCount(" Status=1 ");
            var ds = stuBll.GetClassStuListByPage(coId, "ck", startIndex, endIndex);
            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }


        private void GetClassStuByConditon(HttpContext context)
        {
            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var coId = context.Request["coId"]; //班级id
            var school = context.Request["school"];
            var student = context.Request["student"];
            var card = context.Request["card"];
            var code = context.Request["code"];

            var strWhere = " Status=1 ";
            if (!string.IsNullOrEmpty(school))
            {
                strWhere += string.Format(" and School like '%" + school + "%' ");
            }
            if (!string.IsNullOrEmpty(student))
            {
                strWhere += string.Format(" and  StuName  like '%" + student + "%' ");
            }
            if (!string.IsNullOrEmpty(card))
            {
                strWhere += string.Format(" and  IdentityNo  like '%" + card + "%' ");
            }
            if (!string.IsNullOrEmpty(code))
            {
                strWhere += string.Format(" and  TeachNo  like '%" + code + "%' ");
            }

            var stuBll = new BLL.Student();

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = stuBll.GetRecordCount(strWhere);
            var ds = stuBll.GetClassStuListByPageCondition(coId, strWhere, "ck", startIndex, endIndex);
            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }



        private void Query(HttpContext context)
        {
            //var stuName = context.Request["name"].Trim();
            //var school = context.Request["sch"].Trim();
            //var title = context.Request["title"].Trim();
            //var telno = context.Request["telno"].Trim();
            //var gender = context.Request["gender"].Trim();
            //var idno = context.Request["idno"].Trim();

            var schoolname = "all";
            if (context.Session["SchoolName"] != null)
            {
                schoolname = context.Session["SchoolName"].ToString();
            }

            var stuName = string.IsNullOrEmpty(context.Request["name"]) ? "" : context.Request["name"].Trim();
            var school = string.IsNullOrEmpty(context.Request["sch"]) ? "" : context.Request["sch"].Trim();
            var title = string.IsNullOrEmpty(context.Request["title"]) ? "" : context.Request["title"].Trim();
            var telno = string.IsNullOrEmpty(context.Request["telno"]) ? "" : context.Request["telno"].Trim();
            var gender = string.IsNullOrEmpty(context.Request["gender"]) ? "" : context.Request["gender"].Trim();
            var idno = string.IsNullOrEmpty(context.Request["idno"]) ? "" : context.Request["idno"].Trim();
            var rank = string.IsNullOrEmpty(context.Request["rank"]) ? "" : context.Request["rank"].Trim();

            /*
            var birthday = string.IsNullOrEmpty(context.Request["birthday"]) ? "" : context.Request["birthday"].Trim();
            var nation = string.IsNullOrEmpty(context.Request["nation"]) ? "" : context.Request["nation"].Trim();

            var firstrecord = string.IsNullOrEmpty(context.Request["firstrecord"]) ? "" : context.Request["firstrecord"].Trim();
            var firstschool = string.IsNullOrEmpty(context.Request["firstschool"]) ? "" : context.Request["firstschool"].Trim();
            var lastrecord = string.IsNullOrEmpty(context.Request["lastrecord"]) ? "" : context.Request["lastrecord"].Trim();
            var lastschool = string.IsNullOrEmpty(context.Request["lastschool"]) ? "" : context.Request["lastschool"].Trim();
            var policticsstatus = string.IsNullOrEmpty(context.Request["policticsstatus"]) ? "" : context.Request["policticsstatus"].Trim();
            var rank = string.IsNullOrEmpty(context.Request["rank"]) ? "" : context.Request["rank"].Trim();
            var ranktime = string.IsNullOrEmpty(context.Request["ranktime"]) ? "" : context.Request["ranktime"].Trim();
            var post = string.IsNullOrEmpty(context.Request["post"]) ? "" : context.Request["post"].Trim();
            var posttime = string.IsNullOrEmpty(context.Request["posttime"]) ? "" : context.Request["posttime"].Trim();
            var mobile = string.IsNullOrEmpty(context.Request["mobile"]) ? "" : context.Request["mobile"].Trim();
            var teachno = string.IsNullOrEmpty(context.Request["teachno"]) ? "" : context.Request["teachno"].Trim();
            var description = string.IsNullOrEmpty(context.Request["description"]) ? "" : context.Request["description"].Trim();
           */


            var ds = new DataSet();
            var stuBll = new BLL.Student();
            var strWhere = " status = 1 ";
            if (!string.IsNullOrEmpty(stuName))
            {
                strWhere += string.Format(" and StuName like '%" + stuName + "%' ");
            }
            if (!string.IsNullOrEmpty(school))
            {
                strWhere += string.Format(" and  School  like '%" + school + "%' ");
            }
            if (!string.IsNullOrEmpty(title))
            {
                strWhere += string.Format(" and  JobTitle  like '%" + title + "%' ");
            }
            if (!string.IsNullOrEmpty(telno))
            {
                strWhere += string.Format(" and  TelNo  like '%" + telno + "%' ");
            }
            if (!string.IsNullOrEmpty(gender) && gender != "0")
            {
                strWhere += string.Format(" and  Gender  like '%" + gender + "%' ");
            }
            if (!string.IsNullOrEmpty(idno))
            {
                strWhere += string.Format(" and  IdentityNo  like '%" + idno + "%' ");
            }
            if (!string.IsNullOrEmpty(rank))
            {
                strWhere += string.Format(" and  Rank  like '%" + rank + "%' ");
            }

            if (schoolname != "all")
            {
                strWhere += " and School in(select SchoolName from School where (SchDisName = '" + schoolname + "' or SchoolName = '" + schoolname + "') and Status = 1) ";
            }

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "StuName" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];
            if (sort == "GenderName")
            {
                sort = "Gender";
            }
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = stuBll.GetRecordCount(strWhere);
            ds = stuBll.GetListByPage(strWhere, sort, startIndex, endIndex, order);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }



        private DataSet QueryDataResultForExp(HttpContext context)
        {
            var schoolname = "all";
            if (context.Session["SchoolName"] != null)
            {
                schoolname = context.Session["SchoolName"].ToString();
            }
            var stuName = string.IsNullOrEmpty(context.Request["name"]) ? "" : context.Request["name"].Trim();
            var school = string.IsNullOrEmpty(context.Request["sch"]) ? "" : context.Request["sch"].Trim();
            var title = string.IsNullOrEmpty(context.Request["title"]) ? "" : context.Request["title"].Trim();
            var telno = string.IsNullOrEmpty(context.Request["telno"]) ? "" : context.Request["telno"].Trim();
            var gender = string.IsNullOrEmpty(context.Request["gender"]) ? "" : context.Request["gender"].Trim();
            var idno = string.IsNullOrEmpty(context.Request["idno"]) ? "" : context.Request["idno"].Trim();
            var rank = string.IsNullOrEmpty(context.Request["rank"]) ? "" : context.Request["rank"].Trim();

            /*
            var birthday = string.IsNullOrEmpty(context.Request["birthday"]) ? "" : context.Request["birthday"].Trim();
            var nation = string.IsNullOrEmpty(context.Request["nation"]) ? "" : context.Request["nation"].Trim();

            var firstrecord = string.IsNullOrEmpty(context.Request["firstrecord"]) ? "" : context.Request["firstrecord"].Trim();
            var firstschool = string.IsNullOrEmpty(context.Request["firstschool"]) ? "" : context.Request["firstschool"].Trim();
            var lastrecord = string.IsNullOrEmpty(context.Request["lastrecord"]) ? "" : context.Request["lastrecord"].Trim();
            var lastschool = string.IsNullOrEmpty(context.Request["lastschool"]) ? "" : context.Request["lastschool"].Trim();
            var policticsstatus = string.IsNullOrEmpty(context.Request["policticsstatus"]) ? "" : context.Request["policticsstatus"].Trim();
            var rank = string.IsNullOrEmpty(context.Request["rank"]) ? "" : context.Request["rank"].Trim();
            var ranktime = string.IsNullOrEmpty(context.Request["ranktime"]) ? "" : context.Request["ranktime"].Trim();
            var post = string.IsNullOrEmpty(context.Request["post"]) ? "" : context.Request["post"].Trim();
            var posttime = string.IsNullOrEmpty(context.Request["posttime"]) ? "" : context.Request["posttime"].Trim();
            var mobile = string.IsNullOrEmpty(context.Request["mobile"]) ? "" : context.Request["mobile"].Trim();
            var teachno = string.IsNullOrEmpty(context.Request["teachno"]) ? "" : context.Request["teachno"].Trim();
            var description = string.IsNullOrEmpty(context.Request["description"]) ? "" : context.Request["description"].Trim();
             * */
            var ds = new DataSet();
            var stuBll = new BLL.Student();
            var strWhere = "status = 1";
            if (!string.IsNullOrEmpty(stuName))
            {
                strWhere += string.Format(" and StuName like '%" + HttpUtility.UrlDecode(stuName) + "%' ");
            }
            if (!string.IsNullOrEmpty(school))
            {
                strWhere += string.Format(" and  School  like '%" + school + "%' ");
            }
            if (!string.IsNullOrEmpty(title))
            {
                strWhere += string.Format(" and  JobTitle  like '%" + title + "%' ");
            }
            if (!string.IsNullOrEmpty(telno))
            {
                strWhere += string.Format(" and  TelNo  like '%" + telno + "%' ");
            }
            if (!string.IsNullOrEmpty(gender) && gender != "0")
            {
                strWhere += string.Format(" and  Gender  like '%" + gender + "%' ");
            }
            if (!string.IsNullOrEmpty(idno))
            {
                strWhere += string.Format(" and  IdentityNo  like '%" + idno + "%' ");
            }
            if (!string.IsNullOrEmpty(rank))
            {
                strWhere += string.Format(" and  Rank  like '%" + rank + "%' ");
            }

            if (schoolname != "all")
            {
                strWhere += " and School in(select SchoolName from School where (SchDisName = '" + schoolname + "' or SchoolName = '" + schoolname + "') and Status = 1) ";
            }

            ds = stuBll.GetDataForExport(strWhere);
            return ds;
        }


        private void ExportStuInfo(HttpContext context)
        {
            var exXls = new ExportXls();
            var fieldsNames = new List<string>();
            fieldsNames.Add("姓名");
            fieldsNames.Add("性别");
            fieldsNames.Add("身份证号");
            fieldsNames.Add("所在学校");
            fieldsNames.Add("职称");
            fieldsNames.Add("联系电话");
            fieldsNames.Add("出生日期");
            fieldsNames.Add("民族");
            fieldsNames.Add("全日制学历");
            fieldsNames.Add("全日制毕业学校");
            fieldsNames.Add("在职学历");
            fieldsNames.Add("在职毕业学校");
            fieldsNames.Add("政治面貌");
            fieldsNames.Add("现任级别");
            fieldsNames.Add("任现任级别时间");
            fieldsNames.Add("现任职务");
            fieldsNames.Add("任职时间");
            fieldsNames.Add("手机号");
            fieldsNames.Add("继教号");
            fieldsNames.Add("主管工作");
            fieldsNames.Add("描述");

            var ds = QueryDataResultForExp(context);
            var filename = DateTime.Now.ToString("yyyy-MM-dd") + "-学员信息.xls";

            if (ds != null && ds.Tables.Count > 0)
            {
                exXls.ExportToXls(context.Response, fieldsNames, ds.Tables[0], filename);
            }
        }


        private void AddData(HttpContext context)
        {
            var result = false;
            var msg = "";
            try
            {
                var stuModel = new Models.Student();
                stuModel.StudentId = Guid.NewGuid();
                SetModelValue(stuModel, context);
                stuModel.CreateTime = DateTime.Now;
                stuModel.LastModifyTime = DateTime.Now;
                var stuBll = new BLL.Student();

                // 添加内容到sysuser表中
                var sysUserMo = new Models.SysUser();
                var sysuserbll = new BLL.SysUser();
                sysUserMo.UserRole = (int)EnumUserRole.Student;
                sysUserMo.UserName = stuModel.StuName;
                sysUserMo.UserId = stuModel.StudentId;
                //sysUserMo.UserPassWord = stuBll.GetPwd();
                sysUserMo.UserPassWord = sysuserbll.GetPwd(stuModel.IdentityNo);
                sysUserMo.UserAccount = stuBll.GetStuAccount();
                sysUserMo.IdentityNo = stuModel.IdentityNo;
                sysUserMo.CreateTime = System.DateTime.Now;

                sysuserbll.AddComeStudent(sysUserMo);

                // 添加内容到Student表中
                result = stuBll.Add(stuModel);

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
            var stuBll = new BLL.Student();
            var stuModel = stuBll.GetModel(new Guid(id));
            stuModel.LastModifyTime = DateTime.Now;
            var result = false;
            var msg = "";
            try
            {
                SetModelValue(stuModel, context);
                var isSet = context.Request["SetPwd"];
                var sysuserbll = new BLL.SysUser();
                var sysUserMo = sysuserbll.GetModel(stuModel.StudentId);
                sysUserMo.UserName = stuModel.StuName;
                sysUserMo.UserId = stuModel.StudentId;
                sysUserMo.IdentityNo = stuModel.IdentityNo;
                sysUserMo.CreateTime = System.DateTime.Now;
                if (isSet == "yes")
                {
                    sysUserMo.UserPassWord = sysuserbll.GetPwd(stuModel.IdentityNo);
                }

                sysuserbll.Update(sysUserMo);

                result = stuBll.Update(stuModel);
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

        private void EditDataPerson(string id, HttpContext context)
        {
            var stuBll = new BLL.Student();
            var stuModel = stuBll.GetModel(new Guid(id));
            stuModel.LastModifyTime = DateTime.Now;
            var result = false;
            var msg = "";
            try
            {
                SetModelValue(stuModel, context);
                var sysuserbll = new BLL.SysUser();
                var sysUserMo = sysuserbll.GetModel(stuModel.StudentId);
                sysUserMo.UserName = stuModel.StuName;
                sysUserMo.UserId = stuModel.StudentId;
                sysUserMo.IdentityNo = stuModel.IdentityNo;
                sysUserMo.CreateTime = System.DateTime.Now;
                sysUserMo.UserPassWord = context.Request["UserPassWord"]; 

                sysuserbll.Update(sysUserMo);

                result = stuBll.Update(stuModel);
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


        private void SetModelValue(Models.Student stuModel, HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request["Gender"]))
            {
                stuModel.Gender = Convert.ToInt32(context.Request["Gender"]);
            }
            stuModel.StuName = context.Request["StuName"];
            stuModel.IdentityNo = context.Request["IdentityNo"];
            stuModel.School = context.Request["School"];
            if (!string.IsNullOrEmpty(context.Request["JobTitle"]))
            {
                stuModel.JobTitle = Convert.ToInt32(context.Request["JobTitle"]);
            }

            stuModel.TelNo = context.Request["TelNo"];
            if (!string.IsNullOrEmpty(context.Request["Birthday"]))
            {
                stuModel.Birthday = DateTime.Parse(context.Request["Birthday"]);
            }
            if (!string.IsNullOrEmpty(context.Request["Nation"]))
            {
                stuModel.Nation = Convert.ToInt32(context.Request["Nation"]);
            }
            stuModel.FirstRecord = context.Request["FirstRecord"];
            stuModel.FirstSchool = context.Request["FirstSchool"];
            stuModel.LastRecord = context.Request["LastRecord"];
            stuModel.LastSchool = context.Request["LastSchool"];
            if (!string.IsNullOrEmpty(context.Request["PoliticsStaus"]))
            {
                stuModel.PoliticsStatus = Convert.ToInt32(context.Request["PoliticsStaus"]);
            }
            stuModel.Rank = context.Request["Rank"];
            if (!string.IsNullOrEmpty(context.Request["RankTime"]))
            {
                // stuModel.RankTime = DateTime.Parse(context.Request["RankTime"]);
                stuModel.RankTime = context.Request["RankTime"];
            }
            stuModel.Post = context.Request["Post"];
            if (!string.IsNullOrEmpty(context.Request["PostTime"]))
            {
              //  stuModel.PostTime = DateTime.Parse(context.Request["PostTime"]);
                stuModel.PostTime = context.Request["PostTime"] ;
            }

            stuModel.Mobile = context.Request["Mobile"];
            stuModel.TeachNo = context.Request["TeachNo"];
            stuModel.Description = context.Request["Description"];
            stuModel.ManageWork = context.Request["ManageWork"];

            if (!string.IsNullOrEmpty(context.Request["PostOptName"]))
            {
                stuModel.PostOptName = context.Request["PostOptName"];
            }
            if (!string.IsNullOrEmpty(context.Request["PostOptId"]))
            {
                stuModel.PostOptId = context.Request["PostOptId"];
            }
        }



        private void DelData(string id, HttpContext context)
        {
            var stuBll = new BLL.Student();
            var result = false;
            var msg = "";
            try
            {
                result = stuBll.Delete(new Guid(id));
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

        private void SaveClassStuData(HttpContext context)
        {
            var msg = "";
            var classId = context.Request["ClassId"];
            var isAll = context.Request["IsAll"];
            if (!string.IsNullOrEmpty(isAll)) //全选
            {
                if (isAll == "1")
                {
                    var stuBll = new BLL.Student();
                    var result = stuBll.SaveClassStuForAll(classId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                else //全不选
                {
                    var stuBll = new BLL.Student();
                    var result = stuBll.DeletClassStuForAll(classId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
            }
            else
            {
                var stuids = context.Request["StuIds"];
                var unstuids = context.Request["UnStuIds"];
                if (!string.IsNullOrEmpty(stuids))
                {
                    if (!string.IsNullOrEmpty(unstuids))
                    {
                        var unid = unstuids.Split('|');
                        if (unid.Length > 0)
                        {
                            foreach (var uid in unid)
                            {
                                if (!string.IsNullOrEmpty(uid))
                                {
                                    stuids = stuids.Replace(uid, "");
                                }
                            }
                        }
                    }
                    var stu = stuids.Split('|');
                    var stuList = new List<string>();
                    foreach (var sid in stu)
                    {
                        if (!string.IsNullOrEmpty(sid))
                        {
                            if (!stuList.Contains(sid))
                            {
                                stuList.Add(sid);
                            }
                        }
                    }
                    var stuBll = new BLL.Student();
                    var result = stuBll.SaveClassStu(stuList.ToArray(), classId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                else
                {
                    var stuBll = new BLL.Student();
                    var result = stuBll.SaveClassStu(null, classId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
            }
            context.Response.Write(msg);
        }

        private string GetPersonArchive(HttpContext context)
        {
            var ds = new DataSet();
            var stuBll = new BLL.Student();

            var stuId = context.Request["uid"];

            ds = stuBll.GetListByPage(" Status = 1   and StudentId = '" + stuId + "' ", "StuName", 1, 5, "asc");
            var str = string.Empty;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                str = JsonConvert.SerializeObject(new { rows = ds.Tables[0] });
            }
            return str;
        }

        private string GetPersonStudentInfo(HttpContext context)
        {
            var ds = new DataSet();
            var stuBll = new BLL.Student();

            var stuId = context.Request["uid"];

            ds = stuBll.GetStudentInfo(" Status = 1   and StudentId = '" + stuId + "' ", "StuName", 1, 5, "asc");
            var str = string.Empty;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                str = JsonConvert.SerializeObject(new { rows = ds.Tables[0] });
            }
            return str;
        }


        private string GetStudentTree()
        {
            var ds = new DataSet();
            var stuBll = new BLL.Student();
            ds = stuBll.GetStudentTree();

            EasyUITree EUItree = new EasyUITree();
            DataTable dt = ds.Tables[0];
            List<JsonTree> list = EUItree.initTree(dt);
            var json = JsonConvert.SerializeObject(list); //把对象集合转换成Json
            return json;
        }
    }

}