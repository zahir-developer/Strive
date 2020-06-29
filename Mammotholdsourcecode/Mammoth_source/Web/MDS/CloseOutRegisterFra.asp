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
	Dim dbMain,txtRptDate,rsData,strSQL,LocationID,LoginID
	Dim intCOp 
	dim intCOn,intCOd,intCOq,intCOh,intCO1,intCO5,intCO10,intCO20,intCO50,intCO100
	Dim intCORp,intCORn,intCORd,intCORq,intCashdID,intCoChecks,intCOCreditCards,intCOCreditCards2
	dim intTotalCash,strStatus,intDrawerNo,intCOPayouts,intCOCreditCards3,intCOCreditCards4
	intCOp =0
	intCOn =0
	intCOd =0
	intCOq =0
	intCOh =0
	intCO1 =0
	intCO5 =0
	intCO10 =0
	intCO20 =0
	intCO50 =0
	intCO100 =0
	intCORp =0
	intCORn =0
	intCORd =0
	intCORq =0
	



	Set dbMain = OpenConnection	
	
	txtRptDate =  Request("txtRptDate")
	intDrawerNo =  Request("intDrawerNo")
	intCashdID =  Request("intCashdID")
LocationID = Request("LocationID")
LoginID = Request("LoginID")
	Select case Request("FormAction")
		Case "btnSave"
			Call SaveData(dbMain,intCashdID)
			strStatus = "Yes"
	END SELECT
strSQL = " Select * from CashD (NOLOCK) where ndate='"&txtRptDate&"' and DrawerNo = "&intDrawerNo &" and LocationID=" & LocationID

	'Response.write strSQL

If dbOpenRecordSet(dbmain,rsData,strSQL) Then
	IF not rsData.EOF then
		intCashdID = rsData("CashdID") 
		intCOCreditCards = rsData("COCreditCards") 
		intCOCreditCards2 = rsData("COCreditCards2") 
		intCOCreditCards3 = rsData("COCreditCards3") 
		intCOCreditCards4 = rsData("COCreditCards4") 
		intCOPayouts = rsData("COPayouts") 
		intCOChecks = rsData("COChecks") 

	if isNull(rsData("COp")) then
	intCOp = 0
	else
	intCOp = rsData("COp")
	end if


		intCOn = rsData("COn")
		intCOd = rsData("COd")
		intCOq = rsData("COq")
		intCORp = rsData("CORp")
		intCORn = rsData("CORn")
		intCORd = rsData("CORd")
		intCORq = rsData("CORq")
		intCOh = rsData("COh")
		intCO1 = rsData("CO1")
		intCO5 = rsData("CO5")
		intCO10 = rsData("CO10")
		intCO20 = rsData("CO20")
		intCO50 = rsData("CO50")
		intCO100 = rsData("CO100")
	END IF
End if

IF isnull(intCOCreditCards) then
	intCOCreditCards = 0.0
END IF
IF isnull(intCOCreditCards2) then
	intCOCreditCards2 = 0.0
END IF
IF isnull(intCOCreditCards3) then
	intCOCreditCards3 = 0.0
END IF
IF isnull(intCOCreditCards4) then
	intCOCreditCards4 = 0.0
END IF
IF isnull(intCOPayouts) then
	intCOPayouts = 0.0
END IF
IF isnull(intCOChecks) then
	intCOChecks = 0.0
