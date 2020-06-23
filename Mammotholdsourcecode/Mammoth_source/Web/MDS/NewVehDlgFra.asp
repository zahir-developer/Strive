<%@ LANGUAGE="VBSCRIPT" %>
<%
'********************************************************************
' Name: 
'********************************************************************
Option Explicit
Response.Expires = 0
Response.Buffer = True

Dim Title
Dim gstrMessage

'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incDatabase.asp"-->
<!--#include file="inccommon.asp"-->
<%
'********************************************************************
' Global Variables
'********************************************************************
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

Dim dbMain, intdata,arrData,intClientID, strfname, strlname,intvehid,intVehNum,strUPC,strTag
dim intmake,intmodel,intColor,strSQL,rs,strVmodel,LocationID,LoginID
Set dbMain =  OpenConnection
    LocationID = request("LocationID")
    LoginID = request("LoginID")
	intdata=Request("intdata")
	arrData = split(intdata,"|")

IF Request("FormAction") = "btnSave" then
	call UpdateData(dbMain)
	intClientID = Request("intClientID")
	Call GetNewInfo(dbMain,intClientID,intvehid,intVehNum,strfname, strlname)
	intmake = 0
	intmodel = 0
	intColor = 0
	strUPC = ""
	strTag = ""
	strVmodel = ""
ELSEIF Request("FormAction") = "DeleteVeh" then
	strSQL=" DELETE Vehical WHERE vehid=" & Request("hdnvehid")
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
	intClientID = arrData(0)
	intmake = arrData(1)
	strVmodel = arrData(2)
	intmodel = arrData(3)
	intColor = arrData(4)
	If intClientID="" OR intClientID<3 Then 
		intClientID=0
	ELSE
		Call GetNewInfo(dbMain,intClientID,intvehid,intVehNum,strfname, strlname)
	End If
ELSE
	intClientID = arrData(0)
	intmake = arrData(1)
	strVmodel = arrData(2)
	intmodel = arrData(3)
	intColor = arrData(4)
	If intClientID="" OR intClientID<3 Then 
		intClientID=0
	ELSE
		Call GetNewInfo(dbMain,intClientID,intvehid,intVehNum,strfname, strlname)
	End If
END IF

%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="NewVehDlgFra.asp">
<div style="text-align:center">
<input type="hidden" name="FormAction" tabindex="-2" value>
<input type="hidden" name="intClientID" tabindex="-2" value="<%=intClientID%>">
<input type="hidden" name="intdata" tabindex="-2" value="<%=intdata%>">
<input type=hidden name="intvehid" value="<%=intvehid%>"></td>
<input type=hidden name="intVehNum" value="<%=intVehNum%>"></td>
            <input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
            <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<table align="center" border="0" width="500" cellspacing="0" cellpadding="0">
	<tr>
		<td align="center" class="control" nowrap>Name:&nbsp;<%=strfname%>&nbsp;<%=strlname%>&nbsp; </td>
	</tr>
</table>
<table align="center" border="0" width="500" cellspacing="0" cellpadding="0">
  	<tr>
		<td align="left"><label class=control>Vehical #:&nbsp;</label><label class=control><b><%=intVehNum%>&nbsp;</b></label></td>&nbsp;
		<td align="left"><label class=control>Bar Code:</label>
		<input maxlength="20" size="20" type=text tabindex=1 DirtyCheck=TRUE name="strUPC" value="<%=strUPC%>"></td>
		<td align="left"><label class=control>Tag:</label>
		<input maxlength="10" size="10" type=text tabindex=2 DirtyCheck=TRUE name="strTag" value="<%=strTag%>"></td>
	</tr>
</table>
<table align="center" border="0" width="500" cellspacing="0" cellpadding="0">
	<tr>
       <td>&nbsp;</td>
		<td align="left" class="control" nowrap>Type:</td>
		<td align="left" class="control" nowrap>Model:</td>
		<td align="left" class="control" nowrap>Upcharge:</td>
		<td align="left" class="control" nowrap>Color:</td>
 	</tr>
	<tr>
       <td>&nbsp;</td>
		<td align="left" class="control" nowrap> 
		<Select name="cboVehMan" tabindex=1 >
			<%Call LoadListA(dbMain,3,intmake)%>		
		</select></td>
		<td align="left" class="control" nowrap><input maxlength="40" size="30" type=text tabindex=2 DirtyCheck=TRUE name="strVmodel" value="<%=strVmodel%>"></td>
		<td align="left" class="control" nowrap> 
		<Select name="cboVehMod" tabindex=1  >
			<%Call LoadList(dbMain,4,intmodel)%>		
		</select></td>
		<td align="left" class="control" nowrap> 
		<Select name="cboVehColor" tabindex=1 >
			<%Call LoadListA(dbMain,5,intColor)%>		
		</select></td>
	</tr>
