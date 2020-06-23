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

Dim LocationID,LoginID
    LocationID = request("LocationID")
    LoginID = request("LoginID")

'********************************************************************
'HTML
'********************************************************************
%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css" />
    <title></title>
</head>
<body class="pgframe">
    <form method="post" name="admTimeWeekFra" action="admTimeweekFra.asp?hdnSheetID=<%=Request("hdnSheetID")%>&LocationID=<%=Request("LocationID")%>&LoginID=<%=Request("LoginID")%>">
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" value="<%=LoginID%>" />
        <table style="width: 100%; border-collapse: collapse;" class="Data">
            <tr>
                <td class="Header" style="width: 50%; text-align: center">Week</td>
                <td class="Header" style="width: 50%; text-align: center">Week No</td>
            </tr>
           <%=DoDataRow()%>
       </table>
    </form>
</body>
</html>

<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>

<%

'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
	Dim htmlDataRow,strDesc,strDescTitle,hdnSheetID 
	Dim db,strSQL,rs,strLoc,intListCode,rowColor,strStatus,strTitle,LocationID,LoginID
	Set db = OpenConnection
    
    LocationID = request("LocationID")
    LoginID = request("LoginID")
    hdnSheetID = request("hdnSheetID")

    IF  isnull(hdnSheetID) or len(hdnSheetID)=0 then
        hdnSheetID = 0
    END IF        
	strSQL ="SELECT TOP(5) TimeSheet.SheetID, TimeSheet.Weekno,TimeSheet.weekof FROM TimeSheet with (Nolock) WHERE TimeSheet.LocationID=" & LocationID &" order by yearno desc,weekNo desc"

    IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF
				IF rs("SheetID") = hdnSheetID then
					rowColor = "Statusgreen"
				ELSE
					rowColor = "data"
				END IF
					htmlDataRow = htmlDataRow & "<tr>" 
					htmlDataRow = htmlDataRow & "<td style=""cursor:hand; text-align=center"" highlight=""y"" class="& rowColor &"><a target=body href='admtime.asp?hdnSheetID=" & rs("SheetID") & "&LocationID="& LocationID &"&LoginID="& LoginID &"'>" &  rs("weekof") & "</a></td>" 
					htmlDataRow = htmlDataRow & "<td style=""cursor:hand; text-align=center"" highlight=""y"" class="& rowColor &"><a target=body href='admtime.asp?hdnSheetID=" & rs("SheetID") &  "&LocationID="& LocationID &"&LoginID="& LoginID &"'>" &  rs("weekNo") & "</td>" 
					htmlDataRow = htmlDataRow & "</tr>" 
				rs.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=2 align=""center"" Class=""data"">No records were found.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=2 align=""center"" Class=""data"">No records were found.</td></tr>"
	END IF
	DoDataRow = htmlDataRow
	Set RS = Nothing
    Call CloseConnection(db)
End Function

%>
 