END IF

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
<form name="frmMain" Method="POST" action="CloseOutRegisterFra.Asp">
<input type=hidden name="FormAction" />
<Input type="hidden" name="LocationID" value="<%=LocationID%>" />
<Input type="hidden" name="LoginID" value="<%=LoginID%>" />
<input type="hidden" name="intCashdID" tabindex="-2" value="<%=intCashdID%>">
<input type="hidden" name="intTotalCash" tabindex="-2" value="<%=intTotalCash%>">
<input type="hidden" name="txtRptDate" tabindex="-2" value="<%=txtRptDate%>">
<input type="hidden" name="intDrawerNo" tabindex="-2" value="<%=intDrawerNo%>">
<input type="hidden" name="strStatus" tabindex="-2" value="<%=strStatus%>">
<table cellspacing="0" width="500">
	<tr>
		<td align="center" class="control" colspan=5  ><label class=control>Drawer #:&nbsp;
		<B><label class="blkdata" ID="lblDrawerNo" ></B></label></td>
	</tr>
	<tr>
		<td align="right" class="control"  >&nbsp;</td>
		<td align="left" class="control"  ><b>Coins</B></td>
		<td align="right" class="control"  >&nbsp;</td>
		<td align="right" class="control"  >&nbsp;</td>
		<td align="left" class="control"  ><b>Rolls</B></td>
	</tr>
	<tr>
		<td align="right" class="control"  >Pennies:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=1 onchange="UpdateTotal()" name="intCOp" value="<%=intCOp%>"></td>
		<td align="right" class="control"  >&nbsp;</td>
		<td align="right" class="control"  >Pennies:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=1 onchange="UpdateTotal()" name="intCORp" value="<%=intCORp%>"></td>
	</tr>
	<tr>
		<td align="right" class="control"  >Nickels:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=2 onchange="UpdateTotal()" name="intCOn" value="<%=intCOn%>"></td>
		<td align="right" class="control"  >&nbsp;</td>
 		<td align="right" class="control"  >Nickels:&nbsp;</td>
       <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=1 onchange="UpdateTotal()" name="intCORn" value="<%=intCORn%>"></td>
	</tr>
	<tr>
		<td align="right" class="control"  >Dimes:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=3 onchange="UpdateTotal()" name="intCOd" value="<%=intCOd%>"></td>
		<td align="right" class="control"  >&nbsp;</td>
		<td align="right" class="control"  >Dimes:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=1 onchange="UpdateTotal()" name="intCORd" value="<%=intCORd%>"></td>
	</tr>
	<tr>
		<td align="right" class="control"  >Quarters:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=4 onchange="UpdateTotal()" name="intCOq" value="<%=intCOq%>"></td>
		<td align="right" class="control"  >&nbsp;</td>
		<td align="right" class="control"  >Quarters:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=1 onchange="UpdateTotal()" name="intCORq" value="<%=intCORq%>"></td>
	</tr>
	<tr>
		<td align="right" class="control"  >Half Dollars:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=5 onchange="UpdateTotal()" name="intCOh" value="<%=intCOh%>"></td>
	</tr>
	<tr>
		<td align="right" class="control"  >&nbsp;</td>
		<td align="left" class="control" colspan=2  ><b>Bills</B></td>
	</tr>
	<tr>
		<td align="right" class="control"  >1s:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=6 onchange="UpdateTotal()" name="intCO1" value="<%=intCO1%>"></td>
		<td align="right" class="control"  >&nbsp;</td>
		<td align="right" class="control"  >Checks in Dollers:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=1  name="intCOChecks" value="<%=formatcurrency(intCOChecks,2)%>"></td>
	</tr>
	<tr>
		<td align="right" class="control"  >5s:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=7 onchange="UpdateTotal()" name="intCO5" value="<%=intCO5%>"></td>
		<td align="right" class="control"  >&nbsp;</td>
		<td align="right" class="control"  >BC in Dollers:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=1  name="intCOCreditCards" value="<%=formatcurrency(intCOCreditCards,2)%>"></td>
	</tr>
	<tr>
		<td align="right" class="control"  >10s:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=8 onchange="UpdateTotal()" name="intCO10" value="<%=intCO10%>"></td>
		<td align="right" class="control"  >&nbsp;</td>
		<td align="right" class="control"  >AMX in Dollers:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=1  name="intCOCreditCards2" value="<%=formatcurrency(intCOCreditCards2,2)%>"></td>
	</tr>
	<tr>
		<td align="right" class="control"  >20s:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=9 onchange="UpdateTotal()" name="intCO20" value="<%=intCO20%>"></td>
		<td align="right" class="control"  >&nbsp;</td>
		<td align="right" class="control"  >Dinners in Dollers:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=1  name="intCOCreditCards3" value="<%=formatcurrency(intCOCreditCards3,2)%>"></td>
	</tr>
	<tr>
		<td align="right" class="control"  >50s:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=10 onchange="UpdateTotal()" name="intCO50" value="<%=intCO50%>"></td>
		<td align="right" class="control"  >&nbsp;</td>
		<td align="right" class="control"  >Discover in Dollers:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=1  name="intCOCreditCards4" value="<%=formatcurrency(intCOCreditCards4,2)%>"></td>
	</tr>
	<tr>
		<td align="right" class="control"  >100s:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=11 onchange="UpdateTotal()" name="intCO100" value="<%=intCO100%>"></td>
		<td align="right" class="control"  >&nbsp;</td>
		<td align="right" class="control"  >Pay Outs:&nbsp;</td>
        <td align="left" class="control"  ><input maxlength="8" size="8" type=text tabindex=1  name="intCOPayouts" value="<%=formatcurrency(intCOPayouts,2)%>"></td>
	</tr>
	<tr>
		<td align="right" class="control"  >&nbsp;</td>
		<td align="left" class="control"  >__________</td>
	</tr>
	<tr>
		<td align="right" class="control"  >Total:&nbsp;</td>
		<td align="left" class="control"  ><b><label class="blkdata" ID="lblTotalCash" ></label></b></td>
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
<script type="text/VBSCRIPT">
Option Explicit
Dim obj,Rolltotal
Sub Window_OnLoad()	

	'msgbox frmMain.strStatus.value

	IF frmMain.strStatus.value="Yes" then
		parent.location.href="CloseOutRegister.asp?intDrawerNo="&document.all("intDrawerNo").value & "&LocationID="&document.all("LocationID").value &"&LoginID="&document.all("LoginID").value
	END IF

	'msgbox(document.all("intCOp").value)
	   
	Rolltotal = (((document.all("intCORp").value*.01)*50) + ((document.all("intCORn").value*.05)*40)) +((document.all("intCORd").value*.1)*50) +((document.all("intCORq").value*.25)*40)
	document.all("intTotalCash").value = Rolltotal+(document.all("intCOp").value*.01 + document.all("intCOn").value*.05 +document.all("intCOd").value*.1 +document.all("intCOq").value*.25 +document.all("intCOh").value*.50 +document.all("intCO1").value +document.all("intCO5").value*5 +document.all("intCO10").value*10 +document.all("intCO20").value*20 +document.all("intCO50").value*50 +document.all("intCO100").value*100)
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
	Rolltotal = (((document.all("intCORp").value*.01)*50) + ((document.all("intCORn").value*.05)*40)) +((document.all("intCORd").value*.1)*50) +((document.all("intCORq").value*.25)*40)
	document.all("intTotalCash").value = Rolltotal+(document.all("intCOp").value*.01 + document.all("intCOn").value*.05 +document.all("intCOd").value*.1 +document.all("intCOq").value*.25 +document.all("intCOh").value*.50 +document.all("intCO1").value +document.all("intCO5").value*5 +document.all("intCO10").value*10 +document.all("intCO20").value*20 +document.all("intCO50").value*50 +document.all("intCO100").value*100)
	document.all("lblTotalCash").innerText = formatcurrency(document.all("intTotalCash").value,2)
