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

Dim dbMain, strLoc,hdnFilterBy,hdnVehArr,hdnvehid,LocationID,LoginID
Set dbMain =  OpenConnection
hdnFilterBy = Request("hdnFilterBy")
hdnVehArr = Request("hdnVehArr")
hdnvehid = Request("hdnvehid")
    LocationID = request("LocationID")
    LoginID = request("LoginID")

Select Case Request("FormAction")
	Case "SaveVeh"
		Call VehData(dbMain,hdnVehArr)
	Case "DeleteVeh"
		Call DeleteVeh(dbMain,hdnvehid)
End Select

'********************************************************************
' HTML
'********************************************************************
%>

<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<title></title>
</head>
<body class=pgbody Onclick="SetDirty" onkeyup="SetDirty()">
<form method="POST" name="frmMain" action="VehList.asp"> 
<input type=hidden name=FormAction value=>
<input type=hidden name=hdnFilterBy value="<%=hdnFilterBy%>">
<input type=hidden name=hdnVehArr value="<%=hdnVehArr%>">
<input type=hidden name=hdnvehid value="<%=hdnvehid%>">
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
		<td align=center class="tdcaption" background="images/header.jpg" width=210>Vehical List</td>
		<td align=right>			
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
	

		Dim strFilt
		strFilt = ShowModalDialog("VehListDlg.asp?LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value,,"center:1;dialogwidth:550px;dialogheight:450px;")
		If Len(strFilt) = 0 Then 
			window.event.ReturnValue = False
		Else
		'msgbox strFilt
			fraMain.location.href = "VehListFra.asp?hdnFilterBy="& ClientEncode(strFilt) &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
			window.event.ReturnValue = False
		End If
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
		location.href="VehEdit.asp?hdnBPOID=0&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	
	Case "btnFilter"

		Dim strFilt
		strFilt = ShowModalDialog("VehListDlg.asp?LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value,,"center:1;dialogwidth:550px;dialogheight:450px;")
		If Len(strFilt) = 0 Then 
			window.event.ReturnValue = False
		Else
		'msgbox strFilt
			fraMain.location.href = "VehListFra.asp?hdnFilterBy="& ClientEncode(strFilt) &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
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
Sub VehData(dbMain,hdnVehArr)
	Dim strSQL,rsData,arrFilterBy,strSQL2
	arrFilterBy = Split(hdnVehArr,"|")
	strSQL = " Select * from vehical (NOLOCK) where vehid="&arrFilterBy(1)
	If dbOpenRecordSet(dbmain,rsData,strSQL) Then
		IF rsData.eof then
			strSQL2= "Insert into vehical (LocationID,ClientID,vehid,Vehnum,UPC,TAG,Make,vmodel,model,color,MonthlyCharge) Values ("&_
				request("LocationID") &","&_
				arrFilterBy(0) &","&_
				arrFilterBy(1) &","&_
				arrFilterBy(2) &","&_
				"'" & arrFilterBy(3) &"',"&_
				"'" & ucase(arrFilterBy(4)) &"',"&_
				arrFilterBy(5) &","&_
				"'" & arrFilterBy(6) &"',"&_
				arrFilterBy(7) &","&_
				arrFilterBy(8) &","&_
				arrFilterBy(9) &")"
			IF NOT DBExec(dbMain, strSQL2) then
				Response.Write gstrMsg
				Response.End
			END IF
		ELSE
			strSQL2= "UPDATE vehical  set "&_
					"UPC ='" & arrFilterBy(3) &"',"&_
					"TAG ='" & ucase(arrFilterBy(4)) &"',"&_
					"Make =" & arrFilterBy(5) &","&_
					"vmodel ='" & arrFilterBy(6) &"',"&_
					"model =" & arrFilterBy(7) &","&_
					"color =" & arrFilterBy(8) &","&_
					"MonthlyCharge =" & arrFilterBy(9) &_
					" Where vehid =" & arrFilterBy(1)
			IF NOT DBExec(dbMain, strSQL2) then
				Response.Write gstrMsg
				Response.End
			END IF
		END IF
	End if
	Response.Redirect "VehList.asp?LocationID="& request("LocationID") &"&LoginID="&request("LoginID") 
End Sub



Sub DeleteVeh(dbMain, intvehid)
	Dim strSQL,rs
	strSQL=" DELETE Vehical WHERE vehid=" & intvehid 
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
	Response.Redirect "VehList.asp?LocationID="& request("LocationID") &"&LoginID="&request("LoginID")
End Sub

%>
