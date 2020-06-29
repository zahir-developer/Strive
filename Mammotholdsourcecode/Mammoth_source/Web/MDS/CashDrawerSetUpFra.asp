<%@ LANGUAGE="VBSCRIPT" %>
<%
Option Explicit
Response.Expires = 0
Response.Buffer = True
Server.ScriptTimeout = 90000

'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incCommon.asp"-->
<!--#include file="incDatabase.asp"-->
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
	Dim dbMain,txtRptDate,rsData,strSQL
	Dim intCIp,intCIn,intCId,intCIq,intCIh,intCI1,intCI5,intCI10,intCI20,intCI50,intCI100,LocationID,LoginID
	Dim intCIRp,intCIRn,intCIRd,intCIRq
	dim intTotalCash,strStatus,intDrawerNo
	Set dbMain = OpenConnection	
	
	txtRptDate =  Request("txtRptDate")
	intDrawerNo =  Request("intDrawerNo")
    LocationID = Request("LocationID")
    LoginID = Request("LoginID")
	Select case Request("FormAction")
		Case "btnSave"
			Call SaveData(dbMain,intDrawerNo)
			strStatus = "Yes"
	END SELECT
strSQL = " Select * from CashD (NOLOCK) where ndate='"&txtRptDate&"' and DrawerNo = "&intDrawerNo &" and LocationID=" & LocationID
If dbOpenRecordSet(dbmain,rsData,strSQL) Then
	IF not rsData.EOF then
		intTotalCash = rsData("CITotal")
		intCIp = rsData("CIp")
		intCIn = rsData("CIn")
		intCId = rsData("CId")
		intCIq = rsData("CIq")
		intCIRp = rsData("CIRp")
		intCIRn = rsData("CIRn")
		intCIRd = rsData("CIRd")
		intCIRq = rsData("CIRq")
		intCIh = rsData("CIh")
		intCI1 = rsData("CI1")
		intCI5 = rsData("CI5")
		intCI10 = rsData("CI10")
		intCI20 = rsData("CI20")
		intCI50 = rsData("CI50")
		intCI100 = rsData("CI100")
	ELSE
		intTotalCash = 0.0
		intCIp = 0
		intCIn = 0
		intCId = 0
		intCIq = 0
		intCIRp = 0
		intCIRn = 0
		intCIRd = 0
		intCIRq = 0
		intCIh = 0
		intCI1 = 0
		intCI5 = 0
		intCI10 = 0
		intCI20 = 0
		intCI50 = 0
		intCI100 = 0
	END IF
End if


