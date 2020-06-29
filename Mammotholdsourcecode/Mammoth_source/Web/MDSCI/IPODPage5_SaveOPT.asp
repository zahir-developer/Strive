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
Dim dbMain,strSQL,rsData,strSQL2,rsData2,Jout,blnUpdatePass,strValue,strOPT,strAddService
Set dbMain =  OpenConnection

strValue = trim(Request("Value"))
strOPT = trim(Request("OPT"))

'response.Write strValue
'response.Write strOPT 

strAddService = ""
strSQL = "SELECT  isnull(AddService,'') as AddService FROM ScanIn WHERE (LocationID = "& Application("LocationID") &")"
if DBOpenRecordset(dbMain,rsData,strSQL) Then
	if not rsData.EOF Then
        strAddService = rsData("AddService")
    end if
end if

if len(trim(strAddService))=0 then
    strAddService = "0000000000000000000000000000000000000000"
end if

Select Case strOPT
    Case "2"
        strAddService = trim(Mid(strAddService,1,1)) + trim(strValue) + trim(Mid(strAddService,3,36))
    Case "3"
        strAddService = trim(Mid(strAddService,1,2)) + trim(strValue) + trim(Mid(strAddService,4,36))
    Case "4"
        strAddService = trim(Mid(strAddService,1,3)) + trim(strValue) + trim(Mid(strAddService,5,36))
    Case "6"
        strAddService = trim(Mid(strAddService,1,5)) + trim(strValue) + trim(Mid(strAddService,7,36))
    Case "7"
        strAddService = trim(Mid(strAddService,1,6)) + trim(strValue) + trim(Mid(strAddService,8,36))
    Case "8"
        strAddService = trim(Mid(strAddService,1,7)) + trim(strValue) + trim(Mid(strAddService,9,36))
    Case "9"
        strAddService = trim(Mid(strAddService,1,8)) + trim(strValue) + trim(Mid(strAddService,10,36))
    Case "11"
        strAddService = trim(Mid(strAddService,1,10)) + trim(strValue) + trim(Mid(strAddService,12,36))
    Case "12"
        strAddService = trim(Mid(strAddService,1,11)) + trim(strValue) + trim(Mid(strAddService,13,36))
    Case "13"
        strAddService = trim(Mid(strAddService,1,12)) + trim(strValue) + trim(Mid(strAddService,14,36))
    Case "14"
        strAddService = trim(Mid(strAddService,1,13)) + trim(strValue) + trim(Mid(strAddService,15,36))
    Case "19"
        strAddService = trim(Mid(strAddService,1,18)) + trim(strValue) + trim(Mid(strAddService,20,36))
    Case "34"
        strAddService = trim(Mid(strAddService,1,33)) + trim(strValue) + trim(Mid(strAddService,35,36))
    Case "35"
        strAddService = trim(Mid(strAddService,1,34)) + trim(strValue) + trim(Mid(strAddService,36,36))
    Case "36"
        strAddService = trim(Mid(strAddService,1,35)) + trim(strValue) 
End Select
  strAddService = strAddService 
blnUpdatePass = 1
strSQL2= " Update ScanIn set AddService='"& strAddService &"' WHERE (ScanIn.LocationID = "& Application("LocationID") &")"
'response.Write strSQL2 

If NOT (dbExec(dbMain,strSQL2)) Then
	blnUpdatePass = 0
End If
'response.Write blnUpdatePass 

Jout = "{""success"":"& blnUpdatePass &"}"
response.write(Jout)
Call CloseConnection(dbMain)
%>