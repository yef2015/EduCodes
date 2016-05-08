using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
                case "exdi":
                    // 导出班级详细信息
                    ExportClassDetailsInfo(context);
                    break;
                case "stcl":
                    var stcla = GetClassInfoByStudentId(context);
                    context.Response.Write(stcla);
                    break;
                case "stcp":
                    var stclp = GetProfessExperByTeacherId(context);
                    context.Response.Write(stclp);
                    break;
                case "pah":
                    var strInfo= GetClassInfoByClassId(context);
                    context.Response.Write(strInfo);
                    break;   
                case "ppt":
                    var ppts= GetClasspptByClassId(context);
                    context.Response.Write(ppts);
                    break;
                case "upppts":
                    var upppts = UploadPPTsByClassId(context);
                    context.Response.Write(upppts);
                    break;    
                case "delppts":
                    var delppts =DelPPTs(context);
                    context.Response.Write(delppts);
                    break;
                case "downppt":
                    DownloadPPTs(context); 
                    break;
                case "downpptall":
                    DownloadPPTsAll(context); 
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
            var classBll = new BLL.Class();
            ds = classBll.GetAllList();

            if (ds != null && ds.Tables.Count > 0)
            {
                var str = new StringBuilder("[");

                str.Append("{\"ClassId\": \"0000\",");
                str.Append("\"ClassName\": \"全部\"},");

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    str.Append("{\"ClassId\": \"" + row["ID"] + "\",");
                    str.Append("\"ClassName\": \"" + row["Name"] + "\"},");
                }
                str.Remove(str.Length - 1, 1);
                str.Append("]");

                context.Response.Write(str.ToString());
            }
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



            var strWhere = " Status=1 and YearLevel = '" + classYear + "' ";

            //项目负责人只能看到自己的负责的项目
            if (Profile.CurrentUser.UserRole == 3)
            {
                var role = new BLL.Roles();
                var result = role.GetCurrentUserIsCharge(Profile.CurrentUser.UserId);
                if (result)
                {
                    strWhere += string.Format(" and  Teacher like '%{0}%' ", Profile.CurrentUser.UserName);
                }
            }  
            var num = classBll.GetRecordCount(strWhere);
            ds = classBll.GetListByPage(strWhere, sort, startIndex, endIndex, order);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }

        private void Query(HttpContext context)
        {
            var name = context.Request["Name"];
            var description = context.Request["Description"];
            var level = context.Request["Level"];
            var type = context.Request["Type"];
            var classYear = System.DateTime.Now.Year.ToString();
            if (context.Session["ClassYear"] != null)
            {
                classYear = context.Session["ClassYear"].ToString();
            }
            var ds = new DataSet();
            var classBll = new BLL.Class();
            var strWhere = " Status = 1 ";
            if (!string.IsNullOrEmpty(classYear))
            {
                strWhere += string.Format(" and  YearLevel = '" + classYear + "' ");
            }
            if (!string.IsNullOrEmpty(name))
            {
                strWhere += string.Format(" and Name like '%" + name.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(description))
            {
                strWhere += string.Format(" and  Description  like '%" + description.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(level))
            {
                strWhere += string.Format(" and  Level =" + level.Trim() + " ");
            }
            if (!string.IsNullOrEmpty(type))
            {
                strWhere += string.Format(" and  Type =" + type.Trim() + " ");
            }

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);

            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "Name" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

             
            //项目负责人只能看到自己的负责的项目
            if (Profile.CurrentUser.UserRole == 3)
            {
                var role = new BLL.Roles();
                var result = role.GetCurrentUserIsCharge(Profile.CurrentUser.UserId);
                if (result)
                {
                    strWhere += string.Format(" and  Teacher like '%{0}%' ", Profile.CurrentUser.UserName);
                }
            }  

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
            fieldsNames.Add("项目负责人");
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

        private void ExportClassDetailsInfo(HttpContext context)
        {
            var classid = context.Request["classId"];
            var className = context.Request["className"];
            var exXls = new ExportXls();
            var filename = DateTime.Now.ToString("yyyy-MM-dd") + "-" + className + "-班级信息.xls";

            exXls.ExportClassDetailsToxls(context.Response, filename, classid);
        }

      

        private DataSet QueryDataResultForExp(HttpContext context)
        {
            var name = context.Request["Name"].Trim();
            var description = context.Request["Description"].Trim();
            var level = context.Request["Level"].Trim();
            var type = context.Request["Type"].Trim();

            var classYear = System.DateTime.Now.Year.ToString();
            if (context.Session["ClassYear"] != null)
            {
                classYear = context.Session["ClassYear"].ToString();
            }

            var ds = new DataSet();
            var classBll = new BLL.Class();
            var strWhere =  "a.Status = 1  ";
            if (!string.IsNullOrEmpty(name))
            {
                strWhere += string.Format(" and a.Name like '%" + name + "%' ");
            }
            if (!string.IsNullOrEmpty(classYear))
            {
                strWhere += string.Format(" and  a.YearLevel = '" + classYear + "' ");
            }
            if (!string.IsNullOrEmpty(description))
            {
                strWhere += string.Format(" and  a.Description  like '%" + description + "%' ");
            }
            if (!string.IsNullOrEmpty(level))
            {
                strWhere += string.Format(" and  a.Level =" + level + " ");
            }
            if (!string.IsNullOrEmpty(type))
            {
                strWhere += string.Format(" and  a.Type= " + type + "  ");
            }

            ds = classBll.GetDataForExport(strWhere);
            return ds;
        }

        private void AddData(HttpContext context)
        {
            var classModel = new Models.Class();
            SetModelValue(classModel, context);

            var classBll = new BLL.Class();
            var currentId = classBll.GetId();
            if (currentId == 1)
            {
                var year = DateTime.Now.Year*1000;
                classModel.ID = year + currentId;
            }
            else
            {
                var year = DateTime.Now.Year;
                if (year*1000 > currentId)  //新的一年
                {
                    classModel.ID = year*1000 + 1;
                }
                else
                {
                    classModel.ID = currentId;
                } 
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
            if (!string.IsNullOrEmpty(context.Request["Object"]))
            {
                classModel.Object = Convert.ToInt32(context.Request["Object"]);
            }   
            if (!string.IsNullOrEmpty(context.Request["ObjectName"]))
            {
                classModel.ObjectName = context.Request["ObjectName"];
            }  
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
            if (!string.IsNullOrEmpty(context.Request["PointTypeName"]))
            {
                classModel.PointTypeName =  context.Request["PointTypeName"];
            }
            if (!string.IsNullOrEmpty(context.Request["Level"]))
            {
                classModel.Level = Convert.ToInt32(context.Request["Level"]);
            }
            if (!string.IsNullOrEmpty(context.Request["LevelName"]))
            {
                classModel.LevelName = context.Request["LevelName"];
            }
            if (!string.IsNullOrEmpty(context.Request["Type"]))
            {
                classModel.Type = int.Parse(context.Request["Type"]);
            }   
            if (!string.IsNullOrEmpty(context.Request["TypeName"]))
            {
                classModel.TypeName =  context.Request["TypeName"];
            }
            if (!string.IsNullOrEmpty(context.Request["Area"]))
            {
                classModel.Area = int.Parse(context.Request["Area"]);
            }
            if (!string.IsNullOrEmpty(context.Request["AreaName"]))
            {
                classModel.AreaName = context.Request["AreaName"];
            }
            if (context.Session["ClassYear"] != null)
            {
                classModel.YearLevel = context.Session["ClassYear"].ToString();
            }
            if (!string.IsNullOrEmpty(context.Request["SetIsReport"]))
            {
                var isSet = context.Request["SetIsReport"];
                if (isSet == "yes")
                {
                    classModel.IsReport = 1;
                }
                else
                {
                    classModel.IsReport = 0;
                }
            }
            if (!string.IsNullOrEmpty(context.Request["ReportMax"]))
            {
                classModel.ReportMax = int.Parse(context.Request["ReportMax"]);
            }
            if (!string.IsNullOrEmpty(context.Request["CloseDate"]))
            {
                classModel.CloseDate = DateTime.Parse(context.Request["CloseDate"]);
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

        private string GetClassInfoByStudentId(HttpContext context)
        {
            var str = string.Empty;

            var ds = new DataSet();
            var classBll = new BLL.Class();
            var studentId = context.Request["studentId"];
            if (!string.IsNullOrEmpty(studentId))
            { 
                ds = classBll.GetClassInfoByStudentId(studentId);
                var num = 10;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    num = ds.Tables[0].Rows.Count;
                }
                str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            }
            
            return str;
        }

        /// <summary>
        /// 获取教师的任教经历信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetProfessExperByTeacherId(HttpContext context)
        {
            var str = string.Empty;

            var ds = new DataSet();
            var classBll = new BLL.Class();
            var teacherId = context.Request["teacherId"];
            if (!string.IsNullOrEmpty(teacherId))
            {
                ds = classBll.GetProfessExperByTeacherId(teacherId);
                var num = 10;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    num = ds.Tables[0].Rows.Count;
                }

                str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            }
            return str;
        }


        private string GetClassInfoByClassId(HttpContext context)
        {
            var str = string.Empty;
            var classBll = new BLL.Class();
            var classId = context.Request["classId"];
            var cinfo = classBll.GetModel(Convert.ToInt32(classId));
            if (cinfo != null)
            {
                str = JsonConvert.SerializeObject(cinfo);
            }
            return str;
        }





        /// <summary>
        /// 提取班级已经上传的ppt
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetClasspptByClassId(HttpContext context)
        {
            var str = string.Empty;

            var ds = new DataSet();
            var classAttachBll = new BLL.ClassAttach();
            var classId = context.Request["cId"];
            
            if (!string.IsNullOrEmpty(classId))
            { 
                var page = Convert.ToInt32(context.Request["page"]);
                var rows = Convert.ToInt32(context.Request["rows"]);

                //var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "Name" : context.Request["sort"];
                //var order = string.IsNullOrEmpty(context.Request["order"]) ? "desc" : context.Request["order"];

                var startIndex = (page - 1)*rows + 1;
                var endIndex = startIndex + rows - 1;

                ds = classAttachBll.GetListByPage(" IsValid=1 and  ClassId= " + classId, "", startIndex, endIndex);
                var num = 0;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    num = ds.Tables[0].Rows.Count;
                }
                str = JsonConvert.SerializeObject(new {total = num, rows = ds.Tables[0]});
            }

            return str;
        }




        /// <summary>
        /// 上传课件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string UploadPPTsByClassId(HttpContext context)
        {
            var str = string.Empty;
            try
            {
                var basePath = HttpContext.Current.Server.MapPath("Uploadppts/");
                if (context.Request.Files["Filedata"] != null)
                {
                    var classAttach = new Models.ClassAttach();
                    classAttach.Id = Guid.NewGuid();

                    HttpPostedFile myFile = context.Request.Files["Filedata"];
                    int nFileLen = myFile.ContentLength;
                    var filename = classAttach.Id + "." + myFile.FileName;
                    byte[] myData = new byte[nFileLen];
                    myFile.InputStream.Read(myData, 0, nFileLen);
                    System.IO.FileStream newFile = new System.IO.FileStream(basePath + filename,
                        System.IO.FileMode.Create);
                    newFile.Write(myData, 0, myData.Length);
                    newFile.Close();


                    var classAttachBll = new BLL.ClassAttach();
                    classAttach.FileType = myFile.ContentType;
                    classAttach.Name = myFile.FileName;
                    classAttach.Url = "Uploadppts/" + filename;
                    classAttach.IsValid = true;
                    classAttach.ClassId = context.Request["cid"]!=null?Convert.ToInt32(context.Request["cid"]):0;
                    classAttach.CreateId = Profile.CurrentUser.UserId;
                    classAttach.CreateUserName = Profile.CurrentUser.UserName;
                    classAttach.CreateTime = DateTime.Now;
                    if (context.Request["Remark"] != null)
                    {
                        classAttach.Remark = context.Request["Remark"];
                    }  
                    classAttachBll.Add(classAttach);  
                }
                else
                {
                    str = "请选择上传文件！";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                str = "处理异常：" + ex.Message;
            }
            return str;
        }


        private string DelPPTs(HttpContext context)
        {
            var str = string.Empty;
            try
            {
                if (context.Request["id"] != null)
                {
                    var id = new Guid(context.Request["id"]);
                    var classAttachBll = new BLL.ClassAttach();
                    classAttachBll.DeleteLogic(id);
                }
                else
                {
                    str = "参数错误！";
                } 
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                str = "处理异常：" + ex.Message;
            }
            return str;
        }




        /// <summary>
        /// 下载课件
        /// </summary>
        /// <param name="context"></param>
        private void DownloadPPTs(HttpContext context)
        {
            var id = context.Request["id"];
            try
            {

                if (!string.IsNullOrEmpty(id))
                {
                    var ca = new BLL.ClassAttach();
                    var ppt = ca.GetModel(new Guid(id));

                    var filename = ppt.Name;
                    var filetype = ppt.FileType;
                    var url = ppt.Url;

                    var basePath = HttpContext.Current.Server.MapPath("~");
                    context.Response.ContentType = filetype;
                    context.Response.AddHeader("Content-Disposition",
                        string.Format("attachment;filename={0}",
                            System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8)));
                    context.Response.Clear();

                    context.Response.WriteFile(basePath + "/" + url);
                    context.Response.Flush();
                    context.Response.End();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
            }
        }




        private void DownloadPPTsAll(HttpContext context)
        {
            var id = context.Request["cid"];
            try
            {
                var basePath = HttpContext.Current.Server.MapPath("~");
                if (!string.IsNullOrEmpty(id))
                {
                    var ca = new BLL.ClassAttach();
                    var ppts = ca.GetModelList(string.Format("  ClassId='{0}' and IsValid=1 ", id));

                    var cla = new BLL.Class();
                    var cl = cla.GetModel(Convert.ToInt32(id));

                    string err = string.Empty;
                    List<string[]> files = new List<string[]>();

                    foreach (var file in ppts)
                    {
                        files.Add(new[] { basePath + "/" + file.Url,file.Name });
                    }

                    string zipPath = basePath + "/" + cl.Name + ".zip";
                    ZipHelper.ZipFile(files, zipPath, out err);


                    context.Response.ContentType = "application/x-zip-compressed";
                    context.Response.AddHeader("Content-Disposition",
                        string.Format("attachment;filename={0}",
                            System.Web.HttpUtility.UrlEncode(cl.Name + ".zip", System.Text.Encoding.UTF8)));
                    context.Response.Clear();

                    context.Response.WriteFile(zipPath);
                    context.Response.Flush();
                    context.Response.End();
                    File.Delete(zipPath);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
            }
        }



    }
}