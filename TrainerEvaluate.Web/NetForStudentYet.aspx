<%@ Page Title="" Language="C#" MasterPageFile="~/PageContent.Master" AutoEventWireup="true" CodeBehind="NetForStudentYet.aspx.cs" Inherits="TrainerEvaluate.Web.NetForStudentYet" %>

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
        <table id="dg" title="我已报名" class="easyui-datagrid" style="width: 100%"
            url="NetForStudentInfo.ashx?t=qyet&studentId=<%= UserId %>"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="ID" width="0" hidden="true">编号</th>
                    <th field="Name" width="20%" sortable="true">培训班名称</th>
                    <%--      <th field="explain" width="28%" sortable="true">说明</th>--%>
                    <th field="StartDate" width="15%" sortable="true" formatter="formatterdate">开始时间</th>
                    <th field="FinishDate" width="15%" sortable="true" formatter="formatterdate">结束时间</th>
                    <th field="ReportMax" width="10%" sortable="true">人数限制</th>
                    <th field="HasReportNum" width="10%" sortable="true">已报人数</th>
                </tr>
            </thead>
        </table>
        <div id="toolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="Cancel()">取消报名</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-application" plain="true" onclick="classInfo()">班级信息</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-book_open_mark" plain="true" onclick="courseInfo()">课程信息</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-user_group" plain="true" onclick="stuInfo()">学员信息</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-book_open_mark" plain="true" onclick="downloadppts()">课件下载</a>
        </div>
    </div>
    <div id="dlg7" class="easyui-dialog" style="width: 650px; height: 510px; padding: 10px 20px" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons7">
        <div class="ftitle">详细信息</div>
        <form id="fm7" method="post">
            <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1">
                <tr>
                    <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">班级名称：</div>
                    </td>
                    <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aName"></span>
                    </td>
                    <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">培训对象： </div>
                    </td>
                    <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aObjectName"></span>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">培训类别：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aLevel"></span>
                    </td>
                    <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25"></td>
                    <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a"></td>
                </tr>
                <tr>
                    <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">培训内容： </div>
                    </td>
                    <td colspan="3" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aDescription"></span>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">开始日期：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aStartDate"></span>
                    </td>
                    <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">结束日期： </div>
                    </td>
                    <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aFinishDate"></span>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">学员人数：</div>
                    </td>
                    <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aStudents"></span>
                    </td>
                    <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">学时： </div>
                    </td>
                    <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aPoint"></span>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">学时类型：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aPointType"></span>
                    </td>
                    <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">项目负责人： </div>
                    </td>
                    <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aTeacher"></span>
                    </td>
                </tr>

                <tr>
                    <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">培训形式：</div>
                    </td>
                    <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aArea"></span>
                    </td>
                    <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">培训层次： </div>
                    </td>
                    <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aType"></span>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">是否报名班级：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aIsReport"></span>
                    </td>
                    <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">报名上限人数： </div>
                    </td>
                    <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aReportMax"></span>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">报名截止日期：</div>
                    </td>
                    <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aCloseDate"></span>
                    </td>
                    <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25"></td>
                    <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a"></td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlg-buttons7">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg7').dialog('close')" style="width: 90px">关闭</a>
    </div>
    <div id="dlg5" class="easyui-dialog" style="width: 750px; height: 480px; padding: 10px 20px" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons5">
        <table id="dg5" class="easyui-datagrid"
            data-options="rownumbers:true,singleSelect:false,url:'Course.ashx?t=dcct',method:'post',checkOnSelect:true, pagination:true">
            <thead>
                <tr>
                    <%-- <th data-options="field:'ck',checkbox:true"></th>--%>
                    <th data-options="field:'RId'" hidden="true">RId</th>
                    <th width="35%" data-options="field:'CoursName'">课程名称</th>
                    <th width="15%" data-options="field:'TeacherName'">教师姓名</th>
                    <th width="18%" data-options="field:'teacherplace'">授课地点</th>
                    <th field="StartDate" width="18%" sortable="true" formatter="formatterdatemore">开始时间</th>
                    <th field="FinishDate" width="18%" sortable="true" formatter="formatterdatemore">结束时间</th>
                </tr>
            </thead>
        </table>
        <input type="hidden" id="hcctIsAll" />
        <input type="hidden" id="hcctIds" />
        <input type="hidden" id="hUncctIds" />
    </div>
    <div id="dlg-buttons5">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg5').dialog('close');" style="width: 90px">关闭</a>
    </div>

    <div id="dlg1" class="easyui-dialog" style="width: 630px; height: 500px; padding: 10px 20px" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons1">
        <table id="dg1" class="easyui-datagrid"
            data-options="rownumbers:true,singleSelect:false,url:'StudentsInfo.ashx',method:'post',checkOnSelect:true, pagination:true">
            <thead>
                <tr>
                    <%--      <th data-options="field:'ck',checkbox:true"></th>--%>
                    <th data-options="field:'StudentId'" hidden="true">StudentId</th>
                    <th data-options="field:'StuName'" width="18%">姓名</th>
                    <th data-options="field:'GenderName'" width="24%">性别</th>
                    <th data-options="field:'School'" width="35%">所在学校</th>
                    <th data-options="field:'Post'" width="16%">职务</th>
                </tr>
            </thead>
        </table>
        <input type="hidden" id="hClassid" />
        <input type="hidden" id="hIsAllStu" />
        <input type="hidden" id="hStuIds" />
        <input type="hidden" id="hUnStuIds" />
    </div>
    <div id="dlg-buttons1">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg1').dialog('close');$('#dg1').datagrid('load');" style="width: 90px">关闭</a>
    </div>


    <div id="dlgUploadppt" class="easyui-dialog" style="width: 600px; height: 400px; padding: 10px 20px" closed="true" data-options="modal:true,top:10">
        <table id="dgppts" title="课件列表" class="easyui-datagrid" style="width: 100%"
            url="ClassInfo.ashx?t=ppt"
            toolbar="#toolbarppts" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="Id" width="0" hidden="true">编号</th>
                    <th field="Name" width="60%">课件名称</th>
                    <th field="CreateTime" width="25%" sortable="true" formatter="formatterdate">上传时间</th>
                </tr>
            </thead>
        </table>
        <div id="toolbarppts">
            <a id="btndownppt" href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="downloadppt()">下载</a>
            <a id="btndownppts" href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="downloadpptall()">全部下载</a>
        </div>
    </div>

    <script type="text/javascript"> 

        function formatterdatemore(val, row) {
            if (val != "" && val != undefined) {
                return val.substring(0, 10) + " " + val.substring(11, 19);
            }
            else { return val; }
        }

        var url = 'NetForStudentInfo.ashx';

        function Cancel() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                var data = {
                    t: "tyet",
                    userid: "<%= UserId %>",
                    classid: row.ID
                };
                $.post(url, data, function (result) {
                    if (result == "") {
                        alert("取消报名成功。");
                        $('#dg').datagrid('reload');
                    }
                    else {
                        messageAlert('提示', result, 'warning');
                    }
                });
            } else {
                messageAlert('提示', '请选择要取消报名的行!', 'warning');
            }
        }

        function clearCondition() {
            $('#TrainName1').textbox('clear');
            $('#Explain1').textbox('clear');
        }

        function query() {
            $('#dg').datagrid('load', {
                t: "qyet",
                studentId: "<%= UserId %>",
                name: $("#TrainName1").textbox('getText'),
                desp: $("#Explain1").textbox('getText')
            });
        }

        function classInfo() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg7').dialog('open').dialog('setTitle', '班级信息');
                $('#aName').text(row.Name);
                $('#aObjectName').text(row.ObjectName);
                $('#aDescription').text(row.Description);
                $('#aStartDate').text(StringToDate(row.StartDate));
                $('#aFinishDate').text(StringToDate(row.FinishDate));
                $('#aStudents').text(row.Students);
                $('#aPoint').text(row.Point);
                $('#aPointType').text(row.PointType);
                $('#aTeacher').text(row.Teacher);
                $('#aArea').text(row.AreaName);
                $('#aLevel').text(row.levelname);
                $('#aType').text(row.Type);
                $('#aReportMax').text(row.ReportMax);
                $('#aCloseDate').text(StringToDate(row.CloseDate));
                if (row.IsReport == 1) {
                    $('#aIsReport').text("是");
                }
                else {
                    $('#aIsReport').text("否");
                }
            } else {
                messageAlert('提示', '请选择要查看的行!', 'warning');
            }
        }


        function courseInfo() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg5').dialog('open').dialog('setTitle', '查看课程设置');
                $("#hClassid").val(row.ID);
                $('#dg5').datagrid('reload', { t: 'dcct', cId: $("#hClassid").val() });
            } else {
                messageAlert('提示', '请选择要查看的行!', 'warning');
            }
        }

        function stuInfo() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg1').dialog('open').dialog('setTitle', '学员信息');

                $("#hClassid").val(row.ID);
                $('#dg1').datagrid('reload', { t: 'cs1', coId: $("#hClassid").val() });
            } else {
                messageAlert('提示', '请选择要设置的行!', 'warning');
            }
        }

        var cid;
        function downloadppts() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlgUploadppt').dialog({
                    title: row.Name + '-- 课件信息',
                    closed: false,
                    cache: false,
                    modal: true
                });
                cid = row.ID;
                $('#dgppts').datagrid('reload', { t: 'ppt', cId: row.ID });
                $('#dlgUploadppt').dialog('open');
            } else {
                messageAlert('提示', '请选择课件所属班级!', 'warning');
            }

        }

        function downloadppt() {
            var row = $('#dgppts').datagrid('getSelected');
            if (row) {
                var url = "ClassInfo.ashx?t=downppt" + "&id=" + row.Id;
                window.location = url;
            } else {
                messageAlert('提示', '请选择要下载的课件!', 'warning');
            }
        }

        function downloadpptall() {
            var rows = $('#dgppts').datagrid('getRows');
            if (rows != null && rows.length > 0) {
                var url = "ClassInfo.ashx?t=downpptall" + "&cid=" + cid;
                window.location = url;
            }
        }

    </script>
</asp:Content>
