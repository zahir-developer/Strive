<%@ LANGUAGE="VBSCRIPT" %>
<%
'********************************************************************
' Name: 
'********************************************************************
Option Explicit
Response.Expires = 0
Response.Buffer = True

'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incDatabase.asp"-->
<%
'********************************************************************
' Global Variables
'********************************************************************
Dim Title,intChargeAmt,hdnStatus

'********************************************************************
' Main
'********************************************************************
intChargeAmt = request("intChargeAmt")


'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Select Credit Card</title>
</head>
<body class="pgbody">
<input type="hidden" name="intChargeAmt" value="<%=intChargeAmt%>" />
<div style="text-align:center">
<iframe  Name="fraMain" src="CashOutCardDlgFra.asp?intChargeAmt=<%=intChargeAmt%>"  height="550" width="320" ></iframe>
</div>
</body>
</html>
<%

'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/VBSCRIPT">
Option Explicit

Sub Window_onUnload()
End Sub
</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************

%>
