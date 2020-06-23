<%@ LANGUAGE="VBSCRIPT" %>
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

Dim dbMain, intdata,arrData,intClientID, strfname, strlname, straddr1, straddr2, strcity, strst, strzip, intCtype,_
    strPhone, strPhone2, strPhoneType, strPhone2Type, strEmail, txtNotes,intStatus
dim intmake,intmodel,intColor,strVmodel
Set dbMain =  OpenConnection




strfname=Request("strfname")
strlname=Request("strlname")


%>
<html>

<head>
<link rel="stylesheet" href="main.css" type="text/css">
<title></title>
</head>
<input type="hidden" name="strfname" ID="strfname" value="<%=strfname%>">
<input type="hidden" name="strlname" ID="strlname" value="<%=strlname%>">
<body class="pgbody">
 <div style="text-align:center">
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
<script language="vbscript">
Option Explicit

Sub ShowDlgBox(intClientID)
	IF LEN(intClientID) > 0 then
		parent.frmMain.hdnstatus.value =  intClientID
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
    Dim strReceivedDate, strDisplayLab, strStatus,strOnClick
 	Set db = OpenConnection
   
IF len(trim(request("strfname")))>0 and len(trim(request("strLname")))>0 then
   
	strSQL = " SELECT top 50 ClientID, fname, lname, addr1, city, Phone"&_
			" FROM client(NOLOCK)"&_
			" WHERE (fname LIKE '" & request("strfname") & "%')"&_
			" AND (lname LIKE '" & request("strLname") & "%')"&_
			" Order By lname,fname"

ELSEIF len(trim(request("strfname")))>0 then

	strSQL = " SELECT top 50 ClientID, fname, lname, addr1, city, Phone"&_
			" FROM client(NOLOCK)"&_
			" WHERE (fname LIKE '" & request("strfname") & "%')"&_
			" Order By lname,fname"

ELSEIF len(trim(request("strLname")))>0 then

	strSQL = " SELECT top 50 ClientID, fname, lname, addr1, city, Phone"&_
			" FROM client(NOLOCK)"&_
			" WHERE (lname LIKE '" & request("strLname") & "%')"&_
			" Order By lname,fname"


END IF

'Response.Write strSQL
'Response.End
    if LEN(TRIM(strSQL))>0 then
	    If dbopenrecordset(db,rsData,strSQL)  Then
		    IF NOT 	rsData.EOF then
			    Do while Not rsData.EOF
				    strOnClick = "Call ShowDlgBox('" & rsData("ClientID") & "')"
				    htmlDataRow = htmlDataRow & "<tr><td class=data align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>&nbsp;" & trim(rsData("fname")) & "</Label></td>"
				    htmlDataRow = htmlDataRow & "<td class=data align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>&nbsp;"  &  trim(rsData("lname"))  & "</Label></td>"
				    htmlDataRow = htmlDataRow & "<td class=data align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>&nbsp;"  &  trim(rsData("addr1"))  & "</Label></td>"
				    'htmlDataRow = htmlDataRow & "<td class=data align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>&nbsp;"  &  trim(rsData("city"))  & "</Label></td>"
				    htmlDataRow = htmlDataRow & "<td class=data align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>"  &  trim(rsData("phone"))  & "</Label></td></tr>"
				    rsData.MoveNext
			    Loop	
		    ELSE
				    htmlDataRow = "<tr><td colspan=7 align=""center"" Class=""data"">No Clients.</td></tr>"
		    END IF
	    ELSE
			    htmlDataRow = "<tr><td colspan=7 align=""center"" Class=""data"">No Clients.</td></tr>"
	    END IF
	ELSE
			htmlDataRow = "<tr><td colspan=7 align=""center"" Class=""data"">No Clients.</td></tr>"
	END IF
	DoDataRow = htmlDataRow
	Set rsData = Nothing
	Call CloseConnection(db)
End Function


%>