'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<title></title>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
</head>
<body class="pgBody">
<div style="text-align:center">
<form name="frmMain" Method="POST" action="CashDrawerSetUpFra.Asp">
<Input type="hidden" name="LocationID" value="<%=LocationID%>" />
<Input type="hidden" name="LoginID" value="<%=LoginID%>" />
<input type=hidden name=FormAction value=>
<input type="hidden" name="intTotalCash" tabindex="-2" value="<%=intTotalCash%>">
<input type="hidden" name="txtRptDate" tabindex="-2" value="<%=txtRptDate%>">
<input type="hidden" name="intDrawerNo" tabindex="-2" value="<%=intDrawerNo%>">
<input type="hidden" name="strStatus" tabindex="-2" value="<%=strStatus%>">
<table cellspacing="0" width="500">
	<tr>
		<td align="center" class="control" colspan=5 nowrap><label class=control>Drawer #:&nbsp;
		<B><label class="blkdata" ID="lblDrawerNo" ></B></label></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="left" class="control" nowrap><b>Coins</B></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="left" class="control" nowrap><b>Rolls</B></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>Pennies:&nbsp;</td>
        <td align="left" class="control" nowrap><input maxlength="8" size="8" type=text tabindex=1 onchange="UpdateTotal()" name="intCIp" value="<%=intCIp%>"></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="right" class="control" nowrap>Pennies:&nbsp;</td>
        <td align="left" class="control" nowrap><input maxlength="8" size="8" type=text tabindex=1 onchange="UpdateTotal()" name="intCIRp" value="<%=intCIRp%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>Nickels:&nbsp;</td>
        <td align="left" class="control" nowrap><input maxlength="8" size="8" type=text tabindex=2 onchange="UpdateTotal()" name="intCIn" value="<%=intCIn%>"></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
 		<td align="right" class="control" nowrap>Nickels:&nbsp;</td>
       <td align="left" class="control" nowrap><input maxlength="8" size="8" type=text tabindex=1 onchange="UpdateTotal()" name="intCIRn" value="<%=intCIRn%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>Dimes:&nbsp;</td>
        <td align="left" class="control" nowrap><input maxlength="8" size="8" type=text tabindex=3 onchange="UpdateTotal()" name="intCId" value="<%=intCId%>"></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="right" class="control" nowrap>Dimes:&nbsp;</td>
        <td align="left" class="control" nowrap><input maxlength="8" size="8" type=text tabindex=1 onchange="UpdateTotal()" name="intCIRd" value="<%=intCIRd%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>Quarters:&nbsp;</td>
        <td align="left" class="control" nowrap><input maxlength="8" size="8" type=text tabindex=4 onchange="UpdateTotal()" name="intCIq" value="<%=intCIq%>"></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="right" class="control" nowrap>Quarters:&nbsp;</td>
        <td align="left" class="control" nowrap><input maxlength="8" size="8" type=text tabindex=1 onchange="UpdateTotal()" name="intCIRq" value="<%=intCIRq%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>Half Dollars:&nbsp;</td>
        <td align="left" class="control" nowrap><input maxlength="8" size="8" type=text tabindex=5 onchange="UpdateTotal()" name="intCIh" value="<%=intCIh%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="left" class="control" nowrap><b>Bills</B></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>1s:&nbsp;</td>
        <td align="left" class="control" nowrap><input maxlength="8" size="8" type=text tabindex=6 onchange="UpdateTotal()" name="intCI1" value="<%=intCI1%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>5s:&nbsp;</td>
        <td align="left" class="control" nowrap><input maxlength="8" size="8" type=text tabindex=7 onchange="UpdateTotal()" name="intCI5" value="<%=intCI5%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>10s:&nbsp;</td>
        <td align="left" class="control" nowrap><input maxlength="8" size="8" type=text tabindex=8 onchange="UpdateTotal()" name="intCI10" value="<%=intCI10%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>20s:&nbsp;</td>
        <td align="left" class="control" nowrap><input maxlength="8" size="8" type=text tabindex=9 onchange="UpdateTotal()" name="intCI20" value="<%=intCI20%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>50s:&nbsp;</td>
        <td align="left" class="control" nowrap><input maxlength="8" size="8" type=text tabindex=10 onchange="UpdateTotal()" name="intCI50" value="<%=intCI50%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>100s:&nbsp;</td>
        <td align="left" class="control" nowrap><input maxlength="8" size="8" type=text tabindex=11 onchange="UpdateTotal()" name="intCI100" value="<%=intCI100%>"></td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="left" class="control" nowrap>__________</td>
	</tr>
	<tr>
		<td align="right" class="control" nowrap>Total:&nbsp;</td>
		<td align="left" class="control" nowrap><b><label class="blkdata" ID="lblTotalCash" ></b></label></td>
	</tr>

</table>
</form>
</div>
</body>


<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">
Option Explicit
Dim obj,Rolltotal
Sub Window_OnLoad()	
	IF frmMain.strStatus.value="Yes" then
		parent.location.href="CashDrawerSetUp.asp?intDrawerNo="&document.all("intDrawerNo").value & "&LocationID="&document.all("LocationID").value &"&LoginID="&document.all("LoginID").value
	END IF
	Rolltotal = (((document.all("intCIRp").value*.01)*50) + ((document.all("intCIRn").value*.05)*40)) +((document.all("intCIRd").value*.1)*50) +((document.all("intCIRq").value*.25)*40)
	document.all("intTotalCash").value = Rolltotal+(document.all("intCIp").value*.01 + document.all("intCIn").value*.05 +document.all("intCId").value*.1 +document.all("intCIq").value*.25 +document.all("intCIh").value*.50 +document.all("intCI1").value +document.all("intCI5").value*5 +document.all("intCI10").value*10 +document.all("intCI20").value*20 +document.all("intCI50").value*50 +document.all("intCI100").value*100)
	document.all("lblTotalCash").innerText = formatcurrency(document.all("intTotalCash").value,2)
	document.all("lblDrawerNo").innerText = document.all("intDrawerNo").value
