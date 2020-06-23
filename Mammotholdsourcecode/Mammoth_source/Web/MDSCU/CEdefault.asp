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

Dim dbMain, blnLoginFailed
Set dbMain =  OpenConnection

'Call CheckBrowser()

IF Len(Request("loginid"))>0 and Len(Request("Password"))>0 then
	Call DoLogin(dbMain,blnLoginFailed)
End if

'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<title>Mammoth</title>
<meta name="viewport" content="width=device-width" /> 
<link rel="stylesheet" href="main.css" type="text/css" />
</head>
<body class="pgBody">
<form action="CEDefault.asp"method="post" id="frmMain">
<input type="hidden" name="blnLoginFailed" value="<%=blnLoginFailed%>" />
<center>
<table  style="width:100;height:300" cellspacing="0" cellpadding="0">
	<tr>
	    <td colspan="2" align="right"><label class="control">Check Out</label></td>
	</tr>
	<tr>
		<td align="right">&nbsp;</td>
		<td align="left">&nbsp;</td>
	</tr>
	<tr>
	    <td align="right"><label class="control">Login ID:</label></td>
		<td align="left"><input type="text" name="loginid" /></td>
	</tr>
	<tr>
		<td align="right">&nbsp;</td>
		<td align="left">&nbsp;</td>
	</tr>
	<tr>
		<td align="right"><label class="control">Password:</label></td>
		<td align="left"><input type="password" name="password" /></td>
	</tr>
	<tr>
		<td align="right">&nbsp;</td>
		<td align="left">&nbsp;</td>
	</tr>
	<tr>
		<td align="right">&nbsp;</td>
		<td align="left">&nbsp;</td>
	</tr>
	<tr>
	    <td  colspan="2" align="center">
			<input type="submit" name="loginbutton" value="Login" />
		</td>
		<td align="left">&nbsp;</td>
	 </tr>
	<tr>
		<td align="right">&nbsp;</td>
		<td align="left">&nbsp;</td>
	</tr>
	<tr>
		<td colspan="2" align="center" valign="middle">
			<img id="piclogo" src="images/MammothLogo.gif" alt="images/MammothLogo.gif" width="100" height="100" />
		</td>
	</tr>
</table>
</center>
</form>
</body>
</html>

<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/vbscript" language="VBSCRIPT">
Option Explicit
sub window_onload()
	frmMain.loginid.focus
END SUB
</script>
<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************

sub checkbrowser()
	'If Instr(Request.ServerVariables("HTTP_User_Agent"),"Windows CE")= 0 then
	'	Response.Clear
	'	Response.Redirect "Default.asp"
	'End If
End Sub

Sub DoLogin(db,bLF)
Dim strSQL, RS
	strSQL = "SELECT UserID,Password,Active, LastName, FirstName, RoleID,Initials FROM dbo.LM_Users WHERE (UPPER(LoginID) = '" & UCASE(Request("LoginID")) & "') AND (LocationID = "& Application("LocationID") &")"

	If dbOpenStaticRecordset(db,rs,strSQL) Then
		If Not RS.EOF Then
			If  Trim(RS("Password")) = Trim(Request("Password")) Then
				If Clong(rs("Active")) <> 0 Then
						bLF=0
						Session("UserID") = Clong(RS("UserID"))
						Session("UserName") = Trim(rs("FirstName")) + " " + Trim(rs("LastName"))
						Session("Initials") = Trim(rs("Initials")) 
						Session("RoleID") = Clong(RS("RoleID"))
				Else
					bLF = 3
				End If
			Else
				bLF  = 1
			End If
		Else
			blF = 1
		End If
	End If

    If bLF = 0 then
		Response.Redirect "CEmenu1.asp"
	End if
End Sub

%>

