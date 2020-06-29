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
	Dim dbMain,strSQL,rs
	Dim intRECID,intTotal,intTax,intGTotal,LocationID,LoginID
Set dbMain = OpenConnection

intRECID=Request("intRECID")
LocationID = request("LocationID")
LoginID = request("LoginID")

IF len(trim(intRECID))=0 then
	intRECID = 0
END IF
IF intRECID >0 then
	strSQL =" SELECT isnull(SUM(Price),0) as total FROM RECITEM (nolock) WHERE RecID = "& intRECID &" AND LocationID="& LocationID
    IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
		IF NOT 	rs.EOF then
			intTotal = rs("total")
		ELSE
			intTotal = 0.0
		END IF
	END IF
	Set RS = Nothing
	strSQL =" SELECT isnull(SUM(TaxAmt),0) as taxAmt FROM RECITEM (nolock) WHERE RecID = "& intRECID &" AND LocationID="& LocationID &" and Taxable=1"
    IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
		IF NOT 	rs.EOF then
			intTax = rs("taxAmt")
		ELSE
			intTax = 0.0
		END IF
	END IF
	Set RS = Nothing
ELSE
	intTotal =0.0
	intTax = 0.0
END IF

intGTotal = intTotal+intTax

'********************************************************************
'HTML
'********************************************************************
%>
<html>
<head>
<link rel="stylesheet" href="main.css" type="text/css"/>
<title></title>
</head>
<body class=pgframe>
<form method="POST" name="frmMain" action="CashOutFra.asp">
<input type="hidden" name="intRECID" tabindex="-2" value="<%=intRECID%>"/>
<input type="hidden" name="LocationID" tabindex="-2" value="<%=LocationID%>"/>
<input type="hidden" name="LoginID" tabindex="-2" value="<%=LoginID%>"/>
<input type="hidden" name="intTotal" tabindex="-2" value="<%=intTotal%>"/>
<input type="hidden" name="intGTotal" tabindex="-2" value="<%=intGTotal%>"/>
<input type="hidden" name="intTax" tabindex="-2" value="<%=intTax%>"/>
<table cellspacing="0" width="350" class="Data">
	<tr>
     <td class="Header" style="width:40px" >&nbsp;</td>
     <td class="Header"  style="width:40px" >&nbsp;</td>
     <td class="Header"  style="width:200px" >Item</td>
    <td class="Header"  style="text-align:center; width:40px">Qty</td>
    <td class="Header"  style="width:5px">T</td>
    <td class="Header" style="text-align:center; width:70px">Price</td>
     <td class="Header" style="width:20px" >&nbsp;</td>
    </tr>
	<%=DoDataRow(dbMain)%>

</table>
</form>
</body>
</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/VBSCRIPT">
Option Explicit

Sub Window_OnLoad()
	parent.frmMain.intTotal.value = document.all("intTotal").value
	parent.frmMain.intTax.value = document.all("intTax").value
	parent.frmMain.intGTotal.value = document.all("intGTotal").value
	'parent.frmMain.lblTotal.innerText = formatcurrency(document.all("intTotal").value,2)
	'parent.frmMain.lblTax.innerText = formatcurrency(document.all("intTax").value,2)
	'parent.frmMain.lblGTotal.innerText = formatcurrency(document.all("intGTotal").value,2)
End Sub



Sub DeleteInv(RecItemID)
	Dim Answer,retDel
	window.event.cancelBubble = false
	Answer = MsgBox("Are you sure you want to Delete this Item?",276,"Confirm Cancel")
	If Answer = 6 then
		retDel= ShowModalDialog("CashOutDelDlg.asp?intRecItemID="& RecItemID &"&LoginID="& document.all("LoginID").value &"&LocationID="& document.all("LocationID").value  ,"","dialogwidth:400px;dialogheight:300px;")
		parent.frmMain.submit()
	Else
		Exit Sub
	End if
	window.event.returnValue = False 
End Sub 
Sub EditInv(RecItemID)
	Dim Answer,retDel

	window.event.cancelBubble = false
	retDel= ShowModalDialog("CashOutEditDlg.asp?intRecItemID="& RecItemID &"&intRECID="& document.all("intRECID").value &"&LoginID="& document.all("LoginID").value &"&LocationID="& document.all("LocationID").value ,"","dialogwidth:400px;dialogheight:300px;")
	parent.frmMain.submit()
	window.event.returnValue = False 
End Sub 
</script>

<%

	Call CloseConnection(dbMain)

End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow(dbMain)
	Dim htmlDataRow,strDesc,strDescTitle,strStatus,strTax
	Dim strSQL,rs,strLoc,intCat,intCatold,rowColor,intRECID,strqaDate,strTitle,LocationID,LoginID

	intRECID = Request("intRECID")
	LocationID = Request("LocationID")
    LoginID = request("LoginID")

               
	strSQL =" SELECT RECITEM.RecItemID,Product.Descript, RECITEM.QTY,RECITEM.Taxable, RECITEM.Price, Product.cat,LM_ListItem.Listdesc"&_
			" FROM RECITEM (nolock)"&_
			" INNER JOIN Product(nolock) ON RECITEM.ProdID = Product.ProdID"&_
			" AND Product.Descript <> 'None' "&_
			" INNER JOIN LM_ListItem(nolock) ON Product.cat = LM_ListItem.ListValue "&_
			" AND  LM_ListItem.ListType = 1"&_
			" WHERE RECITEM.recId = "& intRECID &_
			" AND RECITEM.LocationID = "& LocationID &_
			" ORDER BY LM_ListItem.ItemOrder, RECITEM.RecItemID"
    IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF

				strTitle = rs("Descript")
				rowColor = "data"
				intCat = rs("Cat")
				IF intcat <> intCatold then
				htmlDataRow = htmlDataRow & "<tr><td colspan=6 class=header align=left><b>" &  NullTest(rs("Listdesc")) & "</b></td>" 
				END IF

				htmlDataRow = htmlDataRow & "<tr><td width=40 class=header align=center onclick=""DeleteInv('"& rs("RecItemID") &"')"" style=""cursor:hand"" ><u>Del</u></td>" 
				htmlDataRow = htmlDataRow & "<td width=40 class=header align=center onclick=""EditInv('"& rs("RecItemID") &"')"" style=""cursor:hand"" ><u>Edit</u></td>" 

				htmlDataRow = htmlDataRow & "<td align=left Title=""" & strTitle & """ class="& rowColor &">&nbsp;"  & NullTest(rs("Descript")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=right Title=""" & strTitle & """ class="& rowColor &">" &  NullTest(rs("QTY")) & "&nbsp;</td>" 
				IF rs("Taxable") then
				htmlDataRow = htmlDataRow & "<td align=center Title=""" & strTitle & """ class="& rowColor &">*</td>" 
				ELSE
				htmlDataRow = htmlDataRow & "<td align=center Title=""" & strTitle & """ class="& rowColor &"></td>" 
				END IF
				htmlDataRow = htmlDataRow & "<td align=right Title=""" & strTitle & """  class="& rowColor &">" & formatcurrency(NullTest(rs("Price")),2) & "&nbsp;</td></tr>"
				intCatold = intcat
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
 

