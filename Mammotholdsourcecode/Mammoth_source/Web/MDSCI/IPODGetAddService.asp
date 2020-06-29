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
Dim dbMain,strSQL,rsData,Jout, strAddService

Set dbMain =  OpenConnection
strAddService = ""
strSQL = "SELECT  isnull(AddService,'') as AddService FROM ScanIn WHERE (LocationID = "& Application("LocationID") &")"
if DBOpenRecordset(dbMain,rsData,strSQL) Then
	if not rsData.EOF Then
        strAddService = ltrim(rtrim(rsData("AddService")))
    end if
end if

if len(trim(strAddService))=0 then
    strAddService = "0000000000000000000000000000000000000000"
end if


Jout = "{""AddService"" : """& strAddService &"""}"
response.write(Jout)
Call CloseConnection(dbMain)
%>