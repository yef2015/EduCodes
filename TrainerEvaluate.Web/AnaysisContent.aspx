<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnaysisContent.aspx.cs" ValidateRequest="false" Inherits="TrainerEvaluate.Web.AnaysisContent" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>��Сѧ�ɲ���ѵ����ϵͳ</title>
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
            var url = "QuestionnaireHadler.ashx?t=r&id=" + $("#hCourseid").val() + "&classId=" + +$("#hClassid").val();
            window.location = url;
        }

        function getSuggestion() {
            $('#dgSug').datagrid('reload', 'QuestionnaireHadler.ashx?t=s&coId=' + $("#hCourseid").val());
            $('#dlgSuggest').dialog('open').dialog('setTitle', '�����ʾ�����ѧԱ����');
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
        // ������ʦ�����
        function getTrainTeachReports() {
            var url = "QuestionnaireHadler.ashx?t=extrainteach";
            window.location = url;
        }
        // ������ѵ�γ������
        function getTrainCourseReports() {
            var url = "QuestionnaireHadler.ashx?t=extraincourse";
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
            <span id="theYear" runat="server" style="font-size: 25px; font-weight: bold">2013��������ɲ�����������ѵ��γ�������</span><br />
        </div>
        <table width="98%" border="0" cellspacing="1" cellpadding="3" align="center" bgcolor="C4D4E1" id="analysisTable" runat="server">
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">�γ����ƣ�</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <div runat="server" id="courseName">XXX�γ���ѵ</div>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">��ѵ�ص㣺 </div>
                </td>
                <td width="34%" bgcolor="F0F9FF" height="25" class="gray10a" colspan="3">
                    <div runat="server" id="coursePlace">������У</div>
                </td>
            </tr>
            <tr bgcolor="#FFFFFF">
                <td width="16%" class="gray10a" height="26">
                    <div align="center">��ѵ��ʦ��</div>
                </td>
                <td width="35%" class="gray10a" height="26">
                    <div runat="server" id="teacherName">��һɽ</div>
                </td>
                <td width="15%" class="gray10a" height="26">
                    <div align="center">��ѵʱ�䣺</div>
                </td>
                <td width="34%" class="gray10a" height="26"  colspan="3">
                    <div runat="server" id="trainTime">2014-9-20</div>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">Ӧ��������</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <div runat="server" id="totalPeople">0</div>
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">ʵ�������� </div>
                </td>
                <td width="11%" bgcolor="FFFFFF" height="25" class="gray10a"  >
                    <div runat="server" id="totalDone">0</div>
                </td>
                  <td width="11%" bgcolor="FFFFFF" height="25" class="gray10a"  >
                    <div align="center">��������</div>
                </td>
                  <td width="12%" bgcolor="FFFFFF" height="25" class="gray10a" >
                    <div runat="server" id="evProgress"  onclick="getNoFinish();" style="cursor: pointer">0</div>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">��ƽ���֣�</div>
                </td>
                <td width="35%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <div runat="server" id="spTotalAvg">0 �֣�����52��</div>
                </td>
                <td width="15%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">����ȣ� </div>
                </td>
                <td width="11%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <div runat="server" id="satisfaction">0</div>
                </td>
                  <td width="11%" bgcolor="F0F9FF" class="gray10a" height="25">
                    <div align="center">�ȼ�: </div>
                </td>
                <td width="12%" bgcolor="F0F9FF" height="25" class="gray10a">
                    <div runat="server" id="level">0</div>
                </td>
            </tr>
            <tr>
                <td width="16%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">�γ����� ��</div>
                </td>
                <td width="35%" bgcolor="FFFFFF" height="25" class="gray10a">
                   <div runat="server" id="divCourseContent">0</div>
                </td>
                <td width="15%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">��ѵ��ʦ�� </div>
                </td>
                <%--<td width="34%" bgcolor="FFFFFF" height="25" class="gray10a">δ��� <span runat="server" id="nofinish" onclick="getNoFinish()" style="cursor: pointer; color: red">0</span> ��
                </td>--%>
                   <td width="11%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <div runat="server" id="divTeacher">0</div>
                </td>
                   <td width="11%" bgcolor="FFFFFF" class="gray10a" height="25">
                    <div align="center">����֯����: </div>
                </td>
                <td width="12%" bgcolor="FFFFFF" height="25" class="gray10a">
                    <div runat="server" id="divOrg">0</div>
                </td>
            </tr>


            <tr bgcolor="4A5C69">
                <td height="21" colspan="7" class="white10">��ϸ�����</td>
            </tr>
            <tr>
                <td colspan="7" bgcolor="#FFFFFF">
                    <div id="details" runat="server"></div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="7" bgcolor="#FFFFFF">
                    <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="getReport()" style="width: 180px">�����������浥��ѧԱ����</a>
                </td>
                <%--  <td align="left" colspan="2"  bgcolor="#FFFFFF"> 
                    <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="getSuggestion()" style="width: 120px">����ѧԱ����</a>
                </td>--%>
            </tr>
        </table>

        <div style="background-color: #FFFFFF; width: 98%; height: 900px; margin: 0 auto;" id="container1" runat="server">
        </div>
        <%--<div>
            <a  class="easyui-linkbutton c6" iconcls="icon-ok"  style="width: 120px" id="exportImg">����</a>
        </div>--%>
        <div style="background-color: #FFFFFF; width: 98%; margin: 0 auto;" id="divReports" runat="server">
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
            <div  title="�����ѧԱ" tyle="padding: 10px">
                <div class="ftitle">ѧԱ��Ϣ��</div>
                <table id="dg12" class="easyui-datagrid" toolbar="#toolbarf2"
                    data-options="rownumbers:true,singleSelect:false,url:'QuestionnaireHadler.ashx?t=f2&coId='+$('#hCourseid').val()+'&classId='+$('#hClassid').val(),method:'get',checkOnSelect:false, pagination:true">
                    <thead>
                        <tr>
                            <th data-options="field:'StudentId'" hidden="true">StudentId</th>
                            <th data-options="field:'StuName'">ѧ������</th>
                            <th data-options="field:'School'">����ѧУ</th>
                            <th data-options="field:'TelNo'">��ϵ�绰</th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div title="δ���ѧԱ" style="padding: 10px">
                <div class="ftitle">ѧԱ��Ϣ��</div>
                <table id="dg1" class="easyui-datagrid" toolbar="#toolbar"
                    data-options="rownumbers:true,singleSelect:false,url:'QuestionnaireHadler.ashx?t=f&coId='+$('#hCourseid').val()+'&classId='+$('#hClassid').val(),method:'get',checkOnSelect:false, pagination:true">
                    <thead>
                        <tr>
                            <th data-options="field:'StudentId'" hidden="true">StudentId</th>
                            <th data-options="field:'StuName'">ѧ������</th>
                            <th data-options="field:'School'">����ѧУ</th>
                            <th data-options="field:'TelNo'">��ϵ�绰</th>
                        </tr>
                    </thead>
                </table> 
            </div>
        </div>
    </div>
   
    <div id="toolbar">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="exportStuNoFinish()">����</a>
    </div> 
    
     <div id="toolbarf2">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="exportStuFinish()">����</a>
    </div> 
    

    <div id="dlgSuggest" class="easyui-dialog" style="width: 400px; height: 400px; padding: 10px 20px"
        closed="true" buttons="#dlg-buttons">
        <div class="ftitle">ѧԱ������Ϣ��</div>

        <table id="dgSug" class="easyui-datagrid" toolbar="#toolbar1"
            data-options="rownumbers:true,singleSelect:false,url:'QuestionnaireHadler.ashx?t=s&coId='+$('#hCourseid').val(),method:'get',checkOnSelect:false, pagination:true">
            <thead>
                <tr>
                    <th data-options="field:'AppraiserId'" hidden="true">StudentId</th>
                    <th data-options="field:'StuName'">ѧԱ����</th>
                    <th data-options="field:'Suggest'">��������</th>
                </tr>
            </thead>
        </table>
    </div>
    <div id="toolbar1">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="exportSuggestion()">����</a>
    </div>

    <script type="text/javascript">

        $.ajaxSetup({
            cache: false //�ر�AJAX��Ӧ�Ļ���
        });

        if ($.fn.pagination) {
            $.fn.pagination.defaults.beforePageText = '��';
            $.fn.pagination.defaults.afterPageText = '��{pages}ҳ';
            $.fn.pagination.defaults.displayMsg = '��ʾ{from}��{to},��{total}��¼';
        }


        function getNoFinish() {
            // if ($('#nofinish').text() != "0") {
            $('#dlg').dialog('open').dialog('setTitle', 'ѧԱ������');
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
                        name: '����ȵ���',
                        data: [
                            ['������', top2],
                            ['����', top3],
                            ['һ��', top4],
                            ['������', top5]
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
                    name: '��ƽ����',
                    data: [total1, total2, total3, total4, total5]
                }]
            });
        });--%>




        $(function () {
            if ($('#container1').css("display") == "block") {
                //var categ = ['�γ�1', '�γ�2', '�γ�3', '�γ�4', '�γ�5'];
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
                        text: '�γ�����ȷֲ�'
                    },
                    subtitle: {
                        text: ''
                    },
                    xAxis: {
                        //categories: ['�γ�1', '�γ�2', '�γ�3', '�γ�4', '�γ�5'],
                        categories: categ,
                        title: {
                            text: null
                        }
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: '�ٷֱ�',
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
                                text: '��������',
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
                            name: '�����',
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
