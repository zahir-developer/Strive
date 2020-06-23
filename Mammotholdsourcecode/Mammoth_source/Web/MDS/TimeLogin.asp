<%@  language="VBSCRIPT" %>
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
<!--#include file="incCommon.asp"-->
<%
'********************************************************************
' Global Variables
'********************************************************************
Dim Title
'********************************************************************
' Main
'********************************************************************
Call Main

'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
Sub Main

Dim dbMain, blnLoginFailed,LocationID
Set dbMain =  OpenConnection

      LocationID = Request("LocationID")


Select Case Request("FormAction")
	Case "btnLogin"
		Call DoLogin(dbMain,blnLoginFailed,LocationID)
End Select

%>
<html>
<head>
    <title></title>
    <link rel="stylesheet" href="main.css" type="text/css">
</head>
<body class="pgBody">
    <form action="TimeLogin.asp?LocationID=<%=LocationID%>" method="POST" id="frmMain" name="frmMain" align="left">
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="blnLoginFailed" value="<%=blnLoginFailed%>" />
        <table style="height: 160; width: 100%" border="0">
            <tr>
                <td align="middle" valign="center">
                    <img id="piclogo" src="images/logo_trans.gif" width="235" height="160">
                </td>
            </tr>
        </table>
        <table border="0" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div style="text-align: center">
                        <table style="width: 500; height: 100" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="right">
                                    <label class="control" style="font: bold 18px arial"><b>Scan Card:&nbsp;&nbsp;</b></label></td>
                                <td align="left">
                                    <input type="text" size="11" name="tUserName" onkeypress="Check4Enter()" style="font: bold 28px arial">
                                &nbsp;&nbsp;
    <td align="Right">
        <button style="height: 60; width: 120; font: bold 18px arial" onclick="loginbutton()" id="logon" name="logon">Enter</button>
    </td>
                            </tr>
                        </table>
                        <table style="width: 180" border="2" cellspacing="2" cellpadding="2">
                            <tr>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(1)" id="button1" name="button1">1</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(2)" id="button2" name="button2">2</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(3)" id="button3" name="button3">3</button>
                            </tr>
                            <tr>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(4)" id="button4" name="button4">4</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(5)" id="button5" name="button5">5</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(6)" id="button6" name="button6">6</button>
                            </tr>
                            <tr>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(7)" id="button7" name="button7">7</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(8)" id="button8" name="button8">8</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(9)" id="button9" name="button9">9</button>
                            </tr>
                            <tr>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="DeleteText()" id="buttonD" name="buttonD">BkSp</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(0)" id="button0" name="button0">0</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="ClearText()" id="buttonC" name="buttonC">CLR</button>
                            </tr>
                        </table>
                        <input type="hidden" name="FormAction" value="">
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
	'MSComm1.PortOpen =True
	document.all("tUserName").focus() 
	document.all("tUserName").select
end sub

Sub MSComm1_OnComm()
	dim strInput
	strinput = MSComm1.Input
	If len(trim(strinput))=5 then
		document.all("tUserName").Value = strInput
		frmMain.FormAction.value="btnLogin"
		frmMain.Submit()
	END IF
END Sub
Sub button(Val)
	document.frmMain.tUserName.value = trim(trim(document.frmMain.tUserName.value) & cstr(trim(Val)))
End Sub

Sub DeleteText()
	IF len(trim(document.frmMain.tUserName.value))>0 then
		document.frmMain.tUserName.value = left(trim(document.frmMain.tUserName.value),len(trim(document.frmMain.tUserName.value))-1)
	END IF
End Sub

Sub ClearText()
	document.ALL("tUserName").value = ""
	document.ALL("tUserName").innerText = ""	
End Sub

Sub loginbutton()
	If Trim(document.frmMain.tUserName.value) = ""  Then
		MsgBox "Please enter a valid Username.",48,"Mammoth"
			document.frmMain.tUserName.focus
				
		Exit Sub
	Else
		frmMain.FormAction.value="btnLogin"
		frmMain.Submit()
	End If
End Sub
Sub Check4Enter()
	If window.event.keycode = 13 Then
		window.event.cancelbubble = True
		window.event.returnvalue = FALSE
		loginbutton
	End If
End Sub
</script>

<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Sub DoLogin(db,bLF,LocationID)

Dim strSQL, RS,count,i
	strSQL = "SELECT userid FROM LM_Users WHERE LoginID='" & Request("tUserName") & "' and LocationID="& LocationID
	If DBOpenRecordset(db,rs,strSQL) Then
		If Not RS.EOF Then
			bLF=0
			Response.redirect "TimePassword.asp?userid="&RS("userid") &"&LocationID="& LocationID
		Else
			blF = 1
		End If
	End If
End Sub
%>

