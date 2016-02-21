<%@ Page Title="" Language="C#" MasterPageFile="~/PageContent.Master" AutoEventWireup="true" CodeBehind="StudentList.aspx.cs" Inherits="TrainerEvaluate.Web.StudentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    
    <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">姓名：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" class="gray10a" height="25">
                <input name="StuName11" class="easyui-textbox" id="StuName11" style="width:165px;" />
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">所在学校： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" class="gray10a" height="25">
                <input name="School11" class="easyui-textbox" id="School11" style="width:165px;" />
            </td>
        </tr>   
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="26">
                <div align="center">职称：</div>
            </td>
            <td width="35%" bgcolor="FFFFFF" class="gray10a" height="26"> 
              <select class="easyui-combobox" name="Title11" id="Title11" style="width: 165px;"  data-options="url:'ComboboxGetData.ashx?t=stuj',method:'get',valueField:'ID',textField:'Name'">
               </select>
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="26">
                <div align="center">联系电话： </div>
            </td>
            <td width="34%" bgcolor="FFFFFF" class="gray10a" height="26">
                <input name="TelNo11" class="easyui-textbox" id="TelNo11" style="width:165px;" />
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">性别：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" class="gray10a" height="25">
                <select class="easyui-combobox" name="Gender11" id="Gender11" style="width:165px;"  data-options="url:'ComboboxGetData.ashx?t=g',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'" > 
                </select>
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">身份证号： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" class="gray10a" height="25">
                <input name="IdentityNo11" class="easyui-textbox" id="IdentityNo11" style="width:165px;" />
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center" bgcolor="FFFFFF">
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="queryUser()" style="width: 90px">查询</a>
                &nbsp;&nbsp; 
                <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-cancel" onclick="clearCondition()" style="width: 90px">清除条件</a>
            </td>
        </tr>
    </table>
    <div style="margin-top: 10px; margin-left: 20px; width: 98%">
        <table id="dg" title="学员信息表" class="easyui-datagrid" style="width: 100%"
            url="StudentsInfo.ashx"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="StudentId" width="0" hidden="true">编号</th>
                    <th field="StuName" width="15%" sortable="true">姓名</th>
                    <th field="GenderName" width="10%" sortable="true">性别</th>
                    <th field="IdentityNo" width="22%" sortable="true">身份证号</th>
                    <th field="School" width="20%" sortable="true">所在学校</th>
                    <th field="JobTitleName" width="15%" sortable="true">职称</th>
                    <th field="TelNo" width="15%" sortable="true">联系电话</th>
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
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-user" plain="true" onclick="ArcInfo()">学员档案</a>
        </div>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 700px; height: 530px; padding: 10px 20px" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons2">
        <div class="ftitle">详细信息</div>
        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1">
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">姓名：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <input name="StuName" id="StuName" class="easyui-textbox" required="true" />
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">性别： </div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <select class="easyui-combobox" name="Gender" id="Gender" style="width: 153px;"  data-options="url:'ComboboxGetData.ashx?t=g',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                    </select>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">身份证号：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <input name="IdentityNo" id="IdentityNo" class="easyui-textbox" required="true" />
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">所在学校： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <select class="easyui-combobox" name="School" id="School" style="width: 153px;" data-options="url:'ComboxGetDropData.ashx?t=shl',method:'get',valueField:'Id',textField:'Name'" > 
                    </select>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">职称：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a"> 
                    <select class="easyui-combobox" name="JobTitle" id="JobTitle" style="width: 153px;"  data-options="url:'ComboboxGetData.ashx?t=stuj',method:'get',valueField:'ID',textField:'Name'">
                     </select>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">联系电话： </div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <input name="TelNo" id="TelNo" class="easyui-textbox" />
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">出生日期：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <input name="Birthday" id="Birthday" class="easyui-datebox" />
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">民族： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <select class="easyui-combobox" name="Nation" id="Nation" style="width: 153px;"  data-options="url:'ComboboxGetData.ashx?t=n',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                     </select>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">全日制学历：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <input name="FirstRecord" id="FirstRecord" class="easyui-textbox" />
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">全日制学校： </div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <input name="FirstSchool" id="FirstSchool" class="easyui-textbox" />
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">在职学历：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <input name="LastRecord" id="LastRecord" class="easyui-textbox" />
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">在职学校： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <input name="LastSchool" id="LastSchool" class="easyui-textbox" />
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">政治面貌：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <select class="easyui-combobox" name="PoliticsStaus" id="PoliticsStaus" style="width: 153px;"  data-options="url:'ComboboxGetData.ashx?t=p',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                   </select>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">现任级别： </div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">

                    <input name="Rank" id="Rank" class="easyui-textbox" />
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">任现任时间：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">

                    <input name="RankTime" id="RankTime" class="easyui-datebox" />
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">现任职务： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a"> 
                    <input name="Post" id="Post" style="width:75px;" class="easyui-textbox" />
                    <select class="easyui-combobox" name="PostOptName" id="PostOptName" style="width: 75px;"  data-options="url:'ComboboxGetData.ashx?t=ptn',method:'get',valueField:'ID',textField:'Name',panelHeight:'auto'">
                    </select>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">任职时间：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <input name="PostTime" id="PostTime" class="easyui-datebox" />
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">手机号码： </div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <input name="Mobile" id="Mobile" class="easyui-textbox" required="true" />
                </td>
            </tr>
            <tr>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">继教号： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <input name="TeachNo" id="TeachNo" class="easyui-textbox" required="true" />
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25"></td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a"></td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">描述：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a" colspan="3">
                    <input name="Description" id="Description" class="easyui-textbox" 
                        data-options="multiline:true" style="height: 65px;width:500px;" />
                </td>
            </tr>
        </table>

    </div>
    <div id="dlg-buttons2">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveUser()" style="width: 90px">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 90px">取消</a>
    </div>


    <div id="dlg1" class="easyui-dialog" style="width: 700px; height: 720px; padding: 10px 20px" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons1">
        <div class="ftitle" style="text-align: center">学员档案信息</div>
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
                    <span id="aStuName"></span>
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
                    <div align="center">所在学校：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <span id="aSchool"></span>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">职务： </div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <span id="aJobTitle"></span>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">身份证号：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aIdentityNo"></span>
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">联系电话： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aTelNo"></span>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">任职时间：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <span id="aPostTime"></span>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">手机号码：</div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a"><span id="aMobile"></span></td>
            </tr>

            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">出生日期：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aBirthday"></span>
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">民族： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aNation"></span>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">全日制学历：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <span id="aFirstRecord"></span>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">全日制学校：</div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a"><span id="aFirstSchool"></span></td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">在职学历：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aLastRecord"></span>
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">在职学校： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aLastSchool"></span>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">政治面貌：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <span id="aPoliticsStaus"></span>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">现任级别：</div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a"><span id="aRank"></span></td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">任现任时间：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aRankTime"></span>
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">现任职务： </div>
                </td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aPost"></span>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">描述：</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <span id="aDescription"></span>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">继教号： </div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <span id="aTeachNo"></span>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">学时：</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <span id="aCredit"></span>
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25"></td>
                <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a"></td>
            </tr>
        </table>

        <div style="margin: 0px; margin-left:6px;margin-top:10px; width: 99%;">  
            <table id="dg-train" title="参加培训情况" class="easyui-datagrid" style="width: 99%;height:190px;"
                data-options="rownumbers:true,singleSelect:false,url:'ClassInfo.ashx?t=stcl',method:'post'"
                pagination="true">
                <thead>
                    <tr>
                        <th field="ID" width="0" hidden="true">编号</th>
                        <th field="Name" width="150">班级名称</th>
                        <th field="Object" width="200" sortable="true">培训对象</th>
                        <th field="Description" width="250" sortable="true">培训内容</th>
                        <th field="StartDate" width="150" sortable="true" formatter="formatterdate">开始日期</th>
                        <th field="FinishDate" width="150" sortable="true" formatter="formatterdate">结束日期</th>
                        <th field="Students" width="80" sortable="true">学员人数</th>
                        <th field="Point" width="80" sortable="true">学时</th>
                        <th field="Teacher" width="120" sortable="true">项目负责人</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <div id="dlg-buttons1">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg1').dialog('close')" style="width: 90px">关闭</a>
    </div>

    <div id="dlgUpload" class="easyui-dialog" style="width: 400px; height: 200px; padding: 10px 20px" data-options="modal:true,top:10" closed="true">
        <div class="ftitle">上传excel数据文件</div>
        <input type="file" id="upData" name="upData" />
        <div id="fileQueue"></div>
    </div>

    <script type="text/javascript">
        var url = 'StudentsInfo.ashx';
        function newUser() {
            $('#dlg').dialog('open').dialog('setTitle', '新增学员信息');
            $('#School').combobox("setText", "");
            $('#JobTitle').combobox("setValue", "");
            $('#IdentityNo').textbox("setText", "");
            $('#TelNo').textbox("setText", "");
            $('#StuName').textbox("setText", "");
            $('#Gender').combobox("setValue", "");
            $('#Birthday').textbox("setText", "");
            $('#Nation').combobox("setValue", "");
            $('#FirstRecord').textbox("setText", "");
            $('#FirstSchool').textbox("setText", "");
            $('#LastRecord').textbox("setText", "");
            $('#LastSchool').textbox("setText", "");
            $('#PoliticsStaus').combobox("setValue", "");
            $('#Rank').textbox("setText", "");
            $('#RankTime').textbox("setText", "");
            $('#Post').textbox("setText", "");
            $('#PostOptName').combobox("setText", "");
            $('#PostTime').textbox("setText", "");
            $('#Mobile').textbox("setText", "");
            $('#Description').textbox("setText", "");
            $('#TeachNo').textbox("setText", "");
            url = 'StudentsInfo.ashx' + '?t=n';
        }
        function editUser() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '编辑学员信息');
                $('#School').combobox("setText", row.School);
                if (row.JobTitle != 0) {
                    // 职称
                    $('#JobTitle').combobox("setValue", row.JobTitle);
                }
                else {
                    $('#JobTitle').combobox("setValue", "");
                }
                $('#IdentityNo').textbox("setText", row.IdentityNo);
                $('#TelNo').textbox("setText", row.TelNo);
                $('#StuName').textbox("setText", row.StuName);
                $('#Gender').combobox("setValue", row.Gender);
                $('#Birthday').datebox("setValue", row.Birthday);
                if (row.Nation != 0) {
                    // 民族
                    $('#Nation').combobox("setValue", row.Nation);
                }
                else {
                    $('#Nation').combobox("setValue", "");
                }
                $('#FirstRecord').textbox("setText", row.FirstRecord);
                $('#FirstSchool').textbox("setText", row.FirstSchool);
                $('#LastRecord').textbox("setText", row.LastRecord);
                $('#LastSchool').textbox("setText", row.LastSchool);
                if (row.PoliticsStatus != 0) {
                    // 政治面貌
                    $('#PoliticsStaus').combobox("setValue", row.PoliticsStatus);
                }
                else {
                    $('#PoliticsStaus').combobox("setValue", "");
                }
                $('#Rank').textbox("setText", row.Rank);
                $('#RankTime').datebox("setValue", row.RankTime);
                $('#Post').textbox("setText", row.Post);
                $('#PostOptName').combobox("setValue", row.PostOptId);
                $('#PostTime').datebox("setValue", row.PostTime);
                $('#Mobile').textbox("setText", row.Mobile);
                $('#Description').textbox("setText", row.Description);
                $('#TeachNo').textbox("setText", row.TeachNo);
                url = 'StudentsInfo.ashx' + '?t=e&id=' + row.StudentId;
            } else {
                // messageAlert('提示', '请选择要编辑的行!', 'warning');  
                messageAlert('提示', '请选择要编辑的行!', 'warning');
            }
        }

        function ArcInfo() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg1').dialog('open').dialog('setTitle', '学员档案');
                $('#aStuName').text(row.StuName);
                if (row.GenderName != null) {
                    $('#aGender').text(row.GenderName);
                }
                $('#aLastSchool').text(row.LastSchool);
                if (row.JobTitleName != null) {
                    $('#aJobTitle').text(row.JobTitleName);
                }
                $('#aIdentityNo').text(row.IdentityNo);
                $('#aTelNo').text(row.TelNo);
                $('#aCredit').text(row.Credit);
                if (row.Birthday != null) {
                    $('#aBirthday').text(StringToDate(row.Birthday));
                }
                if (row.NationName != null) {
                    $('#aNation').text(row.NationName);
                }
                $('#aFirstRecord').text(row.FirstRecord);
                $('#aFirstSchool').text(row.FirstSchool);
                $('#aLastRecord').text(row.LastRecord);
                $('#aLastSchool').text(row.LastSchool);
                if (row.PoliticsStatusName != null) {
                    $('#aPoliticsStaus').text(row.PoliticsStatusName);
                }
                $('#aRank').text(row.Rank);
                if (row.RankTime != null) {
                    $('#aRankTime').text(StringToDate(row.RankTime));
                }
                if (row.Post != null) {
                    if (row.PostOptName != null) {
                        $('#aPost').text(row.Post + "  " + row.PostOptName);
                    }
                    else {
                        $('#aPost').text(row.Post);
                    }
                }
                if (row.PostTime != null) {
                    $('#aPostTime').text(StringToDate(row.PostTime));
                }
                $('#aMobile').text(row.Mobile);
                $('#aDescription').text(row.Description);
                $('#aTeachNo').text(row.TeachNo);
                $('#aSchool').text(row.School);

                $('#dg-train').datagrid('reload', { t: 'stcl', studentId: row.StudentId });
            } else {
                messageAlert('提示', '请选择要查看的行!', 'warning');
            }
        }

        function saveUser() {
            var data = {
                StuName: $('#StuName').textbox("getText"),
                Gender: $('#Gender').combobox("getValue"),
                IdentityNo: $('#IdentityNo').textbox("getText"),
                School: $('#School').combobox("getText"),
                JobTitle: $('#JobTitle').combobox("getValue"),
                TelNo: $('#TelNo').textbox("getText"),
                Birthday: $('#Birthday').textbox("getText"),
                Nation: $('#Nation').combobox("getValue"),
                FirstRecord: $('#FirstRecord').textbox("getText"),
                FirstSchool: $('#FirstSchool').textbox("getText"),
                LastRecord: $('#LastRecord').textbox("getText"),
                LastSchool: $('#LastSchool').textbox("getText"),
                PoliticsStaus: $('#PoliticsStaus').combobox("getValue"),
                Rank: $('#Rank').textbox("getText"),
                RankTime: $('#RankTime').textbox("getText"),
                Post: $('#Post').textbox("getText"),
                PostTime: $('#PostTime').textbox("getText"),
                Mobile: $('#Mobile').textbox("getText"),
                Description: $('#Description').textbox("getText"),
                TeachNo: $('#TeachNo').textbox("getText"),
                PostOptName: $('#PostOptName ').combobox("getText"),
                PostOptId: $('#PostOptName ').combobox("getValue")
            };
            if (data.StuName == "") {
                messageAlert('提示', "请填写姓名", 'warning');
                return;
            }
            if (data.IdentityNo == "") {
                messageAlert('提示', "请填写身份证号", 'warning');
                return;
            }
            if (data.Mobile == "") {
                messageAlert('提示', "请填写手机号码", 'warning');
                return;
            }
            if (data.TeachNo == "") {
                messageAlert('提示', "请填写继教号", 'warning');
                return;
            }
            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlg').dialog('close');
                    $('#dg').datagrid('reload');
                } else {
                    messageAlert('提示', result, 'warning');
                }
            });
        }


        function destroyUser() {  
            url = 'StudentsInfo.ashx' + '?t=d';
            var row = $('#dg').datagrid('getSelected');
            if (row) {

              //  $.messager.confirm('确认', '确定删除吗?', function (r) {
                    if (confirm('确定删除吗?')) {
                        $.post(url, { id: row.StudentId }, function (result) {
                            if (result == "" || result == null) {
                                $('#dg').datagrid('reload');    // reload the user data
                            } else {
                                messageAlert('提示', result, 'warning');
                            }
                        });
                    }
             //   });
            } else {
                messageAlert('标题', '请选择要删除的行!', 'warning');
            }
        }


        function downloadTmp() {
            var url = "DownloadTemplate.ashx?t=stu";
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
                'uploader': 'UploadTmpData.ashx?t=stu',
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
            var url = "UploadTmpData.ashx?t=cstu";
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
    </script>

    <script type="text/javascript">

        function clearCondition() {
            $('#School11').textbox('clear');
            $('#Title11').textbox('clear');
            $('#IdentityNo11').textbox("setText", "");
            $('#TelNo11').textbox("setText", "");
            $('#StuName11').textbox("setText", "");
            $('#Gender11').combobox("setValue", "");
        }


        function queryUser() {
            $('#dg').datagrid('load', {
                t: "q",
                name: $("#StuName11").textbox('getText'),
                sch: $("#School11").textbox('getText'),
                title: $("#Title11").combobox("getValue"),
                telno: $("#TelNo11").textbox('getText'),
                gender: $('#Gender11').combobox("getValue"),
                idno: $("#IdentityNo11").textbox('getText')
            });
        }

        function exportData() {
            var url = "StudentsInfo.ashx?t=ex" + "&name=" + encodeURIComponent($("#StuName11").textbox('getText')) + "&sch=" + $("#School11").textbox('getText')
                + "&title=" + $("#Title11").textbox('getText') + "&telno=" + $("#TelNo11").textbox('getText') + "&gender=" + $('#Gender11').combobox("getValue")
            + "&idno=" + $("#IdentityNo11").textbox('getText');
            window.location = url;
        }

    </script>

</asp:Content>
