<%@  language="VBSCRIPT" %>
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
Dim hdnClientID,LocationID,LoginID
	hdnClientID = Request("hdnClientID")
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
<body class="pgframe">
    <form method="POST" name="ClientVehListFra" action="ClientVehListFra.asp?hdnClientID=<%=Request("hdnClientID")%>">
        <input type="hidden" name="hdnClientID" value="<%=hdnClientID%>">
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" value="<%=LoginID%>" />
        <table cellspacing="0" width="752" class="Data">
            <tr>
                <td class="Header" align="center" width="80" colspan="2">Vehical</td>
                <td class="Header" width="40" align="center">Acc</td>
                <td class="Header" width="20" align="center">#</td>
                <td class="Header" width="100" align="center">UPC</td>
                <td class="Header" width="100" align="center">Tag</td>
                <td class="Header" width="150" align="center">Color</td>
                <td class="Header" width="150" align="center">Make</td>
                <td class="Header" width="100" align="center">Model</td>
                <td class="Header" width="80" align="center">Upcharge</td>
            </tr>
            <%=DoDataRow()%>
        </table>
    </form>
</body>

</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/vbscript">
Option Explicit

Sub Window_OnLoad()
End Sub

Sub DeleteVeh(hdnvehid)
	Dim Answer
	window.event.cancelBubble = false
	Answer = MsgBox("Are you sure you want to Delete this Vehical " &"?"& chr(13) & chr(13) & " WARNING: A Vehical cannot be restored!",276,"Confirm Cancel")
	If Answer = 6 then
		parent.document.location.href = "ClientEdit.asp?hdnClientID=" & ClientVehListFra.hdnClientID.Value & "&hdnvehid=" & hdnvehid &"&FormAction=DeleteVeh" &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	Else
		Exit Sub
	End if
	window.event.returnValue = False 
End Sub 

Sub EditVeh(hdnvehid)
	Dim strVehArr
	window.event.cancelBubble = false
	strVehArr = ShowModalDialog("ClientVehEditDlg.asp?hdnClientID=" & ClientVehListFra.hdnClientID.Value & "&hdnvehid=" & hdnvehid  &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,,"center:1;dialogleft:210px;dialogtop:150px; dialogwidth:450px;dialogheight:350px;")
	If Len(strVehArr) = 0 Then 
		window.event.ReturnValue = False
	Else
		parent.document.location.href = "ClientEdit.asp?hdnClientID=" & ClientVehListFra.hdnClientID.Value & "&hdnvehid=" & hdnvehid &"&FormAction=SaveVeh&hdnVehArr="&strVehArr &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	End If
	window.event.returnValue = False 
End Sub 

Sub EditCustAcc(hdnCustAccID)
	Dim strVehCArr
	window.event.cancelBubble = false
	strVehCArr = ShowModalDialog("ClientVehAccEditDlg.asp?hdnCustAccID=" & hdnCustAccID &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,,"center:1;dialogleft:210px;dialogtop:150px; dialogwidth:670px;dialogheight:450px;")
	'If Len(strVehArr) = 0 Then 
	'	window.event.ReturnValue = False
	'Else
		parent.document.location.href = "ClientEdit.asp?hdnClientID=" & ClientVehListFra.hdnClientID.Value  &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	'End If
	window.event.returnValue = False 
End Sub 

</script>

<%
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
	Dim htmlDataRow,strDesc,hdnClientID,intCustAccID,strSQL2,rs2
	Dim db,strSQL,rs,strLoc,intListCode,rowColor,strFilter,strqaDate,strTitle
	Set db = OpenConnection

	hdnClientID = Request("hdnClientID")

               
	strSQL =" SELECT vehical.vehid,vehical.UPC, vehical.tag, vehical.vehnum, vehical.vyear, "&_
			" LM_ListItem.ListDesc AS make, LM_ListItem1.ListDesc AS model, vehical.vmodel,  "&_
			" LM_ListItem2.ListDesc AS color, client.account,client.Ctype "&_
			" FROM vehical(Nolock) "&_
			" INNER JOIN LM_ListItem (Nolock) ON vehical.make = LM_ListItem.ListValue "&_
			" INNER JOIN LM_ListItem LM_ListItem1(Nolock) ON vehical.model = LM_ListItem1.ListValue "&_
			" INNER JOIN LM_ListItem LM_ListItem2(Nolock) ON vehical.Color = LM_ListItem2.ListValue "&_
			" INNER JOIN client (Nolock) ON vehical.ClientID = client.ClientID "&_
			" WHERE (vehical.ClientID = "& hdnClientID &") "&_
			" AND (LM_ListItem.ListType = 3) "&_
			" AND (LM_ListItem1.ListType = 4) "&_
			" AND (LM_ListItem2.ListType = 5) "&_
			" ORDER BY vehical.vehnum "
    
       IF DBOpenRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF


				rowColor = "data"
	
				htmlDataRow = htmlDataRow & "<tr><td width=45 class=header align=center onclick=""DeleteVeh('"& rs("vehid") &"')"" style=""cursor:hand"" ><u>Delete</u></td>" 
				htmlDataRow = htmlDataRow & "<td width=45 class=header align=center onclick=""EditVeh('"& rs("vehid") &"')"" style=""cursor:hand"" ><u>Edit</u></td>" 
				IF rs("account") and rs("Ctype")=2 then

					strSQL2 =" SELECT CustAccID"&_
							" FROM CustAcc(Nolock) "&_
							" WHERE (CustAcc.vehid = "& rs("vehid") &")"
					IF dbOpenStaticRecordset(db, rs2, strSQL2) then   
						IF NOT 	rs2.EOF then
							intCustAccID = rs2("CustAccID") &"|"& rs("vehid") &"|"& hdnClientID
							htmlDataRow = htmlDataRow & "<td width=45 class=header align=center onclick=""EditCustAcc('"& intCustAccID &"')"" style=""cursor:hand"" ><u>Edit</u></td>" 
						ELSE
							intCustAccID = "0|"& rs("vehid") &"|"& hdnClientID
							htmlDataRow = htmlDataRow & "<td width=45 class=header align=center onclick=""EditCustAcc('"& intCustAccID &"')"" style=""cursor:hand"" ><u>Add</u></td>" 
						END IF
					END IF
				ELSE
				htmlDataRow = htmlDataRow & "<td width=45 class=header align=center >&nbsp;</td>" 
				END IF
				htmlDataRow = htmlDataRow & "<td class="& rowColor &">&nbsp;" &  NullTest(rs("VehNum")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td class="& rowColor &">&nbsp;" & NullTest( rs("UPC")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td class="& rowColor &">&nbsp;" & NullTest( Ucase(rs("tag"))) & "</td>" 
				htmlDataRow = htmlDataRow & "<td class="& rowColor &">&nbsp;" &  NullTest(rs("Color")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td class="& rowColor &">&nbsp;" &  NullTest(rs("Make")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=left class="& rowColor &">&nbsp;" & NullTest(rs("vModel")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=left class="& rowColor &">&nbsp;" & NullTest(rs("Model")) & "</td></tr>"

				rs.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=10 align=""center"" Class=""data"">No records were found.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=10 align=""center"" Class=""data"">No records were found.</td></tr>"
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
 

