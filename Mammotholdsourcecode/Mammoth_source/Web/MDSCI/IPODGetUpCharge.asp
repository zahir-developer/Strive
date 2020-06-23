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
Dim dbMain,strSQL,rsData,Jout, strUpCharge

Set dbMain =  OpenConnection
strUpCharge = 0 
strSQL = "SELECT  UpCharge FROM ScanIn WHERE (LocationID = "& Application("LocationID") &")"
if DBOpenRecordset(dbMain,rsData,strSQL) Then
	if not rsData.EOF Then
        strUpCharge = rsData("UpCharge")
    end if
end if

Jout = "{""success"":"& strUpCharge &"}"
response.write(Jout)
Call CloseConnection(dbMain)
%>