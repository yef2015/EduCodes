<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="MyTeacherArchive.aspx.cs" Inherits="TrainerEvaluate.Web.MyTeacherArchive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div style="margin-top:10px;">
        <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1">
                <tr>
                    <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">个人档案信息</div>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">姓名：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aTeacherName">张三</span>
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
                        <div align="center">单位：</div>
                    </td>
                    <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aDept"></span>
                    </td>
                    <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">职务： </div>
                    </td>
                    <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aPost"></span>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">证件号：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aIdentityNo"></span>
                    </td>
                    <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">电话： </div>
                    </td>
                    <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aMobile"></span>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">研究方向：</div>
                    </td>
                    <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aResearch"></span>
                    </td>
                    <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">职称：</div>
                    </td>

                    <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a"><span id="aTitle"></span></td>

                </tr>
                <tr>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">描述：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aDescription"></span>
                    </td>
                    <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25"></td>
                    <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a"></td>
                </tr>
            </table>            
    </div>
    <div style="text-align:center;margin-top:10px;display:none;">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="editUser()" style="width: 120px">编辑个人信息</a>
    </div>


     <div id="dlg" class="easyui-dialog" style="width: 500px; height: 480px; padding: 10px 20px" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons">
        <div class="ftitle">详细信息</div>
        <form id="fm" method="post">
            <div class="fitem">
                <label>姓名:</label>
                <input name="TeacherName" id="TeacherName" class="easyui-textbox" style="width:280px;" required="true">
            </div>
            <div class="fitem">
                <label>性别</label> 
                 <select class="easyui-combobox" name="Gender" id="Gender" style="width:280px;"  data-options="url:'ComboboxGetData.ashx?t=g',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'" > 
                </select>
            </div>
            <div class="fitem">
                <label>身份证号:</label>
                <input name="IdentityNo" id="IdentityNo" style="width:280px;" class="easyui-textbox">
            </div>
            <div class="fitem">
                <label>所在单位:</label>
                <input name="Dept" id="Dept" style="width:280px;" class="easyui-textbox">
            </div>
            <div class="fitem">
                <label>职称:</label> 
                  <select class="easyui-combobox" name="Title" id="Title" style="width:280px;" data-options="url:'ComboboxGetData.ashx?t=j',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
               </select>
            </div>
            <div class="fitem">
                <label>职务:</label>
                <input name="Post" id="Post" class="easyui-textbox" style="width:280px;">
            </div>
            <div class="fitem">
                <label>研究方向:</label>                
                <input name="ResearchBigName" id="ResearchBigName" class="easyui-textbox" style="width:138px;">
                <input name="Research" id="Research" class="easyui-textbox" style="width:138px;">
            </div>
            <div class="fitem">
                <label>手机号:</label>
                <input name="Mobile" id="Mobile" class="easyui-textbox" style="width:280px;">
            </div>
            <div class="fitem">
                <label>描述:</label>
                <input name="Description" id="Description" class="easyui-textbox" data-options="multiline:true" style="height: 80px;width:280px;">
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveUser()" style="width: 90px">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 90px">取消</a>
    </div>
    <script type="text/javascript">
        var url = 'TeacherInfo.ashx';

        $(document).ready(function () {
            personArchive();
        });

        function personArchive() {
            url = "TeacherInfo.ashx";
            var data = { t: 'pah', uid: '<%= UserId %>' };
            $.post(url, data, function (result) {
                if (result != "") {
                    var dataObj = eval("(" + result + ")");//转换为json对象 

                    $.each(dataObj.rows, function (i, item) {
                        $('#aTeacherName').text(item.TeacherName);
                        $('#aGender').text(item.GenderName);
                        $('#aDept').text(item.Dept);
                        $('#aPost').text(item.Post);
                        $('#aIdentityNo').text(item.IdentityNo);
                        $('#aMobile').text(item.Mobile);
                        $('#aResearch').text(item.ResearchBigName + "  " + item.Research);
                        $('#aTitle').text(item.JobTitleName);
                        $('#aDescription').text(item.Description);
                    });
                }
            });
        }

        function editUser() {
            url = "TeacherInfo.ashx";
            var data = { t: 'pah', uid: '<%= UserId %>' };
            $.post(url, data, function (result) {
                if (result != "") {
                    var dataObj = eval("(" + result + ")");//转换为json对象 
                    
                    $.each(dataObj.rows, function (i, item) {
                        $('#dlg').dialog('open').dialog('setTitle', '编辑');
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
                        url = 'TeacherInfo.ashx' + '?t=e&id=' + item.TeacherId;
                    });
                }
            });
        }

        function saveUser() {
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
                Description: $('#Description').textbox("getText")
            };
            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlg').dialog('close');
                    personArchive();
                }
                else {
                    messageAlert('提示', result, 'warning');
                }
            });
        }
    </script>

</asp:Content>
