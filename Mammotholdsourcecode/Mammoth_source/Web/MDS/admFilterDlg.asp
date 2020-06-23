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
Dim Title
'********************************************************************
' Main
'********************************************************************
Main

'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
Sub Main

Dim dbMain, strFilterType
Set dbMain =  OpenConnection

strFilterType = Request("Type")
Title="Filter" & strFilterType

%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="post" Name="frmMain" >
<div style="text-align:center">
<table class="tblcaption" cellspacing="0" cellpadding="2" width=450>
	<tr>
		<td align=Left class="tdcaption" background="images/header.jpg" width=100>Filter By</td>
		<td align=right>&nbsp;</td>
	</tr>
</table>
<table border="0" width="350" cellspacing="0" cellpadding="4">
    <tr>
		<td align="right" nowrap><label class=control for="dboJobLocationID">First Name:</label></td>
		<td align="Left" nowrap>
			<input type="text" tabindex="1" sqlBefore="AND FirstName Like '%" sqlAfter="%'" name="txtFirstName" size=20  title="First Name" >
		</td>
		<td align="right" nowrap ></td>
		<td align="Left" nowrap ></td>
	</tr>
	<tr>
	<td align="right" nowrap><label class=control for="dboJobLocationID">Last Name:</label></td>
		<td align="Left" nowrap>
			<input type="text" tabindex="1" sqlBefore="AND LastName Like '%" sqlAfter="%'" name="txtLastName" size=20  title="Last Name" >
		</td>
			<td align="right" nowrap ></td>
		<td align="Left" nowrap ></td>
	</tr>
	<tr>
		<td align="right" nowrap><label class=control for="dboJobLocationID">Login:</label></td>
		<td align="Left" nowrap>
			<input type="text" tabindex="1" sqlBefore="AND LoginID Like '%" sqlAfter="%'" name="txtLogin" size=20  title="Login ID" >
		</td>
		<td align="right" nowrap ></td>
		<td align="Left" nowrap ></td>
	</tr>
	<tr>
		<td align="right" nowrap><label class=control for="dboJobLocationID">Status:</label></td>
		<td align="left" nowrap ><input  type="radio"  ID="Active" name="rbActive"><label class=control for="Active">Active</label>
			<input  type="radio" ID="Inactive"  name="rbActive"><label class=control for="Inactive">Inactive</label>
		</td>
		<td align="right" nowrap ></td>
		<td align="Left" nowrap ></td>
	</tr>
</table>
<br>
<table class="tblcaption" cellspacing=0 cellpadding=2 width=450>
	<tr>
		<td align=Left class="tdcaption" background="images/header.jpg" width=100>Order By</td>
		<td align=right>&nbsp;</td>
	</tr>
</table>
<table  cellspacing=0 cellpadding=2 width=450>
    <tr>
		<td align=right><select tabindex=7 name="cboOrderBy" size="1">
		<option value=" FirstName">First Name</option>
		<option value=" LastName">Last Name</option>
		<option value=" LoginID">Login ID</option>
		<option value=" Active">Staus</option>
		</select></td>
		<td><input type="radio" name="rbOrderBy" tabindex=8 id=optSort0 value="" checked>
		<label class="control" for=optSort0 >Ascending</label>
		<input type="radio" name="rbOrderBy" tabindex=9 id=optSort1 value="DESC">
		<label class="control" for=optSort1>Descending</label>
		</td>
 	</tr>
</table>
<br>
<table cellspacing=0 cellpadding=2 width=450>
    <tr>
		  <td align="center" colspan="3">
		  <button name="btnOK" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" style="width:75">OK</button>&nbsp;
		  <button name="btnCancel" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" style="width:75">Cancel</button>
		</td>
	</tr>		
</table>
</form>
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

Sub Window_OnLoad
End Sub

Sub btnCancel_OnClick()
	window.ReturnValue = "<%=Replace(Request("FilterBy"),"""","""""")%>"
	Window.close
End Sub

Sub btnOK_OnClick()
Dim Result,objElement
If valDataType() Then
Result = ""
For Each objElement in document.all
	If UCase(ObjElement.TagName) = "INPUT" Or UCase(ObjElement.TagName) = "SELECT" Then
		If Not(IsNull(ObjElement.GetAttribute("sqlBefore"))) Then
			If Len(Trim(objElement.Value)) > 0 Then
				Result = Result & " " & ObjElement.sqlBefore & ClientReplace(CStr(ObjElement.Value))
				If Not(IsNull(ObjElement.GetAttribute("sqlAfter"))) Then
					Result = Result & ObjElement.sqlAfter
				End If
			End If
		End If
	End If
Next
	If frmMain.rbActive(0).checked Then
		Result = Result & " AND Active=1 "
	Elseif  frmMain.rbActive(1).checked Then
		Result = Result & " AND Active=0 "
	End IF
	If frmMain.rbOrderby(0).checked Then
		Result = Result & " Order BY  " & frmMain.cboOrderBy.Value
	Else
		Result = Result & " Order BY  " & frmMain.cboOrderBy.Value & " DESC "
	End IF
	window.returnvalue = Result
	window.close
End If
End Sub
</script>

<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
