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

Dim dbMain,strSQL2,rs2, intUserID,intCollID,intactID, dtActDate,intActType,strActAmt,strActDesc,strBalance
Dim hdnSheetID,strGtotal, rsData, strSQL,strCollbal,blSave,LocationID,LoginID
Set dbMain =  OpenConnection

intUserID=Request("intUserID")
hdnSheetID=Request("hdnSheetID")
strGTotal=Request("strGTotal")
LocationID = request("LocationID")
LoginID = request("LoginID")

strSQL =" SELECT sum(actamt) as Bal From UserCol (Nolock) where UserID=" & intUserID &" and LocationID=" & LocationID   &" group by UserID"
IF dbOpenStaticRecordset(dbmain, rsData, strSQL) then   
	IF NOT 	rsData.EOF then
	strCollbal = rsData("Bal")
	END IF
END IF
Set rsData = Nothing
		blSave = 0

Select Case Request("FormAction")
	Case "btnSave"
		Call UpdateColl(dbMain,intUserID,hdnSheetID,strGTotal)
		blSave = 1
End Select

%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="post" name="frmMain" action="admTimeAddCollFra.asp">
        <div style="text-align: center;">
<input type="hidden" name="FormAction" tabindex="-2" value>
<input type="hidden" name="intUserID" tabindex="-2" value="<%=intUserID%>">
<input type="hidden" name="hdnSheetID" tabindex="-2" value="<%=hdnSheetID%>">
<input type="hidden" name="strGtotal" tabindex="-2" value="<%=strGtotal%>">
<input type="hidden" name="strCollbal" tabindex="-2" value="<%=strCollbal%>">
<input type="hidden" name="blSave" tabindex="-2" value="<%=blSave%>">
            <input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
            <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
            <table border="0" style="width: 350px; border-collapse: collapse;">
	<tr>
		<td align="right" class="control" nowrap>Date:
			<img style="cursor:hand" id="dtActDate" alt="Calendar" src="images/calendr1.gif" OnClick="Calendar(1)" DirtyCheck=TRUE WIDTH="30" HEIGHT="31"></td>
			<td align="left" class="control"><input maxlength="10" size="10" Type="RD" title="To Request:" tabindex=1 DirtyCheck=TRUE name="dtActDate" value="<%=dtActDate%>"></td>
			<td align="right"><label class="control">Amount:</label></td>
        <td><input tabindex="2" type="text" name="strActAmt" size="10" DataType="text" DirtyCheck="TRUE" value="<%=strActAmt%>"></td>
	</tr>
</table>
            <table border="0" style="width: 350px; border-collapse: collapse;">
	<tr>
		<td align="right">&nbsp;</td>
		<td align="right">&nbsp;</td>
	</tr>
	<tr>
		<td align="right"><label class="control">Notes:</label></td>
		<td align="left" class="control" nowrap>
			<Textarea   tabindex="3" COLS="40" ROWS="3" Title="Description." name="strActDesc" DirtyCheck=TRUE><%=strActDesc%>
			</Textarea>
		</td>
	</tr>
</table>
<br>
            <table border="0" style="width: 350px; border-collapse: collapse;">
   <tr>
      <td align="center" colspan="3">
	  <button name="btnSave" class="button" style="width:80" OnClick="CollUpdate()">Save</button>&nbsp;&nbsp;&nbsp;&nbsp;
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
	IF frmMain.blSave.value = 1 then
		parent.window.close
	END IF
 frmMain.dtActDate.value = date()
 frmMain.strActDesc.value  ="Payroll Payment"
 frmMain.strActAmt.focus 
 frmMain.strActAmt.select 
 
End Sub

