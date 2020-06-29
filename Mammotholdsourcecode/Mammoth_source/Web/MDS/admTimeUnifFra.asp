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
Dim dbmain,hdnUserID,hdnSheetID,strSQL,rs,strcardname,strGtotal,strUnifbal,strUnifAmt,LocationID,LoginID
Dim strSalery
Set dbMain =  OpenConnection


hdnSheetID = Request("hdnSheetID")
hdnUserID = Request("hdnUserID")
LocationID = request("LocationID")
LoginID = request("LoginID")

strSQL =" SELECT FirstName + ' ' + LastName AS cardname,salery"&_
	" FROM LM_Users"&_
	" WHERE LM_Users.userid ="& hdnUserID &" and LocationID="& LocationID
IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
	IF NOT rs.EOF then
		strcardname = rs("cardname")
		IF isnull(rs("salery")) then
		strSalery = 0.00
		ELSE
		strSalery = rs("salery")
		END IF
	END IF
END IF
Set rs = Nothing
strSQL =" SELECT sum(actamt) as Bal From UserUnif (Nolock) where UserID=" & hdnUserID &" and LocationID="& LocationID &" group by UserID"
IF dbOpenStaticRecordset(dbmain, rs, strSQL) then   
	IF NOT 	rs.EOF then
	strUnifbal = rs("Bal")
	ELSE
	strUnifbal = 0.0
	END IF
END IF
Set rs = Nothing
strSQL =" SELECT sum(actamt) as Bal From UserUnif (Nolock) where UserID=" & hdnUserID & " and sheetID="& hdnSheetID &" and LocationID="& LocationID &" group by UserID"
IF dbOpenStaticRecordset(dbmain, rs, strSQL) then   
	IF NOT 	rs.EOF then
	strUnifAmt = rs("Bal")
	ELSE
	strUnifAmt = 0.0
	END IF
END IF
Set rs = Nothing


'********************************************************************
'HTML
'********************************************************************
%>
<html>
<head>
<link rel="stylesheet" href="main.css" type="text/css">
<title></title>
</head>
<body class=pgbody>
<form method="post" name="admTimeUnifFra" action="admTimeUnifFra.asp" >
<input type="hidden" name="hdnUserID" tabindex="-2" value="<%=hdnUserID%>" />
<input type="hidden" name="hdnSheetID" tabindex="-2" value="<%=hdnSheetID%>" />
<input type="hidden" name="strGtotal" tabindex="-2" value="<%=strGtotal%>" />
<input type="hidden" name="strSalery" tabindex="-2" value="<%=strSalery%>" />
<input type="hidden" name="strUnifbal" tabindex="-2" value="<%=strUnifbal%>" />
<input type="hidden" name="strUnifAmt" tabindex="-2" value="<%=strUnifAmt%>" />
<input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<% IF isnull(hdnUserID) or len(hdnUserID)=0 then %>
<table style="width: 98%; border-collapse: collapse;" class="Data">
	<tr>
     <td colspan=11 style="text-align: center;"  Class="data">No records Selected.</td>
    </tr>
</table>
<% Else %>
<table style="width: 98%; border-collapse: collapse;">
	<tr>
	<td style="text-align: center;" class="control"><b><%=strcardname%></b></td>
		<td align=right>			
			<button  name="btnSave" style="width:140px">Add Payment</button>
		</td>
	</tr>
</table>
<table style="width: 98%; border-collapse: collapse;" class="Data">
	<tr>
    <td class="Header" style="text-align: center; width: 80px">Item #</td>
    <td class="Header" style="text-align: center; width: 80px">Date</td>
    <td class="Header" style="text-align: center; width: 300px">Description</td>
    <td class="Header" style="text-align: center; width: 100px">Cost</td>
    <td class="Header" style="text-align: center; width: 50px">Qty</td>
    <td class="Header" style="text-align: center; width: 100px">Amount</td>
    <td class="Header" style="text-align: center; width: 100px">Balance</td>
    </tr>
	<%=DoDataRow()%>

</table>
<% END IF %>
</form>
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
	parent.document.all("lblUnifbal").innerText = formatcurrency(cdbl(document.all("strUnifbal").value),2)
	parent.document.all("lblUnifAmt").innerText = formatcurrency(cdbl(document.all("strUnifAmt").value),2)
	IF cdbl(document.all("strSalery").value) > 0.0 then
		parent.document.all("lblGTotal").innerText = formatCurrency(cdbl(parent.document.all("lblAdjAmt").innerText)+cdbl(parent.document.all("lblCollAmt").innerText)+cdbl(parent.document.all("lblUnifAmt").innerText) +cdbl(parent.document.all("lblTotal").innerText),2)
	ELSE
		parent.document.all("lblGTotal").innerText = formatCurrency(cdbl(parent.document.all("lblAdjAmt").innerText)+cdbl(parent.document.all("lblcollAmt").innerText)+cdbl(parent.document.all("lblUnifAmt").innerText) +cdbl(parent.document.all("lblTotal").innerText) + cdbl(parent.document.all("lblDTotal").innerText)+cdbl(parent.document.all("lblBonus").innerText),2)
	END IF
	document.all("strGTotal").value = cdbl(parent.document.all("lblGTotal").innerText)
End Sub

Sub btnSave_OnClick()
	Dim retUniforms
	window.event.cancelBubble = false

	retUniforms= ShowModalDialog("admTimeAddUnif.asp?hdnSheetID=<%=Request("hdnSheetID")%>&intUserID=<%=Request("hdnUserID")%>&LocationID=<%=Request("LocationID")%>&LoginID=<%=Request("LoginID")%>&strGTotal="&document.all("strGTotal").value ,"","dialogwidth:460px;dialogheight:260px;")
	parent.fraMain2.location.href = "admTimeUnifFra.asp?hdnSheetID=<%=Request("hdnSheetID")%>&hdnUserID=<%=Request("hdnUserID")%>&LocationID=<%=Request("LocationID")%>&LoginID=<%=Request("LoginID")%>"
	window.event.returnValue = False 
