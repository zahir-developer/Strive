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
Dim Title,intProdID,hdnStatus

'********************************************************************
' Main
'********************************************************************
intProdID = request("intProdID")


'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Select Opt</title>
<input type="hidden" name="intProdID" value="<%=intProdID%>">
</head>
<body class="pgbody">
<div align="center">
<table  border="0" width="250" cellspacing="0" cellpadding="0">
<iframe align="center" Name="fraMain" src="NewDetailSelOptFra.asp?intProdID=<%=intProdID%>"  height="110" width="230" frameborder="0"></iframe>
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
