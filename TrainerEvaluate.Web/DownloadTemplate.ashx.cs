using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// DownloadTemplate 的摘要说明
    /// </summary>
    public class DownloadTemplate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var opType = context.Request["t"];
            Download(context, opType);
        }



        private void Download(HttpContext context, string tmpType)
        {
            var filename = "";
            switch (tmpType)
            {
                case "stu":
                    filename = "学员信息.xls";
                    break;
                case "tch":
                    filename = "教师信息.xls";
                    break;
                case "cor":
                    filename = "课程信息.xls";
                    break;
                case "tsd":
                    filename = "学区信息.xls";
                    break;
                case "tsh":
                    filename = "学校信息.xls";
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(filename))
            {
                context.Response.Clear();
                context.Response.ContentType = "application/vnd.ms-excel";
                context.Response.AddHeader("Content-Disposition",
                    string.Format("attachment;filename={0}",
                        System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8)));

                string filePath = HttpContext.Current.Server.MapPath("UploadTemplate/" + filename);

                FileStream fs = new FileStream(filePath, FileMode.Open);
                byte[] fileBytes = new byte[(int)fs.Length];
                fs.Read(fileBytes, 0, fileBytes.Length);
                fs.Close();

                context.Response.AppendHeader("Content-Length", fileBytes.Length.ToString());
                context.Response.OutputStream.Write(fileBytes, 0, fileBytes.Length);
                context.Response.End();
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