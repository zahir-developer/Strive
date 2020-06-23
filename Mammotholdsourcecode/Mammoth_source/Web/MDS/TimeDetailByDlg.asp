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
Dim Title,intrecID,hdnStatus

'********************************************************************
' Main
'********************************************************************
if len(request("intrecID"))>0 then
	intrecID = request("intrecID")
ELSE
	intrecID = 0 
END IF


'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Detail By</title>
<input type="hidden" name="intRECID" value="<%=intRECID%>">
<input type="hidden" name="hdnStatus" value="<%=hdnStatus%>">
</head>
<body class="pgbody">
<div align="center">
<table  border="0" width="625" cellspacing="0" cellpadding="0">
<iframe align="center" Name="fraMain" src="timeDetailByDlgFra.asp?intrecID=<%=intrecID%>"  height="450" width="610" frameborder="0"></iframe>
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
	IF hdnStatus.value then
		window.returnValue = true 
	ELSE
		window.returnValue = False 
	END IF
End Sub
</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************

%>
