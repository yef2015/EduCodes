<%@ Page Title="" Language="C#" MasterPageFile="~/PageContent.Master" AutoEventWireup="true" CodeBehind="Dictionaries.aspx.cs" Inherits="TrainerEvaluate.Web.Dictionaries" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
     <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1"  style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">字典名称：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="dname" class="easyui-textbox" id="dname">
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">字典类型： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                  <input name="dtype" class="easyui-textbox" id="dtype"> 
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

    <div style="margin-top: 10px; margin-left: 20px; width: 98%">
        <table id="dg" title="字典信息列表" class="easyui-datagrid" style="margin: 10px; width: 100%"
            url="Dictionaries.ashx"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="ID" width="0" hidden="true">编号</th>
                    <th field="Name" width="35%" sortable="true">字典名称</th>
                    <th field="DType" width="32%" sortable="true">字典类型</th>
                    <th field="CreateTime" width="30%" sortable="true"    formatter="formatterdate">创建时间</th> 
                </tr>
            </thead>
        </table>
        <div id="toolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="add()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="edit()">编辑</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroy()">删除</a>
        </div>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 380px; padding: 10px 20px" data-options="top:10"
        closed="true" buttons="#dlg-buttons" modal="true">
        <div class="ftitle">详细信息</div>
        <form id="fm1" method="post" novalidate>
            <div class="fitem">
                <label>字典名称:</label>
                <input name="Name" id="Name" class="easyui-textbox" required="true">
            </div>
            <div class="fitem">
                <label>字典类型:</label>
                <input name="DType" id="DType" class="easyui-textbox" required="true">
            </div>
         </form>
    </div>
    <div id="dlg-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="save()" style="width: 90px">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width: 90px">取消</a>
    </div>

    <script type="text/javascript">
        var url = "";
        function query() {
            $('#dg').datagrid('load', {
                t: "q",
                Name: $("#dname").textbox('getText'),
                Ptype: $("#dtype").textbox("getText")
            });
        }

        function clearCondition() {
            $("#dname").textbox('clear');
            $("#dtype").textbox("clear");
        }

        function add() {
            $('#dlg').dialog('open').dialog('setTitle', '新增字典信息');
            $('#fm1').form('clear');
            url = "Dictionaries.ashx?t=add";
        }


        function edit() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改字典信息');
                $('#fm1').form('load', row);  
                url = "Dictionaries.ashx?t=edit&id=" + row.ID;
            } else {
                alert("请选择要修改的行！");
                //   messageAlert("提示", "请选择要修改的行！", 'warning');
            }
        }

        function destroy() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                if (confirm("确认要删除吗？")) {
                    url = "Dictionaries.ashx?t=del";
                    $.post(url, { id: row.ID }, function (result) {
                        if (result == "") {
                            $('#dlg').dialog('close');
                            $('#dg').datagrid('reload');
                        } else {
                            alert(result);
                            //  messageAlert('提示', result, 'warning');
                        }
                    });
                }
            } else {
                alert("请选择要删除的行！");
                //  messageAlert("提示", "请选择要删除的行！", 'warning');
            }
        }

        function save() {
            var data = {
                Name: $('#Name').textbox("getText"), DType: $('#DType').textbox("getText")
            };
            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlg').dialog('close');
                    $('#dg').datagrid('reload');
                } else {
                    alert(result);
                    //  messageAlert('提示', result, 'warning');
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


    </script>
    

</asp:Content>