End Sub

</script>

<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Sub SaveData(dbMain,intCashdID)
	Dim rsData,strSQL

	strSQL= "Update CashD Set "&_
		" COUserID=" & Request("LoginID") &","&_
		" COTime=" & "'" & Date() &"',"&_
		" COTotal=" & SQLReplace(Request("intTotalCash")) &","&_
		" COChecks=" & SQLReplace(Request("intCOChecks")) &","&_
		" COCreditCards=" & SQLReplace(Request("intCOCreditCards")) &","&_
		" COCreditCards2=" & SQLReplace(Request("intCOCreditCards2")) &","&_
		" COCreditCards3=" & SQLReplace(Request("intCOCreditCards3")) &","&_
		" COCreditCards4=" & SQLReplace(Request("intCOCreditCards4")) &","&_
		" COPayouts=" & SQLReplace(Request("intCOPayouts")) &","&_
		" COp=" & SQLReplace(Request("intCOp")) &","&_
		" COn=" & SQLReplace(Request("intCOn")) &","&_
		" COd=" & SQLReplace(Request("intCOd")) &","&_
		" COq=" & SQLReplace(Request("intCOq")) &","&_
		" CORp=" & SQLReplace(Request("intCORp")) &","&_
		" CORn=" & SQLReplace(Request("intCORn")) &","&_
		" CORd=" & SQLReplace(Request("intCORd")) &","&_
		" CORq=" & SQLReplace(Request("intCORq")) &","&_
		" COh=" & SQLReplace(Request("intCOh")) &","&_
		" CO1=" & SQLReplace(Request("intCO1")) &","&_
		" CO5=" & SQLReplace(Request("intCO5")) &","&_
		" CO10=" & SQLReplace(Request("intCO10")) &","&_
		" CO20=" & SQLReplace(Request("intCO20")) &","&_
		" CO50=" & SQLReplace(Request("intCO50")) &","&_
		" CO100=" & SQLReplace(Request("intCO100")) &_
		"Where CashdID = " & intCashdID &" AND LocationID="& Request("LocationID")
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End


	END IF

        strSQL ="Update LM_Locations set CurrentStatus='Closed' WHERE (LocationID = "& Request("LocationID") &")"
    if not DBExec(dbMain,strSQL) then
        response.write gstrmsg
        response.end
    end if
        strSQL ="Update stats set CurrentStatus='Closed' WHERE (LocationID = "& Request("LocationID") &")"
    if not DBExec(dbMain,strSQL) then
        response.write gstrmsg
        response.end
    end if


End Sub


%>
