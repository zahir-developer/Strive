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
Dim Title,intUserID,LocationID,LoginID

'********************************************************************
' Main
'********************************************************************
intUserID = request("intUserID")
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
    <title>Add Items</title>
</head>
<body class="pgbody">
    <input type="hidden" name="intUserID" value="<%=intUserID%>" />
    <input type="hidden" name="LocationID" value="<%=LocationID%>" />
    <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
    <div style="text-align: center">
        <iframe name="fraMain" src="admNewUnifFra.asp?intUserID=<%=intUserID%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>" style="height: 200px; width: 440px" frameborder="0"></iframe>
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
