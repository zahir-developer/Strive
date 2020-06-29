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
Dim intAssigned

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

	Dim dbMain, intrecID,DetailCompID,rsData,strSQL,strComm,strComAmt,strComPer,intFirstpass

	Set dbMain =  OpenConnection
	intrecID = request("intrecID")
	if len(request("intFirstpass"))>0 then
		intFirstpass = 1
	ELSE
		intFirstpass = 0 
	END IF


	DetailCompID = request("DetailCompID")
	Select case Request("FormAction")
		Case "DeleteUser"
			Call DeleteUser(dbMain)
		Case "Assign"
			Call Assign(dbMain)
		Case "CancelChg"
			Call CancelChg(dbMain)
		Case "Complete"
			Call CompleteChg(dbMain)
	End select

	strSQL = "Select count(*) From DetailComp(nolock) where RecID="&intrecID
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF NOT rsData.eof then
			intAssigned = rsData(0)
	
			IF intAssigned = 1 then

				
				
				strComPer = 100/intAssigned

				strSQL = "SELECT SUM(comm) as sumComm FROM RECITEM(nolock) WHERE recid =" & intrecID
				If DBOpenRecordset(dbMain,rsData,strSQL) Then
					IF not rsData.eof then
					strComm = rsData("sumComm")
					ELse
					strComm = 0.0
					END IF
				End If
				strComAmt = strComm * strComPer*.01
				strSQL= "	UPDATE DetailComp Set " & _
						"	Comm=" & strComm  &"," & _
						"	ComAmt=" & strComAmt &"," & _
						"	ComPer=" & strComPer & _
						"	WHERE recID=" & intrecID
				If NOT (dbExec(dbMain,strSQL)) Then
					Response.Write gstrMsg
				End If
			END IF
		ELSE
			intAssigned = 0
		END IF
	End If



%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="TimeDetailByDlgFra.asp">
<div align="center">
<input type="hidden" name="intRECID" value="<%=intRECID%>">
<input type="hidden" name="DetailCompID" value="<%=DetailCompID%>">
<input type="hidden" name="intAssigned" value="<%=intAssigned%>">
<input type="hidden" name="intFirstpass" value="<%=intFirstpass%>">
<table border="0" width="600" cellspacing="0" cellpadding="0">
	<tr>
	<td align=Center>			
		<select tabindex=2 style="font:bold 28px arial" name="cboUserID">
			<% Call LoadName(dbMain,intrecID) %>				
		</select>&nbsp;&nbsp;
			<button  name="btnAddName" align="right" style="height:60;width:120;font:bold 18px arial" OnClick="Assign()">Assign</button>
		</td>
	</tr>
</table>
<br>
<table border="0" width="600" cellspacing="0" cellpadding="0">
	<tr>
	<td>
		<iframe Name="fraDetailComp" src="admLoading.asp" scrolling=yes height="260" width="600" frameborder="0"></iframe>
	</td>
	</tr>
</table>
<br>
<table align="center" border="0" width="600" cellspacing="1" cellpadding="1">
   <tr>
      <td align="center" colspan="3">
	  <button name="btnComplete" class="button" style="height:60;width:120;font:bold 18px arial" OnClick="CompleteChg()">Complete</button>&nbsp;&nbsp;&nbsp;&nbsp;
	  <button name="btnSave" class="button" style="height:60;width:120;font:bold 18px arial" OnClick="SaveChg()">Save</button>&nbsp;&nbsp;&nbsp;&nbsp;
	  <button name="btnCancel" class="button" style="height:60;width:120;font:bold 18px arial" OnClick="CancelChg()">Cancel</button>
		</td>
	</tr>		
</table>

</div>
<input type="hidden" name="FormAction" value="">
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
	IF frmMain.intAssigned.Value > 1 and frmMain.intFirstPass.Value > 0 then
		Dim Multi
		Multi= ShowModalDialog("TimeMultiDetailByDlg.asp?intRecID="& frmMain.intRecID.value ,"","dialogwidth:650px;dialogheight:500px;")
	END IF
	fraDetailComp.location.href = "TimeNewDetailByList.asp?intRecID=" & frmMain.intRecID.Value
End Sub


Sub Assign()
		IF LEN(TRIM(frmMain.cboUserID.value)) > 0 then
		frmMain.FormAction.value="Assign"
		frmMain.intFirstpass.value=1
		frmMain.submit()
		END IF
End Sub

