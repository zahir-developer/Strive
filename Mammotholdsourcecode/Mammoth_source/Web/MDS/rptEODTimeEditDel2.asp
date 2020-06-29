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
<%
Main
Sub Main
	Dim dbMain,strSQL
	Set dbMain =  OpenConnection
	IF LEN(ltrim(Request("intclockID")))>0 then
		strSQL= " Delete timeclock Where ClockID = " &  Request("intclockID") & " and LocationID=" & Request("LocationID")
		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
			
	END IF
'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
</head>
<body class="pgbody">
</body>
</html>
<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">
Option Explicit

Sub Window_Onload()
	Window.close
End Sub

</script>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
