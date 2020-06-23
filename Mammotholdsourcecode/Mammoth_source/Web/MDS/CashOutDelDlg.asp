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
	Dim dbMain,strSQL,rsData,intGiftCardID,intRecID
	Set dbMain =  OpenConnection
	IF LEN(ltrim(Request("intRecItemID")))>0 then
		strSQL="SELECT GiftCardID,RecID"&_
		" FROM RecItem (nolock) "&_
		" WHERE RecItemID ='" & Request("intRecItemID") &"'"
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			If Not rsData.EOF Then
				intGiftCardID = rsData("GiftCardID")
				intRecID = rsData("RecID")
				strSQL= " Delete GiftCard Where GiftCardID = '" & intGiftCardID &"' and RecID=" &intRecID
				IF NOT DBExec(dbMain, strSQL) then
					Response.Write gstrMsg
					Response.End
				END IF
				strSQL= " Delete GiftCardHist Where GiftCardID = '" & intGiftCardID &"' and RecID=" &intRecID & " and LocationID=" & Request("LocationID")
				IF NOT DBExec(dbMain, strSQL) then
					Response.Write gstrMsg
					Response.End
				END IF
			End If
		END IF
		strSQL= " Delete RecItem Where RecItemID = " &  Request("intRecItemID") & " and LocationID=" & Request("LocationID")
		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF

	END IF
'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css" />
</head>
<body class="pgbody">
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
	Window.close
End Sub

</script>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