Sub CollUpdate()

	IF LEN(frmMain.dtActDate.value)=0 then
		MsgBox "Please enter the Date.",64,"Error"
		exit sub
	END IF
	IF LEN(frmMain.strActAmt.value)=0 then
		MsgBox "Please enter the Amount.",64,"Error"
		exit sub
	ELSE
		IF cdbl(frmMain.strActAmt.value) > cdbl(frmMain.strGtotal.value) then
			MsgBox "The Amount "& cstr(formatcurrency(frmMain.strActAmt.value,2)) & " is greater then the Total " & CSTR(formatcurrency(frmMain.strGtotal.value,2)) &"." ,64,"Error"
			exit sub
		ELSE
			IF cdbl(frmMain.strActAmt.value) > cdbl(frmMain.strCollbal.value) then
				MsgBox "The Amount "& cstr(formatcurrency(frmMain.strActAmt.value,2)) & " is greater then the Balance Due " & CSTR(formatcurrency(frmMain.strCollbal.value,2)) &"." ,64,"Error"
				exit sub
			END IF
		END IF
	END IF
	IF LEN(frmMain.strActDesc.value)=0 then
		MsgBox "Please enter the Reason.",64,"Error"
		exit sub
	END IF
	frmMain.FormAction.value="btnSave"
	frmMain.submit()
	'parent.window.close
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
Sub UpdateColl(dbMain,intUserID,hdnSheetID,strGTotal)
	Dim strSQL,rsData,MaxSQL,intActID,strActAmt,dtActDate,strActDesc
	Dim strActBal,intBalDue,intCollID,rsData2,strSQL2,LocationID,LoginID
	strActAmt = Request("strActAmt")
	dtActDate = Request("dtActDate")
	strActDesc = Request("strActDesc")
	strActBal = strActAmt
LocationID = request("LocationID")
LoginID = request("LoginID")

	strSQL =" SELECT UserCol.UserID, UserCol.CollID,"&_
		" Sum(UserCol.ActAmt) as BalDue "&_
		" FROM UserCol(Nolock)"&_
		" where UserID=" & intUserID &" and LocationID=" & LocationID  &" GROUP BY UserID, CollID Order By CollID"
 
     IF dbOpenStaticRecordset(dbMain, rsData, strSQL) then   
		IF NOT 	rsData.EOF then
			Do while Not rsData.EOF
				IF rsData("BalDue") > 0 and strActBal > 0 then
					intBalDue = rsData("BalDue")
					intCollID = rsData("CollID")
					IF cdbl(strActBal) >= cdbl(intBalDue) then
						' insert intBalDue
						MaxSQL = " Select ISNULL(Max(ActID),0)+1 from UserCol (NOLOCK) where userid="&intUserID &" and LocationID=" & LocationID  &" AND CollID="&intCollID 
						If dbOpenRecordSet(dbmain,rsData2,MaxSQL) Then
							intActID=rsData2(0)
						End if
						Set rsData2 = Nothing

						strSQL2= "Insert into UserCol(UserID,LocationID,CollID,ActID,SheetID,ActDate,ActType,ActAmt,ActDesc,editby,editdate) Values ("&_
							intUserID &","&_
							LocationID &","&_
							intCollID &","&_
							intActID &","&_
							hdnSheetID &","&_
							"'" & dtActDate &"',"&_
							"'Payment',"&_
							intBalDue*-1 & ","&_
							"'" & strActDesc & "',"&_
							LoginID & ","&_
							"'" & date() & "')"
						IF NOT DBExec(dbMain, strSQL2) then
							Response.Write gstrMsg
							Response.End
						END IF

						strActBal = cdbl(strActBal) - cdbl(intBalDue)
					ELSE
						' insert strActBal
						MaxSQL = " Select ISNULL(Max(ActID),0)+1 from UserCol (NOLOCK) where userid="&intUserID &" and LocationID=" & LocationID &" AND CollID="&intCollID 
						If dbOpenRecordSet(dbmain,rsData2,MaxSQL) Then
							intActID=rsData2(0)
						End if
						Set rsData2 = Nothing

						strSQL2= "Insert into UserCol(UserID,LocationID,CollID,ActID,SheetID,ActDate,ActType,ActAmt,ActDesc,editby,editdate) Values ("&_
							intUserID &","&_
							LocationID &","&_
							intCollID &","&_
							intActID &","&_
							hdnSheetID &","&_
							"'" & dtActDate &"',"&_
							"'Payment',"&_
							strActBal*-1 & ","&_
							"'" & strActDesc & "',"&_
							LoginID & ","&_
							"'" & date() & "')"
						IF NOT DBExec(dbMain, strSQL2) then
							Response.Write gstrMsg
							Response.End
						END IF

						strActBal = 0
					END IF
				END IF
				rsData.MoveNext
			Loop	
		END IF
	END IF
	Set rsData = Nothing

End Sub

%>
