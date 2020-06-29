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

Dim dbMain, intUserID,intColID,intactID, dtActDate,intActType,strActAmt,strActDesc,blsave,LocationID,LoginID

Set dbMain =  OpenConnection

intUserID=Request("intUserID")
    LocationID = request("LocationID")
    LoginID = request("LoginID")

blsave = false

Select Case Request("FormAction")
	Case "btnSave"
		Call UpdateData(dbMain,intUserID,LocationID,LoginID)
		blsave = true
End Select

%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="admNewCollFra.asp">
<div style="text-align:center">
<input type="hidden" name="FormAction" tabindex="-2" value>
<input type="hidden" name="intUserID" tabindex="-2" value="<%=intUserID%>">
<input type="hidden" name="blsave" tabindex="-2" value="<%=blsave%>">
    <input type="hidden" name="LocationID" value="<%=LocationID%>" />
    <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<table align="center" border="0" width="350" cellspacing="0" cellpadding="0">
	<tr>
		<td align="right" class="control" nowrap>Date:</td>
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
		<td align="right"><label class="control">Description:</label></td>
		<td align="left" class="control" nowrap>
			<Textarea   tabindex="3" COLS="40" ROWS="3" Title="Description." name="strActDesc" DirtyCheck=TRUE><%=strActDesc%>
			</Textarea>
		</td>
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
frmMain.strActAmt.value = "250.00"
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
	IF LEN(frmMain.strActDesc.value)=0 then
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
	Dim strSQL,rsData,MaxSQL,intCollID
	MaxSQL = " Select ISNULL(Max(CollID),0)+1 from UserCol (NOLOCK) where userid="&intUserID &" AND LocationID="& LocationID
	If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
		intCollID=rsData(0)
	End if
	Set rsData = Nothing

	strSQL= "Insert into UserCol (UserID,LocationID,CollID,ActID,ActDate,ActType,ActAmt,ActDesc,editby,editdate) Values ("&_
		intUserID &","&_
		LocationID &","&_
		intCollID &","&_
		1 &","&_
		"'" & SQLReplace(Request("dtActDate")) &"',"&_
		"'Logged',"&_
		SQLReplace(Request("strActAmt")) & ","&_
		"'" & SQLReplace(Request("strActDesc")) & "',"&_
		LoginID & ","&_
		"'" & date() & "')"
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
End Sub


%>
