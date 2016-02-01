<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="MyQuestionnaireResult.aspx.cs" Inherits="TrainerEvaluate.Web.MyQuestionnaireResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">课程名称：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="corName11" class="easyui-textbox" id="corName11" style="width: 165px;"/>
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">授课老师： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="Teacher11" class="easyui-textbox" id="Teacher11" style="width: 165px;"/>
            </td>
        </tr>
        <tr bgcolor="#FFFFFF">
            <td colspan="4" class="gray10a" height="26" align="middle">
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="queryQues()" style="width: 90px">查询</a>
                &nbsp;&nbsp; <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-cancel" onclick="clearCondition()" style="width: 90px">清除条件</a>
            </td>
        </tr>
    </table>
    <div style="margin: 10px; width: 99%">  
        <table id="dg" title="评估结果" class="easyui-datagrid" style="width: 99%"
            url="MyQuestionnaire.ashx?t=mqr&studentId=<%=UserId %>"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="id" width="0" hidden="true">编号</th>
                    <th field="ClassName" width="35%">班级名称</th>
                    <th field="CourseName" width="40%" sortable="true">课程名称</th>
                    <th field="TeacherName" width="20%" sortable="true">授课老师</th>
                </tr>
            </thead>
        </table>
        <div id="toolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-user" plain="true" onclick="showResult()">查看评估结果</a>
        </div>
    </div>
    <div id="dlg1" class="easyui-dialog" style="width: 850px; height: 600px; padding: 10px 20px;overflow-x:hidden;" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons1">
        <div class="ftitle" style="text-align: center">评估结果信息</div>        
        <div id="resultqn" name="resultqn"></div>
    </div>
    <div id="dlg-buttons1">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg1').dialog('close')" style="width: 90px">关闭</a>
    </div>
    <script type="text/javascript">
        function clearCondition() {
            $('#corName11').textbox('clear');
            $('#Teacher11').textbox('clear');
        }


        function queryQues() {
            $('#dg').datagrid('load', {
                qt: "q",
                corName: $("#corName11").textbox('getText'),
                teacherName: $("#Teacher11").textbox('getText')
            });
        }

        function showResult() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg1').dialog('open').dialog('setTitle', '评估信息');
                var url = 'MyQuestionnaire.ashx' + '?t=shrt&id=' + row.id+'&userid=<%=UserId %>';
                var data = {
                    CourseName: ""
                };
                $.post(url, data, function (result) {
                    $("#resultqn").html(result);
                });
            } else {
                messageAlert('提示', '请选择评估结果的行!', 'warning');
            }
        }
    </script>
</asp:Content>
