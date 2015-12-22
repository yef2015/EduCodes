<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TrainerEvaluate.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>中小学干部培训评估系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="Styles/pgxt.css" type="text/css" /> 
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript">
        function login() {
            if ($("#useraccount").val() == "") {
                alert("请输入用户名！");
                //  $("#msg").val("请输入用户名！");
            } else if ($("#pwd").val() == "") {
                alert("请输入用户名！");
            } else {
                $.ajax({
                    type: "POST",
                    dataType: "text",
                    async: false,
                    url: 'LogInHandler.ashx',
                    data: "acc=" + $("#useraccount").val() + "&pwd=" + $("#pwd").val(),
                    success: function (data) {
                        if (data == "1") {
                            //  window.location.href = "QuestionnaireManage.aspx";
                            window.location.href = "default.aspx";
                            $("#ifrcont").attr("src", "QuestionnaireManage.aspx");
                        } else {
                            alert("用户名或密码错误！");
                        }
                    },
                    error: function (data) {
                        alert(data);
                    }
                });
            }
        }


        function pwdChange() {
            login();
        }

    </script>


</head>

<body bgcolor="#FFFFFF" text="#000000" topmargin="0" leftmargin="0">

    <br>
    <br>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" background="images/back1.gif" align="center">
        <tr>
            <td width="100%" background="images/index4.jpg">
                <table width="820" border="0" cellspacing="0" cellpadding="0" align="center" valign="top">
                    <tr>
                        <td height="110">
                            <img src="images/index1.jpg" width="850" height="151"></td>
                    </tr>
                </table>
                <table width="848" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td width="435">
                            <img src="images/index2a.jpg" width="435" height="138"></td>
                        <td width="186" background="images/index2b.jpg" valign="top">
                            <table width="189" border="0" cellspacing="0" cellpadding="0" height="103">
                                <tr>
                                    <td height="14" width="58">
                                        <img src="images/bank.gif" width="58" height="23"></td>
                                    <td height="14" width="131">
                                        <img src="images/bank.gif" width="180" height="23"></td>
                                </tr>
                                <tr>
                                    <td height="10" width="58">&nbsp;</td>
                                    <td height="10" width="131">
                                        <input type="text" name="useraccount" id="useraccount" size="22">
                                    </td>
                                </tr>
                                <tr>
                                    <td height="3" width="58">
                                        <img src="images/bank.gif" width="50" height="10"></td>
                                    <td height="3" width="131">
                                        <img src="images/bank.gif" width="100" height="10"></td>
                                </tr>
                                <tr>
                                    <td width="58">&nbsp;</td>
                                    <td width="131">
                                        <input type="password" name="pwd" id="pwd" size="23" onchange="pwdChange()">
                                    </td>
                                </tr>
                                <tr>
                                    <td width="58" height="6">&nbsp;</td>
                                    <td width="131" height="6">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="58">&nbsp;</td>
                                    <td width="131"> 
                                        <input type="image" border="0" name="imageField" src="images/regist.jpg" width="168" height="27" onclick="login()">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="371">
                            <img src="images/index2c.jpg" width="178" height="138"></td>
                    </tr>

                </table>
                <table width="765" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td> 
                            <img src="images/index3.jpg" width="850" height="169">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td background="images/back1.gif" height="30">
                <div align="center" class="white9">北京市海淀区教育党校 北京市海淀区中小学干部研修中心 版权所有 </div>
            </td>
        </tr>
    </table>

</body>

</html>
