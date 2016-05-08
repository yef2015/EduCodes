<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnaysisContent.aspx.cs" ValidateRequest="false" Inherits="TrainerEvaluate.Web.AnaysisContent" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>中小学干部培训评估系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <link rel="stylesheet" type="text/css" href="themes/default/easyui.css"/>
    <link rel="stylesheet" type="text/css" href="themes/icon.css"/>
    <link rel="stylesheet" type="text/css" href="themes/color.css"/>
  <%--  <link rel="stylesheet" type="text/css" href="themes/demo.css"/>--%>
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="Scripts/highcharts.js"  ></script>
    <script type="text/javascript" src="Scripts/exporting.src.js" charset="GBK"></script>
    <link rel="stylesheet" href="Styles/pgxt.css" type="text/css">


    <script type="text/javascript">

        function getReport() {
            var url = "QuestionnaireHadler.ashx?t=r&id=" + $("#hCourseid").val() + "&classId=" + $("#hClassid").val();
            window.location = url;
        }

        // 批量导出课程评估表
        function getAllReport(e) {
            var url = "Course.ashx?t=tips";
            var data = { ClassId: $("#hClassid").val() };
            $.post(url, data, function (result) {
                if (result != "") {
                    var tips = result.split('|');
                    if (tips.length > 0) {
                        if (tips[0] == "export") {
                            $('#atipsClass').text(tips[1]);
                            $('#atipsCourse').text(tips[2]); 
                            $('#dlg6').dialog('open').dialog('setTitle', '批量导出课程评估表');
                        }
                    }
                }
            });
        }

        function getAllReportInfo() {
            $('#dlg6').dialog('close');
            var url = "QuestionnaireHadler.ashx?t=rall&id=" + $("#hCourseid").val() + "&classId=" + $("#hClassid").val();
            window.location = url;
        }

        function getSuggestion() {
            $('#dgSug').datagrid('reload', 'QuestionnaireHadler.ashx?t=s&coId=' + $("#hCourseid").val());
            $('#dlgSuggest').dialog('open').dialog('setTitle', '本次问卷调查的学员建议');
        }


        function getTotalReports(classId) {
            var url = "QuestionnaireHadler.ashx?t=extotal&classId=" + classId;
            window.location = url;
        }

        function getCourseReports(classId) {
            var url = "QuestionnaireHadler.ashx?t=excourse&classId=" + classId;
            window.location = url;
        }
        function getTeacherReports(classId) {
            var url = "QuestionnaireHadler.ashx?t=exteacher&classId=" + classId;
            window.location = url;
        }
        function getOrgReports(classId) {
            var url = "QuestionnaireHadler.ashx?t=exorg&classId=" + classId;
            window.location = url;
        }
        // 导出教师满意度
        function getTrainTeachReports(classId) {
            var url = "QuestionnaireHadler.ashx?t=extrainteach&classId=" + classId;
            window.location = url;
        }
        // 导出培训课程满意度
        function getTrainCourseReports(classId) {
            var url = "QuestionnaireHadler.ashx?t=extraincourse&classId=" + classId;
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

    </script>


</head>

<body bgcolor="#FFFFFF" text="#000000" topmargin="0" leftmargin="0">
    <form id="form1" runat="server">
        <div style="text-align: center; width: 100%">
            <span id="theYear" runat="server" style="font-size: 25px; font-weight: bold">2013年中青年干部教育管理培训班课程评估表</span><br />
        </div>
        <table width="99%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" id="analysisTable" runat="server">
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">课程名称： </div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <div runat="server" id="courseName"></div>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">培训地点： </div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a" colspan="3">
                    <div runat="server" id="coursePlace"></div>
                </td>
            </tr>
            <tr bgcolor="#FFFFFF">
                <td width="16%" class="gray10a" height="26">
                    <div align="center">培训讲师： </div>
                </td>
                <td width="35%" class="gray10a" height="26">
                    <div runat="server" id="teacherName"></div>
                </td>
                <td width="15%" class="gray10a" height="26">
                    <div align="center">培训时间： </div>
                </td>
                <td width="34%" class="gray10a" height="26"  colspan="3">
                    <div runat="server" id="trainTime"></div>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">应评人数： </div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <div runat="server" id="totalPeople">0</div>
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">实评人数： </div>
                </td>
                <td width="11%" bgcolor="FFFFFF" height="25" class="gray10a"  >
                    <div runat="server" id="totalDone">0</div>
                </td>
                  <td width="11%" bgcolor="FFFFFF" height="25" class="gray10a"  >
                    <div align="center">评估进度： </div>
                </td>
                  <td width="12%" bgcolor="FFFFFF" height="25" class="gray10a" >
                    <div runat="server" id="evProgress"  onclick="getNoFinish();" style="cursor: pointer">0</div>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">总平均分： </div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <div runat="server" id="spTotalAvg">0 分（满分52）</div>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">满意度： </div>
                </td>
                <td width="11%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <div runat="server" id="satisfaction">0</div>
                </td>
                  <td width="11%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">等级： </div>
                </td>
                <td width="12%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <div runat="server" id="level">0</div>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">课程内容： </div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                   <div runat="server" id="divCourseContent">0</div>
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">培训讲师： </div>
                </td>
                <%--<td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">未完成 <span runat="server" id="nofinish" onclick="getNoFinish()" style="cursor: pointer; color: red">0</span> 人
                </td>--%>
                   <td width="11%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <div runat="server" id="divTeacher">0</div>
                </td>
                   <td width="11%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">培训组织管理： </div>
                </td>
                <td width="12%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <div runat="server" id="divOrg">0</div>
                </td>
            </tr>


            <tr bgcolor="4A5C69">
                <td height="21" colspan="7" class="white10">详细情况： </td>
            </tr>
            <tr>
                <td colspan="7" bgcolor="#FFFFFF">
                    <div id="details" runat="server"></div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="7" bgcolor="#FFFFFF">
                    <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="getReport()" style="width: 180px">导出评估报告单及学员建议</a>&nbsp;&nbsp;
                    <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="getAllReport(event)" style="width: 200px">批量导出评估报告单及学员建议</a>
                </td>
            </tr>
        </table>

        <div style="background-color: #FFFFFF; width: 98%; height: 900px; margin: 0 auto;" id="container1" runat="server">
        </div>
        <%--<div>
            <a  class="easyui-linkbutton c6" iconcls="icon-ok"  style="width: 120px" id="exportImg">导出</a>
        </div>--%>
        <div style="background-color: #FFFFFF; width: 99%; margin: 0 auto;" id="divReports" runat="server">
        </div>
        <div>
            <input type="hidden" runat="server" id="htp1" name="htp1" />
            <input type="hidden" runat="server" id="htp2" name="htp2" />
            <input type="hidden" runat="server" id="htp3" name="htp3" />
            <input type="hidden" runat="server" id="htp4" name="htp4" />
            <input type="hidden" runat="server" id="htp5" name="htp5" />
            <input type="hidden" runat="server" id="htot1" name="htot1" />
            <input type="hidden" runat="server" id="htot2" name="htot2" />
            <input type="hidden" runat="server" id="htot3" name="htot3" />
            <input type="hidden" runat="server" id="htot4" name="htot4" />
            <input type="hidden" runat="server" id="htot5" name="htot5" />


            <input type="hidden" runat="server" id="hcate" name="hcate" />
            <input type="hidden" runat="server" id="hdata" name="hdata" />
        </div>
    </form>
    <input type="hidden" id="hCourseid" runat="server" />
    <input type="hidden" id="hClassid" runat="server" />
    <div id="dlg" class="easyui-dialog" style="width: 500px; height: 500px; padding: 10px 20px" data-options="modal:true,top:10"
        closed="true" buttons="#dlg-buttons">
        <div class="easyui-tabs" style="width: 400px; height: 400px"> 
            <div  title="已完成学员" tyle="padding: 10px">
                <div class="ftitle">学员信息：</div>
                <table id="dg12" class="easyui-datagrid" toolbar="#toolbarf2"
                    data-options="rownumbers:true,singleSelect:false,url:'QuestionnaireHadler.ashx?t=f2&coId='+$('#hCourseid').val()+'&classId='+$('#hClassid').val(),method:'get',checkOnSelect:false, pagination:true">
                    <thead>
                        <tr>
                            <th data-options="field:'StudentId'" hidden="true">StudentId</th>
                            <th data-options="field:'StuName'">学生姓名</th>
                            <th data-options="field:'School'">所在学校</th>
                            <th data-options="field:'TelNo'">联系电话</th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div title="未完成学员" style="padding: 10px">
                <div class="ftitle">学员信息：</div>
                <table id="dg1" class="easyui-datagrid" toolbar="#toolbar"
                    data-options="rownumbers:true,singleSelect:false,url:'QuestionnaireHadler.ashx?t=f&coId='+$('#hCourseid').val()+'&classId='+$('#hClassid').val(),method:'get',checkOnSelect:false, pagination:true">
                    <thead>
                        <tr>
                            <th data-options="field:'StudentId'" hidden="true">StudentId</th>
                            <th data-options="field:'StuName'">学生姓名</th>
                            <th data-options="field:'School'">所在学校</th>
                            <th data-options="field:'TelNo'">联系电话</th>
                        </tr>
                    </thead>
                </table> 
            </div>
        </div>
    </div>
   
    <div id="toolbar">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="exportStuNoFinish()">导出</a>
    </div> 
    
     <div id="toolbarf2">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="exportStuFinish()">导出</a>
    </div> 
    

    <div id="dlgSuggest" class="easyui-dialog" style="width: 400px; height: 400px; padding: 10px 20px"
        closed="true" buttons="#dlg-buttons">
        <div class="ftitle">学员建议信息：</div>

        <table id="dgSug" class="easyui-datagrid" toolbar="#toolbar1"
            data-options="rownumbers:true,singleSelect:false,url:'QuestionnaireHadler.ashx?t=s&coId='+$('#hCourseid').val(),method:'get',checkOnSelect:false, pagination:true">
            <thead>
                <tr>
                    <th data-options="field:'AppraiserId'" hidden="true">StudentId</th>
                    <th data-options="field:'StuName'">学员姓名</th>
                    <th data-options="field:'Suggest'">建议内容</th>
                </tr>
            </thead>
        </table>
    </div>
    <div id="toolbar1">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="exportSuggestion()">导出</a>
    </div>

     <div id="dlg6" class="easyui-dialog" style="width: 450px; height: 200px; padding: 10px 20px" data-options="modal:true,top:600"
            closed="true" buttons="#dlg-buttons6">
            <form id="fm1" method="post">
                <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1">
                    <tr>
                        <td bgcolor="FFFFFF" class="gray10a" height="35" >
                            您是否导出
                            <span id="atipsClass" style="font-size:16px;font-weight:bold;color:red;">班级</span>&nbsp;&nbsp;班共
                            <span id="atipsCourse"  style="font-size:16px;font-weight:bold;color:red;">课程</span>&nbsp;&nbsp;门课的课程评估表
                        </td>
                    </tr>
                </table>            
            </form>
        </div>
        <div id="dlg-buttons6">
            <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="getAllReportInfo()" style="width: 120px">确认导出</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg6').dialog('close'); " style="width: 120px">取消导出</a>
        </div> 

    <script type="text/javascript">

        $.ajaxSetup({
            cache: false //关闭AJAX相应的缓存
        });

        if ($.fn.pagination) {
            $.fn.pagination.defaults.beforePageText = '第';
            $.fn.pagination.defaults.afterPageText = '共{pages}页';
            $.fn.pagination.defaults.displayMsg = '显示{from}到{to},共{total}记录';
        }


        function getNoFinish() {
            // if ($('#nofinish').text() != "0") {
            $('#dlg').dialog('open').dialog('setTitle', '学员完成情况');
            $('#dg12').datagrid('reload');
            // }
        }



        function exportStuNoFinish() {
            var url = "QuestionnaireHadler.ashx?t=stuno&id=" + $("#hCourseid").val();
            window.location = url;
        }


        function exportStuFinish() {
            var url = "QuestionnaireHadler.ashx?t=stuf2&id=" + $("#hCourseid").val();
            window.location = url;
        }



        var chart;
        var top1 = parseFloat($("#htp1").val());
        var top2 = parseFloat($("#htp2").val());
        var top3 = parseFloat($("#htp3").val());
        var top4 = parseFloat($("#htp4").val());
        var top5 = parseFloat($("#htp5").val());
        var total1 = parseFloat($("#htot1").val());
        var total2 = parseFloat($("#htot2").val());
        var total3 = parseFloat($("#htot3").val());
        var total4 = parseFloat($("#htot4").val());
        var total5 = parseFloat($("#htot5").val());


        //var top1 = 0;
        //var top2 = 100;
        //var top3 = 0;
        //var top4 = 0;
        //var top5 = 0; 

        $(document).ready(function () {
            if ($('#container').css("display") == "block") {
                chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'container'
                    },
                    title: {
                        text: ''
                    },
                    plotArea: {
                        shadow: null,
                        borderWidth: null,
                        backgroundColor: null
                    },
                    credits: {
                        enabled: false
                    },
                    tooltip: {
                        formatter: function () {
                            return '<b>' + this.point.name + '</b>: ' + this.y + ' %';
                        }
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            showInLegend: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: true,
                                formatter: function () {
                                    if (this.y > 5) return this.point.name;
                                },
                                color: 'black',
                                style: {
                                    font: '13px Trebuchet MS, Verdana, sans-serif'
                                }
                            }
                        }
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right'
                    },
                    series: [{
                        type: 'pie',
                        name: '满意度调查',
                        data: [
                            ['很满意', top2],
                            ['满意', top3],
                            ['一般', top4],
                            ['不满意', top5]
                        ]
                    }]
                });
            } else {

            }
        });


        <%-- $(function () {
            $('#<%= container1.ClientID %>').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: ''
                },
                credits: {
                    enabled: false
                },
                xAxis: {
                    categories: [
                        '2014',
                        '2015',
                        '2016',
                        '2017',
                        '2018'
                    ]
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: ''
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.2f} </b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0
                    }
                },
                series: [{
                    name: '总平均分',
                    data: [total1, total2, total3, total4, total5]
                }]
            });
        });--%>




        $(function () {
            if ($('#container1').css("display") == "block") {
                //var categ = ['课程1', '课程2', '课程3', '课程4', '课程5'];
                //var dt = [17, 31, 635, 203, 2];

                var categ = $("#hcate").val().toString().split(',');
                var dt1 = $("#hdata").val().toString().split(',');
                var dt = new Array();
                for (var i = 0; i < dt1.length; i++) {
                    dt[i] = parseFloat(dt1[i]);
                }
                $('#container1').highcharts({
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: '课程满意度分布'
                    },
                    subtitle: {
                        text: ''
                    },
                    xAxis: {
                        //categories: ['课程1', '课程2', '课程3', '课程4', '课程5'],
                        categories: categ,
                        title: {
                            text: null
                        }
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: '百分比',
                            align: 'high'
                        },
                        labels: {
                            overflow: 'justify'
                        }
                    },
                    tooltip: {
                        valueSuffix: '%'
                    },
                    exporting: {
                        enabled: true,
                        buttons: {
                            exportButton: {
                                text: '导出报表',
                                menuItems: null,
                                backgroundColor: '#ff0000',
                                hoverBorderColor: '#ff0000',
                                onclick: function () {
                                    this.exportChart();
                                }
                            },
                            printButton: {
                                enabled: false
                            }
                        }
                    },
                    plotOptions: {
                        bar: {
                            dataLabels: {
                                enabled: true,
                                formatter: function () {
                                    if (this.y != 0) {
                                        return this.y + "%";
                                    }
                                }
                            },
                            showInLegend: false
                        }
                    },
                    //legend: {
                    //    layout: 'vertical',
                    //    align: 'right',
                    //    verticalAlign: 'top',
                    //    x: -40,
                    //    y: 100,
                    //    floating: true,
                    //    borderWidth: 1,
                    //    backgroundColor: '#FFFFFF',
                    //    shadow: true
                    //},
                    credits: {
                        enabled: false
                    },
                    series: [
                        {
                            name: '满意度',
                            // data: [107, 31, 635, 203, 2]
                            data: dt
                        }
                    ]
                });
            }
        });
    </script>
</body>
</html>
