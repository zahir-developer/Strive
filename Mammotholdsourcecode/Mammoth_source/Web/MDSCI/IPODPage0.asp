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
Dim dbMain,strSQL,rsData,strSQL2,rsData2,Jout,blnUpdatePass,strDept,strUPC,strType
Set dbMain =  OpenConnection
blnUpdatePass = 0
strDept = trim(UCase(Request("Dept")))
strUPC = trim(UCase(Request("UPC")))
strType = trim(UCase(Request("Type")))
IF len(trim( strUPC )) > 0 then
    strSQL = "SELECT vehical.tag, vehical.make, vehical.model, vehical.Color, "&_
            " CASE WHEN patindex('%(A)%', LM_ListItem.ListDesc) > 0 THEN '1' "&_
            " WHEN patindex('%(B)%',LM_ListItem.ListDesc) > 0 THEN '2' "&_
            " WHEN patindex('%(C)%',LM_ListItem.ListDesc) > 0 THEN '3' "&_
            " WHEN patindex('%(D)%',LM_ListItem.ListDesc) > 0 THEN '4' "&_
            " WHEN patindex('%(E)%',LM_ListItem.ListDesc) > 0 THEN '5' ELSE '0' END AS UpCharge, "&_
            "   isnull((SELECT TOP (1) RECITEM.ProdID FROM REC INNER JOIN RECITEM ON REC.recid = RECITEM.recId  AND (RECITEM.LocationID = "& Application("LocationID") &")"&_
                " INNER JOIN Product ON RECITEM.ProdID = Product.ProdID WHERE (REC.vehID = vehical.vehid) "&_
                " AND (Product.cat = '"& strDept &"') AND (REC.LocationID = "& Application("LocationID") &") ORDER BY REC.recid DESC),'') AS LastWash "&_
            " FROM vehical INNER JOIN LM_ListItem ON vehical.model = LM_ListItem.ListValue AND LM_ListItem.ListType = 4 "&_
            " WHERE (vehical.upc = '" & strUPC &"')"
'response.write strsql&"<br />"

    if DBOpenRecordset(dbMain,rsData,strSQL) Then
	    if not rsData.EOF Then
'response.write "got here"&"<br />"
            blnUpdatePass = 2
            strSQL2= " Update ScanIn set Dept = '" & strDept &"', UPC='" & strUPC &"', Type='" & strType &"',Wash='0',Air='0', UpCharge='"& rsData("UpCharge") &_
                     "', NoEng='0', tag='"& rsData("tag") &"', make='"& rsData("make") &"', model='"& rsData("model") &"', Color='"& rsData("Color") &"', LastWash='"& rsData("LastWash") &"', AddService=''" &_
                     " WHERE (LocationID = "& Application("LocationID") &")"
            If NOT (dbExec(dbMain,strSQL2)) Then
		            blnUpdatePass = 0
            End If
        else
            blnUpdatePass = 1
            strSQL2= " Update ScanIn set Dept = '" & strDept &"', UPC='" & strUPC &"', Type='" & strType &"',Wash='0',Air='0', UpCharge='0', NoEng='0', LastWash='0', AddService=''  WHERE (LocationID = "& Application("LocationID") &")"
 
            If NOT (dbExec(dbMain,strSQL2)) Then
		            blnUpdatePass = 0
            End If
        end if
    end if
'response.write blnUpdatePass&"<br />"
'response.write strsql2&"<br />"


else
'response.write "got here"&"<br />"
    blnUpdatePass = 1
    strSQL2= " Update ScanIn set Dept = '" & strDept &"', UPC='', Type='" & strType &"',Wash='0',Air='0', UpCharge='0', tag='', make='', model='', Color='', NoEng='0', LastWash='0', AddService='' WHERE (LocationID = "& Application("LocationID") &")"
'response.write strsql2&"<br />"
 
    If NOT (dbExec(dbMain,strSQL2)) Then
		    blnUpdatePass = 0
    End If
end if
'response.write "<br />" & "end of line"

Jout = "{""success"":"& blnUpdatePass &"}"
response.write(Jout)
Call CloseConnection(dbMain)
%>