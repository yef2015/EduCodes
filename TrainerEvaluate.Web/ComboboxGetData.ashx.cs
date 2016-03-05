using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// ComboboxGetData 的摘要说明
    /// </summary>
    public class ComboboxGetData : IHttpHandler
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
           
            switch (dataType)
            {
                case "g":
                    dataType = "性别";
                    break;  
                case "n":
                    dataType = "民族";
                    break;  
                case "p":
                    dataType = "政治面貌";
                    break;  
                case "j":
                    dataType = "职称";
                    break;
                case "stuj":
                    dataType = "职称";
                    break; 
                case "t":
                    dataType = "课程类型";
                    break;     
                case "s":
                    dataType = "学时类型";
                    break;   
                case "a":
                    dataType = "培训范围";
                    break;    
                case "l":
                    dataType = "培训级别";
                    break;     
                case "pt":
                    dataType = "培训层次";
                    break;
                case "rc":
                    dataType = "办学性质";
                    break;
                case "st":
                    dataType = "学校类型";
                    break;  
                case "ptn":
                    dataType = "学员职务";
                    break;
                default:
                    break; 
            }
           var dt=   BLL.Common.GetDataSourceFromSysCode(dataType);
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