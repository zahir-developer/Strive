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
<!--#include file="incSecurity.asp"-->
<%
Main
Sub Main
	Dim dbMain, strDummy,LocationID,LoginID
	Set dbMain =  OpenConnection

    LocationID = request("LocationID")
    LoginID = request("LoginID")


'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Client Filter</title>
</head>
<body class="pgbody">
<form method=post name="frmMain">
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
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
		<td align="right"><label class=control>Last Name:</label></td>
		<td align="left">
			<input type=text tabindex=1 name="txtlname" size=20
			SQLBefore=" AND Client.lName LIKE '%" SQLAfter="%'"> 
		<button align=Center class="button"  OnClick="SelClient()" style="width:120" id=button1 name=button1>Select</button>&nbsp;&nbsp;&nbsp;
		</td>
	</tr>
  	<tr>
		<td align="right"><label class=control>First Name:</label></td>
		<td align="left">
			<input type=text tabindex=2 name="txtfName" size=20
			SQLBefore=" AND  Client.fName LIKE '%" SQLAfter="%'"> 
		</td>
	</tr>
  	<tr>
		<td align="right"><label class=control>Phone:</label></td>
		<td align="left">
			<input type=text tabindex=3 name="txtPhone" size=20
			SQLBefore=" AND Client.Phone LIKE '%" SQLAfter="%'"> 
		</td>
	</tr>
	<tr>
		<td align="right"><label class=control>Type:</label></td>
		<td align="left">
			<select tabindex=4 name="cboType"
			SQLBefore=" AND  Client.Ctype = " SQLAfter="">
				<Option Value="" selected></option>
				<% Call LoadList(dbMain, 8, strDummy) %>				
			</select>
		</td>
	</tr>
	<tr>
			<td align="right"><label class=control>Email:</label></td>
		<td align="left">
		<input type=text tabindex=5 name="txtEmail" size=60
			SQLBefore=" AND Client.EMail LIKE '%" SQLAfter="%'"> 
	</tr>
	<tr>
  	<tr>
		<td align="right"><label class=control>Vehical Bar Code:</label></td>
		<td align="left">
			<input type=text tabindex=6 name="txtUPC" size=8> 
		</td>
	</tr>
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
				<Option Value="fName">First&nbsp;&nbsp;Name</option>
				<Option Value="Phone">Phone</option>
				<Option Value="Ctype">Type</option>
				<Option Value="C_Corp">Company</option>
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
	frmMain.txtUPC.focus
	frmMain.txtUPC.select
	
End Sub

Sub btnCancel_OnClick()
	Window.close
End Sub

Sub SelClient()
	Dim retClient,intdata
	'intdata = frmMain.intClientID.value&"|"&document.frmmain.cboVehMan.value&"|"&document.frmmain.strVModel.value&"|"&document.frmmain.cboVehMod.value&"|"&document.frmmain.cboVehColor.value
	retClient= ShowModalDialog("SelClientDlg.asp" ,"","dialogwidth:600px;dialogheight:350px;")
	IF retClient > 2 and len(trim(retClient))>0 then
		window.returnvalue = " AND Client.ClientID = "&retClient
		Window.close
	END IF
End Sub


Sub btnOK_OnClick()
	Dim objElement, strResult , retClientID
	
	If Not ValDataType Then
		Exit Sub
	End If	
	IF LEN(frmMain.txtUPC.value)> 0 then
		retClientID= ShowModalDialog("ClientAddDlg.asp?strUPC="& frmMain.txtUPC.value  &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:600px;dialogheight:350px;")
		IF retClientID > 0 then
			window.returnvalue = " AND Client.ClientID="&retClientID
			Window.close
		END IF
		Exit Sub

	END IF
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
