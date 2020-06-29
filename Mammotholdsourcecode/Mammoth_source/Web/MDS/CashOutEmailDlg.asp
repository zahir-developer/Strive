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
'********************************************************************
' Global Variables
'********************************************************************
Dim Title,intClientID,hdnStatus

'********************************************************************
' Main
'********************************************************************
intClientID = request("intClientID")


'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Client Email Entry</title>
<input type="hidden" name="intClientID" value="<%=intClientID%>">
</head>
<body class="pgbody">
<div style="text-align:center">
<table  border="0" width="100%" cellspacing="0" cellpadding="0">
<iframe align="center" Name="fraMain" src="CashOutEmailFra.asp?intClientID=<%=intClientID%>"  height="180" width="450" frameborder="0"></iframe>
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
