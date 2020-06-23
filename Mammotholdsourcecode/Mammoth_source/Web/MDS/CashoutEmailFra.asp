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

	Dim dbMain,intClientID,strEmail,blSaved,strLoadMsg
	Dim strSQL, RS

	Set dbMain =  OpenConnection
		blSaved = false

	intClientID = request("intClientID")


	Select case Request("FormAction")
		Case "BtnSave"
			Call SaveEmail(dbMain,intClientID)
			blSaved = true
		Case "btnCoupon"
			Call SaveCoupon(dbMain,intClientID)
			blSaved = true
		Case "btnNoEmail"
			Call SaveNoEmail(dbMain,intClientID)
			blSaved = true
	End select


	strLoadMsg =""


%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="CashOutEmailFra.asp">
<div style="text-align:center">
<input type="hidden" name="strLoadMsg" value="<%=strLoadMsg%>">
<input type="hidden" name="intClientID" value="<%=intClientID%>">
<input type="hidden" name="blSaved" value="<%=blSaved%>">
<table border="0" cellspacing="0" cellpadding="0">

	<tr>
		<td align="right" class="control" nowrap>Ask Customer if we can have there E-Mail:&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
	<tr>
		<td align="left" class="control" nowrap><input maxlength="50" size="50" type=text tabindex=1 D name="strEmail" value="<%=strEmail%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
</table>
<br>
<table align="center" border="0" cellspacing="1" cellpadding="1">
   <tr>
      <td align="center">
	  <button name="btnSave" tabindex=2 class="button" style="width:75" OnClick="SaveChg()">Save</button>&nbsp;&nbsp;
	  <button name="btnCancel" tabindex=3 class="button" style="width:75" OnClick="CancelChg()">Cancel</button>&nbsp;&nbsp;
	  <button name="btnNoEmail" tabindex=4 class="buttonred" style="width:75" OnClick="NoEmailChg()">No-Email</button>&nbsp;&nbsp;
	  <button name="btnCoupon" tabindex=5 class="button" style="width:75" OnClick="CouponChg()">Coupon</button>
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

Sub CouponChg()
		frmMain.FormAction.value="btnCoupon"
		frmMain.submit()
End Sub

Sub NoEmailChg()
		frmMain.FormAction.value="btnNoEmail"
		frmMain.submit()
End Sub

Sub CancelChg()
	window.returnValue = 0
	parent.window.close
End Sub



</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************

Sub SaveEmail(dbMain,intClientID)
    Dim strSQL
	strSQL= "Update Client SET  Email='"& request("strEmail") &"' Where ClientID ="& intClientID 
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF

END SUB

Sub SaveCoupon(dbMain,intClientID)
    Dim strSQL,JMail
	strSQL= "Update Client SET  Email='"& request("strEmail") &"' Where ClientID ="& intClientID 
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF

        Set JMail= Server.CreateObject("JMail.Message")
        Jmail.Logging = TRUE
        Jmail.Silent = TRUE
        JMail.from = "Mammoth3@comcast.net"
        JMail.Subject = " E-Mail Request Coupon " 
        JMail.AddRecipient "Mammoth2@comcast.net"
        'JMail.AddRecipient "alan@testoil.com"
        jmail.Body = "  E-Mail Request Coupon for: "& request("strEmail")&" On: "& date()
         Jmail.Send("")
       set JMail = nothing

END SUB

Sub SaveNoEmail(dbMain,intClientID)
    Dim strSQL
	strSQL= "Update Client SET  NoEmail=1 Where ClientID ="& intClientID 
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
END SUB

%>
