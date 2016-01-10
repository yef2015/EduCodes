<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="MyProfessExper.aspx.cs" Inherits="TrainerEvaluate.Web.MyProfessExper" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div style="margin: 10px; width: 99%">  
        <table id="dg" title="任教经历信息" class="easyui-datagrid" style="width: 99%"
            url="ClassInfo.ashx?t=stcp&teacherId=<%= UserId %>"
             pagination="true"
            rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="Id" width="0" hidden="true">编号</th>
                    <th field="CourseName" width="100">课程名称</th>
                    <th field="TeachPlace" width="100" sortable="true">授课地点</th>
                    <th field="ClassName" width="100" sortable="true">班级名称</th>
                    <th field="Object" width="100" sortable="true">培训对象</th>
                    <th field="Description" width="100" sortable="true">培训内容</th>
                    <th field="StartDate" width="100" sortable="true" formatter="formatterdate">开始日期</th>
                    <th field="FinishDate" width="100" sortable="true" formatter="formatterdate">结束日期</th>
                    <th field="StudentCount" width="100" sortable="true">学员人数</th>
                    <th field="Point" width="100" sortable="true">学时</th>
                    <th field="Teacher" width="100" sortable="true">项目负责人</th>
                </tr>
            </thead>
        </table>
    </div>
</asp:Content>
