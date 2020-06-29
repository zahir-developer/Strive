<%@  language="VBSCRIPT" %>
<%
'********************************************************************
Option Explicit
Response.Expires = 0
Response.Buffer = True
'response.ContentType = "application/json"
'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incDatabase.asp"-->
<%
Dim dbMain,strSQL,rsData,strSQL2,rsData2,Jout,blnLoginPass,strUsername,strPassword,Loc
Set dbMain =  OpenConnection
blnLoginPass = 0
strUsername = trim(UCase(Request("username")))
strPassword = trim(UCase(Request("password")))
response.write  "hey hi"
    response.write("applicationid is..."+Application("LocationID"))

      
strSQL = "SELECT LM_Users.UserID FROM LM_Users WHERE (LM_Users.Active = 1) AND (rtrim(LM_Users.LoginID) = '" & strUsername & "') AND (rtrim(LM_Users.Password) = '" & strPassword & "')  AND (LocationID = "& Application("LocationID") &")"
response.write  strSQL&"<BR>"
      'response.end()
If DBOpenRecordset(dbMain,rsData,strSQL) Then
	If Not rsData.EOF Then
        if rsData("UserID") > 0 then
            blnLoginPass = 1
 
    	    strSQL2= " Update ScanIn set UserID = " & rsData("UserID") &" WHERE (LocationID = "& Application("LocationID") &")"
		    If NOT (dbExec(dbMain,strSQL2)) Then
			        blnLoginPass = 4
		    End If
        else
            blnLoginPass = 0
        end if
    else
        blnLoginPass = 2
    end if
else
    blnLoginPass = 3
end if

Loc = Application("LocationID")


Jout = "{""success"":"& blnLoginPass &" }"

response.write(Jout)

Call CloseConnection(dbMain)
%>