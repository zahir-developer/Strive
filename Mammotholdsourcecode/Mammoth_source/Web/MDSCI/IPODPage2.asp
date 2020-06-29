<%@  language="VBSCRIPT" %>
<%
'********************************************************************
Option Explicit
Response.Expires = 0
Response.Buffer = True
response.ContentType = "application/json"
'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incDatabase.asp"-->
<%
Dim dbMain,strSQL,rsData,strSQL2,rsData2,Jout,blnUpdatePass,strWash,strNoEng
Set dbMain =  OpenConnection
blnUpdatePass = 0
strWash = trim(UCase(Request("Wash")))
strNoEng = trim(UCase(Request("NoEng")))

 
blnUpdatePass = 1

strSQL2= " Update ScanIn set Wash = '" & strWash &"',NoEng = '" & strNoEng &"' WHERE (LocationID = "& Application("LocationID") &")"
If NOT (dbExec(dbMain,strSQL2)) Then
		blnUpdatePass = 0
End If

Jout = "{""success"":"& blnUpdatePass &"}"

response.write(Jout)

Call CloseConnection(dbMain)
%>