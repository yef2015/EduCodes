using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrainerEvaluate.Web
{
    public partial class ClassManageList : BasePage
    {
        protected string yearLevel = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack) {
                var key = Request.QueryString["key"].ToString();
                if (string.IsNullOrEmpty(key))
                {
                    DateTime date = DateTime.Now;
                    key = date.Year.ToString();
                }
                Session["ClassYear"] = key;
                yearLevel = key + "年";
            }
        }
    }
}