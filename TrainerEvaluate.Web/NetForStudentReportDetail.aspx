<%@ Page Title="学员网上报名" Language="C#" MasterPageFile="~/PageContent.Master" AutoEventWireup="true" CodeBehind="NetForStudentReportDetail.aspx.cs" Inherits="TrainerEvaluate.Web.NetForStudentReportDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin: 20px;">
        <tr>
            <td bgcolor="F0F9FF" class="gray10a" height="25" colspan="4" style="text-align: center; font-weight: bold">培训班信息
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">班级名称：</div>
            </td>
            <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                <span id="cName"></span>
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">培训对象：</div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <span id="cObject"></span>
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">开始日期：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <span id="cStartDate"></span>
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">结束日期：</div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <span id="cFinishDate"></span>
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25" colspan="1">
                <div align="center">培训内容：</div>
            </td>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25" colspan="3">
                <span id="cDescription"></span>
            </td>
        </tr>
        <tr>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">学时：</div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <span id="cPoint"></span>
            </td>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">学时类型：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <span id="cPointType"></span>
            </td>
        </tr>
        <tr>

            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">项目负责人：</div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <span id="cTeacher"></span>
            </td>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">培训形式：</div>
            </td>
            <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                <span id="cArea"></span>
            </td>
        </tr>
        <tr>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
               <div align="center">报名截止日期:：</div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
              <span id="cCloseDate"></span>
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">培训层次：</div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <span id="cType"></span>
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">报名上限人数：</div>
            </td>
            <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                <span id="cReportMax"></span>
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
               
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                
            </td>
        </tr>
        <tr>
            <td bgcolor="F0F9FF" class="gray10a" height="25" colspan="4" style="text-align: center; font-weight: bold">学员个人信息
            </td>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">姓名：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aStuName"></span>
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">性别： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aGender"></span>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">所在学校：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <span id="aSchool"></span>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">职务： </div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <span id="aJobTitle"></span>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">身份证号：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aIdentityNo"></span>
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">办公电话： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aTelNo"></span>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">任职时间：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <span id="aPostTime"></span>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">手机号码：</div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a"><span id="aMobile"></span></td>
            </tr>

            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">出生日期：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aBirthday"></span>
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">民族： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aNation"></span>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">全日制学历：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <span id="aFirstRecord"></span>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">全日制毕业学校：</div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a"><span id="aFirstSchool"></span></td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">在职学历：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aLastRecord"></span>
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">在职毕业学校： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aLastSchool"></span>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">政治面貌：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <span id="aPoliticsStaus"></span>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">现任级别：</div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a"><span id="aRank"></span></td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">主管工作：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aManageWork"></span>
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">现任职务： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aPost"></span>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">描述：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <span id="aDescription"></span>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">继教号： </div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <span id="aTeachNo"></span>
                </td>
            </tr>
        </tr>
        <tr bgcolor="#FFFFFF">
            <td colspan="4" class="gray10a" height="26" align="center">
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="signUp()" style="width: 90px">报名</a>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var num = 0;//单位、姓名、性别、职务、主管工作、联系电话、继教号
        function signUp() {
            if (num > 0) { //信息不全的，没有维护的就不让报名，给一个链接跳转到个人信息维护页面
                if (confirm("个人信息不完整，请补充完整后再次报名！")) {
                    window.parent.location.href = "PersonalInfoStudent.aspx";
                }
            } else { 
                var url = "NetForStudentInfo.ashx?t=sv&cid=" + '<%= ClassId %>' + "&uid=" + '<%= UserId %>';
                
                $.post(url, null, function (result) {
                    if (result == "") {
                        alert("报名成功！");
                        window.parent.location.href = "NetForStudent.aspx";
                    } else {
                        alert("数据保存失败！" + result);
                    }
                });
            }
        }


        $(document).ready(function () {
            setClassInfo('<%= ClassId %>');
            personArchive();
        });

        function personArchive() {
            url = "StudentsInfo.ashx";
            var data = { t: 'pah', uid: '<%= UserId %>' };
            $.post(url, data, function (result) {
                num = 0;
                if (result != "") {
                    var dataObj = eval("(" + result + ")");//转换为json对象 

                    $.each(dataObj.rows, function (i, item) {
                        if (item.StuName != null) {
                            $('#aStuName').text(item.StuName);
                        } else {
                            num++;
                        }
                        if (item.GenderName != null) {
                            $('#aGender').text(item.GenderName);
                        }else {
                            num++;
                        }
                        if (item.JobTitleName != null) {
                            $('#aJobTitle').text(item.JobTitleName);
                        }else {
                            num++;
                        }
                        if (item.IdentityNo != null) {
                            $('#aIdentityNo').text(item.IdentityNo);
                        }
                        if (item.TelNo != null) {
                            $('#aTelNo').text(item.TelNo);
                        }else {
                            num++;
                        }
                        if (item.Birthday != null) {
                            $('#aBirthday').text(StringToDate(item.Birthday));
                        }
                        if (item.NationName != null) {
                            $('#aNation').text(item.NationName);
                        }
                        if (item.FirstRecord != null) {
                            $('#aFirstRecord').text(item.FirstRecord);
                        }
                        if (item.FirstSchool != null) {
                            $('#aFirstSchool').text(item.FirstSchool);
                        }
                        if (item.LastRecord != null) {
                            $('#aLastRecord').text(item.LastRecord);
                        }
                        if (item.LastSchool != null) {
                            $('#aLastSchool').text(item.LastSchool);
                        }
                        if (item.PoliticsStatusName != null) {
                            $('#aPoliticsStaus').text(item.PoliticsStatusName);
                        }
                        if (item.Rank != null) {
                            $('#aRank').text(item.Rank);
                        }
                        if (item.ManageWork != null) {
                            $('#aManageWork').text(item.ManageWork);
                        }
                        if (item.Post != null) {
                            if (item.PostOptName != null) {
                                $('#aPost').text(item.Post + "  " + item.PostOptName);
                            }
                            else {
                                $('#aPost').text(item.Post);
                            }
                        }
                        if (item.PostTime != null) {
                            $('#aPostTime').text(StringToDate(item.PostTime));
                        }
                        if (item.Mobile != null) {
                            $('#aMobile').text(item.Mobile);
                        }
                        if (item.Description != null) {
                            $('#aDescription').text(item.Description);
                        }
                        if (item.TeachNo != null) {
                            $('#aTeachNo').text(item.TeachNo);
                        } else {
                            num++;
                        }
                        if (item.School != null) {
                            $('#aSchool').text(item.School);
                        }
                    });
                }
            });
        }


        function setClassInfo(cid) {
            url = "ClassInfo.ashx";
            var data = { t: 'pah', classId: cid };
            $.post(url, data, function (result) {
                if (result != "") {
                    var dataObj = eval("(" + result + ")");//转换为json对象 
                    if (dataObj != null) { 
                        $('#cName').text(dataObj.Name);
                        $('#cObject').text(dataObj.ObjectName);
                        $('#cStartDate').text(dataObj.StartDate.substring(0,10));
                        $('#cFinishDate').text(dataObj.FinishDate.substring(0, 10));
                        $('#cDescription').text(dataObj.Description);
                        $('#cPointType').text(dataObj.PointTypeName);
                        $('#cPoint').text(dataObj.Point);
                        $('#cTeacher').text(dataObj.Teacher);
                        $('#cArea').text(dataObj.AreaName);
                        $('#cLevel').text(dataObj.Level);
                        $('#cType').text(dataObj.TypeName);
                        $('#cReportMax').text(dataObj.ReportMax);
                        $('#cCloseDate').text(dataObj.CloseDate.substring(0, 10));
                    } 
                }
            });
        }
    </script>


</asp:Content>
