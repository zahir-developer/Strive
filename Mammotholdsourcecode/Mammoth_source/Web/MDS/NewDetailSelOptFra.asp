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

	Dim dbMain, intProdID,rsData,strSQL,blSaved

	Set dbMain =  OpenConnection
	intProdID = request("intProdID")


%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="NewDetailSelOptFra.asp">
<div align="center">
<input type="hidden" name="intProdID" value="<%=intProdID%>">
<input type="hidden" name="blSaved" value="<%=blSaved%>">
<table border="0" width="210" cellspacing="0" cellpadding="0">
	<tr>
	<td align=Center>			
		<select tabindex=2 name="cboProductID">
			<% Call LoadProduct(dbMain,intProdID) %>				
		</select>&nbsp;&nbsp;
		</td>
	</tr>
</table>
<br>
<table align="center" border="0" width="210" cellspacing="1" cellpadding="1">
   <tr>
      <td align="center" colspan="3">
	  <button name="btnSave" class="button" style="width:75" OnClick="SaveChg()">Save</button>&nbsp;&nbsp;&nbsp;&nbsp;
	  <button name="btnCancel" class="button" style="width:75" OnClick="CancelChg()">Cancel</button>
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
End Sub


Sub SaveChg()
	window.returnvalue = frmMain.cboProductID.value
	parent.window.close
End Sub


Sub CancelChg()
	window.returnvalue = ""
	parent.window.close
End Sub

</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************

Function LoadProduct(db,intProdID)
	Dim strSQL,RS
	strSQL= " SELECT ProdID, Descript FROM Product(nolock)"&_
			" WHERE prodID <> "& intProdID &" AND Prodid IN (SELECT productId FROM ProdOpt (NOLOCK)"&_
			" WHERE Optid = "& intProdID &")"&_
			" and Product.CAT = 22 order by Product.number"
		%>
		<option Value=""></option>
		<%
	If dbOpenRecordSet(db,rs,strSQL) Then
		Do While Not RS.EOF
						%>
						<option Value="<%=RS("ProdID")%>"><%=RS("Descript")%></option>
						<%
			RS.MoveNext
		Loop
	End If
End Function

%>
