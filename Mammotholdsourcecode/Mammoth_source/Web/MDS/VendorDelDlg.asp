<%@ LANGUAGE="VBSCRIPT" %>
<%
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
Main
Sub Main
	Dim dbMain,strSQL,strMessage,rs
	Set dbMain =  OpenConnection
	IF Request("intVenID")>0 then

	strSQL =" SELECT VenID FROM INV (nolock) where VenID="&Request("intVenID")
		 
	   IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
			IF NOT rs.EOF then
				strMessage = "Vendor is in-use and can not be deleted"		
			ELSE
				strSQL= " Delete Vendor Where VenID = " &  Request("intVenID")
				IF NOT DBExec(dbMain, strSQL) then
					Response.Write gstrMsg
					Response.End
				END IF
			END IF
		END IF
	ELSE
		strMessage = "Vendor Delete Failed"		
	END IF
'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
</head>
<body class="pgbody">
<input type="hidden" name="strMessage" tabindex="-2" value="<%=strMessage%>">
</body>
</html>
<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">
Option Explicit

Sub Window_Onload()
	IF LEN(trim(document.all("strMessage").value))>0 then
		msgbox document.all("strMessage").value
	END IF
	Window.close
End Sub

</script>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
