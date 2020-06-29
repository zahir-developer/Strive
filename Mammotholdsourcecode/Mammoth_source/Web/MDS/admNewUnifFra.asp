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

Dim dbMain, intUserID,intUnifID,intactID, dtActDate,intActType,strActAmt,intProdID,blsave,strActCost,intActQty,LocationID,LoginID

Set dbMain =  OpenConnection
    LocationID = request("LocationID")
    LoginID = request("LoginID")

intUserID=Request("intUserID")
blsave = false

Select Case Request("FormAction")
	Case "btnSave"
		Call UpdateData(dbMain,intUserID,LocationID,LoginID)
		blsave = true
End Select

%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css" />
<Title></title>
</head>
<body class="pgbody">
<form method="post" name="frmMain" action="admNewUnifFra.asp">
<div style="text-align: center">
<input type="hidden" name="FormAction" tabindex="-2" value>
<input type="hidden" name="intUserID" tabindex="-2" value="<%=intUserID%>" />
<input type="hidden" name="intProdID" tabindex="-2" value="<%=intProdID%>" />
<input type="hidden" name="strActCost" tabindex="-2" value="<%=strActCost%>" />
<input type="hidden" name="strActAmt" tabindex="-2" value="<%=strActAmt%>" />
<input type="hidden" name="blsave" tabindex="-2" value="<%=blsave%>" />
    <input type="hidden" name="LocationID" value="<%=LocationID%>" />
    <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<table align="center" border="0" width="350" cellspacing="0" cellpadding="0">
	<tr>
		<td align="right" class="control" nowrap>Date:</td>
			<td align="left" class="control"><input maxlength="10" size="10" Type="RD" title="To Request:" tabindex=1 DirtyCheck=TRUE name="dtActDate" value="<%=dtActDate%>"></td>
	</tr>
	<tr>
		<td align="right">&nbsp;</td>
		<td align="right">&nbsp;</td>
	</tr>
	<tr>

		<td align="right" class="control" nowrap>Product:</td>
		<td align="left" class="control" nowrap> 
		<Select name="cboProdID" tabindex=1 OnClick="ProdChg()" >
			<option Value=""></option>
			<%Call LoadProduct(dbMain)%>		
		</select>
		</td>
	</tr>
	<tr>
		<td align="right">&nbsp;</td>
		<td align="right">&nbsp;</td>
	</tr>
</table>
<table align="center" border="0" width="350" cellspacing="0" cellpadding="0">
	<tr>
		<td align="right">&nbsp;</td>
		<td align="left"><label class="control">Cost:&nbsp;&nbsp;</label><label id="lblActCost" class="control"></label></td>
			<td align="right"><label class="control">Qty:</label></td>
        <td><input tabindex="2" type="text" name="intActQty" size="4" DataType="text" Onkeyup="ProdChg()" value="<%=intActQty%>"></td>
		<td align="right">&nbsp;</td>
		<td align="left"><label class="control">Amount:&nbsp;&nbsp;</label><label id="lblActAmt" class="control"></label></td>
	</tr>
</table>
<br>
<table align="center" border="0" width="350" cellspacing="0" cellpadding="0">
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
	IF frmMain.blSave.value then
		window.close
	END IF 
frmMain.dtActDate.value = date()
frmMain.intActQty.value = 1
lblActCost.innerText =  formatcurrency(0.0)
lblActAmt.innerText =  formatcurrency(0.0)
End Sub

Sub ProdChg()
	dim arrProd
	IF LEN(frmMain.cboProdID.value)=0 then
		MsgBox "Please Select the product.",64,"Error"
		exit sub
	END IF
	arrProd = split(frmMain.cboProdID.value,"|")
	frmMain.intProdID.value = arrProd(0)
	frmMain.strActCost.value = arrProd(1)
	lblActCost.innerText =  formatcurrency(frmMain.strActCost.value)
	frmMain.strActAmt.value = cdbl(arrProd(1))*cdbl(frmMain.intActQty.value)
	lblActAmt.innerText =  formatcurrency(frmMain.strActAmt.value)
End Sub

Sub chnSave()
	IF LEN(frmMain.dtActDate.value)=0 then
		MsgBox "Please enter the Date.",64,"Error"
		exit sub
	END IF
	IF LEN(frmMain.strActAmt.value)=0 then
		MsgBox "Please enter the Amount.",64,"Error"
		exit sub
	END IF
	IF LEN(frmMain.intProdID.value)=0 then
		MsgBox "Please enter the Reason.",64,"Error"
		exit sub
	END IF
		frmMain.FormAction.value="btnSave"
		frmMain.submit()
End Sub


Sub Cancel()
	window.close
End Sub

</script>

<%
	Call CloseConnection(dbMain)
End Sub

'********************************************************************
' Server-Side Functions
'********************************************************************
Sub UpdateData(dbMain,intUserID,LocationID,LoginID)
	Dim strSQL,rsData,MaxSQL,intUnifID
	MaxSQL = " Select ISNULL(Max(UnifID),0)+1 from UserUnif (NOLOCK) where userid="&intUserID &" AND LocationID="& LocationID
	If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
		intUnifID=rsData(0)
	End if
	Set rsData = Nothing

	strSQL= "Insert into UserUnif (UserID,LocationID,UnifID,ActID,ActDate,ActType,ActCost,ActAmt,ActQty,ProdID,editby,editdate) Values ("&_
		intUserID &","&_
		LocationID &","&_
		intUnifID &","&_
		1 &","&_
		"'" & SQLReplace(Request("dtActDate")) &"',"&_
		"'Purchase',"&_
		SQLReplace(Request("strActCost")) & ","&_
		SQLReplace(Request("strActAmt")) & ","&_
		SQLReplace(Request("intActQty")) & ","&_
		SQLReplace(Request("intProdID")) & ","&_
		LoginID & ","&_
		"'" & date() & "')"
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
End Sub

Function LoadProduct(db)
	Dim strSQL,RS,strSel,temp,intType,blnDeleted,rsData2,strSQL2
	blnDeleted =False
	strSQL="SELECT Descript,ProdID,cost FROM Product (nolock) where Dept=3 order by Descript"
	If dbOpenRecordSet(db,rs,strSQL) Then
		Do While Not RS.EOF
			strSel = RS("ProdID") & "|"&RS("cost")
			%>
			<option Value="<%=strSel%>"><%=RS("Descript")%></option>
			<%
			RS.MoveNext
		Loop
	End If
End Function

%>
