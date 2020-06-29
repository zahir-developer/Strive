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
<table cellspacing="0" width="584" class="Data">
	<tr>
    <td class="Header"  width="60">Ticket #</td>
     <td class="Header" width="100" >Date</td>
    <td class="Header"  width="100">Service</td>
    <td class="Header"  width="80">Amount</td>
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
<script type="text/VBSCRIPT">
Option Explicit

Sub Window_OnLoad()
End Sub
</script>

<%
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
	Dim htmlDataRow,strDesc,strDescTitle,strStatus,strTax,intMonth
	Dim db,strSQL,rs,strLoc,intListCode,rowColor,hdnClientID,strTitle
	Set db = OpenConnection


	strSQL =" SELECT CustAccHist.TXRecID, Product.Descript, REC.CloseDte, REC.accamt"&_
			" FROM Product (NOLOCK)"&_
			" INNER JOIN RECITEM (NOLOCK) ON Product.ProdID = RECITEM.ProdID"&_
			" INNER JOIN REC (NOLOCK) ON RECITEM.recId = REC.recid "&_
			" INNER JOIN CustAccHist (NOLOCK) ON REC.recid = CustAccHist.TXRecID and REC.LocationID = CustAccHist.TXLocationID "&_
			" WHERE (CustAccHist.CustAccID = "& Request("intCustAccID") &")"&_
			" AND (Product.cat = 1 OR Product.cat = 2)"&_
			" AND (REC.LocationID = RECITEM.LocationID)"&_
			" AND (CustAccHist.InvoiceID is null)"&_
			" Order by REC.CloseDte desc"
  'Response.Write strsql
 'Response.End
  
    IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF

				rowColor = "data"
				
				htmlDataRow = htmlDataRow & "<tr><td align=left  class="& rowColor &">&nbsp;"  & NullTest(rs("TXRecID")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td class="& rowColor &">&nbsp;" &  NullTest(rs("CloseDte")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td class="& rowColor &">&nbsp;" &  NullTest(rs("Descript")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=right class="& rowColor &">" &  formatcurrency(rs("accamt"),2) & "&nbsp;</td></tr>"

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
Function NullZero(var)
	If IsNull(var) then
		NullZero = 0.0
	Else
		If Trim(var) = "" Then
			NullZero =0.0
		Else
			NullZero = var
		End If
	End If
End Function
%>
 

