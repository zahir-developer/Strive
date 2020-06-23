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
Dim Title
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

dim dbMain,intUserID,strLastname,strFirstName,strSQL, RS,strCdate,intCstat,intCtype,LocationID
dim hdnTimeChange,strCtype,strSQL2, RS2
Set dbMain =  OpenConnection
LocationID = Request("LocationID")
intUserID = request("UserID")
hdnTimeChange = request("hdnTimeChange")
IF len(hdnTimeChange) = 0 then
	hdnTimeChange = 0
END IF
Select Case Request("FormAction")
	Case "btnCancel"
		Response.Redirect "Timelogin.asp?LocationID="& LocationID
	Case "btnSetTime"
		Call SetTime(dbMain,intUserID)
End Select

strSQL = "SELECT Lastname,FirstName FROM LM_Users WHERE userid=" & intuserid & " and LocationID="& LocationID
If DBOpenRecordset(dbMain,rs,strSQL) Then
	If Not RS.EOF Then
		strLastname = Trim(RS("Lastname")) 
		strFirstName = Trim(RS("FirstName")) 
	End If
End If
strSQL = "SELECT Lastname,FirstName FROM LM_Users WHERE userid=" & intuserid & " and LocationID="& LocationID
If DBOpenRecordset(dbMain,rs,strSQL) Then
	If Not RS.EOF Then
		strLastname = Trim(RS("Lastname")) 
		strFirstName = Trim(RS("FirstName")) 
	End If
End If
strCdate = now()

strSQL = "SELECT Caction,CType FROM timeClock WHERE userid=" & intuserid &_
		" and DatePart(Month,CDatetime) = '"& Month(Date()) &"'"&_
		" and DatePart(Day,CDatetime) = '"& Day(Date()) &"'"&_
		" and DatePart(Year,CDatetime) = '"& Year(Date()) &"'"&_
        " and LocationID="& LocationID &_
		" Order by CDatetime desc"
		'Response.Write strSQL 
		'Response.End
If DBOpenRecordset(dbMain,rs,strSQL) Then
	If Not RS.EOF Then
		IF ISNULL(RS("CType")) then
			strCtype = "Unknown"
		ELSE
			intCtype =RS("CType")
			strSQL2 = "SELECT listDesc FROM LM_ListItem WHERE listtype = 6 AND listvalue =" & intCtype
			If DBOpenRecordset(dbMain,rs2,strSQL2) Then
				If Not RS2.EOF Then
					strCtype = RS2("listDesc")
				END IF
			END IF
			set RS2 = nothing
		END IF

		IF RS("Caction") = 1 then
			intCstat = 0
		ELSE
			intCstat = 1
		END IF
	ELSE
		intCstat = 0
		strSQL2 = "SELECT LM_Users.RoleID, LM_ListItem.ListDesc"&_
				" FROM LM_Users INNER JOIN LM_ListItem ON LM_Users.RoleID = LM_ListItem.ListValue"&_
				" WHERE (LM_ListItem.ListType = 6) AND LM_Users.UserID = "& intuserid & " and LM_Users.LocationID="& LocationID
		If DBOpenRecordset(dbMain,rs2,strSQL2) Then
			If Not RS2.EOF Then
				intCtype = RS2("RoleID")
				strCtype = RS2("listDesc")
			END IF
		END IF
		set RS2 = nothing
	End If
ELSE
	intCstat = 0
	strSQL2 = "SELECT LM_Users.RoleID, LM_ListItem.ListDesc"&_
			" FROM LM_Users INNER JOIN LM_ListItem ON LM_Users.RoleID = LM_ListItem.ListValue"&_
			" WHERE (LM_ListItem.ListType = 6) AND LM_Users.UserID = "&intuserid & " and LM_Users.LocationID="& LocationID
	If DBOpenRecordset(dbMain,rs2,strSQL2) Then
		If Not RS2.EOF Then
			intCtype = RS2("RoleID")
			strCtype = RS2("listDesc")
		END IF
	END IF
	set RS2 = nothing
End If

'********************************************************************
' HTML
'********************************************************************
Title = "Time Set"
%>

<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
</head>
<body class="pgBody">
<form method="POST" name="frmMain" action="TimeSet.asp?userid=<%=intUserID%>&LocationID=<%=LocationID%>"> 
<input type=hidden name=FormAction value=>
<Input type="hidden" name="LocationID" value="<%=LocationID%>" />
<input type=hidden name=intUserID value="<%=intUserID%>">
<input type=hidden name=hdnTimeChange value="<%=hdnTimeChange%>">
<input type=hidden name=strLastname value="<%=strLastname%>">
<input type=hidden name=strFirstName value="<%=strFirstName%>">
<input type=hidden name=strCdate value="<%=strCdate%>">
<input type=hidden name=intCstat value="<%=intCstat%>">
<input type=hidden name=intCtype value="<%=intCtype%>">
<input type=hidden name=strCtype value="<%=strCtype%>">
<div style="text-align:center">
<table>
<tr>
<td>
	<table BORDER=0 WIDTH=100 height=100% CELLPADDING=0 CELLSPACING=0>
	<tr>
		<td align=right>&nbsp;</td>
	</tr>
	<tr>
		<td align=right>&nbsp;</td>
	</tr>
	<tr>
		<td align=right>			
			<button  name="btnSetTime" align="right" style="height:60;width:120;font:bold 18px arial" style="width:100">Clock In/Out</button>
		</td>
	</tr>
	<tr>
		<td align=right>&nbsp;</td>
	</tr>
	<tr>
		<td align=right>&nbsp;</td>
	</tr>
	<tr>
		<td align=right>			
			<button  name="btnShowCard" align="right" style="height:60;width:120;font:bold 18px arial" style="width:100" >Time Card</button>
		</td>
	</tr>
	<% IF intCtype = 9 then %>
	<tr>
		<td align=right>&nbsp;</td>
	</tr>
	<tr>
		<td align=right>&nbsp;</td>
	</tr>
	<tr>
		<td align=right>			
			<button  name="btnDetail" align="right" style="height:60;width:120;font:bold 18px arial" style="width:100">Detail</button>
		</td>
	</tr>
	<% END IF %>
	<tr>
		<td align=right>&nbsp;</td>
	</tr>
	<tr>
		<td align=right>&nbsp;</td>
	</tr>
	<tr>
		<td align=right>			
			<button  name="btncancel" align="right" style="height:60;width:120;font:bold 18px arial" style="width:100">Return</button>
		</td>
	</tr>

	</table>
