<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="PersonalInfoStudent.aspx.cs" Inherits="TrainerEvaluate.Web.PersonalInfoStudent" %>

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
                <input name="StuName" id="StuName" class="easyui-textbox" required="true" />
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">性别： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <select class="easyui-combobox" name="Gender" id="Gender" style="width: 153px;" data-options="url:'ComboboxGetData.ashx?t=g',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'" required="true">
                </select>
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">身份证号：</div>
            </td>
            <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="IdentityNo" id="IdentityNo" class="easyui-textbox" required="true" />
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">所在学校： </div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <select class="easyui-combobox" name="School" id="School" style="width: 153px;" data-options="url:'ComboxGetDropData.ashx?t=shl',method:'get',valueField:'Id',textField:'Name'"  required="true">
                </select>
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">职称：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <select class="easyui-combobox" name="JobTitle" id="JobTitle" style="width: 153px;" data-options="url:'ComboboxGetData.ashx?t=j',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                </select>
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">办公电话： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="TelNo" id="TelNo" class="easyui-textbox" />
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">出生日期：</div>
            </td>
            <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="Birthday" id="Birthday" class="easyui-datebox" />
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">民族： </div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <select class="easyui-combobox" name="Nation" id="Nation" style="width: 153px;" data-options="url:'ComboboxGetData.ashx?t=n',method:'get',valueField:'ID',textField:'Name'">
                </select>
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">全日制学历：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="FirstRecord" id="FirstRecord" class="easyui-textbox" />
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">全日制毕业学校： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="FirstSchool" id="FirstSchool" class="easyui-textbox" />
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">在职学历：</div>
            </td>
            <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="LastRecord" id="LastRecord" class="easyui-textbox" />
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">在职毕业学校： </div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="LastSchool" id="LastSchool" class="easyui-textbox" />
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">政治面貌：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <select class="easyui-combobox" name="PoliticsStaus" id="PoliticsStaus" style="width: 153px;" data-options="url:'ComboboxGetData.ashx?t=p',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                </select>
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">现任级别： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">  
               <select class="easyui-combobox" name="Rank" id="Rank" style="width:165px;"  data-options="url:'ComboboxGetData.ashx?t=rank',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'" > 
                </select>
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">主管工作：</div>
            </td>
            <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a"> 
                <input name="ManageWork" id="ManageWork" class="easyui-textbox" required="true" />
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">现任职务： </div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="Post" id="Post"   class="easyui-textbox"  required="true" />
               <%-- <select class="easyui-combobox" name="PostOptName" id="PostOptName" style="width: 75px;" data-options="url:'ComboboxGetData.ashx?t=ptn',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'"  required="true">
                </select>--%>
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">任职时间：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="PostTime" id="PostTime" class="easyui-textbox" />
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">手机号码： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="Mobile" id="Mobile" class="easyui-textbox" required="true" required="true" />
            </td>
        </tr>
        <tr>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">继教号： </div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="TeachNo" id="TeachNo" class="easyui-textbox" required="true" />
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">密码： </div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input type="text" id="UserPassWord" class="easyui-textbox" style="width: 165px;"/>
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">描述：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a" colspan="3">
                <input name="Description" id="Description" class="easyui-textbox"
                    data-options="multiline:true" style="height: 65px; width: 500px;" />
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center" bgcolor="#F0F9FF">
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveUser()" style="width: 90px">保存</a>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var url = 'StudentsInfo.ashx';

        $(document).ready(function () {
            personArchive();
        });

        function personArchive() {
            url = "StudentsInfo.ashx";
            var data = { t: 'psdif', uid: '<%= UserId %>' };
             $.post(url, data, function (result) {
                 if (result != "") {
                     var dataObj = eval("(" + result + ")"); //转换为json对象 

                     $.each(dataObj.rows, function(i, item) {
                         $('#School').textbox("setText", item.School);
                         if (item.JobTitle != 0) {
                             // 职称
                             $('#JobTitle').combobox("setValue", item.JobTitle);
                         } else {
                             $('#JobTitle').combobox("setValue", "");
                         }
                         $('#IdentityNo').textbox("setText", item.IdentityNo);
                         $('#TelNo').textbox("setText", item.TelNo);
                         $('#StuName').textbox("setText", item.StuName);
                         $('#Gender').combobox("setValue", item.Gender);
                         $('#Birthday').datebox("setValue", item.Birthday);
                         if (item.Nation != 0) {
                             // 民族
                             $('#Nation').combobox("setValue", item.Nation);
                         } else {
                             $('#Nation').combobox("setValue", "");
                         }
                         $('#FirstRecord').textbox("setText", item.FirstRecord);
                         $('#FirstSchool').textbox("setText", item.FirstSchool);
                         $('#LastRecord').textbox("setText", item.LastRecord);
                         $('#LastSchool').textbox("setText", item.LastSchool);
                         if (item.PoliticsStatus != 0) {
                             // 政治面貌
                             $('#PoliticsStaus').combobox("setValue", item.PoliticsStatus);
                         } else {
                             $('#PoliticsStaus').combobox("setValue", "");
                         }
                         $('#Rank').textbox("setText", item.Rank);
                         $('#ManageWork').textbox("setText", item.ManageWork);
                         //    $('#RankTime').datebox("setValue", item.RankTime);
                         $('#Post').textbox("setText", item.Post);
                       //  $('#PostOptName').combobox("setValue", item.PostOptId);
                         $('#PostTime').textbox("setValue", item.PostTime);
                         $('#Mobile').textbox("setText", item.Mobile);
                         $('#Description').textbox("setText", item.Description);
                         $('#TeachNo').textbox("setText", item.TeachNo);
                         $('#UserPassWord').textbox("setText", item.UserPassWord);
                         url = 'StudentsInfo.ashx' + '?t=esif&id=' + item.StudentId;
                     });
                 } else {
                     alert("获取信息失败！");
                 }
             });
         }

         function saveUser() {
             var data = {
                 StuName: $('#StuName').textbox("getText"),
                 Gender: $('#Gender').combobox("getValue"),
                 IdentityNo: $('#IdentityNo').textbox("getText"),
                 School: $('#School').combobox("getText"),
                 JobTitle: $('#JobTitle').combobox("getValue"),
                 TelNo: $('#TelNo').textbox("getText"),
                 Birthday: $('#Birthday').textbox("getText"),
                 Nation: $('#Nation').combobox("getValue"),
                 FirstRecord: $('#FirstRecord').textbox("getText"),
                 FirstSchool: $('#FirstSchool').textbox("getText"),
                 LastRecord: $('#LastRecord').textbox("getText"),
                 LastSchool: $('#LastSchool').textbox("getText"),
                 PoliticsStaus: $('#PoliticsStaus').combobox("getValue"),
                 Rank: $('#Rank').textbox("getText"),
                 ManageWork: $('#ManageWork').textbox("getText"),
             //    RankTime: $('#RankTime').textbox("getText"),
                 Post: $('#Post').textbox("getText"),
                 PostTime: $('#PostTime').textbox("getText"),
                 Mobile: $('#Mobile').textbox("getText"),
                 Description: $('#Description').textbox("getText"),
                 TeachNo: $('#TeachNo').textbox("getText"),
                // PostOptName: $('#PostOptName').combobox("getText"),
                // PostOptId: $('#PostOptName').combobox("getValue"),
                 UserPassWord: $('#UserPassWord ').textbox("getText"),
             };
             if (data.StuName == "") {
                 messageAlert('提示', "请填写姓名", 'warning');
                 return;
             }
             if (data.Gender == "") {
                 messageAlert('提示', "请填写性别", 'warning');
                 return;
             }
             if (data.IdentityNo == "") {
                 messageAlert('提示', "请填写身份证号", 'warning');
                 return;
             }
             if (data.School == "") {
                 messageAlert('提示', "请选择所在学校", 'warning');
                 return;
             }
             if (data.Post == "") {
                 messageAlert('提示', "请填写现任职务", 'warning');
                 return;
             }
             //if (data.PostOptName == "") {
             //    messageAlert('提示', "请填写选择现任职务", 'warning');
             //    return;
             //}
             if (data.Mobile == "") {
                 messageAlert('提示', "请填写手机号码", 'warning');
                 return;
             }
             if (data.TeachNo == "") {
                 messageAlert('提示', "请填写继教号", 'warning');
                 return;
             }
             if (data.ManageWork == "") {
                 messageAlert('提示', "请填主管工作", 'warning');
                 return;
             }
             $.post(url, data, function (result) {
                 if (result == "") {
                     alert('保存成功!');
                     window.location.href = "NetForStudent.aspx";
                 } else {
                     alert(result);
                 }
             });
         }
    </script>
</asp:Content>
