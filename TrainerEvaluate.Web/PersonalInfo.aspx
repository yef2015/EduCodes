<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="PersonalInfo.aspx.cs" Inherits="TrainerEvaluate.Web.PersonalInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">姓名：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input type="text" id="StuName" name="StuName" class="easyui-textbox" /></td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">性别： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <select class="easyui-combobox" name="Gender" id="Gender" style="width: 170px;">
                    <option value="1">男</option>
                    <option value="2">女</option>
                </select>
            </td>
        </tr>
     <%--   <tr bgcolor="#FFFFFF">
            <td width="16%" class="gray10a" height="26">
                <div align="center">所在学校：</div>
            </td>
            <td width="35%" class="gray10a" height="26">
                <input type="text" id="School" class="easyui-textbox" /></td>
            <td width="15%" class="gray10a" height="26">
                <div align="center">职务：</div>
            </td>
            <td width="34%" class="gray10a" height="26">
                <input type="text" id="Title" class="easyui-textbox" /></td>
        </tr>
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">联系电话：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input type="text" id="TelNo" class="easyui-textbox" /></td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">身份证号： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input type="text" id="IdentityNo" class="easyui-textbox" /></td>
        </tr>--%>
        <tr bgcolor="#FFFFFF">
            <td width="16%" class="gray10a" height="26">
                <div align="center">账号：</div>
            </td>
            <td width="35%" class="gray10a" height="26">
                <input type="text" id="UserAccount" class="easyui-textbox" /></td>
            <td width="15%" class="gray10a" height="26">
                <div align="center">密码：</div>
            </td>
            <td width="34%" class="gray10a" height="26">
                <input type="text" id="UserPassWord" class="easyui-textbox" /></td>
        </tr>
        <tr>
            <td colspan="4" align="center" bgcolor="#F0F9FF">
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveUserInfo()" style="width: 90px">保存</a>
            </td>
        </tr>
    </table>
    <script type="text/javascript">

        function saveUserInfo() {
            var url = "GetUserInfo.ashx";
        <%--    var data = {
                t: 'e',
                uid: ' <%= UserId %>', UserName: $("#StuName").textbox("getText"), Gender: $("#Gender").combobox("getValue"),
                 School: $("#School").textbox("getText"), Title: $("#Title").textbox("getText"), TelNo: $("#TelNo").textbox("getText"),
                 IdentityNo: $("#IdentityNo").textbox("getText"), UserAccount: $("#UserAccount").textbox("getText"), UserPassWord: $("#UserPassWord").textbox("getText")

            };--%>
            var data = {
                t: 'e',
                uid: ' <%= UserId %>', UserName: $("#StuName").textbox("getText"), Gender: $("#Gender").combobox("getValue"),
                UserAccount: $("#UserAccount").textbox("getText"), UserPassWord: $("#UserPassWord").textbox("getText")  
             };
             $.post(url, data, function (result) {
                 if (result == "") {
                     $.messager.alert('提示', '保存成功!', 'info');
                 } else {
                     $.messager.alert('提示', result, 'warning');
                 }
             });
         }

         $(document).ready(function () {
             var url = "GetUserInfo.ashx";
             var data = { t: 'n', uid: ' <%= UserId %>' };
            $.post(url, data, function (result) {
                if (result != "") {
                    var res = result.split(',');
                    if (res.length > 0) {
                        for (var i = 0; i < res.length; i++) {
                            var stu = res[i].split(':');
                            switch (stu[0]) {
                                case "UserName":
                                    $("#StuName").textbox("setText", stu[1]);
                                    break;
                                case "Gender":
                                    $("#Gender").combobox("setValue", stu[1]);
                                    break;
                                case "School":
                                    $("#School").textbox("setText", stu[1]);
                                    break;
                                case "Title":
                                    $("#Title").textbox("setText", stu[1]);
                                    break;
                                case "TelNo":
                                    $("#TelNo").textbox("setText", stu[1]);
                                    break;
                                case "IdentityNo":
                                    $("#IdentityNo").textbox("setText", stu[1]);
                                    break;
                                case "UserAccount":
                                    $("#UserAccount").textbox("setText", stu[1]);
                                    break;
                                case "UserPassWord":
                                    $("#UserPassWord").textbox("setText", stu[1]);
                                    break;
                                case "UserRole":
                                    $("#urole").text();
                                default:
                            }
                        }
                    }
                }
            });

        });
    </script>

</asp:Content>
