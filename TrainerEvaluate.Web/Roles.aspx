<%@ Page Title="" Language="C#" MasterPageFile="~/PageContent.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="TrainerEvaluate.Web.Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function formatterRoleStatus(val, row) {
            if (val == "1") {
                return "有效";
            }
            else {
                return "无效";    
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1"  style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">角色名称：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="rname" class="easyui-textbox" id="rname" style="width: 160px;" />
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">描述： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="rdesc" class="easyui-textbox" id="rdesc" style="width: 160px;" />
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">创建日期（起）：</div>
            </td>
            <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="rtime" class="easyui-datebox" id="rtime" style="width: 160px;" />
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                  <div align="center">创建日期（止）：</div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="endTime" class="easyui-datebox" id="endTime" style="width: 160px;" />
            </td>
        </tr> 
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                 <div align="center">状态： </div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
               <select class="easyui-combobox" name="rstatus" id="rstatus" style="width: 160px;" panelheight="auto">
                    <option value="1">有效</option>
                    <option value="2">无效</option>
                </select>
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
              
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
             
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

    <div style="margin: 10px; width: 99%">
        <table id="dg" title="角色信息列表" class="easyui-datagrid" style="margin: 10px; width: 99%"
            url="Roles.ashx"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="ID" width="0" hidden="true">编号</th>
                    <th field="Name" width="150" sortable="true">角色名称</th>
                    <th field="Rstatus" width="100" sortable="true" formatter="formatterRoleStatus">状态</th>
                    <th field="Description" width="100" sortable="true">描述</th>
                    <th field="CreateTime" width="150" sortable="true" formatter="formatterdate">创建时间</th>
                </tr>
            </thead>
        </table>
        <div id="toolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="add()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="edit()">编辑</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroy()">删除</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" plain="true" onclick="editAuth()">设置权限</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-user" plain="true" onclick="editUser()">设置用户</a>
        </div>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 500px; height: 300px; padding: 10px 20px" data-options="top:10"
        closed="true" buttons="#dlg-buttons" modal="true">
        <div class="ftitle">详细信息</div>
        <form id="fm1" method="post" novalidate>
            <div class="fitem">
                <label>角色名称:</label>
                <input name="Name" id="Name" class="easyui-textbox" required="true" style="width:280px;" />
            </div>
            <div class="fitem">
                <label>状态:</label>
                <select class="easyui-combobox" name="Rstatus" id="Rstatus" style="width: 280px;" panelheight="auto">
                    <option value="1">有效</option>
                    <option value="2">无效</option>
                </select>

            </div>
            <div class="fitem">
                <label>描述:</label>
                <input name="Description" id="Description"  class="easyui-textbox" 
                    data-options="multiline:true" style="height: 75px;width:280px;" />
            </div>  
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="save()" style="width: 90px">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 90px">取消</a>
    </div>
    
     
        <div id="dlg1" class="easyui-dialog" style="width: 400px; height: 400px; padding: 10px 20px" data-options="modal:true,top:10"
            closed="true" buttons="#dlg-buttons1">
            <div class="ftitle">请选择该角色拥有的权限</div> 
                <table id="dg1" class="easyui-datagrid"
                    data-options="rownumbers:true,singleSelect:false,url:'Roles.ashx',method:'post',checkOnSelect:true, pagination:true">
                    <thead>
                        <tr>
                            <th data-options="field:'ck',checkbox:true"></th>
                            <th data-options="field:'ID'" hidden="true">ID</th>
                            <th data-options="field:'FuncName'">权限名称</th> 
                        </tr>
                    </thead>
                </table>
                <input type="hidden" id="hRoleID" />
                <input type="hidden" id="hRoleName" />
                <input type="hidden" id="hIsAllFunc" />
                <input type="hidden" id="hFuncIds" />
                <input type="hidden" id="hUnFuncIds" /> 
        </div>
        <div id="dlg-buttons1">
            <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveChoseFunc()" style="width: 90px">保存</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg1').dialog('close');$('#dg').datagrid('load');" style="width: 90px">取消</a>
        </div>
    
    
        
        <div id="dlg2" class="easyui-dialog" style="width: 400px; height: 400px; padding: 10px 20px" data-options="modal:true,top:10"
            closed="true" buttons="#dlg-buttons2">
            <div class="ftitle">请选择该角色对应的用户</div> 
                <table id="dg2" class="easyui-datagrid"
                    data-options="rownumbers:true,singleSelect:false,url:'Roles.ashx',method:'post',checkOnSelect:true, pagination:true">
                    <thead>
                        <tr>
                            <th data-options="field:'ck',checkbox:true"></th>
                            <th data-options="field:'UserId'" hidden="true">UserId</th>
                            <th data-options="field:'UserName'">姓名</th>
                            <th data-options="field:'Dept'">单位</th>
                        </tr>
                    </thead>
                </table> 
                <input type="hidden" id="hIsAllUser" />
                <input type="hidden" id="hUserIds" />
                <input type="hidden" id="hUnUserIds" /> 
        </div> 
        <div id="dlg-buttons2">
            <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveChoseUser()" style="width: 90px">保存</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg2').dialog('close');$('#dg').datagrid('load');" style="width: 90px">取消</a>
        </div>


    <script type="text/javascript">
        var url = "";
        function query() {
            $('#dg').datagrid('load', {
                t: "q",
                Name: $("#rname").textbox('getText'),
                Rstatus: $("#rstatus").combobox("getValue"),
                CreateTime: $("#rtime").textbox("getText"),
                EndTime: $("#endTime").textbox("getText"),
                Description: $("#rdesc").textbox("getText")
            });
        }

        function clearCondition() {
            $("#rname").textbox('clear');
            $("#rstatus").textbox("clear");
            $("#rtime").textbox("clear");
            $("#endTime").textbox("clear");
            $("#rdesc").textbox("clear");
        }

        function add() {
            $('#dlg').dialog('open').dialog('setTitle', '新增角色信息');
            $('#fm').form('clear');
            url = "Roles.ashx?t=add";
        }


        function edit() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {                
                var roleName = row.Name;
                if (roleName == '超级管理员' || roleName == '项目负责人' || roleName == '学员' || roleName == '教师')
                {
                    alert("角色名称为：" + roleName + "不允许修改，请核实。");
                    return;
                }

                $('#dlg').dialog('open').dialog('setTitle', '修改角色信息');
                $('#fm1').form('load', row);

                url = "Roles.ashx?t=edit&id=" + row.ID;
            } else {
                alert("请选择要修改的行！");
                //   $.messager.alert("提示", "请选择要修改的行！", 'warning');
            }
        }

        function destroy() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                if (confirm("确认要删除吗？")) {
                    url = "Roles.ashx?t=del";
                    $.post(url, { id: row.ID }, function (result) {
                        if (result == "") {
                            $('#dlg').dialog('close');
                            $('#dg').datagrid('reload');
                        } else {
                            alert(result);
                            //  $.messager.alert('提示', result, 'warning');
                        }
                    });
                }
            } else {
                alert("请选择要删除的行！");
                //  $.messager.alert("提示", "请选择要删除的行！", 'warning');
            }
        }

        function save() {
            var data = {
                Name: $('#Name').textbox("getText"), Rstatus: $('#Rstatus').combobox("getValue"), Description: $('#Description').textbox("getText")
            };
            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlg').dialog('close');
                    $('#dg').datagrid('reload');
                } else {
                    alert(result);
                    //  $.messager.alert('提示', result, 'warning');
                }
            });
        }

        function showMsg(title, msg) {
            $.messager.show({
                title: title,
                msg: msg,
                showType: 'show',
                style: {
                    right: '',
                    top: document.body.scrollTop + document.documentElement.scrollTop,
                    bottom: ''
                }
            });

        }


        function editAuth() { 
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg1').dialog('open').dialog('setTitle', '角色权限设置');
                $("#hRoleID").val(row.ID);
                $('#dg1').datagrid('reload', { t: 'gr', rId: $("#hRoleID").val() });
            } else {
                $.messager.alert('提示', '请选择要设置的行!', 'warning');
            } 
        } 

        var funcids = "";
        var unfuncids = "";
        $('#dg1').datagrid({
            onSelectAll: function () {
                $("#hIsAllFunc").val("1");
            },
            onUnselectAll: function () {
                $("#hIsAllFunc").val("0");
                $("#hFuncIds").val("");
            },
            onSelect: function (index, row) {
                if (funcids == "") {
                    var rows = $('#dg1').datagrid('getSelections');
                    for (var i = 0; i < rows.length; i++) {
                        var row1 = rows[i];
                        if (funcids != "") {
                            funcids = funcids + "|" + row1.ID;
                        } else {
                            funcids = row1.ID;
                        }
                    }
                }
                funcids = funcids + "|" + row.ID;
                $("#hFuncIds").val(funcids);
            },
            onUnselect: function (index, row) {
                if (unfuncids != "") {
                    unfuncids = unfuncids + "|" + row.ID;
                } else {
                    unfuncids = row.ID;
                }
                $("#hUnFuncIds").val(unfuncids);
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

        function saveChoseFunc() {
            var data;
            if ($("#hIsAllFunc").val() == "1") {
                data = { IsAll: 1, RoleId: $("#hRoleID").val() };
            } else {
                data = { FuncIds: $("#hFuncIds").val(), RoleId: $("#hRoleID").val(), UnFuncIds: $("#hUnFuncIds").val() };
            }
            var url = "Roles.ashx?t=sf";
            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlg1').dialog('close');
                    $('#dg1').datagrid('load');
                    $('#dg').datagrid('reload');
                    funcids = "";
                    unfuncids = "";
                } else {
                    $.messager.alert('提示', result, 'warning');
                    $('#dlg2').dialog('close');
                    $('#dg2').datagrid('load');
                    $('#dg').datagrid('reload');
                    funcids = "";
                    unfuncids = "";
                }
            });
        }





        function editUser() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg2').dialog('open').dialog('setTitle', '角色用户设置');
                $("#hRoleID").val(row.ID);
                $("#hRoleName").val(row.Name);
                $('#dg2').datagrid('reload', { t: 'gu', rId: $("#hRoleID").val() ,rName:$("#hRoleName").val()});
            } else {
                $.messager.alert('提示', '请选择要设置的行!', 'warning');
            }
        }



        var userids = "";
        var unuserids = "";
        $('#dg2').datagrid({
            onSelectAll: function () {
                $("#hIsAllUser").val("1");
            },
            onUnselectAll: function () {
                $("#hIsAllUser").val("0");
                $("#hUserIds").val("");
            },
            onSelect: function (index, row) {
                if (userids == "") {
                    var rows = $('#dg2').datagrid('getSelections');
                    for (var i = 0; i < rows.length; i++) {
                        var row1 = rows[i];
                        if (userids != "") {
                            userids = userids + "|" + row1.UserId;
                        } else {
                            userids = row1.UserId;
                        }
                    }
                }
                userids = userids + "|" + row.UserId;
                $("#hUserIds").val(userids);
            },
            onUnselect: function (index, row) {
                if (unuserids != "") {
                    unuserids = unuserids + "|" + row.UserId;
                } else {
                    unuserids = row.UserId;
                }
                $("#hUnUserIds").val(unuserids);
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

        function saveChoseUser() {
            var data;
            if ($("#hIsAllUser").val() == "1") {
                data = { IsAll: 1, RoleId: $("#hRoleID").val() };
            } else {
                data = { UserIds: $("#hUserIds").val(), RoleId: $("#hRoleID").val(), UnUserIds: $("#hUnUserIds").val() };
            }
            var url = "Roles.ashx?t=su";
            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlg2').dialog('close');
                    $('#dg2').datagrid('load');
                    $('#dg').datagrid('reload');
                    userids = "";
                    unuserids = "";
                } else {
                    $.messager.alert('提示', result, 'warning');
                    $('#dlg2').dialog('close');
                    $('#dg2').datagrid('load');
                    $('#dg').datagrid('reload');
                    userids = "";
                    unuserids = "";
                }
            });
        }

    </script>

</asp:Content>
