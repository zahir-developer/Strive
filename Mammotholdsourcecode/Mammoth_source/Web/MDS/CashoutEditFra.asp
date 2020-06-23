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
Dim intAssigned

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

	Dim dbMain,intRecItemID, strDesc,rsData,blSaved,intPrice,intQty,intTaxamt,intTaxable,intTaxRate
	Dim strSQL, RS ,intstatus , intCat, intGiftCardID,strCurrentAmt,intRecID,strLoadMsg,LocationID,LoginID

	Set dbMain =  OpenConnection

LocationID = request("LocationID")
LoginID = request("LoginID")
intRecID = request("intRecID")


		blSaved = false
	Select case Request("FormAction")
		Case "BtnSave"
			Call SaveInv(dbMain)
			blSaved = true
		Case "BtnGift"
			Call SaveInv(dbMain)
	End select
	intRecItemID = request("intRecItemID")
	strLoadMsg =""
	strSQL="SELECT Product.Descript,Product.Cat,RecItem.RecID, RECITEM.QTY,RECITEM.GiftCardID, RECITEM.Price,RECITEM.taxable, RECITEM.taxamt,LM_Locations.TaxRate AS TaxRate"&_
	" FROM RECITEM (nolock) INNER JOIN Product(nolock) ON RECITEM.ProdID = Product.ProdID INNER JOIN LM_Locations (nolock) ON RECITEM.LocationID = LM_Locations.LocationID "&_
	" WHERE RECITEM.RecItemID =" & intRecItemID &" and RECITEM.LocationID="&LocationID

	If DBOpenRecordset(dbMain,rs,strSQL) Then
		If Not RS.EOF Then
			intCat = RS("Cat")
			intRecID = RS("RecID")
			strDesc = RS("Descript")
			intPrice = RS("Price")
			intQty = RS("QTY")
			intTaxamt = RS("taxamt")
			intTaxable = RS("taxable")
			intTaxRate = RS("TaxRate")
			intGiftCardID = RS("GiftCardID")
		End If
	END IF
	IF LEN(TRIM(intGiftCardID))> 0 then
		strSQL="SELECT CurrentAmt"&_
		" FROM GiftCard (nolock) "&_
		" WHERE GiftCardID ='" & intGiftCardID &"'"
		If DBOpenRecordset(dbMain,rs,strSQL) Then
			If Not RS.EOF Then
				strCurrentAmt = formatCurrency(RS("CurrentAmt"),2)
				intPrice = RS("CurrentAmt")
			End If
		END IF
		' IF already Purchased
		strSQL="SELECT GiftCardID,recID"&_
		" FROM GiftCardHist (nolock) "&_
		" WHERE GiftCardID ='" & intGiftCardID &"' and TransType='Purchased'"
		If DBOpenRecordset(dbMain,rs,strSQL) Then
			If Not RS.EOF Then
				IF rs("recID") <> intRecID then
					intGiftCardID = ""
					strCurrentAmt = formatCurrency(0,2)
					intPrice = formatCurrency(0,2)
					strLoadMsg = "Already Purchased Ticket #:"+cstr(rs("recID"))
				END IF
			End If
		END IF
	ELSE
		intGiftCardID = ""
		strCurrentAmt = formatCurrency(0,2)
	END IF
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="CashOutEditFra.asp">
<input type="hidden" name="strLoadMsg" value="<%=strLoadMsg%>">
<input type="hidden" name="intRecItemID" value="<%=intRecItemID%>">
<input type="hidden" name="intRecID" value="<%=intRecID%>">
<input type="hidden" name="strDesc" value="<%=strDesc%>">
<input type="hidden" name="strCurrentAmt" value="<%=strCurrentAmt%>">
<input type="hidden" name="blSaved" value="<%=blSaved%>">
<input type="hidden" name="intTaxRate" value="<%=intTaxRate%>">
<input type="hidden" name="intTaxamt" value="<%=intTaxamt%>">
<input type="hidden" name="intTaxable" value="<%=intTaxable%>">
<input type="hidden" name="intCat" value="<%=intCat%>">
<input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<div style="text-align:center">
<table border="0" width="300" cellspacing="0" cellpadding="0">

<% IF intCat = 23 then %>
	<input type="hidden" name="intQty" value="1">
	<tr>
		<td align="right" class="control" nowrap>Item:&nbsp;</td>
		<td align="left" class="control" nowrap><label class="control">&nbsp;<%=strDesc%></label></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>Gift Card #:&nbsp;</td>
		<td align="left" class="control" nowrap><input maxlength="10" size="10" type=int tabindex=1 DirtyCheck=TRUE  onkeyPress="Check4Enter()" name="intGiftCardID" value="<%=intGiftCardID%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>Amount:&nbsp;</td>
		<td align="left" class="control" nowrap><label class="control">&nbsp;<%=strCurrentAmt%></label></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>Price:&nbsp;</td>
		<td align="left" class="control" nowrap><input maxlength="50" size="20" type=text tabindex=1 DirtyCheck=TRUE name="intPrice" value="<%=intPrice%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
<% Else %>
	<input type=hidden  name="intGiftCardID" value="<%=intGiftCardID%>">
	<tr>
		<td align="right" class="control" nowrap>Item:&nbsp;</td>
		<td align="left" class="control" nowrap><label class="control">&nbsp;<%=strDesc%></label></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>Qty:&nbsp;</td>
		<td align="left" class="control" nowrap><input maxlength="3" size="3" type=int tabindex=1 DirtyCheck=TRUE name="intQty" value="<%=intQty%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>Price:&nbsp;</td>
		<td align="left" class="control" nowrap><input maxlength="50" size="20" type=text tabindex=1 DirtyCheck=TRUE name="intPrice" value="<%=intPrice%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
