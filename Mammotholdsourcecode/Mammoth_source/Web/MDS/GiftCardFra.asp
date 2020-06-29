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

Dim intGiftCardID,LocationID,LoginID
intGiftCardID=Request("intGiftCardID")
        LocationID = request("LocationID")
    LoginID = request("LoginID")

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
<input type="hidden" name="intGiftCardID" tabindex="-2" value="<%=intGiftCardID%>">
            <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />


<table cellspacing="0" width="484" class="Data">
	<tr>
     <td class="Header" width="100" >Date</td>
    <td class="Header"  width="150">Type</td>
    <td class="Header"  width="100">Ticket #</td>
    <td class="Header"  width="150">Amount</td>
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
</script>

<%
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
	Dim htmlDataRow,strDesc,strDescTitle,strStatus,strTax
	Dim db,strSQL,rs,strLoc,intListCode,rowColor,intGiftCardID,strqaDate,strTitle
	Set db = OpenConnection

	intGiftCardID = Request("intGiftCardID")

               
	strSQL =" SELECT TransDte,TransType,TransAmt,recID "&_
			" FROM GiftCardHist(nolock) "&_
			" WHERE GiftCardHist.GiftCardID = '"& intGiftCardID &"'"&_
			" order by GiftCardHist.GiftCardTID"
    IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF

				rowColor = "data"

				htmlDataRow = htmlDataRow & "<tr><td align=left  class="& rowColor &">&nbsp;"  & NullTest(rs("TransDte")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td class="& rowColor &">&nbsp;" &  NullTest(rs("TransType")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td class="& rowColor &">&nbsp;" &  NullTest(rs("RecID")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=right class="& rowColor &">" &  formatcurrency(rs("TransAmt"),2) & "&nbsp;</td></tr>"

				rs.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=3 align=""center"" Class=""data"">No records were found.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=3 align=""center"" Class=""data"">No records were found.</td></tr>"
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
 

