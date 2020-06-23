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
<%
Main
Sub Main
	Dim dbMain, intCustAccID,LocationID,LoginID
	Set dbMain =  OpenConnection

	intCustAccID = Request("intCustAccID")
    LocationID = request("LocationID")
    LoginID = request("LoginID")
	Dim strSQL,rsData,intInvoiceID,STMAmt,MaxSQL,strSQL2,intCustAccTID,intTXCustID,strTXAmt,strcurrentamt
		MaxSQL = " Select ISNULL(Max(InvoiceID),0)+1 from CustAccStm (NOLOCK)"
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			intInvoiceID=rsData(0)
		End if
		Set rsData = Nothing
			strSQL2= "UPDATE CustAccHist  set "&_
					"InvoiceID =" & intInvoiceID &_
					" Where CustAccID =" & intCustAccID &_
					" AND InvoiceID is null AND TXRecID > 0"
			IF NOT DBExec(dbMain, strSQL2) then
				Response.Write gstrMsg
				Response.End
			END IF
		MaxSQL = " Select SUM(TXAmt) from CustAccHist (NOLOCK) where InvoiceID =" & intInvoiceID &" Group by InvoiceID"
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			strTXAmt=rsData(0)
		End if
		Set rsData = Nothing
	
		MaxSQL = " Select SUM(TXAmt) from CustAccHist (NOLOCK) where CustAccID =" & intCustAccID
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			strCurrentAmt = CDBL(rsData(0)) + ABS(CDBL(strTXAmt)) 
		End if
		Set rsData = Nothing
	
		STMAmt =   cdbl(strCurrentAmt) + CDBL(strTXAmt)
		IF STMAmt > 0 then
			STMAmt = 0
		END IF
		strSQL= "Insert into CustAccStm (CustAccID,InvoiceID,STMDate,StmBy,STMAmt,currentamt,amount) Values ("&_
			intCustAccID &","&_
			intInvoiceID &","&_
			"'" & now() &"',"&_
			LoginID &","&_
			STMAmt &","&_
			strCurrentAmt &","&_
			strTXAmt & ")"
		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF

'		MaxSQL = " SELECT DISTINCT CustAccHist.TXCustID"&_
'				" FROM CustAccHist(NOLOCK)"&_
'				" WHERE CustAccHist.CustAccID = "&intCustAccID
'		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
'			intTXCustID = rsData(0)
'		End if
'		Set rsData = Nothing
'		MaxSQL = " Select ISNULL(Max(CustAccTID),0)+1 from CustAccHist (NOLOCK)"
'		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
'			intCustAccTID=rsData(0)
'		End if
'		Set rsData = Nothing
'		strSQL= "Insert into CustAccHist (CustAccID,TXCustID,CustAccTID,TXType,TXNote,TXAmt,InvoiceID,TXDte,TXuser) Values ("&_
'		intCustAccID &","&_
'		intTXCustID &","&_
'		intCustAccTID &","&_
'		"'Invoice',"&_
'		"'Invoice no."& cstr(intInvoiceID) &" ',"&_
'		 formatnumber(STMAmt*-1,2) &","&_
'		 intInvoiceID &","&_
'		"'" & now() &"',"&_
'		Session("UserID") &")"
'		IF NOT DBExec(dbMain, strSQL) then
'			Response.Write gstrMsg
'			Response.End
'		END IF

		Response.Redirect "admCrystalReportINV.asp?rpt=Invoice.rpt&@InvoiceID=" & intInvoiceID 
'********************************************************************
' HTML
'********************************************************************
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/VBSCRIPT">
Option Explicit

Sub Window_Onload()
		window.close
End Sub

</script>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
