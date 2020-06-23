<%@ LANGUAGE="VBSCRIPT" %>
<%
'********************************************************************
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
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
Sub Main

Dim dbMain,intUserID,LocationID,LoginID
Set dbMain = OpenConnection
intUserID = Request("intUserID")
LocationID = request("LocationID")
LoginID = request("LoginID")

'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Add Document</title>
<body scroll="no" class="pgbody" Onclick="SetDirty" onkeyup="SetDirty()" topmargin=0> 
<input type="hidden" name="intUserID" tabindex="-2" value="<%=intUserID%>">
    <input type="hidden" name="LocationID" value="<%=LocationID%>" />
<center>
<div id="divDocumentsList" border="0">
	<iframe src="FileUpload.asp?hdnFolder=Docs\<%=cstr(LocationID)%>\<%=cstr(intUserID)%>" scrolling="no" height="75" width="260" frameborder="0"></iframe>
</div>
<button onclick="window.close">Close</button>
</center>
</body>
</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<%
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
%>

