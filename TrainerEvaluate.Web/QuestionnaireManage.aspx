<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="QuestionnaireManage.aspx.cs" Inherits="TrainerEvaluate.Web.QuestionnaireManage" %>


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
            $("#questionmanage li").click(function () {
                var key = parseInt($(this).attr('value'));
                var url = "";
                for (var i = 0; i < $("#questionmanage  li").length; i++) {
                    if ($("#questionmanage  li")[i].value == key) {
                        $("#questionmanage  li")[i].style.color = "#000000";
                        $("#questionmanage  li")[i].style.fontWeight = "bold";
                        $("#questionmanage  li")[i].style.fontSize = "15px";
                    } else {
                        $("#questionmanage  li")[i].style.color = "#000000";
                        $("#questionmanage  li")[i].style.fontWeight = "bold";
                        $("#questionmanage  li")[i].style.fontSize = "15px";
                    }
                }
                switch (key) {
                    case 2:
                        url = "QuestionnairTreeContent.aspx";
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
     <div id="questionmgr" name="questionmgr" style="width: 100%; height: 1271px; font-size: 14px; margin-left: 5px;padding-top:20px;">
        <ul id="questionmanage">
            <li value="2">问卷内容管理</li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
    <iframe id="ifrMgrContent" width="100%" height="1271" frameborder="0" scrolling="auto" src="QuestionnairManageShow.aspx"> 
    </iframe>   
    
</asp:Content>
