<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IPOD_Login.aspx.cs" Inherits="MPOS.IPOD_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MPOS IPOD Login</title>
    <link href="~/Content/Site.css" rel="stylesheet" />
    <link href="~/Content/Styles.css" rel="stylesheet" />
    <style type="text/css">
        .loginbutton {
        }
    </style>
</head>
<body style="align-content: center; max-width: 640px; max-height: 1136px">
    <form id="form1" runat="server">
        <div>
            <telerik:RadSkinManager ID="QsfSkinManager" runat="server" ShowChooser="false" />
            <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All" EnableRoundedCorners="false" />
            <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
            <div id="top" style="padding-top: 50px; padding-bottom: 50px; width: 640px; text-align: center">
                <p>
                    <telerik:RadTextBox ID="txtUsername" runat="server" Label="Username: " Font-Bold="true" Width="196px" />
                </p>
                <p>
                    <telerik:RadTextBox ID="txtPassword" runat="server" Label="Password: " Font-Bold="true" TextMode="Password" Width="184px" />
                </p>
                <p>
                    <asp:Button ID="Button" OnClick="SubmitBtn_Clicked" runat="server" Text="Login" Width="80px" />
                </p>
            </div>
        </div>
    </form>
</body>
</html>
