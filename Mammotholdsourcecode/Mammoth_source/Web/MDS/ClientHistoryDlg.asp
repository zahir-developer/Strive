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
Main
Sub Main
	Dim dbMain, hdnClientID
	Set dbMain =  OpenConnection


	hdnClientID = Request("hdnClientID")
	

'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Client Account History</title>
</head>
<body class="pgbody">
<form method=post name="frmMain">
        <input type=hidden name="hdnClientID" value="<%=hdnClientID%>">
<div style="text-align:center">
<iframe style="text-align:center" Name="fraMain" src="ClientHistoryDlgFra.asp?hdnClientID=<%=hdnClientID%>"   height="300px" width="600px" frameborder="0"></iframe>
</div>
</form>
</body>
</html>
<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/VBSCRIPT">
Option Explicit

Sub Window_Onload()
End Sub

</script>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
