<%@ Page Title="" Language="C#" MasterPageFile="~/PageContent.Master" AutoEventWireup="true" CodeBehind="ClassManageList.aspx.cs" Inherits="TrainerEvaluate.Web.ClassManageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="auto-style1">
                <div align="center">班级名称：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" class="auto-style1">
                <input name="name" class="easyui-textbox" style="width: 165px;" id="name">
            </td>
            <td width="15%" bgcolor="F0F9FF" class="auto-style1">
                <div align="center">培训内容： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" class="auto-style1">
                <input name="description" class="easyui-textbox" style="width: 165px;" id="description">
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">培训类别：</div>
            </td>
            <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
              <%--  <select class="easyui-combobox" name="tArea" id="tArea" style="width: 165px;" data-options="url:'ComboboxGetData.ashx?t=a',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                </select>--%>
                  <select class="easyui-combobox" name="tLevel" id="tLevel" style="width: 165px;"    data-options="url:'ComboboxGetData.ashx?t=obj',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                  </select>
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">培训层次： </div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <select class="easyui-combobox" name="tType" id="tType" style="width: 165px;" data-options="url:'ComboboxGetData.ashx?t=pt',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
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
    <div style="margin-top: 10px; margin-left: 20px; width: 98%">
        <table id="dg" title="<%=yearLevel %> 班级信息" class="easyui-datagrid" style="width: 100%"
            url="ClassInfo.ashx"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="ID" width="0" hidden="true">编号</th>
                    <th field="Name" width="100">班级名称</th>
                    <th field="levelname" width="100" sortable="true">培训类别</th>
                    <th field="Description" width="100" sortable="true">培训内容</th>
                    <th field="StartDate" width="100" sortable="true" formatter="formatterdate">开始日期</th>
                    <th field="FinishDate" width="100" sortable="true" formatter="formatterdate">结束日期</th>
                    <th field="Students" width="100" sortable="true">学员人数</th>
                    <th field="HasReportNum" width="100" sortable="true">已报名人数</th>
                    <th field="Point" width="100" sortable="true">学时</th>
                    <th field="Teacher" width="100" sortable="true">项目负责人</th>
                </tr>
            </thead>
        </table>
        <div id="toolbar"> 
            <a id="btnNew" href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newClass()">新增</a>
            <a id="btnEdit" href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editClass()">编辑</a>
            <a id="btnDel" href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="DeleteClass()">删除</a>
            <a id="btnStu" href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-user" plain="true" onclick="setStu()">学员设置</a>
            <a id="btnExstu" href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-user_group" plain="true" onclick="uploadTmpStuData()">导入学员</a>
            <a id="btnCharge" href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-user_business_boss" plain="true" onclick="setTeacher()">项目负责人设置</a>
            <a id="btnExclass" href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="downloadTmp()">批量导出班级信息</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-book_open_mark" plain="true" onclick="setCourseNew()">课程设置</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-book_open_mark" plain="true" onclick="showCourseNew()">查看课程设置</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="downloadDetailTmp()">导出班级详细信息</a> 
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="disInfo()">查看班级信息</a> 
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-upload" plain="true" onclick="uploadDatappt()">课件信息</a> 
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="homeworkinfo()">学员作业</a> 
        </div>

        <div id="dlg" class="easyui-dialog" style="width: 550px; height: 510px; padding: 10px 20px" data-options="modal:true,top:10"
            closed="true" buttons="#dlg-buttons">
            <div class="ftitle">详细信息</div>
            <form id="fm" method="post">
                <div class="fitem">
                    <label>班级名称:</label>
                    <input name="Name" id="Name" class="easyui-textbox" style="width: 280px;" required="true">
                </div>
                <div class="fitem">
                    <label>培训对象:</label> 
                     <input name="ObjectName" id="ObjectName" class="easyui-textbox" style="width: 280px;" required="true">
                </div>
                  <div class="fitem">
                    <label>培训类别:</label>
                    <select class="easyui-combobox" name="Level" id="Level" style="width: 280px;"  required="true" data-options="url:'ComboboxGetData.ashx?t=obj',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                    </select>
                </div>
                <div class="fitem">
                    <label>培训内容:</label>
                    <input name="Description" id="Description" class="easyui-textbox" data-options="multiline:true" style="height: 45px; width: 280px;" />
                </div>
                <div class="fitem">
                    <label>开始日期:</label>
                    <input name="StartDate" id="StartDate" class="easyui-datebox" style="width: 280px;" />
                </div>
                <div class="fitem">
                    <label>结束日期:</label>
                    <input name="FinishDate" id="FinishDate" class="easyui-datebox" style="width: 280px;" />
                </div>
                <div class="fitem">
                    <label>学员人数:</label>
                    <input name="Students" id="Students" class="easyui-textbox" style="width: 280px;" disabled="true" />
                </div>
                <div class="fitem">
                    <label>学时:</label>
                    <input name="Point" id="Point" class="easyui-textbox" style="width: 280px;">
                </div>
                <div class="fitem">
                    <label>学时类型:</label>
                    <select class="easyui-combobox" name="PointType" id="PointType" style="width: 280px;" data-options="url:'ComboboxGetData.ashx?t=s',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                    </select> 
                </div>
                <div class="fitem">
                    <label>项目负责人:</label>
                    <input name="Teacher" id="Teacher" class="easyui-textbox" style="width: 280px;" disabled="true">
                </div>
                <div class="fitem">
                    <label>培训形式:</label>
                    <select class="easyui-combobox" name="Area" id="Area" style="width: 280px;" data-options="url:'ComboboxGetData.ashx?t=a',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto',multiple:true">
                    </select>
                </div> 
                <div class="fitem">
                    <label>培训层次:</label>
                    <select class="easyui-combobox" name="Type" id="Type" style="width: 280px;" data-options="url:'ComboboxGetData.ashx?t=pt',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                    </select>
                </div>
                
                <div class="fitem">
                    <label>是否报名班级:</label>
                    <input type="checkbox" name="chkIsReport" id="chkIsReport" style="width:15px;" value="yes" />
                </div>
                <div class="fitem">
                    <label>报名上限人数:</label>
                    <input name="ReportMax" id="ReportMax" class="easyui-textbox" style="width: 280px;">
                </div>
                <div class="fitem">
                    <label>报名截止日期:</label>
                    <input name="CloseDate" id="CloseDate" class="easyui-datebox" style="width: 280px;" />
                </div>
            </form>
        </div>
        <div id="dlg-buttons">
            <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveClass()" style="width: 90px">保存</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 90px">取消</a>
        </div>

        <div id="dlg1" class="easyui-dialog" style="width: 630px; height: 560px; padding: 10px 20px" data-options="modal:true,top:10"
            closed="true" buttons="#dlg-buttons1">
            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin-bottom:10px;">
                <tr>
                    <td width="16%" bgcolor="F0F9FF" class="auto-style1">
                        <div align="center">学校名称：</div>
                    </td>
                    <td width="35%" bgcolor="F0F9FF" class="auto-style1">
                        <input name="SchoolName11" id="SchoolName11" class="easyui-textbox" style="width: 165px;">
                    </td>
                    <td width="15%" bgcolor="F0F9FF" class="auto-style1">
                        <div align="center">姓名： </div>
                    </td>
                    <td width="34%" bgcolor="F0F9FF" class="auto-style1">
                        <input name="StudentName11" id="StudentName11" class="easyui-textbox" style="width: 165px;">
                    </td>
                </tr>
                <tr>
                    <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">证件号：</div>
                    </td>
                    <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <input name="CardCode11" id="CardCode11" class="easyui-textbox" style="width: 165px;">
                    </td>
                    <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                        <div align="center">继教号： </div>
                    </td>
                    <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                        <input name="JijiaoCode11" id="JijiaoCode11" class="easyui-textbox" style="width: 165px;">
                    </td>
                </tr>
                <tr bgcolor="#FFFFFF"> 
                    <td colspan="4" class="gray10a" height="26" align="middle">
                        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="queryStudent()" style="width: 90px">查询</a>
                    </td>
                </tr>
            </table>
            <table id="dg1" class="easyui-datagrid"
                data-options="rownumbers:true,singleSelect:false,url:'StudentsInfo.ashx',method:'post',checkOnSelect:true, pagination:true">
                <thead>
                    <tr>
                        <th data-options="field:'ck',checkbox:true"></th>
                        <th data-options="field:'StudentId'" hidden="true">StudentId</th>
                        <th data-options="field:'StuName'" width="18%">姓名</th>
                        <th data-options="field:'School'" width="35%">所在学校</th>
                        <th data-options="field:'IdentityNo'" width="24%">证件号</th>
                        <th data-options="field:'TeachNo'" width="16%">继教号</th>
                    </tr>
                </thead>
            </table>
            <input type="hidden" id="hClassid" />
            <input type="hidden" id="hIsAllStu" />
            <input type="hidden" id="hStuIds" />
            <input type="hidden" id="hUnStuIds" />
        </div>
        <div id="dlg-buttons1">
             全选<input type="radio" name="IschkAll"  />        
             清空<input type="radio" name="IschkAll"  /> 
            <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveChoseStu()" style="width: 90px">保存</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg1').dialog('close');$('#dg1').datagrid('load');" style="width: 90px">取消</a>
        </div>

        <div id="dlg2" class="easyui-dialog" style="width: 400px; height: 470px; padding: 10px 20px" data-options="modal:true,top:10"
            closed="true" buttons="#dlg-buttons2">
            <div class="ftitle">请选择项目负责人</div>

            <table id="dg2" class="easyui-datagrid"
                data-options="rownumbers:true,singleSelect:false,url:'TeacherInfo.ashx',method:'post',checkOnSelect:true, pagination:true">
                <thead>
                    <tr>
                        <th data-options="field:'ck',checkbox:true"></th>
                        <th data-options="field:'UserId'" hidden="true">UserId</th>
                        <th data-options="field:'UserName'">姓名</th>
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

        <div id="dlg4" class="easyui-dialog" style="width: 450px; height: 350px; padding: 10px 20px" data-options="modal:true,top:10"
            closed="true" buttons="#dlg-buttons4">
            <div class="ftitle">设置课程信息</div>
            <form id="fmCourse" method="post">
                <div class="fitem">
                    <label>选择课程:</label>
                    <select class="easyui-combobox" name="CusCourse" id="CusCourse" style="width: 260px;" data-options="url:'ComboxGetDropData.ashx?t=ccus',method:'post',valueField:'ID',textField:'Name'">
                    </select>
                </div>
                 <div class="fitem">
                    <label>授课地点:</label>
                    <input name="TeachPlace" id="TeachPlace" style="width:260px;"  class="easyui-textbox" />
                </div>
                <div class="fitem">
                    <label>授课老师:</label>
                    <select class="easyui-combobox" name="CusTeacher" id="CusTeacher" style="width: 260px;" data-options="url:'ComboxGetDropData.ashx?t=ctea',method:'post',valueField:'ID',textField:'Name'">
                    </select>
                </div>
                <div class="fitem">
                    <label>开始时间:</label>
                    <input name="CusStartDate" id="CusStartDate" class="easyui-datetimebox" style="width: 260px;" />
                </div>
                <div class="fitem">
                    <label>结束时间:</label>
                    <input name="CusFinishDate" id="CusFinishDate" class="easyui-datetimebox" style="width: 260px;" />
                </div>
                <input type="hidden" id="hClassName" />
            </form>
        </div>
        <div id="dlg-buttons4">
            <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveChoseCourseNew()" style="width: 90px">保存</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg4').dialog('close'); " style="width: 90px">取消</a>
        </div>

        <div id="dlg5" class="easyui-dialog" style="width: 750px; height: 480px; padding: 10px 20px" data-options="modal:true,top:10"
            closed="true" buttons="#dlg-buttons5">
            <div class="ftitle">请选择要删除的课程</div> 
            <table id="dg5" class="easyui-datagrid"
                data-options="rownumbers:true,singleSelect:false,url:'Course.ashx?t=dcct',method:'post',checkOnSelect:true, pagination:true">
                <thead>
                    <tr>
                        <th data-options="field:'ck',checkbox:true"></th>
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
            <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveDeleteCourse()" style="width: 90px">保存</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg5').dialog('close');" style="width: 90px">取消</a>
        </div>


        <div id="dlg6" class="easyui-dialog" style="width: 550px; height: 360px; padding: 10px 20px" data-options="modal:true,top:10"
            closed="true" buttons="#dlg-buttons6">
            <form id="fm1" method="post">
                <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1">
                    <tr>
                        <td colspan="4" bgcolor="F0F9FF" class="gray10a" height="25">
                            <div align="center"><span id="aExportClass"></span></div>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%" bgcolor="FFFFFF" class="gray10a" height="25">
                            <div align="center">班级现有人数：</div>
                        </td>
                        <td width="60%" bgcolor="FFFFFF" height="25" class="gray10a">
                            <span id="aCurPerson"></span>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%" bgcolor="FFFFFF" class="gray10a" height="25">
                            <div align="center">要导入人数：</div>
                        </td>
                        <td width="60%" bgcolor="FFFFFF" height="25" class="gray10a">
                            <span id="aExportCount"></span>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%" bgcolor="FFFFFF" class="gray10a" height="25">
                            <div align="center">与现有班级重复人数：</div>
                        </td>
                        <td width="60%" bgcolor="FFFFFF" height="25" class="gray10a">
                            <span id="aRepeatCount"></span>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%" bgcolor="FFFFFF" class="gray10a" height="25">
                            <div align="center">系统不存在人数：</div>
                        </td>
                        <td width="60%" bgcolor="FFFFFF" height="25" class="gray10a">
                            <span id="aNotExistCount"></span>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%" bgcolor="FFFFFF" class="gray10a" height="25">
                            <div align="center">身份证号为空的人数：</div>
                        </td>
                        <td width="60%" bgcolor="FFFFFF" height="25" class="gray10a">
                            <span id="aNULLidentityNo"></span>
                        </td>
                    </tr>
                </table>
            </form>
        </div>
        <div id="dlg-buttons6">
            <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveClassAndStudent()" style="width: 120px">确认导入</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg6').dialog('close'); " style="width: 120px">取消导入</a>
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




    <div id="dlgUpload" class="easyui-dialog" style="width: 400px; height: 400px; padding: 10px 20px" closed="true" data-options="modal:true,top:10">
        <div class="ftitle">上传excel数据文件</div>
        <input type="file" id="upData" name="upData" />
        <div id="fileQueue"></div>
    </div>
    <input type="hidden" id="setClassIdForStu" />
    <input type="hidden" id="setClassNameStu" />


    <div id="dlgUploadppt" class="easyui-dialog" style="width:600px; height: 400px; padding: 10px 20px" closed="true" data-options="modal:true,top:10">
        <table id="dgppts" title="已上传课件列表" class="easyui-datagrid" style="width: 100%"
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
            <a id="btnNewppt" href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newppt()">新增</a>
            <a id="btnDelppt" href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="Deleteppt()">删除</a>
            <a id="btndownppt" href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="downloadppt()">下载</a>
            <a id="btndownppts" href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="downloadpptall()">全部下载</a>
        </div>
    </div>
  
    <div id="dlgUploadpptfile" class="easyui-dialog" style="width: 400px; height:200px; padding: 10px 20px" closed="true" data-options="modal:true,top:10">
        <div class="ftitle">上传课件</div>
        <input type="file" id="upDatappt" name="upDatappt" />
        <div id="fileQueueppt"></div>
    </div>
    
    
    
    <div id="dlghomework" class="easyui-dialog" style="width:600px; height: 400px; padding: 10px 20px" closed="true" data-options="modal:true,top:10">
        <table id="dghomework" title="作业信息列表" class="easyui-datagrid" style="width: 100%"
            url="ClassInfo.ashx?t=getallhomework"
            toolbar="#toolbarhomework" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="Id" width="0" hidden="true">编号</th>
                    <th field="TaskName" width="40%">作业名称</th>
                    <th field="StuName" width="20%">学员姓名</th>
                    <th field="School" width="20%">所在学校</th>
                    <th field="CreateTime" width="15%" sortable="true" formatter="formatterdate">上传时间</th>
                </tr>
            </thead>
        </table>
        <div id="toolbarhomework"> 
            <a id="btndownhomeworks" href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="downloadhomeworks()">下载</a>
            <a id="btndownallhomeworks" href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-download" plain="true" onclick="downloadhomeworksall()">全部下载</a>
        </div>
    </div>

    <script type="text/javascript">

        var url = 'ClassInfo.ashx';
        function newClass() {
            $('#dlg').dialog('open').dialog('setTitle', '新增');

            $('#Name').textbox("setText", "");
            $('#ObjectName').textbox("setText", "");
            $('#Description').textbox("setText", "");
            $('#StartDate').textbox("setText", "");
            $('#FinishDate').textbox("setText", "");
            $('#Students').textbox("setText", "");
            $('#Point').textbox("setText", "");
            $('#PointType').textbox("setText", "");
            $('#Teacher').textbox("setText", "");
            $('#Area').textbox("setText", "");
            $('#Level').combobox("setValue", "");
            $('#Type').textbox("setText", "");
            $('#Type').textbox("setText", "");
            $('#ReportMax').textbox("setText", "");
            $('#CloseDate').textbox("setText", "");
            $("[name='chkIsReport']").removeAttr("checked");

            url = 'ClassInfo.ashx' + '?t=n';
        }
        function editClass() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '编辑');

                $('#Name').textbox("setText", row.Name);
                $('#ObjectName').textbox("setText", row.ObjectName);
                $('#Description').textbox("setText", row.Description);
                $('#StartDate').datebox("setValue", row.StartDate);
                $('#FinishDate').datebox("setValue", row.FinishDate);
                $('#Students').textbox("setText", row.Students);
                $('#Point').textbox("setText", row.Point);
                $('#PointType').combobox("setValue", row.PointType);
                $('#Teacher').textbox("setText", row.Teacher);
                $('#Area').combobox("setText", row.AreaName);
                $('#Level').combobox("setValue", row.Level);
                $('#Type').combobox("setValue", row.Type);
                $('#ReportMax').textbox("setText", row.ReportMax);
                $('#CloseDate').datebox("setValue", row.CloseDate);

                var objchk = window.document.getElementById("chkIsReport");
                if (row.IsReport == 1) {
                    objchk.checked = true;
                }
                else {
                    objchk.checked = false;
                }                

                url = 'ClassInfo.ashx' + '?t=e&id=' + row.ID;
            } else {
                messageAlert('提示', '请选择要编辑的行!', 'warning');
            }
        }

        function saveClass() {
            var IsReport = "no";
            if ($('#chkIsReport').is(':checked')) {
                IsReport = "yes";
            }

            var data = {
                Name: $('#Name').textbox("getText"),
                Object: 0,
                ObjectName: $('#ObjectName').textbox("getText"),
                Description: $('#Description').textbox("getText"),
                StartDate: $('#StartDate').textbox("getText"),
                FinishDate: $('#FinishDate').textbox("getText"),
                Students: $('#Students').textbox("getText"),
                Point: $('#Point').textbox("getText"),
                PointType: $('#PointType').combobox("getValue"),
                PointTypeName: $('#PointType').combobox("getText"),
                Teacher: $('#Teacher').textbox("getText"),
                Level: $('#Level').combobox("getValue"),
                LevelName: $('#Level').combobox("getText"),
                Type: $('#Type').combobox("getValue"),
                TypeName: $('#Type').combobox("getText"),
                Area: $('#Area').combobox("getValue"),
                AreaName: $('#Area').combobox("getText"),
                SetIsReport: IsReport,
                ReportMax: $('#ReportMax').textbox("getText"),
                CloseDate: $('#CloseDate').textbox("getText")
            };
            if (data.Name == "") {
                alert("请填写班级名称！");
                return;
            }
            if (data.ObjectName == "") {
                alert("请填写培训对象！");
                return;
            }
            if (data.Level == "") {
                alert("请选择培训类别！");
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


        function DeleteClass() {
            url = 'ClassInfo.ashx' + '?t=d';
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                //  $.messager.confirm('确认', '确定删除吗?', function (r) {
                if (confirm('确定删除吗?')) {
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
                //  });
            } else {
                messageAlert('提示', '请选择要编辑的行!', 'warning');
            }
        }

        function downloadTmp() {
            var url = "ClassInfo.ashx?t=ex" + "&name=" + encodeURIComponent($("#name").textbox('getText')) + "&description=" + encodeURIComponent($("#description").textbox('getText'))
                + "&level=" + $("#tLevel").combobox('getValue') + "&type=" + $("#tType").combobox('getValue');
            window.location = url;
        }

        function downloadDetailTmp() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                var url = "ClassInfo.ashx?t=exdi&classId=" + row.ID + "&className=" + row.Name;
                window.location = url;
            } else {
                messageAlert('提示', '请选择要导出班级所在的行!', 'warning');
            }
        }

        var courseids = "";
        var uncourseids = "";
        function setCourse() {
            courseids = "";
            uncourseids = "";
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg3').dialog('open').dialog('setTitle', '课程设置');
                $("#hClassid").val(row.ID);
                $('#dg3').datagrid('reload', { t: 'gc', cId: row.ID });
            } else {
                messageAlert('提示', '请选择要设置的行!', 'warning');
            }
        }


        function setCourseNew() {

            $('#CusCourse').textbox("setValue", "");
            $('#CusTeacher').textbox("setValue", "");
            $('#CusStartDate').textbox("setValue", "");
            $('#CusFinishDate').datebox("setValue", "");
            $('#TeachPlace').textbox("setValue", "");

            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg4').dialog('open').dialog('setTitle', '课程设置');
                $("#hClassid").val(row.ID);
                $("#hClassName").val(row.Name);
            } else {
                messageAlert('提示', '请选择要设置的行!', 'warning');
            }
        }

        function showCourseNew() {
            courseids = "";
            uncourseids = "";
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg5').dialog('open').dialog('setTitle', '查看课程设置');
                $("#hClassid").val(row.ID);
                $('#dg5').datagrid('reload', { t: 'dcct', cId: $("#hClassid").val() });
            } else {
                messageAlert('提示', '请选择要设置的行!', 'warning');
            }
        }



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
                    messageAlert('提示', result, 'warning');
                    $('#dlg3').dialog('close');
                    $('#dg3').datagrid('load');
                    $('#dg').datagrid('reload');
                    courseids = "";
                    uncourseids = "";
                }
            });
        }

        function saveChoseCourseNew() {
            var url = "Course.ashx?t=ctc";
            var data = {
                CourseId: $('#CusCourse').combobox("getValue"),
                CoursName: $('#CusCourse').combobox("getText"),
                TeacherId: $('#CusTeacher').combobox("getValue"),
                TeacherName: $('#CusTeacher').combobox("getText"),
                TeachPlace: $('#TeachPlace').textbox("getText"),
                ClassId: $("#hClassid").val(),
                ClassName: $("#hClassName").val(),
                StartDate: $('#CusStartDate').textbox("getText"),
                FinishDate: $('#CusFinishDate').textbox("getText")
            };
            if (data.CoursName == "") {
                alert("请选择课程！");
                return;
            }
            if (data.TeacherName == "") {
                alert("请选择老师！");
                return;
            }
            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlg4').dialog('close');
                    alert("设置成功。");
                }
                else {
                    messageAlert('提示', result, 'warning');
                }
            });
        }

        function saveDeleteCourse() {
            var data;
            if ($("#hcctIsAll").val() == "1") {
                data = { IsAll: 1, ClassId: $("#hClassid").val() };
            } else {
                data = { CctIds: $("#hcctIds").val(), ClassId: $("#hClassid").val() };
            }
            var url = "Course.ashx?t=cctdel";
            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlg5').dialog('close');
                    $('#dg5').datagrid('load');
                    $('#dg').datagrid('reload');
                    cctids = "";
                    uncctids = "";
                } else {
                    messageAlert('提示', result, 'warning');
                    $('#dlg5').dialog('close');
                    $('#dg5').datagrid('load');
                    $('#dg').datagrid('reload');
                    cctids = "";
                    uncctids = "";
                }
            });
        }

        var cctids = "";
        var uncctids = "";

        $('#dg5').datagrid({
            onSelectAll: function () {
                $("#hcctIsAll").val("1");
            },
            onUnselectAll: function () {
                $("#hcctIsAll").val("0");
                $("#hcctIds").val("");
                cctids = "";
            },
            onSelect: function (index, row) {
                if (cctids == "") {
                    var rows = $('#dg5').datagrid('getSelections');
                    for (var i = 0; i < rows.length; i++) {
                        var row1 = rows[i];
                        if (cctids != "") {
                            cctids = cctids + "|" + row1.RId;
                        } else {
                            cctids = row1.RId;
                        }
                    }
                }
                cctids = cctids + "|" + row.RId;
                $("#hcctIds").val(cctids);
            },
            onUnselect: function (index, row) {
                if (uncctids != "") {
                    uncctids = uncctids + "|" + row.RId;
                } else {
                    uncctids = row.RId;
                }
                $("#hUncctIds").val(uncctids);
            },
            onLoadSuccess: function (data) {
                if (data) {
                    $.each(data.rows, function (index, item) {
                        if (item.ck == 1) {
                            $('#dg5').datagrid('checkRow', index);
                        }
                    });
                }
            }
        });


        var stuids = "";
        var unstuids = "";

        function setStu() {
            stuids = "";
            unstuids = "";
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg1').dialog('open').dialog('setTitle', '学员设置');

                $('#SchoolName11').textbox("setText", "");
                $('#StudentName11').textbox("setText", "");
                $('#CardCode11').textbox("setText", "");
                $('#JijiaoCode11').textbox("setText", "");

                $("#hClassid").val(row.ID);
                $('#dg1').datagrid('reload', { t: 'cs', coId: $("#hClassid").val() });
            } else {
                messageAlert('提示', '请选择要设置的行!', 'warning');
            }

        }

        function queryStudent()
        {
            var schoolname = $('#SchoolName11').textbox("getText");
            var studentname = $('#StudentName11').textbox("getText");
            var cardcode = $('#CardCode11').textbox("getText");
            var jijiaocode = $('#JijiaoCode11').textbox("getText");

            $('#dg1').datagrid('reload', {
                t: 'csq', coId: $("#hClassid").val(), school: schoolname,
                student: studentname, card: cardcode, code: jijiaocode
            });
        }

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
                    messageAlert('提示', result, 'warning');
                    $('#dlg1').dialog('close');
                    $('#dg1').datagrid('load');
                    $('#dg').datagrid('reload');
                    stuids = "";
                    unstuids = "";
                }
            });
        }

        var teacherids = "";
        var unteacherids = "";

        function setTeacher() {
            teacherids = "";
            unteacherids = "";
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg2').dialog('open').dialog('setTitle', '项目负责人设置');
                $("#hClassid").val(row.ID);
                $('#dg2').datagrid('reload', { t: 'gt', cId: $("#hClassid").val() });
            } else {
                messageAlert('提示', '请选择要设置的行!', 'warning');
            }
        }


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
                        if (teacherids != "") {
                            teacherids = teacherids + "|" + row1.UserId;
                        } else {
                            teacherids = row1.UserId;
                        }
                    }
                }
                teacherids = teacherids + "|" + row.UserId;
                $("#hTeacherIds").val(teacherids);
            },
            onUnselect: function (index, row) {
                if (unteacherids != "") {
                    unteacherids = unteacherids + "|" + row.UserId;
                } else {
                    unteacherids = row.UserId;
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
                    messageAlert('提示', result, 'warning');
                    $('#dlg2').dialog('close');
                    $('#dg2').datagrid('load');
                    $('#dg').datagrid('reload');
                    stuids = "";
                    unstuids = "";
                }
            });
        }

        function uploadTmpStuData() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $("#setClassIdForStu").val(row.ID);
                $("#setClassNameStu").val(row.Name);
                $('#aExportClass').text(row.Name);

                $('#dlgUpload').dialog({
                    title: '批量设置学员',
                    height: '300px',
                    closed: false,
                    cache: false,
                    modal: true
                });
                SetUploadTmpStuData(row.ID);
                $('#dlgUpload').dialog('open');
            } else {
                messageAlert('提示', '请选择要设置的行!', 'warning');
            }
        }

        function SetUploadTmpStuData(classId) {
            $('#upData').uploadify({
                'swf': 'Scripts/uploadify.swf',
                'uploader': 'UploadTmpData.ashx?t=stet&d=' + classId,
                'cancelImg': 'Scripts/cancel.png',
                'removeCompleted': true,
                'hideButton': false,
                'auto': true,
                'queueID': 'fileQueue',
                'fileTypeExts': '*.xls;*.xlsx',
                'fileTypeDesc': 'xls Files (.xls)',
                'buttonText': '请选择要上传的文件',
                'onSelect': function (e) {
                    if (e.type != '.xlsx' && e.type != '.xls') {
                        alert("请上传excel文件!");
                    }
                },
                'onUploadSuccess': function (file, data, response) {
                    if (data != "" && data != "1") {
                        var result = data.split('|');
                        if (result.length > 0) {
                            if (result[0] == "studentexport") {
                                $('#aCurPerson').text(result[1]);
                                $('#aExportCount').text(result[2]);
                                $('#aRepeatCount').text(result[3]);
                                $('#aNotExistCount').text(result[4]);
                                $('#aNULLidentityNo').text(result[5] + '  ( 注：身份证号为空的学员，不允许入库 ) ');
                                $('#dlg6').dialog('open').dialog('setTitle', '导入学员');
                            }
                            else {
                                ifConfirmCover(result[1]);
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
                        $('#dlgUpload').dialog('close');
                        $('#dg').datagrid('reload');
                    }
                }
            });
        }

        function saveClassAndStudent() {
            var url = "UploadTmpData.ashx?t=cstet&d=" + $("#setClassIdForStu").val();
            var data = { "fname": "" };
            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlg6').dialog('close');
                    $('#dg').datagrid('reload');
                }
                else {
                    messageAlert('提示', result, 'warning');
                }
            });
        }


        function ifConfirmCover(filename) {
            var url = "UploadTmpData.ashx?t=cstet&d=" + $("#setClassIdForStu").val();
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

        function formatterdate(val, row) {
            if (val != "" && val != undefined) {
                return val.substring(0, 10);
            }
            else { return val; }
        }

        function formatterdatemore(val, row) {
            if (val != "" && val != undefined) {
                return val.substring(0, 10) + " " + val.substring(11, 19);
            }
            else { return val; }
        }

        function clearCondition() {
            $('#name').textbox('clear');
            $('#description').textbox('clear');
            $('#tLevel').textbox('clear');;
            $('#tType').textbox('clear');
        }

        function queryUser() {
            $('#dg').datagrid('load', {
                t: "q",
                Name: $("#name").textbox('getText'),
                Description: $("#description").textbox('getText'),
                Level: $("#tLevel").combobox('getValue'),
                Type: $("#tType").textbox('getValue')
            });
        }

        function exportData() {
            var url = "class.ashx?t=ex" + "&name=" + encodeURIComponent($("#name").textbox('getText')) + "&description=" + $("#description").textbox('getText')
                + "&area=" + $("#area").textbox('getText') + "&type=" + $('#type').combobox("getValue");
            window.location = url;
        }



        $(function () {
            if ('<%=IsDel%>' == "block") {
                $("#btnNew").show();
                $("#btnEdit").show();
                $("#btnDel").show();
                $("#btnStu").show();
                $("#btnExstu").show();
                $("#btnCharge").show();
                $("#btnExclass").show();
               } else { 
                $("#btnNew").hide();
                $("#btnEdit").hide();
                $("#btnDel").hide();
                $("#btnStu").hide();
                $("#btnExstu").hide();
                $("#btnCharge").hide();
                $("#btnExclass").hide();
               }
           });


        function disInfo() {  
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
                $('#aPointType').text(row.PointTypeName);
                $('#aTeacher').text(row.Teacher);
                $('#aArea').text(row.AreaName);
                $('#aLevel').text(row.levelname);
                $('#aType').text(row.TypeName);
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


        var cid;
        function uploadDatappt() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {  
                $('#dlgUploadppt').dialog({
                    title: row.Name+ '-- 课件信息', 
                    closed: false,
                    cache: false,
                    modal: true
                });
                cid = row.ID;
                SetUploadPPtData(row.ID);
                $('#dgppts').datagrid('reload', { t: 'ppt', cId: row.ID });
                $('#dlgUploadppt').dialog('open');
            } else {
                messageAlert('提示', '请选择课件所属班级!', 'warning');
            }
        }


        function newppt() {
            $('#dlgUploadpptfile').dialog({
                title: '上传课件',
                height: '300px',
                closed: false,
                cache: false,
                modal: true
            });
            $('#dlgUploadpptfile').dialog('open'); 
        }


        function Deleteppt() {
            var row = $('#dgppts').datagrid('getSelected');
            var url = "ClassInfo.ashx?t=delppts";
            if (row) {
                if (confirm("确定删除此课件？")) {
                    $.post(url, { id: row.Id }, function (result) {
                        if (result == "" || result == null) {
                            $('#dgppts').datagrid('reload');    // reload the user data
                        } else {
                            //$.messager.show({    // show error message
                            //    title: 'Error',
                            //    msg: result
                            //});
                            alert(result);
                        }
                    });
                }
            } else {
                messageAlert('提示', '请选择要删除的课件!', 'warning');
            }
        }


        function downloadppt() {
            var row = $('#dgppts').datagrid('getSelected');
            if (row) {
                var url = "ClassInfo.ashx?t=downppt" + "&id=" +row.Id;
                window.location = url;
            } else {
                messageAlert('提示', '请选择要下载的课件!', 'warning');
            }
        }

        function downloadpptall() { 
           var rows= $('#dgppts').datagrid('getRows');
           if (rows != null && rows.length>0) {
                var url = "ClassInfo.ashx?t=downpptall" + "&cid=" + cid;
                window.location = url;
            } 
        }





        function SetUploadPPtData(classId) {
            $('#upDatappt').uploadify({
                'swf': 'Scripts/uploadify.swf',
                'uploader': 'ClassInfo.ashx?t=upppts&c=' + classId,
                'cancelImg': 'Scripts/cancel.png',
                'removeCompleted': true,
                'hideButton': false,
                'auto': true,
                'buttonText': '请选择要上传的文件',
                'queueID': 'fileQueue',
                //'fileTypeExts': '*.xls;*.xlsx',
                //'fileTypeDesc': 'xls Files (.xls)',
                'onSelect': function (e) {
                    //if (e.type != '.xlsx' && e.type != '.xls') {
                    //    alert("请上传excel文件!");
                    //}
                },
                'onUploadStart': function (file) {
                    //$('#upDatappt').uploadify('upload', '*');
                    $("#upDatappt").uploadify("settings", 'formData', {
                        cid: classId
                    }); //动态传参数
                },
                'onUploadSuccess': function (file, data, response) {
                    if (data != "" && data != "1") {
                        //var result = data.split('|');
                        //if (result.length > 0) {
                        //    if (result[0] == "studentexport") {
                        //        $('#aCurPerson').text(result[1]);
                        //        $('#aExportCount').text(result[2]);
                        //        $('#aRepeatCount').text(result[3]);
                        //        $('#aNotExistCount').text(result[4]);
                        //        $('#aNULLidentityNo').text(result[5] + '  ( 注：身份证号为空的学员，不允许入库 ) ');
                        //        $('#dlg6').dialog('open').dialog('setTitle', '导入学员');
                        //    }
                        //    else {
                        //        ifConfirmCover(result[1]);
                        //    }
                        //} else {
                        //    alert(data);
                        //}
                    }
                },
                'onUploadError': function (file, errorCode, errorMsg, errorString) {
                    $('#permissions_hint').show();
                },
                'onQueueComplete': function (queueData) {
                    if (queueData.uploadsSuccessful > 0) {
                        $('#dlgUploadppt').dialog('close');
                        $('#dlgUploadpptfile').dialog('close');
                        $('#dg').datagrid('reload');
                      //  $('#dgppts').datagrid('reload', { t: 'ppt', cId: row.ID });
                    }
                }
            });
        }


        function homeworkinfo() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlghomework').dialog({
                    title: row.Name + '-- 作业信息',
                    closed: false,
                    cache: false,
                    modal: true
                });
                cid = row.ID; 
                $('#dghomework').datagrid('reload', { t: 'getallhomework', cId: row.ID });
                $('#dlghomework').dialog('open');
            } else {
                messageAlert('提示', '请选择班级!', 'warning');
            }
        }


        //下载作业
        function downloadhomeworks() {
            var row = $('#dghomework').datagrid('getSelected');
            if (row) {
                var url = "ClassInfo.ashx?t=downtask" + "&id=" + row.Id;
                window.location = url;
            } else {
                messageAlert('提示', '请选择要下载的作业!', 'warning');
            }
        }

        //下载全部作业
        function downloadhomeworksall() {
            var rows = $('#dghomework').datagrid('getRows');
            if (rows != null && rows.length > 0) {
                var url = "ClassInfo.ashx?t=downhwall" + "&cid=" + cid;
                window.location = url;
            }
        }
    </script>

</asp:Content>
