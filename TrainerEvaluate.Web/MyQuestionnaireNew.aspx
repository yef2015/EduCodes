<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="MyQuestionnaireNew.aspx.cs" Inherits="TrainerEvaluate.Web.MyQuestionnaireNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function toOther(id) {
            window.location.href = "MyQuestionnaireNew.aspx?cid=" + id;
        }

        $.extend($.fn.combobox.defaults, {
            onSelect: function (data) {
                if (data != null) {
                    toOther(data.CourseId);
                }
            },
            onLoadSuccess: function (data) {
                var sel = document.getElementById('<%=hCouseId.ClientID  %>').value;
                $(this).combobox("setValue", sel);
            }
        });

        function checkRadio()
        {
            var val = $('input:radio[name="radAll"]:checked').val();
            if (val == null) {
                alert("您没有对‘您对本次培训课程的总体评价’进行评价。");
                $('input:radio[name="radAll"]').focus();
                return false;
            }

            var val1 = $('input:radio[name="radSubject"]:checked').val();
            if (val1 == null) {
                alert("您没有对‘课程主题清晰明确’进行评价。");
                $('input:radio[name="radSubject"]').focus();
                return false;
            }

            var val2 = $('input:radio[name="radContentRich"]:checked').val();
            if (val2 == null) {
                alert("您没有对‘课程内容丰富、能吸引人’进行评价。");
                $('input:radio[name="radContentRich"]').focus();
                return false;
            }

            var val3 = $('input:radio[name="radCoursePractical"]:checked').val();
            if (val3 == null) {
                alert("您没有对‘课程内容切合实际，能指导实践’进行评价。");
                $('input:radio[name="radCoursePractical"]').focus();
                return false;
            }

            var val4 = $('input:radio[name="radCourseKey"]:checked').val();
            if (val4 == null) {
                alert("您没有对‘课程内容重点突出，易于理解’进行评价。");
                $('input:radio[name="radCourseKey"]').focus();
                return false;
            }

            var val5 = $('input:radio[name="radCourseDevelop"]:checked').val();
            if (val5 == null) {
                alert("您没有对‘课程内容有助于个人发展’进行评价。");
                $('input:radio[name="radCourseDevelop"]').focus();
                return false;
            }

            var val6 = $('input:radio[name="radTeacherPrepare"]:checked').val();
            if (val6 == null) {
                alert("您没有对‘讲师准备比较充分’进行评价。");
                $('input:radio[name="radTeacherPrepare"]').focus();
                return false;
            }

            var val7 = $('input:radio[name="radTeacherLanguage"]:checked').val();
            if (val7 == null) {
                alert("您没有对‘语言表达清晰，态度端正’进行评价。");
                $('input:radio[name="radTeacherLanguage"]').focus();
                return false;
            }

            var val8 = $('input:radio[name="radTeacherBearing"]:checked').val();
            if (val8 == null) {
                alert("您没有对‘仪表仪容端庄大方，有亲和力’进行评价。");
                $('input:radio[name="radTeacherBearing"]').focus();
                return false;
            }

            var val9 = $('input:radio[name="radTeacherStyle"]:checked').val();
            if (val9 == null) {
                alert("您没有对‘培训方式多样，生动有趣’进行评价。");
                $('input:radio[name="radTeacherStyle"]').focus();
                return false;
            }

            var val10 = $('input:radio[name="radTeacherCommunication"]:checked').val();
            if (val10 == null) {
                alert("您没有对‘与学员沟通和互动有效’进行评价。");
                $('input:radio[name="radTeacherCommunication"]').focus();
                return false;
            }

            var val11 = $('input:radio[name="radOrgService"]:checked').val();
            if (val11 == null) {
                alert("您没有对‘培训服务周到细致’进行评价。");
                $('input:radio[name="radOrgService"]').focus();
                return false;
            }

            var val12 = $('input:radio[name="radOrgTime"]:checked').val();
            if (val12 == null) {
                alert("您没有对‘培训时间安排和控制合理’进行评价。");
                $('input:radio[name="radOrgTime"]').focus();
                return false;
            }

            var val13 = $('input:radio[name="radOrgArrange"]:checked').val();
            if (val13 == null) {
                alert("您没有对‘培训场所、设备安排到位’进行评价。");
                $('input:radio[name="radOrgArrange"]').focus();
                return false;
            }

            return true;
        }
    </script>

    <style type="text/css">
        .textbox .textbox-text {
            padding-top: 0px;
            padding-bottom: 0px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="CourseNames" runat="server" style="width: 169px; height: 1271px; font-size: 14px; margin-left: 15px;">
        待评估课程：
        <input class="easyui-combobox"
            name="CourseInfo" id="CourseInfo" style="height: 20px;"
            data-options="
                    url:'MyQuestionnaire.ashx?uid='+document.getElementById('<%=hUid.ClientID  %>').value,
                    method:'get',
                    valueField:'CourseId',
                    textField:'CourseName' 
            ">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="formStu" runat="server">
        <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" id="quNo" runat="server">
            <tr>
                <td class="gray10a">恭喜你，你已经答完全部课程。
                </td>
            </tr>
        </table>
        <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" id="quTime" runat="server">
            <tr>
                <td class="gray10a"><span id="timeTip" runat="server">评估时间为2014-11-29  00：00：00  到 2014-11-07  23：59：59 </span>
                </td>
            </tr>
        </table>
        
        <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" id="queHas" runat="server" style="margin-left: 10px;">
            <tr>
                <td width="16%" bgcolor="F8DCC2" class="gray10a" height="25">
                    <div align="center">课程名称：</div>
                </td>
                <td width="35%" bgcolor="F8DCC2" height="25" class="gray10a">
                    <span id="ipCourseName" runat="server"></span>
                </td>
                <td width="15%" bgcolor="F8DCC2" class="gray10a" height="25">
                    <div align="center">授课教师： </div>
                </td>
                <td width="34%" bgcolor="F8DCC2" height="25" class="gray10a">
                    <span id="ipTeacher" runat="server"></span>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F9ECD9" class="gray10a" height="25">
                    <div align="center">授课时间：</div>
                </td>
                <td width="35%" bgcolor="F9ECD9" height="25" class="gray10a">
                    <span id="ipTime" runat="server"></span>
                </td>
                <td width="15%" bgcolor="F9ECD9" class="gray10a" height="25">
                    <div align="center">授课地点： </div>
                </td>
                <td width="34%" bgcolor="F9ECD9" height="25" class="gray10a">
                    <span id="ipPlace" runat="server"></span>
                </td>
            </tr>
            <tr>
                <td width="16%" colspan="4" bgcolor="#FFFFFF" class="gray10a" height="25">&nbsp; </td>
            </tr>
        </table>
        <asp:Literal ID="LiteralContent" runat="server"></asp:Literal>        
        <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" id="queHasBtn" runat="server" style="margin-left: 10px;">
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="25">
                    <div align="center">
                        <asp:ImageButton runat="server" Width="91" Height="27" OnClientClick="return checkRadio();" OnClick="SubmitBtn_Click" ID="btnSubmit" />
                    </div>
                </td>
            </tr>
        </table>
        <div>
            <input type="hidden" runat="server" id="hCouseId" />
            <input type="hidden" runat="server" id="hUid" />
        </div>
    </form>
</asp:Content>
