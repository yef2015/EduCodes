﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TrainerEvaluate.BLL;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// StudentsInfo 的摘要说明
    /// </summary>
    public class StudentsInfo : IHttpHandler
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
                    EditData(id,context);
                    break;
                case "d":
                    DelData(id,context);
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
                case "sc":
                    SaveClassStuData(context); 
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
            var stuBll = new BLL.Student();
            var page =Convert.ToInt32(context.Request["page"]) ;
            var rows =Convert.ToInt32(context.Request["rows"]) ;
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "StuName" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];

            if (sort == "GenderName")
            {
                sort = "Gender";
            }

            var startIndex = (page - 1)*rows+1;
            var endIndex = startIndex + rows-1;

            var num = stuBll.GetRecordCount(" Status=1 ");
            ds = stuBll.GetListByPage("Status = 1", sort, startIndex, endIndex,order);
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

            var startIndex = (page - 1)*rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = stuBll.GetRecordCount(" Status=1 ");
            var ds = stuBll.GetClassStuListByPage(coId, "ck", startIndex, endIndex);
            var str = JsonConvert.SerializeObject(new {total = num, rows = ds.Tables[0]});
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


            var stuName = string.IsNullOrEmpty(context.Request["name"]) ? "" : context.Request["name"].Trim();
            var school = string.IsNullOrEmpty(context.Request["sch"]) ? "" : context.Request["sch"].Trim();
            var title = string.IsNullOrEmpty(context.Request["title"]) ? "" : context.Request["title"].Trim();
            var telno = string.IsNullOrEmpty(context.Request["telno"]) ? "" : context.Request["telno"].Trim();
            var gender = string.IsNullOrEmpty(context.Request["gender"]) ? "" : context.Request["gender"].Trim();
            var idno = string.IsNullOrEmpty(context.Request["idno"]) ? "" : context.Request["idno"].Trim();
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
            var strWhere = "";
            if (!string.IsNullOrEmpty(stuName))
            {
                strWhere = string.Format(" StuName like '%" + stuName + "%' ");
            }
            if (!string.IsNullOrEmpty(school))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  School  like '%" + school + "%' ");
                }
                else
                {
                    strWhere = string.Format(" School  like '%" + school + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(title))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  JobTitle  like '%" + title + "%' ");
                }
                else
                {
                    strWhere = string.Format(" JobTitle  like '%" + title + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(telno))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  TelNo  like '%" + telno + "%' ");
                }
                else
                {
                    strWhere = string.Format(" TelNo  like '%" + telno + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(gender)&& gender!="0")
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Gender  like '%" + gender + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Gender  like '%" + gender + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(idno))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  IdentityNo  like '%" + idno + "%' ");
                }
                else
                {
                    strWhere = string.Format(" IdentityNo  like '%" + idno + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere += string.Format(" and  status = 1 ");
            }
            else
            {
                strWhere += string.Format("status = 1 ");
            }
            /*
            if (!string.IsNullOrEmpty(birthday))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Birthday  like '%" + birthday + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Birthday  like '%" + birthday + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(nation))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Nation  like '%" + nation + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Nation  like '%" + nation + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(firstrecord))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  FirstRecord  like '%" + firstrecord + "%' ");
                }
                else
                {
                    strWhere = string.Format(" FirstRecord  like '%" + firstrecord + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(firstschool))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  FirstSchool  like '%" + firstschool + "%' ");
                }
                else
                {
                    strWhere = string.Format(" FirstSchool  like '%" + firstschool + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(lastrecord))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  LastRecord  like '%" + lastrecord + "%' ");
                }
                else
                {
                    strWhere = string.Format(" LastRecord  like '%" + lastrecord + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(lastschool))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and LastSchool  like '%" + lastschool + "%' ");
                }
                else
                {
                    strWhere = string.Format(" LastSchool  like '%" + lastschool + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(policticsstatus))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and PoliticsStatus  like '%" + policticsstatus + "%' ");
                }
                else
                {
                    strWhere = string.Format(" PoliticsStatus  like '%" + policticsstatus + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(rank))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and Rank  like '%" + rank + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Rank  like '%" + rank + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(ranktime))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and RankTime  like '%" + ranktime + "%' ");
                }
                else
                {
                    strWhere = string.Format(" RankTime  like '%" + ranktime + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(post))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and Post  like '%" + post + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Post like '%" + post + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(mobile))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and Mobile  like '%" + mobile + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Mobile like '%" + mobile + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(teachno))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and Teachno  like '%" + teachno + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Teachno like '%" + teachno + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(description))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and Description  like '%" + description + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Description like '%" + description + "%' ");
                }
            }
             * */
           
            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "StuName" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];
            if (sort == "GenderName")
            {
                sort = "Gender";
            }
            var startIndex = (page - 1)*rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = stuBll.GetRecordCount(strWhere);
            ds = stuBll.GetListByPage(strWhere, sort, startIndex, endIndex,order); 

            var str = JsonConvert.SerializeObject(new {total = num, rows = ds.Tables[0]});
            context.Response.Write(str);
        }




        private DataSet QueryDataResultForExp(HttpContext context)
        {
            var stuName = string.IsNullOrEmpty(context.Request["name"]) ? "" : context.Request["name"].Trim();
            var school = string.IsNullOrEmpty(context.Request["sch"]) ? "" : context.Request["sch"].Trim();
            var title = string.IsNullOrEmpty(context.Request["title"]) ? "" : context.Request["title"].Trim();
            var telno = string.IsNullOrEmpty(context.Request["telno"]) ? "" : context.Request["telno"].Trim();
            var gender = string.IsNullOrEmpty(context.Request["gender"]) ? "" : context.Request["gender"].Trim();
            var idno = string.IsNullOrEmpty(context.Request["idno"]) ? "" : context.Request["idno"].Trim();
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
            var strWhere = "";
            if (!string.IsNullOrEmpty(stuName))
            {
                strWhere = string.Format(" StuName like '%" + HttpUtility.UrlDecode(stuName) + "%' ");
            }
            if (!string.IsNullOrEmpty(school))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  School  like '%" + school + "%' ");
                }
                else
                {
                    strWhere = string.Format(" School  like '%" + school + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(title))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  JobTitle  like '%" + title + "%' ");
                }
                else
                {
                    strWhere = string.Format(" JobTitle  like '%" + title + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(telno))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  TelNo  like '%" + telno + "%' ");
                }
                else
                {
                    strWhere = string.Format(" TelNo  like '%" + telno + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(gender) && gender != "0")
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Gender  like '%" + gender + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Gender  like '%" + gender + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(idno))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  IdentityNo  like '%" + idno + "%' ");
                }
                else
                {
                    strWhere = string.Format(" IdentityNo  like '%" + idno + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere += string.Format(" and  status = 1 ");
            }
            else
            {
                strWhere += string.Format("status = 1 ");
            }
            
            /*
            if (!string.IsNullOrEmpty(birthday))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Birthday  like '%" + birthday + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Birthday  like '%" + birthday + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(nation))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Nation  like '%" + nation + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Nation  like '%" + nation + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(firstrecord))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  FirstRecord  like '%" + firstrecord + "%' ");
                }
                else
                {
                    strWhere = string.Format(" FirstRecord  like '%" + firstrecord + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(firstschool))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  FirstSchool  like '%" + firstschool + "%' ");
                }
                else
                {
                    strWhere = string.Format(" FirstSchool  like '%" + firstschool + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(lastrecord))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  LastRecord  like '%" + lastrecord + "%' ");
                }
                else
                {
                    strWhere = string.Format(" LastRecord  like '%" + lastrecord + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(lastschool))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and LastSchool  like '%" + lastschool + "%' ");
                }
                else
                {
                    strWhere = string.Format(" LastSchool  like '%" + lastschool + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(policticsstatus))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and PoliticsStatus  like '%" + policticsstatus + "%' ");
                }
                else
                {
                    strWhere = string.Format(" PoliticsStatus  like '%" + policticsstatus + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(rank))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and Rank  like '%" + rank + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Rank  like '%" + rank + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(ranktime))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and RankTime  like '%" + ranktime + "%' ");
                }
                else
                {
                    strWhere = string.Format(" RankTime  like '%" + ranktime + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(post))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and Post  like '%" + post + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Post like '%" + post + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(mobile))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and Mobile  like '%" + mobile + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Mobile like '%" + mobile + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(teachno))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and Teachno  like '%" + teachno + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Teachno like '%" + teachno + "%' ");
                }
            }

            if (!string.IsNullOrEmpty(description))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and Description  like '%" + description + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Description like '%" + description + "%' ");
                }
            }
             * */

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
            fieldsNames.Add("所在单位");
            fieldsNames.Add("职称");
            fieldsNames.Add("联系电话");
            fieldsNames.Add("出生日期");
            fieldsNames.Add("民族");
            fieldsNames.Add("全日制学历");
            fieldsNames.Add("全日制学校");
            fieldsNames.Add("在职学历");
            fieldsNames.Add("在职学校");
            fieldsNames.Add("政治面貌"); 
            fieldsNames.Add("现任级别");
            fieldsNames.Add("任现任级别时间");
            fieldsNames.Add("现任职务"); 
            fieldsNames.Add("任职时间");
            fieldsNames.Add("手机号"); 
            fieldsNames.Add("继教号");
            fieldsNames.Add("描述");
             
            var ds = QueryDataResultForExp(context); 
            var filename =DateTime.Now.ToString("yyyy-MM-dd")+ "-学员信息.xls";

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
                LogHelper.WriteLogofExceptioin(new Exception("begin"));
                var stuModel = new Models.Student();
                stuModel.StudentId = Guid.NewGuid();
                SetModelValue(stuModel, context);
                stuModel.CreateTime = DateTime.Now;
                stuModel.LastModifyTime = DateTime.Now;
                var stuBll = new BLL.Student(); 

                var sysUserMo = new Models.SysUser();
                var sysuserbll = new BLL.SysUser();
                sysUserMo.UserRole = (int) EnumUserRole.Student;
                sysUserMo.UserName = stuModel.StuName;
                sysUserMo.UserId = stuModel.StudentId;
                sysUserMo.UserPassWord = stuBll.GetPwd();
                sysUserMo.UserAccount = stuBll.GetStuAccount();

                sysuserbll.Add(sysUserMo);
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
                SetModelValue(stuModel,context);  
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
                stuModel.RankTime = DateTime.Parse(context.Request["RankTime"]);
            } 
            stuModel.Post = context.Request["Post"];
            if (!string.IsNullOrEmpty(context.Request["PostTime"]))
            {
                stuModel.PostTime = DateTime.Parse(context.Request["PostTime"]);
            }
          
            stuModel.Mobile = context.Request["Mobile"];
            stuModel.TeachNo = context.Request["TeachNo"];
            stuModel.Description = context.Request["Description"];
       
        }



        private void DelData(string id,HttpContext context)
        {
            var stuBll = new BLL.Student(); 
            var result = false;
            var msg = "";
            try
            {　   
                result= stuBll.Delete(new Guid(id)); 
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







    }
    
}