<%@ LANGUAGE="VBSCRIPT" %>
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

	Dim dbMain,intCustAccID, intAmount,strStatus,strCurrentAmt,intAccAmt,intRecID,intBalance,LocationID,LoginID,intCtype
	Dim strAccName
	Set dbMain =  OpenConnection

intCustAccID = Request("intCustAccID")
intRecID = Request("intRecID")
strCurrentAmt = Request("strCurrentAmt")
intAccAmt = Request("intAccAmt")
intAmount = Request("intAmount")
LocationID = request("LocationID")
LoginID = request("LoginID")

Select Case Request("FormAction")
	Case "ProcessChg"
		Call ProcessChg(dbMain)
		strStatus = "Yes"
End Select

IF LEN(trim(intCustAccID))>0 then
	Call GetAccInfo(dbMain,intCustAccID,strCurrentAmt,intAmount,intAccAmt,strAccName,intRecID,LocationID,LoginID,intCtype)
	IF isnull(strCurrentAmt) then
		strCurrentAmt = 0.0
	END IF
	IF isnull(intAmount) then
		intAmount = 0.0
	END IF

	intBalance = cdbl(strCurrentAmt) - cdbl(intAmount)
ELSE
	intCustAccID = ""
	strCurrentAmt = 0.0
	intAmount = ""
	intBalance = 0.0
END IF

%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<Title></title>
</head>
<body class="pgbody">
<form method="POST" name="frmMain" action="CashoutAccDlgFra.asp">
<input type="hidden" name="strStatus" tabindex="-2" value="<%=strStatus%>">
<input type="hidden" name="intRecID" tabindex="-2" value="<%=intRecID%>">
<input type="hidden" name="intCustAccID" tabindex="-2" value="<%=intCustAccID%>">
<input type="hidden" name="strAccName" tabindex="-2" value="<%=strAccName%>">
<input type="hidden" name="strCurrentAmt" tabindex="-2" value="<%=strCurrentAmt%>">
<input type="hidden" name="intBalance" tabindex="-2" value="<%=intBalance%>">
<input type="hidden" name="intAccAmt" tabindex="-2" value="<%=intAccAmt%>">
<input type="hidden" name="intAmount" tabindex="-2" value="<%=intAmount%>">
<input type="hidden" name="intCtype" tabindex="-2" value="<%=intCtype%>">
<input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<div style="text-align:center">
<br>
<table border="0">
	<tr>
		<td style="text-align:right" class="control" >&nbsp;</td>
	</tr>
	<% if intCtype = 1 then %>
	<tr style="text-align: center">
		<td style="text-align: center; font:bold 18px arial" colspan="2" class="control">Corporate Account</td>
	</tr>
	<% elseif intCtype = 2 then %>
	<tr style="text-align: center">
		<td style="text-align: center;font:bold 18px arial" colspan="2" class="control" >Monthly Account</td>
	</tr>
	<% elseif intCtype = 3 then %>
	<tr style="text-align: center">
		<td style="text-align: center;font:bold 18px arial" colspan="2" class="control" >Comp Account</td>
	</tr>
	<% End if %>

	<tr style="text-align: center">
        <td style="text-align:left; " class="control"  colspan="2"><label style="font:bold 18px arial" class="blkdata" ID="lblAccName" ></label></td>
	</tr>
	<tr>
		<td style="text-align:right" class="control" >&nbsp;</td>
	</tr>
	<tr style="text-align: center">
		<td style="text-align:right; font:bold 18px arial; width:50%" class="control" >Current Value:&nbsp;</td>
        <td style="text-align:left; width:50%" class="control" ><label style="font:bold 18px arial" class="blkdata" ID="lblCurrentAmt" ></label></td>
	</tr>
	<tr>
		<td style="text-align:right" class="control" >&nbsp;</td>
	</tr>
	<tr>
		<td style="text-align:right; font:bold 18px arial; width:50%" class="control" >Amount:&nbsp;</td>
        <td style="text-align:left; width:50%" class="control" ><label style="font:bold 18px arial" class="blkdata" ID="lblAmount" ></label></td>
	</tr>
	<tr>
		<td style="text-align:right" class="control" >&nbsp;</td>
	</tr>
	<tr style="text-align: center">
		<td style="text-align:right; font:bold 18px arial; width:50%" class="control" >Balance:&nbsp;</td>
        <td style="text-align:left; width:50%" class="control" ><label style="font:bold 18px arial" class="blkdata" ID="lblBalance" ></label></td>
	</tr>
	<tr>
		<td style="text-align:right" class="control" >&nbsp;</td>
	</tr>
	<tr>
	     <td colspan=2 style="text-align: center"><button  name="btnProcess" class="button"  style="height:35px;width:200px;font:bold 18px arial" OnClick="ProcessChg()">&nbsp;Process&nbsp;</button></td>
	</tr>
	<tr>
		<td style="text-align:right" class="control" >&nbsp;</td>
	</tr>
   <tr>
      <td  colspan=2  style="text-align: center" >
	  <button name="btnCancel" class="button" style="height:35px;width:200px;font:bold 18px arial" OnClick="CancelChg()">Cancel</button>
		</td>
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

