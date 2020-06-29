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
Main
Sub Main
	Dim dbMain,strSQL,rsData,MaxSQL,intClockID
    dim varDate,varTime,intAct
	Set dbMain =  OpenConnection

    varDate = formatdatetime(Request("txtRptDate"),2)
    varTime = Request("dtTime")
    intAct = Request("intAct")


	IF ISNULL(Request("intClockID")) or LEN(Request("intClockID"))=0 or Request("intClockID")=0 then
		MaxSQL = " Select ISNULL(Max(ClockID),0)+1 from timeclock (NOLOCK) where LocationId=" &  Request("LocationID")
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			intClockID=rsData(0)
		End if
		Set rsData = Nothing
		strSQL=" Insert into timeclock(ClockID,UserID,LocationID,Cdatetime,CAction,CType,EditBy,EditDate,Paid)Values(" & _
					intClockID & ", " & _
					Request("intuserID") & ", " & _
					Request("LocationID") & ", " & _
					"'" & varDate &" "& varTime  & "', " & _
					intAct & ", " & _
					"1, " & _
					Request("LoginID") & ", " & _
					"'" & Date() & "',0)"
		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
	ELSE
		strSQL= " Update timeclock SET "&_
			" Cdatetime = '"& replace(varDate,"'","") &" "& varTime &"',"&_
			" CAction = "& intAct &","&_
			" Editby = "&  Request("LoginID") & ","&_
			" Editdate = '"& Now() & "'"&_
			" Where ClockID = " & Request("intClockID") &" and LocationId=" &  Request("LocationID")
		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
	END IF
'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
</head>
<body class="pgbody">
</body>
</html>
<%
Call CloseConnection(dbMain)
End Sub
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