</table>
<br>
<table align="center" border="0" width="500" cellspacing="0" cellpadding="0">
	<iframe align="center" Name="fraMain" src="NewVehListFra.asp?hdnClientID=<%=intClientID%>&intdata=<%=intdata%>"  height="120" width="500" frameborder="0"></iframe>
</table>
<table align="center" border="0" width="500" cellspacing="1" cellpadding="1">
   <tr>
      <td align="center" colspan="3">
	  <button name="btnSave" class="button" style="width:75" OnClick="chnSave()">Save</button>&nbsp;&nbsp;&nbsp;&nbsp;
	  <button name="btnCancel" class="button" style="width:75" OnClick="Cancel()">Cancel</button>
		</td>
	</tr>		
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


Sub Window_OnLoad()
	framain.location.href = "NewVehListFra.asp?hdnClientID=" & frmMain.intClientID.Value & "&intdata=" & frmMain.intdata.Value &"&LocationID=" & document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
End Sub

Sub chnSave()
		'frmMain.intdata.value  = frmMain.intClientID.value
		frmMain.FormAction.value="btnSave"
		frmMain.submit()
End Sub


Sub Cancel()
	window.returnValue = frmMain.intvehid.value
	window.close
End Sub

</script>

<%
	Call CloseConnection(dbMain)
End Sub

'********************************************************************
' Server-Side Functions
'********************************************************************
Sub GetNewInfo(dbMain,intClientID,intvehid,intVehNum,strfname, strlname)
	Dim strSQL, RS ,MaxSQL,rsData
	
	strSQL="SELECT client.fname, client.lname"&_
		" FROM client(NOLOCK)"&_
		" WHERE client.ClientID = " & intClientID 


	If DBOpenRecordset(dbMain,rs,strSQL) Then
		If Not RS.EOF Then
			strfname = RS("fname")
			strlname = RS("lname")
		End If
	End If


	strSQL="SELECT vehid from Vehical(NOLOCK) WHERE ClientID=" & intClientID &" AND vehnum=1 AND MAKE=0 "
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		If Not rsData.EOF Then
				intvehid=rsData(0)
				intVehNum=1
		ELSE
			MaxSQL = " Select ISNULL(Max(vehid),0)+1 from Vehical "
			If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
				intvehid=rsData(0)
			End if
			Set rsData = Nothing
			MaxSQL = " Select ISNULL(Max(VehNum),0)+1 from Vehical(NOLOCK) WHERE ClientID=" & intClientID 
			If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
				intVehNum=rsData(0)
			End if
			Set rsData = Nothing
		END IF
	END IF
End Sub


Sub UpdateData(dbMain)
	Dim strSQL,rsData,strSQL2
	strSQL="SELECT Vehnum from Vehical(NOLOCK) WHERE vehid=" & Request("intvehid") 
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		If Not rsData.EOF Then
			strSQL2= "UPDATE vehical  set "&_
					"UPC ='" & Request("strUPC") &"',"&_
					"TAG ='" & Request("strTAG")&"',"&_
					"Make =" & Request("cboVehMan") &","&_
					"vmodel ='" & Request("strVmodel") &"',"&_
					"model =" & Request("cboVehMod") &","&_
					"color =" & Request("cboVehColor") &_
					" Where vehid =" & Request("intvehid") 
			IF NOT DBExec(dbMain, strSQL2) then
				Response.Write gstrMsg
				Response.End
			END IF



		ELSE
			strSQL= "Insert into Vehical (LocationID,ClientID,vehid,Vehnum,UPC,TAG,vmodel,Make,model,color) Values ("&_
				SQLReplace(Request("LocationID")) &","&_
				SQLReplace(Request("intClientID")) &","&_
				SQLReplace(Request("intvehid")) &","&_
				SQLReplace(Request("intVehnum")) &","&_
				"'" & SQLReplace(Request("strUPC")) &"',"&_
				"'" & SQLReplace(Request("strTAG")) &"',"&_
				"'" & Request("strVmodel") &"',"&_
				"'" & Request("cboVehMan") &"',"&_
				"'" & Request("cboVehMod") &"',"&_
				"'" & Request("cboVehColor") &"')"
			IF NOT DBExec(dbMain, strSQL) then
				Response.Write gstrMsg
				Response.End
			END IF
		END IF
	END IF

End Sub


%>
