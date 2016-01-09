﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="ClassManage.aspx.cs" Inherits="TrainerEvaluate.Web.ClassManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="auto-style1">
                <div align="center">班级名称：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" class="auto-style1">
                <input name="name" class="easyui-textbox" id="name">
            </td>
            <td width="15%" bgcolor="F0F9FF" class="auto-style1">
                <div align="center">培训内容： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" class="auto-style1">
                <input name="description" class="easyui-textbox" id="description">
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">培训范围：</div>
            </td>
            <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a"> 
               <select class="easyui-combobox" name="tArea" id="tArea" style="width: 165px;"    data-options="url:'ComboboxGetData.ashx?t=a',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'" > 
                </select>
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">培训类型： </div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
               <select class="easyui-combobox" name="tType" id="tType" style="width: 165px;"    data-options="url:'ComboboxGetData.ashx?t=pt',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'" > 
                </select>
            </td>
        </tr>
        <tr bgcolor="#FFFFFF">
            <td colspan="4" class="gray10a" height="26" align="middle">
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="queryUser()" style="width: 90px">查询</a>
                &nbsp;&nbsp; <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-cancel" onclick="clearCondition()" style="width: 90px">清除条件</a>
            </td>
        </tr>
    </table>
    <div style="margin: 10px; width: 99%">  
        <table id="dg" title="班级信息" class="easyui-datagrid" style="width: 99%"
            url="ClassInfo.ashx"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="ID" width="0" hidden="true">编号</th>
                    <th field="Name" width="100">班级名称</th>
                    <th field="Object" width="100" sortable="true">培训对象</th>
                    <th field="Description" width="100" sortable="true">培训内容</th>
                    <th field="StartDate" width="100" sortable="true" formatter="formatterdate">开始日期</th>
                    <th field="FinishDate" width="100" sortable="true" formatter="formatterdate">结束日期</th>
                    <th field="Students" width="100" sortable="true">学员人数</th>
                    <th field="Point" width="100" sortable="true">学时</th>
                    <th field="Teacher" width="100" sortable="true">项目负责人</th>
                </tr>
            </thead>
        </table>
        <div id="toolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newClass()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editClass()">编辑</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="DeleteClass()">删除</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="setStu()">学员设置</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="uploadTmpStuData()">导入学员</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-upload" plain="true" onclick="setTeacher()">项目负责人设置</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-upload" plain="true" onclick="setCourse()">课程设置</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="downloadTmp()">导出</a>
        </div>

        <div id="dlg" class="easyui-dialog" style="width: 500px; height: 520px; padding: 10px 20px" data-options="modal:true,top:10"
            closed="true" buttons="#dlg-buttons">
            <div class="ftitle">详细信息</div>
            <form id="fm" method="post">
                <div class="fitem">
                    <label>班级名称:</label>
                    <input name="Name" id="Name" class="easyui-textbox" style="width:280px;" required="true">
                </div>
                <div class="fitem">
                    <label>培训对象</label>
                    <input name="Object" id="Object" class="easyui-textbox" style="width:280px;">
                </div>
                <div class="fitem">
                    <label>培训内容:</label>
                    <input name="Description" id="Description" class="easyui-textbox" data-options="multiline:true" style="height: 75px;width:280px;">
                </div>
                <div class="fitem">
                    <label>开始日期:</label>
                    <input name="StartDate" id="StartDate" class="easyui-datebox" style="width:280px;"  />
                </div>
                <div class="fitem">
                    <label>结束日期:</label>
                    <input name="FinishDate" id="FinishDate" class="easyui-datebox" style="width:280px;" />
                </div>
                <div class="fitem">
                    <label>学员人数:</label>
                    <input name="Students" id="Students" class="easyui-textbox" style="width:280px;" disabled="true">
                </div>
                <div class="fitem">
                    <label>学时:</label>
                    <input name="Point" id="Point" class="easyui-textbox" style="width:280px;">
                </div>
                <div class="fitem">
                    <label>学时类型:</label> 
                <select class="easyui-combobox" name="PointType" id="PointType" style="width:280px;" data-options="url:'ComboboxGetData.ashx?t=s',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'" > 
                </select>

                </div>
                <div class="fitem">
                    <label>项目负责人:</label>
                    <input name="Teacher" id="Teacher" class="easyui-textbox" style="width:280px;" disabled="true">
                </div>
                <div class="fitem">
                    <label>培训范围:</label>  
                  <select class="easyui-combobox" name="Area" id="Area" style="width:280px;"  data-options="url:'ComboboxGetData.ashx?t=a',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'" > 
                </select>
                </div>
                <div class="fitem">
                    <label>培训级别:</label>  
                <select class="easyui-combobox" name="Level" id="Level" style="width:280px;"  data-options="url:'ComboboxGetData.ashx?t=l',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'" > 
                </select>
                </div>
                <div class="fitem">
                    <label>培训类型:</label> 
                     <select class="easyui-combobox" name="Type" id="Type" style="width:280px;"   data-options="url:'ComboboxGetData.ashx?t=pt',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'" > 
                </select>
                </div>
            </form>
        </div>
        <div id="dlg-buttons">
            <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveClass()" style="width: 90px">保存</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 90px">取消</a>
        </div>
     

        <div id="dlg1" class="easyui-dialog" style="width: 400px; height: 400px; padding: 10px 20px" data-options="modal:true,top:10"
            closed="true" buttons="#dlg-buttons1">
            <div class="ftitle">请选择学生</div> 
                <table id="dg1" class="easyui-datagrid"
                    data-options="rownumbers:true,singleSelect:false,url:'StudentsInfo.ashx',method:'post',checkOnSelect:true, pagination:true">
                    <thead>
                        <tr>
                            <th data-options="field:'ck',checkbox:true"></th>
                            <th data-options="field:'StudentId'" hidden="true">StudentId</th>
                            <th data-options="field:'StuName'">姓名</th>
                            <th data-options="field:'LastSchool'">所在单位</th>
                        </tr>
                    </thead>
                </table>
                <input type="hidden" id="hClassid" />
                <input type="hidden" id="hIsAllStu" />
                <input type="hidden" id="hStuIds" />
                <input type="hidden" id="hUnStuIds" /> 
        </div>
        <div id="dlg-buttons1">
            <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveChoseStu()" style="width: 90px">保存</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg1').dialog('close');$('#dg1').datagrid('load');" style="width: 90px">取消</a>
        </div>



        <div id="dlg2" class="easyui-dialog" style="width: 400px; height: 400px; padding: 10px 20px" data-options="modal:true,top:10"
            closed="true" buttons="#dlg-buttons2">
            <div class="ftitle">请选择项目负责人</div>
           
                <table id="dg2" class="easyui-datagrid"
                    data-options="rownumbers:true,singleSelect:false,url:'TeacherInfo.ashx',method:'post',checkOnSelect:true, pagination:true">
                    <thead>
                        <tr>
                            <th data-options="field:'ck',checkbox:true"></th>
                            <th data-options="field:'TeacherId'" hidden="true">TeacherId</th>
                            <th data-options="field:'TeacherName'">姓名</th>
                            <th data-options="field:'Dept'">所在单位</th>
                        </tr>
                    </thead>
                </table> 
                <input type="hidden" id="hIsAllTeacher" />
                <input type="hidden" id="hTeacherIds" />
                <input type="hidden" id="hUnTeacherIds" /> 
        </div>
        <div id="dlg-buttons2">
            <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveChoseTeacher()" style="width: 90px">保存</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg2').dialog('close');" style="width: 90px">取消</a>
        </div>


        <div id="dlg3" class="easyui-dialog" style="width: 400px; height: 400px; padding: 10px 20px" data-options="modal:true,top:10"
            closed="true" buttons="#dlg-buttons3">
            <div class="ftitle">请选择课程</div>
           
                <table id="dg3" class="easyui-datagrid"
                    data-options="rownumbers:true,singleSelect:false,url:'Course.ashx',method:'post',checkOnSelect:true, pagination:true">
                    <thead>
                        <tr>
                            <th data-options="field:'ck',checkbox:true"></th>
                            <th data-options="field:'CourseId'" hidden="true">CourseId</th>
                            <th data-options="field:'CourseName'">课程名称</th>
                        </tr>
                    </thead>
                </table> 
            <input type="hidden" id="hCourseIsAll" />
            <input type="hidden" id="hCourseIds" /> 
            <input type="hidden" id="hUnCourseIds" /> 
        </div>
        <div id="dlg-buttons3">
            <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveChoseCourse()" style="width: 90px">保存</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg3').dialog('close'); " style="width: 90px">取消</a>
        </div> 
    </div>

    <div id="dlgUpload" class="easyui-dialog" style="width: 400px; height: 400px; padding: 10px 20px" closed="true" data-options="modal:true,top:10">
        <div class="ftitle">上传excel数据文件</div>
        <input type="file" id="upData" name="upData" />
        <div id="fileQueue"></div>
    </div>
    
    <script type="text/javascript">
        
        var url = 'ClassInfo.ashx';
        function newClass() {
            $('#dlg').dialog('open').dialog('setTitle', '新增');
            // $('#fm').form('clear'); 

            $('#Name').textbox("setText", "");
            $('#Object').textbox("setText", "");
            $('#Description').textbox("setText", "");
            $('#StartDate').textbox("setText", "");
            $('#FinishDate').textbox("setText", "");
            $('#Students').textbox("setText", "");
            $('#Point').textbox("setText", "");
            $('#PointType').textbox("setText", "");
            $('#Teacher').textbox("setText", "");
            $('#Area').textbox("setText", "");
            $('#Level').textbox("setText", "");
            $('#Type').textbox("setText", "");


            url = 'ClassInfo.ashx' + '?t=n';
    }
    function editClass() {
        var row = $('#dg').datagrid('getSelected');
        if (row) {
            $('#dlg').dialog('open').dialog('setTitle', '编辑');
            //$('#fm').form('load', row); 

            $('#Name').textbox("setText", row.Name);
            $('#Object').textbox("setValue", row.Object);
            $('#Description').textbox("setText", row.Description);
            $('#StartDate').datebox("setValue", row.StartDate);
            $('#FinishDate').datebox("setValue", row.FinishDate);
            $('#Students').textbox("setText", row.Students);
            $('#Point').textbox("setText", row.Point);
            $('#PointType').textbox("setText", row.PointType);
            $('#Teacher').textbox("setText", row.Teacher);
            $('#Area').combobox("setValue", row.Area);
            $('#Level').combobox("setValue", row.Level);
            $('#Type').combobox("setValue", row.Type);

            url = 'ClassInfo.ashx' + '?t=e&id=' + row.ID;
        } else {
            $.messager.alert('提示', '请选择要编辑的行!', 'warning');
        }
    }

    function saveClass() {
        var data = {
            Name: $('#Name').textbox("getText"), Object: $('#Object').textbox("getText"),
            Description: $('#Description').textbox("getText"), StartDate: $('#StartDate').textbox("getText"),
            FinishDate: $('#FinishDate').textbox("getText"), Students: $('#Students').textbox("getText"),
            Point: $('#Point').textbox("getText"), PointType: $('#PointType').combobox("getValue"),
            Teacher: $('#Teacher').textbox("getText"),
            Level: $('#Level').combobox("getValue"),
            Type: $('#Type').combobox("getValue"),
            Area: $('#Area').combobox("getValue")
        };
        if (data.Name == "") {
            alert("请填写班级名称！");
            return;
        }
        $.post(url, data, function (result) {
            if (result == "") {
                $('#dlg').dialog('close');
                $('#dg').datagrid('reload');
            }
            else {
                $.messager.alert('提示', result, 'warning');
            }
        });
    }


    //function createQuestionnaire() {
    //    $('#dlg1').dialog('open').dialog('setTitle', '生成问卷');
    //}


    function DeleteClass() {
        url = 'ClassInfo.ashx' + '?t=d';
        var row = $('#dg').datagrid('getSelected');
        if (row) {
            $.messager.confirm('确认', '确定删除吗?', function (r) {
                if (r) {
                    $.post(url, { id: row.ID }, function (result) {
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
            });
        } else {
            $.messager.alert('提示', '请选择要编辑的行!', 'warning');
        }
    }

    
    function downloadTmp() {
        var url = "ClassInfo.ashx?t=ex" + "&name=" + encodeURIComponent($("#name").textbox('getText')) + "&description=" + encodeURIComponent($("#description").textbox('getText'))
            + "&area=" +  $("#tArea").combobox('getValue')+ "&type=" + $("#tType").combobox('getValue');
        window.location = url;
    }


   


    function setCourse() {
        var row = $('#dg').datagrid('getSelected');
        if (row) {
            $('#dlg3').dialog('open').dialog('setTitle', '课程设置'); 
            $("#hClassid").val(row.ID);
            $('#dg3').datagrid('reload', { t: 'gc', cId: row.ID });
        } else {
            $.messager.alert('提示', '请选择要设置的行!', 'warning');
        }

    }

    var courseids = "";
    var uncourseids = "";
    $('#dg3').datagrid({
        onSelectAll: function () {
            $("#hCourseIsAll").val("1");
        },
        onUnselectAll: function () {
            $("#hCourseIsAll").val("0");
            $("#hCourseIds").val("");
        },
        onSelect: function (index, row) {
            if (courseids == "") {
                var rows = $('#dg3').datagrid('getSelections');
                for (var i = 0; i < rows.length; i++) {
                    var row1 = rows[i];
                    if (courseids != "") {
                        courseids = courseids + "|" + row1.CourseId;
                    } else {
                        courseids = row1.CourseId;
                    }
                }
            }
            courseids = courseids + "|" + row.CourseId;
            $("#hCourseIds").val(courseids);
        },
        onUnselect: function (index, row) {
            if (uncourseids != "") {
                uncourseids = uncourseids + "|" + row.CourseId;
            } else {
                uncourseids = row.CourseId;
            }
            $("#hUnCourseIds").val(uncourseids);
        },
        onLoadSuccess: function (data) { 
            if (data) {
                $.each(data.rows, function (index, item) {
                    if (item.ck == 1) {
                        $('#dg3').datagrid('checkRow', index);
                    }
                });
            }
        }
    });

    //所选课程
    function saveChoseCourse() {
        var url = "Course.ashx?t=s";
        var data;
        if ($("#hCourseIsAll").val() == "1") {
            data = { IsAll: 1, ClassId: $("#hClassid").val() };
        } else {
            data = { CourseIds: $("#hCourseIds").val(), ClassId: $("#hClassid").val(), UnCourseIds: $("#hUnCourseIds").val() };
        }
        $.post(url, data, function (result) {
            if (result == "") {
                $('#dlg3').dialog('close');
                $('#dg3').datagrid('load');
                $('#dg').datagrid('reload');
                courseids = "";
                uncourseids = "";
            }
            else {
                $.messager.alert('提示', result, 'warning');
                $('#dlg3').dialog('close');
                $('#dg3').datagrid('load');
                $('#dg').datagrid('reload');
                courseids = "";
                uncourseids = "";
            }
        });
    }



    function setStu() {
        var row = $('#dg').datagrid('getSelected');
        if (row) {
            $('#dlg1').dialog('open').dialog('setTitle', '学员设置');
            $("#hClassid").val(row.ID);
            $('#dg1').datagrid('reload', { t: 'cs', coId: $("#hClassid").val() });
        } else {
            $.messager.alert('提示', '请选择要设置的行!', 'warning');
        }

    }
         

    var stuids = "";
    var unstuids = "";
    $('#dg1').datagrid({
        onSelectAll: function () {
            $("#hIsAllStu").val("1");
        },
        onUnselectAll: function () {
            $("#hIsAllStu").val("0");
            $("#hStuIds").val("");
        },
        onSelect: function (index, row) { 
            if (stuids == "") { 
                var rows = $('#dg1').datagrid('getSelections');
                for (var i = 0; i < rows.length; i++) {
                    var row1 = rows[i];
                    if (stuids != "") { 
                        stuids = stuids + "|" + row1.StudentId;
                    } else { 
                        stuids = row1.StudentId;
                    }
                }
            } 
            stuids = stuids + "|" + row.StudentId;
            $("#hStuIds").val(stuids);
        },
        onUnselect: function (index, row) {
            if (unstuids != "") {
                unstuids = unstuids + "|" + row.StudentId;
            } else {
                unstuids = row.StudentId;
            }
            $("#hUnStuIds").val(unstuids);
        },
        onLoadSuccess: function (data) {
            if (data) {
                $.each(data.rows, function (index, item) {
                    if (item.ck == 1) {
                        $('#dg1').datagrid('checkRow', index);
                    }
                });
            }
        }
    }); 
 
    function saveChoseStu() { 
        var data;
        if ($("#hIsAllStu").val() == "1") {
            data = { IsAll: 1, ClassId: $("#hClassid").val() };
        } else {
            data = { StuIds: $("#hStuIds").val(), ClassId: $("#hClassid").val(), UnStuIds: $("#hUnStuIds").val() };
        }
        var url = "StudentsInfo.ashx?t=sc";
        $.post(url, data, function (result) {
            if (result == "") {
                $('#dlg1').dialog('close');
                $('#dg1').datagrid('load');
                $('#dg').datagrid('reload');
                stuids = "";
                unstuids = "";
            } else {
                $.messager.alert('提示', result, 'warning');
                $('#dlg1').dialog('close');
                $('#dg1').datagrid('load');
                $('#dg').datagrid('reload');
                stuids = "";
                unstuids = "";
            }
        });  
    }


    function setTeacher() {
        var row = $('#dg').datagrid('getSelected');
        if (row) {
            $('#dlg2').dialog('open').dialog('setTitle', '项目负责人设置');
            $("#hClassid").val(row.ID);
            $('#dg2').datagrid('reload', { t: 'gt', cId: $("#hClassid").val() });
        } else {
            $.messager.alert('提示', '请选择要设置的行!', 'warning');
        }

    } 

    var teacherids = "";
    var unteacherids = "";
    $('#dg2').datagrid({
        onSelectAll: function () {
            $("#hIsAllTeacher").val("1");
        },
        onUnselectAll: function () {
            $("#hIsAllTeacher").val("0");
            $("#hTeacherIds").val("");
        },
        onSelect: function (index, row) {
            if (teacherids == "") {
                var rows = $('#dg2').datagrid('getSelections');
                for (var i = 0; i < rows.length; i++) {
                    var row1 = rows[i];
                    if (stuids != "") {
                        teacherids = teacherids + "|" + row1.TeacherId;
                    } else {
                        teacherids = row1.TeacherId;
                    }
                }
            }
            teacherids = teacherids + "|" + row.TeacherId;
            $("#hTeacherIds").val(teacherids);
        },
        onUnselect: function (index, row) {
            if (unteacherids != "") {
                unteacherids = unteacherids + "|" + row.TeacherId;
            } else {
                unteacherids = row.TeacherId;
            }
            $("#hUnTeacherIds").val(unteacherids);
        },
        onLoadSuccess: function (data) {
            if (data) { 
                $.each(data.rows, function (index, item) {
                    if (item.ck == 1) {
                        $('#dg2').datagrid('checkRow', index);
                    }
                });
            }
        }
    });


    function saveChoseTeacher() { 
        var data;
        if ($("#hIsAllTeacher").val() == "1") {
            data = { IsAll: 1, ClassId: $("#hClassid").val() };
        } else {
            data = { TeacherIds: $("#hTeacherIds").val(), ClassId: $("#hClassid").val(), UnTeacherIds: $("#hUnTeacherIds").val() };
        }
        var url = "TeacherInfo.ashx?t=ct";
        $.post(url, data, function (result) {
            if (result == "") {
                $('#dlg2').dialog('close');
                $('#dg2').datagrid('load');
                $('#dg').datagrid('reload');
                stuids = "";
                unstuids = "";
            } else {
                $.messager.alert('提示', result, 'warning');
                $('#dlg2').dialog('close');
                $('#dg2').datagrid('load');
                $('#dg').datagrid('reload');
                stuids = "";
                unstuids = "";
            }
        });
    }


    var vClassId = "";
    function uploadTmpStuData() {
        vClassId = "";
        var row = $('#dg').datagrid('getSelected');
        if (row) {
            vClassId = row.ID;
            $('#dlgUpload').dialog({
                title: '批量设置学员',
                height: '300px',
                closed: false,
                cache: false,
                modal: true
            });
            $('#dlgUpload').dialog('open');
        } else {            
            $.messager.alert('提示', '请选择要设置的行!', 'warning');
        }
    }

    $(function () {
        $('#upData').uploadify({
            'swf': 'Scripts/uploadify.swf',
            'uploader': 'UploadTmpData.ashx?t=stet&ClassId=' + vClassId,
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
                    //window.location.reload(true);
                    $('#dlgUpload').dialog('close');
                    $('#dg').datagrid('reload');
                }
            }
        });
    });


    function ifConfirmCover(filename) {
        alert(vClassId);
        var url = "UploadTmpData.ashx?t=cstet&ClassId=" + vClassId;
        var data = { "fname": filename };
        $.post(url, data, function (result) {
            if (result == "") {
                $('#dlg').dialog('close');
                $('#dg').datagrid('reload');
            }
            else {
                $.messager.alert('提示', result, 'warning');
            }
        });
    }

    function formatterdate(val, row) {
        if (val != "" && val != undefined) {
            return val.substring(0, 10);
        }
        else
       { return val;} 
    }

    function clearCondition() {
        $('#name').textbox('clear');
        $('#description').textbox('clear');
        $('#tArea').textbox('clear');;
        $('#tType').textbox('clear');
    }


    function queryUser() { 
        $('#dg').datagrid('load', {
            t: "q",
            Name: $("#name").textbox('getText'),
            Description: $("#description").textbox('getText'),
            Area: $("#tArea").combobox('getValue'),
            Type: $("#tType").textbox('getValue')
        });
    }



    function exportData() {
        var url = "class.ashx?t=ex" + "&name=" + encodeURIComponent($("#name").textbox('getText')) + "&description=" + $("#description").textbox('getText')
            + "&area=" + $("#area").textbox('getText') + "&type=" + $('#type').combobox("getValue");
        window.location = url;
    }



  

    </script>

</asp:Content>
