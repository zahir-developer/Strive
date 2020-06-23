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
Dim Title,intrecID,dtDate,intLine,LocationID,LoginID

'********************************************************************
' Main
'********************************************************************
if len(request("intrecID"))>0 then
	intrecID = request("intrecID")
ELSE
	intrecID = 0 
END IF
LocationID = request("LocationID")
LoginID = request("LoginID")
	dtDate = now() 
	intLine = 1


'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css">
    <title>New Wash</title>
</head>

<body class="pgbody">
    <input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
    <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
    <input type="hidden" name="intRECID" value="<%=intRECID%>">
    <input type="hidden" name="dtDate" value="<%=dtDate%>" />
    <input type="hidden" name="FormAction" />

    <iframe name="fraMain" src="NewWashDlgFra.asp?intrecID=<%=intrecID%>&dtDate=<%=dtDate%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>" style="height:100%; width:100%" ></iframe>

</body>
</html>
<%

'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/vbscript" >
Option Explicit
Sub Window_onUnload()
	IF document.all("FormAction").value = "PayChg" then
		window.returnvalue = document.all("intrecID").value
	END IF

end sub
</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************

%>
