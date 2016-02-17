<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="TeacherManage.aspx.cs" Inherits="TrainerEvaluate.Web.TeacherManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">姓名：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="StuName11" class="easyui-textbox" id="StuName11" style="width: 165px;"/>
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">所在单位： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="School11" class="easyui-textbox" id="School11" style="width: 165px;"/>
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">性别： </div>
            </td>
            <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">  
                 <select class="easyui-combobox" name="Gender11" id="Gender11"  style="width: 165px;" data-options="url:'ComboboxGetData.ashx?t=g',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'" > 
                </select>
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">身份证号： </div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="IdentityNo11" class="easyui-textbox" id="IdentityNo11" style="width: 165px;"/>
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">职称：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a"> 
              <select class="easyui-combobox" name="Title11" id="Title11"  style="width: 165px;" data-options="url:'ComboboxGetData.ashx?t=j',method:'get',valueField:'ID',textField:'Name'">
               </select>
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25"></td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a"></td>
        </tr>
        <tr bgcolor="#FFFFFF">
            <td colspan="4" class="gray10a" height="26" align="center">
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="queryUser()" style="width: 90px">查询</a>
                &nbsp;&nbsp;
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-cancel" onclick="clearCondition()" style="width: 90px">清除条件</a>
            </td>
        </tr>
    </table>

    <div style="margin-top: 10px; margin-left: 20px; width: 99%">
        <table id="dg" title="教师信息" class="easyui-datagrid" style="width: 100%"
            url="TeacherInfo.ashx"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="TeacherId" width="0" hidden="true">编号</th>
                    <th field="TeacherName" width="18%" sortable="true">姓名</th>
                    <th field="GenderName" width="10%" sortable="true">性别</th>
                    <th field="IdentityNo" width="30%" sortable="true">身份证号</th>
                    <th field="Dept" width="25%" sortable="true">所在单位</th>
                    <th field="JobTitleName" width="15%" sortable="true">职称</th>
                </tr>
            </thead>
        </table>
        <div id="toolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newUser()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editUser()">编辑</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyUser()">删除</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="downloadTmp()">下载模板</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-upload" plain="true" onclick="uploadTmpData()">批量导入</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-redo" plain="true" onclick="exportData()">导出</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-user_business_boss" plain="true" onclick="ArchInfo()">教师档案</a>
        </div>
    </div>



    <div id="dlg" class="easyui-dialog" style="width: 500px; height: 480px; padding: 10px 20px" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons">
        <div class="ftitle">详细信息</div>
        <form id="fm" method="post">
            <div class="fitem">
                <label>姓名:</label>
                <input name="TeacherName" id="TeacherName" class="easyui-textbox" style="width:280px;" required="true">
            </div>
            <div class="fitem">
                <label>性别</label> 
                 <select class="easyui-combobox" name="Gender" id="Gender" style="width:280px;"  data-options="url:'ComboboxGetData.ashx?t=g',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'" > 
                </select>
            </div>
            <div class="fitem">
                <label>身份证号:</label>
                <input name="IdentityNo" id="IdentityNo" style="width:280px;" class="easyui-textbox">
            </div>
            <div class="fitem">
                <label>所在单位:</label>
                <input name="Dept" id="Dept" style="width:280px;" class="easyui-textbox">
            </div>
            <div class="fitem">
                <label>职称:</label> 
                  <select class="easyui-combobox" name="Title" id="Title" style="width:280px;" data-options="url:'ComboboxGetData.ashx?t=j',method:'get',valueField:'ID',textField:'Name'">
               </select>
            </div>
            <div class="fitem">
                <label>职务:</label>
                <input name="Post" id="Post" class="easyui-textbox" style="width:280px;">
            </div>
            <div class="fitem">
                <label>研究方向:</label>                
                <input name="ResearchBigName" id="ResearchBigName" class="easyui-textbox" style="width:138px;">
                <input name="Research" id="Research" class="easyui-textbox" style="width:138px;">
            </div>
            <div class="fitem">
                <label>手机号:</label>
                <input name="Mobile" id="Mobile" class="easyui-textbox" style="width:280px;">
            </div>
            <div class="fitem">
                <label>描述:</label>
                <input name="Description" id="Description" class="easyui-textbox" data-options="multiline:true" style="height: 80px;width:280px;">
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveUser()" style="width: 90px">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 90px">取消</a>
    </div>

    <div id="dlg1" class="easyui-dialog" style="width: 700px; height: 600px; padding: 10px 20px" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons1">
        <div class="ftitle" style="text-align: center">教师档案信息</div>
        <form id="fm1" method="post">
            <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1">
                <tr>
                    <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">基本信息：</div>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">姓名：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aTeacherName">张三</span>
                    </td>
                    <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">性别： </div>
                    </td>
                    <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aGender"></span>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">单位：</div>
                    </td>
                    <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aDept"></span>
                    </td>
                    <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">职务： </div>
                    </td>
                    <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aPost"></span>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">证件号：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aIdentityNo"></span>
                    </td>
                    <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">电话： </div>
                    </td>
                    <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aMobile"></span>
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">研究方向：</div>
                    </td>
                    <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                        <span id="aResearch"></span>
                    </td>
                    <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                        <div align="center">职称：</div>
                    </td>

                    <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a"><span id="aTitle"></span></td>

                </tr>
                <tr>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">描述：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <span id="aDescription"></span>
                    </td>
                    <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25"></td>
                    <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a"></td>
                </tr>
            </table>
            <div style="margin: 0px; margin-left:6px;margin-top:10px; width: 99%;">  
                <table id="dg-train" title="任教经历信息" class="easyui-datagrid" style="width: 99%;height:250px;"
                    data-options="rownumbers:true,singleSelect:false,url:'ClassInfo.ashx?t=stcp',method:'post'">
                    <thead>
                        <tr>
                            <th field="Id" width="0" hidden="true">编号</th>
                            <th field="CourseName" width="100">课程名称</th>
                            <th field="TeachPlace" width="100" sortable="true">授课地点</th>
                            <th field="ClassName" width="100" sortable="true">班级名称</th>
                            <th field="Object" width="100" sortable="true">培训对象</th>
                            <th field="Description" width="100" sortable="true">培训内容</th>
                            <th field="StartDate" width="100" sortable="true" formatter="formatterdate">开始日期</th>
                            <th field="FinishDate" width="100" sortable="true" formatter="formatterdate">结束日期</th>
                            <th field="StudentCount" width="100" sortable="true">学员人数</th>
                            <th field="Point" width="100" sortable="true">学时</th>
                            <th field="Teacher" width="100" sortable="true">项目负责人</th>
                        </tr>
                    </thead>
                </table>
            </div>
        </form>
    </div>
    <div id="dlg-buttons1">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg1').dialog('close')" style="width: 90px">关闭</a>
    </div>

    <div id="dlgUpload" class="easyui-dialog" style="width: 400px; height: 400px; padding: 10px 20px" closed="true" data-options="modal:true,top:10">
        <div class="ftitle">上传excel数据文件</div>
        <input type="file" id="upData" name="upData" />
        <div id="fileQueue"></div>
    </div>

    <script type="text/javascript">

        var url = 'TeacherInfo.ashx';
        function newUser() {
            $('#dlg').dialog('open').dialog('setTitle', '新增');
            // $('#fm').form('clear');

            $('#TeacherName').textbox("setText", "");
            $('#IdentityNo').textbox("setText", "");
            $('#Dept').textbox("setText", "");
            $('#Title').combobox("setValue", "");
            $('#Gender').combobox("setValue", "");
            $('#Post').textbox("setText", "");
            $('#Research').textbox("setText", "");
            $('#ResearchBigName').textbox("setText", "");            
            $('#Mobile').textbox("setText", "");
            $('#Description').textbox("setText", "");
             

            url = 'TeacherInfo.ashx' + '?t=n';
        }
        function editUser() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '编辑');
                //  $('#fm').form('load', row);

                $('#TeacherName').textbox("setText", row.TeacherName);
                $('#IdentityNo').textbox("setText", row.IdentityNo);
                $('#Dept').textbox("setText", row.Dept);
                $('#Title').combobox("setValue", row.Title);
                $('#Gender').combobox("setValue", row.Gender);
                $('#Post').textbox("setText", row.Post);
                $('#Research').textbox("setText", row.Research);
                $('#ResearchBigName').textbox("setText", row.ResearchBigName);
                $('#Mobile').textbox("setText", row.Mobile);
                $('#Description').textbox("setText", row.Description);
                url = 'TeacherInfo.ashx' + '?t=e&id=' + row.TeacherId;
            } else {
                messageAlert('提示', '请选择要编辑的行!', 'warning');
            }
        }

        function ArchInfo() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg1').dialog('open').dialog('setTitle', '教师档案');
                $('#aTeacherName').text(row.TeacherName);
                $('#aGender').text(row.GenderName);
                $('#aDept').text(row.Dept);
                $('#aPost').text(row.Post);
                $('#aIdentityNo').text(row.IdentityNo);
                $('#aMobile').text(row.Mobile);
                $('#aResearch').text(row.ResearchBigName + "  " + row.Research);
                $('#aTitle').text(row.JobTitleName);
                $('#aDescription').text(row.Description);

                $('#dg-train').datagrid('reload', { t: 'stcl', teacherId: row.TeacherId });
            } else {
                messageAlert('提示', '请选择要查看的行!', 'warning');
            }
        }

        function saveUser() {
            var data = {
                TeacherName: $('#TeacherName').textbox("getText"),
                Gender: $('#Gender').combobox("getValue"),
                IdentityNo: $('#IdentityNo').textbox("getText"),
                Dept: $('#Dept').textbox("getText"),
                Title: $('#Title').combobox("getValue"),
                Post: $('#Post').textbox("getText"),
                Research: $('#Research').textbox("getText"),
                ResearchBigName: $('#ResearchBigName').textbox("getText"),
                Mobile: $('#Mobile').textbox("getText"),
                Description: $('#Description').textbox("getText")
            };
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


        function destroyUser() {
            url = 'TeacherInfo.ashx' + '?t=d';
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('确认', '确定删除吗?', function (r) {
                    if (r) {
                        $.post(url, { id: row.TeacherId }, function (result) {
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
            }
            else {
                messageAlert('标题', '请选择要删除的行!', 'warning');
            }
        }


        function downloadTmp() {
            var url = "DownloadTemplate.ashx?t=tch";
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
                'uploader': 'UploadTmpData.ashx?t=tch',
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
            var url = "UploadTmpData.ashx?t=ctch";
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


        function exportData() {
            var url = "TeacherInfo.ashx?t=ex" + "&name=" + encodeURIComponent($("#StuName11").textbox('getText')) + "&sch=" + $("#School11").textbox('getText')
                + "&title=" + $("#Title11").textbox('getValue') + "&gender=" + $('#Gender11').combobox("getValue")
            + "&idno=" + $("#IdentityNo11").textbox('getText');
            window.location = url;
        }


        function clearCondition() {
            $('#StuName11').textbox('clear');
            $('#School11').textbox('clear');
            $('#Gender11').combobox('clear');
            $('#IdentityNo11').textbox('clear');
            $('#Title11').combobox('clear');
        }



        function queryUser() {
            $('#dg').datagrid('load', {
                t: "q",
                name: $("#StuName11").textbox('getText'),
                sch: $("#School11").textbox('getText'),
                gender: $("#Gender11").combobox("getValue"),
                title: $("#Title11").combobox("getValue"),
                idno: $("#IdentityNo11").textbox('getText')
            });
        }


    </script>
</asp:Content>
