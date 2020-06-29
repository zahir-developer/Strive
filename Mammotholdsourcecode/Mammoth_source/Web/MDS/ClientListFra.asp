<%@  language="VBSCRIPT" %>
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
	Dim cnt,hdnClientID,LocationID,LoginID
	cnt = 0
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
<body class="pgframe">
    <input type="hidden" name="cnt" tabindex="-2" value="<%=cnt%>">
    <input type="hidden" name="hdnClientID" tabindex="-2" value="<%=hdnClientID%>">
    <input type="hidden" name="LocationID" value="<%=LocationID%>" />
    <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />

    <table cellspacing="0" width="752" class="Data">
        <tr>
            <td class="Header" width="40">#</td>
            <td class="Header" width="100">Last Name</td>
            <td class="Header" width="100">First Name</td>
            <td class="Header" width="100">Phone #1</td>
            <td class="Header" width="250">Address</td>
            <td class="Header" width="80">Type</td>
            <td class="Header" width="80">Status</td>
        </tr>
        <%=DoDataRow(cnt,hdnClientID)%>
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
	IF document.all("cnt").value = 1 then
		parent.location.href="ClientEdit.asp?hdnClientID=" &document.all("hdnClientID").value &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	END IF

End Sub

</script>

<%
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow(cnt,hdnClientID)
	Dim htmlDataRow,strDesc,strDescTitle,strClientName,strCorpTitle
	Dim db,strSQL,rs,strLoc,intListCode,rowColor,strFilter,strqaDate,strTitle
	Set db = OpenConnection

	strFilter = Request("hdnFilterBy")

	if strFilter= "undefined" then
	strFilter=""
	end if
	'Response.Write strFilter
	

    strSQL =""
	If Len(trim(strFilter)) > 0 Then

'Response.End
	
               
	strSQL =" SELECT client.ClientID,client.fname, client.lname, client.addr1, client.Phone,"&_
			" client.Phone2, LM_ListItem.ListDesc AS ctype, LM_ListItem1.ListDesc AS status"&_
			" FROM client(nolock)"&_
			" INNER JOIN LM_ListItem (nolock) ON client.Ctype = LM_ListItem.ListValue"&_
			" INNER JOIN LM_ListItem LM_ListItem1 (nolock) ON client.status = LM_ListItem1.ListValue"&_
			" WHERE (LM_ListItem.ListType = 8) AND (LM_ListItem1.ListType = 9)"
		strSQL = strSQL & strFilter

'Response.Write strSQL
'Response.End
	End if
	cnt = 0 
    if LEN(TRIM(strSQL))>0 then
        IF dbOpenStaticRecordset(db, rs, strSQL) then   
		    IF NOT 	rs.EOF then
			    Do while Not rs.EOF
				    hdnClientID = rs("ClientID")
				    rowColor = "data"
				    htmlDataRow = htmlDataRow & "<tr><td class="& rowColor &"><a target=body href=""ClientEdit.asp?hdnClientID=" & rs("ClientID") & "&LocationID=" & request("LocationID") & "&LoginID=" & request("LoginID") & """>" &  rs("ClientID") & "</td>" 
				    htmlDataRow = htmlDataRow & "<td class="& rowColor &"><a target=body href=""ClientEdit.asp?hdnClientID=" & rs("ClientID") & "&LocationID=" & request("LocationID") & "&LoginID=" & request("LoginID") &  """>" &  rs("lname") & "</td>" 
				    htmlDataRow = htmlDataRow & "<td class="& rowColor &"><a target=body href=""ClientEdit.asp?hdnClientID=" & rs("ClientID") & "&LocationID=" & request("LocationID") & "&LoginID=" & request("LoginID") &  """>" &  rs("fname") & "</td>" 
				    htmlDataRow = htmlDataRow & "<td class="& rowColor &">" &  NullTest(rs("Phone")) & "</td>" 
				    htmlDataRow = htmlDataRow & "<td align=left class="& rowColor &">" & NullTest(rs("addr1")) & "</td>" 
				    htmlDataRow = htmlDataRow & "<td align=center class="& rowColor &">" & NullTest(rs("CType")) & "</td>"
				    htmlDataRow = htmlDataRow & "<td align=center class="& rowColor &">" & NullTest(rs("Status")) & "</td></tr>"
				    cnt = cnt+1
				    rs.MoveNext
			    Loop	
		    ELSE
				    htmlDataRow = "<tr><td colspan=8 align=""center"" Class=""data"">No records were selected.</td></tr>"
		    END IF
	    ELSE
			    htmlDataRow = "<tr><td colspan=8 align=""center"" Class=""data"">No records were selected.</td></tr>"
	    END IF
	ELSE
			htmlDataRow = "<tr><td colspan=8 align=""center"" Class=""data"">No records were selected.</td></tr>"
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
 