Sub SaveChg()
		IF frmMain.intAssigned.value > 0 then
		parent.document.ALL("hdnStatus").value = true
		ELSE
		parent.document.ALL("hdnStatus").value = False
		END IF
		parent.window.close
End Sub
Sub CompleteChg()
		frmMain.FormAction.value="Complete"
		frmMain.submit()
End Sub


Sub CancelChg()
	Dim Answer
	window.event.cancelBubble = false
	Answer = MsgBox("Are you sure you want to remove ALL Employees ?",276,"Confirm Cancel")
	If Answer = 6 then
		frmMain.FormAction.value="CancelChg"
		frmMain.submit()
		parent.document.ALL("hdnStatus").value = False
		parent.window.close

	Else
		Exit Sub
	End if
	parent.document.ALL("hdnStatus").value = False
End Sub

</script>

<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Sub DeleteUser(dbMain)
	Dim intrecID,strSQL,DetailCompID
	intrecID = request("intrecID")
	DetailCompID = request("DetailCompID")
	strSQL= "	Delete DetailComp WHERE DetailCompID=" & DetailCompID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
		Response.end
	End If
END SUB

Sub CancelChg(dbMain)
	Dim intrecID,strSQL
	intrecID = request("intrecID")
	strSQL= "	Delete DetailComp WHERE recID=" & intrecID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
		Response.end
	End If
END SUB

Sub CompleteChg(dbMain)
	Dim intrecID,strSQL
	intrecID = request("intrecID")
	strSQL= "Update REC SET Status=20 WHERE recID=" & intrecID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
		Response.end
	End If
END SUB

Sub Assign(dbMain)
	Dim intrecID,strSQL,rsData,intUserID,strComm,intDetailCompID,dtCDateTime
	intrecID = request("intrecID")
	intUserID = request("cboUserID")
		strSQL = "Select cDateTime From DetailComp where recID="&intrecID
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			IF not rsData.eof then
				dtCDateTime = rsData("CDateTime")
			End If
		End If
		Set rsData = Nothing

		strSQL = "Select DetailCompID=IsNull(Max(DetailCompID),0) + 1 From DetailComp"
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			intDetailCompID = rsData("DetailCompID")
		End If
		strSQL = "SELECT SUM(comm) as sumComm FROM RECITEM(nolock) WHERE recid =" & intrecID
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			IF not rsData.eof then
			strComm = rsData("sumComm")
			ELse
			strComm = 0.0
			END IF
		End If
		Set rsData = Nothing
		IF isnull(dtCDateTime) then
			strSQL= " Insert into DetailComp(DetailCompID,recID,UserID,Paid,Comm)Values(" & _
					intDetailCompID & ", " & _
					intrecID & ", " & _
					intUserID & ", " & _
					"0, " & _
					strComm & ") " 
			If NOT (dbExec(dbMain,strSQL)) Then
				Response.Write gstrMsg
			End If
		ELSE
			strSQL= " Insert into DetailComp(DetailCompID,CDateTime,recID,UserID,Paid,Comm)Values(" & _
					intDetailCompID & ", " & _
					"'" & dtCDateTime & "', " & _
					intrecID & ", " & _
					intUserID & ", " & _
					"0, " & _
					strComm & ") " 
			If NOT (dbExec(dbMain,strSQL)) Then
				Response.Write gstrMsg
			End If
		END IF
END SUB

Function LoadName(db,hdnRecID)
	Dim strSQL,RS
	IF isnull(hdnRecID) or len(ltrim(hdnRecID))=0 then
		hdnRecID = 0
	END IF
	strSQL= " SELECT TimeClock.UserID, LM_Users.FirstName,LM_Users.LastName"&_
		" FROM TimeClock(NOLOCK) INNER JOIN LM_Users(NOLOCK) ON TimeClock.UserID = LM_Users.UserID"&_
		" WHERE (CONVERT(char, TimeClock.Cdatetime, 101) = CONVERT(char, GETDATE(), 101))"&_
		" AND TimeClock.CType = 9 AND TimeClock.Caction = 0"&_
		" AND LM_Users.userID NOT IN (SELECT userid FROM DetailComp"&_
		" WHERE DetailComp.RecID = "& hdnRecID &")"
		%>
		<option Value=""></option>
		<%
	If dbOpenRecordSet(db,rs,strSQL) Then
		Do While Not RS.EOF
						%>
						<option Value="<%=RS("UserID")%>"><%=RS("FirstName")%>&nbsp; <%=RS("LastName")%></option>
						<%
			RS.MoveNext
		Loop
	End If
End Function

%>
