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
            <td width="15%" bgcolor="F0F9FF" class="auto-style1">
                <div align="center">培训内容： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" class="auto-style1">
                <input name="description" class="easyui-textbox" style="width: 165px;" id="description">
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

    <div style="margin: 10px; width: 99%">  
        <table id="dg" title="我已参加" class="easyui-datagrid" style="width: 99%"
            url="NetForStudentInfo.ashx?t=qjoin&studentId=<%= UserId %>"
            pagination="true">
            <thead>
                <tr>
                    <th field="ID" width="0" hidden="true">编号</th>
                    <th field="Name" width="28%">班级名称</th>
                    <th field="Description" width="30%" sortable="true">培训内容</th>
                    <th field="StartDate" width="15%" sortable="true" formatter="formatterdate">开始日期</th>
                    <th field="FinishDate" width="15%" sortable="true" formatter="formatterdate">结束日期</th>
                    <th field="Point" width="10%" sortable="true">学时</th>
                </tr>
            </thead>
        </table>
    </div>


    <script type="text/javascript">

        var url = 'NetForStudentInfo.ashx';

        function clearCondition() {
            $('#TrainName1').textbox('clear');
            $('#description').textbox('clear');
        }

        function query() {
            $('#dg').datagrid('load', {
                t: "qjoin",
                studentId: "<%= UserId %>",
                name: $("#TrainName1").textbox('getText'),
                desp: $("#description").textbox('getText')
            });
        }

    </script>
</asp:Content>