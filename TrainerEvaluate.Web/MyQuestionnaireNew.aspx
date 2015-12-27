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
        <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" id="queHas2" runat="server" style="margin-left: 10px;">
            <tr bgcolor="#FFFFFF">
                <td width="16%" colspan="4" class="white10" height="26" bgcolor="4A5C69">
                    <img src="images/bank.gif" width="10" height="10">一、您对本次培训课程的总体评价是：</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="25">
                    <img src="images/bank.gif" width="35" height="10">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" ID="radAll" ForeColor="#000000">
                        <asp:ListItem Value="4">满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="3">比较满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">一般&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="1">不满意&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ID="req1" ControlToValidate="radAll" ErrorMessage="请选择" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="4A5C69" class="white10" height="25">
                    <img src="images/bank.gif" width="10" height="10">二、课程内容</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="28">
                    <img src="images/bank.gif" width="25" height="10">1.课程主题清晰明确</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="28">
                    <img src="images/bank.gif" width="35" height="10">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" ID="radSubject" ForeColor="#000000">
                        <asp:ListItem Value="4">满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="3">比较满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">一般&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="1">不满意&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="radSubject" ErrorMessage="请选择" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="26">
                    <img src="images/bank.gif" width="25" height="10">2.课程内容丰富、能吸引人</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="29">
                    <img src="images/bank.gif" width="35" height="10">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" ID="radContentRich" ForeColor="#000000">
                        <asp:ListItem Value="4">满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="3">比较满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">一般&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="1">不满意&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="radContentRich" ErrorMessage="请选择" Display="Dynamic"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="28">
                    <img src="images/bank.gif" width="25" height="10">3.课程内容切合实际，能指导实践</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="32">
                    <img src="images/bank.gif" width="35" height="10">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" ID="radCoursePractical" ForeColor="#000000">
                        <asp:ListItem Value="4">满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="3">比较满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">一般&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="1">不满意&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="radCoursePractical" ErrorMessage="请选择" Display="Dynamic"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="27">
                    <img src="images/bank.gif" width="25" height="10">4.课程内容重点突出，易于理解</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="30">
                    <img src="images/bank.gif" width="35" height="10">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" ID="radCourseKey" ForeColor="#000000">
                        <asp:ListItem Value="4">满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="3">比较满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">一般&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="1">不满意&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="radCourseKey" ErrorMessage="请选择" Display="Dynamic"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="29">
                    <img src="images/bank.gif" width="25" height="10">5.课程内容有助于个人发展</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="30">
                    <img src="images/bank.gif" width="35" height="10">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" ID="radCourseDevelop" ForeColor="#000000">
                        <asp:ListItem Value="4">满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="3">比较满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">一般&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="1">不满意&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="radCourseDevelop" ErrorMessage="请选择" Display="Dynamic"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="4A5C69" class="white10" height="25">
                    <img src="images/bank.gif" width="10" height="10">三、培训讲师</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="29">
                    <img src="images/bank.gif" width="25" height="10">1.讲师准备比较充分</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="28">
                    <img src="images/bank.gif" width="35" height="10">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" ID="radTeacherPrepare" ForeColor="#000000">
                        <asp:ListItem Value="4">满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="3">比较满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">一般&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="1">不满意&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="radTeacherPrepare" ErrorMessage="请选择" Display="Dynamic"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="25">
                    <img src="images/bank.gif" width="25" height="10">2.语言表达清晰，态度端正</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="28">
                    <img src="images/bank.gif" width="35" height="10">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" ID="radTeacherLanguage" ForeColor="#000000">
                        <asp:ListItem Value="4">满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="3">比较满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">一般&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="1">不满意&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="radTeacherLanguage" ErrorMessage="请选择" Display="Dynamic"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="28">
                    <img src="images/bank.gif" width="25" height="10">3.仪表仪容端庄大方，有亲和力</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="31">
                    <img src="images/bank.gif" width="35" height="10">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" ID="radTeacherBearing" ForeColor="#000000">
                        <asp:ListItem Value="4">满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="3">比较满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">一般&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="1">不满意&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="radTeacherBearing" ErrorMessage="请选择" Display="Dynamic"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="25">
                    <img src="images/bank.gif" width="25" height="10">4.培训方式多样，生动有趣</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="30">
                    <img src="images/bank.gif" width="35" height="10">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" ID="radTeacherStyle" ForeColor="#000000">
                        <asp:ListItem Value="4">满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="3">比较满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">一般&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="1">不满意&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="radTeacherStyle" ErrorMessage="请选择" Display="Dynamic"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="25">
                    <img src="images/bank.gif" width="25" height="10">5.与学员沟通和互动有效</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="28">
                    <img src="images/bank.gif" width="35" height="10">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" ID="radTeacherCommunication" ForeColor="#000000">
                        <asp:ListItem Value="4">满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="3">比较满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">一般&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="1">不满意&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="radTeacherCommunication" ErrorMessage="请选择" Display="Dynamic"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="4A5C69" class="white10" height="25">
                    <img src="images/bank.gif" width="10" height="10">四、培训组织和管理</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="28">
                    <img src="images/bank.gif" width="25" height="10">1.培训服务周到细致</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="30">
                    <img src="images/bank.gif" width="35" height="10">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" ID="radOrgService" ForeColor="#000000">
                        <asp:ListItem Value="4">满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="3">比较满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">一般&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="1">不满意&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="radOrgService" ErrorMessage="请选择" Display="Dynamic"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="30">
                    <img src="images/bank.gif" width="25" height="10">2.培训时间安排和控制合理</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="32">
                    <img src="images/bank.gif" width="35" height="10">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" ID="radOrgTime" ForeColor="#000000">
                        <asp:ListItem Value="4">满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="3">比较满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">一般&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="1">不满意&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ControlToValidate="radOrgTime" ErrorMessage="请选择" Display="Dynamic"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="25">
                    <img src="images/bank.gif" width="25" height="10">3.培训场所、设备安排到位</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="32">
                    <img src="images/bank.gif" width="35" height="10">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" ID="radOrgArrange" ForeColor="#000000">
                        <asp:ListItem Value="4">满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="3">比较满意&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">一般&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="1">不满意&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ControlToValidate="radOrgArrange" ErrorMessage="请选择" Display="Dynamic"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="4A5C69" class="gray10a" height="25">
                    <img src="images/bank.gif" width="10" height="10"><span class="white10">五、您对本课程还有哪些建议？</span></td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="187">
                    <img src="images/bank.gif" width="25" height="10">
                    <textarea name="textfield" cols="100" rows="10" runat="server" id="txtSuggest"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#FFFFFF" class="gray10a" height="25">
                    <div align="center">
                        <asp:ImageButton runat="server" Width="91" Height="27" OnClick="SubmitBtn_Click" ID="btnSubmit" />
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
