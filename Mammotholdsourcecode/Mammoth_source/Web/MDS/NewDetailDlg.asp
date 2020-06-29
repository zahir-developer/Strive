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
Dim Title,intrecID,dtDate,intLine,LocationID,LoginID

'********************************************************************
' Main
'********************************************************************
LocationID = request("LocationID")
LoginID = request("LoginID")


if len(request("intrecID"))>0 then
	intrecID = request("intrecID")
ELSE
	intrecID = 0 
END IF
if len(request("dtDate"))>0 then
	dtDate = request("dtDate")
ELSE
	dtDate = now() 
END IF
if len(request("intLine"))>0 then
	intLine = request("intLine")
ELSE
	intLine = 4
END IF


'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css"/>
<title>New Detail</title>
</head>
<body class="pgbody">
<div style="text-align:center">
<input type="hidden" name="LocationID" ID="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" ID="LoginID" value="<%=LoginID%>" />
<input type="hidden" name="intRECID" value="<%=intRECID%>" />
<input type="hidden" name="dtDate" value="<%=dtDate%>" />
<input type="hidden" name="intLine" value="<%=intLine%>" />
<input type="hidden" name="FormAction" />
<iframe Name="fraMain" src="NewDetailDlgFra.asp?intrecID=<%=intrecID%>&dtDate=<%=dtDate%>&intLine=<%=intLine%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>"  style="height:100%; width:100%" frameborder="0"></iframe>
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

Sub btnDone_OnClick()
Window.close
End Sub
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
