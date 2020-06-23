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
Dim dbMain,strSQL,rsData,strSQL2,rsData2,Jout,blnUpdatePass,strMake,intMake
Set dbMain =  OpenConnection
blnUpdatePass = 0
strMake = Replace(trim(Request("Make")),"%20"," ")
intMake = 0
  strSQL = "SELECT ListValue FROM LM_ListItem Where LM_ListItem.ListType = 3 and (LTRIM(RTRIM(LM_ListItem.ListDesc)) ='"& strMake &"')"

if dbOpenStaticRecordset(dbMain,rsData,strSQL) Then
	if not rsData.EOF Then
        intMake = rsData("ListValue")
    end if
end if

blnUpdatePass = 1
strSQL2= " Update ScanIn set Make='"& intMake &"' WHERE (ScanIn.LocationID = "& Application("LocationID") &")"
If NOT (dbExec(dbMain,strSQL2)) Then
	blnUpdatePass = 0
End If

Jout = "{""success"":"& blnUpdatePass &"}"
response.write(Jout)
Call CloseConnection(dbMain)
%>