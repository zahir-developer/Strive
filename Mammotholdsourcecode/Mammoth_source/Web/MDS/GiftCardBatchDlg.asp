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
Dim Title,LocationID,LoginID

'********************************************************************
' Main
'********************************************************************

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
<Title>Gift Card Batch</title>
</head>
<body class="pgbody">
            <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />

<div style="text-align:center">
<table  border="0" width="320" cellspacing="0" cellpadding="0">
<iframe align="center" Name="fraMain" src="GiftCardBatchDlgFra.asp?LocationID=<%=LocationID%>&LoginID=<%=LoginID%>"  height="300" width="320" frameborder="0"></iframe>
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
