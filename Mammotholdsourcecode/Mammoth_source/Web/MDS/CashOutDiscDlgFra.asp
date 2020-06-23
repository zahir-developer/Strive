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

	Dim dbMain 
	Dim strSQL, RS 

	Set dbMain =  OpenConnection
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="CashOutDiscDlgFra.asp">
<div style="text-align:center">
<table border="0" style="width:98%" >

<%
	strSQL="SELECT ProdID, Descript FROM Product WHERE cat = 4 and status=1 ORDER BY Number"

	If DBOpenRecordset(dbMain,rs,strSQL) Then
		If Not RS.EOF Then
			Do While Not  RS.EOF

%>

	<tr>
	     <td style="text-align:center"><button class="button" style="height:40px;width:90%;font:bold 18px arial" onclick="CardType('<%=rs("ProdID")%>')" ><%=rs("Descript")%></Button>
	</tr>

	<tr>
		<td style="text-align:right" class="control">&nbsp;</td>
	</tr>


<%	
				rs.MoveNext
			Loop
		END IF
	END IF
	Set RS = Nothing
%>
   <tr>
      <td style="text-align:center" >
	  <button name="btnCancel" class="button" style="height:40px;width:200px;font:bold 18px arial" OnClick="CancelChg()">Cancel</button>
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


Sub CardType(val)
	window.returnValue = val 
	parent.window.close
End Sub


Sub CancelChg()
	parent.window.close
End Sub

</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
