<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="SPSchoolManage.aspx.cs" Inherits="TrainerEvaluate.Web.SPSchoolManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">名称：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="SchoolName1" class="easyui-textbox" id="SchoolName1" style="width: 165px;" />
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">所属学区： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="SchDisName1" class="easyui-textbox" id="SchDisName1" style="width: 165px;" />
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">地址：</div>
            </td>
            <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="AddressInfo1" class="easyui-textbox" id="AddressInfo1" style="width: 165px;" />
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">邮编： </div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="PostCode1" class="easyui-textbox" id="PostCode1" style="width: 165px;" />
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">办学性质： </div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <select class="easyui-combobox" name="RunNatureName1" id="RunNatureName1" style="width: 165px;" data-options="url:'ComboboxGetData.ashx?t=rc',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                </select>
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">法人名称： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="LegalName1" class="easyui-textbox" id="LegalName1" style="width: 165px;" />
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">学校类型：</div>
            </td>
            <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                <select class="easyui-combobox" name="SchoolTypeName1" id="SchoolTypeName1" style="width: 165px;" data-options="url:'ComboboxGetData.ashx?t=st',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                </select>
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25"></td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a"></td>
        </tr>
        <tr bgcolor="#F0F9FF">
            <td colspan="4" class="gray10a" height="26" align="center">
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="querySchool()" style="width: 90px">查询</a>
                &nbsp;&nbsp;
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-cancel" onclick="clearCondition()" style="width: 90px">清除条件</a>
            </td>
        </tr>
    </table>

    <div style="margin-top: 10px; margin-left: 20px; width: 99%">
        <table id="dg" title="学校信息" class="easyui-datagrid" style="width: 100%"
            url="SPSchoolInfo.ashx"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="SchoolId" width="0" hidden="true">编号</th>
                    <th field="SchoolName" width="20%" sortable="true">名称</th>
                    <th field="SchDisName" width="10%" sortable="true">所属学区</th>
                    <th field="AddressInfo" width="15%" sortable="true">地址</th>
                    <th field="PostCode" width="15%" sortable="true">邮编</th>
                    <th field="RunNatureName" width="15%" sortable="true">办学性质</th>
                    <th field="SchoolTypeName" width="18%" sortable="true">学校类型</th>
                    <th field="LegalName" width="15%" sortable="true">法人名称</th>
                  
                </tr>
            </thead>
        </table>
        <div id="toolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newSchool()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editSchool()">编辑</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroySchool()">删除</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="downloadTmp()">下载模板</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-upload" plain="true" onclick="uploadTmpData()">批量导入</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-redo" plain="true" onclick="exportData()">导出</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="ArchInfo()">查看学校信息</a>
        </div>
    </div>

    <div id="dlg" class="easyui-dialog" style="width: 500px; height: 520px; padding: 10px 20px" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons">
        <div class="ftitle">详细信息</div>
        <form id="fm" method="post">
            <div class="fitem">
                <label>名称：</label>
                <input name="SchoolName" id="SchoolName" class="easyui-textbox" style="width: 260px;" required="true">
            </div>
            <div class="fitem">
                <label>所属学区：</label>
                <select class="easyui-combobox" name="SchDisName" id="SchDisName" style="width: 260px;" data-options="url:'ComboxGetDropData.ashx?t=sdn',method:'post',valueField:'Id',textField:'Name'">
                </select>
            </div>
            <div class="fitem">
                <label>地址：</label>
                <input name="AddressInfo" id="AddressInfo" class="easyui-textbox" style="width: 260px;">
            </div>
            <div class="fitem">
                <label>邮编：</label>
                <input name="PostCode" id="PostCode" class="easyui-textbox" style="width: 260px;">
            </div>
            <div class="fitem">
                <label>办学性质：</label>
                <select class="easyui-combobox" name="RunNatureName" id="RunNatureName" style="width: 260px;" data-options="url:'ComboboxGetData.ashx?t=rc',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                </select>
            </div>
            <div class="fitem">
                <label>学校类型：</label>
                <select class="easyui-combobox" name="SchoolTypeName" id="SchoolTypeName" style="width: 260px;" data-options="url:'ComboboxGetData.ashx?t=st',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                </select>
            </div>
            <div class="fitem">
                <label>校址数：</label>
                <input name="AddrNum" id="AddrNum" class="easyui-textbox" style="width: 260px;">
            </div>
            <div class="fitem">
                <label>班级数：</label>
                <input name="ClassNum" id="ClassNum" class="easyui-textbox" style="width: 260px;">
            </div>
            <div class="fitem">
                <label>学生数：</label>
                <input name="StudentNum" id="StudentNum" class="easyui-textbox" style="width: 260px;">
            </div>
            <div class="fitem">
                <label>教师数：</label>
                <input name="TeacherNum" id="TeacherNum" class="easyui-textbox" style="width: 260px;">
            </div>
            <div class="fitem">
                <label>党员数：</label>
                <input name="PartyNum" id="PartyNum" class="easyui-textbox" style="width: 260px;">
            </div>
            <div class="fitem">
                <label>法人名称：</label>
                <input name="LegalName" id="LegalName" class="easyui-textbox" style="width: 260px;">
            </div>
            <div class="fitem">
                <label>联系电话：</label>
                <input name="LinkTel" id="LinkTel" class="easyui-textbox" style="width: 260px;">
            </div>
            <div class="fitem">
                <label>描述：</label>
                <input name="Description" id="Description" class="easyui-textbox" data-options="multiline:true" style="height: 65px; width: 260px;">
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveSchool()" style="width: 90px">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 90px">取消</a>
    </div>

    <div id="dlg1" class="easyui-dialog" style="width: 700px; height: 450px; padding: 10px 20px" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons1">
        <div class="ftitle" style="text-align: center">学校信息</div>
        <form id="fm1" method="post">
            <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1">
                <tr>
                    <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">基本信息：</div>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">名称：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aSchoolName"></span>
                    </td>
                    <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">所属学区： </div>
                    </td>
                    <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aSchDisName"></span>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">办学性质：</div>
                    </td>
                    <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aRunNatureName"></span>
                    </td>
                    <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">学校类型： </div>
                    </td>
                    <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aSchoolTypeName"></span>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">校址数：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aAddrNum"></span>
                    </td>
                    <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">班级数： </div>
                    </td>
                    <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aClassNum"></span>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">学生数：</div>
                    </td>
                    <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aStudentNum"></span>
                    </td>
                    <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">教师数：</div>
                    </td>
                    <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a"><span id="aTeacherNum"></span></td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">党员数：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aPartyNum"></span>
                    </td>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">法人名称：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aLegalName"></span>
                    </td>
                </tr> 
                 <tr>
                    <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">联系电话：</div>
                    </td>
                    <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a"><span id="aLinkTel"></span></td>
                    <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">描述：</div>
                    </td>
                    <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aDescription"></span>
                    </td>
                </tr>
                  <tr>
                    <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">地址：</div>
                    </td>
                    <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aAddressInfo"></span>
                    </td>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">邮编：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aPostCode"></span>
                    </td>
                </tr> 
            </table>

        </form>
    </div>
    <div id="dlg-buttons1">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg1').dialog('close')" style="width: 90px">关闭</a>
    </div>

    <div id="dlgUpload" class="easyui-dialog" style="width: 400px; height: 400px; padding: 10px 20px" closed="true" data-options="modal:true,top:10">
        <div class="ftitle">上传excel数据文件</div>
        <input type="file" id="upData" name="upData" />
        <div id="fileQueue"></div>
    </div>
    <input type="hidden" id="beforeSchoolName" name="beforeSchoolName" />

    <script type="text/javascript">

        var url = 'SPSchoolInfo.ashx';
        function newSchool() {
            $('#dlg').dialog('open').dialog('setTitle', '新增');
            $('#SchoolName').textbox("setText", "");
            $('#SchDisName').combobox("setValue", "");
            $('#RunNatureName').combobox("setValue", "");
            $('#SchoolTypeName').combobox("setValue", "");
            $('#AddrNum').textbox("setText", "");
            $('#ClassNum').textbox("setText", "");
            $('#StudentNum').textbox("setText", "");
            $('#TeacherNum').textbox("setText", "");
            $('#PartyNum').textbox("setText", "");
            $('#LegalName').textbox("setText", "");
            $('#LinkTel').textbox("setText", "");
            $('#Description').textbox("setText", "");
            $('#AddressInfo').textbox("setText", "");
            $('#PostCode').textbox("setText", "");

            url = 'SPSchoolInfo.ashx' + '?t=n';
        }
        function editSchool() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '编辑');

                $('#SchoolName').textbox("setText", row.SchoolName);
                $('#beforeSchoolName').val(row.SchoolName);

                $('#SchDisName').combobox("setValue", row.SchDisId);
                $('#RunNatureName').combobox("setValue", row.RunNatureCode);
                $('#SchoolTypeName').combobox("setValue", row.SchoolTypeCode);
                $('#AddrNum').textbox("setText", row.AddrNum);
                $('#ClassNum').textbox("setText", row.ClassNum);
                $('#StudentNum').textbox("setText", row.StudentNum);

                $('#TeacherNum').textbox("setText", row.TeacherNum);
                $('#PartyNum').textbox("setText", row.PartyNum);
                $('#LegalName').textbox("setText", row.LegalName);
                $('#LinkTel').textbox("setText", row.LinkTel);
                $('#Description').textbox("setText", row.Description);
                $('#AddressInfo').textbox("setText", row.AddressInfo);
                $('#PostCode').textbox("setText", row.PostCode);

                url = 'SPSchoolInfo.ashx' + '?t=e&id=' + row.SchoolId;
            } else {
                messageAlert('提示', '请选择要编辑的行!', 'warning');
            }
        }

        function ArchInfo() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg1').dialog('open').dialog('setTitle', '学校信息');
                $('#aSchoolName').text(row.SchoolName);
                $('#aSchDisName').text(row.SchDisName);
                $('#aRunNatureName').text(row.RunNatureName);
                $('#aSchoolTypeName').text(row.SchoolTypeName);
                $('#aAddrNum').text(row.AddrNum);
                $('#aClassNum').text(row.ClassNum);
                $('#aStudentNum').text(row.StudentNum);
                $('#aTeacherNum').text(row.TeacherNum);
                $('#aPartyNum').text(row.PartyNum);
                $('#aLegalName').text(row.LegalName);
                $('#aLinkTel').text(row.LinkTel);
                $('#aDescription').text(row.Description);
                $('#aAddressInfo').text(row.AddressInfo);
                $('#aPostCode').text(row.PostCode);
            } else {
                messageAlert('提示', '请选择要查看的行!', 'warning');
            }
        }

        function saveSchool() {
            var data = {
                SchoolName: $('#SchoolName').textbox("getText"),
                BeforeSchoolName: $('#beforeSchoolName').val(),
                SchDisId: $('#SchDisName').combobox("getValue"),
                SchDisName: $('#SchDisName').textbox("getText"),
                RunNatureCode: $('#RunNatureName').combobox("getValue"),
                RunNatureName: $('#RunNatureName').textbox("getText"),
                SchoolTypeCode: $('#SchoolTypeName').combobox("getValue"),
                SchoolTypeName: $('#SchoolTypeName').combobox("getText"),
                AddrNum: $('#AddrNum').textbox("getText"),
                ClassNum: $('#ClassNum').textbox("getText"),
                StudentNum: $('#StudentNum').textbox("getText"),
                TeacherNum: $('#TeacherNum').textbox("getText"),
                PartyNum: $('#PartyNum').textbox("getText"),
                LegalName: $('#LegalName').textbox("getText"),
                LinkTel: $('#LinkTel').textbox("getText"),
                Description: $('#Description').textbox("getText"),
                AddressInfo: $('#AddressInfo').textbox("getText"),
                PostCode: $('#PostCode').textbox("getText")
            };
            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlg').dialog('close');
                    $('#dg').datagrid('reload');
                }
                else {
                    alert(result);
                    //messageAlert('提示', result, 'warning');
                }
            });
        }


        function destroySchool() {
            url = 'SPSchoolInfo.ashx' + '?t=d';
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                //  $.messager.confirm('确认', '确定删除吗?', function (r) {
                if (confirm('确定删除吗?')) {
                    $.post(url, { id: row.SchoolId }, function (result) {
                        if (result == "" || result == null) {
                            $('#dg').datagrid('reload');    // reload the user data
                        } else {
                            $.messager.show({    // show error message
                                title: 'Error',
                                msg: result
                            });
                        }
                    });
                }
                //    });
            }
            else {
                messageAlert('标题', '请选择要删除的行!', 'warning');
            }
        }


        function downloadTmp() {
            var url = "DownloadTemplate.ashx?t=tsh";
            window.location = url;
        }

        function uploadTmpData() {
            $('#dlgUpload').dialog({
                title: '批量导入',
                height: '300px',
                closed: false,
                cache: false,
                modal: true
            });
            $('#dlgUpload').dialog('open');
        }


        $(function () {
            $('#upData').uploadify({
                'swf': 'Scripts/uploadify.swf',
                'uploader': 'UploadTmpData.ashx?t=tsh',
                'cancelImg': 'Scripts/cancel.png',
                'removeCompleted': true,
                'hideButton': false,
                'auto': true,
                'queueID': 'fileQueue',
                'fileTypeExts': '*.xls;*.xlsx',
                'fileTypeDesc': 'xls Files (.xls)',
                'onSelect': function (e) {
                    if (e.type != '.xlsx' && e.type != '.xls') {
                        alert("请上传excel文件!");
                    }
                },
                'onUploadSuccess': function (file, data, response) {
                    if (data != "" && data != "1") {
                        var result = data.split('|');
                        if (result.length > 0) {
                            if (confirm(result[0])) {
                                ifConfirmCover(result[1]);
                            } else {
                                $('#dlgUpload').dialog('close');
                                $('#dg').datagrid('reload');
                            }
                        } else {
                            alert(data);
                        }
                    }
                },
                'onUploadError': function (file, errorCode, errorMsg, errorString) {
                    $('#permissions_hint').show();
                },
                'onQueueComplete': function (queueData) {
                    if (queueData.uploadsSuccessful > 0) {
                        $('#dlgUpload').dialog('close');
                        $('#dg').datagrid('reload');
                    }
                }
            });
        });


        function ifConfirmCover(filename) {
            var url = "UploadTmpData.ashx?t=ctsh";
            var data = { "fname": filename };
            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlg').dialog('close');
                    $('#dg').datagrid('reload');
                }
                else {
                    messageAlert('提示', result, 'warning');
                }
            });
        }

        function exportData() {
            var url = "SPSchoolInfo.ashx?t=ex"
                + "&name=" + encodeURIComponent($("#SchoolName1").textbox('getText'))
                + "&sdn=" + $("#SchDisName1").textbox('getText')
                + "&rnn=" + $("#RunNatureName1").combobox("getValue")
                + "&lln=" + $('#LegalName1').textbox('getText')
                + "&addr=" + $('#AddressInfo1').textbox('getText')
                + "&postc=" + $('#PostCode1').textbox('getText')
                + "&sht=" + $("#SchoolTypeName1").combobox("getValue");
            window.location = url;
        }

        function clearCondition() {
            $('#SchoolName1').textbox('clear');
            $('#SchDisName1').textbox('clear');
            $('#RunNatureName1').combobox('clear');
            $('#LegalName1').textbox('clear');
            $('#SchoolTypeName1').combobox('clear');
            $('#AddressInfo1').textbox('clear');
            $('#PostCode1').textbox('clear');
        }


        function querySchool() {
            $('#dg').datagrid('load', {
                t: "q",
                schoolname: $("#SchoolName1").textbox('getText'),
                schdisname: $("#SchDisName1").textbox('getText'),
                runnature: $("#RunNatureName1").combobox("getValue"),
                schooltype: $("#SchoolTypeName1").combobox("getValue"),
                addressInfo: $("#AddressInfo1").textbox("getText"),
                postCode: $("#PostCode1").textbox("getText"),
                legalname: $("#LegalName1").textbox('getText')
            });
        }


    </script>
</asp:Content>
