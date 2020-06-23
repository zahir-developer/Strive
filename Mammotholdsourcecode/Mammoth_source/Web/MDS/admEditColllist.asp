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
'********************************************************************
' Global Variables
'********************************************************************
'********************************************************************
' Main
'********************************************************************
Call Main

'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
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
<table cellspacing="0" width="380" class="Data">
	<tr>
    <td class="Header" align="center" width="20">Act #</td>
    <td class="Header" align="center" width="80">Type</td>
    <td class="Header" align="center" width="80">Date</td>
    <td class="Header" align="center" width="160">Description</td>
    <td class="Header" align="center" width="50">Amount</td>
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
	Dim htmlDataRow,strDisplay,strTitle
	Dim db,strSQL,rs, intUserID,intCollID,LocationID,LoginID
	Set db = OpenConnection

   intUserID=Request("intUserID")
   intCollID=Request("intCollID")
    LocationID = request("LocationID")
LoginID = request("LoginID")

	strSQL =" SELECT * From UserCol (Nolock) where UserID=" & intUserID &" and LocationID=" & LocationID   &" and CollID="& intCollID &" Order by ActID"
    IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF
				strTitle = SQLReplace(trim(rs("ActDesc")))
				IF LEN(TRIM(strTitle)) > 23 then
					strDisplay = left(strTitle,20)& "..."
				ELSE
					strDisplay = strTitle
				END IF

				htmlDataRow = htmlDataRow & "<tr><td class=data width=20  align=right >"&  NullTest(rs("ActID")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td class=data width=80 align=right>" & NullTest(rs("ActType")) & "&nbsp;</td>" 
				htmlDataRow = htmlDataRow & "<td class=data width=80 align=right>" & NullTest(rs("ActDate")) & "&nbsp;</td>" 
				htmlDataRow = htmlDataRow & "<td title='"& strTitle &"' class=data width=160 align=left>&nbsp;" & strDisplay & "&nbsp;</td>" 
				htmlDataRow = htmlDataRow & "<td class=data align=right width=50>" & formatcurrency(rs("ActAmt")) & "&nbsp;</td></tr>" 
				
				rs.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=8 align=""center"" Class=""data"">No records were found.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=8 align=""center"" Class=""data"">No records were found.</td></tr>"
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
%>
 

