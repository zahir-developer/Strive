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
Dim Title,hdnStatus,strfname,strlname

'********************************************************************
' Main
'********************************************************************


'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Client Add/Edit </title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="SelClientDlg.asp">
<input type="hidden" name="hdnStatus" value="<%=hdnStatus%>">
<div style="text-align:center">
<table  border="0" width="577" cellspacing="0" cellpadding="0">
	<tr>
		<td align="right" class="control" nowrap>First Name:</td>
        <td align="left" class="control" nowrap><input maxlength="20" size="20" type=text tabindex=1 Onkeyup="NameChg()" name="strfname" value="<%=strfname%>">&nbsp; </td>
		<td align="right" class="control" nowrap>Last Name:</td>
        <td align="left" class="control" nowrap><input maxlength="20" size="20" type=text tabindex=1 Onkeyup="NameChg()" name="strlname" value="<%=strlname%>">&nbsp; </td>
	</tr>
</table>
<br>
    <table cellspacing="0" width="577" class="data">
      <tr>
        <td class="header" width=80 align="center" valign="bottom">First Name</td>
        <td class="header" width=100 align="center" valign="bottom">Last Name</td>
        <td class="header" width=150  align="center" valign="bottom">Address</td>
        <td class="header" width=110 align="center" valign="bottom">Phone</td>
      </tr>
    <table cellspacing="0" width="577" class="data">
<iframe align="center" Name="fraMain" src="selClientDlgFra.asp?strfname=<%=strfname%>&strlname=<%=strlname%>"  height="240" width="577" frameborder="0"></iframe>
</table>
</div>
</form>
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
	IF Len(trim(frmMain.hdnStatus.value))>0 then
		window.returnValue = cstr(frmMain.hdnStatus.value)
	ELSE
		window.returnValue = 0
	END IF
End Sub


Sub NameChg()
	fraMain.location.href = "selClientDlgFra.asp?strfname="& frmMain.strfname.value &"&strlname="& frmMain.strlname.value
End Sub

</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************

%>
