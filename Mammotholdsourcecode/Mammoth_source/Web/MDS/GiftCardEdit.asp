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
<!--#include file="incCommon.asp"-->
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

Dim dbMain, intGiftCardID,dtActiveDte,strCurrentAmt
Dim strStatus,hdnFilterBy,LocationID,LoginID

Set dbMain =  OpenConnection

intGiftCardID=Request("intGiftCardID")
    LocationID = request("LocationID")
    LoginID = request("LoginID")

Select Case Request("FormAction")
	Case "btnSel"
		Call SelectData(dbMain,intGiftCardID,dtActiveDte,strCurrentAmt,LocationID,LoginID)
End Select


If LEN(TRIM(intGiftCardID))>0 Then 
	Call GetGiftCardInfo(dbMain,intGiftCardID,dtActiveDte,strCurrentAmt)
End If
IF Len(trim(strCurrentAmt))=0 then
	strCurrentAmt = 0.00
END IF
IF Len(trim(dtActiveDte))=0 then
	dtActiveDte = "None"
END IF
'********************************************************************
'HTML
'********************************************************************
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<title></title>
</head>
<body class="pgbody" Onclick="SetDirty" onkeyup="SetDirty()">
<form method="POST" name="frmMain" action="GiftCardEdit.asp"> 
<input type="hidden" name="FormAction" tabindex="-2" value>
<input type="hidden" name="dtActiveDte" tabindex="-2" value="<%=dtActiveDte%>" />
<input type="hidden" name="strCurrentAmt" tabindex="-2" value="<%=strCurrentAmt%>" />

        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<div style="text-align: center">

<table width="768px">
	<tr>
		<td align="right" class="control" style="font:bold 18px arial"nowrap>Number:</td>
        <td align="left" class="control" nowrap><input style="font:bold 18px arial" maxlength="8" size="8" type=number tabindex=1 name="intGiftCardID" value="<%=intGiftCardID%>">&nbsp;&nbsp;
			<button  name="btnSel" tabindex="1" onmouseover="ButtonHigh()" onmouseout="ButtonLow()"  OnClick="SubmitForm()" >&nbsp;Select&nbsp;</button>&nbsp;&nbsp;
		</td>
	</tr>
	<tr>
		<td align="right" class="control" style="font:bold 18px arial" nowrap>Active Date:</td>
        <td align="left" class="control" nowrap><label style="font:bold 18px arial" class="blkdata" ID="lblActiveDte" ></td>
	</tr>
	<tr>
		<td align="right" class="control" style="font:bold 18px arial"  nowrap>Value:</td>
        <td align="left" class="control" nowrap><label style="font:bold 18px arial" class="blkdata" ID="lblCurrentAmt" ></label></td>
	</tr>
</table>
<%If len(trim(intGiftCardID)) > 1 Then%>
<table class="tblcaption" cellspacing=0 cellpadding=0 width=500>
	<tr>
		<td align=center class="tdcaption" background="images/header.jpg" width=110>Activity</td>
		<td align=right>			
			<button  name="btnAddInv" align="right" type="submit" OnClick="SubmitForm()">Add Activity</button>
		</td>
	</tr>
</table>
<iframe Name="fraMain" src="admLoading.asp" scrolling="yes" height="200" width="500" frameborder="0"></iframe>
<% End IF %>
<hr>
<table border="0" width="768" cellspacing="0" cellpadding="0">
	<tr>
		<td width="100%"><div align="right">
			<button name="btnBatch" tabindex="14" onmouseover="ButtonHigh()" onmouseout="ButtonLow()"  OnClick="SubmitForm()" >&nbsp;Batch Add&nbsp;</button>&nbsp;&nbsp;
			<button style="display:none" name="btnSave" class="buttondead" tabindex="15" onmouseover="ButtonHigh()" onmouseout="ButtonLow()"  OnClick="SubmitForm()" >&nbsp;Save&nbsp;</button>&nbsp;&nbsp;
			<button name="btnDone" width="90"  tabindex="16" onmouseover="ButtonHigh()" onmouseout="ButtonLow()"  OnClick="SubmitForm()" >&nbsp;Done&nbsp;</button>
		</div></td>
	</tr>
