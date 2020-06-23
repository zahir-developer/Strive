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

Dim dbMain, strLoc,hdnFilterBy,strUPC,LocationID,LoginID
Set dbMain =  OpenConnection
hdnFilterBy = Request("hdnFilterBy")
    LocationID = request("LocationID")
    LoginID = request("LoginID")

'********************************************************************
' HTML
'********************************************************************
%>

<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css" />
<title></title>
</head>
<body class=pgbody Onclick="SetDirty" onkeyup="SetDirty()">
<form method="POST" name="frmMain" action="ClientList.asp"> 
<input type=hidden name=FormAction value=>
<input type=hidden name=hdnFilterBy value="<%=hdnFilterBy%>">
<input type=hidden name=strLoc value="<%=strLoc%>">
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<div style="text-align: center">
<table height="10" cellspacing="0" cellpadding="0" border="0" width=768>
	<tr>
		<td align=center ></td>
		<td align=right></td>
	</tr>
</table>
<table class="tblcaption" cellspacing=0 cellpadding=0 width=768>
	<tr>
		<td align=center class="tdcaption" background="images/header.jpg" width=100>Client List</td>
		<td align=right>			
			<button align=Center class="button"  OnClick="SelClient()" style="width:120" id=button1 name=button1>Search</button>&nbsp;&nbsp;&nbsp;
			<button  name="btnNew" align="right" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" type="submit" style="width:75;display:none" OnClick="SubmitForm()">New</button>&nbsp;&nbsp;&nbsp;
			<button  name="btnFilter" align="right" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" type="submit" style="width:75;display:none" OnClick="SubmitForm()">Filter</button>
		</td>
	</tr>
</table>

<iframe Name="fraMain" src="blank.asp" scrolling=yes height="275" width="768" frameborder="0"></iframe>
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
<script Type="TEXT/VBSCRIPT">
Option Explicit
Sub Window_Onload()
		Dim strFilt,retClient
		'strFilt = ShowModalDialog("ClientListDlg.asp?LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value,,"center:1;dialogwidth:550px;dialogheight:450px;")
		'If Len(strFilt) = 0 Then 
		'	fraMain.location.href = "ClientListFra.asp?LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
		'	window.event.ReturnValue = False
		'Else
			fraMain.location.href = "ClientListFra.asp?hdnFilterBy="& ClientEncode(strFilt) &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
			window.event.ReturnValue = False
		'End If
	retClient= ShowModalDialog("SelClientDlg2.asp" ,"","dialogwidth:600px;dialogheight:350px;")
	IF retClient > 2 and len(trim(retClient))>0 then
		strFilt = " AND Client.ClientID = "&retClient
		fraMain.location.href = "ClientListFra.asp?hdnFilterBy="& ClientEncode(strFilt) &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
		window.event.ReturnValue = False
	elseIF retClient = -1 then
		location.href="ClientEdit.asp?hdnClientID=0&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
		window.event.ReturnValue = False
	else
		window.event.ReturnValue = False
    END IF
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
		location.href="ClientEdit.asp?hdnClientID=0&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	
	Case "btnFilter"

		Dim strFilt
		strFilt = ShowModalDialog("ClientListDlg.asp?LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value,,"center:1;dialogwidth:550px;dialogheight:450px;")
		If Len(strFilt) = 0 Then 
			window.event.ReturnValue = False
		Else
		'msgbox strFilt
			fraMain.location.href = "ClientListFra.asp?hdnFilterBy="& ClientEncode(strFilt) &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
			window.event.ReturnValue = False
		End If

	End Select
End Sub

Sub SelClient()
	Dim retClient,intdata,strFilt
	retClient= ShowModalDialog("SelClientDlg2.asp" ,"","dialogwidth:600px;dialogheight:350px;")
	IF retClient > 2 and len(trim(retClient))>0 then
		strFilt = " AND Client.ClientID = "&retClient
		fraMain.location.href = "ClientListFra.asp?hdnFilterBy="& ClientEncode(strFilt) &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
		window.event.ReturnValue = False
	elseIF retClient = -1 then
		location.href="ClientEdit.asp?hdnClientID=0&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
		window.event.ReturnValue = False
	else
		window.event.ReturnValue = False
    END IF
End Sub


</script>
<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************

%>
