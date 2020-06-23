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
Dim Title,intUserID,intCollID,LocationID,LoginID

'********************************************************************
' Main
'********************************************************************
intUserID = request("intUserID")
intCollID = request("intCollID")
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
<Title>New Collision</title>
</head>
<body class="pgbody">
<input type="hidden" name="intUserID" value="<%=intUserID%>">
<input type="hidden" name="intCollID" value="<%=intCollID%>">
<input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />

<div  style="text-align: center;">
<iframe align="center" Name="fraMain" src="admEditCollFra.asp?intUserID=<%=intUserID%>&intCollID=<%=intCollID%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>"  height="410" width="430" frameborder="0"></iframe>
</div>
</body>
</html>
<%

'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">
Option Explicit

Sub Window_onUnload()
End Sub
</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
