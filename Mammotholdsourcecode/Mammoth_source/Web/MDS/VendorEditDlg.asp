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
Dim Title,hdnStatus,intVenID

'********************************************************************
' Main
'********************************************************************
intVenID = Request("intVenID")

'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Vendor Add/Edit </title>
<input type="hidden" name="hdnStatus" value="<%=hdnStatus%>">
</head>
<body class="pgbody">
<div style="text-align:center">
<table  border="0" width="515" cellspacing="0" cellpadding="0">
<iframe align="center" Name="fraMain" src="VendorEditDlgFra.asp?intVenID=<%=intVenID%>"  height="300" width="570" frameborder="0"></iframe>
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
