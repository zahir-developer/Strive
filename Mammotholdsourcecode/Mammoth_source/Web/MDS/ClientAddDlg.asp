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
Dim Title,strUPC,hdnStatus,LocationID,LoginID

'********************************************************************
' Main
'********************************************************************
if len(request("strUPC"))>0 then
	strUPC = request("strUPC")
ELSE
	strUPC = ""
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
    <link rel="stylesheet" href="main.css" type="text/css">
    <title>Bar Code Client Data</title>
</head>
<body class="pgbody">
    <input type="hidden" name="strUPC" value="<%=strUPC%>">
    <input type="hidden" name="hdnStatus" value="<%=hdnStatus%>">
    <input type="hidden" name="LocationID" value="<%=LocationID%>" />
    <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
    <div style="text-align: center">
            <iframe align="center" name="fraMain" src="ClientAddDlgFra.asp?strUPC=<%=strUPC%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>" height="300" width="570" frameborder="0"></iframe>
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
