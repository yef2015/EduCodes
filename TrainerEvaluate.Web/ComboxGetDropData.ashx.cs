using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// ComboxGetDropData 的摘要说明
    /// </summary>
    public class ComboxGetDropData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var dataType = context.Request["t"];
            var str = GetData(dataType);
            context.Response.Write(str);
        }

        private string GetData(string dataType)
        {
            var dt = new DataTable();
            switch (dataType)
            {
                case "sdn":
                    // 所属学区
                    dt = BLL.SPSchoolDistrict.GetDataSourceOnSchDistrict();
                    break;
                case "shl":
                    // 所属学校
                    dt = BLL.SPSchool.GetDataSourceOnSchool();
                    break;
                case "ccus":
                    // 选择课程
                    dt = BLL.Course.GetDataSourceOnCourse();
                    break;
                case "ctea":
                    // 选择授课老师
                    dt = BLL.Teacher.GetDataSourceOnTeacher();
                    break;
                default:
                    break;
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                var str = JsonConvert.SerializeObject(dt);
                return str;
            }
            else
            {
                return "";
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}