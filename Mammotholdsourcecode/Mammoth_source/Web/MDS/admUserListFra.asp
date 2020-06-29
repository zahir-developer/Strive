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

<!--#include file="incNavTable.asp"-->
<!--#include file="incDatabase.asp"-->
<!--#include file="incCommon.asp"-->
<%
'********************************************************************
' Global Variables
'********************************************************************
'********************************************************************
' Main
'********************************************************************
Call Main

'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
Sub Main

'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<link rel="stylesheet" href="main.css" type="text/css" />
<title></title>
</head>
<body class=pgframe>
<table style="width: 752px; border-collapse: collapse;" class=data>
	<tr>
		<td style="width: 100px" class=header>Login</td>
		<td style="width: 300px" class=header>User Name</td>
		<td style="width: 100px" class=header>Status</td>
	</tr>
	<%=DoDataRow()%>
			
</table>

</body>
</html>

<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">

Option Explicit

Sub Window_OnLoad()
	
End Sub

</script>
<%
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
    Dim htmlDataRow
	Dim dbMain,strSQL,htmlTable,rsData,LocationID,LoginID
	Set dbMain = OpenConnection

    LocationID = request("LocationID")
    LoginID = request("LoginID")


	strSQL ="SELECT UserID, LoginID, LastName, FirstName, Active FROM LM_Users(nolock) where UserID > 0 and LocationID="& LocationID
	
	Response.write(strSQL)
	
	
	If Len(Trim(Request("FilterBy"))) > 0 Then
			strSQL = strSQL + Request("FilterBy")
	Else
			strSQL = strSQL + " and Active=1 Order By LastName,FirstName "
	End If
	If dbOpenStaticRecordset(dbMain,rsData,strSQL)  Then
		IF NOT 	rsData.EOF then
			Do while Not rsData.EOF
	            htmlDataRow = htmlDataRow & "<tr><td class=""data""><a target=body href=""admUserEdit.asp?UserID=" & rsdata("UserID") & "&LocationID="& LocationID &"&LoginID="& LoginID & """>" &  NullTest(rsdata("LoginID")) & "</a></td>"
	            htmlDataRow = htmlDataRow & "<td class=""data""><a target=body href=""admUserEdit.asp?UserID=" & rsdata("UserID") & "&LocationID="& LocationID &"&LoginID="& LoginID & """>"  & NullTest(rsData("LastName")) & ", " & NullTest(rsData("FirstName")) & "</a></td>"
	            htmlDataRow = htmlDataRow & "<td class=""data"" style=""text-align:left;"">" &   ActionLookup(CBool(rsData("Active"))) & "</td></tr>"
				rsData.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=3 align=""center"" Class=""data"">No Users.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=3 align=""center"" Class=""data"">No Users.</td></tr>"
	END IF
	DoDataRow = htmlDataRow
	Set rsData = Nothing
	Call CloseConnection(dbMain)

End Function
Function NullTest(var)
	If IsNull(var) then
		NullTest = "&nbsp;"
	Else
		If Trim(var) = "" Then
			NullTest ="&nbsp;"
		Else
			NullTest = var
		End If
	End If
End Function

%>