</td>
<td>
	<table BORDER=0 WIDTH=600 height=100% CELLPADDING=0 CELLSPACING=0>
		<tr>
			<td align=right>&nbsp;</td>
		</tr>
		<tr>
			<td align="right" class="control" nowrap style="font:bold 18px arial">Current User:&nbsp;</td>
        <td align="left" class="control" nowrap><Label class=control style="font:bold 18px arial"><b><%=strFirstName%>&nbsp;<%=strLastName%></b></label></td>
		</tr>
		<tr>
			<td align="right" class="control" style="font:bold 18px arial" nowrap>Current Status:&nbsp;</td>
			<% IF intCstat = 0 then %>
			<td align="left" class="control" style="font:bold 18px arial" nowrap><Label class=control style="font:bold 18px arial" ><b>Clocked Out</b></label></td>
			<%Else%>
			<td align="left" class="control" style="font:bold 18px arial" nowrap><Label class=control style="font:bold 18px arial" ><b>Clocked In</b>&nbsp;As:&nbsp;<b><%=strCtype%></b></label></td>
			<%End If%>
			
		</tr>
		<tr>
			<td align="right" class="control" style="font:bold 18px arial" nowrap>Current Date/Time:&nbsp;</td>
        <td align="left" class="control" style="font:bold 18px arial" nowrap><Label class=control style="font:bold 18px arial" ><b><%=strCdate%></b></label></td>
		</tr>
		<tr>
			<td align=right>&nbsp;</td>
		</tr>
	</table>
<iframe Name="fraMain" src="admLoading.asp" scrolling=no height="275" width="700" frameborder="0"></iframe>
</td>
</tr>
</table>
    </div>
</form>
</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">
Option Explicit

Sub Window_Onload()
	IF frmMain.hdnTimeChange.value=1 then
		fraMain.location.href = "TimeCardFra.asp?UserID="& document.all("intUserID").value &"&LocationID="& document.all("LocationID").value
	ELSE
		fraMain.location.href = "TimeSetFra.asp?UserID="& document.all("intUserID").value &"&Cstat="& document.all("intCstat").value &"&CType="& document.all("intCType").value  &"&LocationID="& document.all("LocationID").value
	END IF
End Sub

Sub btncancel_OnClick()
	frmMain.FormAction.value="btnCancel"
	frmMain.submit()
End Sub

Sub btnShowCard_OnClick()
		fraMain.location.href = "TimeCardFra.asp?UserID="& document.all("intUserID").value &"&LocationID="& document.all("LocationID").value
End Sub
Sub btnSetTime_OnClick()
		fraMain.location.href = "TimeSetFra.asp?UserID="& document.all("intUserID").value &"&Cstat="& document.all("intCstat").value &"&CType="& document.all("intCType").value  &"&LocationID="& document.all("LocationID").value
End Sub
Sub btnDetail_OnClick()
		fraMain.location.href = "TimeDetailFra.asp?UserID="& document.all("intUserID").value &"&LocationID="& document.all("LocationID").value
End Sub

</script>


<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Sub SetTime(dbMain,intUserID)
	Dim strSQL,rsData,MaxSQL
	Dim intClockID

'Response.Write Request("intuserid")&"<BR>"
'Response.Write Request("strCTime")&"<BR>"
'Response.Write Request("strCdate")&"<BR>"
'Response.Write Request("intCstat")&"<BR>"

		MaxSQL = " Select ISNULL(Max(ClockID),0)+1 from timeclock (NOLOCK) where LocationID ="& Request("LocationID")
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			intClockID=rsData(0)
		End if
		Set rsData = Nothing
		strSQL=" Insert into timeclock(ClockID,UserID,LocationID,Cdatetime,CAction,Ctype,EditBy,EditDate,Paid)Values(" & _
					intClockID & ", " & _
					intUserID & ", " & _
					Request("LocationID") & ", " & _
					"'" & Request("strCdate") & "', " & _
					Request("intCstat") & ", " & _
					Request("intCType") & ", " & _
					intUserID & ", " & _
					"'" & Date() & "',0)"
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
	strSQL=" exec stp_Washers " &  Request("LocationID") 
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write strSQL
		Response.End
	END IF
	strSQL=" exec stp_Labor " &  Request("LocationID") 
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write strSQL
		Response.End
	END IF





	Response.Redirect "TimeSet.asp?userid="& intUserID &"&hdnTimeChange=" & 1 & "&LocationID="&Request("LocationID") 
End Sub


%>
