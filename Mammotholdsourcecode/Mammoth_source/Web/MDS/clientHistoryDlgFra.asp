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
<table style="width:584px" class="Data">
	<tr>
    <td class="Header"  style="width:60px">Ticket #</td>
     <td class="Header" style="width:100px" >Date</td>
    <td class="Header"  style="width:100px">Service</td>
    <td class="Header"  style="width:100px">Detailer</td>
    <td class="Header"  style="width:80px">Amount</td>
    <td class="Header"  style="width:80px">Price</td>
    <td class="Header"  style="width:80px">Comm</td>
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
	Dim htmlDataRow,strDesc,strDescTitle,strStatus,strTax
	Dim db,strSQL,rs,strLoc,intListCode,rowColor,hdnClientID,strTitle
	Set db = OpenConnection

	hdnClientID = Request("hdnClientID")

               
	strSQL =" SELECT REC.recid, REC.datein, Product.Descript, "&_
		 " ISNULL(LM_Users.LastName, '') AS detail, RECITEM.Price,  "&_
		 " ISNULL(REC.gtotal, 0) AS Paid, isnull(RECITEM.Comm,0)as Comm "&_
		 " FROM LM_Users (nolock)"&_
		 " INNER JOIN DetailComp(nolock) ON LM_Users.UserID = DetailComp.UserID RIGHT "&_
		 " OUTER JOIN REC(nolock) "&_
		 " INNER JOIN RECITEM (nolock)ON REC.recid = RECITEM.recId and REC.LocationID = RECITEM.LocationID "&_
		 " INNER JOIN Product (nolock)ON RECITEM.ProdID = Product.ProdID ON DetailComp.RecID = REC.recid "&_
		 " WHERE (REC.ClientID = "& hdnClientID &") AND (Product.Descript <> 'None') "&_
		 " ORDER BY REC.recid DESC"
'  Response.Write strsql
 ' Response.End
  
    IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF

				rowColor = "data"
				
				htmlDataRow = htmlDataRow & "<tr><td align=left  class="& rowColor &">&nbsp;"  & NullTest(rs("recid")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td class="& rowColor &">&nbsp;" &  formatdatetime(NullTest(rs("datein")),2) & "</td>" 
				htmlDataRow = htmlDataRow & "<td class="& rowColor &">&nbsp;" &  NullTest(rs("Descript")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td class="& rowColor &">&nbsp;" &  NullTest(rs("detail")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=right class="& rowColor &">" &  formatcurrency(rs("Price"),2) & "&nbsp;</td>"
				htmlDataRow = htmlDataRow & "<td align=right class="& rowColor &">" &  formatcurrency(rs("Paid"),2) & "&nbsp;</td>"
				htmlDataRow = htmlDataRow & "<td align=right class="& rowColor &">" &  formatcurrency(rs("Comm"),2) & "&nbsp;</td></tr>"

				rs.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=7 align=""center"" Class=""data"">No records were found.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=7 align=""center"" Class=""data"">No records were found.</td></tr>"
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
 

