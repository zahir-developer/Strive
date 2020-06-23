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

Dim dbMain, strLoc,hdnFilterBy
Set dbMain =  OpenConnection
hdnFilterBy = Request("hdnFilterBy")

'********************************************************************
' HTML
'********************************************************************
%>

<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<title><%=gStrTitle%></title>
</head>
<body class=pgbody Onclick="SetDirty" onkeyup="SetDirty()">
<form method="POST" name="frmMain" action="VendorList.asp"> 
<input type=hidden name=FormAction value=>
<input type=hidden name=hdnFilterBy value="<%=hdnFilterBy%>">
<input type=hidden name=strLoc value="<%=strLoc%>">
<div align=center>
<table height="10" cellspacing="0" cellpadding="0" border="0" width=768>
	<tr>
		<td align=center ></td>
		<td align=right></td>
	</tr>
</table>
<table class="tblcaption" cellspacing=0 cellpadding=0 width=768>
	<tr>
		<td align=center class="tdcaption" background="images/header.jpg" width=210>Vendor List</td>
		<td align=right>			
			<button  name="btnNew" align="right" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" type="submit" style="width:75;display:none" OnClick="SubmitForm()">New</button>
			<button  name="btnFilter" align="right" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" type="submit" style="width:75;display:none" OnClick="SubmitForm()">Filter</button>
		</td>
	</tr>
</table>

<iframe Name="fraMain" src="admLoading.asp" scrolling=yes height="275" width="768" frameborder="0"></iframe>
<table width=768>
	<tr>
		<% IF LEN(hdnFilterBy)=0 then %>
		<td align="left" class="control" nowrap>No Filter Set</td>
		<% ELSE %>
		<td align="left" class="control" nowrap>Filter is Set </td>
		<% END IF %>
	</tr>
</table>

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
Sub Window_Onload()
		fraMain.location.href = "VendorListFra.asp?hdnFilterBy=<%=Request("hdnFilterBy")%>"
End Sub

Sub SubmitForm()
 window.event.CancelBubble=True
 window.event.ReturnValue=False

	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If

Select Case window.event.srcElement.name
	Case "btnNew"
		Dim retVenID,strVenArr
		retVenID= ShowModalDialog("NewVendorDlg.asp" ,"","dialogwidth:600px;dialogheight:350px;")
		IF LEN(trim(retVenID)) > 0 then
			frmMain.submit()
		END IF
	
	Case "btnFilter"

		Dim strFilt
		strFilt = ShowModalDialog("VendorListDlg.asp",,"center:1;dialogwidth:550px;dialogheight:450px;")
		If Len(strFilt) = 0 Then 
			window.event.ReturnValue = False
		Else
		'msgbox strFilt
			fraMain.location.href = "VendorListFra.asp?hdnFilterBy="& ClientEncode(strFilt)
			window.event.ReturnValue = False
		End If

	End Select
End Sub

</script>
<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************

%>
