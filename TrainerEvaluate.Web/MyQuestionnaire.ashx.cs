using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// MyQuestionnaire 的摘要说明
    /// </summary>
    public class MyQuestionnaire : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var opType = context.Request["t"];
            var qt = context.Request["qt"];
            switch (opType)
            {
                case "mqr":
                    if (qt == "q")
                    {
                        var strq = Query(context);
                        context.Response.Write(strq);
                    }
                    else
                    {
                        var strmqr = GetQuestionByStudentId(context);
                        context.Response.Write(strmqr);
                    }
                    break;
                case "shrt":
                    var strRt = SetResultContent(context);
                    context.Response.Write(strRt);
                    break;
                default:
                    GetDataForCombobox(context);
                    break;
            }

            GetDataForCombobox(context);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string GetQuestionByStudentId(HttpContext context)
        {
            var ds = new DataSet();
            var bll = new BLL.Questionnaire();
            var studentId = context.Request["studentId"];

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "AppraiserTime" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "desc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = bll.GetQuestionByStudentIdCount("", studentId);

            ds = bll.GetListStudentIdByPage("",studentId, sort, startIndex, endIndex, order);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }

        private string Query(HttpContext context)
        {
            var ds = new DataSet();
            var bll = new BLL.Questionnaire();
            var studentId = context.Request["studentId"];

            var corName = context.Request["corName"].Trim();
            var teaName = context.Request["teacherName"].Trim();

            var strWhere = "";
            if (!string.IsNullOrEmpty(corName))
            {
                strWhere = string.Format(" and CourseName like '%" + corName + "%' ");
            }
            if (!string.IsNullOrEmpty(teaName))
            {
                strWhere = string.Format(" and T.TeacherName like '%" + teaName + "%' ");
            }

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "AppraiserTime" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "desc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = bll.GetQuestionByStudentIdCount(strWhere, studentId);

            ds = bll.GetListStudentIdByPage(strWhere, studentId, sort, startIndex, endIndex, order);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }

        private void GetDataForCombobox(HttpContext context)
        {
            try
            {
                var uid = HttpContext.Current.Request["uid"];
                var questionbll = new BLL.Questionnaire();
                var ds = questionbll.GetStudentQuestionnaireNew(uid);

                //  [{"SUBITEM_VALUE":"1","SUBITEM_NAME":"男"},{"SUBITEM_VALUE":"2","SUBITEM_NAME":"女"}]  

                if (ds != null && ds.Tables.Count > 0)
                {
                    var str = new StringBuilder("[");
                    var i = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        i++;
                        str.Append("{\"CourseId\": \"" + row["ID"] + "\",");
                        str.Append("\"CourseName\": \"" + i + ". " + row["CourseName"] + "\"},");
                    }
                    str.Remove(str.Length - 1, 1);
                    str.Append("]");

                    context.Response.Write(str.ToString());
                }
            }
            catch (Exception exception)
            {
                LogHelper.WriteLogofExceptioin(exception);
            }
        }

        private string SetResultContent(HttpContext context)
        {
            string strResult = string.Empty;

            try
            {
                var id = HttpContext.Current.Request["id"];
                var qairBll = new BLL.Questionnaire();
                DataSet dsQ = qairBll.GetQuesAireById(id);
                DataTable dtRQ = new DataTable();
                string courseId = string.Empty;
                string userid = HttpContext.Current.Request["userid"]; 
                if(dsQ != null && dsQ.Tables.Count > 0 && dsQ.Tables[0].Rows.Count > 0)
                {
                    dtRQ = dsQ.Tables[0];
                }
                if(dtRQ.Rows.Count>0)
                {
                    courseId = dtRQ.Rows[0]["CourseId"].ToString();
                }

                var queBll = new BLL.QuestionnaireSurvey();
                DataSet ds = queBll.GetQuestionnaireSurveryList();
                StringBuilder strBuilder = new StringBuilder();

                DataTable dtTT = BLL.Course.GetCourseByCorIdStuId(courseId, userid);

                if (dtTT.Rows.Count > 0)
                {
                    strBuilder.Append(" <table width='98%' border='0' cellspacing='1' cellpadding='3' align='center' bgcolor='C4D4E1' style='margin-left: 10px;'> ");
                    strBuilder.Append(" <tr><td width='16%' bgcolor='F8DCC2' class='gray10a' height='25'> ");
                    strBuilder.Append(" <div align='center'>课程名称：</div></td> ");
                    strBuilder.Append(" <td width='35%' bgcolor='F8DCC2' height='25' class='gray10a'> ");
                    strBuilder.Append(" <span id='ipCourseName'>" + dtTT.Rows[0]["CourseName"].ToString() + "</span></td> ");
                    strBuilder.Append(" <td width='15%' bgcolor='F8DCC2' class='gray10a' height='25'> ");
                    strBuilder.Append(" <div align='center'>授课教师： </div></td> ");
                    strBuilder.Append(" <td width='34%' bgcolor='F8DCC2' height='25' class='gray10a'> ");
                    strBuilder.Append(" <span id='ipTeacher'>" + dtTT.Rows[0]["TeacherName"].ToString() + "</span></td></tr> ");
                    strBuilder.Append(" <tr><td width='16%' bgcolor='F9ECD9' class='gray10a' height='25'> ");
                    strBuilder.Append(" <div align='center'>授课时间：</div></td> ");
                    strBuilder.Append(" <td width='35%' bgcolor='F9ECD9' height='25' class='gray10a'> ");
                    strBuilder.Append(" <span id='ipTime'>" + Convert.ToDateTime(dtTT.Rows[0]["StartDate"].ToString()).ToString("yyyy-MM-dd") + "到" +
                                       Convert.ToDateTime(dtTT.Rows[0]["FinishDate"].ToString()).ToString("yyyy-MM-dd") + "</span></td> ");
                    strBuilder.Append(" <td width='15%' bgcolor='F9ECD9' class='gray10a' height='25'> ");
                    strBuilder.Append(" <div align='center'>授课地点： </div></td> ");
                    strBuilder.Append(" <td width='34%' bgcolor='F9ECD9' height='25' class='gray10a'> ");
                    strBuilder.Append(" <span id='ipPlace'>" + dtTT.Rows[0]["TeachPlace"].ToString() + "</span></td></tr> ");
                    strBuilder.Append(" <tr><td width='16%' colspan='4' bgcolor='#FFFFFF' class='gray10a' height='25'>&nbsp; </td> ");
                    strBuilder.Append(" </tr></table> ");
                }

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
                            int rqst = 0;
                            string check1 = string.Empty;
                            string check2 = string.Empty;
                            string check3 = string.Empty;
                            string check4 = string.Empty;
                            string suggest = string.Empty;

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
                                                #region test
                                                //// 二、课程内容
                                                //// 1.课程主题清晰明确
                                                //if (drChild["ShowId"].ToString() == "radSubject")
                                                //{
                                                //    rqst = Convert.ToInt32(dtRQ.Rows[0]["CourseSubject"]);
                                                //}

                                                //// 2.课程内容丰富、能吸引人
                                                //if (drChild["ShowId"].ToString() == "radContentRich")
                                                //{
                                                //    rqst = Convert.ToInt32(dtRQ.Rows[0]["CourseRich"]);
                                                //}

                                                //// 3.课程内容切合实际，能指导实践
                                                //if (drChild["ShowId"].ToString() == "radCoursePractical")
                                                //{
                                                //    rqst = Convert.ToInt32(dtRQ.Rows[0]["CoursePractical"]);
                                                //}

                                                //// 4.课程内容重点突出，易于理解
                                                //if (drChild["ShowId"].ToString() == "radCourseKey")
                                                //{
                                                //    rqst = Convert.ToInt32(dtRQ.Rows[0]["CourseKey"]);
                                                //}

                                                //// 5.课程内容有助于个人发展
                                                //if (drChild["ShowId"].ToString() == "radCourseDevelop")
                                                //{
                                                //    rqst = Convert.ToInt32(dtRQ.Rows[0]["CourseDevelop"]);
                                                //}

                                                //// 三、培训讲师
                                                //// 1.讲师准备比较充分
                                                //if (drChild["ShowId"].ToString() == "radTeacherPrepare")
                                                //{
                                                //    rqst = Convert.ToInt32(dtRQ.Rows[0]["TeacherPrepare"]);
                                                //}

                                                //// 2.语言表达清晰，态度端正
                                                //if (drChild["ShowId"].ToString() == "radTeacherLanguage")
                                                //{
                                                //    rqst = Convert.ToInt32(dtRQ.Rows[0]["TeacherLanguage"]);
                                                //}

                                                //// 3.仪表仪容端庄大方，有亲和力
                                                //if (drChild["ShowId"].ToString() == "radTeacherBearing")
                                                //{
                                                //    rqst = Convert.ToInt32(dtRQ.Rows[0]["TeacherBearing"]);
                                                //}

                                                //// 4.培训方式多样，生动有趣
                                                //if (drChild["ShowId"].ToString() == "radTeacherStyle")
                                                //{
                                                //    rqst = Convert.ToInt32(dtRQ.Rows[0]["TeacherStyle"]);
                                                //}

                                                //// 5.与学员沟通和互动有效
                                                //if (drChild["ShowId"].ToString() == "radTeacherCommunication")
                                                //{
                                                //    rqst = Convert.ToInt32(dtRQ.Rows[0]["TeacherCommunication"]);
                                                //}

                                                //// 四、培训组织和管理
                                                //// 1.培训服务周到细致
                                                //if (drChild["ShowId"].ToString() == "radOrgService")
                                                //{
                                                //    rqst = Convert.ToInt32(dtRQ.Rows[0]["OrgService"]);
                                                //}

                                                //// 2.培训时间安排和控制合理
                                                //if (drChild["ShowId"].ToString() == "radOrgTime")
                                                //{
                                                //    rqst = Convert.ToInt32(dtRQ.Rows[0]["OrgTime"]);
                                                //}

                                                //// 3.培训场所、设备安排到位
                                                //if (drChild["ShowId"].ToString() == "radOrgArrange")
                                                //{
                                                //    rqst = Convert.ToInt32(dtRQ.Rows[0]["OrgArrange"]);
                                                //}
                                                #endregion

                                                if (drChild["ShowId"].ToString() == "radSubject")
                                                {
                                                    rqst = Convert.ToInt32(dtRQ.Rows[0]["CourseSubject"]);
                                                }
                                                else if (drChild["ShowId"].ToString() == "radContentRich")
                                                {
                                                    rqst = Convert.ToInt32(dtRQ.Rows[0]["CourseRich"]);
                                                }
                                                else
                                                {
                                                    rqst = Convert.ToInt32(dtRQ.Rows[0][drChild["ShowId"].ToString().Replace("rad", "")]);
                                                }

                                                switch (rqst)
                                                {
                                                    case 4:
                                                        check1 = "checked='checked'";
                                                        break;
                                                    case 3:
                                                        check2 = "checked='checked'";
                                                        break;
                                                    case 2:
                                                        check3 = "checked='checked'";
                                                        break;
                                                    case 1:
                                                        check4 = "checked='checked'";
                                                        break;
                                                    default:
                                                        check1 = string.Empty;
                                                        check2 = string.Empty;
                                                        check3 = string.Empty;
                                                        check4 = string.Empty;
                                                        break;
                                                }

                                                strBuilder.Append("<tr><td colspan='4' bgcolor='#FFFFFF' class='gray10a' height='26'>");
                                                strBuilder.Append("<img src='images/bank.gif' width='25' height='10'>" + drChild["ShowName"] + "</td>");
                                                strBuilder.Append("</tr><tr><td colspan='4' bgcolor='F0F9FF' class='gray10a' height='29'>");
                                                strBuilder.Append("<img src='images/bank.gif' width='35' height='10'>");
                                                strBuilder.Append("<input type='radio' " + check1 + " name='" + drChild["ShowId"] + "' id='" + drChild["ShowId"] + "1' value='" + drChild["OptValue1"] + "' />" + drChild["OptText1"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                                strBuilder.Append("<input type='radio' " + check2 + " name='" + drChild["ShowId"] + "' id='" + drChild["ShowId"] + "2' value='" + drChild["OptValue2"] + "' />" + drChild["OptText2"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                                strBuilder.Append("<input type='radio' " + check3 + " name='" + drChild["ShowId"] + "' id='" + drChild["ShowId"] + "3' value='" + drChild["OptValue3"] + "' />" + drChild["OptText3"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                                strBuilder.Append("<input type='radio' " + check4 + " name='" + drChild["ShowId"] + "' id='" + drChild["ShowId"] + "4' value='" + drChild["OptValue4"] + "' />" + drChild["OptText4"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                                strBuilder.Append("</td></tr>");

                                                rqst = 0;
                                                check1 = string.Empty;
                                                check2 = string.Empty;
                                                check3 = string.Empty;
                                                check4 = string.Empty;
                                            }
                                        }
                                        break;
                                    case "AnswerRadio":
                                        // 一、您对本次培训课程的总体评价是：
                                        if (dr["ShowId"].ToString() == "radAll")
                                        {
                                            rqst = Convert.ToInt32(dtRQ.Rows[0]["TotalEvaluation"]);
                                            switch (rqst)
                                            {
                                                case 4:
                                                    check1 = "checked='checked'";
                                                    break;
                                                case 3:
                                                    check2 = "checked='checked'";
                                                    break;
                                                case 2:
                                                    check3 = "checked='checked'";
                                                    break;
                                                case 1:
                                                    check4 = "checked='checked'";
                                                    break;
                                                default:
                                                    check1 = string.Empty;
                                                    check2 = string.Empty;
                                                    check3 = string.Empty;
                                                    check4 = string.Empty;
                                                    break;
                                            }
                                        }
                                        strBuilder.Append("<tr><td colspan='4' bgcolor='F0F9FF' class='gray10a' height='25'>");
                                        strBuilder.Append("<img src='images/bank.gif' width='35' height='10'>");
                                        strBuilder.Append("<input type='radio' " + check1 + " name='" + dr["ShowId"] + "' id='" + dr["ShowId"] + "1' value='" + dr["OptValue1"] + "' />" + dr["OptText1"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                        strBuilder.Append("<input type='radio' " + check2 + " name='" + dr["ShowId"] + "' id='" + dr["ShowId"] + "2' value='" + dr["OptValue2"] + "' />" + dr["OptText2"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                        strBuilder.Append("<input type='radio' " + check3 + " name='" + dr["ShowId"] + "' id='" + dr["ShowId"] + "3' value='" + dr["OptValue3"] + "' />" + dr["OptText3"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                        strBuilder.Append("<input type='radio' " + check4 + " name='" + dr["ShowId"] + "' id='" + dr["ShowId"] + "4' value='" + dr["OptValue4"] + "' />" + dr["OptText4"] + "&nbsp;&nbsp;&nbsp;&nbsp;");
                                        strBuilder.Append("</td></tr>");
                                        rqst = 0;
                                        check1 = string.Empty;
                                        check2 = string.Empty;
                                        check3 = string.Empty;
                                        check4 = string.Empty;
                                        break;
                                    case "AnswerText":
                                        suggest = dtRQ.Rows[0][dr["ShowId"].ToString().Replace("txt", "")].ToString();
                                        strBuilder.Append("<tr><td colspan='4' bgcolor='#FFFFFF' class='gray10a' height='160' style=' vertical-align:top; padding-top:10px; padding-left:10px; padding-right:10px; line-height:28px;'>");
                                        strBuilder.Append("<img src='images/bank.gif' width='25' height='10'>" + suggest);
                                        strBuilder.Append("</td></tr>");
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }

                    strBuilder.Append("</table>");
                    strResult = strBuilder.ToString();
                }                
            }
            catch (Exception exception)
            {
                LogHelper.WriteLogofExceptioin(exception);
            }

            return strResult;
        }
    }
}