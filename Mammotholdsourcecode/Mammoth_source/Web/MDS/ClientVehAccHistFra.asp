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

Dim intCustAccID
intCustAccID=Request("intCustAccID")
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
<input type="hidden" name="intCustAccID" tabindex="-2" value="<%=intCustAccID%>">
<table cellspacing="0" width="584" class="Data">
	<tr>
     <td class="Header" width="100" >Date</td>
    <td class="Header"  width="100">Type</td>
    <td class="Header"  width="100">Ticket #</td>
    <td class="Header"  width="100">Amount</td>
    <td class="Header"  width="200">Note</td>
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
	Dim db,strSQL,rs,strLoc,intListCode,rowColor,intCustAccID,strqaDate,strTitle
	Set db = OpenConnection

	intCustAccID = Request("intCustAccID")

               
	strSQL =" SELECT top(20) TXDte,TXType,TXAmt,TXrecID,TXNote "&_
			" FROM CustAccHist(nolock) "&_
			" WHERE CustAccHist.CustAccID = '"& intCustAccID &"'"&_
			" order by CustAccHist.CustAccTID desc"
    IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF

				rowColor = "data"
				strDescTitle =  NullTest(rs("TXNote"))
				IF LEN(NullTest(rs("TXNote")))>60 then
					strTitle = LEFT(NullTest(rs("TXNote")),57)+"..."
				ELSE
					strTitle = NullTest(rs("TXNote"))
				END IF
				
				htmlDataRow = htmlDataRow & "<tr><td align=left  class="& rowColor &">&nbsp;"  & NullTest(rs("TXDte")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td class="& rowColor &">&nbsp;" &  NullTest(rs("TXType")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td class="& rowColor &">&nbsp;" &  NullTest(rs("TXRecID")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=right class="& rowColor &">" &  formatcurrency(rs("TXAmt"),2) & "&nbsp;</td>"
				htmlDataRow = htmlDataRow & "<td align=left Title='"& strDescTitle &"' class="& rowColor &">" &  strTitle & "</td></tr>"

				rs.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=5 align=""center"" Class=""data"">No records were found.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=5 align=""center"" Class=""data"">No records were found.</td></tr>"
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
 

