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
Dim dbMain,strSQL,rsData,strSQL2,rsData2,Jout,blnUpdatePass,strColor,intColor
Set dbMain =  OpenConnection
blnUpdatePass = 0
strColor = trim(UCase(Request("Color")))


intColor = 0
  strSQL = "SELECT ListValue FROM LM_ListItem Where LM_ListItem.ListType = 5 and ListDesc ='"& strColor &"'"

if DBOpenRecordset(dbMain,rsData,strSQL) Then
	if not rsData.EOF Then
        intColor = rsData("ListValue")
    end if
end if

blnUpdatePass = 1
strSQL2= " Update ScanIn set Color='"& intColor &"' WHERE (LocationID = "& Application("LocationID") &")"
If NOT (dbExec(dbMain,strSQL2)) Then
	blnUpdatePass = 0
End If

Jout = "{""success"":"& blnUpdatePass &"}"
response.write(Jout)
Call CloseConnection(dbMain)
%>