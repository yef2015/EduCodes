<%@ Page Title="" Language="C#" MasterPageFile="~/PageContent.Master" AutoEventWireup="true" CodeBehind="NetForStudentJoin.aspx.cs" Inherits="TrainerEvaluate.Web.NetForStudentJoin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">班级名称：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="TrainName1" class="easyui-textbox" id="TrainName1">
            </td>
        </tr>

        <tr bgcolor="#FFFFFF">
            <td colspan="2" class="gray10a" height="26" align="center">
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="query()" style="width: 90px">查询</a>
                &nbsp;&nbsp;
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-cancel" onclick="clearCondition()" style="width: 90px">清除条件</a>
            </td>
        </tr>
    </table>

    <div style="margin-top: 10px; margin-left: 20px; width: 99%">
        <table id="dg" title="我已参加" class="easyui-datagrid" style="width: 100%"
            url="NetForAdminInfo.ashx"
            pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="Guid" width="0" hidden="true">编号</th>
                    <th field="TrainName" width="48%" sortable="true">班级名称</th>
                    <th field="BeginTime" width="25%" sortable="true" formatter="formatterdate">开始时间</th>
                    <th field="EndTime" width="25%" sortable="true" formatter="formatterdate">结束时间</th>
                </tr>
            </thead>
        </table>
    </div>

    <div id="dlg" class="easyui-dialog" style="width: 550px; height: 380px; padding: 10px 20px" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons">
        <div class="ftitle">报名信息</div>
        <form id="fm" method="post">
            <div class="fitem">
                <label>培训班名称：</label>
                <input name="TrainName" id="TrainName" class="easyui-textbox" style="width:320px;" required="true">
            </div>
            <div class="fitem">
                <label>开始时间:</label>
                <input name="BeginTime" id="BeginTime" class="easyui-datebox" style="width:320px;"  />
            </div>
            <div class="fitem">
                <label>结束时间:</label>
                <input name="EndTime" id="EndTime" class="easyui-datebox" style="width:320px;" />
            </div>
            <div class="fitem">
                <label>人数限制:</label>
                <input name="PersonMax" id="PersonMax" class="easyui-textbox" style="width:320px;"  />
            </div>
            <div class="fitem">
                <label>说明：</label>
                <input name="Explain" id="Explain" class="easyui-textbox" data-options="multiline:true" style="height: 100px;width:320px;">
            </div>            
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="save()" style="width: 90px">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 90px">取消</a>
    </div>

    <script type="text/javascript">

        var url = 'NetForAdminInfo.ashx';
        function newInfo() {
            $('#dlg').dialog('open').dialog('setTitle', '新增');

            $('#TrainName').textbox("setText", "");
            $('#Explain').textbox("setText", "");
            $('#BeginTime').textbox("setText", "");
            $('#EndTime').textbox("setText", "");
            $('#PersonMax').textbox("setText", "");

            url = 'NetForAdminInfo.ashx' + '?t=n';
        }
        function edit() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '编辑');
                //  $('#fm').form('load', row);

                $('#TrainName').textbox("setText", row.TrainName);
                $('#Explain').textbox("setText", row.explain);
                $('#BeginTime').datebox("setValue", row.BeginTime);
                $('#EndTime').datebox("setValue", row.EndTime);
                $('#PersonMax').textbox("setText", row.PersonMax);

                url = 'NetForAdminInfo.ashx' + '?t=e&id=' + row.Guid;
            } else {
                messageAlert('提示', '请选择要编辑的行!', 'warning');
            }
        }

        function save() {
            var data = {
                TrainName: $('#TrainName').textbox("getText"),
                Explain: $('#Explain').textbox("getText"),
                BeginTime: $('#BeginTime').textbox("getText"),
                EndTime: $('#EndTime').textbox("getText"),
                PersonMax: $('#PersonMax').textbox("getText")
            };
            if (data.TrainName == "") {
                alert("请填写培训班名称！");
                return;
            }
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

        function destroy() {
            url = 'NetForAdminInfo.ashx' + '?t=d';
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                if (confirm('确定删除吗?')) {
                    $.post(url, { id: row.Guid }, function (result) {

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
            }
            else {
                messageAlert('标题', '请选择要删除的行!', 'warning');
            }
        }

        function clearCondition() {
            $('#TrainName1').textbox('clear');
        }

        function query() {
            $('#dg').datagrid('load', {
                t: "q",
                trainName: $("#TrainName1").textbox('getText')
            });
        }

    </script>
</asp:Content>