</table>
</div>
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

Sub Window_Onload()
	document.all("lblActiveDte").innerText = document.all("dtActiveDte").value
	document.all("lblCurrentAmt").innerText = formatcurrency(document.all("strCurrentAmt").value,2)
	IF len(trim(frmMain.intGiftCardID.value))>1 then
		fraMain.location.href = "GiftCardFra.asp?intGiftCardID=" & frmMain.intGiftCardID.Value
	END IF
	frmMain.intGiftCardID.focus()
	frmMain.intGiftCardID.select()
	

End Sub


Sub Window_OnBeforeUnLoad
	Call dirtycheck
End Sub

Sub SubmitForm()
	window.event.CancelBubble=True
	window.event.ReturnValue=False

	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	Select Case window.event.srcElement.name
		Case "btnDone"
			location.href="admWelcome.asp"
		Case "btnSel"
			IF len(trim(frmMain.intGiftCardID.value))>1 then
				Call ResetDirty
				frmMain.FormAction.value="btnSel"
				frmMain.Submit()
			ELSE
				msgbox "Invalid Gift Card Number"
			END IF
		Case "btnBatch"
			Dim strBatch
				strBatch= ShowModalDialog("GiftCardBatchDlg.asp?LocationID=" & frmMain.LocationID.value &"&LoginID=" & frmMain.LoginID.value ,"","dialogwidth:350px;dialogheight:350px;")
		Case "btnAddInv"
			Dim strAct
				strAct= ShowModalDialog("GiftCardActDlg.asp?intGiftCardID=" & frmMain.intGiftCardID.value &"&strCurrentAmt=" &frmMain.strCurrentAmt.value &"&LocationID=" & frmMain.LocationID.value &"&LoginID=" & frmMain.LoginID.value ,"","dialogwidth:350px;dialogheight:370px;")
				frmMain.Submit()
 	End Select
End Sub


</script>
<%
	Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Sub GetGiftCardInfo(dbMain,intGiftCardID,dtActiveDte,strCurrentAmt)
	
	Dim strSQL, RS 

	strSQL="SELECT GiftCardID,ActiveDte,CurrentAmt FROM GiftCard(Nolock) WHERE ltrim(rtrim(GiftCardID))='" & trim(intGiftCardID) & "'"
	If DBOpenRecordset(dbMain,rs,strSQL) Then
		If Not RS.EOF Then
			intGiftCardID = RS("GiftCardID")
			dtActiveDte = RS("ActiveDte")
			strCurrentAmt = RS("CurrentAmt")
		End If
	End If
End Sub


Sub SelectData(dbMain,intGiftCardID,dtActiveDte,strCurrentAmt,LocationID,LoginID)
	Dim strSQL,rsData,MaxSQL,intGiftCardTID
	strSQL="SELECT GiftCardID,ActiveDte,CurrentAmt FROM GiftCard(Nolock) WHERE GiftCardID=" & intGiftCardID 
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF NOT rsData.eof then
			intGiftCardID = rsData("GiftCardID")
			dtActiveDte = rsData("ActiveDte")
			strCurrentAmt = rsData("CurrentAmt")
		ELSE
			strSQL= "Insert into GiftCard (GiftCardID,LocationID,ActiveDte,CurrentAmt) Values ("&_
				"'" & intGiftCardID &"',"&_
				 LocationID &","&_
				"'" & Date() &"',"&_
				0.00 &")"
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
				0.00 &","& LoginID &")"
			IF NOT DBExec(dbMain, strSQL) then
				Response.Write gstrMsg
				Response.End
			END IF
		END IF
	END IF
	Set rsData = Nothing
End Sub




%>
