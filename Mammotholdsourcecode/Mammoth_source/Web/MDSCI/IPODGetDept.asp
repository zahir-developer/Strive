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
Dim dbMain,strSQL,rsData,Jout, strDept

Set dbMain =  OpenConnection
strDept = 0 
strSQL = "SELECT  Dept FROM ScanIn WHERE (LocationID = "& Application("LocationID") &")"
if DBOpenRecordset(dbMain,rsData,strSQL) Then
	if not rsData.EOF Then
        strDept = rsData("Dept")
    end if
end if

Jout = "{""success"":"& strDept &"}"
response.write(Jout)
Call CloseConnection(dbMain)
%>