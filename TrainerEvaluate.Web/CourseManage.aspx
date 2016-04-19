<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="CourseManage.aspx.cs" Inherits="TrainerEvaluate.Web.CourseManage" %>

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
                <input name="corName" class="easyui-textbox" id="corName" style="width: 165px;" />
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">课程类型： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <select class="easyui-combobox" name="cType" id="cType" style="width: 138px;" data-options="url:'ComboboxGetData.ashx?t=t',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
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
    <div style="margin-top: 10px; margin-left: 20px; width: 99%">
        <table id="dg" title="课程信息" class="easyui-datagrid" style="width: 100%"
            url="Course.ashx"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="CourseId" width="0" hidden="true">编号</th> 
                    <th field="CourseName" width="30%" sortable="true">课程名称</th> 
                    <th field="TypeName" width="30%" sortable="true">课程类型</th> 
                    <th field="Description" width="35%" sortable="true">描述</th>
                </tr>
            </thead>
        </table>
        <div id="toolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newUser()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editUser()">编辑</a>
            <a id="btnDel"    href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyUser()">删除</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="downloadTmp()">下载模板</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-upload" plain="true" onclick="uploadTmpData()">批量导入</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-redo" plain="true" onclick="exportData()">导出</a>
<%--            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-user_business_boss" plain="true" onclick="setTeacher()">授课教师设置</a>--%>
        </div>

        <div id="dlg" class="easyui-dialog" style="width: 500px; height: 400px; padding: 10px 20px" data-options="modal:true,top:10"
            closed="true" buttons="#dlg-buttons">
            <div class="ftitle">详细信息</div>
            <form id="fm" method="post">
                <div class="fitem">
                    <label>课程名称:</label>
                    <input name="CourseName" id="CourseName" class="easyui-textbox" style="width:280px;"  required="true" />
                </div>
<%--                <div class="fitem">
                    <label>授课教师</label>
                    <input class="easyui-combobox" style="height: 50px;width:280px;"
                        name="CourseInfo" id="CourseInfo"
                        data-options="multiple:true,multiline:true,panelHeight:'auto',url:'TeacherInfo.ashx?t=c',method:'get',valueField:'TeacherId',textField:'TeacherName'" />
                </div>--%>
             <%--   <div class="fitem">
                    <label>授课地点:</label>
                    <input name="TeachPlace" id="TeachPlace" style="width:280px;"  class="easyui-textbox" />
                </div>--%>
