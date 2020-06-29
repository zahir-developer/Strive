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
	Dim dbMain, intClientID,hdnvehid,intvehid,intVehNum,strClientName
	Dim MaxSQL,rsData,strSQL,strUPC,strTAG,intMake,intModel,intColor,strVModel,strMonthlyCharge
	Set dbMain =  OpenConnection
	hdnvehid = Request("hdnvehid")
	strSQL =" SELECT vehical.vehid, vehical.upc, vehical.tag, vehical.ClientID, "&_
			" vehical.vehnum, vehical.make, vehical.model,vehical.Vmodel, vehical.vyear, "&_
			" vehical.Color, client.fname + ' ' + client.lname AS ClientName,isnull(MonthlyCharge,0) as MonthlyCharge"&_
			" FROM vehical(nolock)"&_
			" INNER JOIN client(nolock) ON vehical.ClientID = client.ClientID"&_
			" WHERE vehid ="& hdnvehid


	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		If Not rsData.EOF Then
			intvehid = rsData("vehid")
			intClientID  = rsData("ClientID")
			strClientName   = rsData("ClientName")
			intVehNum = rsData("VehNum")
			strUPC = rsData("UPC")
			strTAG = ucase(rsData("TAG"))
			intMake = rsData("Make")
			intModel = rsData("Model")
			strVModel = rsData("VModel")
			intColor = rsData("Color")
            strMonthlyCharge = rsData("MonthlyCharge")
		End If
		Set rsData = Nothing
	End If

'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title>Client Vehical</title>
</head>
<body class="pgbody">
<form method=post name="frmMain">
<div style="text-align:center">
<table border="0" style="width:425px" cellspacing="0" cellpadding="4">
  	<tr>
		<td style="text-align: right"><label class=control>Client Name:</label></td>
        <td style="text-align: left; white-space:nowrap" class="control" ><b><%=strClientName%></b></td>
	</tr>
  	<tr>
		<td style="text-align: right"><label class=control>Vehical Number:</label></td>
        <td style="text-align: left; white-space:nowrap" class="control" ><b><%=intVehNum%></b></td>
        <input type=hidden name="intClientID" value="<%=intClientID%>"></td>
        <input type=hidden name="intvehid" value="<%=intvehid%>"></td>
        <input type=hidden name="intVehNum" value="<%=intVehNum%>"></td>
	</tr>
  	<tr>
		<td style="text-align: right"><label class=control>Bar Code:</label></td>
        <td style="text-align: left; white-space:nowrap" class="control" >
        <input maxlength="20" size="20" type=text tabindex=1 DirtyCheck=TRUE name="strUPC" value="<%=strUPC%>"></td>
	</tr>
  	<tr>
		<td style="text-align: right"><label class=control>Tag:</label></td>
        <td style="text-align: left; white-space:nowrap" class="control" >
        <input maxlength="10" size="10" type=text tabindex=2 DirtyCheck=TRUE name="strTag" value="<%=strTag%>"></td>
	</tr>
	<tr>
		<td style="text-align: right"><label class=control>Make:</label></td>
		<td style="text-align: left">
		<Select name="cboMake" tabindex=3 DirtyCheck=TRUE>
			<%Call LoadList(dbMain,3,intMake)%>		
		</select>
		</td>
	</tr>
	<tr>
		<td style="text-align: right"><label class=control>Model:</label></td>
		<td style="text-align: left">
        <input maxlength="40" size="30" type=text tabindex=2 DirtyCheck=TRUE name="strVModel" value="<%=strVModel%>">
		</td>
	</tr>
	<tr>
		<td style="text-align: right"><label class=control>Up Charge:</label></td>
		<td style="text-align: left">
		<Select name="cboModel" tabindex=4 DirtyCheck=TRUE>
			<%Call LoadList(dbMain,4,intModel)%>		
		</select>
		</td>
	</tr>
	<tr>
		<td style="text-align: right"><label class=control>Color:</label></td>
		<td style="text-align: left">
		<Select name="cboColor" tabindex=6 DirtyCheck=TRUE>
			<%Call LoadList(dbMain,5,intColor)%>		
		</select>
		</td>
	</tr>
    <tr>
		<td style="text-align: right"><label class=control>Monthly Charge:</label></td>
		<td style="text-align: left">
        <input type=number min="0" step="any" tabindex=2 DirtyCheck=TRUE name="strMonthlyCharge" value="<%=strMonthlyCharge%>">
		</td>
	</tr>

</table>
<table>
   <tr>
      <td style="text-align: center" colspan="3">
	  <button name="btnSave" class="button" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" style="width:75px">Save</button>&nbsp;&nbsp;&nbsp;
	  <button name="btnCancel" class="button" tabindex=16 onmouseover="ButtonHigh()" onmouseout="ButtonLow()" style="width:75px">Cancel</button>
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
<script type="text/vbscript">
Option Explicit

Sub Window_Onload()
End Sub

Sub btnCancel_OnClick()
	Window.close
End Sub

Sub btnsave_OnClick()
	Dim objElement, strResult
	
	If Not ValDataType Then
		Exit Sub
	End If	
	
	For Each objElement in document.all
		If UCase(objElement.Tagname)="INPUT" Or UCase(objElement.Tagname)="SELECT" Then
			strResult = strResult & SQLReplace(objElement.Value)&"|"
		End If
	Next	
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
