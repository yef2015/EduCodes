<%@ Page Title="" Language="C#" MasterPageFile="~/PageContent.Master" AutoEventWireup="true" CodeBehind="SysParameters.aspx.cs" Inherits="TrainerEvaluate.Web.SysParameters" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script type="text/javascript">
           function formatterParType(val, row) {
               if (val == "1") {
                   return "时间";
               }
               else {
                   return "状态";
               }
           }



           function formatterStatus(val, row) {
               if (val == "1") {
                   return "启用";
               }
               else {
                   return "停用";
               }
           }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" style="margin: 20px;">
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">参数名称：</div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <input name="pname" class="easyui-textbox" id="pname">
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">参数类型： </div>
            </td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a">
                <select class="easyui-combobox" name="patype" id="patype" style="width: 160px;" panelheight="auto">
                    <option value="1">时间</option>
                    <option value="2">状态</option>
                </select>
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">开始时间：</div>
            </td>
            <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="stime" class="easyui-datebox"   id="stime">
            </td>
            <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                <div align="center">结束时间： </div>
            </td>
            <td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">
                <input name="etime" class="easyui-datebox"   id="etime">
            </td>
        </tr>
        <tr>
            <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                <div align="center">状态： </div>
            </td>
            <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                <select class="easyui-combobox" name="pstat" id="pstat" style="width: 160px;" panelheight="auto">
                    <option value="0"></option>
                    <option value="1">启用</option>
                    <option value="2">停用</option>
                </select>
            </td>
            <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25"></td>
            <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a"></td>
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
        <table id="dg" title="参数信息列表" class="easyui-datagrid" style="margin: 10px; width: 99%"
            url="SysParameters.ashx"
            toolbar="#toolbar" pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="ID" width="0" hidden="true">编号</th>
                    <th field="Parameter" width="150" sortable="true">参数名称</th>
                    <th field="Ptype" width="100" sortable="true" formatter="formatterParType" >参数类型</th>
                    <th field="StartTime" width="150" sortable="true"    formatter="formatterdate">开始时间</th>
                    <th field="EndTime" width="200" sortable="true"   formatter="formatterdate">结束时间</th>
                    <th field="Pstatus" width="100" sortable="true" formatter="formatterStatus" >状态</th>
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
        <form id="fm1" method="post">
            <div class="fitem">
                <label>问卷名称:</label>
                <input name="Parameter" id="Parameter" class="easyui-textbox" required="true">
            </div>
            <div class="fitem">
                <label>参数类型:</label>
                <select class="easyui-combobox" name="Ptype" id="Ptype" style="width: 170px;">
                    <option value="1">时间</option>
                    <option value="2">状态</option>
                </select>
            </div>
            <div class="fitem">
                <label>开始时间:</label>
                <input name="StartTime" id="StartTime" class="easyui-datebox">
            </div>
            <div class="fitem">
                <label>结束时间:</label>
                <input name="EndTime" id="EndTime" class="easyui-datebox">
            </div>
            <div class="fitem">
                <label>状态</label>
                <select class="easyui-combobox" name="Pstatus" id="Pstatus" style="width: 170px;">
                    <option value="1">启用</option>
                    <option value="2">停用</option>
                </select>
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
                Parameter: $("#pname").textbox('getText'),
                Ptype: $("#patype").combobox("getValue"),
                StartTime: $("#stime").textbox('getText'),
                EndTime: $("#etime").textbox('getText'),
                Pstatus: $('#pstat').combobox("getValue")
            });
        }

        function clearCondition() {
            $("#pname").textbox('clear');
            $("#patype").combobox("clear");
            $("#stime").textbox('clear');
            $("#etime").textbox('clear');
            $('#pstat').combobox("clear");
        }

        function add() {
            $('#dlg').dialog('open').dialog('setTitle', '新增参数信息');
            $('#fm1').form('clear');
            $("Pstatus").combobox("setValue", "1");
            url = "SysParameters.ashx?t=add";
        }


        function edit() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改参数信息');
                $('#fm1').form('load', row); 
                url = "SysParameters.ashx?t=edit&id=" + row.ID;
            } else {
                alert("请选择要修改的行！");
                //   messageAlert("提示", "请选择要修改的行！", 'warning');
            }
        }

        function destroy() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                if (confirm("确认要删除吗？")) {
                    url = "SysParameters.ashx?t=del";
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
                Parameter: $('#Parameter').textbox("getText"), Ptype: $('#Ptype').combobox("getValue"), StartTime: $('#StartTime').textbox("getText"),
                EndTime: $('#EndTime').textbox("getText"), Pstatus: $('#Pstatus').combobox("getValue")
            }; 
            if (data.Parameter == "") {
                alert("请输入参数名称！");
                return;
            }
            if (data.Ptype == "" || data.Ptype == 0 || data.Ptype == "0") {
                alert("请选择参数类型！");
                return;
            }
            if (data.Ptype == "1") {
                if (data.StartTime == "") {
                    alert("您选择的参数类型为【时间】，请输入开始时间！");
                    return;
                }
                if (data.EndTime == "") {
                    alert("您选择的参数类型为【时间】，请输入结束时间！");
                    return;
                }  
            }
            if (data.Pstatus == "") {
                alert("请选择状态！");
                return;
            }

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
