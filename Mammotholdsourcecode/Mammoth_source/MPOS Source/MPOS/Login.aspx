<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MPOS.Login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MPOS Login</title>
    <link href="~/Content/Site.css" rel="stylesheet" />
    <link href="~/Content/Styles.css" rel="stylesheet" />
    <style type="text/css">
        .loginbutton {
        }
    </style>
</head>
<body class="body">
    <form id="form1" runat="server">

         <telerik:RadSkinManager ID="QsfSkinManager" runat="server" ShowChooser="false" />
        <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All" EnableRoundedCorners="false" />
       <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <center>
            <div id="top" style="padding-top: 50px; padding-bottom: 50Px; width: 100%">

                                 <telerik:RadTextBox ID="txtUsername" runat="server" Label="Username" Font-Bold="true" Width="196px" />&nbsp;&nbsp;&nbsp;
                                <telerik:RadTextBox ID="txtPassword" runat="server" Label="Password" Font-Bold="true" TextMode="Password" Width="184px" />&nbsp;&nbsp;&nbsp;
<%--                    <telerik:RadButton  OnClick="SubmitBtn_Clicked" ID="Button" runat="server" Text="Login" Height="43px" Width="92px" style="margin-top: 0px" />--%>
                <asp:Button id="Button" OnClick="SubmitBtn_Clicked" runat="server" Text="Login" Width="80px" />
                </div>

            <div>
                <img id="piclogo" runat="server" src="~/Images/MammothLogo.gif" style="align-self: center; text-align: center;" width="604" height="481" alt="Logo" />
            </div>
                </center>
    </form>
</body>
</html>
