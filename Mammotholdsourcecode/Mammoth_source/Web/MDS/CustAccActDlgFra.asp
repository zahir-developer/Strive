<%@  language="VBSCRIPT" %>
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

	Dim dbMain,intCustAccID, intAmount,strStatus,strCurrentAmt,intClientID,txtNotes,LocationID,LoginID
	Set dbMain =  OpenConnection
intClientID = Request("intClientID")
intCustAccID = Request("intCustAccID")
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
    <link rel="stylesheet" href="main.css" type="text/css">
    <title></title>
</head>
<body class="pgbody">
    <form method="POST" name="frmMain" action="CustAccActDlgFra.asp">
        <input type="hidden" name="strStatus" tabindex="-2" value="<%=strStatus%>">
        <input type="hidden" name="intClientID" tabindex="-2" value="<%=intClientID%>">
        <input type="hidden" name="intCustAccID" tabindex="-2" value="<%=intCustAccID%>">
        <input type="hidden" name="strCurrentAmt" tabindex="-2" value="<%=strCurrentAmt%>">
        <input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
        <div style="text-align: center">
            <table border="0" width="290" cellspacing="0" cellpadding="0">
                <tr align="center">
                    <td align="right" class="control" style="font: bold 18px arial" nowrap>Current Value:&nbsp;</td>
                    <td align="left" class="control" nowrap>
                        <label style="font: bold 18px arial" class="blkdata" id="lblCurrentAmt">
                    </td>
                    <td align="right" class="control" nowrap>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" class="control" nowrap>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" class="control" style="font: bold 18px arial" nowrap>Amount:&nbsp;</td>
                    <td align="left" class="control" nowrap>
                        <input tabindex="1" style="font: bold 18px arial" maxlength="8" size="8" type="number" tabindex="1" dirtycheck="TRUE" name="intAmount" value="<%=intAmount%>"></td>
                    <td align="right" class="control" nowrap>&nbsp;</td>
                </tr>
                <tr>
                    <td align="left" colspan="3">
                        <label class="control">Notes:</label></td>
                </tr>
                <tr>
                    <td align="left" class="control" nowrap colspan="3">
                        <textarea tabindex="2" cols="32" rows="3" title="Notes ." name="txtNotes" dirtycheck="TRUE"><%=txtNotes%>
			</textarea>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="control" nowrap>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <button name="btnCredit" class="button" style="height: 35; width: 200; font: bold 18px arial" onclick="CreditChg()">&nbsp;Credit (+)&nbsp;</button></td>
                </tr>
                <tr>
                    <td align="right" class="control" nowrap>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <button name="btnDebit" class="button" style="height: 35; width: 200; font: bold 18px arial" onclick="DebitChg()">&nbsp;Debit (-)&nbsp;</button></td>
                </tr>
                <tr>
                    <td align="right" class="control" nowrap>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <button name="btnCancel" class="button" style="height: 35; width: 200; font: bold 18px arial" onclick="CancelChg()">Cancel</button>
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

Sub Window_Onload()
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
	IF LEN(trim(frmMain.intCustAccID.value))>0 and LEN(trim(frmMain.intAmount.value))>0 then
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
	Dim strSQL,rsData,MaxSQL,intAmount,intCustAccID,intCustAccTID,strCurrentAmt,strNewAmt,LoginID,LocationID
	Dim intClientID
	intClientID = request("intClientID")
	intCustAccID = request("intCustAccID")
	intAmount = ABS(request("intAmount"))
	strCurrentAmt = request("strCurrentAmt")
	LoginID = request("LoginID")
    LocationID = request("LocationID")
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
			strSQL= "Insert into CustAccHist (CustAccTID,CustAccID,TXCustID,TXLocationID,TXDte,TXType,TXNote,TXAmt,TXUser) Values ("&_
				intCustAccTID &","&_
				intCustAccID &","&_
				intClientID &","&_
				LocationID &","&_
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
	intAmount = ABS(request("intAmount"))
	strCurrentAmt = request("strCurrentAmt")
	LoginID = request("LoginID")
	LocationID = request("LocationID")
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
			strSQL= "Insert into CustAccHist (CustAccTID,CustAccID,TXCustID,TXLocationID,TXDte,TXNote,TXType,TXAmt,TXUser) Values ("&_
				intCustAccTID &","&_
				intCustAccID &","&_
				intClientID &","&_
				LocationID &","&_
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



%>
