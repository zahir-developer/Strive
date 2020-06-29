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

	Dim dbMain,intFirstCard, intNoCards,intAmount,strStatus,LocationID,LoginID
	Set dbMain =  OpenConnection
        LocationID = request("LocationID")
    LoginID = request("LoginID")



Select Case Request("FormAction")
	Case "ProcessChg"
		Call ProcessChg(dbMain)
		strStatus = "Yes"
End Select


%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="GiftCardBatchDlgFra.asp">
<input type="hidden" name="strStatus" tabindex="-2" value="<%=strStatus%>">
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
		<td align="right" class="control" style="font:bold 18px arial"nowrap>First Card #:&nbsp;</td>
        <td align="left" class="control" nowrap><input style="font:bold 18px arial" maxlength="8" size="8" type=number tabindex=1 DirtyCheck=TRUE name="intFirstCard" value="<%=intFirstCard%>"></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" style="font:bold 18px arial"nowrap># of Cards:&nbsp;</td>
        <td align="left" class="control" nowrap><input style="font:bold 18px arial" maxlength="3" size="3" type=number tabindex=1 DirtyCheck=TRUE name="intNoCards" value="<%=intNoCards%>"></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" style="font:bold 18px arial"nowrap>Amount:&nbsp;</td>
        <td align="left" class="control" nowrap><input style="font:bold 18px arial" maxlength="8" size="8" type=number tabindex=1 DirtyCheck=TRUE name="intAmount" value="<%=formatcurrency(intAmount,2)%>"></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
	     <td colspan=3 ALIGN="center"><button  name="btnSel" class="button"  style="height:35;width:200;font:bold 18px arial" OnClick="ProcessChg()">&nbsp;Process&nbsp;</button></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
   <tr>
      <td  colspan=3  align="center" >
	  <button name="btnCancel" class="button" style="height:35;width:200;font:bold 18px arial" OnClick="CancelChg()">Cancel</button>
		</td>
	</tr>		
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
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
	IF frmMain.strStatus.value="Yes" then
		parent.window.close
	END IF
End Sub


Sub ProcessChg()
	IF LEN(trim(frmMain.intFirstCard.value))>0 and LEN(trim(frmMain.intNoCards.value))>=1 and LEN(trim(frmMain.intAmount.value))>0 then
		frmMain.FormAction.value="ProcessChg"
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
Sub ProcessChg(dbMain)
	Dim strSQL,rsData,MaxSQL,intFirstCard, intNoCards,intAmount,intGiftCardID,intGiftCardTID,LocationID,LoginID

	intGiftCardID = request("intFirstCard")
	intNoCards = request("intNoCards")
	intAmount = request("intAmount")
    LocationID = request("LocationID")
    LoginID = request("LoginID")

	DO while intNoCards > 0 
	strSQL="SELECT GiftCardID,ActiveDte,CurrentAmt FROM GiftCard(Nolock) WHERE GiftCardID=" & intGiftCardID 
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF rsData.eof then
			strSQL= "Insert into GiftCard (GiftCardID,LocationID,ActiveDte,CurrentAmt) Values ("&_
				 LocationID &","&_
				"'" & intGiftCardID &"',"&_
				"'" & Date() &"',"&_
				intAmount &")"
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
				"'Activated',"&_
				intAmount &","& LoginID &")"
			IF NOT DBExec(dbMain, strSQL) then
				Response.Write gstrMsg
				Response.End
			END IF
		END IF
	END IF
	Set rsData = Nothing
	intGiftCardID = cstr(intGiftCardID + 1)
	
	intNoCards = intNoCards -1
	Loop
	
End Sub



%>
