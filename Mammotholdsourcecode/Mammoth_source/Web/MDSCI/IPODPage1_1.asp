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
Dim dbMain,strSQL,rsData,strSQL2,rsData2,Jout,blnUpdatePass,strTag,strVModel
Set dbMain =  OpenConnection
blnUpdatePass = 0
strTag = trim(UCase(Request("Tag")))
strVModel = trim(UCase(Request("VModel")))


blnUpdatePass = 1
strSQL2= " Update ScanIn set tag='"& strTag &"', VModel='"& strVModel &"' WHERE (LocationID = "& Application("LocationID") &")"
If NOT (dbExec(dbMain,strSQL2)) Then
	blnUpdatePass = 0
End If

Jout = "{""success"":"& blnUpdatePass &"}"
response.write(Jout)
Call CloseConnection(dbMain)
%>