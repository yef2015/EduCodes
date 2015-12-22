using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrainerEvaluate.Web
{
    public partial class MyQuestionnaireNew : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var courseId = Request.QueryString["cid"];
            if (!IsPostBack)
            {
                SetCourseValue(courseId);
                hUid.Value = Profile.CurrentUser.UserId.ToString();
            }  
        }


        private void SetCourseValue(string courseId)
        {
            //if (DateTime.Compare(DateTime.Now, Convert.ToDateTime("2014-11-15 0:00:00")) > 0 &&
            //    DateTime.Compare(DateTime.Now, Convert.ToDateTime("2014-11-20 23:59:59")) < 0)
            if (ConfigurationManager.AppSettings["IsLimited"] != null &&
                Convert.ToBoolean(ConfigurationManager.AppSettings["IsLimited"]))  //限制
            {
                if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(ConfigurationManager.AppSettings["StartTime"])) >
                    0 &&
                    DateTime.Compare(DateTime.Now, Convert.ToDateTime(ConfigurationManager.AppSettings["EndTime"])) < 0)
                {
                    SetDisplay(courseId);
                }
                else
                {
                    quTime.Visible = true;
                    timeTip.InnerText = "评估时间为： " + ConfigurationManager.AppSettings["StartTime"] + " 到 " + ConfigurationManager.AppSettings["EndTime"];
                    quNo.Visible = false;
                    queHas.Visible = false;
                    CourseNames.Visible = false;

                }
            }
            else  //不限制
            {
                SetDisplay(courseId);
            }
        }


        private void SetDisplay(string courseId)
        {
            quTime.Visible = false;
            var queBll = new BLL.Questionnaire();
            var ds = queBll.GetCourseQuestionnarieInfo(Profile.CurrentUser.UserId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 1)
                {
                    btnSubmit.ImageUrl = "~/images/regist2.jpg";
                }
                else
                {
                    btnSubmit.ImageUrl = "~/images/regist1.jpg";
                }
            }
            else
            {
                btnSubmit.ImageUrl = "~/images/regist1.jpg";
            }
            if (!string.IsNullOrEmpty(courseId))  //点击后传过来的
            {
                var course = new BLL.Course();
                var couModel = course.GetModel(new Guid(courseId));
                // coName.InnerText = couModel.CourseName; 
                hCouseId.Value = couModel.CourseId.ToString();
                ipCourseName.InnerText = couModel.CourseName;
                ipPlace.InnerText = couModel.TeachPlace;
                ipTeacher.InnerText = couModel.TeacherName;
                ipTime.InnerText = couModel.TeachTime;

                // GetTheLeft();
                quNo.Visible = false;
                queHas.Visible = true;
                //   queHas1.Visible = true;
            }
            else
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hCouseId.Value = ds.Tables[0].Rows[0]["CourseId"].ToString();

                    //  coName.InnerText = ds.Tables[0].Rows[0]["CourseName"].ToString(); 
                    //var sb = new StringBuilder();
                    //var i = 0;
                    //foreach (DataRow row in ds.Tables[0].Rows)
                    //{
                    //    i++;
                    //    sb.Append("<span onclick='toOther(\"" + row["CourseId"] + "\")'  style='cursor: pointer'>");
                    //    sb.Append(i + "、");
                    //    sb.Append(row["CourseName"]);
                    //    sb.Append(@"</span>");
                    //    sb.Append(@"<br/><br/>");
                    //}
                    //  CourseNames.InnerHtml = sb.ToString();


                    ipCourseName.InnerText = ds.Tables[0].Rows[0]["CourseName"].ToString();
                    ipPlace.InnerText = ds.Tables[0].Rows[0]["TeachPlace"].ToString();
                    ipTeacher.InnerText = ds.Tables[0].Rows[0]["TeacherName"].ToString();
                    ipTime.InnerText = ds.Tables[0].Rows[0]["TeachTime"].ToString();


                    quNo.Visible = false;
                    queHas.Visible = true;
                    //      queHas1.Visible = true;
                }
                else
                {
                    quNo.Visible = true;
                    queHas.Visible = false;
                    //    queHas1.Visible = false;
                    //Response.Redirect("");
                }
            }
        }



        private void GetTheLeft()
        {
            var queBll = new BLL.Questionnaire();
            var ds = queBll.GetCourseQuestionnarieInfo(Profile.CurrentUser.UserId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var sb = new StringBuilder();
                var i = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    i++;
                    sb.Append("<span onclick='toOther(\"" + row["CourseId"] + "\")'  style='cursor: pointer'>");
                    sb.Append(i + "、");
                    sb.Append(row["CourseName"]);
                    sb.Append(@"</span>");
                    sb.Append(@"<br/><br/>");
                }
                CourseNames.InnerHtml = sb.ToString();
            }
        }




        protected void SubmitBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                var courseId = new Guid(hCouseId.Value);


                var quesModel = new Models.Questionnaire();
                Int32 total = 0;
                int totalCourse = 0;
                int totalTeacher = 0;
                int totalOrg = 0;
                quesModel.QuestionnaireId = Guid.NewGuid();
                quesModel.AppraiserId = Profile.CurrentUser.UserId;
                quesModel.AppraiserTime = DateTime.Now;
                quesModel.CourseDevelop = Convert.ToInt32(radCourseDevelop.SelectedValue);
                total += (int)quesModel.CourseDevelop;
                totalCourse += (int)quesModel.CourseDevelop;
                quesModel.CourseId = courseId;
                quesModel.CourseKey = Convert.ToInt32(radCourseKey.SelectedValue);
                total += (int)quesModel.CourseKey;
                totalCourse += (int)quesModel.CourseKey;
                quesModel.CoursePractical = Convert.ToInt32(radCoursePractical.SelectedValue);
                total += (int)quesModel.CoursePractical;
                totalCourse += (int)quesModel.CoursePractical;
                quesModel.CourseRich = Convert.ToInt32(radContentRich.SelectedValue);
                total += (int)quesModel.CourseRich;
                totalCourse += (int)quesModel.CourseRich;
                quesModel.CourseSubject = Convert.ToInt32(radSubject.SelectedValue);
                total += (int)quesModel.CourseSubject;
                totalCourse += (int)quesModel.CourseSubject;
                quesModel.OrgArrange = Convert.ToInt32(radOrgArrange.SelectedValue);
                total += (int)quesModel.OrgArrange;
                totalOrg += (int)quesModel.OrgArrange;
                quesModel.OrgService = Convert.ToInt32(radOrgService.SelectedValue);
                total += (int)quesModel.OrgService;
                totalOrg += (int)quesModel.OrgService;
                quesModel.OrgTime = Convert.ToInt32(radOrgTime.SelectedValue);
                total += (int)quesModel.OrgTime;
                totalOrg += (int)quesModel.OrgTime;
                quesModel.TeacherBearing = Convert.ToInt32(radTeacherBearing.SelectedValue);
                total += (int)quesModel.TeacherBearing;
                totalTeacher += (int)quesModel.TeacherBearing;
                quesModel.TeacherCommunication = Convert.ToInt32(radTeacherCommunication.SelectedValue);
                total += (int)quesModel.TeacherCommunication;
                totalTeacher += (int)quesModel.TeacherCommunication;
                quesModel.TeacherLanguage = Convert.ToInt32(radTeacherLanguage.SelectedValue);
                total += (int)quesModel.TeacherLanguage;
                totalTeacher += (int)quesModel.TeacherLanguage;
                quesModel.TeacherPrepare = Convert.ToInt32(radTeacherPrepare.SelectedValue);
                total += (int)quesModel.TeacherPrepare;
                totalTeacher += (int)quesModel.TeacherPrepare;
                quesModel.TeacherStyle = Convert.ToInt32(radTeacherStyle.SelectedValue);
                total += (int)quesModel.TeacherStyle;
                totalTeacher += (int)quesModel.TeacherStyle;
                quesModel.TotalEvaluation = Convert.ToInt32(radAll.SelectedValue);
                // total += (int)quesModel.TotalEvaluation;
                quesModel.Suggest = txtSuggest.InnerText.Trim();
                quesModel.Total = total;
                quesModel.TotalOrg = totalOrg;
                quesModel.TotalCousre = totalCourse;
                quesModel.TotalTeacher = totalTeacher;

                var queBll = new BLL.Questionnaire();
                if (queBll.Add(quesModel))
                {
                    queBll.SubmitQuestionnaireState(Profile.CurrentUser.UserId, courseId);
                }
                Response.Redirect("MyQuestionnaireNew.aspx");
            }
            catch (Exception ex)
            {
                Utility.LogHelper.WriteLogofExceptioin(ex);
            }
        }
    }
}