<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="Mags.aspx.cs" Inherits="TrainerEvaluate.Web.Mags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <input class="easyui-textbox" id="ss1" data-options="multiline:true" style="width: 350px; height: 150px;" />
    <input class="easyui-textbox" id="ss2"  style="width: 350px;" />

    <a href="javascript:void(0)" class="easyui-linkbutton c6" iconcls="icon-ok" onclick="queryu()" style="width: 90px">查询</a>
     
     <input class="easyui-textbox" id="ss3" data-options="multiline:true" style="width: 350px; height: 150px;" />

    <script type="text/javascript">
        function queryu() {
            var data = {
                s1: encodeURIComponent($('#ss1').textbox("getText")),
                s2: encodeURIComponent($('#ss2').textbox("getText"))
            };
            $.post("Mags.ashx",data,function(result) { 
                $("#ss3").textbox("setText", result);
            }); 
        } 
    </script> 

</asp:Content>
