<%@ Page Title="" Language="C#" MasterPageFile="~/PageContent.Master" AutoEventWireup="true" CodeBehind="NetForStudentGoOn.aspx.cs" Inherits="TrainerEvaluate.Web.NetForStudentGoOn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">培训类别：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
               <select class="easyui-combobox" name="Level" id="Level" style="width: 280px;"  required="true" data-options="url:'ComboboxGetData.ashx?t=obj',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
               </select>
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
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
            url="NetForStudentInfo.ashx?t=going&uid='<%= UserId %>'"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="train" width="20%" sortable="true">培训对象</th>
                </tr>
            </thead>
        </table>
        <div id="toolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="RedirctReport()">报名</a>
        </div>
    </div>

    <script type="text/javascript">

        var url = 'NetForStudentInfo.ashx';

        function clearCondition() {
            $('#Level').textbox('clear');
        }

        function query() {
            $('#dg').datagrid('load', {
                t: "going",
                name: $("#Level").combobox('getValue')
            });
        }

        function RedirctReport()
        {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                var name = row.train;
                window.location.href = "NetForStudentReport.aspx?train=" + name;
                
            } else {
                messageAlert('提示', '请选择要报名的行!', 'warning');
            }
        }

    </script>
</asp:Content>
