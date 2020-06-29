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
Main
Sub Main
	Dim dbMain, intCustAccID,FormAction,LocationID,LoginID
	Set dbMain =  OpenConnection

	FormAction =  Request("FormAction")
	intCustAccID = Request("intCustAccID")
    LocationID = request("LocationID")
    LoginID = request("LoginID")
Select Case Request("FormAction")
	Case "btnInvoice"
		Call InvoiceData(dbMain,intCustAccID)
End Select
'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Client Monthly Statement</title>
</head>
<body class="pgbody">
<form method=post name="frmMain" action="ClientMonthlyDlg.asp?intCustAccID=<%=intCustAccID%>">
<input type="hidden" name="FormAction" tabindex="-2" value="<%=FormAction%>">
<input type="hidden" name="intCustAccID" tabindex="-2" value="<%=intCustAccID%>">
<input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<div style="text-align:center">

<table  border="0" width="600" cellspacing="0" cellpadding="0">
<iframe align="center" Name="fraMonthly" src="ClientMonthlyDlgFra.asp?intCustAccID=<%=intCustAccID%>"   height="250" width="600" frameborder="0"></iframe>
</table>
<br>
<table cellspacing=0 cellpadding=0 width=600>
	<tr>
		<td align=center>			
			<button  name="btnInvoice" class="button"  align="right" style="width:75" OnClick="Invoice()">Generate</button>&nbsp;&nbsp;&nbsp;&nbsp;
			<button  name="btnDone" class="button"  align="right" style="width:75" OnClick="done()">Done</button>
		</td>
	</tr>
</table>
</div>
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
	If frmMain.FormAction.value="btnInvoice" then
		window.close
	END IF
End Sub

Sub Done()
	window.close
End Sub

Sub Invoice()
	dim strMon
	strMon = ShowModalDialog("ClientInvoiceDlg.asp?intCustAccID=" & document.all("intCustAccID").value  &"&LocationID="& document.all("LocationID").value&"&LoginID="& document.all("LoginID").value ,"","dialogwidth:650px;dialogheight:370px;")
	window.close
End Sub


</script>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