<% END IF %>
</table>
<br>
<table align="center" border="0" width="300" cellspacing="1" cellpadding="1">
   <tr>
      <td align="center" colspan="3">
	  <button name="btnSave" class="button" style="width:75px" OnClick="SaveChg()">Save</button>&nbsp;&nbsp;&nbsp;&nbsp;
	  <button name="btnCancel" class="button" style="width:75px" OnClick="CancelChg()">Cancel</button>
		</td>
	</tr>		
</table>

</div>
<input type="hidden" name="FormAction" value="">
</form>
</body>
</html>
<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/VBSCRIPT">
Option Explicit


Sub Window_OnLoad()
	IF LEN(TRIM(frmMain.strLoadMsg.value))>0 then
		Msgbox  frmMain.strLoadMsg.value
	END IF
	IF frmMain.blSaved.value then
		parent.window.close
	END IF
End Sub

Sub SaveChg()
		frmMain.FormAction.value="BtnSave"
		frmMain.submit()
End Sub

Sub Check4Enter()
	If window.event.keycode = 13 Then
		window.event.cancelbubble = True
		window.event.returnvalue = FALSE
		frmMain.FormAction.value="BtnGift"
		frmMain.submit()
	End If
End Sub

Sub CancelChg()
	parent.window.close
End Sub

</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************

Sub SaveInv(dbMain)
	Dim intRecItemID,strSQL,rsData,intPrice,intQty,intTaxamt,intTaxable,intTaxrate,intGiftCardID
	Dim MaxSQL,intGiftCardTID,intRecID
	intRecID = request("intRecID")
	intRecItemID = request("intRecItemID")
	intPrice = request("intPrice")
	intQty = request("intQty")
	intTaxamt = request("intTaxamt")
	intTaxable = request("intTaxable")
	intTaxrate = request("intTaxrate")
	intGiftCardID = request("intGiftCardID")
		
	IF LEN(intGiftCardID) > 0 then
		strSQL="SELECT GiftCardID"&_
		" FROM GiftCard (nolock) "&_
		" WHERE GiftCardID ='" & intGiftCardID &"'"
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			If  rsData.EOF Then
				strSQL= "Insert into GiftCard (GiftCardID,LocationID,RecID,ActiveDte,CurrentAmt) Values ("&_
					"'" & intGiftCardID &"',"&_
					request("LocationID") &","&_
					intRecID &","&_
					"'" & Date() &"',"&_
					intPrice &")"
				IF NOT DBExec(dbMain, strSQL) then
					Response.Write gstrMsg
					Response.End
				END IF
				MaxSQL = " Select ISNULL(Max(GiftCardTID),0)+1 from GiftCardHist (NOLOCK)"
				If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
					intGiftCardTID=rsData(0)
				End if
				Set rsData = Nothing
				strSQL= "Insert into GiftCardHist (GiftCardTID,LocationID,GiftCardID,RecID,TransDte,TransType,TransAmt,TransUserID) Values ("&_
					intGiftCardTID &","&_
					request("LocationID") &","&_
					"'" & intGiftCardID &"',"&_
					intRecID &","&_
					"'" & Date() &"',"&_
					"'Activated',"&_
					intPrice &","& request("LoginID") &")"
				IF NOT DBExec(dbMain, strSQL) then
					Response.Write gstrMsg
					Response.End
				END IF
			ELSE
				strSQL= "Update GiftCard SET "&_
				" CurrentAmt=" & intPrice &""&_
				" Where GiftCardID ='"& intGiftCardID & "'"
				IF NOT DBExec(dbMain, strSQL) then
					Response.Write gstrMsg
					Response.End
				END IF

				MaxSQL = " Select ISNULL(Max(GiftCardTID),0)+1 from GiftCardHist (NOLOCK)"
				If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
					intGiftCardTID=rsData(0)
				End if
				Set rsData = Nothing
				strSQL= "Insert into GiftCardHist (GiftCardTID,LocationID,GiftCardID,RecID,TransDte,TransType,TransAmt,TransUserID) Values ("&_
					intGiftCardTID &","&_
					request("LocationID") &","&_
					"'" & intGiftCardID &"',"&_
					intRecID &","&_
					"'" & Date() &"',"&_
					"'Purchased',"&_
					intPrice &","& request("LoginID") &")"
				IF NOT DBExec(dbMain, strSQL) then
					Response.Write gstrMsg
					Response.End
				END IF
			End If
		END IF
	END IF



	IF LEN(intRecItemID) > 0 then
	    IF len(ltrim(intTaxable))=0 then
		    intTaxable = 0
		    intTaxamt = 0
	    END IF
	    IF intTaxable = 1 then
		    intTaxamt = (cdbl(intPrice)*cdbl(intQty))*cdbl(intTaxrate)
	    END IF
	
		strSQL= " Update RECITEM set "
		IF len(ltrim(intGiftCardID))>0 then
				strSQL= strSQL & " GiftCardID='" & intGiftCardID & "', "
		END IF
		strSQL= strSQL & " Price=" & intPrice & ", " & _
				" Qty=" & request("intQty") & ", " & _
				" Taxamt=" &	intTaxamt & _
				" Where RecItemID=" & intRecItemID & " AND LocationID="& request("LocationID")

		If NOT (dbExec(dbMain,strSQL)) Then
			Response.Write gstrMsg
		End If
	END IF
END SUB

%>
