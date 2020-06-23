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
	Dim dbMain,strSQL,rsData,intProdid
	Set dbMain =  OpenConnection
	IF LEN(ltrim(Request("intUPC")))>0 then
		strSQL="SELECT ProdID,Descript"&_
		" FROM Product (nolock) "&_
		" WHERE bcode ='" & Request("intUPC") &"'"
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			If Not rsData.EOF Then
				intProdid =  cstr(rsData("Prodid"))&"|"&trim(rsData("Descript"))
			End If
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
<form method="POST" name="frmMain" action="CashOutSelItemUPC.asp">
<input type="hidden" name="intProdid" value="<%=intProdid%>">
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
<script language="VBSCRIPT">
Option Explicit

Sub Window_Onload()
	window.returnValue =  document.all("intProdid").value
	Window.close
End Sub

</script>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
