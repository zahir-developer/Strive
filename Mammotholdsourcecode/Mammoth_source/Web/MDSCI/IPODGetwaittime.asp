<%@  language="VBSCRIPT" %>
<%
'********************************************************************
Option Explicit
Response.Expires = 0
Response.Buffer = True
response.ContentType = "application/json"
'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incDatabase.asp"-->
<%
Dim dbMain,strSQL,rsData,Jout,blnUpdatePass, strwaittime

Set dbMain =  OpenConnection
       strSQL = "{call stp_getCurrWaitTime "& Application("LocationID") &"}"     
        if not DBExec(dbMain, strSQL) then
		    blnUpdatePass = 0
        end if


		strSQL = "SELECT CurrentWaitTime FROM stats WHERE (LocationID = "& Application("LocationID") &")"
		if DBOpenRecordset(dbMain,rsData,strSQL) Then
          	    strwaittime = rsData("CurrentWaitTime")
		end if

Jout = "{""waittime"" : """& strwaittime &"""}"
response.write(Jout)
Call CloseConnection(dbMain)
%>