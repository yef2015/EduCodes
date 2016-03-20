using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.Web
{
    public partial class MyQuestionnaireNew : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ccId = Request.QueryString["cid"];
            if (!IsPostBack)
            {
                var courseId = BLL.Questionnaire.GetCourseIdByccId(ccId);
                SetCourseValue(courseId,ccId);
                hUid.Value = Profile.CurrentUser.UserId.ToString();
            }  
        }

        private void SetCourseValue(string courseId,string ccid)
        {
            if (ConfigurationManager.AppSettings["IsLimited"] != null &&
                Convert.ToBoolean(ConfigurationManager.AppSettings["IsLimited"]))  //限制
            {
                if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(ConfigurationManager.AppSettings["StartTime"])) >
                    0 &&
                    DateTime.Compare(DateTime.Now, Convert.ToDateTime(ConfigurationManager.AppSettings["EndTime"])) < 0)
                {
                    SetDisplay(courseId,ccid);
                }
                else
                {
                    quTime.Visible = true;
                    timeTip.InnerText = "评估时间为： " + ConfigurationManager.AppSettings["StartTime"] + " 到 " + ConfigurationManager.AppSettings["EndTime"];
                    quNo.Visible = false;
                    queHas.Visible = false;
                    queHasBtn.Visible = false;
                    CourseNames.Visible = false;
                }
            }
            else  //不限制
            {
                SetDisplay(courseId,ccid);
            }
        }

        private void SetDisplay(string courseId,string ccid)
        {
            quTime.Visible = false;
            var queBll = new BLL.Questionnaire();
            var ds = queBll.GetCourseQuestionnarieInfoNew(Profile.CurrentUser.UserId);
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

                SetContent();// 设置题目信息
            }
            else
            {
                btnSubmit.ImageUrl = "~/images/regist1.jpg";
            }
            if (!string.IsNullOrEmpty(courseId))  //点击后传过来的
            {
                hCouseId.Value = ccid;

                DataTable dt = BLL.Course.GetCourseByCorIdStuId(courseId, Profile.CurrentUser.UserId.ToString());

                if (dt.Rows.Count > 0)
                {
                    ipCourseName.InnerText = dt.Rows[0]["CourseName"].ToString();
                    ipPlace.InnerText = dt.Rows[0]["TeachPlace"].ToString();
                    ipTeacher.InnerText = dt.Rows[0]["TeacherName"].ToString();
                    ipTime.InnerText = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString()).ToString("yyyy-MM-dd")+"到"+
                                       Convert.ToDateTime(dt.Rows[0]["FinishDate"].ToString()).ToString("yyyy-MM-dd"); 
                }

                quNo.Visible = false;
                queHas.Visible = true;
                queHasBtn.Visible = true;
            }
            else
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    hCouseId.Value = dr["ccId"].ToString();

                    ipCourseName.InnerText = dr["CourseName"].ToString();
                    ipPlace.InnerText = dr["TeachPlace"].ToString();
                    ipTeacher.InnerText = dr["TeacherName"].ToString();
                    ipTime.InnerText = Convert.ToDateTime(dr["StartDate"].ToString()).ToString("yyyy-MM-dd") + "到" +
                                       Convert.ToDateTime(dr["FinishDate"].ToString()).ToString("yyyy-MM-dd"); 

                    quNo.Visible = false;
                    queHas.Visible = true;
                    queHasBtn.Visible = true;
                }
                else
                {
                    quNo.Visible = true;
                    queHas.Visible = false;
                    queHasBtn.Visible = false;
                }
            }
        }

        private void SetContent()
        {
            var queBll = new BLL.QuestionnaireSurvey();
            DataSet ds = queBll.GetQuestionnaireSurveryList();
            StringBuilder strBuilder = new StringBuilder();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                strBuilder.Append("<table width='98%' border='0' cellspacing='1' cellpadding='3' align='center' bgcolor='C4D4E1' style='margin-left: 10px;'>");
                DataTable dt = ds.Tables[0];
                DataRow[] drRoot = dt.Select("ParentId is NULL");

                if (drRoot.Length > 0)
                {
                    DataRow[] drFirst = dt.Select("ParentId = '" + drRoot[0]["QuestId"].ToString() + "'");

                    if (drFirst.Length > 0)
                    {
                        foreach (DataRow dr in drFirst)
                        {
                            strBuilder.Append("<tr><td colspan='4' bgcolor='4A5C69' class='white10' height='25'>");
                            strBuilder.Append("<img src='images/bank.gif' width='10' height='10'>" + dr["ShowName"] + "</td></tr>");

                            switch (dr["ShowType"].ToString())
                            {
                                case "NoAnswer":
                                    DataRow[] drSecond = dt.Select("ParentId = '" + dr["QuestId"].ToString() + "'");
                                    if (drSecond.Length > 0)
                                    {
                                        foreach (DataRow drChild in drSecond)
                                        {
                                            strBuilder.Append("<tr><td colspan='4' bgcolor='#FFFFFF' class='gray10a' height='26'>");
                                            strBuilder.Append("<img src='images/bank.gif' width='25' height='10'>" + drChild["ShowName"] + "</td>");
                                            strBuilder.Append("</tr><tr><td colspan='4' bgcolor='F0F9FF' class='gray10a' height='29'>");
                                            strBuilder.Append("<img src='images/bank.gif' width='35' height='10'>");
                                            strBuilder.Append("<input type='radio' name='" + drChild["ShowId"] + "' id='" + drChild["ShowId"] + "1' value='" + drChild["OptValue1"] + "' />" + drChild["OptText1"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                            strBuilder.Append("<input type='radio' name='" + drChild["ShowId"] + "' id='" + drChild["ShowId"] + "2' value='" + drChild["OptValue2"] + "' />" + drChild["OptText2"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                            strBuilder.Append("<input type='radio' name='" + drChild["ShowId"] + "' id='" + drChild["ShowId"] + "3' value='" + drChild["OptValue3"] + "' />" + drChild["OptText3"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                            strBuilder.Append("<input type='radio' name='" + drChild["ShowId"] + "' id='" + drChild["ShowId"] + "4' value='" + drChild["OptValue4"] + "' />" + drChild["OptText4"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                            strBuilder.Append("</td></tr>");
                                        }
                                    }
                                    break;
                                case "AnswerRadio":
                                    strBuilder.Append("<tr><td colspan='4' bgcolor='F0F9FF' class='gray10a' height='25'>");
                                    strBuilder.Append("<img src='images/bank.gif' width='35' height='10'>");
                                    strBuilder.Append("<input type='radio' name='" + dr["ShowId"] + "' id='" + dr["ShowId"] + "1' value='" + dr["OptValue1"] + "' />" + dr["OptText1"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                    strBuilder.Append("<input type='radio' name='" + dr["ShowId"] + "' id='" + dr["ShowId"] + "2' value='" + dr["OptValue2"] + "' />" + dr["OptText2"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                    strBuilder.Append("<input type='radio' name='" + dr["ShowId"] + "' id='" + dr["ShowId"] + "3' value='" + dr["OptValue3"] + "' />" + dr["OptText3"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                    strBuilder.Append("<input type='radio' name='" + dr["ShowId"] + "' id='" + dr["ShowId"] + "4' value='" + dr["OptValue4"] + "' />" + dr["OptText4"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                    strBuilder.Append("</td></tr>");
                                    break;
                                case "AnswerText":
                                    strBuilder.Append("<tr><td colspan='4' bgcolor='#FFFFFF' class='gray10a' height='187'>");
                                    strBuilder.Append("<img src='images/bank.gif' width='25' height='10'>");
                                    strBuilder.Append("<input name='" + dr["ShowId"] + "' id='" + dr["ShowId"] + "' class='easyui-textbox' data-options='multiline:true' style='height: 160px;width:800px;'>");
                                    strBuilder.Append("</td></tr>");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                strBuilder.Append("</table>");
                LiteralContent.Text = strBuilder.ToString();
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
                var queBll = new BLL.Questionnaire();

                var clacouId = hCouseId.Value;
                var quesModel = new Models.Questionnaire();
                Int32 total = 0;
                int totalCourse = 0;
                int totalTeacher = 0;
                int totalOrg = 0;
                quesModel.QuestionnaireId = Guid.NewGuid();
                quesModel.AppraiserId = Profile.CurrentUser.UserId;
                quesModel.AppraiserTime = DateTime.Now;

                quesModel.StudentId = Profile.CurrentUser.UserId.ToString();

                // 一、您对本次培训课程的总体评价是：
                quesModel.TotalEvaluation = Convert.ToInt32(Request.Form["radAll"].ToString());

                // 二、课程内容
                // 1.课程主题清晰明确
                quesModel.CourseSubject = Convert.ToInt32(Request.Form["radSubject"].ToString());
                total += (int)quesModel.CourseSubject;
                totalCourse += (int)quesModel.CourseSubject;

                // 2.课程内容丰富、能吸引人
                quesModel.CourseRich = Convert.ToInt32(Request.Form["radContentRich"].ToString());
                total += (int)quesModel.CourseRich;
                totalCourse += (int)quesModel.CourseRich;

                // 3.课程内容切合实际，能指导实践
                quesModel.CoursePractical = Convert.ToInt32(Request.Form["radCoursePractical"].ToString());
                total += (int)quesModel.CoursePractical;
                totalCourse += (int)quesModel.CoursePractical;

                // 4.课程内容重点突出，易于理解
                quesModel.CourseKey = Convert.ToInt32(Request.Form["radCourseKey"].ToString());
                total += (int)quesModel.CourseKey;
                totalCourse += (int)quesModel.CourseKey;

                // 5.课程内容有助于个人发展
                quesModel.CourseDevelop = Convert.ToInt32(Request.Form["radCourseDevelop"].ToString());
                total += (int)quesModel.CourseDevelop;
                totalCourse += (int)quesModel.CourseDevelop;

                // 三、培训讲师
                // 1.讲师准备比较充分
                quesModel.TeacherPrepare = Convert.ToInt32(Request.Form["radTeacherPrepare"].ToString());
                total += (int)quesModel.TeacherPrepare;
                totalTeacher += (int)quesModel.TeacherPrepare;

                // 2.语言表达清晰，态度端正
                quesModel.TeacherLanguage = Convert.ToInt32(Request.Form["radTeacherLanguage"].ToString());
                total += (int)quesModel.TeacherLanguage;
                totalTeacher += (int)quesModel.TeacherLanguage;

                // 3.仪表仪容端庄大方，有亲和力
                quesModel.TeacherBearing = Convert.ToInt32(Request.Form["radTeacherBearing"].ToString());
                total += (int)quesModel.TeacherBearing;
                totalTeacher += (int)quesModel.TeacherBearing;

                // 4.培训方式多样，生动有趣
                quesModel.TeacherStyle = Convert.ToInt32(Request.Form["radTeacherStyle"].ToString());
                total += (int)quesModel.TeacherStyle;
                totalTeacher += (int)quesModel.TeacherStyle;

                // 5.与学员沟通和互动有效
                quesModel.TeacherCommunication = Convert.ToInt32(Request.Form["radTeacherCommunication"].ToString());
                total += (int)quesModel.TeacherCommunication;
                totalTeacher += (int)quesModel.TeacherCommunication;

                // 四、培训组织和管理
                // 1.培训服务周到细致
                quesModel.OrgService = Convert.ToInt32(Request.Form["radOrgService"].ToString());
                total += (int)quesModel.OrgService;
                totalOrg += (int)quesModel.OrgService;

                // 2.培训时间安排和控制合理
                quesModel.OrgTime = Convert.ToInt32(Request.Form["radOrgTime"].ToString());
                total += (int)quesModel.OrgTime;
                totalOrg += (int)quesModel.OrgTime;

                // 3.培训场所、设备安排到位
                quesModel.OrgArrange = Convert.ToInt32(Request.Form["radOrgArrange"].ToString());
                total += (int)quesModel.OrgArrange;
                totalOrg += (int)quesModel.OrgArrange;

                // 五、您对本课程还有哪些建议？
                quesModel.Suggest = Request.Form["txtSuggest"].ToString();

                quesModel.Total = total;
                quesModel.TotalOrg = totalOrg;
                quesModel.TotalCousre = totalCourse;
                quesModel.TotalTeacher = totalTeacher;


                quesModel.TrainRemark = Request.Form["txtRequire"].ToString();
                 
                 

                // 保存学生对课程的评估信息
                //DataTable dt = BLL.Course.GetCourseByCorIdStuId(clacouId, Profile.CurrentUser.UserId.ToString());
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        if (dr["CourseId"] != null)
                //        {
                //            quesModel.CourseId = Guid.Parse(dr["CourseID"].ToString());
                //        }
                //        quesModel.ClassId = dr["ClassId"].ToString();
                //        quesModel.QuestairId = Guid.Empty.ToString();
                //        quesModel.TeacherId = dr["TeacherId"].ToString();
                //        quesModel.TeacherName = dr["TeacherName"].ToString();

                //        queBll.Add(quesModel);
                //    }
                //}
                DataSet ds = queBll.GetQuestRelatByCCId(clacouId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["CourseID"] != null)
                        {
                            quesModel.CourseId = Guid.Parse(dr["CourseID"].ToString());
                        }
                        quesModel.ClassId = dr["ClassId"].ToString();
                        quesModel.QuestairId = dr["Id"].ToString();
                        quesModel.TeacherId = dr["TeacherId"].ToString();
                        quesModel.TeacherName = dr["TeacherName"].ToString();

                        queBll.Add(quesModel);
                    }
                }
                
                //if ()
                //{
                //    queBll.SubmitQuestionnaireState(Profile.CurrentUser.UserId, courseId);
                //}
                Response.Redirect("MyQuestionnaireNew.aspx");
            }
            catch (Exception ex)
            {
                Utility.LogHelper.WriteLogofExceptioin(ex);
            }
        }
    }
}