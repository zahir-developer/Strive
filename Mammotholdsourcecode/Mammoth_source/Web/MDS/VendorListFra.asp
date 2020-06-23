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
'********************************************************************
'HTML
'********************************************************************
%>
<html>
<head>
<link rel="stylesheet" href="main.css" type="text/css">
<title></title>
</head>
<body class=pgframe>
<table cellspacing="0" width="752" class="Data">
	<tr>
     <td class="Header" width="40" >&nbsp;</td>
     <td class="Header" width="40" >&nbsp;</td>
     <td class="Header" width="150" >Vendor</td>
    <td class="Header"  width="200">Contact</td>
    <td class="Header"  width="200">Address</td>
    <td class="Header"  width="100">Account</td>
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
Sub DeleteVen(intVenID)
	Dim Answer,retDel
	window.event.cancelBubble = false
	Answer = MsgBox("Are you sure you want to Delete this Vendor?",276,"Confirm Cancel")
	If Answer = 6 then
		retDel= ShowModalDialog("VendorDelDlg.asp?intVenID="& intVenID  ,"","dialogwidth:4px;dialogheight:4px;")
		parent.fraMain.location.href = "VendorListFra.asp"
	Else
		Exit Sub
	End if
	window.event.returnValue = False 
End Sub 

Sub EditVen(intVenID)
	Dim Answer,retDel
	window.event.cancelBubble = false
	retDel= ShowModalDialog("VendorEditDlg.asp?intVenID="& intVenID  ,"","dialogwidth:600px;dialogheight:400px;")
		parent.fraMain.location.href = "VendorListFra.asp"
	window.event.returnValue = False 
End Sub 

</script>

<%
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
	Dim htmlDataRow,strDesc,strDescTitle,strStatus,strTax
	Dim db,strSQL,rs,strLoc,intListCode,rowColor,strFilter,strqaDate,strTitle
	Set db = OpenConnection

	strFilter = Request("hdnFilterBy")

               
	strSQL =" SELECT * FROM Vendor (nolock)"
		 

	If Len(strFilter) > 0 Then
		strSQL = strSQL & strFilter
	ELSE
		strSQL = strSQL & " order by Vendor.Vendor"
	End if

    IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF

				strTitle = rs("Vendor")
				rowColor = "data"
				htmlDataRow = htmlDataRow & "<tr><td width=40 class=header align=center onclick=""DeleteVen('"& rs("VenID") &"')"" style=""cursor:hand"" ><u>Del</u></td>" 
				htmlDataRow = htmlDataRow & "<td width=40 class=header align=center onclick=""EditVen('"& rs("VenID") &"')"" style=""cursor:hand"" ><u>Edit</u></td>" 
				htmlDataRow = htmlDataRow & "<td Title=""" & strTitle & """ class="& rowColor &">" &  NullTest(rs("Vendor")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td Title=""" & strTitle & """  class="& rowColor &">" &  NullTest(rs("venContact")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td Title=""" & strTitle & """  class="& rowColor &">" &  NullTest(rs("Venaddr1")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=left Title=""" & strTitle & """  class="& rowColor &">" & NullTest(rs("VenAcc")) & "</td></tr>"

				rs.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=6 align=""center"" Class=""data"">No records were found.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=6 align=""center"" Class=""data"">No records were found.</td></tr>"
	END IF
	DoDataRow = htmlDataRow
	Set RS = Nothing
	Call CloseConnection(db)
End Function


Function NullTest(var)
	If IsNull(var) then
		NullTest = "&nbsp;"
	Else
		If Trim(var) = "" Then
			NullTest ="&nbsp;"
		Else
			NullTest = var
		End If
	End If
End Function
Function NullZero(var)
	If IsNull(var) then
		NullZero = 0.0
	Else
		If Trim(var) = "" Then
			NullZero =0.0
		Else
			NullZero = var
		End If
	End If
End Function
%>
 