<%--                <div class="fitem">
                    <label>授课时间:</label>
                    <input name="TeachTime" id="TeachTime" style="width:280px;"  class="easyui-datebox" />
                </div>--%>
                <div class="fitem">
                    <label>课程类型:</label>
                    <select class="easyui-combobox" name="Type" id="Type" style="width: 138px;" data-options="url:'ComboboxGetData.ashx?t=t',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                    </select>
                    <input name="TypeSmallName" id="TypeSmallName" style="width:138px;"  class="easyui-textbox" />
                </div>
                <div class="fitem">
                    <label>描述:</label>
                    <input name="Description" id="Description" class="easyui-textbox" data-options="multiline:true" style="height: 75px;width:280px;">
                </div>
            </form>
        </div>
        <div id="dlg-buttons">
            <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveUser()" style="width: 90px">保存</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 90px">取消</a>
        </div>
        <div id="dlgUpload" class="easyui-dialog" style="width: 400px; height: 400px; padding: 10px 20px" closed="true" data-options="modal:true,top:10">
            <div class="ftitle">上传excel数据文件</div>
            <input type="file" id="upData" name="upData" />
            <div id="fileQueue"></div>
        </div>

        <div id="dlg1" class="easyui-dialog" style="width: 400px; height: 400px; padding: 10px 20px" data-options="modal:true,top:10"
            closed="true" buttons="#dlg-buttons1">
            <div class="ftitle">请选择授课教师</div>
          
                <table id="dg1" class="easyui-datagrid"
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
                <input type="hidden" id="hCourseid" />
                <input type="hidden" id="hIsAll" />
                <input type="hidden" id="hTeaIds" /> 
                <input type="hidden" id="hUnTeaIds" />
            
        </div>
        <div id="dlg-buttons1">
            <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveChoseTeacher()" style="width: 90px">保存</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg1').dialog('close')" style="width: 90px">取消</a>
        </div>
    </div>

    <script type="text/javascript">
        function clearCondition() {
            $('#corName').textbox('clear');
            //$('#teacherName').textbox('clear');
            $('#cType').combobox('clear');
            //$('#TeachTime11').textbox("setText", "");
        }


        function queryUser() {
            $('#dg').datagrid('load', {
                t: "q",
                name: $("#corName").textbox('getText'),
                cType: $("#cType").combobox('getValue'),
                //   teaplace: $("#TeachPlace11").textbox('getText')
                teaplace: "",
                //teaTime: $("#TeachTime11").textbox('getText')
            });
        }


        function exportData() {
            var url = "Course.ashx?t=ex" + "&name=" + encodeURIComponent($("#corName").textbox('getText')) + "&teaName=" 
                + "&teaTime=&teaplace=" + $("#TeachPlace11").textbox('getText');
            window.location = url;
        }





        function setTeacher() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg1').dialog('open').dialog('setTitle', '设置授课教师');
                url = 'TeacherInfo.ashx' + '?t=s';
                $("#hCourseid").val(row.CourseId);
                $('#dg1').datagrid('reload', { t: 'g', coId: $("#hCourseid").val() });
            } else {
                messageAlert('提示', '请选择要设置的行!', 'warning');
            }
        }

        var teacherids = "";
        var unteaids = "";
        $.extend($.fn.datagrid.defaults, {
            onSelectAll: function () {
                if (this.id == "dg1") {
                    $("#hIsAll").val("1");
                }
            },
            onUnselectAll: function () {
                if (this.id == "dg1") {
                    $("#hIsAll").val("0");
                    teacherids = "";
                    $("#hTeaIds").val("");
                }
            },
            onSelect: function (index, row) {
                if (this.id == "dg1") {
                    if (teacherids == "") {
                        var rows = $('#dg1').datagrid('getSelections');
                        for (var i = 0; i < rows.length; i++) {
                            var row1 = rows[i];
                            if (teacherids != "") {
                                teacherids = teacherids + "|" + row1.TeacherId;
                            } else {
                                teacherids = row1.TeacherId;
                            }
                        }
                    }
                    teacherids = teacherids + "|" + row.TeacherId;
                    $("#hTeaIds").val(teacherids);
                    //  alert($("#hStuIds").val());
                }
            },
            onUnselect: function (index, row) {
                if (this.id == "dg1") {
                    if (unteaids != "") {
                        unteaids = unteaids + "|" + row.TeacherId;
                    } else {
                        unteaids = row.TeacherId;
                    }
                    $("#hUnTeaIds").val(unteaids);
                    //   alert($("#hUnStuIds").val());
                }
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

        function saveChoseTeacher() {
            var data;
            if ($("#hIsAll").val() == "1") {
                data = { IsAll: 1, CourseId: $("#hCourseid").val() };
            } else {
                data = { TeaIds: $("#hTeaIds").val(), CourseId: $("#hCourseid").val(), UnTeaIds: $("#hUnTeaIds").val() };
            }
            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlg1').dialog('close');
                    $('#dg').datagrid('reload');
                    teacherids = "";
                    unteaids = "";
                } else {
                    messageAlert('提示', result, 'warning');
                    $('#dlg1').dialog('close');
                    $('#dg').datagrid('reload');
                    teacherids = "";
                    unteaids = "";
                }
            });
        }


        var url = 'Course.ashx';
        function newUser() {
            $('#dlg').dialog('open').dialog('setTitle', '新增');
            $('#fm').form('clear');

            //$('#CourseName').textbox("setText", "");
            //$('#TeacherName').textbox("setText", "");
            //$('#TeachPlace').textbox("setText", "");
            //$('#TeachTime').textbox("setText", "");
            //$('#Type').textbox("setText", "");
            //$('#Description').textbox("setText", "");


            url = 'Course.ashx' + '?t=n';
        }
        function editUser() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '编辑');
                $('#CourseName').textbox("setText", row.CourseName);
                //$('#TeacherName').combobox("setText", row.TeacherName);
                $('#TeachPlace').textbox("setText", row.TeachPlace);
                //$('#TeachTime').textbox("setText", row.TeachTime);
                $('#Type').combobox("setValue", row.Type);
                $('#TypeSmallName').textbox("setText", row.TypeSmallName);                
                $('#Description').textbox("setText", row.Description);

                url = 'Course.ashx' + '?t=e&id=' + row.CourseId;
            } else {
                messageAlert('提示', '请选择要编辑的行!', 'warning');
            }
        }

        function saveUser() { 
            var data = {
                CourseName: $('#CourseName').textbox("getText"),
                TeacherName:"", //$('#CourseInfo').combobox("getText"),
                TeacherId:"", //escape($('#CourseInfo').combobox("getValues")),
               // TeachPlace: $('#TeachPlace').textbox("getText"),
                TeachPlace: "",
                TeachTime:"2016-01-10", //$('#TeachTime').textbox("getText"),
                Type: $('#Type').combobox("getValue"),
                TypeName: $('#Type').combobox("getText"),
                TypeSmallName: $('#TypeSmallName').textbox("getText"),
                Description: $('#Description').textbox("getText")
            }; 
            if (data.CourseName == "") {
                alert("请填写课程名称！");
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


        function createQuestionnaire() {
            $('#dlg1').dialog('open').dialog('setTitle', '生成问卷');
        }


        function destroyUser() {
            url = 'Course.ashx' + '?t=d';
            var row = $('#dg').datagrid('getSelected');
            if (row) {
              //  $.messager.confirm('确认', '确定删除吗?', function (r) {
                    if (confirm('确定删除吗?')) {
                        $.post(url, { id: row.CourseId }, function (result) {
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
               // });
            } else {
                messageAlert('提示', '请选择要编辑的行!', 'warning');
            }
        }



        function downloadTmp() {
            var url = "DownloadTemplate.ashx?t=cor";
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
                'uploader': 'UploadTmpData.ashx?t=cor',
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
            var url = "UploadTmpData.ashx?t=ccor";
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


        $(function () {
            if ('<%=IsDel%>' == "block") {
                      $("#btnDel").show();
                  } else {
                      $("#btnDel").hide();
                  }
              });

    </script>

</asp:Content>
