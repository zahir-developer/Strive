<%@  language="VBSCRIPT" %>
<%
'********************************************************************
' Name: 
'********************************************************************
Option Explicit
Response.Expires = 0
Response.Buffer = True

Dim Title
Dim gstrMessage

'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incDatabase.asp"-->
<!--#include file="inccommon.asp"-->
<%
'********************************************************************
' Global Variables
'********************************************************************
'********************************************************************
' Main
'********************************************************************
Main

'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
Sub Main

Dim dbMain, intdata,arrData,intClientID, strDescript, strlname, straddr1, straddr2, strcity, strst, strzip, intCtype,_
    strPhone, strPhone2, strPhoneType, strPhone2Type, strEmail, txtNotes,intStatus
dim intmake,intmodel,intColor,strVmodel,LocationID
Set dbMain =  OpenConnection




strDescript=Request("strDescript")
strlname=Request("strlname")
LocationID = request("LocationID")


%>
<html>

<head>
    <link rel="stylesheet" href="main.css" type="text/css">
    <title></title>
</head>
<body class="pgbody">
    <input type="hidden" name="strDescript" id="strDescript" value="<%=strDescript%>">
    <input type="hidden" name="strlname" id="strlname" value="<%=strlname%>">
    <input type="hidden" name="LocationID" value="<%=LocationID%>" />
    <div style="text-align: center">
        <table cellspacing="0" width="550" class="data">
            <%=DoDataRow()%>
        </table>
    </div>
</body>
</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/vbscript">
Option Explicit

Sub ShowDlgBox(ProdID)
	IF LEN(ProdID) > 0 then
		parent.frmMain.hdnstatus.value =  ProdID
		parent.window.close
		'window.close
	END IF
End Sub


</script>
<%
dbMain.close
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
    Dim  htmlDataRow, db,rsData,rsData2, rsData3, strSQL, strLineColor
    dim  intLine,strVehical,strService,strMOD,strUPCHARGE,strTitle
    Dim strStarttime,strEndtime,strHour,intOffset
    Dim strReceivedDate, strDisplayLab, strStatus,strOnClick,LocationID
 	Set db = OpenConnection

    LocationID = request("LocationID")
   
IF len(trim(request("strDescript")))>0 then
		if LocationID = 3 then
			strSQL = " SELECT Product.Descript,LM_ListItem.ListDesc AS cat, Product.Bcode, Product.Price,Product.ProdID"&_
			" FROM Product(NOLOCK)"&_
			" INNER JOIN LM_ListItem(NOLOCK) ON Product.cat = LM_ListItem.ListValue"&_
			" AND LM_ListItem.ListType = 1"&_
			" WHERE (Product.Descript LIKE '%" & request("strDescript") & "%') and Product.ProdID not in (293,1,2,3)"&_
			" Order By Product.Descript"
		else
			strSQL = " SELECT Product.Descript,LM_ListItem.ListDesc AS cat, Product.Bcode, Product.Price,Product.ProdID"&_
			" FROM Product(NOLOCK)"&_
			" INNER JOIN LM_ListItem(NOLOCK) ON Product.cat = LM_ListItem.ListValue"&_
			" AND LM_ListItem.ListType = 1"&_
			" WHERE (Product.Descript LIKE '%" & request("strDescript") & "%') and Product.ProdID not in (461,462,463,464)"&_
			" Order By Product.Descript"
		end if
ELSE
		if LocationID = 3 then
	strSQL = " SELECT Product.Descript,LM_ListItem.ListDesc AS cat, Product.Bcode, Product.Price,Product.ProdID"&_
	" FROM Product(NOLOCK)"&_
	" INNER JOIN LM_ListItem(NOLOCK) ON Product.cat = LM_ListItem.ListValue"&_
	" AND LM_ListItem.ListType = 1"&_
	" WHERE 1=2 and Product.ProdID not in (293,1,2,3)"&_
	" Order By Product.Descript"
		else
	strSQL = " SELECT Product.Descript,LM_ListItem.ListDesc AS cat, Product.Bcode, Product.Price,Product.ProdID"&_
	" FROM Product(NOLOCK)"&_
	" INNER JOIN LM_ListItem(NOLOCK) ON Product.cat = LM_ListItem.ListValue"&_
	" AND LM_ListItem.ListType = 1"&_
	" WHERE 1=2 and Product.ProdID not in (461,462,463,464)"&_
	" Order By Product.Descript"
		end if
END IF

'Response.Write strSQL
'Response.End
	If dbopenrecordset(db,rsData,strSQL)  Then
		IF NOT 	rsData.EOF then
			Do while Not rsData.EOF
				strOnClick = "Call ShowDlgBox('" & cstr(rsData("ProdID"))&"|"& trim(rsData("Descript"))& "')"
				htmlDataRow = htmlDataRow & "<tr><td class=data align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>&nbsp;" & trim(rsData("Descript")) & "</Label></td>"
				htmlDataRow = htmlDataRow & "<td class=data align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>&nbsp;"  &  trim(rsData("cat"))  & "</Label></td>"
				htmlDataRow = htmlDataRow & "<td class=data align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>&nbsp;"  &  trim(rsData("Bcode"))  & "</Label></td>"
				htmlDataRow = htmlDataRow & "<td class=data align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>"  &  trim(rsData("Price"))  & "</Label></td></tr>"
				rsData.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=4 align=""center"" Class=""data"">No Items Found.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=4 align=""center"" Class=""data"">No Items Found.</td></tr>"
	END IF
	DoDataRow = htmlDataRow
	Set rsData = Nothing
	Call CloseConnection(db)
End Function


%>