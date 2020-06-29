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
'********************************************************************
' Global Variables
'********************************************************************
'********************************************************************
' Main
'********************************************************************
Call Main

'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
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
<table style="width: 752px; border-collapse: collapse;" class="Data">
	<tr>
    <td class="Header" align="center" width="80">Item #</td>
    <td class="Header" align="center" width="80">Date</td>
    <td class="Header" align="center" width="300">Description</td>
    <td class="Header" align="center" width="100">Cost</td>
    <td class="Header" align="center" width="50">Qty</td>
    <td class="Header" align="center" width="100">Amount</td>
    <td class="Header" align="center" width="100" >Balance</td>
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

Sub UnifEdit(strRowID)
	Dim arrData,intUserID,intUnifID,retUniforms,LocationID,LoginID
	window.event.cancelBubble = false
	arrData = Split(strRowID,"|")	
	intUserID = arrdata(0)
	intUnifID = arrdata(1)
    LocationID = arrdata(2)
    LoginID = arrdata(3)
	retUniforms= ShowModalDialog("admEditUnif.asp?intUserID=" & intUserID & "&intUnifID=" & intUnifID &"&LocationID="& LocationID &"&LoginID="& LoginID ,"","dialogwidth:460px;dialogheight:460px;")

	parent.fraMain2.location.href = "admUniformsFra.asp?intUserID=" & intUserID &"&LocationID="& LocationID &"&LoginID="& LoginID
	window.event.returnValue = False 
End Sub 

</script>

<%
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
	Dim htmlDataRow,strDisplay
	Dim db,strSQL,rs, intUserID,strSQL2,rs2,strBal,strRowID,LocationID,LoginID
	Set db = OpenConnection

   LocationID = request("LocationID")
    LoginID = request("LoginID")

   intUserID=Request("intUserID")
	strSQL =" SELECT UserUnif.UserID, UserUnif.UnifID, UserUnif.ActID,"&_
	" UserUnif.ActDate, UserUnif.ActType, UserUnif.ActAmt, "&_
	" Product.Descript, UserUnif.ActCost, UserUnif.ActQty "&_
	" FROM UserUnif(Nolock)"&_
	" INNER JOIN Product(Nolock) ON UserUnif.ProdID = Product.ProdID"&_
	" where UserID=" & intUserID &" AND LocationID="& LocationID &"and UserUnif.ActID=1 Order by UnifID"
    IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF

				strRowID = intUserID & "|" & rs("UnifID")  & "|" & LocationID & "|" & LoginID

				htmlDataRow = htmlDataRow & "<tr><td class=data width=50  align=right ><a href=!# class=data onclick=""UnifEdit('" & strRowID &"')"">"&  NullTest(rs("UnifID")) & "&nbsp;</a></td>" 
				htmlDataRow = htmlDataRow & "<td class=data width=100 align=right><a href=!# class=data onclick=""UnifEdit('" & strRowID &"')"">" & NullTest(rs("ActDate")) & "&nbsp;</a></td>" 
				htmlDataRow = htmlDataRow & "<td class=data width=200 align=left><a href=!# class=data onclick=""UnifEdit('" & strRowID &"')"">&nbsp;" & NullTest(rs("Descript")) & "</a></td>" 
				htmlDataRow = htmlDataRow & "<td class=data align=right width=100>" & formatcurrency(NullTest(rs("ActCost"))) & "&nbsp;</td>" 
				htmlDataRow = htmlDataRow & "<td class=data align=right width=50>" & NullTest(rs("ActQty")) & "&nbsp;</td>" 
				htmlDataRow = htmlDataRow & "<td class=data align=right width=100>" & formatcurrency(NullTest(rs("ActAmt"))) & "&nbsp;</td>" 
				
				strSQL2 =" SELECT sum(actamt) as Bal From UserUnif (Nolock) where UserID=" & intUserID &" and UnifID="& rs("UnifID") &" and LocationID="& LocationID &" group by UserID,UnifID"

				IF dbOpenStaticRecordset(db, rs2, strSQL2) then   
					IF NOT 	rs2.EOF then
					strBal = rs2("Bal")
					END IF
				END IF
				htmlDataRow = htmlDataRow & "<td align=right align=right class=data width=100>"& formatcurrency(strBal) &"&nbsp;</td></tr>"
				rs.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=8 align=""center"" Class=""data"">No records were found.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=8 align=""center"" Class=""data"">No records were found.</td></tr>"
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
%>
 

