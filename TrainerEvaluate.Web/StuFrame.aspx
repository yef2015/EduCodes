<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.Master" AutoEventWireup="true" CodeBehind="StuFrame.aspx.cs" Inherits="TrainerEvaluate.Web.StuFrame" %>
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

        function changeStudent(key) {
            var url = "StudentList.aspx?key=" + key; //StudentList.aspx ClassManageList.aspx
            $("#ifrMgrContent").attr("src", url);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="msysmgr" name="sysmgr" style="height: 1200px; font-size: 14px; margin-left: 5px;width:263px;padding-top:5px;">
        <ul id="tt" class="easyui-tree" data-options="
				url: 'StudentsInfo.ashx?t=stree',
				method: 'post',
				animate: true,
                onClick: function(node){
                    changeStudent(node.text);
				}
			"></ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <iframe id="ifrMgrContent" width="100%" height="1200" frameborder="0" scrolling="auto" src="StudentList.aspx?key=all"> 
    </iframe>  
</asp:Content>
