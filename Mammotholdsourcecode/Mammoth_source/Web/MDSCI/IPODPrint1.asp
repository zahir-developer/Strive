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
Dim dbMain,strSQL,rsData,strSQL2,rsData2,Jout,blnUpdatePass,strPrint1,strDept,intrecID
Set dbMain =  OpenConnection
blnUpdatePass = 0
strPrint1 = trim(UCase(Request("Print1")))
 
 strSQL = "SELECT RecID,Dept FROM ScanIn WHERE (LocationID = "& Application("LocationID") &")"
if DBOpenRecordset(dbMain,rsData,strSQL) Then
	if not rsData.EOF Then
        strDept = rsData("Dept")
        intrecID = rsData("RecID")

        blnUpdatePass = 1

         IF strDept = 1 then
	        strSQL= " Insert into PrintTicket(LocationID,recID,Printer,Report)Values(" & Application("LocationID") & "," & intrecID & ",1,'VehicalCopy')" 
	        If NOT (dbExec(dbMain,strSQL)) Then
                blnUpdatePass = 0
	        End If
        ELSE
	        strSQL= " Insert into PrintTicket(LocationID,recID,Printer,Report)Values(" & Application("LocationID") & "," & intrecID & ", 1,'VehicalCopy')" 
	        If NOT (dbExec(dbMain,strSQL)) Then
                blnUpdatePass = 0
	        End If
        END IF
 
        ''Dim WshShell
        ''remove the "server." part to run this clientside
        'set WshShell = CreateObject("WScript.Shell") 
        ''this is path to notepad in win2k
        ''You should have enough priviledges to run it...
        'WshShell.Run "D:\www\websites\MPOS\PrintTicket\PrintTicket.exe"
        'set WshShell= nothing   

        'Dim objShell
        'Set objShell = WScript.CreateObject( "WScript.Shell" )
        'objshell.Run("D:\www\websites\MPOS\PrintTicket\PrintTicket.exe") 
        'set objshell= nothing 
 
    
       	'strSQL= "{Call stp_Printticket}" 
	    'If NOT (dbExec(dbMain,strSQL)) Then
        '    blnUpdatePass = 0
	    'End If
    end if 
end if


Jout = "{""success"":"& blnUpdatePass &"}"

response.write(Jout)

Call CloseConnection(dbMain)
%>