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
Dim Title,intUserID,hdnSheetID,strGTotal,LocationID,LoginID

'********************************************************************
' Main
'********************************************************************
intUserID = request("intUserID")
hdnSheetID = request("hdnSheetID")
strGTotal = request("strGTotal")
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
    <link rel="stylesheet" href="main.css" type="text/css">
    <title></title>
</head>
<body class="pgbody">
    <div style="text-align: center;">
        <input type="hidden" name="intUserID" value="<%=intUserID%>">
        <input type="hidden" name="hdnSheetID" value="<%=hdnSheetID%>">
        <input type="hidden" name="strGTotal" value="<%=strGTotal%>">
        <input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
        <iframe name="fraMain" src="admTimeAddCollFra.asp?intUserID=<%=intUserID%>&hdnSheetID=<%=hdnSheetID%>&strGTotal=<%=strGTotal%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>" style="height: 210px; width: 430px;" frameborder="0"></iframe>

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
