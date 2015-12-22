<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TrainerEvaluate.Web.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>中小学干部培训评估系统</title>
    <link rel="stylesheet" href="Styles/pgxt.css" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript">

        //$(document).ready(function() { 
        //    $("#analysis").css("color", "#000000");
        //    $("#analysis").css("font-weight", "bold");

        //    $("#stuQue").css("color", "#000000");
        //    $("#stuQue").css("font-weight", "bold");
        //}); 



        function logout() {
            window.location.href = "Login.aspx";
        } 
        function toMenu(mid) {
            changeMenu(mid);
            var url = "";
            switch (mid) {
            case "analysis":
                url = "Analysis.aspx";
                break;
            case "qustionManage":
                url = "QuestionnaireManage.aspx";
                break;
            case "classManage":
                url = "ClassManage.aspx";
                break;
            case "courseManage":
                url = "CourseManage.aspx";
                break;
            case "studentManage":
                url = "StuManage.aspx";
                break;
            case "teacherManage":
                url = "TeacherManage.aspx";
                break;
            case "manage":
                url = "Manage.aspx";
                break;
            case "personalInfo":
                url = "PersonalInfo.aspx";
                break;
            case "stuQue":
                url = "MyQuestionnaireNew.aspx";
                break;
            case "schooldistrict":
                url = "SPSchoolDistrict.aspx";
                break;
            case "schoolManage":
                url = "SPSchoolManage.aspx";
                break;
            default:
                break;
            }
            if (mid == "analysis") {
                $("#CourseNames").show();
            } else {
                $("#CourseNames").hide();
            }
            if (mid == "manage") {
                $("#msysmgr").show();
            } else {
                $("#msysmgr").hide();
            }

            $("#ifrcont").attr("src", url);
        }


        function changeMenu(mid) {
            $.each($(".pgxt"), function (key, value) {
                if (value.id == mid || value.id == "ctl00_" + mid) {
                    $("#" + value.id).css("color", "#000000");
                    $("#" + value.id).css("font-weight", "bold");
                } else {
                    $("#" + value.id).css("color", "#FFFFFF");
                    $("#" + value.id).css("font-weight", "normal");
                }
            });
        }


    </script>
</head>
<body style="background-color: #FFFFFF;margin: 0px;color: #000000"> 
    <table  style="width: 100%;border: 0px;border-collapse:collapse;border-spacing:0; background-image: url(images/back1.gif)">
        <tr>
            <td width="32%">
                <img src="images/logo.gif" width="476" height="125" /></td>
            <td width="68%">&nbsp;</td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="20%" background="images/back2.gif" height="33">
                <div align="center" class="gray9a" runat="server" id="userInfo">欢迎你，系统管理员！</div>
            </td>
            <td width="80%" background="images/back4.gif" height="33">
                <div style="margin-left: 10px;">
                    <div class="pgxt" style="float: left; margin-right: 20px; cursor: pointer;" id="schooldistrict" onclick="toMenu('schooldistrict')"  runat="server" >学区管理</div>
                    <div class="pgxt" style="float: left; margin-right: 20px; cursor: pointer;" id="schoolManage" onclick="toMenu('schoolManage')"  runat="server" >学校管理</div>
                    <div class="pgxt" style="float: left; margin-right: 20px; cursor: pointer;" id="studentManage" onclick="toMenu('studentManage')"  runat="server" >学员管理</div>
                    <div class="pgxt" style="float: left; margin-right: 20px; cursor: pointer" id="teacherManage" onclick="toMenu('teacherManage')"  runat="server" >教师管理</div>
                    <div class="pgxt" style="float: left; margin-right: 20px; cursor: pointer" id="courseManage" onclick="toMenu('courseManage')"  runat="server" >课程管理</div>
                    <div class="pgxt" style="float: left; margin-right: 20px; cursor: pointer" id="classManage" onclick="toMenu('classManage')"  runat="server" >班级管理</div>
                    <div class="pgxt" style="float: left; margin-right: 20px; cursor: pointer" id="qustionManage" onclick="toMenu('qustionManage')" runat="server" >培训评估</div>
                    <div class="pgxt" style="float: left; margin-right: 20px; cursor: pointer" id="analysis" onclick="toMenu('analysis')" runat="server" >统计分析</div>
                    <div class="pgxt" style="float: left; margin-right: 20px; cursor: pointer" id="manage" onclick="toMenu('manage')" runat="server" >系统管理</div>
                    <div class="pgxt" style="float: left; margin-right: 20px; cursor: pointer;" id="stuQue" onclick="toMenu('stuQue')"  runat="server" >问卷调查</div>
                    <div class="pgxt" style="float: left; margin-right: 20px; cursor: pointer" id="personalInfo" onclick="toMenu('personalInfo')" runat="server" >个人信息修改</div>
                    <div class="pgxt" style="float: left; margin-right: 20px; cursor: pointer" onclick="logout()" id="logoutSys" runat="server" >系统退出</div> 
                  <%--  <div class="pgxt" style="float: left; margin-right: 20px; cursor: pointer" onclick="logout()" id="logoutStu" runat="server" >系统退出</div>--%>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <iframe id="ifrcont" width="100%" height="1271" frameborder="0" scrolling="auto"   runat="server"></iframe>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td background="images/back1.gif" height="30">
                <div align="center" class="white9">北京市海淀区教育党校 北京市海淀区中小学干部研修中心 版权所有</div>
            </td>
        </tr>
    </table>

</body>
</html>
