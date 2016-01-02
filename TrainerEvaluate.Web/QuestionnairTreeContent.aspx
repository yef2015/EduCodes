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
                    <th data-options="field:'ShowName',width:500">问题</th>                    
                    <th data-options="field:'ShowOrder',width:100,align:'left'">显示顺序</th> 
                </tr>
            </thead>
        </table>
        <div id="mm" class="easyui-menu" style="width: 120px;">
            <div onclick="addQuestion()" data-options="iconCls:'icon-add'">新增问题</div>
            <div onclick="editQuestion()" data-options="iconCls:'icon-add'">编辑问题</div>
            <div onclick="delQuestion()" data-options="iconCls:'icon-remove'">删除问题</div>
            <%--<div onclick="addAnswer()" data-options="iconCls:'icon-add'">新增答案</div>--%>
            <div onclick="editRadioAnswer()" data-options="iconCls:'icon-add'">编辑单选答案</div>
            <div onclick="editTextAnswer()" data-options="iconCls:'icon-add'">编辑文本框答案</div>
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
                <label>类型:</label>
                <select class="easyui-combobox" name="ShowType" id="ShowType" style="width:260px;">
                    <option value="NoAnswer">不带答案</option>
                    <option value="AnswerRadio">答案是单选</option>
                    <option value="AnswerText">答案是文本框</option>
                </select>        
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

    <div id="dlgAnswerRadio" class="easyui-dialog" style="width:500px;height:380px;padding:10px 20px"
         closed="true" buttons="#dlgAnswerRadio-buttons" modal="true" data-options="modal:true,top:10">
        <div class="ftitle">试卷单选答案信息</div>
        <form id="fmAnswerRadio" method="post" novalidate>
            <div class="fitem">
                <label>问题:</label>
                <input id="ShowNameA"  name="ShowNameA"  class="easyui-textbox" 
                        data-options="multiline:true" style="height: 65px;width:360px;" disabled="disabled" />
            </div>           
            <div class="fitem">
                <label>控件Id:</label>
                <input id="ShowId"  name="ShowId"  class="easyui-textbox" style="width:360px;"  /> 
            </div> 
            <div class="fitem">
                <label></label>
                <label style="width:160px;">描述</label>
                <label style="width:114px;">类型</label>
                <label style="width:80px;">值</label>
            </div> 
            <div class="fitem">
                <label>答案1:</label>
                <input id="OptText1"  name="OptText1"  class="easyui-textbox" style="width:160px;"  /> 
                <input id="OptType1"  name="OptType1"  class="easyui-textbox" style="width:114px;"  /> 
                <input id="OptValue1"  name="OptValue1"  class="easyui-textbox" style="width:80px;"  /> 
            </div> 
            <div class="fitem">
                <label>答案2:</label>
                <input id="OptText2"  name="OptText2"  class="easyui-textbox" style="width:160px;"  /> 
                <input id="OptType2"  name="OptType2"  class="easyui-textbox" style="width:114px;"  /> 
                <input id="OptValue2"  name="OptValue2"  class="easyui-textbox" style="width:80px;"  /> 
            </div> 
            <div class="fitem">
                <label>答案3:</label>
                <input id="OptText3"  name="OptText3"  class="easyui-textbox" style="width:160px;"  /> 
                <input id="OptType3"  name="OptType3"  class="easyui-textbox" style="width:114px;"  /> 
                <input id="OptValue3"  name="OptValue3"  class="easyui-textbox" style="width:80px;"  /> 
            </div> 
            <div class="fitem">
                <label>答案4:</label>
                <input id="OptText4"  name="OptText4"  class="easyui-textbox" style="width:160px;"  /> 
                <input id="OptType4"  name="OptType4"  class="easyui-textbox" style="width:114px;"  /> 
                <input id="OptValue4"  name="OptValue4"  class="easyui-textbox" style="width:80px;"  /> 
            </div>             
        </form>
    </div>
    <div id="dlgAnswerRadio-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveAnswerRadio()" style="width:90px">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgAnswerRadio').dialog('close')" style="width:90px">取消</a>
    </div> 

    <div id="dlgAnswerText" class="easyui-dialog" style="width:500px;height:380px;padding:10px 20px"
         closed="true" buttons="#dlgAnswerText-buttons" modal="true" data-options="modal:true,top:10">
        <div class="ftitle">试卷文本框答案信息</div>
        <form id="fmAnswerText" method="post" novalidate>
            <div class="fitem">
                <label>问题:</label>
                <input id="ShowNameText"  name="ShowNameText"  class="easyui-textbox" 
                        data-options="multiline:true" style="height: 65px;width:360px;" disabled="disabled" />
            </div>           
            <div class="fitem">
                <label>控件Id:</label>
                <input id="ShowIdText"  name="ShowIdText"  class="easyui-textbox" style="width:360px;"  /> 
            </div>    
        </form>
    </div>
    <div id="dlgAnswerText-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="saveAnswerText()" style="width:90px">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgAnswerText').dialog('close')" style="width:90px">取消</a>
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
            $('#ShowType').combobox("setValue", datarow.ShowType);
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
                ShowType: $('#ShowType').combobox("getValue"),
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

        // 新增答案
        function addAnswer() {
            $('#dlgAnswer').dialog('open').dialog('setTitle', '新增答案信息');
            $('#fmAnswer').form('clear');
            url = 'QuestionnairTreeInfo.ashx' + '?t=na&id=' + pid;
        }

        // 编辑答案，单选
        function editRadioAnswer() {
            $('#dlgAnswerRadio').dialog('open').dialog('setTitle', '编辑单选答案信息');
            $('#fmAnswerRadio').form('load', datarow);
            url = 'QuestionnairTreeInfo.ashx' + '?t=era&id=' + pid;
        }

        // 编辑答案，文本框
        function editTextAnswer() {
            $('#dlgAnswerText').dialog('open').dialog('setTitle', '编辑文本框答案信息');
            $('#fmAnswerText').form('load', datarow);
            $('#ShowNameText').textbox("setText", datarow.ShowNameText);
            url = 'QuestionnairTreeInfo.ashx' + '?t=etxt&id=' + pid;
        }

        // 删除答案
        function delAnswer() {
            $.messager.confirm('确认', '确定删除答案信息吗?', function (r) {
                if (r) {
                    url = "QuestionnairTreeInfo.ashx?t=da&id=" + pid;
                    $.post(url, null, function (result) {
                        if (result == "") {
                            $('#dlgAnswer').dialog('close');
                            $('#tg').treegrid('reload');
                        } else {
                            $.messager.alert('提示', result, 'warning');
                        }
                    });
                }
            });
        }

        // 保存，单选
        function saveAnswerRadio() {
            var data = {
                ShowCode: $('#ShowId').textbox("getText"),
                ShowId: $('#ShowId').textbox("getText"),

                OptText1: $('#OptText1').textbox("getText"),
                OptType1: $('#OptType1').textbox("getText"),
                OptValue1: $('#OptValue1').textbox("getText"),

                OptText2: $('#OptText2').textbox("getText"),
                OptType2: $('#OptType2').textbox("getText"),
                OptValue2: $('#OptValue2').textbox("getText"),

                OptText3: $('#OptText3').textbox("getText"),
                OptType3: $('#OptType3').textbox("getText"),
                OptValue3: $('#OptValue3').textbox("getText"),

                OptText4: $('#OptText4').textbox("getText"),
                OptType4: $('#OptType4').textbox("getText"),
                OptValue4: $('#OptValue4').textbox("getText")
            };

            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlgAnswerRadio').dialog('close');
                    $('#tg').treegrid('reload');
                } else {
                    $.messager.alert('提示', result, 'warning');
                }
            });
        }

        // 保存 文本框
        function saveAnswerText() {
            var data = {
                ShowCodeText: $('#ShowIdText').textbox("getText"),
                ShowIdText: $('#ShowIdText').textbox("getText")
            };

            $.post(url, data, function (result) {
                if (result == "") {
                    $('#dlgAnswerText').dialog('close');
                    $('#tg').treegrid('reload');
                } else {
                    $.messager.alert('提示', result, 'warning');
                }
            });
        }

</script>
</asp:Content>
