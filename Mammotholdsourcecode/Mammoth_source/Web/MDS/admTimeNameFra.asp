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
Dim hdnSheetID,hdnUserID,LocationID,LoginID
	hdnSheetID = Request("hdnSheetID")
	hdnUserID = Request("hdnUserID")
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
    <form method="post" name="admTimeNameFra" action="admTimeNameFra.asp?hdnSheetID=<%=Request("hdnSheetID")%>&hdnUserID=<%=Request("hdnUserID")%>&LocationID=<%=Request("LocationID")%>&LoginID=<%=Request("LoginID")%>">
        <input type="hidden" name="hdnSheetID" value="<%=hdnSheetID%>" />
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" value="<%=LoginID%>" />
        <% IF isnull(hdnSheetID) or len(hdnSheetID)=0 then %>
        <table style="width: 100%; border-collapse: collapse;" class="Data">
            <tr>
                <td style="width: 100%; text-align: center" class="data">No Week Selected.</td>
            </tr>
        </table>
        <% Else %>
        <table style="width: 100%; border-collapse: collapse;" class="Data">
            <tr>
                <td class="Header" style="width: 10%; text-align: center"></td>
                <td class="Header" style="width: 45%; text-align: center">Last Name</td>
                <td class="Header" style="width: 45%; text-align: center">First Name</td>
            </tr>
            <%=DoDataRow()%>
        </table>
        <% End IF %>
    </form>
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

Sub Delname(strRowID)
	Dim arrData,hdnUserID,hdnSheetID,strReturnVal
	window.event.cancelBubble = false
	arrData = Split(strRowID,"~")	
	hdnSheetID = arrData(0)
	hdnUserID = arrData(1)
	strReturnVal = ShowModalDialog("admTimeDelNameDlg.asp?hdnSheetID="& hdnSheetID &"&hdnUserID="& hdnUserID &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,,"center:-2;dialogwidth:200px;dialogheight:200px;")
	parent.fraName.location.href = "admTimeNameFra.asp?hdnSheetID=<%=Request("hdnSheetID")%>&hdnUserID=<%=Request("hdnUserID")%>&LocationID=<%=Request("LocationID")%>&LoginID=<%=Request("LoginID")%>"
	parent.fraMain2.location.href = "admTimeCardFra.asp?hdnSheetID=<%=Request("hdnSheetID")%>&hdnUserID=<%=Request("hdnUserID")%>&LocationID=<%=Request("LocationID")%>&LoginID=<%=Request("LoginID")%>"
	window.event.returnValue = False 
End Sub 

</script>

<%
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
	Dim htmlDataRow,strDesc,hdnSheetID ,strRowID
	Dim db,strSQL,rs,strSQL2,rs2,hdnUserID,strStatus,rowColor,LocationID,LoginID
	Set db = OpenConnection

	hdnSheetID = Request("hdnSheetID")
	hdnUserID = Request("hdnUserID")
    LocationID = request("LocationID")
    LoginID = request("LoginID")

    IF  isnull(hdnUserID) or len(hdnUserID)=0 then
    hdnUserID = 0
    END IF        
               
	strSQL =" SELECT LM_Users.FirstName, LM_Users.LastName,LM_Users.UserID "&_
		" FROM LM_Users with(nolock)"&_
        " INNER JOIN TimeSheet with(nolock) ON LM_Users.LocationID = TimeSheet.LocationID "&_
		" LEFT OUTER JOIN TimeClock with(nolock) ON LM_Users.UserID = TimeClock.UserID and TimeClock.LocationID=" & LocationID &_
		" LEFT OUTER JOIN DetailComp with(nolock) ON LM_Users.UserID = DetailComp.UserID and DetailComp.LocationID=" & LocationID &_
		" WHERE (TimeSheet.SheetID = "& hdnSheetID &") and TimeSheet.LocationID=" & LocationID &" "&_
		" AND (TimeClock.Cdatetime BETWEEN TimeSheet.weekof AND DATEADD(day, 7, TimeSheet.weekof)"&_
		" OR DetailComp.CdateTime BETWEEN TimeSheet.weekof AND DATEADD(day, 7, TimeSheet.weekof))"&_
		" Group by LM_Users.FirstName, LM_Users.LastName,LM_Users.UserID Order by LM_Users.LastName"
    IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF
				strStatus = ""
                if len(trim(hdnUserID))>0 then
				    IF rs("UserID") = int(hdnUserID) then
					    rowColor = "Statusgreen"
				    ELSE 
					    rowColor = "data"
				    END IF
				ELSE 
					rowColor = "data"
				END IF

					strRowID = hdnSheetID & "~" & rs("UserID")
					htmlDataRow = htmlDataRow & "<tr>" 
					htmlDataRow = htmlDataRow & "<td class=""header"" onclick=""DelName('"& strRowID &"')"" style=""cursor:hand; width:10%; text-align:center; "" ><u>Delete</u></td>" 
					htmlDataRow = htmlDataRow & "<td  style=""cursor:hand; width:45%; text-align:left;"" class="& rowColor &"><a target=body href='admTime.asp?hdnUserID=" & rs("UserID") &"&hdnSheetID="& Request("hdnSheetID") & "&LocationID="& LocationID &"&LoginID="& LoginID &"'>" &  rs("LastName") & "</a></td>" 
					htmlDataRow = htmlDataRow & "<td  style=""cursor:hand; width:45%; text-align:left;"" class="& rowColor &"><a target=body href='admTime.asp?hdnUserID=" & rs("UserID") &"&hdnSheetID="& Request("hdnSheetID") & "&LocationID="& LocationID &"&LoginID="& LoginID &"'>" &  rs("FirstName") & "</a></td>" 
					htmlDataRow = htmlDataRow & "</tr>" 
				rs.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=""3"" style=""text-align:center"" Class=""data"">No records were found.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=""3"" style=""text-align:center"" Class=""data"">No records were found.</td></tr>"
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
 

