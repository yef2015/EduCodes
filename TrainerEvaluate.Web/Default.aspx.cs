using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.Web
{
    public partial class Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var currentUser = Profile.CurrentUser;
                if ((EnumUserRole) currentUser.UserRole == EnumUserRole.Admin)
                {
                    userInfo.InnerText = "欢迎你，" + currentUser.UserName;
                    qustionManage.InnerText = "问卷管理";
                }
                else if ((EnumUserRole)currentUser.UserRole == EnumUserRole.Student)
                {
                    userInfo.InnerText = "欢迎你，" + currentUser.UserName;
                    ifrcont.Attributes["src"] = "MyQuestionnaireNew.aspx";
                    qustionManage.InnerText = "问卷调查";
                }
                else if ((EnumUserRole)currentUser.UserRole == EnumUserRole.Teacher)
                {
                    userInfo.InnerText = "欢迎你，" + currentUser.UserName;
                }
                else
                {
                    userInfo.InnerText = "欢迎你，" + currentUser.UserName;
                }

                SetUserRoleInfo();
            }
        }






        private void SetUserRoleInfo()
        {
            manage.Visible = false;
            teacherManage.Visible = false;
            analysis.Visible = false;
            studentManage.Visible = false;
            courseManage.Visible = false;
            qustionManage.Visible = false;
            personalInfo.Visible = true;
            //classManage.Visible = false;
            stuQue.Visible = false;
            schooldistrict.Visible = false;
            schoolManage.Visible = false;
            classTreeManage.Visible = false;
            stuQueResult.Visible = false;   // 评估结果
            personArchive.Visible = false;  // 个人档案
            trainExper.Visible = false;     // 培训经历
            teacherArchive.Visible = false; // 教师的个人档案
            professExper.Visible = false;   // 任教经历
            netForAdmin.Visible = false;    // 网上报名：管理员
            netForStudent.Visible = false;  // 网上报名：学生

            if (Profile.CurrentUser.UserRole==1)
            {  
                stuQue.Visible = true;
                ifrcont.Attributes["src"] = "NetForStudent.aspx";
                personArchive.Visible = true;
                trainExper.Visible = true;
                stuQueResult.Visible = true;
                netForStudent.Visible = true;
            }
            else if (Profile.CurrentUser.UserRole == 2)
            {
                ifrcont.Attributes["src"] = "MyTeacherArchive.aspx";
                teacherArchive.Visible = true;
                professExper.Visible = true;
            }
            else
            {
                var roleBll = new BLL.Roles();
                var dt = roleBll.GetCurrentUserRoleInfo(Profile.CurrentUser.UserId);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["FuncCode"].ToString() == "manage")
                        {
                            manage.Visible = true;
                        }
                        if (row["FuncCode"].ToString() == "teacherManage")
                        {
                            teacherManage.Visible = true;
                        }
                        if (row["FuncCode"].ToString() == "analysis")
                        {
                            analysis.Visible = true;
                        }
                        //if (row["FuncCode"].ToString() == "classManage")
                        //{
                        //    classManage.Visible = true;
                        //}
                        if (row["FuncCode"].ToString() == "studentManage")
                        {
                            studentManage.Visible = true;
                        }
                        if (row["FuncCode"].ToString() == "courseManage")
                        {
                            courseManage.Visible = true;
                        }
                        if (row["FuncCode"].ToString() == "qustionManage")
                        {
                            qustionManage.Visible = true;
                        }
                        if (row["FuncCode"].ToString() == "schooldistrict")
                        {
                            schooldistrict.Visible = true;
                        }
                        if (row["FuncCode"].ToString() == "schoolManage")
                        {
                            schoolManage.Visible = true;
                        }
                        if (row["FuncCode"].ToString() == "classTreeManage")
                        {
                            classTreeManage.Visible = true;
                        }
                        if (row["FuncCode"].ToString() == "netForAdmin")
                        {
                            netForAdmin.Visible = true;
                        }
                    }

                    if (dt.Rows.Count > 0)
                    {

                        switch (dt.Rows[0]["FuncCode"].ToString())
                        {
                            case "manage":
                                ifrcont.Attributes["src"] = "Manage.aspx";
                                manage.Style["color"] = "#000000";
                                manage.Style["font-weight"] = "bold";
                                break;
                            case "teacherManage":
                                ifrcont.Attributes["src"] = "TeacherManage.aspx";
                                teacherManage.Style["color"] = "#000000";
                                teacherManage.Style["font-weight"] = "bold";
                                break;
                            case "analysis":
                                ifrcont.Attributes["src"] = "Analysis.aspx";
                                analysis.Style["color"] = "#000000";
                                analysis.Style["font-weight"] = "bold";
                                break;
                            //case "classManage":
                            //    ifrcont.Attributes["src"] = "ClassManage.aspx";
                            //    classManage.Style["color"] = "#000000";
                            //    classManage.Style["font-weight"] = "bold";
                            //    break;
                            case "studentManage":
                                //ifrcont.Attributes["src"] = "StuManage.aspx";
                                ifrcont.Attributes["src"] = "StuFrame.aspx";
                                studentManage.Style["color"] = "#000000";
                                studentManage.Style["font-weight"] = "bold";
                                break;
                            case "courseManage":
                                ifrcont.Attributes["src"] = "CourseManage.aspx";
                                courseManage.Style["color"] = "#000000";
                                courseManage.Style["font-weight"] = "bold";
                                break;
                            case "qustionManage":
                                ifrcont.Attributes["src"] = "QuestionnaireManage.aspx";
                                qustionManage.Style["color"] = "#000000";
                                qustionManage.Style["font-weight"] = "bold";
                                break;
                            case "schooldistrict":
                                ifrcont.Attributes["src"] = "SPSchoolDistrict.aspx";
                                qustionManage.Style["color"] = "#000000";
                                qustionManage.Style["font-weight"] = "bold";
                                break;
                            case "schoolManage":
                                ifrcont.Attributes["src"] = "SPSchoolManage.aspx";
                                qustionManage.Style["color"] = "#000000";
                                qustionManage.Style["font-weight"] = "bold";
                                break;
                            case "classTreeManage":
                                ifrcont.Attributes["src"] = "ClassTreeManage.aspx";
                                qustionManage.Style["color"] = "#000000";
                                qustionManage.Style["font-weight"] = "bold";
                                break;
                            case "netForAdmin":
                                ifrcont.Attributes["src"] = "NetForAdmin.aspx";
                                qustionManage.Style["color"] = "#000000";
                                qustionManage.Style["font-weight"] = "bold";
                                break;
                            default:
                                ifrcont.Attributes["src"] = "PersonalInfo.aspx";
                                personalInfo.Style["color"] = "#000000";
                                personalInfo.Style["font-weight"] = "bold";
                                break;
                        }
                    }
                    else
                    {
                        ifrcont.Attributes["src"] = "PersonalInfo.aspx";
                        personalInfo.Style["color"] = "#000000";
                        personalInfo.Style["font-weight"] = "bold";
                    }
                }
            } 
        }
   







    private void JudgeAuth(Models.SysUser currentUser)
        {
            if (currentUser != null)
            {
                switch ((EnumUserRole)currentUser.UserRole)
                {
                    case EnumUserRole.Admin:
                       // logoutStu.Visible = false;
                        stuQue.Visible = false;
                        break;
                    case EnumUserRole.Student:
                        manage.Visible = false;
                        teacherManage.Visible = false;
                        analysis.Visible = false;
                        studentManage.Visible = false;
                        courseManage.Visible = false;
                        qustionManage.Visible = false;
                        personalInfo.Visible = false;
                        logoutSys.Visible = false;
                        //classManage.Visible = false;
                        schooldistrict.Visible = false;
                        schoolManage.Visible = false;
                        classTreeManage.Visible = false;
                        stuQue.Visible = true;
                        break;
                    case EnumUserRole.Teacher:
                        manage.Visible = false;
                        studentManage.Visible = false;
                        analysis.Visible = false;
                      //  logoutStu.Visible = false;
                        stuQue.Visible = false;
                        break;
                    default:
                        break;
                }
            }

        }

    }
}