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
<!--#include file="incDatabase.asp"-->
<%
'********************************************************************
' Global Variables
'********************************************************************
Dim Title,intdata,hdnStatus,LocationID,LoginID

'********************************************************************
' Main
'********************************************************************
if len(request("intdata"))>0 then
	intdata = request("intdata")
ELSE
	intdata = "0|0|Unk|0|0"
END IF
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
    <title>Client Add/Edit </title>
</head>
<body class="pgbody">
    <input type="hidden" name="intdata" value="<%=intdata%>" />
    <input type="hidden" name="hdnStatus" value="<%=hdnStatus%>" />
    <input type="hidden" name="LocationID" value="<%=LocationID%>" />
    <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
    <div style="text-align:center">
        <iframe name="fraMain" src="NewClientDlgFra.asp?intdata=<%=intdata%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>" style="height:300px; width:570px"></iframe>
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
