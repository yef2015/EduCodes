<%@ Page Title="" Language="C#" MasterPageFile="~/PageContent.Master" AutoEventWireup="true" CodeBehind="QuestionnairTreeContent.aspx.cs" Inherits="TrainerEvaluate.Web.QuestionnairTreeContent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div title="问卷内容管理" style="padding:10px">
     
       <div style="margin: 10px; width: 99%">
        <table id="tg" class="easyui-treegrid" title="试卷内容信息" style="width: 95%;"
               data-options="
                iconCls: 'icon-organization',
                rownumbers: true,
                lines: true,
                animate: true,
                collapsible: true,
                fitColumns: true,
                url: 'QuestionnairTreeInfo.ashx?t=treelist',
                method: 'post',
                idField: 'QuestId',
                treeField: 'ShowName',
                onContextMenu: onContextMenu
            ">
            <thead>
                <tr>
                    <th data-options="field:'ShowName',width:300" style="width:300px;">问题</th>                    
                    <th data-options="field:'ShowOrder',width:60,align:'right'">显示顺序</th> 
                </tr>
            </thead>
        </table>
        <div id="mm" class="easyui-menu" style="width: 120px;">
            <div onclick="addQuestion()" data-options="iconCls:'icon-add'">新增问题</div>
            <div onclick="editQuestion()" data-options="iconCls:'icon-add'">编辑问题</div>
            <div onclick="delQuestion()" data-options="iconCls:'icon-remove'">删除问题</div>
            <div onclick="addAnswer()" data-options="iconCls:'icon-add'">新增答案</div>
            <div onclick="editAnswer()" data-options="iconCls:'icon-add'">编辑答案</div>
            <div onclick="delAnswer()" data-options="iconCls:'icon-remove'">删除答案</div>            
        </div>
    </div>  
      
    <div id="dlgQuestion" class="easyui-dialog" style="width:400px;height:380px;padding:10px 20px"
         closed="true" buttons="#dlgQuestion-buttons" modal="true" data-options="modal:true,top:10">
        <div class="ftitle">问题信息</div>
        <form id="fmQuestion" method="post" novalidate>
            <div class="fitem">
                <label>问题:</label>
                <input name="ShowName" id="ShowName" class="easyui-textbox" 
                        data-options="multiline:true" style="height: 65px;width:260px;" />
            </div>           
            <div class="fitem">
                <label>显示顺序:</label>
                <input id="ShowOrder"  name="ShowOrder"  class="easyui-textbox" style="width:260px;"  /> 
            </div> 
        </form>
    </div>
    <div id="dlgQuestion-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="save()" style="width:90px">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgQuestion').dialog('close')" style="width:90px">取消</a>
    </div> 

    <div id="dlgAnswer" class="easyui-dialog" style="width:400px;height:380px;padding:10px 20px"
         closed="true" buttons="#dlgAnswer-buttons" modal="true" data-options="modal:true,top:10">
        <div class="ftitle">试卷答案信息</div>
        <form id="fmAnswer" method="post" novalidate>
            <div class="fitem">
                <label>试卷标题:</label>
                <input id="Name1"  name="Name" class="easyui-textbox" />
            </div>           
            <div class="fitem">
                <label>显示顺序:</label>
                <input id="ShowOrder1"  name="ShowOrder"  class="easyui-textbox"   /> 
            </div> 
        </form>
    </div>
    <div id="dlgAnswer-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="save()" style="width:90px">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgAnswer').dialog('close')" style="width:90px">取消</a>
    </div> 

</div>

    <script type="text/javascript">

        var pid = "";
        var datarow;
        function onContextMenu(e, row) {
            pid = row.QuestId;
            datarow = row; 
            e.preventDefault(); 
            $(this).treegrid('select', row.QuestId);
            $('#mm').menu('show', {
                left: e.pageX,
                top: e.pageY
            });
        }

        var url = 'QuestionnairTreeInfo.ashx';

        // 新增问题
        function addQuestion() {
            $('#dlgQuestion').dialog('open').dialog('setTitle', '新增问题信息');
            $('#fmQuestion').form('clear');
            url = 'QuestionnairTreeInfo.ashx' + '?t=nq&id=' + pid;
        }

        // 编辑问题
        function editQuestion() {
            $('#dlgQuestion').dialog('open').dialog('setTitle', '编辑问题信息');
            $('#fmQuestion').form('load', datarow);
            url = 'QuestionnairTreeInfo.ashx' + '?t=eq&id=' + pid;
        }

        // 删除问题
        function delQuestion() {
            $.messager.confirm('确认', '确定删除此条记录吗?如果删除其子节点都会被删除', function (r) {
                if (r) {
                    url = "QuestionnairTreeInfo.ashx?t=dq&id=" + pid;
                    $.post(url, null, function (result) {
                        if (result == "") {
                            $('#dlgQuestion').dialog('close');
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
                ShowName: $('#ShowName').textbox("getText"),
                ShowOrder: $('#ShowOrder').textbox("getText")
            };

            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlgQuestion').dialog('close');
                    $('#tg').treegrid('reload');
                } else {
                    $.messager.alert('提示', result, 'warning');
                }
            });
        }

</script>
</asp:Content>
