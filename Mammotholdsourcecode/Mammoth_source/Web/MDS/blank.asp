<%@ LANGUAGE="VBSCRIPT" %>
<%
'********************************************************************
Option Explicit
Response.Expires = 0
Response.Buffer = True
'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incDatabase.asp"-->
<!--#include file="incCommon.asp"-->
<%
'********************************************************************
' Global Variables
'********************************************************************
Dim Title
'********************************************************************
' Main
'********************************************************************
Call Main

'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
Sub Main


'********************************************************************
' HTML
'********************************************************************
Title = ""
%>

<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
</head>
<body class="pgBody">
<table BORDER=0 WIDTH=100% height=98% CELLPADDING=0 CELLSPACING=0>
<tr>
<td>
<div style="text-align:center">
	<label></label>
</div>
</td>
</tr>
</table>
</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">
Option Explicit

Sub window_onload()
End Sub


</script>


<%

End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************


%>
