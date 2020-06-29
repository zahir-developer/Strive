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
Dim Title,intcheckAmt,hdnStatus

'********************************************************************
' Main
'********************************************************************
intcheckAmt = request("intcheckAmt")


'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Check</title>
<input type="hidden" name="intcheckAmt" value="<%=intcheckAmt%>">
</head>
<body class="pgbody">
<div style="text-align:center">
<table  border="0" width="420" cellspacing="0" cellpadding="0">
<iframe align="center" Name="fraMain" src="CashOutCheckDlgFra.asp?intcheckAmt=<%=intcheckAmt%>"  height="450" width="420" frameborder="0"></iframe>
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
