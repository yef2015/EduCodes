using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.Web
{
    public partial class AnaysisContent  : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["t"]) && Request.QueryString["t"]=="r")
            { 
                container1.Visible = false;
                analysisTable.Visible = false;
                theYear.Visible = false;
                divReports.Visible = true;
                var classId = Request.QueryString["classId"];
                switch (Request.QueryString["sid"])
                {
                    case "1":
                        // 课程评估总体情况统计表
                        SetTotalReports(classId);
                        break;
                    case "2":
                        // 课程内容各指标满意度分布表
                        SetCourseReports(classId);
                        break; 
                    case "3":
                        // 培训讲师各指标满意度分布表
                        SetTeacherReports(classId);
                        break; 
                    case "4":
                        // 培训组织和管理满意度分布表
                        SetOrgReports(classId);
                        break;
                    case "5":
                        // 培训教师满意度
                        SetTrainTeachReports(classId);
                        break;
                    case "6":
                        // 培训课程满意度
                        SetTrainCourseReports(classId);
                        break;
                    default:
                        break; 
                } 
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["sid"]))
                {
                    container1.Visible = true;
                    divReports.Visible = false;
                    analysisTable.Visible = false;
                    theYear.Visible = false;
                    var classId = Request.QueryString["classid"];
                    SetSatisfyBar(Request.QueryString["sid"], classId);
                }
                else
                {
                    // 中青年干部教育管理培训班课程评估表
                    //theYear.InnerText = DateTime.Now.Year + "年中青年干部教育管理培训班课程评估表";
                    container1.Visible = false;
                    divReports.Visible = false;
                    analysisTable.Visible = true;
                    SetValue();                    
                }              
            }         
        }


        /// <summary>
        /// 课程评估总体情况表
        /// </summary>
        private void SetTotalReports(string classId)
        {
            var str = new StringBuilder(); 

            str.Append( "<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" bordercolor=\"#000000\" bgcolor=\"#FFFFFF\" " +
                        " style=\"border-collapse:collapse;font-size: 14px;text-align:center\" >");  
            str.Append("<tr height=\"40\">  ");
            str.Append("<td colspan='11'> <span  style=\"font-size: 25px;font-weight: bold\">课程评估总体情况统计表</span><br/> </td>");
            str.Append("</tr>  ");

            str.Append("<tr  height=\"35\"  bgcolor=\"#F0F9FF\" >");
            str.Append("<td><strong>班级名称</strong></td>");
            str.Append("<td><strong>课程名称</strong></td>");
            str.Append("<td><strong>授课教师</strong></td>");
            str.Append("<td><strong>总平均分（满分52分）</strong></td>");
            str.Append("<td><strong>总体满意度</strong></td>");
            str.Append("<td><strong>评估等级</strong></td>");
            str.Append("<td><strong>课程内容满意度</strong></td>");
            str.Append("<td><strong>培训讲师满意度</strong></td>");
            str.Append("<td><strong>培训组织和管理满意度</strong></td>");
            str.Append("<td><strong>实评人数</strong></td>");
            str.Append("<td><strong>培训时间</strong></td>");
            str.Append("</tr>");
             
            var report = new BLL.Questionnaire();
            var dt = report.GetTotalReport(classId);
            var i = 0;
            if (dt != null && dt.Rows.Count > 0)
            { 
                foreach (DataRow row in dt.Rows)
                { 
                    var totalSatisfy = Convert.ToDouble(row["TotalSatisfy"]) >= 1.0
                        ? "100%"
                        : string.Format("{0:N2}%", Convert.ToDouble(row["TotalSatisfy"]) * 100);
                    var totalCourse = Convert.ToDouble(row["TotalCourse"]) >= 1.0
                       ? "100%"
                       : string.Format("{0:N2}%", Convert.ToDouble(row["TotalCourse"]) * 100);
                    var totalTeacher = Convert.ToDouble(row["TotalTeacher"]) >= 1.0
                       ? "100%"
                       : string.Format("{0:N2}%", Convert.ToDouble(row["TotalTeacher"]) * 100);
                    var totalOrg = Convert.ToDouble(row["TotalOrg"]) >= 1.0
                       ? "100%"
                       : string.Format("{0:N2}%", Convert.ToDouble(row["TotalOrg"]) * 100);

                    i++;
                    var color = i % 2 == 1 ? "#FFFFFF" : "#F0F9FF";
                    str.Append("<tr  height=\"35\"  bgcolor=\""+color+"\" > ");
                    str.Append("<td>" + row["ClassName"] + " </td>");
                    str.Append("<td>" + row["CourseName"] + " </td>");
                    str.Append("<td>" + row["MyTeacherName"] + " </td>");
                    str.Append("<td>" + string.Format("{0:N2}",row["TotalAvgScore"]) + " </td>");
                    str.Append("<td>" +  totalSatisfy + " </td>");
                    str.Append("<td>" + row["ToLevel"] + " </td>");
                    str.Append("<td>" + totalCourse + " </td>");
                    str.Append("<td>" + totalTeacher + " </td>");
                    str.Append("<td>" + totalOrg + " </td>");
                    str.Append("<td>" + row["TotalDone"] + " </td>");
                    str.Append("<td>从" + Convert.ToDateTime(row["CourseStartTime"]).ToString("yyyy-MM-dd") + "到"
                        + Convert.ToDateTime(row["CourseFinishTime"]).ToString("yyyy-MM-dd") + " </td>");  
                    str.Append("</tr>");
                }
            }
            i++;
            var color1 = i % 2 == 1 ? "#FFFFFF" : "#F0F9FF";
            str.Append("<tr  height=\"35\" bgcolor=\""+color1+"\" > ");
            str.Append("<td colspan='11'>");
            str.Append("总平均分=各项得分总和/实评人数；满意度=（很满意+满意）/实评人数； 课程（讲师或者组织）的满意度=每项满意度相加/项数 ");
            str.Append("</td>");
            str.Append("</tr>");

            str.Append("<tr  height=\"35\" bgcolor=\"#FFFFFF\" > ");
            str.Append("<td colspan='11'>");
            str.Append("<a href=\"javascript:void(0)\" class=\"easyui-linkbutton c6\" iconcls=\"icon-ok\" onclick=\"getTotalReports('" + classId + "')\" style=\"width: 120px\">导出</a>");
            str.Append("</td>");
            str.Append("</tr>"); 

            str.Append("</table>");
            divReports.InnerHtml = str.ToString();
        }


        private void SetTeacherReports(string classId)
        {
            var str = new StringBuilder();

            str.Append("<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" bordercolor=\"#000000\" bgcolor=\"#FFFFFF\" " +
                        " style=\"border-collapse:collapse;font-size: 14px;text-align:center\" >");
            str.Append("<tr height=\"40\">  ");
            str.Append("<td colspan='8'> <span  style=\"font-size: 25px;font-weight: bold\">培训讲师各指标满意度分布表</span><br/> </td>");
            str.Append("</tr>  ");

            str.Append("<tr  height=\"35\"  bgcolor=\"#F0F9FF\" >");
            str.Append("<td  rowspan=\"2\" ><strong>班级名称</strong></td>");
            str.Append("<td  rowspan=\"2\" ><strong>课程名称</strong></td>");
            str.Append("<td  rowspan=\"2\" ><strong>教师姓名</strong></td>");
          //  str.Append("<td  rowspan=\"2\" ><strong>培训教师</strong></td>");
           // str.Append("<td  rowspan=\"2\" ><strong>培训时间</strong></td>");
            str.Append("<td colspan=\"5\" ><strong>培训讲师各指标满意度</strong></td>");
            str.Append("</tr>");
            str.Append("<tr  height=\"35\" bgcolor=\"#F0F9FF\">");
            str.Append("<td><strong>讲师准备比较充分</strong></td>");
            str.Append("<td><strong>语言表达清晰，态度端正</strong></td>");
            str.Append("<td><strong>仪表仪容端庄大方，有亲和力</strong></td>");
            str.Append("<td><strong>培训方式多样，生动有趣</strong></td>");
            str.Append("<td><strong>与学员沟通和互动有效</strong></td>");
            str.Append("</tr>");  
            var report = new BLL.Questionnaire();
            var dt = report.GetTeacherReport(classId);
            var i = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var teacherBearingP = Convert.ToDouble(row["TeacherBearingP"]) >= 1.0
                        ? "100%"
                        : string.Format("{0:N2}%", Convert.ToDouble(row["TeacherBearingP"]) * 100);
                    var teacherCommunicationP = Convert.ToDouble(row["TeacherCommunicationP"]) >= 1.0
                       ? "100%"
                       : string.Format("{0:N2}%", Convert.ToDouble(row["TeacherCommunicationP"]) * 100);
                    var teacherLanguageP = Convert.ToDouble(row["TeacherLanguageP"]) >= 1.0
                       ? "100%"
                       : string.Format("{0:N2}%", Convert.ToDouble(row["TeacherLanguageP"]) * 100);
                    var teacherPrepareP = Convert.ToDouble(row["TeacherPrepareP"]) >= 1.0
                       ? "100%"
                       : string.Format("{0:N2}%", Convert.ToDouble(row["TeacherPrepareP"]) * 100);
                    var teacherStyleP = Convert.ToDouble(row["TeacherStyleP"]) >= 1.0
                       ? "100%"
                       : string.Format("{0:N2}%", Convert.ToDouble(row["TeacherStyleP"]) * 100);

                    i++;
                    var color = i % 2 == 1 ? "#FFFFFF" : "#F0F9FF";
                    str.Append("<tr  height=\"35\"  bgcolor=\"" + color + "\" > ");
                    str.Append("<td>" + row["ClassName"] + " </td>");
                    str.Append("<td>" + row["CourseName"] + " </td>");
                    str.Append("<td>" + row["TeacherName"] + " </td>");
                //    str.Append("<td>" + row["TeacherName"] + " </td>");
                //    str.Append("<td>" + row["TeachTime"] + " </td>"); 
                    str.Append("<td>" + teacherPrepareP + " </td>");
                    str.Append("<td>" + teacherLanguageP + " </td>");
                    str.Append("<td>" + teacherBearingP + " </td>");
                    str.Append("<td>" + teacherStyleP + " </td>");
                    str.Append("<td>" + teacherCommunicationP + " </td>"); 
                    str.Append("</tr>");
                }
            }
            //i++;
            //var color1 = i % 2 == 1 ? "#FFFFFF" : "#F0F9FF";
            //str.Append("<tr  height=\"35\" bgcolor=\"" + color1 + "\" > ");
            //str.Append("<td colspan='10'>");
            //str.Append("总平均分=各项得分总和/实评人数；满意度=（很满意+满意）/实评人数； 课程（讲师或者组织）的满意度=每项满意度相加/项数 ");
            //str.Append("</td>");
            //str.Append("</tr>");

            str.Append("<tr  height=\"35\" bgcolor=\"#FFFFFF\" > ");
            str.Append("<td colspan='10'>");
            str.Append("<a href=\"javascript:void(0)\" class=\"easyui-linkbutton c6\" iconcls=\"icon-ok\" onclick=\"getTeacherReports('"+classId+"')\" style=\"width: 120px\">导出</a>");
            str.Append("</td>");
            str.Append("</tr>");

            str.Append("</table>");
            divReports.InnerHtml = str.ToString();
        }


        private void SetCourseReports(string classId)
        {
            var str = new StringBuilder();

            str.Append("<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" bordercolor=\"#000000\" bgcolor=\"#FFFFFF\" " +
                        " style=\"border-collapse:collapse;font-size: 14px;text-align:center\" >");
            str.Append("<tr height=\"40\">  ");
            str.Append("<td colspan='7'> <span  style=\"font-size: 25px;font-weight: bold\">课程内容各指标满意度分布表</span><br/> </td>");
            str.Append("</tr>  ");

            str.Append("<tr  height=\"35\"  bgcolor=\"#F0F9FF\" >");
            str.Append("<td  rowspan=\"2\" ><strong>班级名称</strong></td>");
            str.Append("<td  rowspan=\"2\" ><strong>课程名称</strong></td>");
            //  str.Append("<td  rowspan=\"2\" ><strong>培训教师</strong></td>");
            // str.Append("<td  rowspan=\"2\" ><strong>培训时间</strong></td>");
            str.Append("<td colspan=\"5\" ><strong>课程内容各指标满意度</strong></td>");
            str.Append("</tr>");
            str.Append("<tr  height=\"35\" bgcolor=\"#F0F9FF\">");
            str.Append("<td><strong>课程主题清晰明确</strong></td>");
            str.Append("<td><strong>课程内容丰富、能吸引人</strong></td>");
            str.Append("<td><strong>课程内容切合实际，能指导实践</strong></td>");
            str.Append("<td><strong>课程内容重点突出，易于理解</strong></td>");
            str.Append("<td><strong>课程内容有助于个人发展</strong></td>");
            str.Append("</tr>");
            var report = new BLL.Questionnaire();
            var dt = report.GetCourseReport(classId);
            var i = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var courseSubjectP = Convert.ToDouble(row["CourseSubjectP"]) >= 1.0
                        ? "100%"
                        : string.Format("{0:N2}%", Convert.ToDouble(row["CourseSubjectP"]) * 100);
                    var courseDevelopP = Convert.ToDouble(row["CourseDevelopP"]) >= 1.0
                       ? "100%"
                       : string.Format("{0:N2}%", Convert.ToDouble(row["CourseDevelopP"]) * 100);
                    var courseKeyP = Convert.ToDouble(row["CourseKeyP"]) >= 1.0
                       ? "100%"
                       : string.Format("{0:N2}%", Convert.ToDouble(row["CourseKeyP"]) * 100);
                    var coursePracticalP = Convert.ToDouble(row["CoursePracticalP"]) >= 1.0
                       ? "100%"
                       : string.Format("{0:N2}%", Convert.ToDouble(row["CoursePracticalP"]) * 100);
                    var courseRichP = Convert.ToDouble(row["CourseRichP"]) >= 1.0
                       ? "100%"
                       : string.Format("{0:N2}%", Convert.ToDouble(row["CourseRichP"]) * 100);

                    i++;
                    var color = i % 2 == 1 ? "#FFFFFF" : "#F0F9FF";
                    str.Append("<tr  height=\"35\"  bgcolor=\"" + color + "\" > ");
                    str.Append("<td>" + row["ClassName"] + " </td>");
                    str.Append("<td>" + row["CourseName"] + " </td>");
                    //    str.Append("<td>" + row["TeacherName"] + " </td>");
                    //    str.Append("<td>" + row["TeachTime"] + " </td>"); 
                    str.Append("<td>" + courseSubjectP + " </td>");
                    str.Append("<td>" + courseRichP + " </td>");
                    str.Append("<td>" + coursePracticalP + " </td>");
                    str.Append("<td>" + courseKeyP + " </td>");
                    str.Append("<td>" + courseDevelopP + " </td>");
                    str.Append("</tr>");
                }
            }
            //i++;
            //var color1 = i % 2 == 1 ? "#FFFFFF" : "#F0F9FF";
            //str.Append("<tr  height=\"35\" bgcolor=\"" + color1 + "\" > ");
            //str.Append("<td colspan='10'>");
            //str.Append("总平均分=各项得分总和/实评人数；满意度=（很满意+满意）/实评人数； 课程（讲师或者组织）的满意度=每项满意度相加/项数 ");
            //str.Append("</td>");
            //str.Append("</tr>");

            str.Append("<tr  height=\"35\" bgcolor=\"#FFFFFF\" > ");
            str.Append("<td colspan='7'>");
            str.Append("<a href=\"javascript:void(0)\" class=\"easyui-linkbutton c6\" iconcls=\"icon-ok\" onclick=\"getCourseReports('" + classId + "')\" style=\"width: 120px\">导出</a>");
            str.Append("</td>");
            str.Append("</tr>");

            str.Append("</table>");
            divReports.InnerHtml = str.ToString();
        }


        private void SetOrgReports(string classId)
        {
            var str = new StringBuilder();

            str.Append("<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" bordercolor=\"#000000\" bgcolor=\"#FFFFFF\" " +
                        " style=\"border-collapse:collapse;font-size: 14px;text-align:center\" >");
            str.Append("<tr height=\"40\">  ");
            str.Append("<td colspan='5'> <span  style=\"font-size: 25px;font-weight: bold\">培训组织和管理满意度分布表</span><br/> </td>");
            str.Append("</tr>  ");

            str.Append("<tr  height=\"35\"  bgcolor=\"#F0F9FF\" >");
            str.Append("<td  rowspan=\"2\" ><strong>班级名称</strong></td>");
            str.Append("<td  rowspan=\"2\" ><strong>课程名称</strong></td>");
            //  str.Append("<td  rowspan=\"2\" ><strong>培训教师</strong></td>");
            // str.Append("<td  rowspan=\"2\" ><strong>培训时间</strong></td>");
            str.Append("<td colspan=\"3\" ><strong>培训组织和管理各指标满意度</strong></td>");
            str.Append("</tr>");
            str.Append("<tr  height=\"35\" bgcolor=\"#F0F9FF\">");
            str.Append("<td><strong>培训服务周到细致</strong></td>");
            str.Append("<td><strong>培训时间安排和控制合理</strong></td>");
            str.Append("<td><strong>培训场所、设备安排到位</strong></td>"); 
            str.Append("</tr>");
            var report = new BLL.Questionnaire();
            var dt = report.GetOrgReport(classId);
            var i = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var orgArrangeP = Convert.ToDouble(row["OrgArrangeP"]) >= 1.0
                        ? "100%"
                        : string.Format("{0:N2}%", Convert.ToDouble(row["OrgArrangeP"]) * 100);
                    var orgServiceP = Convert.ToDouble(row["OrgServiceP"]) >= 1.0
                       ? "100%"
                       : string.Format("{0:N2}%", Convert.ToDouble(row["OrgServiceP"]) * 100);
                    var orgTimeP = Convert.ToDouble(row["OrgTimeP"]) >= 1.0
                       ? "100%"
                       : string.Format("{0:N2}%", Convert.ToDouble(row["OrgTimeP"]) * 100); 

                    i++;
                    var color = i % 2 == 1 ? "#FFFFFF" : "#F0F9FF";
                    str.Append("<tr  height=\"35\"  bgcolor=\"" + color + "\" > ");
                    str.Append("<td>" + row["ClassName"] + " </td>");
                    str.Append("<td>" + row["CourseName"] + " </td>");
                    //    str.Append("<td>" + row["TeacherName"] + " </td>");
                    //    str.Append("<td>" + row["TeachTime"] + " </td>"); 
                    str.Append("<td>" + orgServiceP + " </td>");
                    str.Append("<td>" + orgTimeP + " </td>");
                    str.Append("<td>" + orgArrangeP + " </td>"); 
                    str.Append("</tr>");
                }
            }
            //i++;
            //var color1 = i % 2 == 1 ? "#FFFFFF" : "#F0F9FF";
            //str.Append("<tr  height=\"35\" bgcolor=\"" + color1 + "\" > ");
            //str.Append("<td colspan='10'>");
            //str.Append("总平均分=各项得分总和/实评人数；满意度=（很满意+满意）/实评人数； 课程（讲师或者组织）的满意度=每项满意度相加/项数 ");
            //str.Append("</td>");
            //str.Append("</tr>");

            str.Append("<tr  height=\"35\" bgcolor=\"#FFFFFF\" > ");
            str.Append("<td colspan='10'>");
            str.Append("<a href=\"javascript:void(0)\" class=\"easyui-linkbutton c6\" iconcls=\"icon-ok\" onclick=\"getOrgReports('"+classId+"')\" style=\"width: 120px\">导出</a>");
            str.Append("</td>");
            str.Append("</tr>");

            str.Append("</table>");
            divReports.InnerHtml = str.ToString();
        }


        private void SetSatisfyBar(string sid, string classId)
        {
            //hcate.Value = "课程1, 课程2, 课程3, 课程4, 课程5";
            //hdata.Value = "17, 31, 335, 203, 2";
            hcate.Value = "";
            hdata.Value = "";

            try
            {
                var quesBll = new BLL.Questionnaire();
                var dt = quesBll.GetSatisfybar(sid, classId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var str1 = new StringBuilder();
                    var str2 = new StringBuilder(); 
                    foreach (DataRow row in dt.Rows)
                    { 
                        str1.Append(row["CourseName"] + ",");
                       // str2.Append(row["Satisfy"].ToString().Substring(0,4) + ",");   
                        if (Convert.ToDouble(row["Satisfy"])*100 > 100)
                        {
                            str2.Append("100"+ ","); 
                        }
                        else
                        {
                            str2.Append(string.Format("{0:N2}", Convert.ToDouble(row["Satisfy"]) * 100) + ","); 
                        } 
                    }
                    str1.Remove(str1.Length - 1, 1);
                    str2.Remove(str2.Length - 1, 1);

                    hcate.Value = str1.ToString();
                    hdata.Value = str2.ToString();
                } 
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
            }
        }

        /// <summary>
        /// 培训教师满意度
        /// </summary>
        private void SetTrainTeachReports(string classId)
        {
            var str = new StringBuilder();

            str.Append("<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" bordercolor=\"#000000\" bgcolor=\"#FFFFFF\" " +
                        " style=\"border-collapse:collapse;font-size: 14px;text-align:center\" >");
            str.Append("<tr height=\"40\">  ");
            str.Append("<td colspan='4'> <span  style=\"font-size: 25px;font-weight: bold\">培训教师满意度</span><br/> </td>");
            str.Append("</tr>  ");

            str.Append("<tr  height=\"30\"  bgcolor=\"#F0F9FF\" >");
            str.Append("<td><strong>教师姓名</strong></td>");
            str.Append("<td><strong>课程名称</strong></td>");
            str.Append("<td><strong>授课班级</strong></td>");
            str.Append("<td><strong>满意度</strong></td>");
            str.Append("</tr>");

            var report = new BLL.Questionnaire();
            var dt = report.GetTeacherEvaluate();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var rowNum = 1;
                    string teacherId = row["TeacherId"].ToString();
                    DataTable dtInfo = new DataTable();
                    dtInfo = report.GetTeacherSatifyById(teacherId,classId);
                    rowNum = dtInfo.Rows.Count;

                    if(rowNum>1)
                    {
                        str.Append("<tr  height=\"35\" > ");
                        str.Append("<td rowspan='" + rowNum + "' >" + row["TeacherName"] + " </td>");
                        bool isFirst = true;

                        foreach (DataRow rowInfo in dtInfo.Rows)
                        {
                            if (!isFirst)
                            {
                                str.Append("<tr  height=\"35\"> ");
                            }
                            isFirst = false;

                            str.Append("<td>" + rowInfo["CourseName"].ToString() + " </td>");
                            str.Append("<td>" + rowInfo["ClassName"].ToString() + " </td>");

                            var teacherSafy = Convert.ToDouble(rowInfo["TotalTeacher"]) >= 1.0
                               ? "100%"
                               : string.Format("{0:N2}%", Convert.ToDouble(rowInfo["TotalTeacher"]) * 100);
                            str.Append("<td>" + teacherSafy + "</td>");
                            str.Append("</tr>");
                        }
                    }
                    else if(rowNum==1)
                    {
                        str.Append("<tr  height=\"35\" > ");
                        str.Append("<td>" + row["TeacherName"] + " </td>");
                        str.Append("<td>" + dtInfo.Rows[0]["CourseName"].ToString() + " </td>");
                        str.Append("<td>" + dtInfo.Rows[0]["ClassName"].ToString() + " </td>");

                        var teacherSingSafy = Convert.ToDouble(dtInfo.Rows[0]["TotalTeacher"]) >= 1.0
                              ? "100%"
                              : string.Format("{0:N2}%", Convert.ToDouble(dtInfo.Rows[0]["TotalTeacher"]) * 100);
                        str.Append("<td>" + teacherSingSafy + "</td>");

                        str.Append("</tr>");
                    }
                }
            }

            str.Append("<tr  height=\"35\" bgcolor=\"#FFFFFF\" > ");
            str.Append("<td colspan='10'>");
            str.Append("<a href=\"javascript:void(0)\" class=\"easyui-linkbutton c6\" iconcls=\"icon-ok\" onclick=\"getTrainTeachReports('" + classId + "')\" style=\"width: 120px\">导出</a>");
            str.Append("</td>");
            str.Append("</tr>");

            str.Append("</table>");
            divReports.InnerHtml = str.ToString();
        }

        /// <summary>
        /// 培训课程满意度
        /// </summary>
        private void SetTrainCourseReports(string classId)
        {
            var str = new StringBuilder();

            str.Append("<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" bordercolor=\"#000000\" bgcolor=\"#FFFFFF\" " +
                        " style=\"border-collapse:collapse;font-size: 14px;text-align:center\" >");
            str.Append("<tr height=\"40\">  ");
            str.Append("<td colspan='4'> <span  style=\"font-size: 25px;font-weight: bold\">培训课程满意度</span><br/> </td>");
            str.Append("</tr>  ");

            str.Append("<tr  height=\"30\"  bgcolor=\"#F0F9FF\" >");
            str.Append("<td><strong>课程名称</strong></td>");
            str.Append("<td><strong>教师姓名</strong></td>");
            str.Append("<td><strong>授课班级</strong></td>");
            str.Append("<td><strong>满意度</strong></td>");
            str.Append("</tr>");

            var courseId = Request.QueryString["courseId"];  // 课程id
            var report = new BLL.Questionnaire();
            var dt = report.GetCourseEvaluate();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var rowNum = 1;
                    string tempCourseId = row["CourseId"].ToString();
                    DataTable dtInfo = new DataTable();
                    dtInfo = report.GetCourseSatifyById(tempCourseId, classId);
                    rowNum = dtInfo.Rows.Count;

                    if (rowNum > 1)
                    {
                        str.Append("<tr  height=\"35\" > ");
                        str.Append("<td rowspan='" + rowNum + "' >" + row["CourseName"] + " </td>");
                        bool isFirst = true;

                        foreach (DataRow rowInfo in dtInfo.Rows)
                        {
                            if (!isFirst)
                            {
                                str.Append("<tr  height=\"35\"> ");
                            }
                            isFirst = false;

                            str.Append("<td>" + rowInfo["TeacherName"].ToString() + " </td>");
                            str.Append("<td>" + rowInfo["ClassName"].ToString() + " </td>");

                            var courseSafy = Convert.ToDouble(rowInfo["TotalCourse"]) >= 1.0
                               ? "100%"
                               : string.Format("{0:N2}%", Convert.ToDouble(rowInfo["TotalCourse"]) * 100);
                            str.Append("<td>" + courseSafy + "</td>");
                            str.Append("</tr>");
                        }
                    }
                    else if(rowNum==1)
                    {
                        str.Append("<tr  height=\"35\" > ");
                        str.Append("<td>" + row["CourseName"] + " </td>");
                        str.Append("<td>" + dtInfo.Rows[0]["TeacherName"].ToString() + " </td>");
                        str.Append("<td>" + dtInfo.Rows[0]["ClassName"].ToString() + " </td>");

                        var courseSafy = Convert.ToDouble(dtInfo.Rows[0]["TotalCourse"]) >= 1.0
                              ? "100%"
                              : string.Format("{0:N2}%", Convert.ToDouble(dtInfo.Rows[0]["TotalCourse"]) * 100);
                        str.Append("<td>" + courseSafy + "</td>");

                        str.Append("</tr>");
                    }
                }
            }

            str.Append("<tr  height=\"35\" bgcolor=\"#FFFFFF\" > ");
            str.Append("<td colspan='10'>");
            str.Append("<a href=\"javascript:void(0)\" class=\"easyui-linkbutton c6\" iconcls=\"icon-ok\" onclick=\"getTrainCourseReports('" + classId + "')\" style=\"width: 120px\">导出</a>");
            str.Append("</td>");
            str.Append("</tr>");

            str.Append("</table>");
            divReports.InnerHtml = str.ToString();
        }


        private void SetValue()
        {  
            try
            {
                var courseId = Guid.Empty;
                var classId = string.Empty;
                var coursebll = new BLL.Course();
                if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
                {
                    courseId = new Guid(Request.QueryString["cid"]);
                    hCourseid.Value = courseId.ToString();
                }
                else //取个默认的
                {
                    courseId = coursebll.GetTop1Guid();
                    hCourseid.Value = courseId.ToString();
                }
                
                if (!string.IsNullOrEmpty(Request.QueryString["classid"]))
                {
                    classId = Request.QueryString["classid"].ToString();
                    hClassid.Value = classId;
                }
                else //取个默认的
                {

                }

                // 根据课程id，查找所属班级，最后确定课程所属年份
                var classbll = new BLL.Class();
                theYear.InnerText = classbll.GetClassInfoByClassId(classId) + "课程评估表";

                //DataSet dsCourse = coursebll.GetCourseInfoByCourseId(courseId.ToString());
                DataTable dtCourse = coursebll.GetCourseInfoByCourseIdAndClassId(courseId.ToString(), classId);
                if (dtCourse.Rows.Count> 0)
                {
                    DataRow dro = dtCourse.Rows[0];
                    courseName.InnerText = dro["CourseName"].ToString(); 
                    coursePlace.InnerText = dro["TeachPlace"].ToString(); 
                    teacherName.InnerText = dro["TeacherName"].ToString();
                    trainTime.InnerText = "从" + Convert.ToDateTime(dro["StartDate"]).ToString("yyyy-MM-dd") + "到" +
                        Convert.ToDateTime(dro["FinishDate"]).ToString("yyyy-MM-dd");
                }

                var question = new BLL.Questionnaire();
                var dsResult = question.GetReportTile(courseId, classId);
                if (dsResult != null && dsResult.Tables.Count > 0)
                {
                    var row = dsResult.Tables[0].Rows[0];
                    spTotalAvg.InnerText = string.Format("{0:N2}", row["totalAvg"]) + " 分（满分52）";
                    satisfaction.InnerText =Convert.ToDouble(row["Satisfy"])>=1.0? "100%": string.Format("{0:N2}" + "%", ((Convert.ToDecimal(row["Satisfy"])) * 100));
                    level.InnerText = question.GetLevel((Convert.ToDouble(row["Satisfy"])));
                }

                var dsrr = question.GetTotalReportByClassIdAndCourseId(classId,courseId.ToString());
                if (dsrr != null && dsrr.Rows.Count > 0)
                {
                    divCourseContent.InnerText = Convert.ToDouble(dsrr.Rows[0]["TotalCourse"]) >= 1.0 ? "100%" : string.Format("{0:N2}" + "%", ((Convert.ToDecimal(dsrr.Rows[0]["TotalCourse"])) * 100));
                    divTeacher.InnerText = Convert.ToDouble(dsrr.Rows[0]["TotalTeacher"]) >= 1.0 ? "100%" : string.Format("{0:N2}" + "%", ((Convert.ToDecimal(dsrr.Rows[0]["TotalTeacher"])) * 100));
                    divOrg.InnerText = Convert.ToDouble(dsrr.Rows[0]["TotalOrg"]) >= 1.0 ? "100%" : string.Format("{0:N2}" + "%", ((Convert.ToDecimal(dsrr.Rows[0]["TotalOrg"])) * 100));
                }

                //var totalAvg = question.GetTotalAvg(courseId.ToString());
                //spTotalAvg.InnerText = totalAvg.ToString();  
                //satisfaction.InnerText = string.Format("{0:N2}" + "%", (((float) totalAvg/52)*100));
                //level.InnerText = question.GetLevel(((float)totalAvg / 52));

                var statifyPercent = question.GetSatisfyPercent(courseId, classId);
                if (statifyPercent != null)
                {
                    htp1.Value = string.Format("{0:N2}", ((statifyPercent[0])*100));
                    htp2.Value = string.Format("{0:N2}", ((statifyPercent[1]) * 100));
                    htp3.Value = string.Format("{0:N2}", ((statifyPercent[2]) * 100));
                    htp4.Value = string.Format("{0:N2}", ((statifyPercent[3]) * 100));
                    htp5.Value = string.Format("{0:N2}", ((statifyPercent[4]) * 100)); 　
                  //  nofinish.InnerText = statifyPercent[5].ToString();

                    totalPeople.InnerText = statifyPercent[6].ToString()+" 人";
                    totalDone.InnerText = statifyPercent[7].ToString() + " 人";

                    evProgress.InnerText = string.Format("{0:N2}%", ((statifyPercent[7] / statifyPercent[6]) * 100)); 

                }


                SetDetail(courseId,classId); 

                #region old tendency

                //var tendency = question.GetTendency(courseId);
                //if (tendency != null)
                //{
                //    htot1.Value = string.Format("{0:N2}", ((tendency[0]) ));
                //    htot2.Value = string.Format("{0:N2}", ((tendency[1])));
                //    htot3.Value = string.Format("{0:N2}", ((tendency[2])));
                //    htot4.Value = string.Format("{0:N2}", ((tendency[3])));
                //    htot5.Value = string.Format("{0:N2}", ((tendency[4])));
                //}

                #endregion 
                 
                #region old the six

                //var theTop6 = question.GetTheSix(courseId, true);
                //if (theTop6 != null)
                //{
                //    var result = new StringBuilder();
                //    var i = 0;
                //    foreach (var thetop in theTop6)
                //    {
                //        i++;
                //        result.Append(i + "、" + thetop + @"<br>");

                //    }
                //    top6.InnerHtml = result.ToString();

                //}

                //var theBottom6 = question.GetTheSix(courseId, false);
                //if (theBottom6 != null)
                //{
                //    var result = new StringBuilder();
                //    var i = 0;
                //    foreach (var thebop in theBottom6)
                //    {
                //        i++;
                //        result.Append(i + "、" + thebop + @"<br>");

                //    }
                //    bot6.InnerHtml = result.ToString();
                //}

                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
            }
        }

        
        private void SetDetail(Guid coid,string classId)
        {
            var report = new BLL.Questionnaire();
            if (string.IsNullOrEmpty(classId))
            {
                classId = "2015006";
            }
            var reportBody = report.GetReportByCourseAndClassId(coid, classId);
            var result = new Dictionary<int, double[]>();
            if (reportBody != null && reportBody.Tables.Count > 0)
            {
                foreach (DataRow row in reportBody.Tables[0].Rows)
                {
                    result.Add((int)row["num"], new[] { Convert.ToDouble(row["top1"]), Convert.ToDouble(row["top2"]), Convert.ToDouble(row["top3"]), Convert.ToDouble(row["top4"]), Convert.ToDouble(row["top5"]) });
                }
            }

            var allTop1 = string.Format("{0:N2}" + "%", result[1][0] * 100);
            var allTop2 = string.Format("{0:N2}" + "%", result[1][1] * 100);
            var allTop3 = string.Format("{0:N2}" + "%", result[1][2] * 100);
            var allTop4 = string.Format("{0:N2}" + "%", result[1][3] * 100);
            var allTop5 = string.Format("{0:N2}" + "%", result[1][4] * 100);   

            var content1Top1 = result[2][0];
            var content1Top2 = result[2][1];
            var content1Top3 = result[2][2];
            var content1Top4 = result[2][3];
            var content1Top5 = result[2][4];

            var content2Top1 = result[3][0];
            var content2Top2 = result[3][1];
            var content2Top3 = result[3][2];
            var content2Top4 = result[3][3];
            var content2Top5 = result[3][4];

            var content3Top1 = result[4][0];
            var content3Top2 = result[4][1];
            var content3Top3 = result[4][2];
            var content3Top4 = result[4][3];
            var content3Top5 = result[4][4];

            var content4Top1 = result[5][0];
            var content4Top2 = result[5][1];
            var content4Top3 = result[5][2];
            var content4Top4 = result[5][3];
            var content4Top5 = result[5][4];

            var content5Top1 = result[6][0];
            var content5Top2 = result[6][1];
            var content5Top3 = result[6][2];
            var content5Top4 = result[6][3];
            var content5Top5 = result[6][4];


            var teacher1Top1 = result[7][0];
            var teacher1Top2 = result[7][1];
            var teacher1Top3 = result[7][2];
            var teacher1Top4 = result[7][3];
            var teacher1Top5 = result[7][4];


            var teacher2Top1 = result[8][0];
            var teacher2Top2 = result[8][1];
            var teacher2Top3 = result[8][2];
            var teacher2Top4 = result[8][3];
            var teacher2Top5 = result[8][4];

            var teacher3Top1 = result[9][0];
            var teacher3Top2 = result[9][1];
            var teacher3Top3 = result[9][2];
            var teacher3Top4 = result[9][3];
            var teacher3Top5 = result[9][4];

            var teacher4Top1 = result[10][0];
            var teacher4Top2 = result[10][1];
            var teacher4Top3 = result[10][2];
            var teacher4Top4 = result[10][3];
            var teacher4Top5 = result[10][4];

            var teacher5Top1 = result[11][0];
            var teacher5Top2 = result[11][1];
            var teacher5Top3 = result[11][2];
            var teacher5Top4 = result[11][3];
            var teacher5Top5 = result[11][4];



            var org1Top1 = result[12][0];
            var org1Top2 = result[12][1];
            var org1Top3 = result[12][2];
            var org1Top4 = result[12][3];
            var org1Top5 = result[12][4];

            var org2Top1 = result[13][0];
            var org2Top2 = result[13][1];
            var org2Top3 = result[13][2];
            var org2Top4 = result[13][3];
            var org2Top5 = result[13][4];

            var org3Top1 = result[14][0];
            var org3Top2 = result[14][1];
            var org3Top3 = result[14][2];
            var org3Top4 = result[14][3];
            var org3Top5 = result[14][4];

            var dept = "海淀区教育党校";
            var time = DateTime.Now.ToString("yyyy-MM-dd");


            var str = new StringBuilder();
            str.Append(
                "<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" bordercolor=\"#000000\" bgcolor=\"#FFFFFF\"  style=\"border-collapse:collapse;font-size: 14px;\" >");

            str.Append("<tr   height=\"35\">");
            str.Append(
                "<td width=\"302\" colspan=\"2\" valign=\"middle\" align=\"center\"><p align=\"center\"><strong>培训满意度评价项目</strong><strong> </strong></p></td>");
            //   str.Append("<td   valign=\"middle\" align=\"center\"><p>很满意 </p></td>");
            str.Append("<td  width=\"15%\"    valign=\"middle\" align=\"center\"><p>很满意 </p></td>");
            str.Append("<td  width=\"15%\" valign=\"middle\" align=\"center\"><p>满意</p></td>");
            str.Append("<td  width=\"15%\" valign=\"middle\" align=\"center\"><p>一般</p></td>");
            str.Append("<td  width=\"15%\"  valign=\"middle\" align=\"center\"><p>不满意</p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append(
                "<td width=\"302\" colspan=\"2\" valign=\"middle\" ><p align=\"center\"><strong>本次课程总体满意度 </strong></p></td>");
            //    str.Append("<td   valign=\"middle\"><p><strong>"+allTop1+"</strong></p></td>");
            str.Append("<td   align=\"center\" valign=\"middle\"><p><strong>" + allTop2 +
                       "</strong></p></td>");
            str.Append("<td   align=\"center\" valign=\"middle\"><p><strong>" + allTop3 +
                       "</strong></p></td>");
            str.Append("<td   align=\"center\" valign=\"middle\"><p><strong>" + allTop4 +
                       "</strong></p></td>");
            str.Append("<td   align=\"center\" valign=\"middle\"><p><strong>" + allTop5 +
                       "</strong></p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append(
                "<td width=\"85\" rowspan=\"5\" valign=\"middle\"><p align=\"center\"><strong>课程内容</strong><strong> </strong></p></td>");
            str.Append("<td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\">课程主题清晰明确 </p></td>");
            //     str.Append("<td   valign=\"middle\"><p align=\"center\">" + content1Top1 + "人</p></td>");
            str.Append("<td   align=\"center\" valign=\"middle\"><p align=\"center\">" + content1Top2 +
                       "人 </p></td>");
            str.Append("<td   valign=\"middle\"><p align=\"center\">" + content1Top3 + "人 </p></td>");
            str.Append("<td   valign=\"middle\"><p align=\"center\">" + content1Top4 + "人 </p></td>");
            str.Append("<td   valign=\"middle\"><p align=\"center\">" + content1Top5 + "人 </p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append("<td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\">课程内容丰富、能吸引人 </p></td>");
            //    str.Append("<td   valign=\"middle\"><p align=\"center\">" + content2Top1 + "人 </p></td>");
            str.Append("<td   align=\"center\" valign=\"middle\"><p align=\"center\">" + content2Top2 +
                       "人 </p></td>");
            str.Append("<td   valign=\"middle\"><p align=\"center\">" + content2Top3 + "人 </p></td>");
            str.Append("<td   valign=\"middle\"><p align=\"center\">" + content2Top4 + "人 </p></td>");
            str.Append("<td   valign=\"middle\"><p align=\"center\">" + content2Top5 + "人 </p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append("<td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\">课程内容切合实际，能指导实践 </p></td>");
            //    str.Append("<td   valign=\"middle\"><p align=\"center\">" + content3Top1 + "人 </p></td>");
            str.Append("<td   align=\"center\" valign=\"middle\"><p align=\"center\">" + content3Top2 +
                       "人 </p></td>");
            str.Append("<td   valign=\"middle\"><p align=\"center\">" + content3Top3 + "人 </p></td>");
            str.Append("<td   valign=\"middle\"><p align=\"center\">" + content3Top4 + "人 </p></td>");
            str.Append("<td   valign=\"middle\"><p align=\"center\">" + content3Top5 + "人 </p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append("<td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\">课程内容重点突出，易于理解 </p></td>");
            //     str.Append("<td   valign=\"middle\"><p align=\"center\">" + content4Top1 + "人 </p></td>");
            str.Append("<td   align=\"center\" valign=\"middle\"><p align=\"center\">" + content4Top2 +
                       "人 </p></td>");
            str.Append("<td   valign=\"middle\"><p align=\"center\">" + content4Top3 + "人 </p></td>");
            str.Append("<td   valign=\"middle\"><p align=\"center\">" + content4Top4 + "人 </p></td>");
            str.Append("<td   valign=\"middle\"><p align=\"center\">" + content4Top5 + "人 </p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append("<td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\">课程内容有助于个人发展 </p></td>");
            //   str.Append("<td   valign=\"middle\"><p align=\"center\">" + content5Top1 + "人 </p></td>");
            str.Append("<td   align=\"center\" valign=\"middle\"><p align=\"center\">" + content5Top2 +
                       "人 </p></td>");
            str.Append("<td   valign=\"middle\"><p align=\"center\">" + content5Top3 + "人 </p></td>");
            str.Append("<td   valign=\"middle\"><p align=\"center\">" + content5Top4 + "人 </p></td>");
            str.Append("<td  valign=\"middle\"><p align=\"center\">" + content5Top5 + "人 </p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append(
                "<td width=\"85\" rowspan=\"5\" valign=\"middle\"><p align=\"center\"><a name=\"OLE_LINK2\" id=\"OLE_LINK2\"></a><a name=\"OLE_LINK1\" id=\"OLE_LINK1\"><strong>培训讲师</strong></a><strong> </strong></p></td>");
            str.Append(
                "<td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK6\" id=\"OLE_LINK6\"></a><a name=\"OLE_LINK5\" id=\"OLE_LINK5\">讲师准备比较充分</a> </p></td>");


            //     str.Append("<td   valign=\"middle\"><p align=\"center\">" + teacher1Top1 + "人 </p></td>");
            str.Append(" <td   align=\"center\" valign=\"middle\"><p align=\"center\">" + teacher1Top2 +
                       "人 </p></td>");
            str.Append(" <td   valign=\"middle\"><p align=\"center\">" + teacher1Top3 + "人 </p></td>");
            str.Append(" <td   valign=\"middle\"><p align=\"center\">" + teacher1Top4 + "人 </p></td>");
            str.Append(" <td  valign=\"middle\"><p align=\"center\">" + teacher1Top5 + "人 </p></td>");

            str.Append(" </tr>");
            str.Append("  <tr   height=\"35\">");
            str.Append(
                "   <td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK8\" id=\"OLE_LINK8\"></a><a name=\"OLE_LINK7\" id=\"OLE_LINK7\">语言表达清晰，态度端正</a> </p></td>");
            //     str.Append("  <td   valign=\"middle\"><p align=\"center\">" + teacher2Top1 + "人 </p></td>");
            str.Append(" <td   align=\"center\" valign=\"middle\"><p align=\"center\">" + teacher2Top2 +
                       "人 </p></td>");
            str.Append(" <td   valign=\"middle\"><p align=\"center\">" + teacher2Top3 + "人 </p></td>");
            str.Append("  <td   valign=\"middle\"><p align=\"center\">" + teacher2Top4 + "人 </p></td>");
            str.Append("  <td   valign=\"middle\"><p align=\"center\">" + teacher2Top5 + "人 </p></td>");
            str.Append("   </tr>");
            str.Append(" <tr   height=\"35\">");
            str.Append(
                "    <td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK10\" id=\"OLE_LINK10\"></a><a name=\"OLE_LINK9\" id=\"OLE_LINK9\">仪表仪容端庄大方，有亲和力</a> </p></td>");
            //     str.Append("    <td   valign=\"middle\"><p align=\"center\">" + teacher3Top1 + "人 </p></td>");
            str.Append("   <td   align=\"center\" valign=\"middle\"><p align=\"center\">" + teacher3Top2 +
                       "人 </p></td>");
            str.Append("  <td   valign=\"middle\"><p align=\"center\">" + teacher3Top3 + "人 </p></td>");
            str.Append("  <td   valign=\"middle\"><p align=\"center\">" + teacher3Top4 + "人 </p></td>");
            str.Append("  <td   valign=\"middle\"><p align=\"center\">" + teacher3Top5 + "人 </p></td>");
            str.Append("   </tr>");
            str.Append(" <tr   height=\"35\">");
            str.Append(
                "  <td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK12\" id=\"OLE_LINK12\"></a><a name=\"OLE_LINK11\" id=\"OLE_LINK11\">培训方式多样，生动有趣</a> </p></td>");
            //     str.Append("  <td   valign=\"middle\"><p align=\"center\">" + teacher4Top1 + "人 </p></td>");
            str.Append("   <td   align=\"center\" valign=\"middle\"><p align=\"center\">" + teacher4Top2 +
                       "人 </p></td>");
            str.Append(" <td   valign=\"middle\"><p align=\"center\">" + teacher4Top3 + "人 </p></td>");
            str.Append(" <td   valign=\"middle\"><p align=\"center\">" + teacher4Top4 + "人 </p></td>");
            str.Append(" <td  valign=\"middle\"><p align=\"center\">" + teacher4Top5 + "人 </p></td>");
            str.Append("  </tr>");
            str.Append(" <tr   height=\"35\">");
            str.Append(
                "  <td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK14\" id=\"OLE_LINK14\"></a><a name=\"OLE_LINK13\" id=\"OLE_LINK13\">与学员沟通和互动有效</a> </p></td>");
            //     str.Append("  <td   valign=\"middle\"><p align=\"center\">" + teacher5Top1 + "人 </p></td>");
            str.Append("  <td   align=\"center\" valign=\"middle\"><p align=\"center\">" + teacher5Top2 +
                       "人 </p></td>");
            str.Append("  <td   valign=\"middle\"><p align=\"center\">" + teacher5Top3 + "人 </p></td>");
            str.Append(" <td   valign=\"middle\"><p align=\"center\">" + teacher5Top4 + "人 </p></td>");
            str.Append("  <td   valign=\"middle\"><p align=\"center\">" + teacher5Top5 + "人 </p></td>");
            str.Append(" </tr>");
            str.Append("  <tr   height=\"35\">");
            str.Append(
                "  <td width=\"85\" rowspan=\"3\" valign=\"middle\"><p align=\"center\"><a name=\"OLE_LINK4\" id=\"OLE_LINK4\"></a><a name=\"OLE_LINK3\" id=\"OLE_LINK3\"><strong>培训组织和管理</strong></a><strong> </strong></p></td>");
            str.Append(
                " <td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK16\" id=\"OLE_LINK16\"></a><a name=\"OLE_LINK15\" id=\"OLE_LINK15\">培训服务周到细致</a> </p></td>");
            //     str.Append("  <td   valign=\"middle\"><p align=\"center\">" + org1Top1 + "人 </p></td>");
            str.Append("  <td   align=\"center\" valign=\"middle\"><p align=\"center\">" + org1Top2 +
                       "人 </p></td>");
            str.Append("  <td   valign=\"middle\"><p align=\"center\">" + org1Top3 + "人 </p></td>");
            str.Append("  <td   valign=\"middle\"><p align=\"center\">" + org1Top4 + "人 </p></td>");
            str.Append("  <td   valign=\"middle\"><p align=\"center\">" + org1Top5 + "人 </p></td>");
            str.Append("   </tr>");
            str.Append(" <tr   height=\"35\">");
            str.Append(
                "   <td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK18\" id=\"OLE_LINK18\"></a><a name=\"OLE_LINK17\" id=\"OLE_LINK17\">培训时间安排和控制合理</a> </p></td>");
            //     str.Append("  <td   valign=\"middle\"><p align=\"center\">" + org2Top1 + "人 </p></td>");
            str.Append("  <td   align=\"center\" valign=\"middle\"><p align=\"center\">" + org2Top2 +
                       "人 </p></td>");
            str.Append("  <td   valign=\"middle\"><p align=\"center\">" + org2Top3 + "人 </p></td>");
            str.Append("  <td   valign=\"middle\"><p align=\"center\">" + org2Top4 + "人 </p></td>");
            str.Append(" <td   valign=\"middle\"><p align=\"center\">" + org2Top5 + "人 </p></td>");
            str.Append(" </tr>");
            str.Append(" <tr   height=\"35\">");
            str.Append(
                "   <td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK20\" id=\"OLE_LINK20\"></a><a name=\"OLE_LINK19\" id=\"OLE_LINK19\">培训场所、设备安排到位</a> </p></td>");
            //      str.Append(" <td   valign=\"middle\"><p>" + org3Top1 + "人 </p></td>");
            str.Append("  <td   align=\"center\" valign=\"middle\"><p>" + org3Top2 + "人 </p></td>");
            str.Append("  <td   valign=\"middle\"  align=\"center\" ><p>" + org3Top3 + "人 </p></td>");
            str.Append(" <td   valign=\"middle\"  align=\"center\" ><p>" + org3Top4 + "人 </p></td>");
            str.Append("  <td   valign=\"middle\"  align=\"center\" ><p>" + org3Top5 + "人 </p></td>");
            str.Append("   </tr>");
            str.Append("  <tr   height=\"35\">");
            str.Append(
                "   <td width=\"85\" valign=\"middle\"  align=\"center\"><p><a name=\"_Hlk401413534\" id=\"_Hlk401413534\"><strong>测评单位 </strong></a></p></td>");
            str.Append("   <td width=\"217\" valign=\"middle\"  align=\"center\"><p>" + dept + " </p></td>");
            str.Append(
                "   <td width=\"85\" colspan=\"2\" align=\"center\" valign=\"middle\"><p><strong>测评时间 </strong></p></td>");
            str.Append("  <td width=\"227\" colspan=\"2\" align=\"center\"   valign=\"middle\"><p>" + time + "</p></td>");
            str.Append(" </tr>");
            str.Append("</table>"); 

            details.InnerHtml = str.ToString(); 
        }

    }
}