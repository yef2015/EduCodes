using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrainerEvaluate.Web
{
    public partial class StudentList : BasePage
    {
        protected string schoolid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var key = Request.QueryString["key"].ToString();
                if (string.IsNullOrEmpty(key))
                {
                    key = Guid.Empty.ToString();
                }
                Session["SchoolName"] = key;
                schoolid = key;
            }
        }
    }
}