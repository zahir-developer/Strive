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
Dim intAssigned

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

	Dim dbMain,intChargeAmt, hdnChargeAmt
	Dim strSQL, RS 

	Set dbMain =  OpenConnection
	intChargeAmt = request("intChargeAmt")
	hdnChargeAmt = intChargeAmt

%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css">
    <title></title>
</head>
<body class="pgbody">
    <form method="post" name="frmMain" action="CashOutEditFra.asp">
        <div style="text-align: center">
            <input type="hidden" name="hdnChargeAmt" value="<%=hdnChargeAmt%>" />
            <table>
                <tr>
                    <td style="text-align: center">
                        <input style="text-align: right; font: bold 28px arial" type="text" size="5" name="intChargeAmt" value="<%=intChargeAmt%>"></td>
                </tr>
                <tr>
                    <td style="text-align: right" class="control">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: center">
                        <button name="btnProcess" class="button" style="height: 35px; width: 200px; font: bold 18px arial" onclick="CardType()">Process</button>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <button name="btnCancel" class="button" style="height: 35px; width: 200px; font: bold 18px arial" onclick="CancelChg()">Cancel</button>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" class="control">&nbsp;</td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="text-align: center; font: bold 18px arial" class="control">Cash Back</td>
                </tr>
                <tr>
                    <td style="text-align: right" class="control">&nbsp;</td>
            </table>
            <table style="width: 180px; border-collapse: separate; border-spacing: 2px;" border="1">
                <tr>
                    <td style="text-align: center">
                        <button style="height: 35px; width: 50px; font: bold 18px arial" onclick="button('3.00')" id="button3d" name="button12">$3</button>
                    <td style="text-align: center">
                        <button style="height: 35px; width: 50px; font: bold 18px arial" onclick="button('5.00')" id="button5d" name="button0">$5</button>
                    <td style="text-align: center">
                        <button style="height: 35px; width: 50px; font: bold 18px arial" onclick="button('10.00')" id="button10d" name="button13">$10</button>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <button style="height: 35px; width: 50px; font: bold 18px arial" onclick="button('15.00')" id="button15d" name="button12">$15</button>
                    <td style="text-align: center">
                        <button style="height: 35px; width: 50px; font: bold 18px arial" onclick="button('20.00')" id="button20d" name="button0">$20</button>
                    <td style="text-align: center">
                        <button style="height: 35px; width: 50px; font: bold 18px arial" onclick="button('25.00')" id="button25d" name="button13">$25</button>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <button style="height: 35px; width: 50px; font: bold 18px arial" onclick="button('30.00')" id="button30d" name="button12">$30</button>
                    <td style="text-align: center">
                        <button style="height: 35px; width: 50px; font: bold 18px arial" onclick="button('40.00')" id="button40d" name="button0">$40</button>
                    <td style="text-align: center">
                        <button style="height: 35px; width: 50px; font: bold 18px arial" onclick="button('50.00')" id="button50d" name="button13">$50</button>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="3">
                        <button style="height: 35px; width: 150px; font: bold 18px arial" onclick="button('-')" id="button1" name="button1">Reset </button>
                </tr>
            </table>


        </div>
        <input type="hidden" name="FormAction" value="">
    </form>
</body>
</html>
<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/VBSCRIPT">
Option Explicit


Sub CardType()



	window.returnValue = "1|"+document.all("intChargeAmt").value
	parent.window.close
End Sub


Sub CancelChg()
	parent.window.close
End Sub

Sub button(Val)
	IF instr(1,val,"-")>0 then
		document.all("intChargeAmt").value = round(cdbl(document.all("hdnChargeAmt").value),2)
	ELSE
		document.all("intChargeAmt").value = round(cdbl(document.all("intChargeAmt").value) + cdbl(Val),2)
	END IF
End Sub

</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
