<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionExpired.aspx.cs" Inherits="MPOS.SessionExpired" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Session Expired</title>
     <link href="~/Content/Site.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
 </head>
<body>
    <form id="form1" runat="server">
    <div>
                <p class="auto-style1">
                    <br />
                    <br />

                    <span>Your session has expired due to inactivity.</span>
                    <br />
                    <br />
                    <a href="Login.aspx">Click here to login again</a>
                    <br />
                    <br />

                </p>
  
    </div>
    </form>
</body>
</html>
