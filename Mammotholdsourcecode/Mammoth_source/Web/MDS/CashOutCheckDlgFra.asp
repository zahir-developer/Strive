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

	Dim dbMain,intCheckAmt, intCardType,hdnCheckAmt,strChkNo,strChkPhone,strChkDL
	Dim strSQL, RS 

	Set dbMain =  OpenConnection
	intCheckAmt = request("intCheckAmt")
	hdnCheckAmt = intCheckAmt
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="CashOutCheckDlgFra.asp">
<div style="text-align:center">
<input type="hidden" name="hdnCheckAmt" value="<%=hdnCheckAmt%>">
<table border="0" width="390" cellspacing="0" cellpadding="0">
<tr>
<td align="right" class="control" nowrap><label style="font:bold 18px arial" class="blkdata">Amount:&nbsp;</label></td>
<td align="left" ><INPUT align=right TYPE=text SIZE=5 NAME="intCheckAmt" style="font:bold 28px arial" value="<%=intCheckAmt%>"></td>
</tr>
<tr>
<td align="right" class="control" nowrap><label style="font:bold 18px arial" class="blkdata">Number:&nbsp;</label></td>
<td align="left" ><INPUT align=right TYPE=text SIZE=4 NAME="strChkNo" style="font:bold 28px arial" value="<%=strChkNo%>"></td>
</tr>
<tr>
<td align="right" class="control" nowrap><label style="font:bold 18px arial" class="blkdata">Phone:&nbsp;</label></td>
<td align="left" ><INPUT align=right TYPE=text SIZE=10 NAME="strChkPhone" style="font:bold 28px arial" value="<%=strChkPhone%>"></td>
</tr>
<tr>
<td align="right" class="control" nowrap><label style="font:bold 18px arial" class="blkdata">D.L. Number:&nbsp;</label></td>
<td align="left" ><INPUT align=right TYPE=text SIZE=10 NAME="strChkDL" style="font:bold 28px arial" value="<%=strChkDL%>"></td>
</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>

   <tr>
      <td align="center" colspan=2>
	  <button name="btnCancel" class="button" style="height:35;width:100;font:bold 18px arial" OnClick="CancelChg()">Cancel</button>&nbsp;&nbsp;
	  <button name="btnSave" class="button" style="height:35;width:100;font:bold 18px arial" OnClick="SaveChg()">Save</button>
		</td>
	</tr>		
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
</table>
		<tr>
		    <td align="center" class="control" style="font:bold 18px arial" nowrap>Cash Back</td>
		</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
	</tr>
		<table  style="width:180" border="2" cellspacing="2" cellpadding="2">
		<tr>
		     <td ALIGN="center"><button style="height:35;width:50;font:bold 18px arial" onclick="button('3.00')"  id=button3d name=button12>$3</Button>
		     <td ALIGN="center"><button style="height:35;width:50;font:bold 18px arial" onclick="button('5.00')" id=button5d name=button0>$5</Button>
		     <td ALIGN="center"><button style="height:35;width:50;font:bold 18px arial" onclick="button('10.00')" id=button10d name=button13>$10</Button>
		</tr>
		<tr>
		     <td ALIGN="center"><button style="height:35;width:50;font:bold 18px arial" onclick="button('15.00')"  id=button15d name=button12>$15</Button>
		     <td ALIGN="center"><button style="height:35;width:50;font:bold 18px arial" onclick="button('20.00')" id=button20d name=button0>$20</Button>
		     <td ALIGN="center"><button style="height:35;width:50;font:bold 18px arial" onclick="button('25.00')" id=button25d name=button13>$25</Button>
		</tr>
		<tr>
		     <td ALIGN="center"><button style="height:35;width:50;font:bold 18px arial" onclick="button('30.00')"  id=button30d name=button12>$30</Button>
		     <td ALIGN="center"><button style="height:35;width:50;font:bold 18px arial" onclick="button('40.00')" id=button40d name=button0>$40</Button>
		     <td ALIGN="center"><button style="height:35;width:50;font:bold 18px arial" onclick="button('50.00')" id=button50d name=button13>$50</Button>
		</tr>
		<tr>
		     <td ALIGN="center" colspan=3><button style="height:35;width:150;font:bold 18px arial" onclick="button('-')" id=button1 name=button1> Reset </Button>
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


Sub SaveChg()
	if LEN(TRIM(document.all("strChkNo").value)) = 0 THEN
		msgbox "You must enter the Check Number!"
		exit sub
	end if
	if LEN(TRIM(document.all("strChkPhone").value)) = 0 THEN
		msgbox "You must enter the Phone Number!"
		exit sub
	end if
	window.returnValue = document.all("intCheckAmt").value+"|"+document.all("strChkNo").value +"|"+ document.all("strChkPhone").value+"|"+document.all("strChkDL").value
	parent.window.close
End Sub


Sub CancelChg()
	parent.window.close
End Sub

Sub button(Val)
	IF instr(1,val,"-")>0 then
		document.all("intCheckAmt").value = round(cdbl(document.all("hdnCheckAmt").value),2)
	ELSE
		document.all("intCheckAmt").value = round(cdbl(document.all("intCheckAmt").value) + cdbl(Val),2)
	END IF
End Sub

</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
