 <%@ LANGUAGE="VBSCRIPT" %>
<%
'********************************************************************
'********************************************************************
Option Explicit
Response.Expires = 0
Response.Buffer = True
Dim Title,strTests

'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incCommon.asp"-->
<!--#include file="incDatabase.asp"-->
<%
'********************************************************************
' MAIN
'********************************************************************
Call Main()
Sub Main
	Dim dbMain,strSQL,htmlTable,userid
	Set dbMain = OpenConnection

userid = request("userid")

'********************************************************************
' HTML
'********************************************************************
%>
<html>

<head>
<link rel="stylesheet" href="main.css" type="text/css">
<title></title>
</head>
<input type="hidden" name="userid" ID="userid" value="<%=userid%>">
<input type="hidden" name="filterby" ID="filterby" value="<%=Request("FilterBy")%>">
<body class="pgbody" onclick="ProcClick()">
 <div align="center">
    <table cellspacing="0" width="700" class="data">
      <tr>
        <td width="20" class="header" align="center" valign="bottom">Bay</td>
        <td width="130" class="header" align="center" valign="bottom">Time In</td>
       <td width="120" class="header" align="center" valign="bottom">Client</td>
       <td width="150" class="header" align="center" valign="bottom">Vehical</td>
         <td width="130" class="header" align="center" valign="bottom">Est. Out</td>
       <td width="150" class="header" align="center" valign="bottom">Service</td>
      </tr>
	<%=DoDataRow()%>
   </table>
</div>
</body>
</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="vbscript">
Option Explicit

Sub Window_OnLoad()
	
End Sub
Sub ShowDlgBox(intrecid)
	Dim strDlg
	strDlg = ShowModalDialog("TimeDetailByDlg.asp?intRecID="& intrecid ,"","dialogwidth:650px;dialogheight:600px;")
		parent.fraMain.location.href = "TimeDetailFra.asp?userid="& document.all("userid").value

End Sub

</script>
<%

dbMain.close
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************


Function DoDataRow()
    Dim  htmlDataRow, db,rsData,rsData2, rsData3, strSQL, strLineColor
    dim  intLine,strVehical,strService,strMOD,strUPCHARGE,strTitle,intBay,strClient
    Dim strStarttime,strEndtime,strHour,intOffset
    Dim strReceivedDate, strDisplayLab, strStatus,strOnClick
 	Set db = OpenConnection
   
