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

	Dim dbMain,intCustAccID,intvehid,intClientID,strStatus,intAmount,txtNotes
	dim strCurrentAmt, strMonthlyCharge, strLimit, strCCNo, intCCType
	Dim MaxSQL,rsData,strSQL,strCCEXP1,strCCEXP2,LoginID,LocationID
	Set dbMain =  OpenConnection

	intClientID = Request("intClientID")
	intCustAccID = Request("intCustAccID")
	intvehid = Request("intvehid")
	LoginID = Request("LoginID")
	LocationID = Request("LocationID")



	Select Case Request("FormAction")
		Case "SaveChg"
			Call SaveChg(dbMain)
		Case "CreditChg"
			Call CreditChg(dbMain)
		Case "DebitChg"
			Call DebitChg(dbMain)
	End Select


	strSQL="SELECT isnull(CurrentAmt,0.0) as CurrentAmt, isnull(MonthlyCharge,0.0) as MonthlyCharge, isnull(Limit,0.0) as Limit, CCNo, CCType,Status,CCEXP FROM CustAcc(Nolock) WHERE CustAccID=" & intCustAccID 
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		If Not rsData.EOF Then
			strCurrentAmt = rsData("CurrentAmt")
			strMonthlyCharge = rsData("MonthlyCharge")
			strLimit = rsData("Limit")
			strCCNo = rsData("CCNo")
			intCCType = rsData("CCType")
			strStatus = rsData("Status")
			strCCEXP1 = left(rsData("CCEXP"),2)
			strCCEXP2 = right(rsData("CCEXP"),2)
		End If
		Set rsData = Nothing
	End If

	IF isnull(strCurrentAmt) then
		strCurrentAmt = 0
	END IF

%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="ClientVehAccEditDlgFra.asp">
<input type="hidden" name="strStatus" tabindex="-2" value="<%=strStatus%>">
<input type="hidden" name="intClientID" tabindex="-2" value="<%=intClientID%>">
<input type="hidden" name="intCustAccID" tabindex="-2" value="<%=intCustAccID%>">
<input type="hidden" name="strCurrentAmt" tabindex="-2" value="<%=strCurrentAmt%>">
<input type="hidden" name="LoginID" tabindex="-2" value="<%=LoginID%>">
<input type="hidden" name="LocationID" tabindex="-2" value="<%=LocationID%>">
<input type="hidden" name="strStatus" tabindex="-2" value="<%=strStatus%>">
<input type="hidden" name="strCCNo" tabindex="-2" value="<%=strCCNo%>">
<input type="hidden" name="strCCEXP1" tabindex="-2" value="<%=strCCEXP1%>">
<input type="hidden" name="strCCEXP2" tabindex="-2" value="<%=strCCEXP2%>">
<div style="text-align:center">
<table border="0" width="625" cellspacing="0" cellpadding="4">
	<tr align="center">
		<td align="right" class="control" nowrap>Current Value:&nbsp;</td>
        <td align="left" class="control" nowrap><label class="blkdata" ID="lblCurrentAmt" ></td>
 			<td align="right" class="control" nowrap>Active:</td>
		<td align="left">
			<INPUT name=chkStatus type=checkbox  DirtyCheck="TRUE"
				<%If strStatus Then%>
					checked
				<%End If%>>
		</td>
		<td align="right" class="control" nowrap>&nbsp;</td>

	</tr>
	<tr>
		<td align="right" class="control" nowrap>MonthlyCharge:&nbsp;</td>
        <td align="left" class="control" nowrap><input tabindex="1" maxlength="8" size="8" type=number tabindex=1 DirtyCheck=TRUE name="strMonthlyCharge" value="<%=formatcurrency(strMonthlyCharge,2)%>"></td>
		<td align="right" class="control" nowrap>Limit:&nbsp;</td>
        <td align="left" class="control" nowrap><input tabindex="2" maxlength="8" size="8" type=number tabindex=1 DirtyCheck=TRUE name="strLimit" value="<%=formatcurrency(strLimit,2)%>"></td>
	</tr>
