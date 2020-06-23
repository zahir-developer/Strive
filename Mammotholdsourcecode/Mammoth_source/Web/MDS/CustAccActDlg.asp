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
Dim Title,intCustAccID,strCurrentAmt,intClientID,LocationID,LoginID

'********************************************************************
' Main
'********************************************************************
intClientID=Request("intClientID")
intCustAccID=Request("intCustAccID")
strCurrentAmt=Request("strCurrentAmt")
    LocationID = request("LocationID")
    LoginID = request("LoginID")

'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Account Activity</title>
</head>
<body class="pgbody">
<input type="hidden" name="intCustAccID" tabindex="-2" value="<%=intCustAccID%>">
<input type="hidden" name="strCurrentAmt" tabindex="-2" value="<%=strCurrentAmt%>">
<input type="hidden" name="intClientID" tabindex="-2" value="<%=intClientID%>">
<input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<div style="text-align:center">
<iframe Name="fraMain" src="CustAccActDlgFra.asp?intCustAccID=<%=intCustAccID%>&strCurrentAmt=<%=strCurrentAmt%>&intClientID=<%=intClientID%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>"   height="320" width="320" frameborder="0"></iframe>
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
