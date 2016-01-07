<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="Analysis.aspx.cs" Inherits="TrainerEvaluate.Web.Analysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script type="text/javascript">

        function getReport() {
            var url = "QuestionnaireHadler.ashx?t=r&id=" + $("#hCourseid").val();
            window.location = url;
        }

        function getSuggestion() {
            $('#dgSug').datagrid('reload', 'QuestionnaireHadler.ashx?t=s&coId=' + $("#hCourseid").val());
            $('#dlgSuggest').dialog('open').dialog('setTitle', '本次问卷调查的学员建议');
        }


        function getTotalReports() {
            var url = "QuestionnaireHadler.ashx?t=extotal";
            window.location = url;
        }

        function getCourseReports() {
            var url = "QuestionnaireHadler.ashx?t=excourse";
            window.location = url;
        }
        function getTeacherReports() {
            var url = "QuestionnaireHadler.ashx?t=exteacher";
            window.location = url;
        }
        function getOrgReports() {
            var url = "QuestionnaireHadler.ashx?t=exorg";
            window.location = url;
        }

        function exportSuggestion() {
            var url = "QuestionnaireHadler.ashx?t=exs&id=" + $("#hCourseid").val();
            window.location = url;
        }

        //function exportSatisfy() {
        //    alert(1);
        //    $('#container1').highcharts.defaultOptions.exporting.exportChart();
        //    alert(11);
        //}



        $(function () {
            var _classid = $('#ClassInfo').combobox({
                url: 'ClassInfo.ashx?t=c',
                editable: false,
                valueField: 'ClassId',
                textField: 'ClassName',
                onSelect: function (record) {
                    _courseid.combobox({
                        disabled: false,
                        url: 'Course.ashx?t=alc&&classId=' + record.ClassId,
                        valueField: 'CourseId',
                        textField: 'CourseName'
                    }).combobox('clear');
                }
            });
            var _courseid = $('#CourseInfo').combobox({
                disabled: false,
                valueField: 'CourseId',
                textField: 'CourseName'
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="CourseNames" name="CourseNames" style="height: 1271px; font-size: 14px; margin-left: 5px;width:263px;padding-top:5px;">
        
        <span style="margin-left: 5px; font-weight: bold; font-size: 15px;">评估班级选择：</span>
        <div>
            <input class="easyui-combobox" name="ClassInfo" id="ClassInfo" style="width:240px"
                data-options="valueField:'ClassId', textField:'ClassName', panelHeight:'auto'" />  
        </div>
        <div style="margin-top: 10px;">
            <span style="margin-left: 5px; font-weight: bold; font-size: 15px;">评估课程选择：</span>
            <div>
                <input class="easyui-combobox" name="CourseInfo" id="CourseInfo" style="width:240px"  
                    data-options="valueField:'CourseId', textField:'CourseName', panelHeight:'auto'" />                  
            </div>
        </div>
        <div style="margin-top: 10px;">
            <span style="margin-left: 5px; font-weight: bold; font-size: 15px;">课程满意度分布区间：</span><br />
            <select id="satisfy" class="easyui-combobox" name="satisfy" style="width: 240px;">
                <option>请选择满意度</option>
                <option value="1">100%-95%</option>
                <option value="2">95%-90%</option>
                <option value="3">90%-80%</option>
                <option value="4">80%以下</option>
            </select>
        </div>
        <div style="margin-top: 10px;">
            <span style="margin-left: 5px; font-weight: bold; font-size: 15px;">统计报表：</span><br />
            <select id="reports" class="easyui-combobox" name="satisfy" style="width: 240px;">
                <option>请选择统计报表</option>
                <option value="1">课程评估总体情况统计表</option>
                <option value="2">课程内容各指标满意度分布表</option>
                <option value="3">培训讲师各指标满意度分布表</option>
                <option value="4">培训组织和管理满意度分布表</option>
                <option value="5">培训教师满意度</option>
                <option value="6">培训课程满意度</option>
            </select>
        </div>

        <script type="text/javascript">
            $("#ClassInfo").combobox({
                onLoadSuccess: function () {
                    var val = $(this).combobox("getData");
                    for (var item in val[0]) {
                        if (item == "ClassId") {
                            $(this).combobox("select", val[0][item]);
                        }
                    }
                },
                onSelect: function (param) {
                    $("#headtable").css("display", "block");
                    $("#ifrAnalysisContent").attr("src", "AnaysisContent.aspx?classid=" + param.ClassId);
                }
            });

            $("#CourseInfo").combobox({
                onLoadSuccess: function () {
                    var val = $(this).combobox("getData");
                    for (var item in val[0]) {
                        if (item == "CourseId") {
                            $(this).combobox("select", val[0][item]);
                        }
                    }
                },
                onSelect: function (param) {
                    $("#headtable").css("display", "block");
                    var classId = $('#ClassInfo').combobox("getValue");
                    $("#ifrAnalysisContent").attr("src", "AnaysisContent.aspx?cid=" + param.CourseId + "&classid=" + classId);
                    //  changeContent("AnaysisContent.aspx?cid=" + param.CourseId); 
                }
            });            

            $("#satisfy").combobox({
                onLoadSuccess: function () {
                    $(this).combobox("setValues", ['0', '请选择满意度']);
                },
                onSelect: function (param) {
                    if (param.value != null && param.value != "请选择满意度") {
                        $("#headtable").css("display", "none");
                        var classId = $('#ClassInfo').combobox("getValue");
                        $("#ifrAnalysisContent").attr("src", "AnaysisContent.aspx?sid=" + param.value + "&classid=" + classId);
                        //    changeContent("AnaysisContent.aspx?sid=" + param.value);

                    }
                }
            });


            $("#reports").combobox({
                onLoadSuccess: function () {
                    $(this).combobox("setValues", ['0', '请选择统计报表']);
                },
                onSelect: function (param) {
                    if (param.value != null && param.value != "请选择统计报表") {
                        $("#headtable").css("display", "none");
                        var classId = $('#ClassInfo').combobox("getValue");
                        var courseId = $('#CourseInfo').combobox("getValue");
                        $("#ifrAnalysisContent").attr("src", "AnaysisContent.aspx?t=r&sid=" + param.value
                            + "&classId=" + classId + "&courseId=" + courseId);
                        // changeContent("AnaysisContent.aspx?t=r&sid=" + param.value);
                    }
                }
            });


        </script>
    </div>
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
   <iframe id="ifrAnalysisContent" width="100%" height="1271" frameborder="0" scrolling="auto" src="AnaysisContent.aspx"> 
    </iframe>   

</asp:Content>
