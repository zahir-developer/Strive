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

Dim dbMain,strSQL2,rs2, intUserID,intCollID,intactID, dtActDate,intActType,strActAmt,strActDesc,strBalance,LocationID,LoginID

Set dbMain =  OpenConnection

intUserID=Request("intUserID")
intCollID=Request("intCollID")
LocationID = request("LocationID")
LoginID = request("LoginID")

Select Case Request("FormAction")
	Case "btnSave"
		Call UpdateColl(dbMain,intUserID,intCollID,LocationID,LoginID)
	Case "btnDelete"
		Call DeleteColl(dbMain,intUserID,intCollID,LocationID,LoginID)
End Select


strSQL2 =" SELECT sum(actamt) as Bal From UserCol (Nolock) where UserID=" & intUserID &" and LocationID=" & LocationID  &" and CollID="& intCollID &" group by UserID,CollID"

IF dbOpenStaticRecordset(dbMain, rs2, strSQL2) then   
	IF NOT 	rs2.EOF then
	strBalance = rs2("Bal")
	END IF
END IF



%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="admEditCollFra.asp">
<div style="text-align:center">
<input type="hidden" name="FormAction" tabindex="-2" value>
<input type="hidden" name="intUserID" tabindex="-2" value="<%=intUserID%>">
<input type="hidden" name="intCollID" tabindex="-2" value="<%=intCollID%>">
<input type="hidden" name="strBalance" tabindex="-2" value="<%=strBalance%>">
<input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<table align="center" border="0" width="350" cellspacing="0" cellpadding="0">
	<tr>
		<td align="right" class="control" nowrap>Date:
			<img style="cursor:hand" id="dtActDate" alt="Calendar" src="images/calendr1.gif" OnClick="Calendar(1)" DirtyCheck=TRUE WIDTH="30" HEIGHT="31"></td>
			<td align="left" class="control"><input maxlength="10" size="10" Type="RD" title="To Request:" tabindex=1 DirtyCheck=TRUE name="dtActDate" value="<%=dtActDate%>"></td>
			<td align="right"><label class="control">Amount:</label></td>
        <td><input tabindex="2" type="text" name="strActAmt" size="10" DataType="text" DirtyCheck="TRUE" value="<%=strActAmt%>"></td>
	</tr>
</table>
<table align="center" border="0" width="350" cellspacing="0" cellpadding="0">
	<tr>
		<td align="right">&nbsp;</td>
		<td align="right">&nbsp;</td>
	</tr>
	<tr>
		<td align="right"><label class="control">Reason:</label></td>
		<td align="left" class="control" nowrap>
			<Textarea   tabindex="3" COLS="40" ROWS="3" Title="Description." name="strActDesc" DirtyCheck=TRUE><%=strActDesc%>
			</Textarea>
		</td>
	</tr>
</table>
<table  border="0" width="350" cellspacing="0" cellpadding="0">
<br>
<iframe align="center" Name="fraMain" src="admEditColllist.asp?intUserID=<%=intUserID%>&intCollID=<%=intCollID%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>"  height="200" width="380" frameborder="0"></iframe>
</table>
<table align="center" border="0" width="350" cellspacing="0" cellpadding="0">
	<tr>
		<td align="right"><label class="control">Balance Due:</label></td>
		<td align="right"><label class="control"><%=Formatcurrency(strBalance)%></label></td>
	</tr>
</table>
<br>
<table align="center" border="0" width="350" cellspacing="0" cellpadding="0">
   <tr>
      <td align="center" colspan="3">
	  <button name="btnSave" class="button" style="width:140" OnClick="CollUpdate()">Add Adjustment</button>&nbsp;&nbsp;&nbsp;&nbsp;
	  <button name="btnDelete" class="button" style="width:80" OnClick="Delete()">Delete</button>&nbsp;&nbsp;&nbsp;&nbsp;
	  <button name="btnCancel" class="button" style="width:80" OnClick="Cancel()">Cancel</button>
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
 
End Sub

Sub CollUpdate()
	IF LEN(frmMain.dtActDate.value)=0 then
		MsgBox "Please enter the Date.",64,"Error"
		exit sub
	END IF
	IF LEN(frmMain.strActAmt.value)=0 then
		MsgBox "Please enter the Amount.",64,"Error"
		exit sub
	END IF
	IF LEN(frmMain.strActDesc.value)=0 then
		MsgBox "Please enter the Reason.",64,"Error"
		exit sub
	END IF
	frmMain.FormAction.value="btnSave"
	frmMain.submit()
End Sub

Sub Delete()
	Dim Answer
	window.event.cancelBubble = false
	Answer = MsgBox("Are you sure you want to Delete this Collision?",276,"Confirm Delete")
	If Answer = 6 then
		frmMain.FormAction.value="btnDelete"
		frmMain.submit()
		parent.window.close	
	Else
		Exit Sub
	End if
End Sub

Sub Cancel()
	window.close
End Sub

Sub calendar(Var)
	Dim retDate
	retDate= ShowModalDialog("recCalendarDlg.asp","","dialogwidth:294px;dialogheight:300px;")
			frmMain.btnSave.ClassName = "BUTTON"
	Select Case Var
		Case 1
			frmMain.dtActDate.value = retDate
	End Select

End Sub


</script>

<%
	Call CloseConnection(dbMain)
End Sub

'********************************************************************
' Server-Side Functions
'********************************************************************
Sub UpdateColl(dbMain,intUserID,intCollID,LocationID,LoginID)
	Dim strSQL,rsData,MaxSQL,intActID
	MaxSQL = " Select ISNULL(Max(ActID),0)+1 from UserCol (NOLOCK) where userid="&intUserID &" and LocationID=" & LocationID   &" AND CollID="&intCollID 
	If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
		intActID=rsData(0)
	End if
	Set rsData = Nothing

	strSQL= "Insert into UserCol (UserID,LocationID,CollID,ActID,ActDate,ActType,ActAmt,ActDesc,editby,editdate) Values ("&_
		intUserID &","&_
		LocationID &","&_
		intCollID &","&_
		intActID &","&_
		"'" & SQLReplace(Request("dtActDate")) &"',"&_
		"'Adjustment',"&_
		SQLReplace(Request("strActAmt")) & ","&_
		"'" & SQLReplace(Request("strActDesc")) & "',"&_
		LoginID & ","&_
		"'" & date() & "')"
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
End Sub

Sub DeleteColl(dbMain,intUserID,intCollID,LocationID,LoginID)
	dim strSQL
	strSQL= "Delete UserCol where userid="&intUserID &" and LocationID=" & LocationID   &" AND CollID="&intCollID 
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF


END SUB
%>
