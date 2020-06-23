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
	Dim dbMain, hdnClientID,hdnvehid,intvehid,intVehNum,strvModel
	Dim MaxSQL,rsData,strSQL,strUPC,strTAG,intMake,intModel,intVyear,intColor,LocationID,LoginID,strMonthlyCharge
	Set dbMain =  OpenConnection

	hdnClientID = Request("hdnClientID")
	hdnvehid = Request("hdnvehid")
    LocationID = request("LocationID")
    LoginID = request("LoginID")
	
	If hdnvehid = 0 then
		MaxSQL = " Select ISNULL(Max(vehid),0)+1 from Vehical (NOLOCK)"
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			intvehid=rsData(0)
		End if
		Set rsData = Nothing
		MaxSQL = " Select ISNULL(Max(VehNum),0)+1 from Vehical (NOLOCK) WHERE ClientID=" & hdnClientID 
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			intVehNum=rsData(0)
		End if
		Set rsData = Nothing
        strMonthlyCharge = 0

	ELSE
		strSQL="SELECT vehical.vehid, vehical.upc, vehical.tag, vehical.ClientID, "&_
			" vehical.vehnum, vehical.make, vehical.model,vehical.Vmodel, vehical.vyear, "&_
			" vehical.Color, isnull(MonthlyCharge,0) as MonthlyCharge FROM Vehical(Nolock) WHERE ClientID=" & hdnClientID &" AND vehid ="& hdnvehid
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			If Not rsData.EOF Then
				intvehid = rsData("vehid")
				intVehNum = rsData("VehNum")
				strUPC = rsData("UPC")
				strTAG = rsData("TAG")
				intMake = rsData("Make")
				intModel = rsData("Model")
				strVModel = rsData("vModel")
				intColor = rsData("Color")
                strMonthlyCharge = rsData("MonthlyCharge")
			End If
			Set rsData = Nothing
		End If
	
	
	END IF

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
        <input type=hidden name="hdnClientID" value="<%=hdnClientID%>"/>
        <input type=hidden name="intvehid" value="<%=intvehid%>"/>
        <input type=hidden name="intVehNum" value="<%=intVehNum%>"/>
<div style="text-align:center">
<table border="0" width="425" cellspacing="0" cellpadding="4">
  	<tr>
		<td align="right"><label class=control>Vehical Number:</label></td>
        <td align="left" class="control" nowrap><b><%=intVehNum%></b></td>

	</tr>
  	<tr>
		<td align="right"><label class=control>UPC:</label></td>
        <td align="left" class="control" nowrap>
        <input maxlength="20" size="20" type=text tabindex=1 DirtyCheck=TRUE name="strUPC" value="<%=strUPC%>"></td>
	</tr>
  	<tr>
		<td align="right"><label class=control>Tag:</label></td>
        <td align="left" class="control" nowrap>
        <input maxlength="10" size="10" type=text tabindex=2 DirtyCheck=TRUE name="strTag" value="<%=strTag%>"></td>
	</tr>
	<tr>
		<td align="right"><label class=control>Make:</label></td>
		<td align="left">
		<Select name="cboMake" tabindex=3 DirtyCheck=TRUE>
			<%Call LoadListA(dbMain,3,intMake)%>		
		</select>
		</td>

	</tr>
	<tr>
		<td align="right"><label class=control>Model:</label></td>
		<td align="left"><input maxlength="40" size="30" type=text tabindex=2 DirtyCheck=TRUE name="strVmodel" value="<%=strVmodel%>"></td>
	</tr>
	<tr>
		<td align="right"><label class=control>UpCharge:</label></td>
		<td align="left">
		<Select name="cboModel" tabindex=4 DirtyCheck=TRUE>
			<%Call LoadList(dbMain,4,intModel)%>		
		</select>
		</td>

	</tr>
	<tr>
		<td align="right"><label class=control>Color:</label></td>
		<td align="left">
		<Select name="cboColor" tabindex=6 DirtyCheck=TRUE>
			<%Call LoadListA(dbMain,5,intColor)%>		
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
      <td align="center" colspan="3">
	  <button name="btnSave" class="button" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" style="width:75">Save</button>&nbsp;&nbsp;&nbsp;
	  <button name="btnCancel" class="button" tabindex=16 onmouseover="ButtonHigh()" onmouseout="ButtonLow()" style="width:75">Cancel</button>
    </td>
	</tr>		
</table>
</div>
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
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
			strResult = strResult & txtReplace(objElement.Value)&"|"
		End If
	Next	
	window.returnvalue = strResult
	Window.close
End Sub

Function txtReplace(SQLField)
	Dim strText
	strText=SQLField
	strText=Replace(strText, "'", "''")
	strText=Replace(strText, """", """""")
	strText=Trim(strText)
	txtReplace=strText
End Function
</script>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
