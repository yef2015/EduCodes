<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="PersonalInfoTeacher.aspx.cs" Inherits="TrainerEvaluate.Web.PersonalInfoTeacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table width="96%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">姓名：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="TeacherName" id="TeacherName" class="easyui-textbox" style="width: 200px;" required="true" /></td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">性别： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <select class="easyui-combobox" name="Gender" id="Gender" style="width: 200px;">
                    <option value="1">男</option>
                    <option value="2">女</option>
                </select>
            </td>
        </tr>
        <tr bgcolor="#FFFFFF">
            <td width="16%" class="gray10a" height="26">
                <div align="center">身份证号：</div>
            </td>
            <td width="35%" class="gray10a" height="26">
                <input name="IdentityNo" id="IdentityNo" style="width: 200px;" class="easyui-textbox" required="true" /></td>
            <td width="15%" class="gray10a" height="26">
                <div align="center">所在单位：</div>
            </td>
            <td width="34%" class="gray10a" height="26">
                <input name="Dept" id="Dept" style="width: 200px;" class="easyui-textbox" /></td>
        </tr>
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">职称：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <select class="easyui-combobox" name="Title" id="Title" style="width:200px;" data-options="url:'ComboboxGetData.ashx?t=j',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
               </select>
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">职务： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="Post" id="Post" class="easyui-textbox" style="width: 200px;" />
            </td>
        </tr>
        <tr bgcolor="#FFFFFF">
            <td width="15%" class="gray10a" height="25">
                <div align="center">研究方向： </div>
            </td>
            <td width="34%" height="25" class="gray10a">
                <input name="ResearchBigName" id="ResearchBigName" class="easyui-textbox" style="width: 98px;">
                <input name="Research" id="Research" class="easyui-textbox" style="width: 98px;">
            </td>
            <td width="15%" class="gray10a" height="25">
                <div align="center">手机号： </div>
            </td>
            <td width="34%" height="25" class="gray10a">
                <input type="text" id="Mobile" name="Mobile" class="easyui-textbox" style="width: 200px;" />
            </td>
        </tr>
        <tr>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">密码： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input type="text" id="UserPassWord" class="easyui-textbox" style="width: 200px;" />
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25"></td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a"></td>
        </tr>
        <tr bgcolor="#FFFFFF">
            <td width="16%" class="gray10a" height="25">
                <div align="center">描述：</div>
            </td>
            <td width="35%" height="25" class="gray10a" colspan="3">
                <input name="Description" id="Description" class="easyui-textbox"
                    data-options="multiline:true" style="height: 65px; width: 500px;" />
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center" bgcolor="#F0F9FF">
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveUserInfo()" style="width: 90px">保存</a>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var url = 'TeacherInfo.ashx';

        $(document).ready(function () {
            personTeacher();
        });

        function personTeacher() {
            url = "TeacherInfo.ashx";
            var data = { t: 'ptinf', uid: '<%= UserId %>' };
            $.post(url, data, function (result) {
                if (result != "") {
                    var dataObj = eval("(" + result + ")");//转换为json对象 

                    $.each(dataObj.rows, function (i, item) {
                        $('#TeacherName').textbox("setText", item.TeacherName);
                        $('#IdentityNo').textbox("setText", item.IdentityNo);
                        $('#Dept').textbox("setText", item.Dept);
                        $('#Title').combobox("setValue", item.Title);
                        $('#Gender').combobox("setValue", item.Gender);
                        $('#Post').textbox("setText", item.Post);
                        $('#Research').textbox("setText", item.Research);
                        $('#ResearchBigName').textbox("setText", item.ResearchBigName);
                        $('#Mobile').textbox("setText", item.Mobile);
                        $('#Description').textbox("setText", item.Description);
                        $('#UserPassWord').textbox("setText", item.UserPassWord);
                        url = 'TeacherInfo.ashx' + '?t=etinf&id=' + item.TeacherId;
                    });
                }
            });
        }

        function saveUserInfo() {
            var data = {
                TeacherName: $('#TeacherName').textbox("getText"),
                Gender: $('#Gender').combobox("getValue"),
                IdentityNo: $('#IdentityNo').textbox("getText"),
                Dept: $('#Dept').textbox("getText"),
                Title: $('#Title').combobox("getValue"),
                Post: $('#Post').textbox("getText"),
                Research: $('#Research').textbox("getText"),
                ResearchBigName: $('#ResearchBigName').textbox("getText"),
                Mobile: $('#Mobile').textbox("getText"),
                Description: $('#Description').textbox("getText"),
                UserPassWord: $('#UserPassWord').textbox("getText")
            };
            if (data.TeacherName == "") {
                messageAlert('提示', "请填写姓名", 'warning');
                return;
            }
            if (data.IdentityNo == "") {
                messageAlert('提示', "请填写身份证号", 'warning');
                return;
            }
            $.post(url, data, function (result) {
                if (result == "") {
                    alert('保存成功!');
                } else {
                    alert(result);
                }
            });
        }
    </script>
</asp:Content>
