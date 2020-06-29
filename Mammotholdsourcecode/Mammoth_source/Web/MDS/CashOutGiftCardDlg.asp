<%@  language="VBSCRIPT" %>
<%
'********************************************************************
' Name: 
'********************************************************************
Option Explicit
Response.Expires = 0
Response.Buffer = True

'********************************************************************
' Include Files
'********************************************************************
%>
<%
'********************************************************************
' Global Variables
'********************************************************************
Dim Title,intGiftCardAmt,intRecID,LocationID,LoginID

intGiftCardAmt=Request("intGiftCardAmt")
intRecID=Request("intRecID")
LocationID = request("LocationID")
LoginID = request("LoginID")
%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css">
    <title>Gift Card</title>
</head>
<body class="pgbody">
    <div style="text-align: center">
        <input type="hidden" name="intGiftCardAmt" tabindex="-2" value="<%=intGiftCardAmt%>" />
        <input type="hidden" name="intRecID" tabindex="-2" value="<%=intRecID%>" />
        <input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
        <iframe name="fraMain" src="CashOutGiftCardDlgFra.asp?intGiftCardAmt=<%=intGiftCardAmt%>&intRecID=<%=intRecID%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>" style="height: 400px; width=: 320px" frameborder="0"></iframe>
    </div>
</body>
</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
