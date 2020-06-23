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
<!--#include file="incCommon.asp"-->
<!--#include file="incSecurity.asp"-->
<%
Main
Sub Main
	Dim dbMain, hdnCustAccID,intClientID,hdnvehid,intvehid,intVehNum,strvModel,LoginID,LocationID
	Dim MaxSQL,rsData,strSQL,intCustAccID,hdnArray
	dim strCurrentAmt, strMonthlyCharge, strLimit, strCCNo, intCCType
	Set dbMain =  OpenConnection


	hdnCustAccID = Request("hdnCustAccID")
	LoginID = Request("LoginID")
	LocationID = Request("LocationID")
	hdnArray = split(hdnCustAccID,"|")
	intCustAccID = hdnArray(0)
	intvehid = hdnArray(1)
	intClientID = hdnArray(2)
	
	If intCustAccID = 0 then
		MaxSQL = " Select ISNULL(Max(CustAccID),0)+1 from CustAcc (NOLOCK)"
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			intCustAccID=rsData(0)
		End if
		Set rsData = Nothing
		strSQL= "Insert into CustAcc (LocationID,CustAccID,ClientID,vehid,ActiveDte,"&_
				"LastUpdate,LastUpdateBy,Type,MonthlyCharge,Limit,status) Values ("&_
			LocationID &","&_
			intCustAccID &","&_
			intClientID &","&_
			intvehid &","&_
			"'" & Date() &"',"&_
			"'" & Date() &"',"&_
			LoginID &","&_
			"2,0,0,"&_
			"1)"
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
<Title>Client Vehical Account</title>
</head>
<body class="pgbody">
<form method=post name="frmMain">
        <input type=hidden name="intClientID" value="<%=intClientID%>"></td>
        <input type=hidden name="intvehid" value="<%=intvehid%>"></td>
        <input type=hidden name="intCustAccID" value="<%=intCustAccID%>"></td>
        <input type=hidden name="LoginID" value="<%=LoginID%>"></td>
        <input type=hidden name="LocationID" value="<%=LocationID%>"></td>
<div style="text-align:center">
<table  border="0" width="650" cellspacing="0" cellpadding="0">
<iframe align="center" Name="fraMain" src="ClientVehAccEditDlgFra.asp?intCustAccID=<%=intCustAccID%>&intClientID=<%=intClientID%>&intvehid=<%=intvehid%>&LoginID=<%=LoginID%>&LocationID=<%=LocationID%>"   height="400" width="650" frameborder="0"></iframe>
</table>
</div>
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
<script language="VBSCRIPT">
Option Explicit

Sub Window_Onload()
End Sub

</script>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