strSQL = " SELECT distinct REC.Line,REC.recid,REC.DayCnt, REC.esttime, REC.datein,REC.vModel, REC.Status, "&_
			" LM_ListItem.ListDesc AS vehman, LM_ListItem1.ListDesc AS vehmod, "&_
			" LM_ListItem2.ListDesc AS vehcolor,"&_
			" client.fname + ' ' + client.lname AS Name, client.Phone,DetailComp.RecID as DetailComp"&_
			" FROM REC (NOLOCK)"&_
			" INNER JOIN LM_ListItem(NOLOCK) ON REC.VehMan = LM_ListItem.ListValue AND LM_ListItem.ListType = 3"&_
			" INNER JOIN LM_ListItem LM_ListItem1(NOLOCK) ON REC.VehMod = LM_ListItem1.ListValue AND LM_ListItem1.ListType = 4"&_
			" INNER JOIN LM_ListItem LM_ListItem2(NOLOCK) ON REC.VehColor = LM_ListItem2.ListValue AND LM_ListItem2.ListType = 5 " & _
			" LEFT OUTER JOIN client ON REC.clientid = client.clientid"&_
			" LEFT OUTER JOIN DetailComp ON REC.recid = DetailComp.RecID"&_
			" WHERE (day(REC.Datein) = day('"& Date() &"')"&_
			" AND month(REC.Datein) = month('"& Date() &"')"&_
			" AND year(REC.Datein) = year('"& Date() &"'))"&_
			" AND REC.Line > 3"&_
			" AND REC.Status < 20"&_
			" Order By Rec.Line,REC.Datein asc"
	intOffset = 30
	strStarttime = "7:00:00 AM"
	strEndtime = "6:00:00 PM"
	strHour = "00:00:00 AM"
	intLine = 1
	If dbopenrecordset(db,rsData,strSQL)  Then
		IF NOT 	rsData.EOF then
			Do while Not rsData.EOF
				strOnClick = "Call ShowDlgBox('" & rsData("recid") & "')"
				IF instr(1,trim(rsData("vehmod")),"(") > 0 then
					strMOD = SQLReplace(LEFT(trim(rsData("vehmod")),instr(1,trim(rsData("vehmod")),"(")-1))
					strUPCHARGE = SQLReplace(MID(trim(rsData("vehmod")),instr(1,trim(rsData("vehmod")),"("),3))
				ELSE
					strMOD = SQLReplace(trim(rsData("vehmod")))
					strUPCHARGE = ""
				END IF
				strClient = SQLReplace(trim(rsData("Name")))
			
				strVehical = trim(rsData("vehcolor"))&" "&SQLReplace(trim(rsData("vehman")))&" "&trim(rsData("vModel"))&" "&trim(rsData("vehmod"))

				strLineColor = "Bstatusyellow"
				 strStatus= trim(rsData("Status"))
				 strService =""
				IF rsData("line") = "4" then
					intBay = 1
				ELSEIF rsData("line") = "5" then
					intBay = 2
				ELSEIF rsData("line") = "6" then
					intBay = 3
				END IF
	
	
					

				IF (rsData("DetailComp")) > 0 THEN
					strLineColor = "Bstatusgreen"
				ELSE
					strLineColor = "Bstatusyellow"
				END IF

				'IF rsData("esttime") <  now() THEN
				'	strLineColor = "BstatusRed"
				'end if
				'IF Trim(rsData("status")) >= 50 THEN
				'	strLineColor = "Bstatusgreen"
				'end if
				strTitle = SQLReplace((trim(rsData("vehcolor")))&" "&(trim(rsData("vehman")))&" "&strMOD)
				
				htmlDataRow = htmlDataRow & "<tr><td Title='"& strTitle &"' class="& strLineColor &" align=center>&nbsp;<Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>" & intbay & "</Label></td>"
				htmlDataRow = htmlDataRow & "<td Title='"& strTitle &"' class="& strLineColor &" align=Right>&nbsp;<Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>" & formatdatetime(rsData("datein"),vbLongTime) & "&nbsp;</Label></td>"
				htmlDataRow = htmlDataRow & "<td Title='"& strTitle &"' class="& strLineColor &"  align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>"  & strClient  & "&nbsp;</Label></td>"
				htmlDataRow = htmlDataRow & "<td Title='"& strTitle &"' class="& strLineColor &"  align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>"  & strVehical  & "&nbsp;</Label></td>"
				htmlDataRow = htmlDataRow & "<td Title='"& strTitle &"' class="& strLineColor &"  align=Right><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>" & rsData("esttime") & "</Label></td>"
				strService = ""
				strSQL = "Select * From RECItem (NOLOCK) WHERE RECID=" & rsData("recid")
				If DBOpenRecordset(db,rsData2,strSQL) Then
					DO While NOT rsData2.eof 
						Select Case rsData2("Prodid")
						Case  6
							strService = "#1 Ultimate"
						Case  11
							strService = "#2 Full"
						Case  12
							strService = "#3 Interior"
						Case  39
							strService = "#4 Express"
						Case  13
							strService = "#5 Exterior"
						Case  40
							strService = "#6 Polish & Wax"
						Case  14
							strService = "#7 Wash & Wax"
						Case  15
							strService = trim(strService)+" NoENG"
						Case  16
							strService = trim(strService)+" *A"
						Case  17
							strService = trim(strService)+" *B"
						Case  18
							strService = trim(strService)+" *C"
						Case  19
							strService = trim(strService)+" *D"
						Case  20
							strService = trim(strService)+" *E"
						End Select
						rsData2.MoveNext
					Loop
				END IF
				strService = trim(strService)+" "+trim(strUPCHARGE)
				htmlDataRow = htmlDataRow & "<td Title='"& strTitle &"' class="& strLineColor &"  align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>&nbsp;" & strService & "</Label></td></tr>"
				intLine=intLine+1
				rsData.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<td colspan=7 align=""center"" Class=""data"">No Vehicals.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=7 align=""center"" Class=""data"">No Vehicals.</td></tr>"
	END IF
	DoDataRow = htmlDataRow
	Set rsData = Nothing
	Call CloseConnection(db)
End Function

Function CheckTest(var)
		If var = 1 Then
			CheckTest = "<label>*</label>"
		Else
			CheckTest = "&nbsp;"
		End If
End Function

%>