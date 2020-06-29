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
	dim intTotalCash,strStatus1,strStatus2,strStatus3,intDrawerNo,LocationID,LoginID
	Set dbMain = OpenConnection	
IF not isdate(txtRptDate) then
	txtRptDate = formatdatetime(NOW(),vbShortDate)
END IF
intDrawerNo = request("intDrawerNo")
LocationID = Request("LocationID")
LoginID = Request("LoginID")

IF LEN(TRIM(intDrawerNo))=0 then
	intDrawerNo = 1
END IF
strSQL = " Select COUserID from CashD (NOLOCK) where ndate='"&txtRptDate&"' and DrawerNo = 1 and LocationID=" & LocationID
If dbOpenRecordSet(dbmain,rsData,strSQL) Then
	IF rsData.EOF then
		strStatus1="Yes"
	ELSE
		IF isnull(rsData("COUserID")) then
			strStatus1="No"
		ELSE
			strStatus1="Yes"
		END IF
	END IF
End if
'strSQL = " Select COUserID from CashD (NOLOCK) where ndate='"&txtRptDate&"' and DrawerNo = 2 and LocationID=" & LocationID
'If dbOpenRecordSet(dbmain,rsData,strSQL) Then
'	IF rsData.EOF then
'		strStatus2="Yes"
'	ELSE
'		IF isnull(rsData("COUserID")) then
'			strStatus2="No"
'		ELSE
'			strStatus2="Yes"
'		END IF
'	END IF
'End if
'strSQL = " Select COUserID from CashD (NOLOCK) where ndate='"&txtRptDate&"' and DrawerNo = 3 and LocationID=" & LocationID
'If dbOpenRecordSet(dbmain,rsData,strSQL) Then
'	IF rsData.EOF then
'		strStatus3="Yes"
'	ELSE
'		IF isnull(rsData("COUserID")) then
'			strStatus3="No"
'		ELSE
'			strStatus3="Yes"
'		END IF
'	END IF
'End if

'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<title></title>
<link REL="stylesheet" HREF="main.css" TYPE="text/css" />
</head>
<body class="pgBody">
    <form name="frmMain" Method="post" action="CloseOutRegister.asp">
<div style="text-align:center">
    <input type=hidden name="FormAction" />
<Input type="hidden" name="LocationID" value="<%=LocationID%>" />
<Input type="hidden" name="LoginID" value="<%=LoginID%>" />
<input type="hidden" name="intDrawerNo" tabindex="-2" value="<%=intDrawerNo%>"/>
<input type="hidden" name="txtRptDate" tabindex="-2" value="<%=txtRptDate%>"/>
<input type="hidden" name="strStatus1" tabindex="-2" value="<%=strStatus1%>"/>
<input type="hidden" name="strStatus2" tabindex="-2" value="<%=strStatus2%>"/>
<input type="hidden" name="strStatus3" tabindex="-2" value="<%=strStatus3%>"/>
<table  style="width: 786px; border-collapse: collapse;" class="tblcaption">
	<tr>
		<td style="text-align: left; white-space: nowrap; background-image:url(images/header.jpg); width:200px;">Close Out Register</td>
		<td style="text-align: right; white-space: nowrap;">&nbsp;</td>
	</tr>
</table>
<table style="width: 786px; border-collapse: collapse;">
	<tr>
		<td style="text-align: right; white-space: nowrap;" class="control" ><label class=control>Date:&nbsp;</label></td>
		<td style="text-align: left; white-space: nowrap;" class="control" ><B><label class="blkdata" ID="lblRptDate" ></label></B></td>
		<td style="text-align: right; white-space: nowrap;">&nbsp;
		<button name="btnSave" style="text-align: center; white-space: nowrap; width:75px;" class="buttondead" OnClick="SubmitForm()" value="yes">Save</button>
		</td>
	</tr>
