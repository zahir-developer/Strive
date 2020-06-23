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
Dim Title,intrecID,hdnStatus,strList,LocationID,LoginID

'********************************************************************
' Main
'********************************************************************
if len(request("intrecID"))>0 then
	intrecID = request("intrecID")
ELSE
	intrecID = 0 
END IF
if len(request("strList"))>0 then
	strList = request("strList")
ELSE
	strList = "N/A" 
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
    <title></title>
</head>
<body class="pgbody">
    <input type="hidden" name="intRECID" value="<%=intRECID%>" />
    <input type="hidden" name="strList" value="<%=strList%>" />
    <input type="hidden" name="hdnStatus" value="<%=hdnStatus%>" />
    <input type="hidden" name="LocationID" value="<%=LocationID%>" />
    <input type="hidden" name="LoginID" value="<%=LoginID%>" />
    <div style="text-align: center">
        <iframe name="fraMain" src="NewDetailByDlgFra.asp?intrecID=<%=intrecID%>&strList=<%=strList%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>" style="height: 250px; width: 400px" frameborder="0"></iframe>
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
	IF hdnStatus.value then
		window.returnValue = true 
	ELSE
		window.returnValue = False 
	END IF
End Sub
</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************

%>
