using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// LogInHandler 的摘要说明
    /// </summary>
    public class LogInHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var account = context.Request["acc"].Trim();
            var pwd = context.Request["pwd"].Trim();

            var result = Profile.Load(account, pwd);


            context.Response.Write(result ? "1" : "0");
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