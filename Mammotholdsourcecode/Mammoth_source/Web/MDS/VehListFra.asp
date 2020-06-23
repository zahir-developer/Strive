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
<link rel="stylesheet" href="main.css" type="text/css">
<title></title>
</head>
<form method="POST" name="VehListFra" action="VehListFra.asp" >
<body class=pgframe>
<input type="hidden" name="hdnClientID" tabindex="-2" value="<%=hdnClientID%>">
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<table cellspacing="0" width="752" class="Data">
	<tr>
     <td class="Header" width="40" align=center>&nbsp;</td>
     <td class="Header" width="40" align=center>&nbsp;</td>
    <td class="Header"  width="150"align=center>Client Name</td>
     <td class="Header" width="20" align=center>#</td>
   <td class="Header"  width="80" align=center>Tag</td>
    <td class="Header"  width="80" align=center>Color</td>
    <td class="Header"  width="100" align=center>Make</td>
    <td class="Header"  width="100" align=center>Model</td>
    <td class="Header"  width="60" align=center>UpCharge</td>
    <td class="Header" width="60" align=center >Bar Code</td>
    </tr>
	<%=DoDataRow()%>

</table>

</body>
</form>
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
		parent.document.location.href = "VehList.asp?hdnvehid=" & hdnvehid &"&FormAction=DeleteVeh&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	Else
		Exit Sub
	End if
	window.event.returnValue = False 
End Sub 

Sub EditVeh(hdnvehid)
	Dim strVehArr
	window.event.cancelBubble = false
	strVehArr = ShowModalDialog("VehEditDlg.asp?hdnvehid=" & hdnvehid &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,,"center:1;dialogleft:210px;dialogtop:150px; dialogwidth:450px;dialogheight:350px;")
	If Len(strVehArr) = 0 Then 
		window.event.ReturnValue = False
	Else
		parent.document.location.href = "VehList.asp?hdnvehid=" & hdnvehid &"&FormAction=SaveVeh&hdnVehArr="&strVehArr &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	End If
	window.event.returnValue = False 
End Sub 


</script>

<%
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
	Dim htmlDataRow,strDesc,strDescTitle,strRequester,strRequesterTitle
	Dim db,strSQL,rs,strLoc,intListCode,rowColor,strFilter,strqaDate,strTitle
	Set db = OpenConnection

	strFilter = Request("hdnFilterBy")

               
	strSQL =" SELECT vehical.vehid,vehical.ClientID, vehical.tag, vehical.vehnum, vehical.UPC, "&_
			" LM_ListItem.ListDesc AS make, LM_ListItem1.ListDesc AS model, vehical.vmodel,  "&_
			" LM_ListItem2.ListDesc AS color,client.fname+' '+client.lname as ClientName "&_
			" FROM vehical(Nolock) "&_
			" INNER JOIN LM_ListItem (Nolock) ON vehical.make = LM_ListItem.ListValue "&_
			" INNER JOIN LM_ListItem LM_ListItem1(Nolock) ON vehical.model = LM_ListItem1.ListValue "&_
			" INNER JOIN LM_ListItem LM_ListItem2(Nolock) ON vehical.Color = LM_ListItem2.ListValue "&_
			" INNER JOIN client(Nolock) ON vehical.ClientID = client.ClientID"&_
			" WHERE  (LM_ListItem.ListType = 3) "&_
			" AND (LM_ListItem1.ListType = 4) "&_
			" AND (LM_ListItem2.ListType = 5) "

	If Len(strFilter) > 0 Then
		strSQL = strSQL & strFilter
	ELSE
		strSQL = strSQL & " order by Vehical.ClientID,Vehical.vehnum"
	End if
'Response.Write STRsql
'Response.End

    IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF
				rowColor = "data"
				htmlDataRow = htmlDataRow & "<tr><td width=40 class=header align=center onclick=""DeleteVeh('"& rs("vehid") &"')"" style=""cursor:hand"" ><u>Del</u></td>" 
				htmlDataRow = htmlDataRow & "<td width=40 class=header align=center onclick=""EditVeh('"& rs("vehid") &"')"" style=""cursor:hand"" ><u>Edit</u></td>" 
				htmlDataRow = htmlDataRow & "<td  class="& rowColor &">&nbsp;" &  NullTest(rs("ClientName")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td width=20 align=left class="& rowColor &">&nbsp;" & rs("Vehnum") & "</td>" 
				htmlDataRow = htmlDataRow & "<td  class="& rowColor &">&nbsp;" &  NullTest(ucase(rs("tag"))) & "</td>" 
				htmlDataRow = htmlDataRow & "<td  class="& rowColor &">&nbsp;" &  NullTest(rs("Color")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td  class="& rowColor &">&nbsp;" &  NullTest(rs("Make")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=left  class="& rowColor &">&nbsp;" & NullTest(rs("vModel")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=left  class="& rowColor &">&nbsp;" & NullTest(rs("Model")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=center class="& rowColor &">" & NullTest(rs("UPC")) & "</td></tr>"

				rs.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=9 align=""center"" Class=""data"">No records were found.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=9 align=""center"" Class=""data"">No records were found.</td></tr>"
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
 

