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

Dim dbMain, intVenID, strVendor, strVenContact, strVenaddr1, strVenaddr2, strVencity,_
	 strVenst, strVenzip,_
    strVenPhone, strVenfax, strVenUrl, strVenEmail, strvenAcc
Set dbMain =  OpenConnection

intVenID = Request("intVenID")



Select Case Request("FormAction")
	Case "btnSave"
		Call UpdateData(dbMain,intVenID)
End Select


If intVenID > 0 Then 
	Call GetVendorInfo(dbMain,intVenID, strVendor, strVenContact, strVenaddr1, strVenaddr2, strVencity, strVenst, strVenzip,_
    strVenPhone, strVenfax, strVenUrl, strVenEmail, strvenAcc)
End If



%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="VendoreditDlgFra.asp">
<div style="text-align:center">
<input type="hidden" name="FormAction" tabindex="-2" value>
<input type="hidden" name="intVenID" tabindex="-2" value="<%=intVenID%>">
<input type="hidden" name="strVenst" tabindex="-2" value="<%=strVenst%>">
<table align="center" border="0" width="500" cellspacing="0" cellpadding="0">
	<tr>
		<td align="right" class="control" nowrap>Vendor:</td>
        <td align="left" class="control" nowrap><input maxlength="20" size="20" type=text tabindex=1 DirtyCheck=TRUE name="strvendor" value="<%=strvendor%>">&nbsp; </td>
		<td align="right" class="control" nowrap>Phone:</td>
        <td align="left" class="control" nowrap><input maxlength="14" size="14" type=text tabindex=8 DirtyCheck=TRUE name="strVenPhone" value="<%=strVenPhone%>">&nbsp; 
	</tr>
	<tr>
 		<td align="right" class="control" nowrap>Contact:</td>
       <td align="left" class="control" nowrap><input maxlength="20" size="20" type=text tabindex=2 DirtyCheck=TRUE name="strvenContact" value="<%=strvenContact%>">&nbsp; </td>
		<td align="right" class="control" nowrap>Fax:</td>
        <td align="left" class="control" nowrap><input maxlength="14" size="14" type=text tabindex=10 DirtyCheck=TRUE name="strVenFax" value="<%=strVenFax%>">&nbsp; 
	</tr>
	<tr>
        <td align="right"  ><label class="control">Address:</label></td>
        <td><input tabindex="3" type="text" name="strvenaddr1" size="40" DataType="text" DirtyCheck="TRUE" value="<%=strvenaddr1%>"></td>
 		<td align="right" class="control" nowrap>Account:</td>
        <td><input tabindex="11" type="text" name="strVenacc" size="20" DataType="text" DirtyCheck="TRUE" value="<%=strvenacc%>"></td>
 	</tr>
	<tr>
      <td align="right"  ><label class="control"> </label></td>
        <td><input tabindex="4" type="text" name="strvenaddr2" size="40" DataType="text" DirtyCheck="TRUE" value="<%=strvenaddr2%>"></td>
    </tr>
	<tr>
		<td align="right"  ><label class="control">City:</label></td>
        <td colspan=3 ><input tabindex="6" type="text" name="strvenCity" size="20" DataType="text" DirtyCheck="TRUE" value="<%=strvenCity%>">&nbsp;<label class="control">St:</label>
		<Select name="cboVenSt" tabindex=7 DirtyCheck=TRUE>
			<%Call LoadList(dbMain,-1,strvenSt)%>		
		</select><label class="control">Zip:</label>
        <input tabindex="5" type="text" name="strVenZip" onkeyup="CheckZip()" size="5" DataType="text" DirtyCheck="TRUE" value="<%=strVenZip%>"></td>
	</tr>
</table>
<table align="center" border="0" width="500" cellspacing="0" cellpadding="0">
	<tr>
		<td align="right"><label class="control">Email:</label></td>
        <td><input tabindex="14" type="text" name="strvenEmail" size="80" DataType="text" DirtyCheck="TRUE" value="<%=strVenEmail%>"></td>
	</tr>
	<tr>
		<td align="right"><label class="control">URL:</label></td>
        <td><input tabindex="14" type="text" name="strvenURL" size="80" DataType="text" DirtyCheck="TRUE" value="<%=strvenURL%>"></td>
	</tr>
</table>
<br>
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
' Vendor-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">
Option Explicit


Sub Window_OnLoad()
End Sub

Sub chnSave()
		frmMain.FormAction.value="btnSave"
		frmMain.submit()
End Sub

