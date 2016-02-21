<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="SPSchoolDistrict.aspx.cs" Inherits="TrainerEvaluate.Web.SPSchoolDistrict" %>
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
                <input name="SchDisName1" class="easyui-textbox" id="SchDisName1">
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">描述： </div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="Description1" class="easyui-textbox" id="Description1">
            </td>
        </tr>

        <tr bgcolor="#FFFFFF">
            <td colspan="4" class="gray10a" height="26" align="center">
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="querySchDis()" style="width: 90px">查询</a>
                &nbsp;&nbsp;
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-cancel" onclick="clearCondition()" style="width: 90px">清除条件</a>
            </td>
        </tr>
    </table>

    <div style="margin-top: 10px; margin-left: 20px; width: 99%">
        <table id="dg" title="学区信息" class="easyui-datagrid" style="width: 100%"
            url="SPSchoolDistrictInfo.ashx"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="SchDisId" width="0" hidden="true">编号</th>
                    <th field="SchDisName" width="42%" sortable="true">名称</th>
                    <th field="Description" width="55%" sortable="true">描述</th>
                </tr>
            </thead>
        </table>
        <div id="toolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newSchDis()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editSchDis()">编辑</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroySchDis()">删除</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="downloadTmp()">下载模板</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-upload" plain="true" onclick="uploadTmpData()">批量导入</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-redo" plain="true" onclick="exportData()">导出</a>
           <%-- <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="ArchInfo()">学区档案</a>--%>
        </div>
    </div>



    <div id="dlg" class="easyui-dialog" style="width: 550px; height: 320px; padding: 10px 20px" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons">
        <div class="ftitle">详细信息</div>
        <form id="fm" method="post">
            <div class="fitem">
                <label>名称:</label>
                <input name="SchDisName" id="SchDisName" class="easyui-textbox" style="width:300px;" required="true">
            </div>
            <div class="fitem">
                <label>描述:</label>
                <input name="Description" id="Description" class="easyui-textbox" data-options="multiline:true" style="height: 100px;width:300px;">
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveSchDis()" style="width: 90px">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 90px">取消</a>
    </div>

    <div id="dlg1" class="easyui-dialog" style="width: 450px; height: 300px; padding: 10px 20px" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons1">
        <div class="ftitle" style="text-align: center">学区档案信息</div>
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
                        <span id="aSchDisName"></span>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">描述：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aDescription"></span>
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

    <script type="text/javascript">

        var url = 'SPSchoolDistrictInfo.ashx';
        function newSchDis() {
            $('#dlg').dialog('open').dialog('setTitle', '新增');

            $('#SchDisName').textbox("setText", "");
            $('#Description').textbox("setText", "");

            url = 'SPSchoolDistrictInfo.ashx' + '?t=n';
        }
        function editSchDis() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '编辑');
                //  $('#fm').form('load', row);

                $('#SchDisName').textbox("setText", row.SchDisName);
                $('#Description').textbox("setText", row.Description);
                url = 'SPSchoolDistrictInfo.ashx' + '?t=e&id=' + row.SchDisId;
            } else {
                messageAlert('提示', '请选择要编辑的行!', 'warning');
            }
        }

        function ArchInfo() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg1').dialog('open').dialog('setTitle', '学区档案');
                $('#aSchDisName').text(row.SchDisName);
                $('#aDescription').text(row.Description);
            } else {
                messageAlert('提示', '请选择要查看的行!', 'warning');
            }
        }

        function saveSchDis() {
            var data = {
                SchDisName: $('#SchDisName').textbox("getText"),
                Description: $('#Description').textbox("getText")
            };
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


        function destroySchDis() {
            url = 'SPSchoolDistrictInfo.ashx' + '?t=d';
            var row = $('#dg').datagrid('getSelected');
            if (row) {
              //  $.messager.confirm('确认', '确定删除吗?', function (r) {
                if (confirm('确定删除吗?')) {
                        $.post(url, { id: row.SchDisId }, function (result) {

                            if (result == "" || result == null) {
                                $('#dg').datagrid('reload');    // reload the user data
                            } else {
                                alert(result);
                                $.messager.show({    // show error message
                                    title: 'Error',
                                    msg: result
                                });
                            }
                        });
                    }
             //   });
            }
            else {
                messageAlert('标题', '请选择要删除的行!', 'warning');
            }
        }


        function downloadTmp() {
            var url = "DownloadTemplate.ashx?t=tsd";
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
                'uploader': 'UploadTmpData.ashx?t=shdi',
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
            var url = "UploadTmpData.ashx?t=cshdi";
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
            var url = "SPSchoolDistrictInfo.ashx?t=ex" + "&name=" + encodeURIComponent($("#SchDisName1").textbox('getText'))
            + "&desp=" + $("#Description1").textbox('getText');
            window.location = url;
        }

        function clearCondition() {
            $('#SchDisName1').textbox('clear');
            $('#Description1').textbox('clear');
        }

        function querySchDis() {
            $('#dg').datagrid('load', {
                t: "q",
                name: $("#SchDisName1").textbox('getText'),
                desp: $("#Description1").textbox('getText')
            });
        }

    </script>
</asp:Content>