<!--	<tr>
		<td align="right" class="control" nowrap>Credit Card No:&nbsp;</td>
        <td align="left" class="control" nowrap><input tabindex="3" maxlength="20" size="20" type=number tabindex=1 DirtyCheck=TRUE name="strCCNo" value="<%=strCCNo%>"></td>
		<td align="right" class="control" nowrap>Type:&nbsp;</td>
		<td align="left" class="control" nowrap>
		<Select name="cboCCType" tabindex=4 DirtyCheck=TRUE>
			<%Call LoadList(dbMain,12,intCCType)%>		
		</select>
		EXP:&nbsp;
			<input tabindex="5" maxlength="2" size="1" type=number tabindex=1 DirtyCheck=TRUE name="strCCEXP1" value="<%=strCCEXP1%>">
			&nbsp;/&nbsp;<input tabindex="6" maxlength="2" size="1" type=number  DirtyCheck=TRUE name="strCCEXP2" value="<%=strCCEXP2%>">
		</td>
	</tr>-->
</table>
<hr>
<table border="0" width="625" cellspacing="0" cellpadding="4">
	<tr>
		<td align="right" class="control" nowrap>Amount:&nbsp;</td>
        <td align="left" class="control" nowrap><input tabindex="7" maxlength="8" size="8" type=number tabindex=7 DirtyCheck=TRUE name="intAmount" value="<%=intAmount%>"></td>
	     <td align="left">
	     <button  name="btnCredit" class="button"  OnClick="CreditChg()">&nbsp;Credit (+)&nbsp;</button>&nbsp;&nbsp;&nbsp;
	     </td>
	     <td align="left">
	     <button  name="btnDebit" class="button"  OnClick="DebitChg()">&nbsp;Debit (-)&nbsp;</button>&nbsp;&nbsp;&nbsp;
	     </td>
	</tr>
	<tr>
		<td align="right"><label class="control">Notes:</label></td>
		<td align="left" class="control" nowrap colspan=3>
			<Textarea tabindex="8" COLS="60" ROWS="3" Title="Notes ." name="txtNotes" DirtyCheck=TRUE><%=txtNotes%>
			</Textarea>
		</td>
	</tr>
</table>
	<iframe Name="fraAcc" src="admLoading.asp" scrolling="yes" height="140" width="600" frameborder="0"></iframe>
</div>
<table border="0" width="600" cellspacing="0" cellpadding="4">
	<tr>
		<td align="right" class="control" nowrap>
	     <button  name="btnSave" class="button"  OnClick="SaveChg()">&nbsp;Save&nbsp;</button>&nbsp;&nbsp;&nbsp;
	     <button  name="btnDone" class="button"  OnClick="CancelChg()">&nbsp;Done&nbsp;</button>
	     </td>
	</tr>
</table>
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

Sub Window_Onload()
	document.all("lblCurrentAmt").innerText = formatcurrency(document.all("strCurrentAmt").value,2)
	fraAcc.location.href = "ClientVehAccHistFra.asp?intCustAccID=" & document.all("intCustAccID").Value
	'IF frmMain.strStatus.value="Yes" then
	'	parent.window.close
	'END IF
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
	IF LEN(trim(frmMain.intCustAccID.value))>0 and LEN(trim(frmMain.intAmount.value))>0 then
		frmMain.FormAction.value="CreditChg"
		frmMain.Submit()
	else
		msgbox "You must enter all fields"
	END IF