Sub CheckZip()
	Dim strZipArr,strCitySTArr
	IF Len(trim(frmMain.strVenZip.value)) = 5 then
		strZipArr = ShowModalDialog("ClientZipDlg.asp?hdnZip=" & trim(frmMain.strVenZip.Value),,"center:1;dialogleft:200px;dialogtop:200px; dialogwidth:200px;dialogheight:200px;")
		IF LEN(TRIM(strZipArr))>3 then
			strCitySTArr = Split(strZipArr,"|")
			frmMain.strVenZip.value = trim(frmMain.strVenZip.Value)
			frmMain.strvenCity.value = strCitySTArr(0)
			frmMain.strVenst.value = strCitySTArr(1)
			frmMain.cbovenSt.value = strCitySTArr(1)
			frmMain.strvenPhone.focus
		END IF
	END IF
END Sub


Sub Cancel()
	window.returnValue = frmMain.intVenID.value &"|"& frmMain.strVendor.value
	window.close
End Sub

</script>

<%
	Call CloseConnection(dbMain)
End Sub

'********************************************************************
' Server-Side Functions
'********************************************************************
Sub GetVendorInfo(dbMain,intVenID, strVendor, strvenContact, strVenaddr1, strVenaddr2, strVencity, strVenst, strVenzip,_
    strVenPhone, strVenfax, strVenUrl, strVenEmail, strvenAcc)
	Dim strSQL, RS 
	strSQL="SELECT * FROM Vendor(Nolock) WHERE VenID=" & intVenID 
	If DBOpenRecordset(dbMain,rs,strSQL) Then
		If Not RS.EOF Then
	
			strVendor = RS("Vendor")
			strvenContact = RS("venContact")
			strvenaddr1 = RS("venaddr1")
			strVenaddr2 = RS("Venaddr2")
			strVencity = RS("Vencity")
			strVenst = RS("Venst")
			strVenzip = RS("Venzip")
			strVenPhone = RS("VenPhone")
			strVenFax = RS("VenFax")
			strVenAcc = RS("VenAcc")
			strVenEmail = RS("VenEmail")
			strVenURL = RS("VenURL")
		End If
	End If


	IF isnull(strVenst) or len(strVenst)=0 then
		strVenst = "GA"
	end if
End Sub


Sub UpdateData(dbMain,intVenID)
	Dim strSQL,rsData,MaxSQL,intvehid
	IF intVenID = 0 then
		MaxSQL = " Select ISNULL(Max(VenID),0)+1 from Vendor (NOLOCK)"
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			intVenID=rsData(0)
		End if
		Set rsData = Nothing
		strSQL= "Insert into Vendor (VenID,Vendor,VenContact,Venaddr1,Venaddr2,Vencity,Venst,Venzip,"&_
			"VenPhone,VenFax,VenAcc,VenEmail,VenURL) Values ("&_
			intVenID &","&_
			"'" & SQLReplace(Request("strVendor")) &"',"&_
			"'" & SQLReplace(Request("strVenContact")) &"',"&_
			"'" & SQLReplace(Request("strVenaddr1")) &"',"&_
			"'" & SQLReplace(Request("strVenaddr2")) &"',"&_
			"'" & SQLReplace(Request("strVencity")) &"',"&_
			"'" & Request("cboVenst") &"',"&_
			"'" & SQLReplace(Request("strVenzip")) &"',"&_
			"'" & SQLReplace(Request("strVenPhone")) &"',"&_
			"'" & SQLReplace(Request("strVenFax")) &"',"&_
			"'" & SQLReplace(Request("strVenAcc")) &"',"&_
			"'" & Request("strVenEmail") &"',"&_
			"'" & Request("strVenURL") &"')"
		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
	ELSE
		strSQL= "UPDATE Vendor  set "&_
				"Vendor ='" & SQLReplace(Request("strVendor")) &"',"&_
				"VenContact ='" & SQLReplace(Request("strVenContact")) &"',"&_
				"Venaddr1 ='" & SQLReplace(Request("strVenaddr1")) &"',"&_
				"Venaddr2 ='" & SQLReplace(Request("strVenaddr2")) &"',"&_
				"Vencity ='" & SQLReplace(Request("strVencity")) &"',"&_
				"Venst ='" & Request("CboVenst") &"',"&_
				"Venzip ='" & SQLReplace(Request("strVenzip")) &"',"&_
				"VenPhone ='" & SQLReplace(Request("strVenPhone")) &"',"&_
				"VenFax ='" & SQLReplace(Request("strVenFax")) &"',"&_
				"VenAcc ='" & SQLReplace(Request("strVenAcc")) &"',"&_
				"VenEmail ='" & Request("strEmail") &"',"&_
				"VenURL ='" & Request("strVenURL") &"'"&_
				" Where VenID =" & intVenID

		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
	END IF
End Sub


%>
