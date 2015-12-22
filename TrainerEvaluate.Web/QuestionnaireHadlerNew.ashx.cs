using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Web;
using System.Web.Management;
using Newtonsoft.Json;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// QuestionnaireHadlerNew 的摘要说明
    /// </summary>
    public class QuestionnaireHadlerNew : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var opType = context.Request["t"];
            var id = context.Request["id"];
            switch (opType)
            {
                case "s":
                    CreateQue(context);
                    break; 
                case "c":
                    CancelQue(context);
                    break;    
                case "q":
                   QueryData(context);
                    break;    
                case "e":
                    EditQue(context);
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
            var questioninfo = new BLL.QuestionInfo();
            //ds = courBll.GetAllList(); 
            //var str = JsonConvert.SerializeObject(new {total = ds.Tables[0].Rows.Count, rows = ds.Tables[0]});

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "TeachTime" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "desc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = questioninfo.GetRecordCountNew("");
            ds = questioninfo.GetListByPageNew("", sort, startIndex, endIndex);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }

 


        private void QueryData(HttpContext context)
        {
            var ds = new DataSet();
            var questioninfo = new BLL.QuestionInfo();
            //ds = courBll.GetAllList(); 
            //var str = JsonConvert.SerializeObject(new {total = ds.Tables[0].Rows.Count, rows = ds.Tables[0]});

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "TeachTime" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "desc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;
             

           var className= context.Request["ClassName"];
           var status = context.Request["Status"];
           var courseName = context.Request["CourseName"];
           var teacher = context.Request["Teacher"];
           var time = context.Request["Time"];
           var place = context.Request["Place"];

            var strWhere = "";
            if (!string.IsNullOrEmpty(className))
            {
                strWhere = string.Format(" ClassName like '%" + className + "%' ");
            }
            if (!string.IsNullOrEmpty(status)&&status!="0")
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Status  = " + status + " ");
                }
                else
                {
                    strWhere = string.Format(" Status  = " + status + " ");
                }
            }
            if (!string.IsNullOrEmpty(courseName))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  CourseName  like '%" + courseName + "%' ");
                }
                else
                {
                    strWhere = string.Format(" CourseName  like '%" + courseName + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(teacher))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  TeacherName  like '%" + teacher + "%' ");
                }
                else
                {
                    strWhere = string.Format(" TeacherName  like '%" + teacher + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(time))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  TeachTime  like '%" + time + "%' ");
                }
                else
                {
                    strWhere = string.Format(" TeachTime  like '%" + time + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(place))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  TeachPlace  like '%" + place + "%' ");
                }
                else
                {
                    strWhere = string.Format(" TeachPlace  like '%" + place + "%' ");
                }
            }

          
            ds = questioninfo.GetListByPageNew(strWhere, sort, startIndex, endIndex);
            var num = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                  num = ds.Tables[0].Rows.Count;
            } 
            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }

 





        //生成问卷
        private void CreateQue(HttpContext context)
        {
           
            var classCourseId = context.Request["id"];
            var name = context.Request["Name"];
            var startTime = context.Request["StartTime"];
            var endTime = context.Request["EndTime"];

            var msg = "";
            if (!string.IsNullOrEmpty(classCourseId))
            {
                var qinfo = new BLL.QuestionInfo();
                var result = qinfo.CreateQuesInfo(name, classCourseId, startTime, endTime, out msg);
                if (!result)
                {
                    if (string.IsNullOrEmpty(msg))
                    {
                        msg = "保存失败！"; 
                    } 
                }
            }
            else
            {
                msg = "参数错误！";
            }
            context.Response.Write(msg);
        }



        private void EditQue(HttpContext context)
        {
            var classCourseId = context.Request["id"];
            var name = context.Request["Name"];
            var startTime = context.Request["StartTime"];
            var endTime = context.Request["EndTime"];

            var msg = "";
            if (!string.IsNullOrEmpty(classCourseId))
            {
                var qinfo = new BLL.QuestionInfo();
                var result = qinfo.EditQuesInfo(name, classCourseId, startTime, endTime, out msg);
                if (!result)
                {
                    if (string.IsNullOrEmpty(msg))
                    {
                        msg = "保存失败！";
                    }
                }
            }
            else
            {
                msg = "参数错误！";
            }
            context.Response.Write(msg);
        }

        //取消问卷
        private void CancelQue(HttpContext context)
        {
            var classCourseId = context.Request["id"];
            var msg = "";  
            if (!string.IsNullOrEmpty(classCourseId))
            {
                var qinfo = new BLL.QuestionInfo();
                var result = qinfo.CancelQue(classCourseId);
                if (!result)
                {
                    msg = "保存失败！";
                }
            }
            else
            {
                msg = "参数错误！";
            }
            context.Response.Write(msg);
        }



      
    }
}