End Sub
Sub SaveChg()
	frmMain.FormAction.value="SaveChg"
	frmMain.Submit()
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
	Dim strSQL,rsData,MaxSQL,intAmount,intCustAccID,intCustAccTID,strCurrentAmt,strNewAmt,LoginID,LocationID
	Dim intClientID
	intClientID = request("intClientID")
	intCustAccID = request("intCustAccID")
	LoginID = request("LoginID")
	LocationID = request("LocationID")
	intAmount = ABS(request("intAmount"))
	strCurrentAmt = request("strCurrentAmt")
	strNewAmt = cdbl(strCurrentAmt) - cdbl(intAmount)
	intAmount =  cdbl(intAmount)*-1
	strSQL="SELECT CustAccID,ActiveDte,CurrentAmt FROM CustAcc(Nolock) WHERE CustAccID=" & intCustAccID 
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF NOT rsData.eof then
			strSQL= "Update CustAcc SET "&_
				" CurrentAmt=" & strNewAmt &","&_
				" LastUpdate='" & date() &"',"&_
				" LastUpdateBy=" & LoginID &_
				" Where CustAccID ="& intCustAccID 
			IF NOT DBExec(dbMain, strSQL) then
				Response.Write gstrMsg
				Response.End
			END IF
			MaxSQL = " Select ISNULL(Max(CustAccTID),0)+1 from CustAccHist (NOLOCK)"
			If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
				intCustAccTID=rsData(0)
			End if
			Set rsData = Nothing
			strSQL= "Insert into CustAccHist (LocationID,CustAccTID,CustAccID,TXCustID,TXDte,TXType,TXNote,TXAmt,TXUser) Values ("&_
				LocationID &","&_
				intCustAccTID &","&_
				intCustAccID &","&_
				intClientID &","&_
				"'" & Date() &"',"&_
				"'Debit',"&_
				"'" & Request("txtNotes") &"',"&_
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
	Dim strSQL,rsData,MaxSQL,intAmount,intCustAccID,intCustAccTID,strCurrentAmt,strNewAmt,LoginID,LocationID
	Dim intClientID
	intClientID = request("intClientID")
	intCustAccID = request("intCustAccID")
	LoginID = request("LoginID")
	LocationID = request("LocationID")
	intAmount = ABS(request("intAmount"))
	strCurrentAmt = request("strCurrentAmt")
	strNewAmt = cdbl(strCurrentAmt) + cdbl(intAmount)
	strSQL="SELECT CustAccID,ActiveDte,CurrentAmt FROM CustAcc(Nolock) WHERE CustAccID=" & intCustAccID 
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF NOT rsData.eof then
			strSQL= "Update CustAcc SET "&_
				" CurrentAmt=" & strNewAmt &","&_
				" LastUpdate='" & date() &"',"&_
				" LastUpdateBy=" & LoginID &_
				" Where CustAccID ='"& intCustAccID & "'"
			IF NOT DBExec(dbMain, strSQL) then
				Response.Write gstrMsg
				Response.End
			END IF
			MaxSQL = " Select ISNULL(Max(CustAccTID),0)+1 from CustAccHist (NOLOCK)"
			If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
				intCustAccTID=rsData(0)
			End if
			Set rsData = Nothing
			strSQL= "Insert into CustAccHist (LocationID,CustAccTID,CustAccID,TXCustID,TXDte,TXNote,TXType,TXAmt,TXUser) Values ("&_
				LocationID &","&_
				intCustAccTID &","&_
				intCustAccID &","&_
				intClientID &","&_
				"'" & Date() &"',"&_
				"'" & Request("txtNotes") &"',"&_
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

Sub SaveChg(dbMain)
	Dim strSQL,rsData,intCustAccID,strCCEXP
	dim strCurrentAmt, strMonthlyCharge, strLimit, strCCNo, intCCType,blStatus,LoginID,LocationID

	intCustAccID = request("intCustAccID")
	LoginID = request("LoginID")
	LocationID = request("LocationID")
	strMonthlyCharge = request("strMonthlyCharge")
	strLimit = request("strLimit")
	'strCCNo = request("strCCNo")
	'strCCEXP = request("strCCEXP1")&"/"& request("strCCEXP2")
	'intCCType = request("cboCCType")

	IF   request("chkStatus") = "on" then
		blStatus = 1
	ELSE
		blStatus = 0
	END IF


	strSQL= "Update CustAcc SET "&_
		" MonthlyCharge=" & strMonthlyCharge &","&_
		" Limit=" & strLimit &","&_
		" Status=" & blStatus &","&_
		" LastUpdate='" & date() &"',"&_
		" LastUpdateBy=" & LoginID &_
		" Where CustAccID ='"& intCustAccID & "'"
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
End Sub



%>
