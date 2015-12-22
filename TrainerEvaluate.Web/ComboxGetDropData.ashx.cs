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
                    // 学校类型
                    dt = BLL.SPSchoolDistrict.GetDataSourceOnSchDistrict();
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