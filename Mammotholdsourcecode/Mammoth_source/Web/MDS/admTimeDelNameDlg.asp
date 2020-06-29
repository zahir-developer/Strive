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
<%
	Dim dbMain,hdnSheetID,hdnUserID,strSQL,rs,intClockid,strSQL2,LocationID,LoginID
	Set dbMain =  OpenConnection
	hdnSheetID = Request("hdnSheetID")
	hdnUserID = Request("hdnUserID")
    LocationID = request("LocationID")
    LoginID = request("LoginID")

	IF LEN(ltrim(hdnSheetID))>0 and LEN(ltrim(hdnUserID))>0 then
		'Check for Details



		strSQL =" SELECT TimeClock.Clockid"&_
			" FROM LM_Users INNER JOIN TimeClock ON LM_Users.UserID = TimeClock.UserID "&_
            " INNER JOIN TimeSheet ON LM_Users.LocationID = TimeSheet.LocationID "&_
			" WHERE (TimeSheet.SheetID ="& hdnSheetID &") and TimeSheet.LocationID=" & LocationID &" "&_
			" AND (TimeClock.Cdatetime BETWEEN TimeSheet.weekof AND DATEADD(day, 7, TimeSheet.weekof))"&_
			" AND (TimeClock.UserID = "& hdnUserID &") AND TimeClock.Paid = 0"
		IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
			IF NOT 	rs.EOF then
				Do while Not rs.EOF

					strSQL2= " delete timeclock Where ClockID = " & rs("Clockid") &" AND LocationID = "& LocationID
					IF NOT DBExec(dbMain, strSQL2) then
						Response.Write gstrMsg
						Response.End
					END IF
				rs.MoveNext
				Loop	
			END IF
		END IF

	END IF
Call CloseConnection(dbMain)

'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">
Option Explicit

Sub Window_Onload()
	Window.close
End Sub

</script>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
