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
<!--#include file="incSecurity.asp"-->
<%
'********************************************************************
' Global Variables
'********************************************************************



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
	Dim dbMain,strSQL,LocationID,LoginID
	Set dbMain =  OpenConnection
    LocationID = request("LocationID")
    LoginID = request("LoginID")
	
		
	Select Case Request("FormAction")
		Case "btnNew"
			Response.Redirect "admUserEdit.asp?UserID=0&LocationID="&LocationID &"&LoginID="&LoginID

	End Select

'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css" />
    <title>User List</title>
</head>
<body class="pgbody">
    <form method="post" name="frmMain" action="admusers.asp">
        <input type="hidden" name="FormAction" />
        <input type="hidden" id="hdnValue" name="hdnValue" />
        <input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
        <div style="text-align: center">

            <table class="tblcaption" style="width: 768px; border-collapse: collapse;">
                <tr>
                    <td class="tdcaption" style="text-align: center; background-image: url(images/header.jpg); width: 110px">Users</td>
                    <td style="text-align: right">
                        <button name="btnNew" onmouseover="Buttonhigh()" onmouseout="Buttonlow()" style="width: 75px;" onclick="SubmitForm()">New</button>
                        <button name="btnFilter" onmouseover="Buttonhigh()" onmouseout="Buttonlow()" style="width: 75px;" onclick="ShowFilter(User)">Filter</button>
                    </td>
                </tr>
            </table>

            <iframe name="fraMain" src="admLoading.asp" scrolling="yes" style="height: 375px; width: 768px" frameborder="0"></iframe>
        </div>
    </form>
</body>
</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/javascript">

    function SubmitForm() {
        window.event.cancelBubble = true;
        window.event.returnValue = false;

        if (window.event.srcElement.ClassName == "BUTTONDEAD") {
            window.event.ReturnValue = false
        }

        switch (window.event.srcElement.name) {
            case "btnNew":
                window.location.href = "admUserEdit.asp?UserID=0&LocationID=" + document.getElementById("LocationID").value + "&LoginID=" + document.getElementById("LoginID").value;
                break;
        }
    }


</script>
<script language="VBSCRIPT">
Option Explicit
Sub Window_Onload()
	Dim Obj
	fraMain.location.href = "admUserListFra.asp?LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value 

End Sub

Sub SubmitForm()
	window.event.CancelBubble=True
	window.event.ReturnValue=False
	
	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	
	Select Case window.event.srcElement.name
		Case "btnNew"
			location.href = "admUserEdit.asp?UserID=0&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value 
	End Select
			
End Sub



Sub ShowFilter(strType)
	Dim strFilt
	strFilt = ShowModalDialog("admFilterDlg.asp?Type=" & strType,,"center:1;dialogwidth:475px;dialogheight:350px;")
		If Len(Trim(strFilt)) > 0 Then
				FraMain.Location = "admUserListFra.asp?FilterBy=" & ClientEncode(strFilt) &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value 
		End If
End Sub
</script>

<%
	Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************



%>
