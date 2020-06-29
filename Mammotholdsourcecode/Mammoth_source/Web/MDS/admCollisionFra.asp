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
<link rel="stylesheet" href="main.css" type="text/css" />
<title></title>
</head>
<body class=pgframe>
<table style="width: 752px; border-collapse: collapse;" class="Data">
	<tr>
    <td  class="Header" style="text-align: center; width: 100px">Collision #</td>
    <td  class="Header" style="text-align: center; width: 100px">Date</td>
    <td  class="Header" style="text-align: center; width: 300px">Description</td>
    <td  class="Header" style="text-align: center; width: 100px">Amount</td>
    <td  class="Header" style="text-align: center; width: 100px" >Balance</td>
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

Sub CollEdit(strRowID)
	window.event.cancelBubble = false
	Dim arrData,intUserID,intCollID,retCollision,LocationID,LoginID

	arrData = Split(strRowID,"|")	
	intUserID = arrdata(0)
	intCollID = arrdata(1)
    LocationID = arrdata(2)
    LoginID = arrdata(3)

	retCollision= ShowModalDialog("admEditColl.asp?intUserID=" & intUserID & "&intCollID=" & intCollID & "&LocationID=" & LocationID & "&LoginID=" & LoginID ,"","dialogwidth:460px;dialogheight:460px;")

	parent.fraMain2.location.href = "admCollisionFra.asp?intUserID=" & intUserID &"&LocationID=" & LocationID &"&LoginID=" & LoginID
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

   intUserID=Request("intUserID")
        	LocationID = Request("LocationID")
    LoginID = Request("LoginID")
	strSQL =" SELECT * From UserCol (Nolock) where UserID=" & intUserID &" and LocationID=" & LocationID &" and ActID=1 Order by CollID"
    IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF

				strRowID = intUserID & "|" & rs("CollID") & "|" & LocationID & "|" & LoginID

				htmlDataRow = htmlDataRow & "<tr><td class=data width=100  align=right ><a href=!# class=data onclick=""CollEdit('" & strRowID &"')"">"&  NullTest(rs("CollID")) & "</a></td>" 
				htmlDataRow = htmlDataRow & "<td class=data width=100 align=right><a href=!# class=data onclick=""CollEdit('" & strRowID &"')"">" & NullTest(rs("ActDate")) & "</a></td>" 
				htmlDataRow = htmlDataRow & "<td class=data width=200 align=left><a href=!# class=data onclick=""CollEdit('" & strRowID &"')"">&nbsp;" & NullTest(rs("ActDesc")) & "</a></td>" 
				htmlDataRow = htmlDataRow & "<td class=data align=right width=100>" & formatcurrency(NullTest(rs("ActAmt"))) & "</td>" 
				
				strSQL2 =" SELECT sum(actamt) as Bal From UserCol (Nolock) where UserID=" & intUserID &" and LocationID=" & LocationID &" and CollID="& rs("CollID") &" group by LocationID,UserID,CollID"

				IF dbOpenStaticRecordset(db, rs2, strSQL2) then   
					IF NOT 	rs2.EOF then
					strBal = rs2("Bal")
					END IF
				END IF
				htmlDataRow = htmlDataRow & "<td align=right align=right class=data width=100>"& formatcurrency(strBal) &"</td></tr>"
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
 

