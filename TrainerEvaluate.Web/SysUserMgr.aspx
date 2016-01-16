<%@ Page Title="" Language="C#" MasterPageFile="~/PageContent.Master" AutoEventWireup="true" CodeBehind="SysUserMgr.aspx.cs" Inherits="TrainerEvaluate.Web.SysUserMgr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">用户名称：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="tName" class="easyui-textbox" id="tName" style="width: 165px;"/>
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">用户部门： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="tDept" class="easyui-textbox" id="tDept" style="width: 165px;"/>
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
    <div style="margin: 10px; width: 99%">
        <table id="dg" title="用户信息" class="easyui-datagrid" style="width: 99%;"
            url="SysUser.ashx"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="UserId" width="0" hidden="true">编号</th>
                    <th field="UserName" width="250" sortable="true">姓名</th>
                    <th field="IdentityNo" width="250" sortable="true">身份证号</th>
                    <th field="Dept" width="100" sortable="true">单位</th>
                    <th field="UserAccount" width="200" sortable="true">登陆帐号</th>
                    <th field="UserPassWord" width="0" hidden="true">密码</th>
                </tr>
            </thead>
        </table>
        <div id="toolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newUser()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editUser()">编辑</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyUser()">删除</a>
        </div>

        <div id="dlg" class="easyui-dialog" style="width: 500px; height: 360px; padding: 10px 20px" data-options="modal:true,top:10"
            closed="true" buttons="#dlg-buttons">
            <div class="ftitle">详细信息</div>
            <form id="fm" method="post">
                <div class="fitem">
                    <label>姓名:</label>
                    <input name="UserName" id="UserName" class="easyui-textbox" required="true" style="width:280px;"/>
                </div>
                <div class="fitem" >
                    <label>身份证号</label>  
                     <input name="IdentityNo" id="IdentityNo" class="easyui-textbox" required="true" style="width:280px;"/>
                </div>
                <div class="fitem" >
                    <label>单位</label>  
                     <input name="Dept" id="Dept" class="easyui-textbox" required="true" style="width:280px;"/>
                </div>
                <div class="fitem">
                    <label>登录账号:</label>
                    <input name="UserAccount" id="UserAccount" class="easyui-textbox" style="width:280px;"/>
                     <input type="hidden" id="UserAccountOld" />
                </div>
                <div class="fitem">
                    <label>密码:</label>
                    <input name="Pwd" id="Pwd" class="easyui-textbox" style="width:280px;"/>
                </div>
            </form>
        </div>
        <div id="dlg-buttons">
            <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveUser()" style="width: 90px">保存</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 90px">取消</a>
        </div>
    </div>
    <script type="text/javascript">
        var url = 'SysUser.ashx';
        function newUser() {
            $('#dlg').dialog('open').dialog('setTitle', '新增');
            //$('#fm').form('clear'); 

            $('#UserName').textbox("setText", "");
            $('#UserAccount').textbox("setText", "");
            $('#Pwd').textbox("setText", "");
            $('#Dept').textbox("setText", "");

            url = 'SysUser.ashx' + '?t=n';
        }
        function editUser() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '编辑');
                //$('#fm').form('load', row);

                $('#UserName').textbox("setText", row.UserName);
                $('#UserAccount').textbox("setText", row.UserAccount);
                $('#IdentityNo').textbox("setText", row.IdentityNo);
                $('#Pwd').textbox("setText", row.UserPassWord);
                $('#Dept').textbox("setText", row.Dept);
                $("#UserAccountOld").val(row.UserAccount);

                url = 'SysUser.ashx' + '?t=e&id=' + row.UserId;
            } else {
                $.messager.alert('提示', '请选择要编辑的行!', 'warning');
            }
        }


        function saveUser() {
            var data = {
                UserName: $('#UserName').textbox("getText"),
                Dept: $('#Dept').textbox("getText"),
                UserAccount: $('#UserAccount').textbox("getText"),
                UserAccountOld:$("#UserAccountOld").val(), 
                IdentityNo: $('#IdentityNo').textbox("getText"),
                UserPassWord: $('#Pwd').textbox("getText")
            };
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


        function destroyUser() {
            url = 'SysUser.ashx' + '?t=d';
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('确认', '确定删除吗?', function (r) {
                    if (r) {
                        $.post(url, { id: row.UserId }, function (result) {
                            if (result == "" || result == null) {
                                $('#dg').datagrid('reload');    // reload the user data
                            } else {
                                $.messager.alert('提示', result, 'warning');
                            }
                        });
                    }
                });
            } else {
                $.messager.alert('提示', '请选择要删除的行!', 'warning');
            }
        }


        function queryUser() {
            $('#dg').datagrid('load', {
                t: "g",
                Name: $("#tName").textbox('getText'),
                Dept: $("#tDept").textbox('getText')
            });
        }

        function clearCondition() {
            $("#tName").textbox('clear');
            $("#tDept").textbox("clear"); 
        }
    </script>
</asp:Content>
 
