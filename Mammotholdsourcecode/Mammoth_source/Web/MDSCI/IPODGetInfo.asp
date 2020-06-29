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
Dim dbMain,strSQL,rsData,Jout, strTag,strCustomerName,strMake,strModel,strColor,strVModel

Set dbMain =  OpenConnection
  strSQL = "SELECT client.fname + ' ' + client.lname AS CustomerName,vehical.tag, vehical.VModel,LM_ListItem_2.ListDesc AS Make, LM_ListItem.ListDesc AS Model, LM_ListItem_1.ListDesc AS Color "&_
    " FROM vehical INNER JOIN "&_
    " LM_ListItem ON vehical.model = LM_ListItem.ListValue AND LM_ListItem.ListType = 4 INNER JOIN "&_
    " LM_ListItem AS LM_ListItem_1 ON vehical.Color = LM_ListItem_1.ListValue AND LM_ListItem_1.ListType = 5 INNER JOIN "&_
    " LM_ListItem AS LM_ListItem_2 ON vehical.make = LM_ListItem_2.ListValue AND LM_ListItem_2.ListType = 3 INNER JOIN "&_
    " client ON vehical.clientid = client.clientid INNER JOIN "&_
    "  ScanIn ON vehical.upc = ScanIn.UPC WHERE (LEN(LTRIM(ScanIn.UPC)) > 0) AND (ScanIn.LocationID = "& Application("LocationID") &")"

if DBOpenRecordset(dbMain,rsData,strSQL) Then
	if not rsData.EOF Then
        strCustomerName = rsData("CustomerName")
        strTag = rsData("Tag")
        strMake = rsData("Make")
        strModel = rsData("Model")
        strColor = rsData("Color")
        strVModel = rsData("VModel")
     else
        strCustomerName = "Not Found!"
        strTag = ""
        strMake = ""
        strModel = ""
        strColor = ""
         strVModel = ""
     end if
end if

Jout = "{""CustomerName"" : """ & strCustomerName &""" , ""Tag"" : """& strTag &""" , ""Make"" : """ & strMake &""" , ""Model"" : """ & strModel &""" , ""Color"" : """ & strColor &""" , ""VModel"" : """ & strVModel &"""}"
response.write(Jout)
Call CloseConnection(dbMain)
%>