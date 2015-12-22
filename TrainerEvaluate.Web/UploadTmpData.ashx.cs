using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using TrainerEvaluate.BLL;
using TrainerEvaluate.Utility;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// UploadTmpData 的摘要说明
    /// </summary>
    public class UploadTmpData : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var dtype = context.Request["t"];
            var result = "";
            if (dtype == "cstu")
            {
                var res = ConfirmStuDatat(context.Request["fname"], out result);
            }
            else if (dtype == "ctch") //老师 
            {
                var res = ConfirmTeacherDatat(context.Request["fname"], out result);
            }
            else if (dtype == "ccor")  //课程
            {
                var res = ConfirmCourseDatat(context.Request["fname"], out result);
            }
            else if (dtype == "cshdi")   // 学区
            {
                var res = ConfirmSchoolDistrictDatat(context.Request["fname"], out result);
            }
            else if (dtype == "ctsh")   // 学校
            {
                var res = ConfirmSchoolDatat(context.Request["fname"], out result);
            }
            else
            {
                result = Upload(context, dtype);
            }
            context.Response.Write(result);
        }


        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dtype"></param>
        /// <returns></returns>
        private string Upload(HttpContext context, string dtype)
        {
            var filename = "";
            var basePath = HttpContext.Current.Server.MapPath("UploadTemplate/");

            switch (dtype)
            {
                case "stu":
                    filename = Guid.NewGuid() + "stu.xls";
                    break;
                case "tch":
                    filename = Guid.NewGuid() + "tch.xls";
                    break;
                case "cor":
                    filename = Guid.NewGuid() + "cor.xls";
                    break;
                case "shdi":
                    filename = Guid.NewGuid() + "shdi.xls";
                    break;
                case "tsh":
                    filename = Guid.NewGuid() + "tsh.xls";
                    break;
                default:
                    break;
            }
            try
            {
                if (!string.IsNullOrEmpty(filename) && context.Request.Files["Filedata"] != null)
                {
                    HttpPostedFile myFile = context.Request.Files["Filedata"];
                    int nFileLen = myFile.ContentLength;
                    byte[] myData = new byte[nFileLen];
                    myFile.InputStream.Read(myData, 0, nFileLen);
                    System.IO.FileStream newFile = new System.IO.FileStream(basePath + filename,
                        System.IO.FileMode.Create);
                    newFile.Write(myData, 0, myData.Length);
                    newFile.Close();
                    var exp = new ExportXls();
                    var dt = exp.ExcelToDataTable("", true, basePath + filename);

                    var msg = "";
                    var result = false;
                    if (dtype == "stu")
                    {
                        result = StudentImport(dt, out msg, filename);
                    }
                    else if (dtype == "tch")
                    {
                        result = TeacherImport(dt, out msg, filename);
                    }
                    else if (dtype == "cor")
                    {
                        result = CourseImport(dt, out msg, filename);
                    }
                    else if (dtype == "shdi")
                    {
                        result = SchoolDistrictImport(dt, out msg, filename);
                    }
                    else if (dtype == "tsh")
                    {
                        result = SchoolImport(dt, out msg, filename);
                    }

                    File.Delete(basePath + filename);

                    if (result)
                    {
                        return "1";
                    }
                    else
                    { 
                        return msg;
                    }

                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteFileLog(ex.Message + ex);
                return "2";
            }
        }

        #region  学区处理

        private bool ConfirmSchoolDistrictDatat(string filename, out string msg)
        {
            msg = "";
            var basePath = HttpContext.Current.Server.MapPath("UploadTemplate/");
            if (HttpContext.Current.Session["csddatat"] != null)
            {
                var dt = (DataTable)HttpContext.Current.Session["csddatat"];
                var dtDouble = (DataTable)HttpContext.Current.Session["csddatatDouble"];
                if (dt != null)
                {
                    try
                    {
                        var sqllist = new List<string>();

                        if (dtDouble != null)
                        {
                            var SchDisName = "";
                            var Description = "";
                            foreach (DataRow row in dtDouble.Rows)
                            {
                                var row1 =
                                    dt.Select(string.Format("名称='{0}'", row["SchDisName"].ToString().Trim()));
                                SchDisName = row1[0]["名称"].ToString();
                                Description = row1[0]["描述"].ToString();
                                sqllist.Add(
                                    string.Format(
                                        " update SchoolDistrict set SchDisName='{0}',Description='{1}',LastModifyTime=GETDATE()" +
                                        " where SchDisName='{0}' ", row["SchDisName"].ToString().Trim(), Description));

                                dt.Rows.Remove(row1[0]);
                                dt.AcceptChanges();
                            }
                        }
                        foreach (DataRow row in dt.Rows)
                        {
                            sqllist.Add(string.Format(
                               "insert into   SchoolDistrict (Status,SchDisId, SchDisName, Description,CreatedDate,LastModifyTime) values" +
                               "  (1,NEWID(),'{0}','{1}',GETDATE(),GETDATE())",
                               row["名称"].ToString().Trim(), row["描述"].ToString().Trim()));
                        }

                        var result = DbHelperSQL.ExecuteSqlTran(sqllist);
                        System.IO.File.Delete(basePath + filename);
                        return result != 0;
                    }
                    catch (Exception ex)
                    {
                        msg = "系统异常：" + ex.Message;
                        LogHelper.WriteLogofExceptioin(ex);
                        return false;
                    }
                }
                else
                {
                    msg = "系统异常";
                    return false;
                }
            }
            else  //session 丢了,再读一次文件。
            {
                msg = "系统异常";
                return false;
            }
        }

        private bool SchoolDistrictImport(DataTable dt, out string msg, string filename)
        {
            msg = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                var sb1 = new StringBuilder();
                foreach (DataRow row in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(sb1.ToString()))
                    {
                        sb1.Append(string.Format(",'{0}'", row["名称"].ToString().Trim()));
                    }
                    else
                    {
                        sb1.Append(string.Format("'{0}'", row["名称"].ToString().Trim()));
                    }
                }

                var sql = string.Format(" select  SchDisName  from SchoolDistrict where SchDisName in ({0}) ",
                    sb1.ToString());
                var dtresult = DbHelperSQL.Query(sql);

                if (dtresult != null && dtresult.Tables.Count > 0 && dtresult.Tables[0].Rows.Count > 0) //有重复数据要提醒
                {
                    var sb = new StringBuilder();
                    foreach (DataRow row in dtresult.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(sb.ToString()))
                        {
                            sb.Append("," + row["SchDisName"]);
                        }
                        else
                        {
                            sb.Append(row["SchDisName"]);
                        }

                    }
                    msg = "以下学区信息重复,是否覆盖？" + sb.ToString() + "|" + filename;
                    HttpContext.Current.Session.Add("csddatat", dt);
                    HttpContext.Current.Session.Add("csddatatDouble", dtresult.Tables[0]);
                    return false;
                }
                else  //直接入库
                {
                    try
                    {
                        var sqllist = new List<string>();
                        foreach (DataRow row in dt.Rows)
                        {
                            sqllist.Add(string.Format(
                                "insert into   SchoolDistrict (Status,SchDisId, SchDisName, Description,CreatedDate,LastModifyTime) values" +
                                "  (1,NEWID(),'{0}','{1}',GETDATE(),GETDATE())", 
                                row["名称"].ToString().Trim(), row["描述"].ToString().Trim()));

                        }
                        var basePath = HttpContext.Current.Server.MapPath("UploadTemplate/");
                        var result = DbHelperSQL.ExecuteSqlTran(sqllist);
                        System.IO.File.Delete(basePath + filename);
                        return result != 0;
                    }
                    catch (Exception ex)
                    {
                        msg = "系统异常：" + ex.Message;
                        LogHelper.WriteLogofExceptioin(ex);
                        return false;
                    }
                }
            }
            else
            {
                msg = "没有数据！";
                return false;
            }
        }

        #endregion

        #region 学校处理

        private bool ConfirmSchoolDatat(string filename, out string msg)
        {
            msg = "";
            var basePath = HttpContext.Current.Server.MapPath("UploadTemplate/");
            if (HttpContext.Current.Session["cshdatat"] != null)
            {
                var dt = (DataTable)HttpContext.Current.Session["cshdatat"];
                var dtDouble = (DataTable)HttpContext.Current.Session["cshdatatDouble"];
                if (dt != null)
                {
                    try
                    {
                        var sqllist = new List<string>();

                        if (dtDouble != null)
                        {
                            foreach (DataRow row in dtDouble.Rows)
                            {
                                var row1 =
                                    dt.Select(string.Format("名称='{0}'", row["SchoolName"].ToString().Trim()));

                                sqllist.Add(
                                    string.Format(
                                        " update School set SchoolName='{0}',SchDisName='{1}'," +
                                        "RunNatureCode='{2}',RunNatureName='{3}',SchoolTypeCode='{4}'," +
                                        "SchoolTypeName='{5}',AddrNum='{6}',ClassNum='{7}'," +
                                        "StudentNum='{8}',TeacherNum='{9}',PartyNum='{10}'," +
                                        "LegalName='{11}',LinkTel='{12}'," +
                                        "Description='{13}',SchDisId='{14}',LastModifyTime=GETDATE()" +
                                        " where SchoolName='{0}' ", row1[0]["名称"].ToString().Trim(),
                                        row1[0]["所属学区"].ToString().Trim(), BLL.Common.GetDicValuefromName(row1[0]["办学性质"].ToString().Trim()),
                                         row1[0]["办学性质"].ToString().Trim(), BLL.Common.GetDicValuefromName(row1[0]["学校类型"].ToString().Trim()),
                                        row1[0]["学校类型"].ToString().Trim(), row1[0]["校址数"].ToString().Trim(),
                                        row1[0]["班级数"].ToString().Trim(), row1[0]["学生数"].ToString().Trim(),
                                        row1[0]["教师数"].ToString().Trim(), row1[0]["党员数"].ToString().Trim(),
                                        row1[0]["法人名称"].ToString().Trim(), row1[0]["联系电话"].ToString().Trim(),
                                        row1[0]["描述"].ToString().Trim(), BLL.SPSchoolDistrict.GetDicValuefromName(row1[0]["所属学区"].ToString().Trim())));

                                dt.Rows.Remove(row1[0]);
                                dt.AcceptChanges();
                            }
                        }
                        foreach (DataRow row in dt.Rows)
                        {
                            sqllist.Add(string.Format(
                                "insert into School (SchoolId,SchoolName,SchDisId,SchDisName,RunNatureCode,RunNatureName,SchoolTypeCode,SchoolTypeName" +
                                ",AddrNum,ClassNum,StudentNum,TeacherNum,PartyNum,LegalName,LinkTel,Status," +
                                "Description,CreatedDate,LastModifyTime" +
                                ") values" +
                                "  (NEWID(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}'," +
                                "'{7}','{8}','{9}','{10}','{11}','{12}','{13}',1," +
                                "'{14}',GETDATE(),GETDATE())",
                                row["名称"].ToString().Trim(),
                                BLL.SPSchoolDistrict.GetDicValuefromName(row["所属学区"].ToString().Trim()), row["所属学区"].ToString().Trim(),
                                BLL.Common.GetDicValuefromName(row["办学性质"].ToString().Trim()), row["办学性质"].ToString().Trim(),
                                BLL.Common.GetDicValuefromName(row["学校类型"].ToString().Trim()), row["学校类型"].ToString().Trim(),
                                row["校址数"].ToString().Trim(), row["班级数"].ToString().Trim(),
                                row["学生数"].ToString().Trim(), row["教师数"].ToString().Trim(),
                                row["党员数"].ToString().Trim(), row["法人名称"].ToString().Trim(),
                                row["联系电话"].ToString().Trim(), row["描述"].ToString().Trim()));
                        }

                        var result = DbHelperSQL.ExecuteSqlTran(sqllist);
                        System.IO.File.Delete(basePath + filename);
                        return result != 0;
                    }
                    catch (Exception ex)
                    {
                        msg = "系统异常：" + ex.Message;
                        LogHelper.WriteLogofExceptioin(ex);
                        return false;
                    }
                }
                else
                {
                    msg = "系统异常";
                    return false;
                }
            }
            else  //session 丢了,再读一次文件。
            {
                msg = "系统异常";
                return false;
            }
        }

        private bool SchoolImport(DataTable dt, out string msg, string filename)
        {
            msg = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                var sb1 = new StringBuilder();
                foreach (DataRow row in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(sb1.ToString()))
                    {
                        sb1.Append(string.Format(",'{0}'", row["名称"].ToString().Trim()));
                    }
                    else
                    {
                        sb1.Append(string.Format("'{0}'", row["名称"].ToString().Trim()));
                    }
                }

                var sql = string.Format(" select  SchoolName  from School where SchoolName in ({0}) ",
                    sb1.ToString());
                var dtresult = DbHelperSQL.Query(sql);

                if (dtresult != null && dtresult.Tables.Count > 0 && dtresult.Tables[0].Rows.Count > 0) //有重复数据要提醒
                {
                    var sb = new StringBuilder();
                    foreach (DataRow row in dtresult.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(sb.ToString()))
                        {
                            sb.Append("," + row["SchoolName"]);
                        }
                        else
                        {
                            sb.Append(row["SchoolName"]);
                        }

                    }
                    msg = "以下学校信息重复,是否覆盖？" + sb.ToString() + "|" + filename;
                    HttpContext.Current.Session.Add("cshdatat", dt);
                    HttpContext.Current.Session.Add("cshdatatDouble", dtresult.Tables[0]);
                    return false;
                }
                else  //直接入库
                {
                    try
                    {
                        var sqllist = new List<string>();
                        foreach (DataRow row in dt.Rows)
                        {
                            sqllist.Add(string.Format(
                                "insert into School (SchoolId,SchoolName,SchDisId,SchDisName,RunNatureCode,RunNatureName,SchoolTypeCode,SchoolTypeName"+
                                ",AddrNum,ClassNum,StudentNum,TeacherNum,PartyNum,LegalName,LinkTel,Status,"+
                                "Description,CreatedDate,LastModifyTime" +
                                ") values" +
                                "  (NEWID(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}',"+
                                "'{7}','{8}','{9}','{10}','{11}','{12}','{13}',1,"+
                                "'{14}',GETDATE(),GETDATE())",
                                row["名称"].ToString().Trim(),
                                BLL.SPSchoolDistrict.GetDicValuefromName(row["所属学区"].ToString().Trim()), 
                                row["所属学区"].ToString().Trim(),
                                BLL.Common.GetDicValuefromName(row["办学性质"].ToString().Trim()),row["办学性质"].ToString().Trim(),
                                BLL.Common.GetDicValuefromName(row["学校类型"].ToString().Trim()),row["学校类型"].ToString().Trim(),
                                row["校址数"].ToString().Trim(),row["班级数"].ToString().Trim(),
                                row["学生数"].ToString().Trim(),row["教师数"].ToString().Trim(),
                                row["党员数"].ToString().Trim(),row["法人名称"].ToString().Trim(),
                                row["联系电话"].ToString().Trim(),row["描述"].ToString().Trim()));
                        }
                        var basePath = HttpContext.Current.Server.MapPath("UploadTemplate/");
                        var result = DbHelperSQL.ExecuteSqlTran(sqllist);
                        System.IO.File.Delete(basePath + filename);
                        return result != 0;
                    }
                    catch (Exception ex)
                    {
                        msg = "系统异常：" + ex.Message;
                        LogHelper.WriteLogofExceptioin(ex);
                        return false;
                    }
                }
            }
            else
            {
                msg = "没有数据！";
                return false;
            }
        }

        
        #endregion


        #region  课程信息处理

        private bool ConfirmCourseDatat(string filename, out string msg)
        {
            msg = "";
            var basePath = HttpContext.Current.Server.MapPath("UploadTemplate/");
            if (HttpContext.Current.Session["ccordatat"] != null)
            {
                var dt = (DataTable)HttpContext.Current.Session["ccordatat"];
                var dtDouble = (DataTable)HttpContext.Current.Session["ccordatatDouble"];
                if (dt != null)
                {
                    try
                    {
                        var sqllist = new List<string>();

                        if (dtDouble != null)
                        {
                            foreach (DataRow row in dtDouble.Rows)
                            {
                                var row1 =
                                    dt.Select(string.Format("课程名称='{0}' and  授课时间='{1}'", row["CourseName"].ToString().Trim(),
                                         row["TeachTime"].ToString().Trim()));
                                sqllist.Add(
                                    string.Format(
                                        " update Course set CourseName='{0}',TeacherName='{1}',TeachTime='{2}',TeachPlace='{3}',LastModifyTime=GETDATE()" +
                                        " where CourseName='{0}' and TeachTime='{2}'", row1[0]["课程名称"].ToString().Trim(), row1[0]["授课教师"].ToString().Trim(),
                                       Convert.ToDateTime(row1[0]["授课时间"].ToString().Trim()).ToString("yyyy-MM-dd"), row1[0]["授课地点"].ToString().Trim()));

                                dt.Rows.Remove(row1[0]);
                                dt.AcceptChanges();
                            }
                        }
                        var stuBll = new BLL.Student();
                        foreach (DataRow row in dt.Rows)
                        {
                            //sqllist.Add(string.Format(
                            //   "insert into   Course (Status,CourseId, CourseName,TeacherName, TeachTime,TeachPlace,CreatTime,LastModifyTime) values" +
                            //   "  (1,NEWID(),'{0}','{1}','{2}','{3}',GETDATE(),GETDATE() )", row["课程名称"].ToString().Trim(),
                            //   row["授课教师"].ToString().Trim(), Convert.ToDateTime(row["授课时间"].ToString().Trim()).ToString("yyyy-MM-dd"), row["授课地点"].ToString().Trim()));

                            sqllist.Add(string.Format(
                                 "insert into   Course (Status,CourseId, CourseName,TeacherName, TeachTime,TeachPlace,CreatTime,LastModifyTime,Type) values" +
                                 "  (1, NEWID(),'{0}','{1}','{2}','{3}',GETDATE(),GETDATE(),{4})", row["课程名称"].ToString().Trim(),
                                 row["授课教师"].ToString().Trim(), Convert.ToDateTime(row["授课时间"].ToString().Trim()).ToString("yyyy-MM-dd"),
                                 row["授课地点"].ToString().Trim(), BLL.Common.GetDicValuefromName(row["课程类型"].ToString().Trim())));
                        
                        
                        }

                        var result = DbHelperSQL.ExecuteSqlTran(sqllist);
                        System.IO.File.Delete(basePath + filename);
                        return result != 0;
                    }
                    catch (Exception ex)
                    {
                        msg = "系统异常：" + ex.Message;
                        LogHelper.WriteLogofExceptioin(ex);
                        return false;
                    }
                }
                else
                {
                    msg = "系统异常";
                    return false;
                }
            }
            else  //session 丢了,再读一次文件。
            {
                msg = "系统异常";
                return false;
            }
        }






        private bool CourseImport(DataTable dt, out string msg, string filename)
        {
            msg = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                var sb1 = new StringBuilder();
                var sb2 = new StringBuilder();
                foreach (DataRow row in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(sb1.ToString()))
                    {
                        sb1.Append(string.Format(",'{0}'", row["课程名称"].ToString().Trim()));
                        sb2.Append(string.Format(",'{0}'", row["授课时间"].ToString().Trim()));
                        // sb2.Append(string.Format(",'{0}'", Convert.ToDateTime(row["授课时间"].ToString().Trim()).ToString("yyyy-MM-dd")));
                        //sb2.Append(string.Format(",'{0}'", 
                        //    DateTime.ParseExact(row["授课时间"].ToString().Trim(), "dd/MM/yy",
                        //        System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd")));
                    }
                    else
                    {
                        sb1.Append(string.Format("'{0}'", row["课程名称"].ToString().Trim()));
                        sb2.Append(string.Format("'{0}'", row["授课时间"].ToString().Trim()));
                        // sb2.Append(string.Format("'{0}'", Convert.ToDateTime(row["授课时间"].ToString().Trim()).ToString("yyyy-MM-dd")));
                        //sb2.Append(string.Format("'{0}'",
                        //    DateTime.ParseExact(row["授课时间"].ToString().Trim(), "dd/MM/yy",
                        //        System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd")));

                        //  DateTime dt = DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    }
                }

                var sql = string.Format(" select CourseName,TeachTime   from Course where CourseName in ({0}) and TeachTime in({1})   ",
                    sb1.ToString(), sb2.ToString());
                var dtresult = DbHelperSQL.Query(sql);

                if (dtresult != null && dtresult.Tables.Count > 0 && dtresult.Tables[0].Rows.Count > 0) //有重复数据要提醒
                {
                    var sb = new StringBuilder();
                    foreach (DataRow row in dtresult.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(sb.ToString()))
                        {
                            sb.Append("," + row["CourseName"]);
                        }
                        else
                        {
                            sb.Append(row["CourseName"]);
                        }

                    }
                    msg = "以下课程信息重复,是否覆盖？" + sb.ToString() + "|" + filename;
                    HttpContext.Current.Session.Add("ccordatat", dt);
                    HttpContext.Current.Session.Add("ccordatatDouble", dtresult.Tables[0]);
                    return false;
                }
                else  //直接入库
                {
                    try
                    {
                        var sqllist = new List<string>();
                        foreach (DataRow row in dt.Rows)
                        {
                            sqllist.Add(string.Format(
                                "insert into   Course (Status,CourseId, CourseName,TeacherName, TeachTime,TeachPlace,CreatTime,LastModifyTime,Type,Description) values" +
                                "  (1, NEWID(),'{0}','{1}','{2}','{3}',GETDATE(),GETDATE(),{4},'{5}')", row["课程名称"].ToString().Trim(),
                                row["授课教师"].ToString().Trim(), Convert.ToDateTime(row["授课时间"].ToString().Trim()).ToString("yyyy-MM-dd"),
                                row["授课地点"].ToString().Trim(), BLL.Common.GetDicValuefromName(row["课程类型"].ToString().Trim()), row["描述"].ToString().Trim()));

                        }

                        var basePath = HttpContext.Current.Server.MapPath("UploadTemplate/");
                        var result = DbHelperSQL.ExecuteSqlTran(sqllist);
                        System.IO.File.Delete(basePath + filename);
                        return result != 0;
                    }
                    catch (Exception ex)
                    {
                        msg = "系统异常：" + ex.Message;
                        LogHelper.WriteLogofExceptioin(ex);
                        return false;
                    }
                }
            }
            else
            {
                msg = "没有数据！";
                return false;
            }
        }



        #endregion

        #region  教师信息处理

        private bool ConfirmTeacherDatat(string filename, out string msg)
        {
            msg = "";
            var basePath = HttpContext.Current.Server.MapPath("UploadTemplate/");
            if (HttpContext.Current.Session["ctchdatat"] != null)
            {
                var dt = (DataTable)HttpContext.Current.Session["ctchdatat"];
                var dtDouble = (DataTable)HttpContext.Current.Session["ctchdatatDouble"];
                if (dt != null)
                {
                    try
                    {
                        var sqllist = new List<string>();

                        if (dtDouble != null)
                        {
                            var IdentityNo = "";
                            var Gender = 0;
                            var Dept = "";
                            var Title = "";
                            var Post = "";
                            var Research = "";
                            var Mobile = "";
                            var Description = "";
                            foreach (DataRow row in dtDouble.Rows)
                            {
                                var row1 =
                                    dt.Select(string.Format("姓名='{0}' and  身份证号='{1}'", row["TeacherName"].ToString().Trim(),
                                        row["IdentityNo"].ToString().Trim()));
                                IdentityNo = row1[0]["身份证号"].ToString();
                                Gender = row1[0]["性别"].ToString().Trim() == "男" ? 1 : 2;
                                Dept = row1[0]["所在单位"].ToString();
                                Title = row1[0]["职称"].ToString();
                                Post = row1[0]["职务"].ToString();
                                Research = row1[0]["研究方向"].ToString();
                                Mobile = row1[0]["手机号"].ToString();
                                Description = row1[0]["描述"].ToString();
                                sqllist.Add(
                                    string.Format(
                                        " update Teacher set TeacherName='{0}',IdentityNo='{1}',Gender={2},Dept='{3}',Title='{4}', Post = '{5}',Research = '{6}',Mobile = '{7}',Description = '{8}',LastModifyTime=GETDATE()" +
                                        " where TeacherName='{0}' and IdentityNo='{1}'", row["TeacherName"].ToString().Trim(), IdentityNo,
                                        Gender, Dept, Title));

                                dt.Rows.Remove(row1[0]);
                                dt.AcceptChanges();
                            }
                        }
                        foreach (DataRow row in dt.Rows)
                        {
                            //sqllist.Add(string.Format(
                            //   "insert into   Teacher (TeacherId, IdentityNo,TeacherName,Gender,Title,Dept,CreateTime,LastModifyTime, Post, Research, Mobile, Description) values" +
                            //   "  ( NEWID(),'{0}','{1}',{2},'{3}','{4}',GETDATE(),GETDATE(), '{5}', '{6}','{7}', '{8}')", row["身份证号"].ToString().Trim(),
                            //   row["姓名"].ToString().Trim(), row["性别"].ToString().Trim() == "男" ? 1 : 2, row["职称"].ToString().Trim(), row["所在单位"].ToString().Trim(), row["职务"].ToString().Trim(),
                            //   row["研究方向"].ToString().Trim(), row["手机号"].ToString().Trim(), row["描述"].ToString().Trim()));


                            sqllist.Add(string.Format(
                               "insert into   Teacher (Status,TeacherId, IdentityNo,TeacherName,Gender,Title,Dept,CreateTime,LastModifyTime, Post, Research, Mobile, Description) values" +
                               "  (1,NEWID(),'{0}','{1}',{2},{3},'{4}',GETDATE(),GETDATE(), '{5}', '{6}','{7}', '{8}')", row["身份证号"].ToString().Trim(),
                               row["姓名"].ToString().Trim(), BLL.Common.GetDicValuefromName(row["性别"].ToString().Trim()), BLL.Common.GetDicValuefromName(row["职称"].ToString().Trim()), row["所在单位"].ToString().Trim(), row["职务"].ToString().Trim(),
                              row["研究方向"].ToString().Trim(), row["手机号"].ToString().Trim(), row["描述"].ToString().Trim()));


                        }

                        var result = DbHelperSQL.ExecuteSqlTran(sqllist);
                        System.IO.File.Delete(basePath + filename);
                        return result != 0;
                    }
                    catch (Exception ex)
                    {
                        msg = "系统异常：" + ex.Message;
                        LogHelper.WriteLogofExceptioin(ex);
                        return false;
                    }
                }
                else
                {
                    msg = "系统异常";
                    return false;
                }
            }
            else  //session 丢了,再读一次文件。
            {
                msg = "系统异常";
                return false;
            }
        }



        private bool TeacherImport(DataTable dt, out string msg, string filename)
        {
            msg = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                var sb1 = new StringBuilder();
                var sb2 = new StringBuilder();
                foreach (DataRow row in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(sb1.ToString()))
                    {
                        sb1.Append(string.Format(",'{0}'", row["姓名"].ToString().Trim()));
                        sb2.Append(string.Format(",'{0}'", row["身份证号"].ToString().Trim()));
                    }
                    else
                    {
                        sb1.Append(string.Format("'{0}'", row["姓名"].ToString().Trim()));
                        sb2.Append(string.Format("'{0}'", row["身份证号"].ToString().Trim()));
                    }
                }

                var sql = string.Format(" select  TeacherName,IdentityNo  from Teacher where TeacherName in ({0}) and IdentityNo in({1})   ",
                    sb1.ToString(), sb2.ToString());
                var dtresult = DbHelperSQL.Query(sql);

                if (dtresult != null && dtresult.Tables.Count > 0 && dtresult.Tables[0].Rows.Count > 0) //有重复数据要提醒
                {
                    var sb = new StringBuilder();
                    foreach (DataRow row in dtresult.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(sb.ToString()))
                        {
                            sb.Append("," + row["TeacherName"]);
                        }
                        else
                        {
                            sb.Append(row["TeacherName"]);
                        }

                    }
                    msg = "以下人员信息重复,是否覆盖？" + sb.ToString() + "|" + filename;
                    HttpContext.Current.Session.Add("ctchdatat", dt);
                    HttpContext.Current.Session.Add("ctchdatatDouble", dtresult.Tables[0]);
                    return false;
                }
                else  //直接入库
                {
                    try
                    {
                        var stuBll = new BLL.Student();
                        var sqllist = new List<string>();
                        foreach (DataRow row in dt.Rows)
                        {
                            sqllist.Add(string.Format(
                                "insert into   Teacher (Status,TeacherId, IdentityNo,TeacherName,Gender,Title,Dept,CreateTime,LastModifyTime, Post, Research, Mobile, Description) values" +
                                "  (1,NEWID(),'{0}','{1}',{2},{3},'{4}',GETDATE(),GETDATE(), '{5}', '{6}','{7}', '{8}')", row["身份证号"].ToString().Trim(),
                                row["姓名"].ToString().Trim(),BLL.Common.GetDicValuefromName(row["性别"].ToString().Trim()),BLL.Common.GetDicValuefromName(row["职称"].ToString().Trim()), row["所在单位"].ToString().Trim(), row["职务"].ToString().Trim(),
                               row["研究方向"].ToString().Trim(), row["手机号"].ToString().Trim(), row["描述"].ToString().Trim()));

                        } 
                        var basePath = HttpContext.Current.Server.MapPath("UploadTemplate/");
                        var result = DbHelperSQL.ExecuteSqlTran(sqllist);
                        System.IO.File.Delete(basePath + filename);
                        return result != 0;
                    }
                    catch (Exception ex)
                    {
                        msg = "系统异常：" + ex.Message;
                        LogHelper.WriteLogofExceptioin(ex);
                        return false;
                    }
                }
            }
            else
            {
                msg = "没有数据！";
                return false;
            }
        }



        #endregion


        #region 学生信息处理
        /// <summary>
        /// 确认后返回
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool ConfirmStuDatat(string filename, out string msg)
        {
            msg = "";
            var basePath = HttpContext.Current.Server.MapPath("UploadTemplate/");
            if (HttpContext.Current.Session["cstudatat"] != null)
            {
                var dt = (DataTable)HttpContext.Current.Session["cstudatat"];
                var dtDouble = (DataTable)HttpContext.Current.Session["cstudatatDouble"];
                if (dt != null)
                {
                    try
                    {
                        var sqllist = new List<string>();

                        if (dtDouble != null)
                        {
                            var IdentityNo = "";
                            var Gender = 0;
                            var School = "";
                            var Title = "";
                            var TelNo = "";
                            var Birthday = "";
                            var Nation = 0;
                            var FirstRecord = "";
                            var FirstSchool = "";
                            var LastRecord = "";
                            var LastSchool = "";
                            var PoliticsStatus = 0;
                            var Rank = "";
                            var RankTime = "";
                            var Post = "";
                            var PostTime = "";
                            var Mobile = "";
                            // var TeachNo = 0;
                            var Description = "";
                            foreach (DataRow row in dtDouble.Rows)
                            {
                                var row1 =
                                    dt.Select(string.Format("姓名='{0}' and  联系电话='{1}'", row[0].ToString().Trim(),
                                        row[1].ToString().Trim()));
                                IdentityNo = row1[0]["身份证号"].ToString();
                                Gender = row1[0]["性别"].ToString().Trim() == "男" ? 1 : 2;
                                School = row1[0]["所在学校"].ToString();
                                Title = row1[0]["职务"].ToString();
                                TelNo = row1[0]["电话号码"].ToString();
                                /*
                                Birthday = row1[0]["出生日期"].ToString();
                                Nation = row1[0]["民族"].ToString().Trim() == "汉" ? 1 : 2;
                                FirstRecord = row1[0]["全日制学历"].ToString().Trim();
                                FirstSchool = row1[0]["全日制学校"].ToString().Trim();
                                LastRecord = row1[0]["在职学历"].ToString().Trim();
                                LastSchool = row1[0]["在职学校"].ToString().Trim();
                                PoliticsStatus = row1[0]["政治面貌"].ToString().Trim() == "党员" ? 1 : 2;

                                Rank = row1[0]["现任级别"].ToString().Trim();
                                RankTime = row1[0]["任现任级别时间"].ToString().Trim();
                                Post = row1[0]["现任职务"].ToString().Trim();
                                PostTime = row1[0]["任职时间"].ToString().Trim();
                                Mobile = row1[0]["手机号"].ToString().Trim();
                                Description = row1[0]["描述"].ToString().Trim();
                                  sqllist.Add(
                                    string.Format(
                                        " update Student set StuName='{0}',IdentityNo='{1}',Gender={2},School='{3}',Title='{4}',TelNo='{5}',Birthday='{6}',Nation='{7}',"+
                                "FirstRecord='{8}',FirstSchool='{9}',LastRecord='{10}',LastSchool='{11}',PoliticsStatus='{12}',Rank='{13}',RankTime='{14}',Post='{15}',PostTime='{16}',Mobile='{17}',Description='{18}',LastModifyTime=GETDATE()" +
                                        " where StuName='{0}' and TelNo='{5}'", row[0].ToString().Trim(), IdentityNo,
                                        Gender, School, Title, row[1].ToString()));
                               */
                                sqllist.Add(
                                    string.Format(
                                        " update Student set StuName='{0}',IdentityNo='{1}',Gender={2},School='{3}',Title='{4}',TelNo='{5}',LastModifyTime=GETDATE()" +
                                        " where StuName='{0}' and TelNo='{5}'", row[0].ToString().Trim(), IdentityNo,
                                        Gender, School, Title, row[1].ToString()));

                                dt.Rows.Remove(row1[0]);
                                dt.AcceptChanges();
                            }
                        }
                        var stuBll = new BLL.Student();
                        var startAcount = stuBll.GetStuAccount();
                        var startNo = startAcount.Substring(startAcount.Length - 3, 3);
                        //  "HB" + DateTime.Now.Year 
                        var i = 0;
                        i = i + Convert.ToInt32(startNo);
                        var uid = Guid.Empty;


                        foreach (DataRow row in dt.Rows)
                        {
                            Random pwd = new Random(GetRandomSeed());
                            i++;
                            uid = Guid.NewGuid();
                            //sqllist.Add(string.Format(
                            //    "insert into   Student (StudentId,IdentityNo,StuName,Gender,School,Title,TelNo,CreateTime,LastModifyTime) values" +
                            //    "  ( '{6}','{0}','{1}',{2},'{3}','{4}','{5}',GETDATE(),GETDATE() )",
                            //    row[2].ToString().Trim(),
                            //    row[0].ToString().Trim(), row[1].ToString().Trim() == "男" ? 1 : 2,
                            //    row[3].ToString().Trim(), row[4].ToString().Trim(), row[5].ToString().Trim(), uid));


                            sqllist.Add(string.Format(
                            "insert into   Student (StudentId,StuName,Gender,IdentityNo,School,JobTitle,TelNo,Birthday,Nation,FirstRecord,FirstSchool," +
                                                   "LastRecord,LastSchool,PoliticsStatus,Rank,RankTime,Post,PostTime,Mobile,TeachNo,Description," +
                                                   "CreateTime,LastModifyTime,Status) values" +
                                                   "( '{0}','{1}',{2},'{3}','{4}',{5},'{6}','{7}',{8},'{9}','{10}'," +
                                                   "'{11}','{12}',{13},'{14}','{15}','{16}','{17}','{18}','{19}','{20}',GETDATE(),GETDATE(),1 )",
                                                    uid, row["姓名"].ToString().Trim(), BLL.Common.GetDicValuefromName(row["性别"].ToString().Trim()),
                                                     row["身份证号"].ToString().Trim(), row["所在单位"].ToString().Trim(), BLL.Common.GetDicValuefromName(row["职称"].ToString().Trim()),
                                                     row["联系电话"].ToString().Trim(), row["出生日期"].ToString().Trim(), BLL.Common.GetDicValuefromName(row["民族"].ToString().Trim()),
                                                     row["全日制学历"].ToString().Trim(), row["全日制学校"].ToString().Trim(), row["在职学历"].ToString().Trim(),
                                                     row["在职学校"].ToString().Trim(), BLL.Common.GetDicValuefromName(row["政治面貌"].ToString().Trim()), row["现任级别"].ToString().Trim(), row["任现任级别时间"].ToString().Trim(),
                                                     row["现任职务"].ToString().Trim(), row["任职时间"].ToString().Trim(), row["手机号"].ToString().Trim(),
                                                     row["继教号"].ToString().Trim(), row["描述"].ToString().Trim()));




                            sqllist.Add(string.Format(" insert into SysUser (UserId,UserRole,UserName,UserPassWord,CreateTime,UserAccount)" +
                                                      " values('{3}',1,'{0}','{1}',GETDATE(),'{2}') ", row[0].ToString().Trim(), pwd.Next(999999).ToString(), "HB" + DateTime.Now.Year + i.ToString().PadLeft(3, '0'), uid));

                        }

                        var result = DbHelperSQL.ExecuteSqlTran(sqllist);
                        System.IO.File.Delete(basePath + filename);
                        return result != 0;
                    }
                    catch (Exception ex)
                    {
                        msg = "系统异常：" + ex.Message;
                        LogHelper.WriteLogofExceptioin(ex);
                        return false;
                    }
                }
                else
                {
                    msg = "系统异常";
                    return false;
                }
            }
            else  //session 丢了,再读一次文件。
            {
                msg = "系统异常";
                return false;
            }
        }


        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }



        private bool StudentImport(DataTable dt, out string msg, string filename)
        {
            msg = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                var sb1 = new StringBuilder();
                var sb2 = new StringBuilder();
                foreach (DataRow row in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(sb1.ToString()))
                    {
                        sb1.Append(string.Format(",'{0}'", row[0].ToString().Trim()));
                        sb2.Append(string.Format(",'{0}'", row[5].ToString().Trim()));
                    }
                    else
                    {
                        sb1.Append(string.Format("'{0}'", row[0].ToString().Trim()));
                        sb2.Append(string.Format("'{0}'", row[5].ToString().Trim()));
                    }
                }

                var sql = string.Format(" select StuName,TelNo   from Student where StuName in ({0}) and TelNo in({1})   ",
                    sb1.ToString(), sb2.ToString());
                var dtresult = DbHelperSQL.Query(sql);

                if (dtresult != null && dtresult.Tables.Count > 0 && dtresult.Tables[0].Rows.Count > 0) //有重复数据要提醒
                {
                    var sb = new StringBuilder();
                    foreach (DataRow row in dtresult.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(sb.ToString()))
                        {
                            sb.Append("," + row["StuName"]);
                        }
                        else
                        {
                            sb.Append(row["StuName"]);
                        }

                    }
                    msg = "以下人员信息重复,是否覆盖？" + sb.ToString() + "|" + filename;
                    HttpContext.Current.Session.Add("cstudatat", dt);
                    HttpContext.Current.Session.Add("cstudatatDouble", dtresult.Tables[0]);
                    return false;
                }
                else  //直接入库
                {
                    try
                    {
                        var stuBll = new BLL.Student();
                        var sqllist = new List<string>();
                        var startAcount = stuBll.GetStuAccount();
                        var startNo = startAcount.Substring(startAcount.Length - 3, 3);
                        //  "HB" + DateTime.Now.Year 
                        var i = 0;
                        i = i + Convert.ToInt32(startNo);
                        var uid = Guid.Empty;
                        foreach (DataRow row in dt.Rows)
                        {
                            Random pwd = new Random(GetRandomSeed());
                            uid = Guid.NewGuid();
                            i++;
                            //sqllist.Add(string.Format(
                            //    "insert into   Student (StudentId,IdentityNo,StuName,Gender,School,Title,TelNo,CreateTime,LastModifyTime) values" +
                            //    "  ( '{6}','{0}','{1}',{2},'{3}','{4}','{5}',GETDATE(),GETDATE() )", row[2].ToString().Trim(),
                            //    row[0].ToString().Trim(), row[1].ToString().Trim() == "男" ? 1 : 2, row[3].ToString().Trim(), row[4].ToString().Trim(), row[5].ToString().Trim(), uid));
 

                            sqllist.Add(string.Format(
                                "insert into   Student (StudentId,StuName,Gender,IdentityNo,School,JobTitle,TelNo,Birthday,Nation,FirstRecord,FirstSchool," +
                                                       "LastRecord,LastSchool,PoliticsStatus,Rank,RankTime,Post,PostTime,Mobile,TeachNo,Description," +
                                                       "CreateTime,LastModifyTime,Status) values" +
                                                       "( '{0}','{1}',{2},'{3}','{4}',{5},'{6}','{7}',{8},'{9}','{10}'," +
                                                       "'{11}','{12}',{13},'{14}','{15}','{16}','{17}','{18}','{19}','{20}',GETDATE(),GETDATE(),1 )",
                                                        uid, row["姓名"].ToString().Trim(),BLL.Common.GetDicValuefromName(row["性别"].ToString().Trim()),
                                                         row["身份证号"].ToString().Trim(), row["所在单位"].ToString().Trim(), BLL.Common.GetDicValuefromName(row["职称"].ToString().Trim()),
                                                         row["联系电话"].ToString().Trim(), row["出生日期"].ToString().Trim(), BLL.Common.GetDicValuefromName(row["民族"].ToString().Trim()),
                                                         row["全日制学历"].ToString().Trim(), row["全日制学校"].ToString().Trim(), row["在职学历"].ToString().Trim(),
                                                         row["在职学校"].ToString().Trim(), BLL.Common.GetDicValuefromName(row["政治面貌"].ToString().Trim()), row["现任级别"].ToString().Trim(), row["任现任级别时间"].ToString().Trim(),
                                                         row["现任职务"].ToString().Trim(), row["任职时间"].ToString().Trim(), row["手机号"].ToString().Trim(),
                                                         row["继教号"].ToString().Trim(), row["描述"].ToString().Trim()));


                            sqllist.Add(string.Format(" insert into SysUser (UserId,UserRole,UserName,UserPassWord,CreateTime,UserAccount)" +
                                                         " values('{3}',1,'{0}','{1}',GETDATE(),'{2}') ", row[0].ToString().Trim(), pwd.Next(999999).ToString(), "HB" + DateTime.Now.Year + i.ToString().PadLeft(3, '0'), uid));


                        }

                        var basePath = HttpContext.Current.Server.MapPath("UploadTemplate/");
                        var result = DbHelperSQL.ExecuteSqlTran(sqllist);
                        System.IO.File.Delete(basePath + filename);
                        if (result == 0)
                        {
                            msg = "入库失败！";
                        }
                        return result != 0;
                    }
                    catch (Exception ex)
                    {
                        msg = "系统异常：" + ex.Message;
                        LogHelper.WriteLogofExceptioin(ex);
                        return false;
                    }
                }
            }
            else
            {
                msg = "没有数据！";
                return false;
            }
        }

        #endregion





        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}