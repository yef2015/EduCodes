using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainerEvaluate.BLL;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// ExportCharts 的摘要说明
    /// </summary>
    public class ExportCharts : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World"); 

            if (context != null && context.Request != null && context.Response != null && context.Request.HttpMethod == "POST")
            {
                HttpRequest request = context.Request;
                // Get HTTP POST form variables, ensuring they are not null.
                string filename = request.Form["filename"];
                string type = request.Form["type"];
                int width = 0;
                string svg = request.Form["svg"];

                if (filename != null &&
                    type != null &&
                    Int32.TryParse(request.Form["width"], out width) &&
                    svg != null)
                {
                    // Create a new chart export object using form variables.
                    //     type = "image/jpeg";
                    Exporter export = new Exporter(filename, type, width, svg);
                    // Write the exported chart to the HTTP Response object.
                    //  export.WriteToHttpResponse(context.Response);

                    // Short-circuit this ASP.NET request and end. Short-circuiting
                    // prevents other modules from adding/interfering with the output.
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    //context.Response.End();
                    var imgHeight = 0;
                    var imgWidth = 0;
                    var imgbytes = export.GetReportImg(out imgHeight, out imgWidth);
                    var xls = new ExportXls();
                    xls.ExportImg2Xls(context.Response, imgbytes, "课程满意度分布", imgHeight, imgWidth);

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