</table>
<br />
<!--<table width=600 cellspacing=0>	
	<tr>
		<td width=15 class=tabgap>&nbsp;</td>
		<td class=tabselect align=middle nowrap><a Name=Drawer1 href="#1" class=control onclick="ChangeTab()">Drawer #1</a>
		<% IF strStatus1 = "No" then %>
			<img id="piclogo" src="images/imgOK.gif" WIDTH="12" HEIGHT="12"></td>
		<% Else %>
			<img id="piclogo" src="images/imgCancel.gif" WIDTH="12" HEIGHT="12"></td>
		<% End IF %>		
		<td class=tabgap>&nbsp;</td>
		<td class=tab align=middle nowrap><a Name=Drawer2 href="#1" class=control onclick="ChangeTab()">Drawer #2</a>
		<% IF strStatus2 = "No" then %>
			<img id="piclogo" src="images/imgOK.gif" WIDTH="12" HEIGHT="12"></td>
		<% Else %>
			<img id="piclogo" src="images/imgCancel.gif" WIDTH="12" HEIGHT="12"></td>
		<% End IF %>		
		<td class=tabgap>&nbsp;</td>
		<td class=tab align=middle nowrap><a Name=Drawer3 href="#1" class=control onclick="ChangeTab()">Drawer #3</a>
		<% IF strStatus3 = "No" then %>
			<img id="piclogo" src="images/imgOK.gif" WIDTH="12" HEIGHT="12"></td>
		<% Else %>
			<img id="piclogo" src="images/imgCancel.gif" WIDTH="12" HEIGHT="12"></td>
		<% End IF %>		
		<td class=tabgap>&nbsp;</td>
		<td width=300 class=tabgap>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
		<td class=tabgap>&nbsp;</td>
		<td align="right">&nbsp;
		<button name="btnReport" align="right" class="button" OnClick="SubmitForm()" value="yes">Report</button>&nbsp;&nbsp;&nbsp;
		<button name="btnSave" align="right" class="buttondead" OnClick="SubmitForm()" value="yes">Save</button>
		</td>
	</tr>
</table>-->
<iframe Name="fraMain2" src="admLoading.asp" scrolling="no" style="height:430px; width:500px; " frameborder="0"></iframe>

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
Dim obj
Sub Window_OnLoad()	
		document.all("lblRptDate").innerText = document.all("txtRptDate").value

'	Select Case document.all("intDrawerNo").value
'		Case 1
'			Drawer1.ParentElement.className = "tabSelect"	
'			Drawer2.ParentElement.className = "tab"	
'			Drawer3.ParentElement.className = "tab"	
'			fraMain2.location.href = "CloseOutRegisterFra.asp?intDrawerNo=1&txtRptDate="&document.all("txtRptDate").value & "&LocationID="&document.all("LocationID").value &"&LoginID="&document.all("LoginID").value
'			IF document.all("strStatus1").value = "No" then
'				frmMain.btnSave.ClassName = "Button"
'			ELSE
'				frmMain.btnSave.ClassName = "ButtonDead"
'			END IF
'		Case 2
'			Drawer1.ParentElement.className = "tab"	
'			Drawer2.ParentElement.className = "tabSelect"	
'			Drawer3.ParentElement.className = "tab"	
'			fraMain2.location.href = "CloseOutRegisterFra.asp?intDrawerNo=2&txtRptDate="&document.all("txtRptDate").value & "&LocationID="&document.all("LocationID").value &"&LoginID="&document.all("LoginID").value
'			IF document.all("strStatus2").value = "No" then
'				frmMain.btnSave.ClassName = "Button"
'			ELSE
'				frmMain.btnSave.ClassName = "ButtonDead"
'			END IF
'		Case 3
'			Drawer1.ParentElement.className = "tab"	
'			Drawer2.ParentElement.className = "tab"	
'			Drawer3.ParentElement.className = "tabSelect"	
'			fraMain2.location.href = "CloseOutRegisterFra.asp?intDrawerNo=3&txtRptDate="&document.all("txtRptDate").value & "&LocationID="&document.all("LocationID").value &"&LoginID="&document.all("LoginID").value
'			IF document.all("strStatus3").value = "No" then
'				frmMain.btnSave.ClassName = "Button"
'			ELSE
'				frmMain.btnSave.ClassName = "ButtonDead"
'			END IF
'	End Select

	fraMain2.location.href = "CloseOutRegisterFra.asp?intDrawerNo=1&txtRptDate="&document.all("txtRptDate").value & "&LocationID="&document.all("LocationID").value &"&LoginID="&document.all("LoginID").value
	IF document.all("strStatus1").value = "No" then
		frmMain.btnSave.ClassName = "Button"
	ELSE
		frmMain.btnSave.ClassName = "ButtonDead"
	END IF



