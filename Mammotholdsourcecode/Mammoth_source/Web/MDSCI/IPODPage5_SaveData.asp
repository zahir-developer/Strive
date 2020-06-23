<%@  language="VBSCRIPT" %>
<%
'********************************************************************
Option Explicit
Response.Expires = 0
Response.Buffer = True
Response.ContentType = "application/json"
'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incDatabase.asp"-->
<%
Dim dbMain,strSQL,rsData,strSQL2,rsData2,Jout,blnUpdatePass,strAddService
Set dbMain =  OpenConnection

strAddService = trim(UCase(Request("AddService")))


blnUpdatePass = 1
strSQL2= " Update ScanIn set AddService='"& strAddService &"' WHERE (LocationID = "& Application("LocationID") &")"
If NOT (dbExec(dbMain,strSQL2)) Then
	blnUpdatePass = 0
End If

Jout = "{""success"":"& blnUpdatePass &"}"
response.write(Jout)
Call CloseConnection(dbMain)
%>