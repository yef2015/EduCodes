<%@ Page Title="" Language="C#" MasterPageFile="~/PageContent.Master" AutoEventWireup="true" CodeBehind="QuestionnairTreeContent.aspx.cs" Inherits="TrainerEvaluate.Web.QuestionnairTreeContent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div title="试卷内容管理" style="padding:10px">
     
       <div style="margin: 10px; width: 99%">
        <table id="tg"    class="easyui-treegrid" title="试卷内容信息" style="width: 95%;"
               data-options="
                iconCls: 'icon-organization',
                rownumbers: true,
                lines: true,
                animate: true,
                collapsible: true,
                fitColumns: true,
                url: 'QuestionnairTreeInfo.ashx?t=treelist',
                method: 'post',
                idField: 'Id',
                treeField: 'Name',
                onContextMenu: onContextMenu
            ">
            <thead>
                <tr>
                    <th data-options="field:'Name',width:180">试卷标题</th>                    
                    <th data-options="field:'ShowOrder',width:60,align:'right'">显示顺序</th> 
                </tr>
            </thead>
        </table>
        <div id="mm" class="easyui-menu" style="width: 120px;">
            <div onclick="append()" data-options="iconCls:'icon-add'">新增问题</div>
            <div onclick="append()" data-options="iconCls:'icon-add'">新增答案</div>
            <div onclick="edit()" data-options="iconCls:'icon-edit'">编辑</div>
            <div onclick="removeIt()" data-options="iconCls:'icon-remove'">删除</div>
            
        </div>
    </div>  
      
    <div id="dlg" class="easyui-dialog" style="width:400px;height:380px;padding:10px 20px"
         closed="true" buttons="#dlg-buttons" modal="true" data-options="modal:true,top:10">
        <div class="ftitle">试卷内容信息</div>
        <form id="fm" method="post" novalidate>
            <div class="fitem">
                <label>试卷标题:</label>
                <input id="Name"  name="Name" class="easyui-textbox">
            </div>           
            <div class="fitem">
                <label>显示顺序:</label>
                <input id="ShowOrder"  name="ShowOrder"  class="easyui-textbox"   /> 
            </div> 
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="save()" style="width:90px">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')" style="width:90px">取消</a>
    </div> 
</div>

    <script type="text/javascript">

        function ismenuformatter(val, row) {
            if (val == "1")
                return "是";
            else
                return "否";
        }




        var pid = "";
        var datarow;
        function onContextMenu(e, row) {
            pid = row.Id;
            datarow = row;
            e.preventDefault();
            $(this).treegrid('select', row.Id);
            $('#mm').menu('show', {
                left: e.pageX,
                top: e.pageY
            });
        }


        function append() {
            $('#dlg').dialog('open').dialog('setTitle', '新增功能信息');
            $('#fm').form('clear');
            url = "/SysFunc/Add?Pid=" + pid;
        }

        function edit() {
            $('#dlg').dialog('open').dialog('setTitle', '编辑功能信息');
            $('#fm').form('load', datarow);
            url = "/SysFunc/Edit?id=" + pid;
        }

        function removeIt() {
            $.messager.confirm('确认', '确定删除此条记录吗?如果删除其子节点都会被删除', function (r) {
                if (r) {
                    url = "/SysFunc/Delete?id=" + pid;
                    $.post(url, null, function (result) {
                        if (result == "") {
                            $('#dlg').dialog('close');
                            $('#tg').treegrid('reload');
                        } else {
                            $.messager.alert('提示', result, 'warning');
                        }
                    });
                }
            });
        }

        function save() {
            var data = {
                Name: $('#Name').textbox("getText"),
                ShowOrder: $('#ShowOrder').textbox("getText"),
                MenuUrl: $('#MenuUrl').textbox("getText"),
                IsMenu: $("#IsMenu").is(':checked')
            };

            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlg').dialog('close');
                    $('#tg').treegrid('reload');
                } else {
                    $.messager.alert('提示', result, 'warning');
                }
            });
        }

        function query() {
            $('#tg').treegrid('reload');
            //$('#dg').datagrid('load', {
            //    t: "q",
            //    Name: $("#tName").textbox('getText'),
            //    Content: $("#tContent").textbox('getText'),
            //    KeyWord: $("#tKeyWord").textbox('getText'),
            //    ReceiveRange: $("#tReceiveRange").textbox('getText'),
            //    CreateTimeStart: $('#tCreateTimeStart').textbox("getValue"),
            //    CreateTimeEnd: $('#tCreateTimeEnd').textbox("getValue")
            //});
        }

        function clearCondition() {
            $("#tName").textbox('clear');
            $("#tContent").textbox('clear');
            $("#tKeyWord").textbox('clear');
            $("#tReceiveRange").textbox('clear');
            $("#tCreateTimeStart").textbox('clear');
            $("#tCreateTimeEnd").textbox('clear');
        }





        $(document).ready(function () {
            $('#acc').accordion('select', 2);
        });

</script>
</asp:Content>
