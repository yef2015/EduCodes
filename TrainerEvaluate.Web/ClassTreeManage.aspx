<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="ClassTreeManage.aspx.cs" Inherits="TrainerEvaluate.Web.ClassTreeManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .mymenu {
            font-size:12px;
            padding-left:20px;
        }
        .myslist {
            padding-left:40px;
        }

        a { color: #000000; } 

            a:link {
                color: #000000;
                TEXT-DECORATION: none;
            }

            a:visited {
                color: #000000;
                TEXT-DECORATION: none;
            }

            a:hover {
                color: #000FA5;
                TEXT-DECORATION: none;
            }
    </style>

    <script type="text/javascript">
       
        function changeyear(key)
        {
            var url = "ClassManageList.aspx?key=" + key;
            $("#ifrMgrContent").attr("src", url);
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="msysmgr" name="sysmgr" style="height: 1271px; font-size: 14px; margin-left: 5px;width:263px;">
        <div class="mymenu">
            <h2 class="myl1">十二五</h2>
            <div class="myslist">
                <h3 class="myl2"><a href="#" onclick="changeyear(2014)">2014年</a></h3>
                <h3 class="myl2"><a href="#" onclick="changeyear(2015)">2015年</a></h3>
            </div>
            <h2 class="myl1">十三五</h2>
            <div class="myslist">
                <h3 class="myl2"><a href="#" onclick="changeyear(2016)">2016年</a></h3>
                <h3 class="myl2"><a href="#" onclick="changeyear(2017)">2017年</a></h3>
                <h3 class="myl2"><a href="#" onclick="changeyear(2018)">2018年</a></h3>
                <h3 class="myl2"><a href="#" onclick="changeyear(2019)">2019年</a></h3>
                <h3 class="myl2"><a href="#" onclick="changeyear(2020)">2020年</a></h3>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<%--    <iframe id="ifrMgrContent" width="100%" height="1271" src="ClassManageList.aspx?key=2015" frameborder="0" scrolling="auto"> 
    </iframe>   --%>
    <iframe id="ifrMgrContent" width="100%" height="1271" frameborder="0" scrolling="auto" src="ClassManageList.aspx?key="> 
    </iframe>  
</asp:Content>