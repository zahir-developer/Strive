<%@ LANGUAGE="VBSCRIPT" %>
<%
'********************************************************************
Option Explicit
Response.Expires = 0
Response.Buffer = True
'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incDatabase.asp"-->
<%
Call Main
Sub Main
	Dim LocationID,LoginID

    LocationID = request("LocationID")
    LoginID = request("LoginID")

'********************************************************************
'HTML
'********************************************************************
%>
<html>
<head>
<link rel="stylesheet" href="main.css" type="text/css" />
<title></title>
</head>
<body class=pgframe>
<input type="hidden" name="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" value="<%=LoginID%>" />
<table style="width: 350px; border-collapse: collapse;" class="Data">
	<tr>
     <td class="Header" style="width:40px; text-align: center; white-space:nowrap;">&nbsp;</td>
    <td class="Header"  style="width:160px; text-align: center; white-space:nowrap;">Employee</td>
 	<td class="Header"  style="width:40px; text-align: center; white-space:nowrap;">%</td>
    <td class="Header" style="width:60px; text-align: center; white-space:nowrap;">Comm</td>
    </tr>
	<%=DoDataRow()%>
</table>
</body>
</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">
Option Explicit

Sub Window_OnLoad()
End Sub

Sub DeleteUser(DetailCompID)
	Dim Answer
	window.event.cancelBubble = false
	Answer = MsgBox("Are you sure you want to remove this Employee ?",276,"Confirm Cancel")
	If Answer = 6 then
		parent.document.ALL("DetailCompID").value = DetailCompID
		parent.frmMain.FormAction.value="DeleteUser"
		parent.frmMain.submit()

	Else
		Exit Sub
	End if
	window.event.returnValue = False 
End Sub 

</script>

<%
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
	Dim htmlDataRow,strDesc,intRecID,intPercent,cnt,strComAmt,LocationID,LoginID
	Dim db,strSQL,rs
	Set db = OpenConnection

	intRecID = Request("intRecID")
	LocationID = request("LocationID")
	LoginID = request("LoginID")

               
	strSQL =" SELECT DetailComp.DetailCompID,DetailComp.Comm, "&_
	" DetailComp.ComPer,DetailComp.ComAmt,LM_Users.LastName + ' ' + LM_Users.FirstName AS Name"&_
	" FROM DetailComp (nolock)"&_
	" INNER JOIN LM_Users(nolock) ON DetailComp.UserID = LM_Users.UserID "&_
	" Where (DetailComp.RecID=" & intRecID &") AND (DetailComp.LocationID="& LocationID &") AND (LM_Users.LocationID="& LocationID &")"
    IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF
				IF isnull(rs("ComAmt")) then
					strComAmt = 0
				ELSE
					strComAmt = rs("ComAmt")
				END IF
				htmlDataRow = htmlDataRow & "<tr><td style=""width:40px;text-align:center;"" class=""header"" onclick=""DeleteUser('"& rs("DetailCompID") &"')"" style=""cursor:hand"" ><u>Remove</u></td>" 
				htmlDataRow = htmlDataRow & "<td style=""width:160px;text-align:left;"" class=""Data"">&nbsp;" &  rs("Name") & "</td>" 
				htmlDataRow = htmlDataRow & "<td style=""width:40px;text-align:center;"" class=""Data"">" & rs("ComPer") & "</td>"
				htmlDataRow = htmlDataRow & "<td style=""width:60px;text-align:right;""  class=""Data"">" & FormatCurrency(strComAmt,2) & "&nbsp;</td></tr>"
				rs.MoveNext
			Loop	
		ELSE
			htmlDataRow = "<tr><td colspan=""4"" style=""text-align:center;"" Class=""data"">No Employees were found.</td></tr>"
		END IF
	ELSE
		htmlDataRow = "<tr><td colspan=""4"" style=""text-align:center;"" Class=""data"">No Employees were found.</td></tr>"
	END IF
	DoDataRow = htmlDataRow
	Set RS = Nothing
	Call CloseConnection(db)
End Function
%>
 

