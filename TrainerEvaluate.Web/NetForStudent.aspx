<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="NetForStudent.aspx.cs" Inherits="TrainerEvaluate.Web.NetForStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        ul, li {
            list-style-type: none;
            margin: 5px;
            color: #000000;
            cursor: pointer;
            font-size: 15px;
            font-weight: bold;
        }
    </style>


    <script type="text/javascript">
        $(document).ready(function () {
            $("#sysmanage li").click(function () {
                var key = parseInt($(this).attr('value'));
                var url = "";
                for (var i = 0; i < $("#sysmanage  li").length; i++) {
                    if ($("#sysmanage  li")[i].value == key) {
                        $("#sysmanage  li")[i].style.color = "#000000";
                        $("#sysmanage  li")[i].style.fontWeight = "bold";
                        $("#sysmanage  li")[i].style.fontSize = "15px";
                    } else {
                        $("#sysmanage  li")[i].style.color = "#000000";
                        $("#sysmanage  li")[i].style.fontWeight = "bold";
                        $("#sysmanage  li")[i].style.fontSize = "15px";
                    }
                }
                switch (key) {
                    case 1:
                        url = "NetForStudentJoin.aspx";
                        break;
                    case 2:
                        url = "NetForStudentYet.aspx";
                        break;
                    case 3:
                        url = "NetForStudentEve.aspx";
                        break;                    
                    default:
                        break;
                }
                $("#ifrMgrContent").attr("src", url);
            });
        });


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="msysmgr" name="sysmgr" style="width: 100%; height: 1200px; font-size: 14px; margin-left: 5px;">
        <ul id="sysmanage">
            <li value="1">我已参加</li>
            <li value="2">我已报名</li>
            <li value="3">我要报名</li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <iframe id="ifrMgrContent" width="100%" height="1200" frameborder="0" scrolling="auto" src="NetForStudentEve.aspx"> 
    </iframe>   
</asp:Content>
