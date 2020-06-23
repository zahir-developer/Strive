<%@  language="VBSCRIPT" %>
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

Dim dbMain,strSQL2,rs2, intUserID,intUnifID,intactID, dtActDate,intActType,strActAmt,strActDesc,strBalance,LocationID,LoginID

Set dbMain =  OpenConnection

intUserID=Request("intUserID")
intUnifID=Request("intUnifID")
            LocationID = request("LocationID")
    LoginID = request("LoginID")


Select Case Request("FormAction")
	Case "btnSave"
		Call UpdateUnif(dbMain,intUserID,intUnifID,LocationID,LoginID)
	Case "btnDelete"
		Call DeleteUnif(dbMain,intUserID,intUnifID,LocationID,LoginID)
End Select


strSQL2 =" SELECT sum(actamt) as Bal From UserUnif (Nolock) where UserID=" & intUserID &" and LocationID="& LocationID &" and UnifID="& intUnifID &" group by UserID,UnifID"

IF dbOpenStaticRecordset(dbMain, rs2, strSQL2) then   
	IF NOT 	rs2.EOF then
	strBalance = rs2("Bal")
	END IF
END IF



%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css" />
    <title></title>
</head>
<body class="pgbody">
    <form method="post" name="frmMain" action="admEditUnifFra.asp">
        <div style="text-align:center">
            <input type="hidden" name="FormAction" tabindex="-2" value>
            <input type="hidden" name="intUserID" tabindex="-2" value="<%=intUserID%>" />
            <input type="hidden" name="intUnifID" tabindex="-2" value="<%=intUnifID%>" />
            <input type="hidden" name="strBalance" tabindex="-2" value="<%=strBalance%>" />
            <input type="hidden" name="LocationID" value="<%=LocationID%>" />
            <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
            <table border="0" style="width: 350px; border-collapse: collapse;">
                <tr>
                    <td align="right" class="control" nowrap>Date:
                    </td>
                    <td align="left" class="control">
                        <input maxlength="10" size="10" type="RD" title="To Request:" tabindex="1" dirtycheck="TRUE" name="dtActDate" value="<%=dtActDate%>"></td>
                    <td align="right">
                        <label class="control">Amount:</label></td>
                    <td>
                        <input tabindex="2" type="text" name="strActAmt" size="10" datatype="text" dirtycheck="TRUE" value="<%=strActAmt%>"></td>
                </tr>
            </table>
            <table align="center" border="0" width="350" cellspacing="0" cellpadding="0">
                <tr>
                    <td align="right">&nbsp;</td>
                    <td align="right">&nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <label class="control">Reason:</label></td>
                    <td align="left" class="control" nowrap>
                        <textarea tabindex="3" cols="40" rows="3" title="Description." name="strActDesc" dirtycheck="TRUE"><%=strActDesc%>
			</textarea>
                    </td>
                </tr>
            </table>
            <table border="0" width="350" cellspacing="0" cellpadding="0">
                <br>
                <iframe align="center" name="fraMain" src="admEditUniflist.asp?intUserID=<%=intUserID%>&intUnifID=<%=intUnifID%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>" height="200" width="380" frameborder="0"></iframe>
            </table>
            <table align="center" border="0" width="350" cellspacing="0" cellpadding="0">
                <tr>
                    <td align="right">
                        <label class="control">Balance Due:</label></td>
                    <td align="right">
                        <label class="control"><%=Formatcurrency(strBalance)%></label></td>
                </tr>
            </table>
            <br>
            <table align="center" border="0" width="350" cellspacing="0" cellpadding="0">
                <tr>
                    <td align="center" colspan="3">
                        <button name="btnSave" class="button" style="width: 140" onclick="UnifUpdate()">Add Adjustment</button>&nbsp;&nbsp;&nbsp;&nbsp;
	  <button name="btnDelete" class="button" style="width: 80" onclick="Delete()">Delete</button>&nbsp;&nbsp;&nbsp;&nbsp;
	  <button name="btnCancel" class="button" style="width: 80" onclick="Cancel()">Cancel</button>
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

Sub UnifUpdate()
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
	Answer = MsgBox("Are you sure you want to Delete this Unifision?",276,"Confirm Delete")
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

</script>

<%
	Call CloseConnection(dbMain)
End Sub

'********************************************************************
' Server-Side Functions
'********************************************************************
Sub UpdateUnif(dbMain,intUserID,intUnifID,LocationID,LoginID)
	Dim strSQL,rsData,MaxSQL,intActID
	MaxSQL = " Select ISNULL(Max(ActID),0)+1 from UserUnif (NOLOCK) where userid="&intUserID &" AND UnifID="&intUnifID &" AND LocationID="&LocationID 
	If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
		intActID=rsData(0)
	End if
	Set rsData = Nothing

	strSQL= "Insert into UserUnif (UserID,LocationID,UnifID,ActID,ActDate,ActType,ActAmt,ActDesc,editby,editdate) Values ("&_
		intUserID &","&_
		LocationID &","&_
		intUnifID &","&_
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

Sub DeleteUnif(dbMain,intUserID,intUnifID,LocationID,LoginID)
	dim strSQL
	strSQL= "Delete UserUnif where userid="&intUserID &" AND UnifID="&intUnifID  &" AND LocationID="&LocationID 
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF


END SUB
%>
