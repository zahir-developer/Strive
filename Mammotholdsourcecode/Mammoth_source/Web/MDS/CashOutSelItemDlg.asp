<%@  language="VBSCRIPT" %>
<%
'********************************************************************
' Name: 
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
'********************************************************************
' Global Variables
'********************************************************************
Dim Title,hdnStatus,strDescript,intProdid,strUPC,LocationID

'********************************************************************
' Main
'********************************************************************

LocationID = request("LocationID")

'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css">
    <title>Item Search</title>
</head>
<body class="pgbody">
    <form method="POST" name="frmMain" action="CashOutSelItemDlg.asp">
        <input type="hidden" name="hdnStatus" value="<%=hdnStatus%>">
        <input type="hidden" name="intProdid" value="<%=intProdid%>">
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <div style="text-align: center">
            <table border="0" width="577" cellspacing="0" cellpadding="0">
                <tr>
                    <td align="right" class="control" nowrap>Item Name:</td>
                    <td align="left" class="control" nowrap>
                        <input maxlength="20" size="20" type="text" tabindex="1" onkeyup="NameChg()" name="strDescript" value="<%=strDescript%>">&nbsp; </td>
                    <td align="right" class="control" nowrap>UPC:</td>
                    <td align="left" class="control" nowrap>
                        <input maxlength="20" size="20" type="text" tabindex="1" onkeyup="UPCChg()" name="strUPC" value="<%=strUPC%>">&nbsp; </td>
                </tr>
            </table>
            <br>
            <table cellspacing="0" width="577" class="data">
                <tr>
                    <td class="header" width="200" align="center" valign="bottom">Item Name</td>
                    <td class="header" width="150" align="center" valign="bottom">Catagory</td>
                    <td class="header" width="100" align="center" valign="bottom">UPC</td>
                    <td class="header" width="80" align="center" valign="bottom">Price</td>
                </tr>
                <table cellspacing="0" width="577" class="data">
                    <iframe align="center" name="fraMain" src="CashOutSelItemDlgFra.asp?strDescript=<%=strDescript%>&LocationID=<%=LocationID%>" height="240" width="577" frameborder="0"></iframe>
                </table>
        </div>
    </form>
</body>
</html>
<%

'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/VBSCRIPT">
Option Explicit

Sub Window_onUnload()
	IF LEN(TRIM(frmMain.intProdID.value))> 0 then
		window.returnValue = frmMain.intProdID.value&"|1"
	ELSE
		IF Len(trim(frmMain.hdnStatus.value))>0 then
				window.returnValue = cstr(frmMain.hdnStatus.value)&"|0"
		ELSE
			window.returnValue = 0
		END IF
	END IF
End Sub


Sub NameChg()
	fraMain.location.href = "CashOutSelItemDlgFra.asp?strDescript="& frmMain.strDescript.value & "&LocationID="& frmMain.LocationID.value
End Sub

Sub UPCChg()
	dim retProdID
		retProdID= ShowModalDialog("CashOutSelItemUPC.asp?intUPC=" & frmMain.strUPC.value ,"","dialogwidth:450px;dialogheight:500px;")
		'check UPC
		IF len(trim(retProdID)) > 0 then
			frmMain.intProdID.value = cstr(retProdID)
			window.close
		END IF
End Sub

</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************

%>
