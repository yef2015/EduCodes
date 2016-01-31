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

        .tree-title {
            font-size:12px;
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
    <div id="msysmgr" name="sysmgr" style="height: 1200px; font-size: 14px; margin-left: 5px;width:263px;padding-top:5px;">
        <ul class="easyui-tree">
            <li>
                <span>班级管理</span>
                <ul>
                    <li>
                        <span>十二五</span>
                        <ul>
                            <li>
                                <span><a onclick="changeyear(2014)">2014年</a></span>
                            </li>
                            <li>
                                <span><a onclick="changeyear(2015)">2015年</a></span>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <span>十三五</span>
                        <ul>
                            <li><a onclick="changeyear(2016)">2016年</a></li>
                            <li><a onclick="changeyear(2017)">2017年</a></li>
                            <li><a onclick="changeyear(2018)">2018年</a></li>
                            <li><a onclick="changeyear(2019)">2019年</a></li>
                            <li><a onclick="changeyear(2020)">2020年</a></li>
                        </ul>
                    </li>
                </ul>
            </li>
        </ul>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <iframe id="ifrMgrContent" width="100%" height="1200" frameborder="0" scrolling="auto" src="ClassManageList.aspx?key="> 
    </iframe>  
</asp:Content>