﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrainerEvaluate.Web
{
    public partial class NetForStudentReportDetail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string UserId
        {
            get { return Profile.CurrentUser.UserId.ToString(); }
        }

        public string ClassId
        {
            get { return System.Web.HttpUtility.UrlEncode(Request.QueryString["cid"]); }
        }
    }
}