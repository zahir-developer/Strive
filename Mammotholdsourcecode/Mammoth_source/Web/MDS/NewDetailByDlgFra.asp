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
Dim intAssigned,strList

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

	Dim dbMain, intrecID,DetailCompID,rsData,strSQL,strComm,strComAmt,strComPer,intFirstpass,LocationID,LoginID

	Set dbMain =  OpenConnection
	intrecID = request("intrecID")
	strList = request("strList")
    LocationID = request("LocationID")
    LoginID = request("LoginID")

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
	End select

	strSQL = "Select count(*) From DetailComp (nolock) where RecID="&intrecID &" and  LocationID=" & LocationID 
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF NOT rsData.eof then
			intAssigned = rsData(0)
	
			IF intAssigned = 1 then

				
				
				strComPer = 100/intAssigned

				strSQL = "SELECT SUM(comm) as sumComm FROM RECITEM (nolock) WHERE recid =" & intrecID &" and  LocationID=" & LocationID 
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
						"	WHERE recID=" & intrecID &" and LocationID=" & LocationID
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
    <link rel="stylesheet" href="main.css" type="text/css" />
    <title></title>
</head>
<body class="pgbody">
    <form method="post" name="frmMain" action="NewDetailByDlgFra.asp">
        <input type="hidden" name="intRECID" value="<%=intRECID%>" />
        <input type="hidden" name="strList" value="<%=strList%>" />
        <input type="hidden" name="DetailCompID" value="<%=DetailCompID%>" />
        <input type="hidden" name="intAssigned" value="<%=intAssigned%>" />
        <input type="hidden" name="intFirstpass" value="<%=intFirstpass%>" />
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" value="<%=LoginID%>" />
        <div style="text-align: center">
            <table border="0" style="width: 350px; border-collapse: collapse;">
                <tr>
                    <td style="text-align: center">
                        <select tabindex="2" name="cboUserID">
                            <% Call LoadName(dbMain,intrecID,strList) %>
                        </select>&nbsp;&nbsp;
			<button name="btnAddName" style="width: 100px; text-align: center;" onclick="Assign()">Assign</button>
                    </td>
                </tr>
            </table>
            <br>
            <iframe name="fraDetailComp" src="admLoading.asp" scrolling="yes" style="height: 120px; width: 366px" frameborder="0"></iframe>
            <br>
            <table border="0" style="width: 350px; border-collapse: collapse;">
                <tr>
                    <td style="text-align: center" colspan="3">
                        <button name="btnSave" class="button" style="width: 75" onclick="SaveChg()">Save</button>&nbsp;&nbsp;&nbsp;&nbsp;
	                    <button name="btnCancel" class="button" style="width: 75" onclick="CancelChg()">Cancel</button>
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
		Multi= ShowModalDialog("MultiDetailByDlg.asp?intRecID="& frmMain.intRecID.value & "&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:450px;dialogheight:300px;")
	END IF
	fraDetailComp.location.href = "NewDetailByList.asp?intRecID=" & frmMain.intRecID.Value & "&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
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
	Dim intrecID,strSQL,DetailCompID,LocationID,LoginID
	intrecID = request("intrecID")
	LocationID = request("LocationID")
	LoginID = request("LoginID")
	DetailCompID = request("DetailCompID")
	strSQL= "	Delete DetailComp WHERE DetailCompID=" & DetailCompID &" AND LocationID = "& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
		Response.end
	End If
END SUB

Sub CancelChg(dbMain)
	Dim intrecID,strSQL,LocationID,LoginID
	intrecID = request("intrecID")
	LocationID = request("LocationID")
	LoginID = request("LoginID")
	strSQL= "	Delete DetailComp WHERE recID=" & intrecID &" AND LocationID = "& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
		Response.end
	End If
END SUB

Sub Assign(dbMain)
	Dim intrecID,strSQL,rsData,intUserID,strComm,intDetailCompID,dtCDateTime,LocationID,LoginID
	intrecID = request("intrecID")
	LocationID = request("LocationID")
	LoginID = request("LoginID")
	intUserID = request("cboUserID")
		strSQL = "Select cDateTime From DetailComp where recID="&intrecID &" AND LocationID = "& LocationID
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			IF not rsData.eof then
				dtCDateTime = rsData("CDateTime")
			End If
		End If
		Set rsData = Nothing

		strSQL = "Select DetailCompID=IsNull(Max(DetailCompID),0) + 1 From DetailComp Where LocationID = "& LocationID
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			intDetailCompID = rsData("DetailCompID")
		End If
		strSQL = "SELECT SUM(comm) as sumComm FROM RECITEM(nolock) WHERE recid =" & intrecID &" AND LocationID = "& LocationID
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			IF not rsData.eof then
			strComm = rsData("sumComm")
			ELse
			strComm = 0.0
			END IF
		End If
		Set rsData = Nothing
		IF isnull(dtCDateTime) then
			strSQL= " Insert into DetailComp(DetailCompID,LocationID,recID,UserID,Paid,Comm)Values(" & _
					intDetailCompID & ", " & _
					LocationID & ", " & _
					intrecID & ", " & _
					intUserID & ", " & _
					"0, " & _
					strComm & ") " 
			If NOT (dbExec(dbMain,strSQL)) Then
				Response.Write gstrMsg
			End If
		ELSE
			strSQL= " Insert into DetailComp(DetailCompID,LocationID,CDateTime,recID,UserID,Paid,Comm)Values(" & _
					intDetailCompID & ", " & _
					LocationID & ", " & _
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

Function LoadName(db,hdnRecID,strList)
	Dim strSQL,RS,LocationID,LoginID
	LocationID = request("LocationID")
	LoginID = request("LoginID")
	IF isnull(hdnRecID) or len(ltrim(hdnRecID))=0 then
		hdnRecID = 0
	END IF
IF strList = "all" then
	strSQL= " SELECT LM_Users.UserID, LM_Users.FirstName,LM_Users.LastName"&_
		" FROM LM_Users(NOLOCK) "&_
		" WHERE LM_Users.LocationID = " & LocationID & " AND LM_Users.Active=1 AND LM_Users.userID NOT IN (SELECT userid FROM DetailComp"&_
		" WHERE DetailComp.RecID = "& hdnRecID  &" AND  DetailComp.LocationID = " & LocationID & ") Order by LM_Users.LastName"
ELSE
	strSQL= " SELECT TimeClock.UserID, LM_Users.FirstName,LM_Users.LastName"&_
		" FROM TimeClock(NOLOCK) INNER JOIN LM_Users(NOLOCK) ON TimeClock.UserID = LM_Users.UserID"&_
		" WHERE (CONVERT(char, TimeClock.Cdatetime, 101) = CONVERT(char, GETDATE(), 101))"&_
		" AND TimeClock.CType = 9 AND TimeClock.Caction = 0"&_
		" AND (LM_Users.LocationID = " & LocationID & ")"&_
		" AND LM_Users.userID NOT IN (SELECT userid FROM DetailComp"&_
		" WHERE DetailComp.RecID = "& hdnRecID &" AND  DetailComp.LocationID = " & LocationID & ") Order by LM_Users.LastName"
END IF
%>
<option value=""></option>
<%
	If dbOpenStaticRecordset(db,rs,strSQL) Then
		Do While Not RS.EOF
%>
<option value="<%=RS("UserID")%>"><%=RS("FirstName")%>&nbsp;&nbsp;&nbsp;<%=RS("LastName")%></option>
<%
			RS.MoveNext
		Loop
	End If
End Function

%>
