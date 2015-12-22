using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrainerEvaluate.Web
{
    public partial class DownloadReport : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var strWord = "xxxx";
            Response.ContentEncoding = System.Text.Encoding.UTF7;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename=report.doc"); //必须的
            Response.AddHeader("Content-type", "application");
            Response.ContentType = "application/ms-html";
            Response.ContentEncoding = System.Text.Encoding.Default; //如果不行改为utf7，默认一般可以，处理头部乱码的问题
            Response.Write(strWord);
            Response.Flush();
            Response.Close();
        }
    }
}