Sub Window_Onload()
	document.all("lblAccName").innerText =document.all("strAccName").value
	document.all("lblAmount").innerText = formatcurrency(document.all("intAmount").value,2)
	document.all("lblCurrentAmt").innerText = formatcurrency(document.all("strCurrentAmt").value,2)
	document.all("lblBalance").innerText = formatcurrency(document.all("intBalance").value,2)
	IF frmMain.strStatus.value="Yes" then
		window.returnValue = document.all("intAmount").value
		parent.window.close
	END IF
	IF frmMain.strStatus.value="No" then
		msgbox "Account is not Active"
	END IF
End Sub

Sub Check4Enter()
	If window.event.keycode = 13 Then
		window.event.cancelbubble = True
		window.event.returnvalue = FALSE
		SelectChg()
	End If
End Sub

Sub SelectChg()
	IF LEN(trim(frmMain.intCustAccID.value))>1 then
		frmMain.FormAction.value="SelectChg"
		frmMain.Submit()
	else
		msgbox "You must enter the Gift Card #"
	END IF
End Sub

Sub ProcessChg()
	IF LEN(trim(frmMain.intCustAccID.value))>0 then
		frmMain.FormAction.value="ProcessChg"
		frmMain.Submit()
	END IF
End Sub


Sub CancelChg()
	parent.window.close
End Sub

</script>

<%
'********************************************************************
' Server-Side Functions
'********************************************************************
Sub GetAccInfo(dbMain,intCustAccID,strCurrentAmt,intAmount,intAccAmt,strAccName,intRecID,LocationID,LoginID,intCtype)
	Dim strSQL, RS ,strSQL2,rsData2,strService
	strSQL="SELECT CustAcc.CustAccID,client.Ctype, CustAcc.CurrentAmt,client.fname + ' ' + client.lname AS AccName"&_
	" FROM CustAcc(Nolock)"&_
	" INNER JOIN client(Nolock) ON CustAcc.ClientID = client.ClientID"&_
	" WHERE CustAcc.CustAccID =" & intCustAccID 
	If DBOpenRecordset(dbMain,rs,strSQL) Then
		If Not RS.EOF Then
			strAccName = RS("AccName")
			strCurrentAmt = RS("CurrentAmt")
			intCtype = RS("Ctype")
			IF RS("Ctype") = 1 then
				intAmount = formatnumber(intAccAmt,2)
			ELSE
			'Check for service Only
				strSQL2=" SELECT SUM(RECITEM.Price) as Service FROM RECITEM(nolock)"&_
						" INNER JOIN Product(nolock) ON RECITEM.ProdID = Product.ProdID"&_
						" AND ((CAT = 1 OR CAT = 9 OR CAT = 13 OR CAT = 14 OR CAT = 15 OR CAT = 16)"&_
						" OR  (CAT = 2 OR CAT = 10 OR CAT = 17 OR CAT = 18 OR CAT = 19 OR CAT = 20))"&_
						" WHERE (RECITEM.recId = "& intRecID & ") and RECITEM.LocationID="& LocationID
				If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
					IF NOT rsData2.eof then
						strService = rsData2("Service")
					END IF
				END IF
				intAmount = formatnumber(strService,2)
			END IF
		ELSE
			intCtype = 0
			intCustAccID = ""
			strCurrentAmt = ""
			intAmount = ""
			strAccName = ""
		End If
	End If
End Sub


Sub ProcessChg(dbMain)
	Dim strSQL,rsData,MaxSQL,intAmount,intCustAccID,intCustAccTID,strCurrentAmt,strNewAmt,intRecID
	Dim intTXCustID
	intRecID = request("intRecID")
	MaxSQL = " Select ClientID from REC (NOLOCK) where RecID="&intRecID
	If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
		intTXCustID=rsData(0)
	End if
	Set rsData = Nothing



	intCustAccID = request("intCustAccID")
	intAmount = ABS(request("intAmount"))
	strCurrentAmt = request("strCurrentAmt")
	strNewAmt = cdbl(strCurrentAmt) - cdbl(intAmount)
	intAmount =  cdbl(intAmount)*-1
	strSQL= "Update CustAcc SET "&_
		" CurrentAmt=" & strNewAmt &","&_
		" LastUpdate='" & date() &"',"&_
		" LastUpdateBy=" & request("LoginID") &_
		" Where CustAccID ='"& intCustAccID & "'"
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
	MaxSQL = " Select ISNULL(Max(CustAccTID),0)+1 from CustAccHist (NOLOCK)"
	If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
		intCustAccTID=rsData(0)
	End if
	Set rsData = Nothing
	strSQL= "Insert into CustAccHist (CustAccTID,CustAccID,TXCustID,TXDte,TXType,TXAmt,TXUser,TXRecID,TXLocationID) Values ("&_
		intCustAccTID &","&_
		intCustAccID &","&_
		intTXCustID &","&_
		"'" & Date() &"',"&_
		"'Ticket',"&_
		intAmount &","& request("LoginID") &","& intRecID &","&  request("LocationID") &")"
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
End Sub


%>