End Sub

</script>

<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
	Dim htmlDataRow,strDesc,hdnSheetID,LocationID
	Dim db,strSQL,rs,strLoc,intListCode,rowColor,hdnUserID,strRowID, rs2, strSQL2,strBal
	Set db = OpenConnection

	hdnUserID = Request("hdnUserID")
	hdnSheetID = Request("hdnSheetID")
    	LocationID = Request("LocationID")

	strSQL =" SELECT UserUnif.ActDate, Sum(UserUnif.ActAmt) as ActAmt "&_
	" FROM UserUnif(Nolock)"&_
	" where UserID=" & hdnUserID &" and sheetid=" & hdnSheetID &" and LocationID=" & LocationID & " Group by sheetid,ActDate"
     IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF
				rowColor = "data"
				'IF rs("paid") = 0 then
				'htmlDataRow = htmlDataRow & "<tr><td width=40 class=header align=center onclick=""DeleteDC('"& rs("DetailCompID") &"')"" style=""cursor:hand"" ><u>Del</u></td>" 
				'htmlDataRow = htmlDataRow & "<td width=40 class=header align=center onclick=""EditRec('"& rs("RECid") &"')"" style=""cursor:hand"" ><u>Edit</u></td>" 
				'ELSE
				'htmlDataRow = htmlDataRow & "<tr><td colspan=2 width=40 class=header align=center>Paid</td>" 
				'END IF
				htmlDataRow = htmlDataRow & "<tr><td class=data width=50  align=right>&nbsp;</a></td>" 
				htmlDataRow = htmlDataRow & "<td class=data width=100 align=right>" & NullTest(rs("ActDate")) & "&nbsp;</a></td>" 
				htmlDataRow = htmlDataRow & "<td class=data width=200 align=left>&nbsp;Payroll Payment</a></td>" 
				htmlDataRow = htmlDataRow & "<td class=data align=right width=100>&nbsp;</td>" 
				htmlDataRow = htmlDataRow & "<td class=data align=right width=50>&nbsp;</td>" 
				htmlDataRow = htmlDataRow & "<td class=data align=right width=100>" & formatcurrency(NullTest(rs("ActAmt"))) & "&nbsp;</td>" 
				htmlDataRow = htmlDataRow & "<td align=right align=right class=data width=100>&nbsp;</td></tr>"
				rs.MoveNext
			Loop	
		END IF
	END IF
	Set RS = Nothing

               
	strSQL =" SELECT UserUnif.UserID, UserUnif.UnifID, UserUnif.ActID,"&_
	" UserUnif.ActDate, UserUnif.ActType, UserUnif.ActAmt, "&_
	" Product.Descript, UserUnif.ActCost, UserUnif.ActQty "&_
	" FROM UserUnif(Nolock)"&_
	" INNER JOIN Product(Nolock) ON UserUnif.ProdID = Product.ProdID"&_
	" where UserID=" & hdnUserID &" and LocationID=" & LocationID  &" Order by UnifID DESC"
     IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF
				rowColor = "data"
			'IF rs("paid") = 0 then
				'htmlDataRow = htmlDataRow & "<tr><td width=40 class=header align=center onclick=""DeleteDC('"& rs("DetailCompID") &"')"" style=""cursor:hand"" ><u>Del</u></td>" 
				'htmlDataRow = htmlDataRow & "<td width=40 class=header align=center onclick=""EditRec('"& rs("RECid") &"')"" style=""cursor:hand"" ><u>Edit</u></td>" 
				'ELSE
				'htmlDataRow = htmlDataRow & "<tr><td colspan=2 width=40 class=header align=center>Paid</td>" 
				'END IF
				htmlDataRow = htmlDataRow & "<tr><td class=data width=50  align=right >"&  NullTest(rs("UnifID")) & "&nbsp;</a></td>" 
				htmlDataRow = htmlDataRow & "<td class=data width=100 align=right>" & NullTest(rs("ActDate")) & "&nbsp;</a></td>" 
				htmlDataRow = htmlDataRow & "<td class=data width=200 align=left>&nbsp;" & NullTest(rs("Descript")) & "</a></td>" 
				htmlDataRow = htmlDataRow & "<td class=data align=right width=100>" & formatcurrency(NullTest(rs("ActCost"))) & "&nbsp;</td>" 
				htmlDataRow = htmlDataRow & "<td class=data align=right width=50>" & NullTest(rs("ActQty")) & "&nbsp;</td>" 
				htmlDataRow = htmlDataRow & "<td class=data align=right width=100>" & formatcurrency(NullTest(rs("ActAmt"))) & "&nbsp;</td>" 
				
				strSQL2 =" SELECT sum(actamt) as Bal From UserUnif (Nolock) where UserID=" & hdnUserID &"and UnifID="& rs("UnifID") &" group by UserID,UnifID"

				IF dbOpenStaticRecordset(db, rs2, strSQL2) then   
					IF NOT 	rs2.EOF then
					strBal = rs2("Bal")
					END IF
				END IF
				htmlDataRow = htmlDataRow & "<td align=right align=right class=data width=100>"& formatcurrency(strBal) &"&nbsp;</td></tr>"
				rs.MoveNext
			Loop	
		END IF
	END IF
	Set RS = Nothing






	DoDataRow = htmlDataRow
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
 

