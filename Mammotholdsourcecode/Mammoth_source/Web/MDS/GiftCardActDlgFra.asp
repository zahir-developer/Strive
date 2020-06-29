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

	Dim dbMain,intGiftCardID, intAmount,strStatus,strCurrentAmt,LocationID,LoginID
	Set dbMain =  OpenConnection
intGiftCardID = Request("intGiftCardID")
strCurrentAmt = Request("strCurrentAmt")
    LocationID = request("LocationID")
    LoginID = request("LoginID")
Select Case Request("FormAction")
	Case "CreditChg"
		Call CreditChg(dbMain)
		strStatus = "Yes"
	Case "DebitChg"
		
		Call DebitChg(dbMain)
		strStatus = "Yes"
End Select


%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="GiftCardActDlgFra.asp">
<input type="hidden" name="strStatus" tabindex="-2" value="<%=strStatus%>">
<input type="hidden" name="intGiftCardID" tabindex="-2" value="<%=intGiftCardID%>">
<input type="hidden" name="strCurrentAmt" tabindex="-2" value="<%=strCurrentAmt%>">
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<div style="text-align:center">
<table border="0" width="290" cellspacing="0" cellpadding="0">
	<tr align="center">
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="left" class="control" nowrap>&nbsp;</td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr align="center">
		<td align="right" class="control" style="font:bold 18px arial"nowrap>Gift Card #:&nbsp;</td>
        <td align="left" class="control" nowrap><label style="font:bold 18px arial" class="blkdata" ID="lblGiftCardID" ></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr align="center">
		<td align="right" class="control" style="font:bold 18px arial"nowrap>Current Value:&nbsp;</td>
        <td align="left" class="control" nowrap><label style="font:bold 18px arial" class="blkdata" ID="lblCurrentAmt" ></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" style="font:bold 18px arial"nowrap>Amount:&nbsp;</td>
        <td align="left" class="control" nowrap><input style="font:bold 18px arial" maxlength="8" size="8" type=number tabindex=1 DirtyCheck=TRUE name="intAmount" value="<%=intAmount%>"></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
	     <td colspan=3 ALIGN="center"><button  name="btnCredit" class="button"  style="height:35;width:200;font:bold 18px arial" OnClick="CreditChg()">&nbsp;Credit (+)&nbsp;</button></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
	     <td colspan=3 ALIGN="center"><button  name="btnDebit" class="button"  style="height:35;width:200;font:bold 18px arial" OnClick="DebitChg()">&nbsp;Debit (-)&nbsp;</button></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
   <tr>
      <td  colspan=3  align="center" >
	  <button name="btnCancel" class="button" style="height:35;width:200;font:bold 18px arial" OnClick="CancelChg()">Cancel</button>
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
<script language="VBSCRIPT">
Option Explicit

Sub Window_Onload()
	document.all("lblGiftCardID").innerText = document.all("intGiftCardID").value
	document.all("lblCurrentAmt").innerText = formatcurrency(document.all("strCurrentAmt").value,2)

	IF frmMain.strStatus.value="Yes" then
		parent.window.close
	END IF
End Sub


Sub DebitChg()
	IF LEN(trim(frmMain.intAmount.value))>0 then
		IF cdbl(frmMain.intAmount.value) <= cdbl(frmMain.strCurrentAmt.value) then
			frmMain.FormAction.value="DebitChg"
			frmMain.Submit()
		else
			msgbox "The Amount can not be greater then the Current Value"
			frmMain.intAmount.value = ""
		END IF
	else
		msgbox "You must enter the Amount"
	END IF
End Sub
Sub CreditChg()
	IF LEN(trim(frmMain.intGiftCardID.value))>0 and LEN(trim(frmMain.intAmount.value))>0 then
		frmMain.FormAction.value="CreditChg"
		frmMain.Submit()
	else
		msgbox "You must enter all fields"
	END IF
End Sub


Sub CancelChg()
	parent.window.close
End Sub

</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************
Sub DebitChg(dbMain)
	Dim strSQL,rsData,MaxSQL,intAmount,intGiftCardID,intGiftCardTID,strCurrentAmt,strNewAmt,LocationID,LoginID

    LocationID = request("LocationID")
    LoginID = request("LoginID")
	intGiftCardID = request("intGiftCardID")
	intAmount = ABS(request("intAmount"))
	strCurrentAmt = request("strCurrentAmt")
	strNewAmt = cdbl(strCurrentAmt) - cdbl(intAmount)
	intAmount =  cdbl(intAmount)*-1
	strSQL="SELECT GiftCardID,ActiveDte,CurrentAmt FROM GiftCard(Nolock) WHERE GiftCardID='" & intGiftCardID &"'"
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF NOT rsData.eof then
			strSQL= "Update GiftCard SET "&_
				" CurrentAmt=" & strNewAmt &""&_
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
			strSQL= "Insert into GiftCardHist (GiftCardTID,LocationID,GiftCardID,TransDte,TransType,TransAmt,TransUserID) Values ("&_
				intGiftCardTID &","&_
				LocationID &","&_
				"'" & intGiftCardID &"',"&_
				"'" & Date() &"',"&_
				"'Debit',"&_
				intAmount &","& LoginID &")"
			IF NOT DBExec(dbMain, strSQL) then
				Response.Write gstrMsg
				Response.End
			END IF
		END IF
	END IF
	Set rsData = Nothing

End Sub

Sub CreditChg(dbMain)
	Dim strSQL,rsData,MaxSQL,intAmount,intGiftCardID,intGiftCardTID,strCurrentAmt,strNewAmt,LocationID,LoginID

    LocationID = request("LocationID")
    LoginID = request("LoginID")
	intGiftCardID = request("intGiftCardID")
	intAmount = ABS(request("intAmount"))
	strCurrentAmt = request("strCurrentAmt")
	strNewAmt = cdbl(strCurrentAmt) + cdbl(intAmount)
	strSQL="SELECT GiftCardID,ActiveDte,CurrentAmt FROM GiftCard(Nolock) WHERE GiftCardID='" & intGiftCardID &"'" 
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF NOT rsData.eof then
			strSQL= "Update GiftCard SET "&_
				" CurrentAmt=" & strNewAmt &""&_
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
			strSQL= "Insert into GiftCardHist (GiftCardTID,LocationID,GiftCardID,TransDte,TransType,TransAmt,TransUserID) Values ("&_
				intGiftCardTID &","&_
				LocationID &","&_
				"'" & intGiftCardID &"',"&_
				"'" & Date() &"',"&_
				"'Credit',"&_
				intAmount &","& LoginID &")"
			IF NOT DBExec(dbMain, strSQL) then
				Response.Write gstrMsg
				Response.End
			END IF
		END IF
	END IF
	Set rsData = Nothing

End Sub



%>