End Sub

Sub SubmitForm()
	dim Answer
	window.event.CancelBubble=True
	window.event.ReturnValue=False

	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	Select Case window.event.srcElement.name
		Case "btnSave"
			Answer = MsgBox("Are you sure the Cash Drawer is Correct?",276,"Confirm Save")
			If Answer = 6 then
			fraMain2.frmMain.FormAction.value="btnSave"
			fraMain2.frmMain.submit
			Else
				Exit Sub
			End if
		Case "btnReport"
			location.href="admDisplayReport.asp?rpt=Reports/EOD.rpt&@RptDate=" & document.all("txtRptDate").value & "&LocationID="&document.all("LocationID").value &"&LoginID="&document.all("LoginID").value
 		Case "btnDone"
			location.href="admWelcome.asp"
 	End Select
End Sub

Sub ChangeTab
	If UCase(window.event.srcElement.ClassName) = "LNKDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	If UCase(window.event.srcElement.parentElement.className) = "TABSELECT" Then  exit sub
	Select Case window.event.srcElement.name
		Case "Drawer1"
			Drawer1.ParentElement.className = "tabSelect"	
			Drawer2.ParentElement.className = "tab"	
			Drawer3.ParentElement.className = "tab"	
			fraMain2.location.href = "CloseOutRegisterFra.asp?intDrawerNo=1&txtRptDate="&document.all("txtRptDate").value & "&LocationID="&document.all("LocationID").value &"&LoginID="&document.all("LoginID").value
			IF document.all("strStatus1").value = "No" then
				frmMain.btnSave.ClassName = "Button"
			ELSE
				frmMain.btnSave.ClassName = "ButtonDead"
			END IF
		Case "Drawer2"
			Drawer1.ParentElement.className = "tab"	
			Drawer2.ParentElement.className = "tabSelect"	
			Drawer3.ParentElement.className = "tab"	
			fraMain2.location.href = "CloseOutRegisterFra.asp?intDrawerNo=2&txtRptDate="&document.all("txtRptDate").value & "&LocationID="&document.all("LocationID").value &"&LoginID="&document.all("LoginID").value
			IF document.all("strStatus2").value = "No" then
				frmMain.btnSave.ClassName = "Button"
			ELSE
				frmMain.btnSave.ClassName = "ButtonDead"
			END IF
		Case "Drawer3"
			Drawer1.ParentElement.className = "tab"	
			Drawer2.ParentElement.className = "tab"	
			Drawer3.ParentElement.className = "tabSelect"	
			fraMain2.location.href = "CloseOutRegisterFra.asp?intDrawerNo=3&txtRptDate="&document.all("txtRptDate").value & "&LocationID="&document.all("LocationID").value &"&LoginID="&document.all("LoginID").value
			IF document.all("strStatus3").value = "No" then
				frmMain.btnSave.ClassName = "Button"
			ELSE
				frmMain.btnSave.ClassName = "ButtonDead"
			END IF
	End Select
	window.focus
End Sub

</script>

<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************

%>
