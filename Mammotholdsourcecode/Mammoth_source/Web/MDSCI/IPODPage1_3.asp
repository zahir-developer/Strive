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
Dim dbMain,strSQL,rsData,strSQL2,rsData2,Jout,blnUpdatePass,strModel,intMod,intUpCharge
Set dbMain =  OpenConnection
blnUpdatePass = 0
strModel = ltrim(rtrim(Request("Model")))
intUpCharge = 0
intMod = 0
 strSQL = "SELECT ListValue, "&_
             " CASE WHEN patindex('%(A)%', LM_ListItem.ListDesc) > 0 THEN '1' "&_
            " WHEN patindex('%(B)%',LM_ListItem.ListDesc) > 0 THEN '2' "&_
            " WHEN patindex('%(C)%',LM_ListItem.ListDesc) > 0 THEN '3' "&_
            " WHEN patindex('%(D)%',LM_ListItem.ListDesc) > 0 THEN '4' "&_
            " WHEN patindex('%(E)%',LM_ListItem.ListDesc) > 0 THEN '5' ELSE '0' END AS UpCharge "&_
            " FROM LM_ListItem Where LM_ListItem.ListType = 4 and replace(replace(replace(ltrim(rtrim(LM_ListItem.ListDesc)),' ',''),' ',''),' ','') ='"& strModel &"'"
if DBOpenRecordset(dbMain,rsData,strSQL) Then
	if not rsData.EOF Then
        intMod = rsData("ListValue")
        intUpCharge = rsData("UpCharge")
    end if
end if

blnUpdatePass = 1
strSQL2= " Update ScanIn set Model='"& intMod &"',UpCharge='"& intUpCharge &"' WHERE (LocationID = "& Application("LocationID") &")"
If NOT (dbExec(dbMain,strSQL2)) Then
	blnUpdatePass = 0
End If

Jout = "{""success"":"& blnUpdatePass &"}"
response.write(Jout)
Call CloseConnection(dbMain)
%>