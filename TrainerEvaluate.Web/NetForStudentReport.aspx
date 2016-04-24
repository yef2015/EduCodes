<%@ Page Title="" Language="C#" MasterPageFile="~/PageContent.Master" AutoEventWireup="true" CodeBehind="NetForStudentReport.aspx.cs" Inherits="TrainerEvaluate.Web.NetForStudentReport" %>
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
        <table id="dg" title="我要报名" class="easyui-datagrid" style="width: 100%"
            url="NetForStudentInfo.ashx?t=goclass&train=<%= Train %>&uid=<%= UserId %>"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="ID" width="0" hidden="true">编号</th>
                    <th field="Name" width="20%">班级名称</th>
                    <th field="Description" width="25%" sortable="true">培训内容</th>
                    <th field="StartDate" width="10%" sortable="true" formatter="formatterdate">开始日期</th>
                    <th field="FinishDate" width="10%" sortable="true" formatter="formatterdate">结束日期</th>
                    <th field="Point" width="10%" sortable="true">学时</th>
                    <th field="ReportMax" width="10%" sortable="true">班级上限人数</th>
                    <th field="HasReportNum" width="10%" sortable="true">已报名人数</th>
                    <th field="LeftNum" width="10%" sortable="true">剩余报名名额</th>
                    <th field="CloseDate" width="12%" sortable="true" formatter="formatterdate">报名截止日期</th>
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
                if (max <= num) {
                    alert("此培训班已经报满，请选择其他培训班进行报名。");
                    return;
                } 
                window.location.href = "NetForStudentReportDetail.aspx?cid="+row.ID; 

              <%--  var data = {
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
                });--%>
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
