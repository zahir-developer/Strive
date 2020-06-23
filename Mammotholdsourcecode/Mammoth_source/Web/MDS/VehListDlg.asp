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
Main
Sub Main
	Dim dbMain, strDummy
	Set dbMain =  OpenConnection
'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Vehical Filter</title>
</head>
<body class="pgbody">
<form method=post name="frmMain">
<br>
<div style="text-align:center">
<table class="tblcaption" cellspacing=0 cellpadding=2 width=400>
	<tr>
		<td align=Left class="tdcaption" background="images/header.jpg" width=100>Filter By</td>
		<td align=right>&nbsp;</td>
	</tr>
</table>
<table border="0" width="450" cellspacing="0" cellpadding="4">
  	<tr>
		<td align="right"><label class=control>Client Last Name:</label></td>
		<td align="left">
			<input type=text tabindex=1 name="txtlname" size=20
			SQLBefore=" AND Client.lName LIKE '%" SQLAfter="%'"> 
		</td>
	</tr>
  	<tr>
		<td align="right"><label class=control>Bar Code:</label></td>
		<td align="left">
			<input type=text tabindex=2 name="txtUPc" size=20
			SQLBefore=" AND vehical.UPC = '" SQLAfter="'"> 
		</td>
	</tr>
  	<tr>
		<td align="right"><label class=control>Tag:</label></td>
		<td align="left">
			<input type=text tabindex=3 name="txtTag" size=20
			SQLBefore=" AND vehical.Tag LIKE '%" SQLAfter="%'"> 
		</td>
	</tr>
	<tr>
		<td align="right"><label class=control>Make:</label></td>
		<td align="left">
			<select tabindex=4 name="cboType"
			SQLBefore=" AND  vehical.Make = " SQLAfter="">
				<Option Value="" selected></option>
				<% Call LoadList(dbMain, 3, strDummy) %>				
			</select>
		</td>
	</tr>
	<tr>
		<td align="right"><label class=control>Model:</label></td>
		<td align="left">
			<select tabindex=5 name="cboModel"
			SQLBefore=" AND  vehical.Model = " SQLAfter="">
				<Option Value="" selected></option>
				<% Call LoadList(dbMain, 4, strDummy) %>				
			</select>
		</td>
	</tr>
	<tr>
		<td align="right"><label class=control>Color:</label></td>
		<td align="left">
			<select tabindex=6 name="cboColor"
			SQLBefore=" AND  vehical.Color = " SQLAfter="">
				<Option Value="" selected></option>
				<% Call LoadList(dbMain, 5, strDummy) %>				
			</select>
		</td>
	</tr>
</table>
<br>
<table class="tblcaption" cellspacing=0 cellpadding=2 width=400>
	<tr>
		<td align=Left class="tdcaption" background="images/header.jpg" width=100>Order By</td>
		<td align=right>&nbsp;</td>
	</tr>
</table>
<table border="0">
    <tr>
		<td>
			<select tabindex=13 name="cboOrderBy" size="1">
				<Option Value="lName">Last&nbsp;&nbsp;Name</option>
				<Option Value="UPC">Bar Code</option>
				<Option Value="Tag">Tag</option>
				<Option Value="Make">Make</option>
				<Option Value="Model">Model</option>
				<Option Value="Color">Color</option>
			</select>
		</td>
		<td><input type="radio" name="rbOrderBy" id="rbOrderBy0" tabindex=14 checked>
		<label class="control" for="rbOrderby0">Ascending</label>
		<input type="radio" name="rbOrderBy" id="rbOrderBy1" tabindex=14>
		<label class="control" for="rbOrderby1">Descending</label>
		</td>
 	</tr>
</table>
<br>
<table>
   <tr>
      <td align="center" colspan="3">
	  <button name="btnOK" class="button" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" style="width:75">OK</button>
	  <button name="btnCancel" class="button" tabindex=16 onmouseover="ButtonHigh()" onmouseout="ButtonLow()" style="width:75">Cancel</button>
    </td>
	</tr>		
</table>
</div>
</form>
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
End Sub

Sub btnCancel_OnClick()
	Window.close
End Sub

Sub btnOK_OnClick()
	Dim objElement, strResult
	
	If Not ValDataType Then
		Exit Sub
	End If	
	
	For Each objElement in document.all
		If UCase(objElement.Tagname)="INPUT" Or UCase(objElement.Tagname)="SELECT" Then
			If Not IsNull(objElement.GetAttribute("sqlBefore")) Then
				If Len(Trim(objElement.Value))>0 Then
						strResult = strResult & objElement.sqlBefore & SQLReplace(objElement.Value)
					If Not IsNull(objElement.GetAttribute("sqlAfter")) Then
						strResult = strResult & objElement.sqlAfter
					End If
				End If
			End If
		End If
	Next	
	IF Len(frmMain.cboOrderBy.value)>0 then
		If frmMain.rbOrderBy0.checked Then
			strResult = strResult & " ORDER BY " & frmMain.cboOrderBy.value
		Else
			strResult = strResult & " ORDER BY " & frmMain.cboOrderBy.value & " DESC"
		End If	
	END IF					
	window.returnvalue = strResult
	Window.close
End Sub

Function SQLReplace(SQLField)
	Dim strText
	strText=SQLField
	strText=Replace(strText, "'", "''")
	strText=Replace(strText, """", """""")
	strText=Trim(strText)
	SQLReplace=strText
End Function
</script>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
