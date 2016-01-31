<%@ Page Title="" Language="C#" MasterPageFile="~/PageContent.Master" AutoEventWireup="true" CodeBehind="QuestionnairManageShow.aspx.cs" Inherits="TrainerEvaluate.Web.QuestionnairManageShow" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">班级名称：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="tClassName" class="easyui-textbox" id="tClassName" style="width: 165px;"/>
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">问卷状态： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <select class="easyui-combobox" name="tStatus" id="tStatus" style="width: 165px;">
                    <option value="0"></option>
                    <option value="1">未生成</option>
                    <option value="2">已生成</option>
                    <option value="3">已取消</option>
                </select>
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">课程名称：</div>
            </td>
            <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="tCourseName" class="easyui-textbox" id="tCourseName" style="width: 165px;"/>
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">授课教师： </div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="tTeacher" class="easyui-textbox" id="tTeacher" style="width: 165px;"/>
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">授课地点：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="tPlace" class="easyui-textbox" id="tPlace" style="width: 165px;"/>
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center"> </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">                
            </td>
        </tr>
        <tr bgcolor="#FFFFFF">
            <td colspan="4" class="gray10a" height="26" align="middle">
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="queryUser()" style="width: 90px">查询</a>
                &nbsp;&nbsp; 
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-cancel" onclick="clearCondition()" style="width: 90px">清除条件</a>
            </td>
        </tr>
    </table>
    <div style="margin-top: 10px; margin-left: 20px; width: 98%">
        <table id="dg" title="问卷信息" class="easyui-datagrid" style="width: 100%"
            url="QuestionnaireHadlerNew.ashx"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="ID" width="0" sortable="true" hidden="true">班级课程老师编号</th>
                    <th field="CourseID" width="0" contenteditable="" hidden="true">课程编号</th>
                    <th field="TeacherID" width="0" hidden="true">教师编号</th>
                    <th field="ClassName" width="25%" sortable="true">班级名称</th>
                    <th field="CourseName" width="20%" sortable="true">课程名称</th>
                    <th field="TeacherName" width="10%" sortable="true">授课教师</th>
                    <th field="TeachPlace" width="14%" sortable="true">授课地点</th>
                    <th field="TeachTime" width="10%" sortable="true">授课开始时间</th>
                    <th field="TeachFinishDate" width="10%" sortable="true">授课结束时间</th>
                    <th field="QuestionInfoStatus" width="10%" sortable="true">问卷状态</th>
                </tr>
            </thead>
        </table>
        <div id="toolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newUser()">生成问卷</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editUser()">编辑调查时间</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyUser()">取消问卷</a>
        </div>
    </div>
      
     <div id="dlg" class="easyui-dialog" style="width: 450px; height: 380px; padding: 10px 20px" data-options="top:10"
        closed="true" buttons="#dlg-buttons" modal="true">
        <div class="ftitle">详细信息</div>
        <form id="fm1" method="post">
            <div class="fitem">
                <label>问卷名称:</label>
                <input name="Parameter" id="Parameter" class="easyui-textbox" required="true" style="width:280px;" />
            </div> 
            <div class="fitem">
                <label>调查开始时间:</label>
                <input name="StartTime" id="StartTime" class="easyui-datebox" style="width:280px;" />
            </div>
            <div class="fitem">
                <label>调查结束时间:</label>
                <input name="EndTime" id="EndTime" class="easyui-datebox" style="width:280px;"/>
            </div>  
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveQueInfo()" style="width: 90px">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 90px">取消</a>
    </div>



    <script type="text/javascript">
        var quid = "";
        var url = "";
        function queryUser() {
            $('#dg').datagrid('reload', {
                t: "q",
                ClassName: $("#tClassName").textbox('getText'),
                Status: $("#tStatus").combobox('getText'),
                CourseName: $('#tCourseName').textbox("getText"),
                Teacher: $("#tTeacher").textbox('getText'),
                Place: $("#tPlace").textbox('getText')
            });
        }

        function clearCondition() {
            $('#tClassName').textbox('clear');
            $('#tStatus').combobox('clear');
            $('#tCourseName').textbox('clear');
            $('#tTeacher').textbox('clear');
            $('#tPlace').textbox('clear');
        }

        function newUser() {
            url = 'QuestionnaireHadlerNew.ashx' + '?t=s';
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '生成问卷');
                $('#fm1').form('clear');
                $("#Parameter").textbox("setText", row.CourseName + "--调查问卷");
                quid = row.ID;
            } else {
                messageAlert('提示', '请选择课程', 'warning');
            }
        }


        function editUser() {
            url = 'QuestionnaireHadlerNew.ashx' + '?t=e';
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '编辑调查时间');
                if (row.Name == "") {
                    $("#Parameter").textbox("setText", row.CourseName + "--调查问卷");
                } else {
                    $("#Parameter").textbox("setText", row.Name);
                }
                $("#StartTime").datebox("setValue", row.StartTime);
                $("#EndTime").datebox("setValue", row.EndTime);
                quid = row.ID;
            } else {
                messageAlert('提示', '请选择课程', 'warning');
            }


        }


        function saveQueInfo() {
            $.messager.confirm('确认', '确定生成问卷吗?', function (r) {
                if (r) {
                    $.post(url, {
                        id: quid,
                        StartTime: $("#StartTime").datebox('getValue'),
                        EndTime: $("#EndTime").datebox('getValue'),
                        Name: $("#Parameter").textbox('getText')
                    }, function (result) {
                        if (result == "" || result == null) {
                            $('#dlg').dialog('close');
                            $('#dg').datagrid('reload'); // reload the user data
                        } else {
                            messageAlert('提示', result, 'warning');
                        }
                    });
                }
            });
        }


        function destroyUser() {
            var url = 'QuestionnaireHadlerNew.ashx' + '?t=c';
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('确认', '确定删除该问卷吗?', function (r) {
                    if (r) {
                        $.post(url, { id: row.ID }, function (result) {
                            if (result == "" || result == null) {
                                $('#dg').datagrid('reload'); // reload the user data
                            } else {
                                messageAlert('提示', result, 'warning');
                            }
                        });
                    }
                });
            } else {
                messageAlert('提示', '请选择课程', 'warning');
            }
        }


        function ExportUser() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                var url = "QuestionnaireHadler.ashx?t=ex&id=" + row.CourseId;
                window.location = url;
            } else {
                messageAlert('提示', '请选择课程', 'warning');
            }
        }


    </script>
</asp:Content>
