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
	Dim dbMain,strSQL,rsData, hdnZip,strcity,strSt
	Set dbMain =  OpenConnection

	hdnZip = Request("hdnZip")
	
	strSQL = " Select city,st from LM_ZIPCODES (NOLOCK) where Right('00000'+rtrim(ZIP),5) = '"& hdnZip & "'"
	If dbOpenRecordSet(dbmain,rsData,strSQL) Then
		If Not rsData.EOF Then
			strcity = rsData("city")
			strSt = rsData("st")
		END IF
	End if
	Set rsData = Nothing

'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Zip Find</title>
</head>
<body class="pgbody">
<input type="hidden" name="strcity" tabindex="-2" value="<%=strcity%>">
<input type="hidden" name="strSt" tabindex="-2" value="<%=strSt%>">
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
	window.returnvalue = trim(strCity.value) &"|"& trim(strSt.value)
	Window.close
End Sub

</script>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
