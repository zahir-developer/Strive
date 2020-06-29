<%@ LANGUAGE="VBSCRIPT" %>
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
<!--#include file="incDatabase.asp"-->
<%
'********************************************************************
' Global Variables
'********************************************************************
Dim Title,intGiftCardID,strCurrentAmt,LocationID,LoginID

'********************************************************************
' Main
'********************************************************************
intGiftCardID=Request("intGiftCardID")
strCurrentAmt=Request("strCurrentAmt")
    LocationID = request("LocationID")
    LoginID = request("LoginID")

'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Gift Card Activity</title>
</head>
<body class="pgbody">
<input type="hidden" name="intGiftCardID" tabindex="-2" value="<%=intGiftCardID%>">
<input type="hidden" name="strCurrentAmt" tabindex="-2" value="<%=strCurrentAmt%>">
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<div style="text-align:center">
<table  border="0" width="320" cellspacing="0" cellpadding="0">
<iframe align="center" Name="fraMain" src="GiftCardActDlgFra.asp?intGiftCardID=<%=intGiftCardID%>&strCurrentAmt=<%=strCurrentAmt%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>"   height="320" width="320" frameborder="0"></iframe>
</table>
</div>
</body>
</html>
<%

'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">
Option Explicit

Sub Window_onUnload()
End Sub
</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************

%>
