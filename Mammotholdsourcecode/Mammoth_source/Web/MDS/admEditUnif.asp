<%@  language="VBSCRIPT" %>
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
<%
'********************************************************************
' Global Variables
'********************************************************************
Dim Title,intUserID,intUnifID,LocationID,LoginID

'********************************************************************
' Main
'********************************************************************
intUserID = request("intUserID")
intUnifID = request("intUnifID")
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
    <link rel="stylesheet" href="main.css" type="text/css" />
    <title>New Unifision</title>
</head>
<body class="pgbody">
    <input type="hidden" name="intUserID" value="<%=intUserID%>" />
    <input type="hidden" name="intUnifID" value="<%=intUnifID%>" />
    <input type="hidden" name="LocationID" value="<%=LocationID%>" />
    <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
    <div style="text-align: center">
        <iframe align="center" name="fraMain" src="admEditUnifFra.asp?intUserID=<%=intUserID%>&intUnifID=<%=intUnifID%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>" style="height: 410px; width: 430px" frameborder="0"></iframe>
    </div>
</body>
</html>
<%

'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