End Sub

Sub UpdateTotal()
	If not isnumeric(window.event.srcElement.value) Then
		window.event.srcElement.value = 0
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	window.event.srcElement.value = int(window.event.srcElement.value)
	Rolltotal = (((document.all("intCIRp").value*.01)*50) + ((document.all("intCIRn").value*.05)*40)) +((document.all("intCIRd").value*.1)*50) +((document.all("intCIRq").value*.25)*40)
	document.all("intTotalCash").value = Rolltotal+(document.all("intCIp").value*.01 + document.all("intCIn").value*.05 +document.all("intCId").value*.1 +document.all("intCIq").value*.25 +document.all("intCIh").value*.50 +document.all("intCI1").value +document.all("intCI5").value*5 +document.all("intCI10").value*10 +document.all("intCI20").value*20 +document.all("intCI50").value*50 +document.all("intCI100").value*100)
	document.all("lblTotalCash").innerText = formatcurrency(document.all("intTotalCash").value,2)
End Sub

</script>

<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Sub SaveData(dbMain,intDrawerNo)
	Dim rsData,strSQL,MaxSQL
	Dim intCashdID

	MaxSQL = " Select ISNULL(Max(CashdID),0)+1 from CashD (NOLOCK) Where LocationID=" & Request("LocationID")
	If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
		intCashdID=rsData(0)
	End if
	Set rsData = Nothing
	strSQL= "Insert into CashD (CashdID,LocationID,ndate,DrawerNo,CIUserID,CITime,CITotal,COTotal,"&_
		"CIp,CIn,CId,CIq,CIh,CI1,CI5,CI10,CI20,CI50,CI100,CIRp,CIRn,CIRd,CIRq,"&_
		"COp,COn,COd,COq,COh,CO1,CO5,CO10,CO20,CO50,CO100,"&_
		"CORp,CORn,CORd,CORq) Values ("&_
		intCashdID &","&_
		Request("LocationID") &","&_
		"'" & SQLReplace(Request("txtRptDate")) &"',"&_
		intDrawerNo &","&_
		Request("LoginID") &","&_
		"'" & Date() &"',"&_
		SQLReplace(Request("intTotalCash")) &",0,"&_
		SQLReplace(Request("intCIp")) &","&_
		SQLReplace(Request("intCIn")) &","&_
		SQLReplace(Request("intCId")) &","&_
		SQLReplace(Request("intCIq")) &","&_
		SQLReplace(Request("intCIh")) &","&_
		SQLReplace(Request("intCI1")) &","&_
		SQLReplace(Request("intCI5")) &","&_
		SQLReplace(Request("intCI10")) &","&_
		SQLReplace(Request("intCI20")) &","&_
		SQLReplace(Request("intCI50")) &","&_
		SQLReplace(Request("intCI100")) &","&_
		SQLReplace(Request("intCIRp")) &","&_
		SQLReplace(Request("intCIRn")) &","&_
		SQLReplace(Request("intCIRd")) &","&_
		SQLReplace(Request("intCIRq")) &","&_
		"0,0,0,0,0,0,0,0,0,0,0,0,0,0,0)"
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
        strSQL ="Update LM_Locations set CurrentStatus='Open' WHERE (LocationID = "& Request("LocationID") &")"
    if not DBExec(dbMain,strSQL) then
        response.write gstrmsg
        response.end
    end if
        strSQL ="Update stats set CurrentStatus='Open' WHERE (LocationID = "& Request("LocationID") &")"
    if not DBExec(dbMain,strSQL) then
        response.write gstrmsg
        response.end
    end if

End Sub


%>
