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

Dim dbMain, blnLoginFailed,hdnuserid,LocationID
Set dbMain =  OpenConnection
hdnuserid=Request("userid")
          LocationID = Request("LocationID")

Select Case Request("FormAction")
	Case "btnLogin"
		Call DoLogin(dbMain,blnLoginFailed,hdnuserid,LocationID)
End Select

%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css" />
    <title></title>
</head>
<body class="pgBody">
    <form action="TimePassword.asp?userid=<%=hdnuserid%>&LocationID=<%=LocationID%>" method="post" id="frmMain" name="frmMain">
        <input type="hidden" name="blnLoginFailed" value="<%=blnLoginFailed%>" />
        <input type="hidden" name="hdnuserid" value="<%=hdnuserid%>" />
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
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
                    <div style="text-align:center">
                        <table style="width: 500; height: 100" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="right">
                                    <label class="control" style="font: bold 18px arial"><b>Password:&nbsp;&nbsp;</b></label></td>
                                <td align="left">
                                    <input type="password" size="11" name="tPassword" style="font: bold 28px arial">
                                &nbsp;&nbsp;
    <td align="Right">
        <button style="height: 60; width: 120; font: bold 18px arial" onclick="loginbutton()" id="logon" name="logon">Enter</button>
    </td>
    </tr>
                        </table>
                        <table style="width: 500" border="2" cellspacing="2" cellpadding="2">
                            <tr>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(1)" id="button1" name="button1">1</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(2)" id="button2" name="button2">2</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(3)" id="button3" name="button3">3</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(4)" id="button4" name="button4">4</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(5)" id="button5" name="button5">5</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(6)" id="button6" name="button6">6</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(7)" id="button7" name="button7">7</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(8)" id="button8" name="button8">8</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(9)" id="button9" name="button9">9</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button(0)" id="button0" name="button0">0</button>
                            </tr>
                            <tr>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('Q')" id="buttonQ" name="buttonQ">Q</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('W')" id="buttonW" name="buttonW">W</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('E')" id="buttonE" name="buttonE">E</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('R')" id="buttonR" name="buttonR">R</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('T')" id="buttonT" name="buttonT">T</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('Y')" id="buttonY" name="buttonY">Y</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('U')" id="buttonU" name="buttonU">U</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('I')" id="buttonI" name="buttonI">I</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('O')" id="buttonO" name="buttonO">O</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('P')" id="buttonP" name="buttonP">P</button>
                            </tr>
                            <tr>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('A')" id="buttonA" name="buttonA">A</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('S')" id="buttonS" name="buttonS">S</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('D')" id="buttonD" name="buttonD">D</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('F')" id="buttonF" name="buttonF">F</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('G')" id="buttonG" name="buttonG">G</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('H')" id="buttonH" name="buttonH">H</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('J')" id="buttonJ" name="buttonJ">J</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('K')" id="buttonK" name="buttonK">K</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('L')" id="buttonL" name="buttonL">L</button>
                            </tr>
                            <tr>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('Z')" id="buttonZ" name="buttonZ">Z</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('X')" id="buttonX" name="buttonX">X</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('C')" id="buttonC" name="buttonC">C</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('V')" id="buttonV" name="buttonV">V</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('B')" id="buttonB" name="buttonB">B</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('N')" id="buttonN" name="buttonN">N</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="button('M')" id="buttonM" name="buttonM">M</button>
                                <td align="center">
                                    <button style="height: 60; width: 60; font: bold 18px arial" onclick="DeleteText()" id="buttonD" name="buttonD">BkSp</button>
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
Sub button(Val)
	document.frmMain.tPassword.value = trim(trim(document.frmMain.tPassword.value) & cstr(trim(Val)))
End Sub

Sub DeleteText()
	IF len(trim(document.frmMain.tPassword.value))>0 then
		document.frmMain.tPassword.value = left(trim(document.frmMain.tPassword.value),len(trim(document.frmMain.tPassword.value))-1)
	END IF
End Sub

Sub ClearText()
	document.ALL("tPassword").value = ""
	document.ALL("tPassword").innerText = ""	
End Sub

Sub loginbutton()
	If Trim(document.frmMain.tPassword.value) = ""  Then
		MsgBox "Please enter a valid Password.",48,"Mammoth"
			document.frmMain.tPassword.focus
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
Sub DoLogin(db,bLF,hdnuserid,LocationID)

Dim strSQL, RS,count,i
	strSQL = "SELECT Password FROM LM_Users WHERE userid=" & hdnuserid  & " and LocationID="& LocationID
	If DBOpenRecordset(db,rs,strSQL) Then
		If Not RS.EOF Then

			If  Trim(RS("Password")) = Trim(Request("tPassword")) Then
				bLF=0
               'response.Write hdnuserid & "<br />"
               'response.Write LocationID & "<br />"
               'response.end
                response.redirect "TimeSet.asp?UserID="& hdnuserid &"&LocationID="& LocationID
			Else
				bLF  = 1
				response.redirect "TimeLogin.asp?LocationID="& LocationID
			End If
		Else
			blF = 1
			response.redirect "TimeLogin.asp?LocationID="& LocationID
		End If
	End If
End Sub
%>

