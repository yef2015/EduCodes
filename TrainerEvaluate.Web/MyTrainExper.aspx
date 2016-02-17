<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="MyTrainExper.aspx.cs" Inherits="TrainerEvaluate.Web.MyTrainExper" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
    <div style="margin: 10px; width: 99%">  
        <table id="dg" title="参加培训班级" class="easyui-datagrid" style="width: 99%"
            url="ClassInfo.ashx?t=stcl&studentId=<%= UserId %>"
            pagination="true">
            <thead>
                <tr>
                    <th field="ID" width="0" hidden="true">编号</th>
                    <th field="Name" width="150">班级名称</th>
                    <th field="Object" width="200" sortable="true">培训对象</th>
                    <th field="Description" width="250" sortable="true">培训内容</th>
                    <th field="StartDate" width="150" sortable="true" formatter="formatterdate">开始日期</th>
                    <th field="FinishDate" width="150" sortable="true" formatter="formatterdate">结束日期</th>
                    <th field="Students" width="80" sortable="true">学员人数</th>
                    <th field="Point" width="80" sortable="true">学时</th>
                    <th field="Teacher" width="120" sortable="true">项目负责人</th>
                </tr>
            </thead>
        </table>
    </div>
</asp:Content>
