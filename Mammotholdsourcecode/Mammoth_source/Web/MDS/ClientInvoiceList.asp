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

Dim dbMain, strLoc,hdnFilterBy
Set dbMain =  OpenConnection
hdnFilterBy = Request("hdnFilterBy")

'********************************************************************
' HTML
'********************************************************************
%>

<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
</head>
<body class=pgbody Onclick="SetDirty" onkeyup="SetDirty()">
<form method="POST" name="frmMain" action="ClientInvoiceList.asp"> 
<input type=hidden name=FormAction value=>
<input type=hidden name=hdnFilterBy value="<%=hdnFilterBy%>">
<input type=hidden name=strLoc value="<%=strLoc%>">
<div align=center>
<table class="tblcaption" cellspacing=0 cellpadding=0 width=620>
	<tr>
		<td align=center class="tdcaption" background="images/header.jpg" width=210>Corporate Client Statement List</td>
		<td align=right>&nbsp;			
		</td>
	</tr>
</table>
<iframe Name="fraMain" src="admLoading.asp" scrolling=yes height="275" width="620" frameborder="0"></iframe>
</form>
</body>
</html>
<%

'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">
Option Explicit
Sub Window_Onload()
	fraMain.location.href = "ClientInvoiceListFra.asp"
End Sub

</script>
<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************

%>
