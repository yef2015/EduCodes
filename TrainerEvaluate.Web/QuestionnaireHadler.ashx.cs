using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using TrainerEvaluate.BLL;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// QuestionnaireHadler 的摘要说明
    /// </summary>
    public class QuestionnaireHadler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var opType = context.Request["t"];
            var id = context.Request["id"];
            var classid = context.Request["classId"];
            switch (opType)
            {
                case "n":
                    AddData(context);
                    break;
                case "e":
                    EditData(id, context);
                    break;
                case "d":
                    DelData(id, context);
                    break;
                case "q":
                    Query(context);
                    break;
                case "g":
                    //GetDataStudent(context);
                    GetDataClass(context);
                    break;
                case "f":
                    GetNofinishedStudent(context);
                    break;
                case "f2":
                    GetfinishedStudent(context);
                    break;
                case "s":
                    GetSuggestions(context);
                    break;
                case "r":
                    //  GetReportExcel(context, id);
                    ExportEvReportToxls(context, id, classid);
                    break;
                case "stu":
                    //  GetStudentsForcheck(context, id);
                    break;
                case "ex":
                    ExportStuInfo(context, id);
                    break;
                case "exs":
                    ExportSuggestionInfo(context, id);
                    break;

                case "stuno":
                    ExportStuNoFinish(context, id);
                    break;
                case "stuf2":
                    ExportStuFinish(context, id);
                    break;
                case "extotal":
                    ExportTotalReport(context);
                    break;
                case "excourse":
                    ExportCourseReport(context);
                    break;
                case "exteacher":
                    ExportTeacherReport(context);
                    break;
                case "exorg":
                    ExportOrgReport(context);
                    break;
                default:
                    var str = GetData(context);
                    context.Response.Write(str);
                    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        private string GetData(HttpContext context)
        {
            var ds = new DataSet();
            var questioninfo = new BLL.QuestionInfo();
            //ds = courBll.GetAllList(); 
            //var str = JsonConvert.SerializeObject(new {total = ds.Tables[0].Rows.Count, rows = ds.Tables[0]});

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "TeachTime" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "desc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = questioninfo.GetRecordCountNew("");
            ds = questioninfo.GetListByPageNew("",sort, startIndex, endIndex);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }



        private void ExportEvReportToxls(HttpContext context, string coursId, string classid)
        {
            var coid = new Guid(coursId);
            var couBll = new BLL.Course();
            var courseModel = couBll.GetModel(coid);
            var courseName = courseModel.CourseName;
            var exXls = new ExportXls();
            var filename = courseName + "-评估报告单.xls";
            exXls.ExportEvReportToxls(context.Response, filename, coid, classid);
        }




        private void ExportTotalReport(HttpContext context)
        {
            var exXls = new ExportXls();
            var fieldsNames = new List<string>();
            fieldsNames.Add("课程名称");
            fieldsNames.Add("授课教师");
            fieldsNames.Add("总平均分（满分52分）");
            fieldsNames.Add("总体满意度");
            fieldsNames.Add("评估等级");
            fieldsNames.Add("课程内容满意度");
            fieldsNames.Add("培训讲师满意度");
            fieldsNames.Add("培训组织和管理满意度");
            fieldsNames.Add("实评人数");
            fieldsNames.Add("培训时间");

            var quesBll = new BLL.Questionnaire();
            var dt = quesBll.GetTotalReport();
            dt.Columns.Remove("CourseId");
            dt.AcceptChanges();


            var filename = "课程评估总体情况统计表.xls";
            exXls.ExportTotalReportToxls(context.Response, fieldsNames, dt, filename);
        }



        private void ExportCourseReport(HttpContext context)
        {
            var exXls = new ExportXls();
            var fieldsNames = new List<string>();
            fieldsNames.Add("班级名称");
            fieldsNames.Add("课程名称");
            fieldsNames.Add("课程主题清晰明确");
            fieldsNames.Add("课程内容丰富、能吸引人");
            fieldsNames.Add("课程内容切合实际，能指导实践");
            fieldsNames.Add("课程内容重点突出，易于理解");
            fieldsNames.Add("课程内容有助于个人发展");
            var quesBll = new BLL.Questionnaire();
            var dt = quesBll.GetCourseReport();
            dt.Columns.Remove("CourseId");
            dt.Columns.Remove("CourseSubject");
            dt.Columns.Remove("CourseDevelop");
            dt.Columns.Remove("CourseKey");
            dt.Columns.Remove("CoursePractical");
            dt.Columns.Remove("CourseRich");
            dt.Columns.Remove("TotalDone");
            dt.Columns.Remove("TotalCourse");
            dt.AcceptChanges();


            var filename = "课程内容满意度分布表.xls";
            exXls.ExportCourseReportToxls(context.Response, fieldsNames, dt, filename);

        }

        private void ExportTeacherReport(HttpContext context)
        {
            var exXls = new ExportXls();
            var fieldsNames = new List<string>();
            fieldsNames.Add("课程名称");
            fieldsNames.Add("讲师准备比较充分");
            fieldsNames.Add("语言表达清晰，态度端正");
            fieldsNames.Add("仪表仪容端庄大方，有亲和力");
            fieldsNames.Add("培训方式多样，生动有趣");
            fieldsNames.Add("与学员沟通和互动有效");
            var quesBll = new BLL.Questionnaire();
            var dt = quesBll.GetTeacherReport();
            dt.Columns.Remove("CourseId");
            dt.Columns.Remove("TeacherName");
            dt.Columns.Remove("TeachTime");
            dt.Columns.Remove("TeacherBearing");
            dt.Columns.Remove("TeacherCommunication");
            dt.Columns.Remove("TeacherLanguage");
            dt.Columns.Remove("TeacherPrepare");
            dt.Columns.Remove("TeacherStyle");
            dt.Columns.Remove("TotalDone");
            dt.Columns.Remove("TotalTeacher");
            dt.AcceptChanges();


            var filename = "培训讲师各指标满意度分布表.xls";
            exXls.ExportTeacherReportToxls(context.Response, fieldsNames, dt, filename);
        }

        private void ExportOrgReport(HttpContext context)
        {
            var exXls = new ExportXls();
            var fieldsNames = new List<string>();
            fieldsNames.Add("课程名称");
            fieldsNames.Add("培训服务周到细致");
            fieldsNames.Add("培训时间安排和控制合理");
            fieldsNames.Add("培训场所、设备安排到位");
            var quesBll = new BLL.Questionnaire();
            var dt = quesBll.GetOrgReport();
            dt.Columns.Remove("CourseId");
            dt.Columns.Remove("OrgArrange");
            dt.Columns.Remove("OrgService");
            dt.Columns.Remove("OrgTime");
            dt.Columns.Remove("TotalDone");
            dt.Columns.Remove("TotalOrg");
            dt.AcceptChanges();


            var filename = "培训组织和管理满意度分布表.xls";
            exXls.ExportOrgReportToxls(context.Response, fieldsNames, dt, filename);
        }

        private void GetDataStudent(HttpContext context)
        {
            var ds = new DataSet();


            var courseId = context.Request["coId"];
            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var stuBll = new BLL.Student();
            var questionbll = new BLL.Questionnaire();
            var num = stuBll.GetRecordCount("");
            //  ds = questionbll.GetCourseStudentState(courseId);
            ds = questionbll.GetCourseStudentStateListByPage(courseId, "ck", startIndex, endIndex);




            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }


        private void GetDataClass(HttpContext context)
        {
            var ds = new DataSet();


            var courseId = context.Request["coId"];
            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var classBll = new BLL.Class();
            var questionbll = new BLL.Questionnaire();
            var num = classBll.GetRecordCount("");
            //  ds = questionbll.GetCourseStudentState(courseId);
            ds = questionbll.GetCourseClassStateListByPage(courseId, "ck", startIndex, endIndex);




            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }







        private void ExportStuInfo(HttpContext context, string id)
        {

            var exXls = new ExportXls();
            var fieldsNames = new List<string>();
            fieldsNames.Add("学员姓名");
            fieldsNames.Add("登录名");
            fieldsNames.Add("密码");
            fieldsNames.Add("所属学校");

            var quesBll = new BLL.Questionnaire();
            var dt = quesBll.GetStuInfoofCourse(id);

            var courseBll = new BLL.Course();
            var courseModel = courseBll.GetModel(new Guid(id));


            var filename = courseModel.CourseName + "--学员信息.xls";

            exXls.ExportToXls(context.Response, fieldsNames, dt, filename);


        }






        /// <summary>
        /// 导出意见
        /// </summary>
        private void ExportSuggestionInfo(HttpContext context, string id)
        {
            var exXls = new ExportXls();
            var fieldsNames = new List<string>();
            fieldsNames.Add("学员姓名");
            fieldsNames.Add("建议");
            var quesBll = new BLL.Questionnaire();
            var ds = quesBll.GetSuggestion(id);

            var courseBll = new BLL.Course();
            var courseModel = courseBll.GetModel(new Guid(id));


            var filename = courseModel.CourseName + "--学员建议.xls";

            if (ds != null && ds.Tables.Count > 0)
            {
                exXls.ExportToXls(context.Response, fieldsNames, ds.Tables[0], filename);
            }
        }


        private void ExportStuNoFinish(HttpContext context, string id)
        {
            var exXls = new ExportXls();
            var fieldsNames = new List<string>();
            fieldsNames.Add("学员姓名");
            fieldsNames.Add("所在学校");
            fieldsNames.Add("联系电话");
            var quesBll = new BLL.Questionnaire();
            var ds = quesBll.GetNofinishedStu(id);

            var courseBll = new BLL.Course();
            var courseModel = courseBll.GetModel(new Guid(id));


            var filename = courseModel.CourseName + "--未完成学员.xls";

            if (ds != null && ds.Tables.Count > 0)
            {
                exXls.ExportToXls(context.Response, fieldsNames, ds.Tables[0], filename);
            }
        }


        private void ExportStuFinish(HttpContext context, string id)
        {
            var exXls = new ExportXls();
            var fieldsNames = new List<string>();
            fieldsNames.Add("学员姓名");
            fieldsNames.Add("所在学校");
            fieldsNames.Add("联系电话");
            var quesBll = new BLL.Questionnaire();
            var ds = quesBll.GetfinishedStu(id);

            var courseBll = new BLL.Course();
            var courseModel = courseBll.GetModel(new Guid(id));


            var filename = courseModel.CourseName + "--已完成学员.xls";

            if (ds != null && ds.Tables.Count > 0)
            {
                exXls.ExportToXls(context.Response, fieldsNames, ds.Tables[0], filename);
            }
        }







        private void GetNofinishedStudent(HttpContext context)
        {
            var ds = new DataSet();
            var courBll = new BLL.Course();
            ds = courBll.GetAllList();

            var courseId = context.Request["coId"];
            var classId = context.Request["classId"];
            if (!string.IsNullOrEmpty(courseId))
            {
                var page = Convert.ToInt32(context.Request["page"]);
                var rows = Convert.ToInt32(context.Request["rows"]);
                var startIndex = (page - 1) * rows + 1;
                var endIndex = startIndex + rows - 1;

                var questionbll = new BLL.Questionnaire();
                ds = questionbll.GetNofinishedStuListByPage(courseId,classId, "School", startIndex, endIndex);
                var num = questionbll.GetNofinishedStuNum(courseId);
                var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
                context.Response.Write(str);
            }
        }

        private void GetfinishedStudent(HttpContext context)
        {
            var ds = new DataSet();
            var courBll = new BLL.Course();
            ds = courBll.GetAllList();

            var courseId = context.Request["coId"];
            var classId = context.Request["classId"];
            if (!string.IsNullOrEmpty(courseId))
            {
                var page = Convert.ToInt32(context.Request["page"]);
                var rows = Convert.ToInt32(context.Request["rows"]);
                var startIndex = (page - 1) * rows + 1;
                var endIndex = startIndex + rows - 1;

                var questionbll = new BLL.Questionnaire();
                ds = questionbll.GetfinishedStuListByPage(courseId,classId, "School", startIndex, endIndex);
                var num = questionbll.GetfinishedStuNum(courseId);
                var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
                context.Response.Write(str);
            }
        }




        /// <summary>
        /// 提取意见列表
        /// </summary>
        /// <param name="context"></param>
        private void GetSuggestions(HttpContext context)
        {
            var ds = new DataSet();
            var courBll = new BLL.Course();
            ds = courBll.GetAllList();

            var courseId = context.Request["coId"];

            if (!string.IsNullOrEmpty(courseId))
            {
                var page = Convert.ToInt32(context.Request["page"]);
                var rows = Convert.ToInt32(context.Request["rows"]);
                var startIndex = (page - 1) * rows + 1;
                var endIndex = startIndex + rows - 1;

                var questionbll = new BLL.Questionnaire();
                var num = questionbll.GetSuggestion(courseId) == null ? 0 : questionbll.GetSuggestion(courseId).Tables[0].Rows.Count;
                ds = questionbll.GetSuggestionByPage(courseId, "", startIndex, endIndex);
                var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
                context.Response.Write(str);
            }
        }



        private void GetReportDoc(HttpContext context, string coursId, string classid)
        {
            var courseName = "";
            var strWord = GetReportData(coursId, out courseName, classid);
            context.Response.ContentEncoding = System.Text.Encoding.UTF7;
            context.Response.ClearContent();
            context.Response.ClearHeaders();
            context.Response.AddHeader("content-disposition", "attachment;filename=report.doc"); //必须的
            context.Response.AddHeader("Content-type", "application");
            context.Response.ContentType = "application/ms-html";
            context.Response.ContentEncoding = System.Text.Encoding.Default; //如果不行改为utf7，默认一般可以，处理头部乱码的问题
            context.Response.Write(strWord);
            context.Response.Flush();
            context.Response.Close();
        }


        private void GetReportExcel(HttpContext context, string coursId, string classid)
        {
            var courseName = "";
            var strWord = GetReportData(coursId, out courseName, classid);
            context.Response.ContentEncoding = System.Text.Encoding.UTF7;
            context.Response.ClearContent();
            context.Response.ClearHeaders();
            context.Response.AddHeader("content-disposition", "attachment;filename=" + courseName + "-评估报告单.xls"); //必须的
            context.Response.AddHeader("Content-type", "application");
            context.Response.ContentType = "application/ms-html";
            context.Response.ContentEncoding = System.Text.Encoding.Default; //如果不行改为utf7，默认一般可以，处理头部乱码的问题
            context.Response.Write(strWord);
            context.Response.Flush();
            context.Response.Close();
        }



        private string GetReportData(string coursId, out string courseName, string classid)
        {
            var coid = new Guid(coursId);
            var couBll = new BLL.Course();
            var courseModel = couBll.GetModel(coid);
            courseName = courseModel.CourseName;
            var report = new BLL.Questionnaire();
            var reportTitle = report.GetReportTile(coid);
            var reportBody = report.GetReport(coid, classid);


            var totalShould = 0;
            var totalPrac = 0;
            var totalScor = "";
            var satisfy = "";
            var level = "良好";
            var contenScor = "";
            var techScor = "";
            var orgScor = "";

            if (reportTitle != null && reportTitle.Tables.Count > 0)
            {
                totalShould = Convert.ToInt32(reportTitle.Tables[0].Rows[0]["totalNum"]);
                totalPrac = Convert.ToInt32(reportTitle.Tables[0].Rows[0]["totalDone"]);
                totalScor = string.Format("{0:N2}", Convert.ToDouble(reportTitle.Tables[0].Rows[0]["totalAvg"]));
                satisfy = string.Format("{0:N2}", Convert.ToDouble(reportTitle.Tables[0].Rows[0]["Satisfy"]) * 100);
                level = report.GetLevel(Convert.ToDouble(reportTitle.Tables[0].Rows[0]["Satisfy"]));

                contenScor = string.Format("{0:N2}", Convert.ToDouble(reportTitle.Tables[0].Rows[0]["CourseAvg"]));
                techScor = string.Format("{0:N2}", Convert.ToDouble(reportTitle.Tables[0].Rows[0]["TeacherAvg"]));
                orgScor = string.Format("{0:N2}", Convert.ToDouble(reportTitle.Tables[0].Rows[0]["OrgAvg"]));
            }


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
            str.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            str.Append("<head>");
            str.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\" />");
            str.Append("</head>");
            str.Append("<body>");
            str.Append("<p align=\"center\" style=\"font-size: 25px;font-weight: bold\"><strong>");
            //str.Append(DateTime.Now.Year+ "年海淀区在职资格培训班培训效果测评表 ");
            str.Append(DateTime.Now.Year + "年中青年干部教育管理培训班课程评估表 ");
            str.Append("</strong><br /></p>");
            str.Append("<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\" width=\"614\" bordercolor=\"#000000\" bgcolor=\"#FFFFFF\"  style=\"border-collapse:collapse;font-size: 14px;\" >");
            str.Append("<tr  height=\"35\">");
            str.Append("<td width=\"85\" align=\"center\" valign=\"middle\"><p><strong>课程名称 </strong></p></td>");
            str.Append("<td width=\"217\" align=\"center\" valign=\"middle\"><p>" + courseModel.CourseName + " </p></td>");
            str.Append("<td width=\"78\" colspan=\"1\" align=\"center\"  valign=\"middle\"><p><strong>培训地点 </strong></p></td>");
            str.Append("<td width=\"234\" colspan=\"3\" align=\"center\"  valign=\"middle\"><p>" + courseModel.TeachPlace + " </p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append("<td width=\"85\" align=\"center\"  valign=\"middle\"><p><strong>培训讲师</strong> </p></td>");
            str.Append("<td width=\"217\" align=\"center\"  valign=\"middle\"><p>" + courseModel.TeacherName + " </p></td>");
            str.Append("<td width=\"78\" colspan=\"1\" align=\"center\"  valign=\"middle\"><p><strong>培训时间 </strong></p></td>");
            str.Append("<td width=\"234\" colspan=\"3\" align=\"center\"  valign=\"middle\"><p>" + courseModel.TeachTime + "</p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append("<td width=\"85\" align=\"center\"  valign=\"middle\"><p><strong>应评人数 </strong></p></td>");
            str.Append("<td width=\"217\" align=\"center\" valign=\"middle\"><p>" + totalShould + "</p></td>");
            str.Append("<td width=\"78\" colspan=\"1\" align=\"center\"  valign=\"middle\"><p><strong>实评人数 </strong></p></td>");
            str.Append("<td width=\"234\" colspan=\"3\" align=\"center\"  valign=\"middle\"><p>" + totalPrac + "</p></td>");
            str.Append("</tr>");
            str.Append("<tr>");
            str.Append("<td width=\"614\" colspan=\"6\" valign=\"left\">");
            str.Append("<p align=\"center\">");
            str.Append("<strong>");
            str.Append(" <br />");
            str.Append("</strong><strong>本次培训总体平均分：" + totalScor + "分(满分52)<br />满意度：" + satisfy + "% <br />等级：" + level + "， </strong><br />");
            str.Append("<strong>其中课程内容</strong><strong>" + contenScor + "</strong><strong>分<br />培训讲师</strong><strong>" + techScor + "</strong><strong>分<br />培训组织和管理</strong><strong>" + orgScor + "</strong><strong>分</strong>");
            str.Append("</p>");
            str.Append("</td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append("<td width=\"302\" colspan=\"2\" valign=\"middle\" align=\"center\"><p align=\"center\"><strong>培训满意度评价项目</strong><strong> </strong></p></td>");
            //   str.Append("<td width=\"78\" valign=\"middle\" align=\"center\"><p>很满意 </p></td>");
            str.Append("<td width=\"78\"    valign=\"middle\" align=\"center\"><p>很满意 </p></td>");
            str.Append("<td width=\"78\" valign=\"middle\" align=\"center\"><p>满意</p></td>");
            str.Append("<td width=\"78\" valign=\"middle\" align=\"center\"><p>一般</p></td>");
            str.Append("<td width=\"78\"  valign=\"middle\" align=\"center\"><p>不满意</p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append("<td width=\"302\" colspan=\"2\" valign=\"middle\" ><p align=\"center\"><strong>本次课程总体满意度 </strong></p></td>");
            //    str.Append("<td width=\"78\" valign=\"middle\"><p><strong>"+allTop1+"</strong></p></td>");
            str.Append("<td width=\"78\" align=\"center\" valign=\"middle\"><p><strong>" + allTop2 + "</strong></p></td>");
            str.Append("<td width=\"78\" align=\"center\" valign=\"middle\"><p><strong>" + allTop3 + "</strong></p></td>");
            str.Append("<td width=\"78\" align=\"center\" valign=\"middle\"><p><strong>" + allTop4 + "</strong></p></td>");
            str.Append("<td width=\"78\" align=\"center\" valign=\"middle\"><p><strong>" + allTop5 + "</strong></p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append("<td width=\"85\" rowspan=\"5\" valign=\"middle\"><p align=\"center\"><strong>课程内容</strong><strong> </strong></p></td>");
            str.Append("<td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\">课程主题清晰明确 </p></td>");
            //     str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + content1Top1 + "人</p></td>");
            str.Append("<td width=\"78\" align=\"center\" valign=\"middle\"><p align=\"center\">" + content1Top2 + "人 </p></td>");
            str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + content1Top3 + "人 </p></td>");
            str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + content1Top4 + "人 </p></td>");
            str.Append("<td  width=\"78\" valign=\"middle\"><p align=\"center\">" + content1Top5 + "人 </p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append("<td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\">课程内容丰富、能吸引人 </p></td>");
            //    str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + content2Top1 + "人 </p></td>");
            str.Append("<td width=\"78\" align=\"center\" valign=\"middle\"><p align=\"center\">" + content2Top2 + "人 </p></td>");
            str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + content2Top3 + "人 </p></td>");
            str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + content2Top4 + "人 </p></td>");
            str.Append("<td  width=\"78\"  valign=\"middle\"><p align=\"center\">" + content2Top5 + "人 </p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append("<td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\">课程内容切合实际，能指导实践 </p></td>");
            //    str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + content3Top1 + "人 </p></td>");
            str.Append("<td width=\"78\" align=\"center\" valign=\"middle\"><p align=\"center\">" + content3Top2 + "人 </p></td>");
            str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + content3Top3 + "人 </p></td>");
            str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + content3Top4 + "人 </p></td>");
            str.Append("<td width=\"78\"   valign=\"middle\"><p align=\"center\">" + content3Top5 + "人 </p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append("<td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\">课程内容重点突出，易于理解 </p></td>");
            //     str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + content4Top1 + "人 </p></td>");
            str.Append("<td width=\"78\" align=\"center\" valign=\"middle\"><p align=\"center\">" + content4Top2 + "人 </p></td>");
            str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + content4Top3 + "人 </p></td>");
            str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + content4Top4 + "人 </p></td>");
            str.Append("<td width=\"78\"   valign=\"middle\"><p align=\"center\">" + content4Top5 + "人 </p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append("<td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\">课程内容有助于个人发展 </p></td>");
            //   str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + content5Top1 + "人 </p></td>");
            str.Append("<td width=\"78\" align=\"center\" valign=\"middle\"><p align=\"center\">" + content5Top2 + "人 </p></td>");
            str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + content5Top3 + "人 </p></td>");
            str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + content5Top4 + "人 </p></td>");
            str.Append("<td width=\"78\"  valign=\"middle\"><p align=\"center\">" + content5Top5 + "人 </p></td>");
            str.Append("</tr>");
            str.Append("<tr   height=\"35\">");
            str.Append("<td width=\"85\" rowspan=\"5\" valign=\"middle\"><p align=\"center\"><a name=\"OLE_LINK2\" id=\"OLE_LINK2\"></a><a name=\"OLE_LINK1\" id=\"OLE_LINK1\"><strong>培训讲师</strong></a><strong> </strong></p></td>");
            str.Append("<td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK6\" id=\"OLE_LINK6\"></a><a name=\"OLE_LINK5\" id=\"OLE_LINK5\">讲师准备比较充分</a> </p></td>");


            //     str.Append("<td width=\"78\" valign=\"middle\"><p align=\"center\">" + teacher1Top1 + "人 </p></td>");
            str.Append(" <td width=\"78\" align=\"center\" valign=\"middle\"><p align=\"center\">" + teacher1Top2 + "人 </p></td>");
            str.Append(" <td width=\"78\" valign=\"middle\"><p align=\"center\">" + teacher1Top3 + "人 </p></td>");
            str.Append(" <td width=\"78\" valign=\"middle\"><p align=\"center\">" + teacher1Top4 + "人 </p></td>");
            str.Append(" <td width=\"78\"  valign=\"middle\"><p align=\"center\">" + teacher1Top5 + "人 </p></td>");

            str.Append(" </tr>");
            str.Append("  <tr   height=\"35\">");
            str.Append("   <td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK8\" id=\"OLE_LINK8\"></a><a name=\"OLE_LINK7\" id=\"OLE_LINK7\">语言表达清晰，态度端正</a> </p></td>");
            //     str.Append("  <td width=\"78\" valign=\"middle\"><p align=\"center\">" + teacher2Top1 + "人 </p></td>");
            str.Append(" <td width=\"78\" align=\"center\" valign=\"middle\"><p align=\"center\">" + teacher2Top2 + "人 </p></td>");
            str.Append(" <td width=\"78\" valign=\"middle\"><p align=\"center\">" + teacher2Top3 + "人 </p></td>");
            str.Append("  <td width=\"78\" valign=\"middle\"><p align=\"center\">" + teacher2Top4 + "人 </p></td>");
            str.Append("  <td  width=\"78\"  valign=\"middle\"><p align=\"center\">" + teacher2Top5 + "人 </p></td>");
            str.Append("   </tr>");
            str.Append(" <tr   height=\"35\">");
            str.Append("    <td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK10\" id=\"OLE_LINK10\"></a><a name=\"OLE_LINK9\" id=\"OLE_LINK9\">仪表仪容端庄大方，有亲和力</a> </p></td>");
            //     str.Append("    <td width=\"78\" valign=\"middle\"><p align=\"center\">" + teacher3Top1 + "人 </p></td>");
            str.Append("   <td width=\"78\" align=\"center\" valign=\"middle\"><p align=\"center\">" + teacher3Top2 + "人 </p></td>");
            str.Append("  <td width=\"78\" valign=\"middle\"><p align=\"center\">" + teacher3Top3 + "人 </p></td>");
            str.Append("  <td width=\"78\" valign=\"middle\"><p align=\"center\">" + teacher3Top4 + "人 </p></td>");
            str.Append("  <td  width=\"78\"  valign=\"middle\"><p align=\"center\">" + teacher3Top5 + "人 </p></td>");
            str.Append("   </tr>");
            str.Append(" <tr   height=\"35\">");
            str.Append("  <td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK12\" id=\"OLE_LINK12\"></a><a name=\"OLE_LINK11\" id=\"OLE_LINK11\">培训方式多样，生动有趣</a> </p></td>");
            //     str.Append("  <td width=\"78\" valign=\"middle\"><p align=\"center\">" + teacher4Top1 + "人 </p></td>");
            str.Append("   <td width=\"78\" align=\"center\" valign=\"middle\"><p align=\"center\">" + teacher4Top2 + "人 </p></td>");
            str.Append(" <td width=\"78\" valign=\"middle\"><p align=\"center\">" + teacher4Top3 + "人 </p></td>");
            str.Append(" <td width=\"78\" valign=\"middle\"><p align=\"center\">" + teacher4Top4 + "人 </p></td>");
            str.Append(" <td width=\"78\"  valign=\"middle\"><p align=\"center\">" + teacher4Top5 + "人 </p></td>");
            str.Append("  </tr>");
            str.Append(" <tr   height=\"35\">");
            str.Append("  <td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK14\" id=\"OLE_LINK14\"></a><a name=\"OLE_LINK13\" id=\"OLE_LINK13\">与学员沟通和互动有效</a> </p></td>");
            //     str.Append("  <td width=\"78\" valign=\"middle\"><p align=\"center\">" + teacher5Top1 + "人 </p></td>");
            str.Append("  <td width=\"78\" align=\"center\" valign=\"middle\"><p align=\"center\">" + teacher5Top2 + "人 </p></td>");
            str.Append("  <td width=\"78\" valign=\"middle\"><p align=\"center\">" + teacher5Top3 + "人 </p></td>");
            str.Append(" <td width=\"78\" valign=\"middle\"><p align=\"center\">" + teacher5Top4 + "人 </p></td>");
            str.Append("  <td width=\"78\"   valign=\"middle\"><p align=\"center\">" + teacher5Top5 + "人 </p></td>");
            str.Append(" </tr>");
            str.Append("  <tr   height=\"35\">");
            str.Append("  <td width=\"85\" rowspan=\"3\" valign=\"middle\"><p align=\"center\"><a name=\"OLE_LINK4\" id=\"OLE_LINK4\"></a><a name=\"OLE_LINK3\" id=\"OLE_LINK3\"><strong>培训组织和管理</strong></a><strong> </strong></p></td>");
            str.Append(" <td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK16\" id=\"OLE_LINK16\"></a><a name=\"OLE_LINK15\" id=\"OLE_LINK15\">培训服务周到细致</a> </p></td>");
            //     str.Append("  <td width=\"78\" valign=\"middle\"><p align=\"center\">" + org1Top1 + "人 </p></td>");
            str.Append("  <td width=\"78\" align=\"center\" valign=\"middle\"><p align=\"center\">" + org1Top2 + "人 </p></td>");
            str.Append("  <td width=\"78\" valign=\"middle\"><p align=\"center\">" + org1Top3 + "人 </p></td>");
            str.Append("  <td width=\"78\" valign=\"middle\"><p align=\"center\">" + org1Top4 + "人 </p></td>");
            str.Append("  <td width=\"78\"   valign=\"middle\"><p align=\"center\">" + org1Top5 + "人 </p></td>");
            str.Append("   </tr>");
            str.Append(" <tr   height=\"35\">");
            str.Append("   <td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK18\" id=\"OLE_LINK18\"></a><a name=\"OLE_LINK17\" id=\"OLE_LINK17\">培训时间安排和控制合理</a> </p></td>");
            //     str.Append("  <td width=\"78\" valign=\"middle\"><p align=\"center\">" + org2Top1 + "人 </p></td>");
            str.Append("  <td width=\"78\" align=\"center\" valign=\"middle\"><p align=\"center\">" + org2Top2 + "人 </p></td>");
            str.Append("  <td width=\"78\" valign=\"middle\"><p align=\"center\">" + org2Top3 + "人 </p></td>");
            str.Append("  <td width=\"78\" valign=\"middle\"><p align=\"center\">" + org2Top4 + "人 </p></td>");
            str.Append(" <td  width=\"78\"  valign=\"middle\"><p align=\"center\">" + org2Top5 + "人 </p></td>");
            str.Append(" </tr>");
            str.Append(" <tr   height=\"35\">");
            str.Append("   <td width=\"217\" valign=\"middle\"><p style=\"margin-left: 3px;\"><a name=\"OLE_LINK20\" id=\"OLE_LINK20\"></a><a name=\"OLE_LINK19\" id=\"OLE_LINK19\">培训场所、设备安排到位</a> </p></td>");
            //      str.Append(" <td width=\"78\" valign=\"middle\"><p>" + org3Top1 + "人 </p></td>");
            str.Append("  <td width=\"78\" align=\"center\" valign=\"middle\"><p>" + org3Top2 + "人 </p></td>");
            str.Append("  <td width=\"78\" valign=\"middle\"  align=\"center\" ><p>" + org3Top3 + "人 </p></td>");
            str.Append(" <td width=\"78\" valign=\"middle\"  align=\"center\" ><p>" + org3Top4 + "人 </p></td>");
            str.Append("  <td  width=\"78\"  valign=\"middle\"  align=\"center\" ><p>" + org3Top5 + "人 </p></td>");
            str.Append("   </tr>");
            str.Append("  <tr   height=\"35\">");
            str.Append("   <td width=\"85\" valign=\"middle\"  align=\"center\"><p><a name=\"_Hlk401413534\" id=\"_Hlk401413534\"><strong>测评单位 </strong></a></p></td>");
            str.Append("   <td width=\"217\" valign=\"middle\"  align=\"center\"><p>" + dept + " </p></td>");
            str.Append("   <td width=\"78\" colspan=\"1\" align=\"center\" valign=\"middle\"><p><strong>测评时间 </strong></p></td>");
            str.Append("  <td width=\"234\" colspan=\"3\" align=\"center\"   valign=\"middle\"><p>" + time + "</p></td>");
            str.Append(" </tr>");
            str.Append("</table>");
            str.Append("</body>");
            str.Append("</html>");

            return str.ToString();
        }





        private void Query(HttpContext context)
        {
            var courseName = context.Request["cname"];
            var teacher = context.Request["cTeacher"];
            var time = context.Request["cTime"];
            var place = context.Request["cPlace"];

            var ds = new DataSet();
            var courBll = new BLL.Course();
            var strWhere = "";
            if (!string.IsNullOrEmpty(courseName))
            {
                strWhere = string.Format(" CourseName like '%" + courseName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(teacher))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  TeacherName  like '%" + teacher.Trim() + "%' ");
                }
                else
                {
                    strWhere = string.Format(" TeacherName  like '%" + teacher.Trim() + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(time))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  TeachTime  like '%" + time.Trim() + "%' ");
                }
                else
                {
                    strWhere = string.Format(" TeachTime  like '%" + time.Trim() + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(place))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  TeachPlace  like '%" + place.Trim() + "%' ");
                }
                else
                {
                    strWhere = string.Format(" TeachPlace  like '%" + place.Trim() + "%' ");
                }
            }

            //ds = courBll.GetList(strWhere);
            //var str = JsonConvert.SerializeObject(new {total = ds.Tables[0].Rows.Count, rows = ds.Tables[0]});

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "TeachTime" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = courBll.GetRecordCount(strWhere);
            ds = courBll.GetListByPage(strWhere, sort, startIndex, endIndex, order);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }




        private void AddData(HttpContext context)
        {
            var msg = "";
            var courseId = context.Request["CourseId"];
            var isAll = context.Request["IsAll"];
            if (!string.IsNullOrEmpty(isAll)) //全选
            {
                if (isAll == "1")
                {
                    var questionBll = new BLL.Questionnaire();
                    var result = questionBll.SaveQuestionsForAll(courseId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                else //全不选
                {
                    var questionBll = new BLL.Questionnaire();
                    var result = questionBll.DeletCourseStudentbyCourseId(courseId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
            }
            else
            {
                var stuids = context.Request["StuIds"];
                var unstuids = context.Request["UnStuIds"];
                if (!string.IsNullOrEmpty(stuids))
                {
                    if (!string.IsNullOrEmpty(unstuids))
                    {
                        var unid = unstuids.Split('|');
                        if (unid.Length > 0)
                        {
                            foreach (var uid in unid)
                            {
                                if (!string.IsNullOrEmpty(uid))
                                {
                                    stuids = stuids.Replace(uid, "");
                                }
                            }
                        }
                    }
                    var stu = stuids.Split('|');
                    var stuList = new List<string>();
                    foreach (var sid in stu)
                    {
                        if (!string.IsNullOrEmpty(sid))
                        {
                            if (!stuList.Contains(sid))
                            {
                                stuList.Add(sid);
                            }
                        }
                    }

                    var questionBll = new BLL.Questionnaire();
                    var result = questionBll.SaveQuestions(stuList.ToArray(), courseId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                else
                {
                    var questionBll = new BLL.Questionnaire();
                    var result = questionBll.SaveQuestions(null, courseId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
            }
            context.Response.Write(msg);
        }




        private void EditData(string id, HttpContext context)
        {
            var stuids = context.Request["StuIds"];
            var courseId = context.Request["CourseId"];
            var msg = "";
            if (!string.IsNullOrEmpty(stuids))
            {
                var stu = stuids.Split('|');
                var questionBll = new BLL.Questionnaire();
                var result = questionBll.EditQuestions(stu, courseId);
                if (!result)
                {
                    msg = "保存失败！";
                }
            }
            //  var str = JsonConvert.SerializeObject(new { success = result, errorMsg = msg});
            context.Response.Write(msg);
        }





        private void DelData(string id, HttpContext context)
        {
            var queBll = new BLL.Questionnaire();
            var result = false;
            var msg = "";
            try
            {
                result = queBll.DeletCourseStudentbyCourseId(id);

                if (!result)
                {
                    msg = "保存失败！";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                result = false;
                msg = ex.Message;

            }
            //  var str = JsonConvert.SerializeObject(new { success = result, errorMsg = msg});
            context.Response.Write(msg);
        }


        /// <summary>
        /// 生成的报表
        /// </summary>
        private void GetDataReport(HttpContext context)
        {
            var msg = "";
            context.Response.Write(msg);
        }
    }
}