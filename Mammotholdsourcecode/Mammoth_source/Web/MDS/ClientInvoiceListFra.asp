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
Call Main
Sub Main
'********************************************************************
'HTML
'********************************************************************
%>
<html>
<head>
<link rel="stylesheet" href="main.css" type="text/css">
<title></title>
</head>
<body class=pgframe>
<table cellspacing="0" width="600" class="Data">
	<tr>
     <td class="Header" width="80" >&nbsp;</td>
    <td class="Header" align="center" width="300">Client</td>
    <td class="Header" align="center" width="80">Prev. Balance</td>
    <td class="Header" align="center" width="80">New Charge</td>
    <td class="Header" align="center" width="80">Total Due</td>
    </tr>
	<%=DoDataRow()%>

</table>

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
End Sub

Sub CreateInv(intCustAccID)
	Dim Answer,retDel
	window.event.cancelBubble = false
	retDel= ShowModalDialog("ClientInvoiceDlg.asp?intCustAccID="& intCustAccID  ,"","dialogwidth:800px;dialogheight:600px;")
		parent.fraMain.location.href = "ClientInvoiceListFra.asp"
	window.event.returnValue = False 
End Sub 

</script>

<%
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
	Dim htmlDataRow,strDesc,strDescTitle,strStatus,strTax,strCurrentAmt,strDue
	Dim db,strSQL,rs,strSQL2,rs2,strLoc,intListCode,rowColor,strFilter,strqaDate,strTitle,strtotalamt
	Set db = OpenConnection

	strFilter = Request("hdnFilterBy")

               
	strSQL =" SELECT CustAccHist.CustAccID,client.fname + ' ' + client.lname AS ClientName, "&_
			" SUM(REC.totalamt) as totalamt "&_
			" FROM CustAccHist (NOLOCK)"&_
			" INNER JOIN CustAcc (NOLOCK) ON CustAccHist.CustAccID = CustAcc.CustAccID "&_
			"  AND CustAcc.Type = 3 "&_
			" INNER JOIN client(NOLOCK) ON CustAcc.ClientID = client.ClientID "&_
			" INNER JOIN REC (NOLOCK) ON CustAccHist.TXRecID = REC.recid "&_
			" INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId "&_
			" INNER JOIN Product(NOLOCK) ON RECITEM.ProdID = Product.ProdID "&_
			" WHERE (CustAccHist.InvoiceID IS NULL "&_
			" AND Product.cat = 1) OR (CustAccHist.InvoiceID IS NULL and Product.cat = 2) "&_
			" GROUP BY CustAccHist.CustAccID, client.fname, client.lname"

	'Response.Write strsql&"<BR>"

    IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT rs.EOF then
			Do while Not rs.EOF
	'Response.Write rs("totalamt")&"<BR>"

				IF isnull(rs("totalamt")) then
					strtotalamt = 0.0
				ELSE
					strtotalamt =  rs("totalamt")
				END IF
				strSQL2 =" SELECT Sum(txAmt) as txAmt "&_
						" FROM CustAccHist (NOLOCK) Where CustAccHist.CustAccID="&rs("CustAccID")
				IF dbOpenStaticRecordset(db, rs2, strSQL2) then   
					IF NOT rs2.EOF then
						strCurrentAmt =  (CDBL(strtotalamt)+cdbl(rs2("txAmt")))*-1
					END IF
				END IF
	
				strDue =  CDBL(strtotalamt) + cdbl(strCurrentAmt)
				IF strDue < 0 then
					strDue = 0.0
				END IF
				strTitle = rs("ClientName")
				rowColor = "data"
				htmlDataRow = htmlDataRow & "<tr><td width=80 class=header align=center onclick=""CreateInv('"& rs("CustAccID") &"')"" style=""cursor:hand"" ><u>Generate</u></td>" 
				htmlDataRow = htmlDataRow & "<td Title=""" & strTitle & """  class="& rowColor &">&nbsp;" &  NullTest(rs("ClientName")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=right Title=""" & strTitle & """  class="& rowColor &">" & formatcurrency(NullZero(strCurrentAmt),2) & "&nbsp;</td>"
				htmlDataRow = htmlDataRow & "<td align=right Title=""" & strTitle & """  class="& rowColor &">" & formatcurrency(NullZero(strtotalamt),2) & "&nbsp;</td>"
				htmlDataRow = htmlDataRow & "<td align=right Title=""" & strTitle & """  class="& rowColor &">" & formatcurrency(NullZero(strDue),2) & "&nbsp;</td></tr>"

				rs.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=4 align=""center"" Class=""data"">No records were found.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=4 align=""center"" Class=""data"">No records were found.</td></tr>"
	END IF
	DoDataRow = htmlDataRow
	Set RS = Nothing
	Call CloseConnection(db)
'Response.End

End Function


Function NullTest(var)
	If IsNull(var) then
		NullTest = "&nbsp;"
	Else
		If Trim(var) = "" Then
			NullTest ="&nbsp;"
		Else
			NullTest = var
		End If
	End If
End Function
%>
 

