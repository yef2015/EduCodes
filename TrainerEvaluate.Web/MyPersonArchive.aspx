<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="MyPersonArchive.aspx.cs" Inherits="TrainerEvaluate.Web.MyPersonArchive" %>
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
                    <div align="center">联系电话： </div>
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
                    <div align="center">全日制学校：</div>
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
                    <div align="center">在职学校： </div>
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
                    <div align="center">任现任时间：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aRankTime"></span>
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
        </table>
    </div>
    <div style="text-align:center;margin-top:10px;display:none;">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="editUser()" style="width: 120px">编辑个人信息</a>
    </div>

    <div id="dlg" class="easyui-dialog" style="width: 700px; height: 530px; padding: 10px 20px" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons2">
        <div class="ftitle">详细信息</div>
        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1">
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
                    <select class="easyui-combobox" name="Gender" id="Gender" style="width: 153px;"  data-options="url:'ComboboxGetData.ashx?t=g',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
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
                    <select class="easyui-combobox" name="School" id="School" style="width: 153px;" data-options="url:'ComboxGetDropData.ashx?t=shl',method:'get',valueField:'Id',textField:'Name',panelHeight:'auto'" > 
                    </select>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">职称：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a"> 
                    <select class="easyui-combobox" name="JobTitle" id="JobTitle" style="width: 153px;"  data-options="url:'ComboboxGetData.ashx?t=j',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                     </select>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">联系电话： </div>
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
                    <select class="easyui-combobox" name="Nation" id="Nation" style="width: 153px;"  data-options="url:'ComboboxGetData.ashx?t=n',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
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
                    <div align="center">全日制学校： </div>
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
                    <div align="center">在职学校： </div>
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
                    <select class="easyui-combobox" name="PoliticsStaus" id="PoliticsStaus" style="width: 153px;"  data-options="url:'ComboboxGetData.ashx?t=p',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                   </select>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">现任级别： </div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">

                    <input name="Rank" id="Rank" class="easyui-textbox" />
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">任现任时间：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">

                    <input name="RankTime" id="RankTime" class="easyui-datebox" />
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">现任职务： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a"> 
                    <input name="Post" id="Post" style="width:75px;" class="easyui-textbox" />
                    <select class="easyui-combobox" name="PostOptName" id="PostOptName" style="width: 75px;"  data-options="url:'ComboboxGetData.ashx?t=ptn',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                    </select>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">任职时间：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <input name="PostTime" id="PostTime" class="easyui-datebox" />
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">手机号码： </div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <input name="Mobile" id="Mobile" class="easyui-textbox" required="true" />
                </td>
            </tr>
            <tr>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">继教号： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <input name="TeachNo" id="TeachNo" class="easyui-textbox" required="true" />
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25"></td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a"></td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">描述：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a" colspan="3">
                    <input name="Description" id="Description" class="easyui-textbox" 
                        data-options="multiline:true" style="height: 65px;width:500px;" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dlg-buttons2">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveUser()" style="width: 90px">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 90px">取消</a>
    </div>

    <script type="text/javascript">
        var url = 'StudentsInfo.ashx';

        $(document).ready(function () {            
            personArchive();
        });

        function personArchive()
        {
            url = "StudentsInfo.ashx";
            var data = { t: 'pah', uid: '<%= UserId %>' };
            $.post(url, data, function (result) {
                if (result != "") {
                    var dataObj = eval("(" + result + ")");//转换为json对象 

                    $.each(dataObj.rows, function (i, item) {
                        $('#aStuName').text(item.StuName);
                        if (item.GenderName != null) {
                            $('#aGender').text(item.GenderName);
                        }
                        if (item.JobTitleName != null) {
                            $('#aJobTitle').text(item.JobTitleName);
                        }
                        if (item.IdentityNo != null) {
                            $('#aIdentityNo').text(item.IdentityNo);
                        }
                        if (item.TelNo != null) {
                            $('#aTelNo').text(item.TelNo);
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
                        if (item.RankTime != null) {
                            $('#aRankTime').text(StringToDate(item.RankTime));
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
                        }
                        if (item.School != null) {
                            $('#aSchool').text(item.School);
                        }
                    });
                }
            });
        }

        function editUser() {
            url = "StudentsInfo.ashx";
            var data = { t: 'pah', uid: '<%= UserId %>' };
            $.post(url, data, function (result) {
                if (result != "") {
                    var dataObj = eval("(" + result + ")");//转换为json对象 

                    $.each(dataObj.rows, function (i, item) {
                        $('#dlg').dialog('open').dialog('setTitle', '编辑个人信息');
                        $('#School').combobox("setText", item.School);
                        if (item.JobTitle != 0) {
                            // 职称
                            $('#JobTitle').combobox("setValue", item.JobTitle);
                        }
                        else {
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
                        }
                        else {
                            $('#Nation').combobox("setValue", "");
                        }
                        $('#FirstRecord').textbox("setText", item.FirstRecord);
                        $('#FirstSchool').textbox("setText", item.FirstSchool);
                        $('#LastRecord').textbox("setText", item.LastRecord);
                        $('#LastSchool').textbox("setText", item.LastSchool);
                        if (item.PoliticsStatus != 0) {
                            // 政治面貌
                            $('#PoliticsStaus').combobox("setValue", item.PoliticsStatus);
                        }
                        else {
                            $('#PoliticsStaus').combobox("setValue", "");
                        }
                        $('#Rank').textbox("setText", item.Rank);
                        $('#RankTime').datebox("setValue", item.RankTime);
                        $('#Post').textbox("setText", item.Post);
                        $('#PostOptName').combobox("setValue", item.PostOptId);
                        $('#PostTime').datebox("setValue", item.PostTime);
                        $('#Mobile').textbox("setText", item.Mobile);
                        $('#Description').textbox("setText", item.Description);
                        $('#TeachNo').textbox("setText", item.TeachNo);
                        url = 'StudentsInfo.ashx' + '?t=e&id=' + item.StudentId;
                    });
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
                RankTime: $('#RankTime').textbox("getText"),
                Post: $('#Post').textbox("getText"),
                PostTime: $('#PostTime').textbox("getText"),
                Mobile: $('#Mobile').textbox("getText"),
                Description: $('#Description').textbox("getText"),
                TeachNo: $('#TeachNo').textbox("getText"),
                PostOptName: $('#PostOptName ').combobox("getText"),
                PostOptId: $('#PostOptName ').combobox("getValue")
            };
            if (data.StuName == "") {
                messageAlert('提示', "请填写姓名", 'warning');
                return;
            }
            if (data.IdentityNo == "") {
                messageAlert('提示', "请填写身份证号", 'warning');
                return;
            }
            if (data.Mobile == "") {
                messageAlert('提示', "请填写手机号码", 'warning');
                return;
            }
            if (data.TeachNo == "") {
                messageAlert('提示', "请填写继教号", 'warning');
                return;
            }
            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlg').dialog('close');
                    personArchive();
                } else {
                    messageAlert('提示', result, 'warning');
                }
            });
        }
    </script>
</asp:Content>
