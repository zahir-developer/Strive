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

	Dim dbMain,intGiftCardID, intAmount,strStatus,strCurrentAmt,intGiftCardAmt,intRecID,intBalance,hdnAmt,LocationID,LoginID
	Set dbMain =  OpenConnection

intGiftCardID = Request("intGiftCardID")
intRecID = Request("intRecID")
LocationID = request("LocationID")
LoginID = request("LoginID")
strCurrentAmt = Request("strCurrentAmt")
hdnAmt = Request("hdnAmt")
intGiftCardAmt = Request("intGiftCardAmt")
intAmount = Request("intAmount")
Select Case Request("FormAction")
	Case "ProcessChg"
		Call ProcessChg(dbMain)
		strStatus = "Yes"
End Select
IF LEN(trim(intGiftCardID))>1 then
	Call GetGiftCardInfo(dbMain,intGiftCardID,strCurrentAmt,intAmount,intGiftCardAmt,LocationID,LoginID)
	IF isnull(strCurrentAmt) or len(trim(strCurrentAmt))=0 then
		strCurrentAmt = 0.0
	END IF
	IF isnull(intAmount) or len(trim(intAmount))=0 then
		intAmount = 0.0
	END IF
	intBalance = cdbl(strCurrentAmt) - cdbl(intAmount)
ELSE
	intGiftCardID = ""
	strCurrentAmt = 0.0
	intAmount = ""
	intBalance = 0.0
END IF

%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="CashoutGiftCardDlgFra.asp">
<input type="hidden" name="strStatus" tabindex="-2" value="<%=strStatus%>">
<input type="hidden" name="intRecID" tabindex="-2" value="<%=intRecID%>">
<input type="hidden" name="strCurrentAmt" tabindex="-2" value="<%=strCurrentAmt%>">
<input type="hidden" name="hdnAmt" tabindex="-2" value="<%=hdnAmt%>">
<input type="hidden" name="intBalance" tabindex="-2" value="<%=intBalance%>">
<input type="hidden" name="intGiftCardAmt" tabindex="-2" value="<%=intGiftCardAmt%>">
<input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<div style="text-align:center">

<br>
<table border="0" width="290" cellspacing="0" cellpadding="0">
	<tr align="center">
		<td align="right" class="control" style="font:bold 18px arial"nowrap>Amount Due:&nbsp;</td>
        <td align="left" class="control" nowrap><label style="font:bold 18px arial" class="blkdata" ID="lblGiftCardAmt" ></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr align="center">
		<td align="right" class="control" style="font:bold 18px arial"nowrap>Gift Card #:&nbsp;</td>
        <td align="left" class="control" nowrap><input style="font:bold 18px arial" maxlength="8" size="8" type=number tabindex=1 onkeyPress="Check4Enter()" name="intGiftCardID" value="<%=intGiftCardID%>"></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
	     <td colspan=3 ALIGN="center"><button  name="btnSelect" class="button"  style="height:35;width:200;font:bold 18px arial" OnClick="SelectChg()">&nbsp;Select&nbsp;</button></td>
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
        <td align="left" class="control" nowrap><input style="font:bold 18px arial" maxlength="8" size="8" type=number tabindex=1 name="intAmount" value="<%=intAmount%>"></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr align="center">
		<td align="right" class="control" style="font:bold 18px arial"nowrap>Balance:&nbsp;</td>
        <td align="left" class="control" nowrap><label style="font:bold 18px arial" class="blkdata" ID="lblBalance" ></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
	     <td colspan=3 ALIGN="center"><button  name="btnProcess" class="button"  style="height:35;width:200;font:bold 18px arial" OnClick="ProcessChg()">&nbsp;Process&nbsp;</button></td>
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
	document.all("lblGiftCardAmt").innerText = formatcurrency(document.all("intGiftCardAmt").value,2)
	document.all("lblCurrentAmt").innerText = formatcurrency(document.all("strCurrentAmt").value,2)
	document.all("lblBalance").innerText = formatcurrency(document.all("intBalance").value,2)
	IF frmMain.strStatus.value="Yes" then
		window.returnValue = frmMain.intGiftCardID.value +"|"+document.all("hdnAmt").value
		parent.window.close
	END IF
	IF frmMain.strStatus.value="No" then
		msgbox "Gift Card is not Active"
	END IF
End Sub

Sub Check4Enter()
	If window.event.keycode = 13 Then
		window.event.cancelbubble = True
		window.event.returnvalue = FALSE
		SelectChg()
	End If
End Sub

Sub SelectChg()
	IF LEN(trim(frmMain.intGiftCardID.value))>1 then
		frmMain.FormAction.value="SelectChg"
		frmMain.Submit()
	else
		msgbox "You must enter the Gift Card #"
	END IF
End Sub
Sub ProcessChg()
	IF LEN(trim(frmMain.intGiftCardID.value))>0 and LEN(trim(frmMain.intAmount.value))>0 then
		IF cdbl(frmMain.intAmount.value) <= cdbl(frmMain.strCurrentAmt.value) then
			frmMain.hdnAmt.value=frmMain.intAmount.value
			frmMain.FormAction.value="ProcessChg"
			frmMain.Submit()
		else
			msgbox "The Amount can not be greater then the Current Value"
			frmMain.intAmount.value = ""
		END IF
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
Sub GetGiftCardInfo(dbMain,intGiftCardID,strCurrentAmt,intAmount,intGiftCardAmt,LocationID,LoginID)
	Dim strSQL, RS 
	strSQL="SELECT GiftCardID,CurrentAmt FROM GiftCard(Nolock) WHERE GiftCardID='" & intGiftCardID &"'" 
	If DBOpenRecordset(dbMain,rs,strSQL) Then
		If Not RS.EOF Then
			intGiftCardID = RS("GiftCardID")
			strCurrentAmt = RS("CurrentAmt")
			IF cdbl(intGiftCardAmt) > cdbl(strCurrentAmt) then
				intAmount = formatnumber(strCurrentAmt,2)
			ELSE
				intAmount = formatnumber(intGiftCardAmt,2)
			END IF
		ELSE
			intGiftCardID = ""
			strCurrentAmt = ""
			intAmount = ""
		End If
	End If
End Sub


Sub ProcessChg(dbMain)
	Dim strSQL,rsData,MaxSQL,intAmount,intGiftCardID,intGiftCardTID,strCurrentAmt,strNewAmt,intRecID

	intRecID = request("intRecID")
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
			strSQL= "Insert into GiftCardHist (GiftCardTID,LocationID,GiftCardID,TransDte,TransType,TransAmt,TransUserID,RecID) Values ("&_
				intGiftCardTID &","&_
				 request("LocationID") &","&_
				"'" & intGiftCardID &"',"&_
				"'" & Date() &"',"&_
				"'Ticket',"&_
				intAmount &","& request("LoginID") &","& intRecID &")"
			IF NOT DBExec(dbMain, strSQL) then
				Response.Write gstrMsg
				Response.End
			END IF
		END IF
	END IF
	Set rsData = Nothing

End Sub


%>
