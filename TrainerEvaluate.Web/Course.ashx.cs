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
    /// Course 的摘要说明
    /// </summary>
    public class Course : IHttpHandler
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
                case "s":
                    SetClassCourse(context);
                    break; 
                case "gc":
                    GetClassCourse(context);
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
            var couBll = new BLL.Course();
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
            var ds = new DataSet();
            var courBll = new BLL.Course();

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]); 
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "TeachTime" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "desc" : context.Request["order"];
             

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = courBll.GetRecordCount(" Status = 1 ");
            ds = courBll.GetListByPage(" Status = 1 ", sort, startIndex, endIndex, order); 

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }


        /// <summary>
        /// 获取班级课程
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private void GetClassCourse(HttpContext context)
        {
            var ds = new DataSet();
            var courBll = new BLL.Course();

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]); 
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "ck" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "desc" : context.Request["order"];
           
            var classid = context.Request["cId"];   

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = courBll.GetRecordOfClassCount(" Status = 1 ",classid);
            ds = courBll.GetListByPageOfClass(" Status = 1 ", sort, startIndex, endIndex, order,classid); 

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }

        

        /// <summary>
        /// 设置班级课程,保存班级所选择的课程
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private void SetClassCourse(HttpContext context)
        {
            var msg = "";
            var classId = context.Request["ClassId"];
            var isAll = context.Request["IsAll"];
            if (!string.IsNullOrEmpty(isAll)) //全选
            {
                if (isAll == "1")
                {
                    var classBll = new BLL.Class();
                    var result = classBll.SaveClassAllCourse(classId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                else //全不选
                {
                    var classBll = new BLL.Class();
                    var result = classBll.DeletAllCourseofClass(classId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
            }
            else
            {
                var courseIds = context.Request["CourseIds"];
                var unCourseIds = context.Request["UnCourseIds"];
                if (!string.IsNullOrEmpty(courseIds))
                {
                    if (!string.IsNullOrEmpty(unCourseIds))
                    {
                        var unid = unCourseIds.Split('|');
                        if (unid.Length > 0)
                        {
                            foreach (var uid in unid)
                            {
                                if (!string.IsNullOrEmpty(uid))
                                {
                                    courseIds = courseIds.Replace(uid, "");
                                }
                            }
                        }
                    }
                    var cor = courseIds.Split('|');
                    var courseList = new List<string>();
                    foreach (var sid in cor)
                    {
                        if (!string.IsNullOrEmpty(sid))
                        {
                            if (!courseList.Contains(sid))
                            {
                                courseList.Add(sid);
                            }
                        }
                    }
                    var classBll = new BLL.Class();
                    var result = classBll.SaveChoseClass(classId, courseList.ToArray());
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(unCourseIds))
                    {
                        var unid = unCourseIds.Split('|');
                       var classBll = new BLL.Class();
                       var result = classBll.SaveUnChoseClass(classId, unid);
                       if (!result)
                       {
                           msg = "保存失败！";
                       }
                         
                    }
                }
            }
            context.Response.Write(msg);
        }


        



        private void Query(HttpContext context)
        {
            var corName = context.Request["name"].Trim();
            var teaName = context.Request["teaName"].Trim();
            var teaplace = context.Request["teaplace"].Trim();
            var teaTime = context.Request["teaTime"].Trim();
            var ds = new DataSet();
            var courBll = new BLL.Course();
            var strWhere = "";
            if (!string.IsNullOrEmpty(corName))
            {
                strWhere = string.Format(" CourseName like '%" + corName + "%' ");
            }
            if (!string.IsNullOrEmpty(teaName))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  TeacherName  like '%" + teaName + "%' ");
                }
                else
                {
                    strWhere = string.Format(" TeacherName like '%" + teaName + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(teaplace))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  TeachPlace  like '%" + teaplace + "%' ");
                }
                else
                {
                    strWhere = string.Format(" TeachPlace like '%" + teaplace + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(teaTime))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  TeachTime  like '%" + teaTime + "%' ");
                }
                else
                {
                    strWhere = string.Format(" TeachTime like '%" + teaTime + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere += string.Format(" and  Status = 1 ");
            }
            else
            {
                strWhere += string.Format("Status = 1 ");
            }

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);

            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "TeachTime" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = courBll.GetRecordCount(strWhere);
            ds = courBll.GetListByPage(strWhere, sort, startIndex, endIndex, order);


            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }



        private void ExportCourseInfo(HttpContext context)
        {
            var exXls = new ExportXls();
            var fieldsNames = new List<string>();
            fieldsNames.Add("课程名称");
            fieldsNames.Add("授课教师");
            fieldsNames.Add("授课地点");
            fieldsNames.Add("授课时间");
            fieldsNames.Add("课程类型");
            fieldsNames.Add("描述");


            var ds = QueryDataResultForExp(context);
            var filename = DateTime.Now.ToString("yyyy-MM-dd") + "-课程信息.xls";

            if (ds != null && ds.Tables.Count > 0)
            {
                exXls.ExportToXls(context.Response, fieldsNames, ds.Tables[0], filename);
            }
        }

        private DataSet QueryDataResultForExp(HttpContext context)
        {
            var corName = context.Request["name"].Trim();
            var teaName = context.Request["teaName"].Trim();
            var teaplace = context.Request["teaplace"].Trim();
            var teaTime = context.Request["teaTime"].Trim();
            var ds = new DataSet();
            var courBll = new BLL.Course();
            var strWhere = "";
            if (!string.IsNullOrEmpty(corName))
            {
                strWhere = string.Format(" CourseName like '%" + corName + "%' ");
            }
            if (!string.IsNullOrEmpty(teaName))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  TeacherName  like '%" + teaName + "%' ");
                }
                else
                {
                    strWhere = string.Format(" TeacherName like '%" + teaName + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(teaplace))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  TeachPlace  like '%" + teaplace + "%' ");
                }
                else
                {
                    strWhere = string.Format(" TeachPlace like '%" + teaplace + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(teaTime))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  TeachTime  like '%" + teaTime + "%' ");
                }
                else
                {
                    strWhere = string.Format(" TeachTime like '%" + teaTime + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere += string.Format(" and  Status = 1 ");
            }
            else
            {
                strWhere += string.Format("Status = 1 ");
            }

            ds = courBll.GetDataForExport(strWhere);
            return ds;
        }



        private void AddData(HttpContext context)
        {
            var courModel = new Models.Course();
            courModel.CourseId = Guid.NewGuid();
            SetModelValue(courModel, context);
            courModel.CreatTime = DateTime.Now;
            courModel.LastModifyTime = DateTime.Now;
            courModel.Status = 1;
            var courBll = new BLL.Course();
            var result = false;
            var msg = "";
            try
            {
                SaveCourseTeacherData(courModel.TeacherId, courModel.CourseId.ToString());
                result = courBll.Add(courModel); 
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
            var courBll = new BLL.Course();
            var courModel = courBll.GetModel(new Guid(id));
            courModel.LastModifyTime = DateTime.Now;
            var result = false;
            var msg = "";
            try
            {
                SetModelValue(courModel, context); 
                SaveCourseTeacherData(courModel.TeacherId, courModel.CourseId.ToString());
                result = courBll.Update(courModel);
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




        private void SetModelValue(Models.Course courModel, HttpContext context)
        {
            courModel.CourseName = context.Request["CourseName"];
            courModel.TeachPlace = context.Request["TeachPlace"];
            courModel.TeachTime = context.Request["TeachTime"]; 
            courModel.TeacherId =HttpUtility.UrlDecode(context.Request["TeacherId"]);
            courModel.TeacherName = context.Request["TeacherName"];
            courModel.Description = context.Request["Description"];
            if (!string.IsNullOrEmpty(context.Request["Type"]))
            {
                courModel.Type = Convert.ToInt32(context.Request["Type"]);
            }
            courModel.TypeName = context.Request["TypeName"];
            courModel.TypeSmallName = context.Request["TypeSmallName"];
        }



        private void SaveCourseTeacherData(string teacherIds,string courseId)
        {
            if ((!string.IsNullOrEmpty(teacherIds)) && (!string.IsNullOrEmpty(courseId)))
            {
                var teaBll = new BLL.Teacher();
                var teaList = teacherIds.Split(',');
                teaBll.SaveChooseTeacher(teaList, courseId);
            } 
        } 


        private void DelData(string id, HttpContext context)
        {
            var courBll = new BLL.Course();
            var result = false;
            var msg = "";
            try
            {
                result = courBll.Delete(new Guid(id));

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