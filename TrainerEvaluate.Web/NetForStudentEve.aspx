<%@ Page Title="" Language="C#" MasterPageFile="~/PageContent.Master" AutoEventWireup="true" CodeBehind="NetForStudentEve.aspx.cs" Inherits="TrainerEvaluate.Web.NetForStudentEve" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">培训班名称：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="TrainName1" class="easyui-textbox" id="TrainName1">
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">说明：</div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="Explain1" class="easyui-textbox" id="Explain1">
            </td>
        </tr>

        <tr bgcolor="#FFFFFF">
            <td colspan="4" class="gray10a" height="26" align="center">
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="query()" style="width: 90px">查询</a>
                &nbsp;&nbsp;
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-cancel" onclick="clearCondition()" style="width: 90px">清除条件</a>
            </td>
        </tr>
    </table>

    <div style="margin-top: 10px; margin-left: 20px; width: 99%">
        <table id="dg" title="即将报名" class="easyui-datagrid" style="width: 100%"
            url="NetForStudentInfo.ashx?t=qeve&studentId=<%= UserId %>"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="Guid" width="0" hidden="true">编号</th>
                    <th field="TrainName" width="20%" sortable="true">培训班名称</th>
                    <th field="explain" width="28%" sortable="true">说明</th>
                    <th field="BeginTime" width="15%" sortable="true" formatter="formatterdate">开始时间</th>
                    <th field="EndTime" width="15%" sortable="true" formatter="formatterdate">结束时间</th>
                    <th field="PersonMax" width="10%" sortable="true">人数限制</th>
                    <th field="EnterForNum" width="10%" sortable="true">已报人数</th>
                </tr>
            </thead>
        </table>
        <div id="toolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="save()">报名</a>
        </div>
    </div>

    <script type="text/javascript">

        var url = 'NetForStudentInfo.ashx';

        function save() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                var max = row.PersonMax;
                var num = row.EnterForNum;
                if (max <= num)
                {
                    alert("此培训班已经报满，请选择其他培训班进行报名。");
                    return;
                }
                
                var data = {
                    t: "teve",
                    userid: "<%= UserId %>",
                    username: "<%= UserName %>",
                    trainname: row.TrainName,
                    trainid: row.Guid
                };
                $.post(url, data, function (result) {
                    if (result == "") {
                        alert("报名成功。");
                        $('#dg').datagrid('reload');
                    }
                    else {
                        messageAlert('提示', result, 'warning');
                    }
                });
            } else {
                messageAlert('提示', '请选择要报名的行!', 'warning');
            }
        }

        function clearCondition() {
            $('#TrainName1').textbox('clear');
            $('#Explain1').textbox('clear');
        }

        function query() {
            $('#dg').datagrid('load', {
                t: "qeve",
                studentId: "<%= UserId %>",
                name: $("#TrainName1").textbox('getText'),
                desp: $("#Explain1").textbox('getText')
            });
        }

    </script>
</asp:Content>
