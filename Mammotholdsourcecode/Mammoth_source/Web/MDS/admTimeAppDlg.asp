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
	Dim dbMain,hdnSheetID,hdnUserID,strSQL,rs,intClockid,strSQL2,LocationID
	Set dbMain =  OpenConnection
	hdnSheetID = Request("hdnSheetID")
    LocationID = request("LocationID")


	IF LEN(ltrim(hdnSheetID))>0 then
		strSQL2= " Update timeSheet SET "&_
			" status = 50"&_
			" Where SheetID = " & hdnSheetID &" AND LocationID = "& LocationID
		IF NOT DBExec(dbMain, strSQL2) then
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
