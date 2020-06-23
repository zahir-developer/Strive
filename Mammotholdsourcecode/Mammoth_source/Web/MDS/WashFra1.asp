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
	Dim dbMain,strSQL,htmlTable,dtDate,LocationID,LoginID
	Set dbMain = OpenConnection

dtDate = request("dtDate")
LocationID = request("LocationID")
LoginID = request("LoginID")

'********************************************************************
' HTML
'********************************************************************
%>
<html>

<head>
<link rel="stylesheet" href="main.css" type="text/css" />
<title></title>
</head>
<body class="pgbody">
<input type="hidden" name="dtDate" ID="dtDate" value="<%=dtDate%>" />
<input type="hidden" name="LocationID" ID="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" ID="LoginID" value="<%=LoginID%>" />
<input type="hidden" name="filterby" ID="filterby" value="<%=Request("FilterBy")%>" />
 <div style="text-align:center">
<table Class="tblcaption" style="width: 400px; border-collapse: collapse;">
   <tr>
   	<td class="tdcaption" style="text-align:left; background-image:url(images/header.jpg); width:200px">Washes for <%=dtDate%></td>
   	<td style="text-align:right">
			<button name="btnWash" style="width:75px" OnClick="NewWash()">Wash</button>&nbsp;&nbsp;&nbsp;
   	</td>
   </tr>
</table>
    <table style="width: 400px; border-collapse: collapse;" class="data">
      <tr>
         <td style="text-align:center; width:100px; vertical-align:bottom" class="header" >Time In</td>
       <td style="text-align:center; width:320px; vertical-align:bottom" class="header" >Client/Phone</td>
      </tr>
      <tr>
         <td style="text-align:center; width:100px; vertical-align:bottom" class="header" >Est. Out</td>
       <td style="text-align:center; width:320px; vertical-align:bottom" class="header" >Service</td>
      </tr>
   </table>
	<%=DoDataRow()%>
</div>
</body>
</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/vbscript">
Option Explicit

Sub Window_OnLoad()
	
End Sub

Sub ShowDlgBox(intrecid)
	Dim strDlg, arrDlg
	strDlg = ShowModalDialog("NewWashDlg.asp?intrecid=" & intrecid &"&dtDate="& document.all("dtDate").value &"&LocationID="& document.all("LocationID").value  &"&LoginID="& document.all("LoginID").value,,"center:1;dialogwidth:950px;dialogheight:750px;")
	IF LEN(strDlg) > 0 then
		parent.location.href = "Cashout.asp?intRecID="&strDlg &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	ELSE

		parent.WashFra1.location.href = "WashFra1.asp?dtDate="& document.all("dtDate").value &"&LocationID=" & document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
		'parent.WashFra2.location.href = "WashFra2.asp?dtDate="& document.all("dtDate").value &"&LocationID=" & document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
		'parent.WashFra3.location.href = "WashFra3.asp?dtDate="& document.all("dtDate").value &"&LocationID=" & document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	END IF
End Sub
Sub ShowPay(intrecid)
	parent.location.href = "Cashout.asp?intRecID="&intrecid &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
End Sub

Sub NewWash()
	Dim strDlg
	strDlg = ShowModalDialog("NewWashDlg.asp?intRecID=0&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,,"center:1;dialogwidth:950px;dialogheight:750px;")
	IF Len(strDlg) >0 then
		parent.location.href = "Cashout.asp?intRecID="&strDlg &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	ELSE
		parent.WashFra1.location.href = "WashFra1.asp?dtDate="& document.all("dtDate").value &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
		'parent.WashFra2.location.href = "WashFra2.asp?dtDate="& document.all("dtDate").value &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
		'parent.WashFra3.location.href = "WashFra3.asp?dtDate="& document.all("dtDate").value &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	END IF
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
    dim  intLine,strVehical,strService,strMOD,strUPCHARGE,strTitle
    Dim strStarttime,strEndtime,strHour,intOffset
    Dim strReceivedDate, strDisplayLab, strStatus,strOnClick,strOnPay
 	Set db = OpenConnection
   
strSQL = " SELECT REC.recid,REC.DayCnt, REC.esttime, REC.datein, REC.Status, "&_
			" LM_ListItem.ListDesc AS vehman, LM_ListItem1.ListDesc AS vehmod, "&_
			" LM_ListItem2.ListDesc AS vehcolor,"&_
			" client.fname + ' ' + client.lname AS Name, client.Phone"&_
			" FROM REC (NOLOCK)"&_
			" INNER JOIN LM_ListItem (NOLOCK) ON REC.VehMan = LM_ListItem.ListValue AND LM_ListItem.ListType = 3"&_
			" INNER JOIN LM_ListItem LM_ListItem1 (NOLOCK) ON REC.VehMod = LM_ListItem1.ListValue AND LM_ListItem1.ListType = 4"&_
			" INNER JOIN LM_ListItem LM_ListItem2 (NOLOCK) ON REC.VehColor = LM_ListItem2.ListValue AND LM_ListItem2.ListType = 5 " & _
			" LEFT OUTER JOIN client ON REC.ClientID = client.ClientID"&_
			" WHERE (day(REC.Datein) = day('"& request("dtDate") &"')"&_
			" AND month(REC.Datein) = month('"& request("dtDate") &"')"&_
			" AND year(REC.Datein) = year('"& request("dtDate") &"'))"&_
			" AND REC.Line <= 3"&_
			" AND REC.status < 70"&_
            " and REC.LocationID="& request("LocationID") &_
			" Order By REC.Datein asc"

'Response.Write strSQL
'Response.End
	intOffset = 30
	strStarttime = "7:00:00 AM"
	strEndtime = "6:00:00 PM"
	strHour = "00:00:00 AM"
	intLine = 1

    htmlDataRow = htmlDataRow & "<table style=""width: 400px; border-collapse: collapse;"" class=""data"">"



	If dbopenrecordset(db,rsData,strSQL)  Then
		IF NOT 	rsData.EOF then
			Do while Not rsData.EOF
				strOnClick = "Call ShowDlgBox('" & rsData("recid") & "')"
				strOnPay = "Call ShowPay('" & rsData("recid") & "')"
				IF instr(1,trim(rsData("vehmod")),"(") > 0 then
					strMOD = SQLReplace(LEFT(trim(rsData("vehmod")),instr(1,trim(rsData("vehmod")),"(")-1))
					strUPCHARGE = SQLReplace(MID(trim(rsData("vehmod")),instr(1,trim(rsData("vehmod")),"("),3))
				ELSE
					strMOD = SQLReplace(trim(rsData("vehmod")))
					strUPCHARGE = ""
				END IF
				strVehical = SQLReplace(trim(rsData("Name"))&" "&trim(rsData("Phone")))

				strLineColor = "data" 
				 strStatus= trim(rsData("Status"))
				 strService =""
				IF (rsData("status")) = 20 THEN
					strLineColor = "statusyellow"
				END IF

				IF rsData("esttime") <  now() THEN
					strLineColor = "statusRed"
				end if
				IF Trim(rsData("status")) >= 50 THEN
					strLineColor = "statusgreen"
				end if
				strTitle = SQLReplace((trim(rsData("vehcolor")))&" "&(trim(rsData("vehman")))&" "&strMOD)
				htmlDataRow = htmlDataRow & "<tr>"
				htmlDataRow = htmlDataRow & "<td>"
                htmlDataRow = htmlDataRow & "<table style=""width: 330px; border-collapse: collapse;"" class=""data"">"
				



				htmlDataRow = htmlDataRow & "<tr><td Title='"& strTitle &"' class="& strLineColor &" style=""text-align:left""><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>&nbsp;" & rsData("recid") & "</Label></td>"
				htmlDataRow = htmlDataRow & "<td Title='"& strTitle &"' class="& strLineColor &"  style=""text-align:left""><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>&nbsp;"  & strTitle  & "</Label></td></tr>"

				htmlDataRow = htmlDataRow & "<tr><td Title='"& strTitle &"' class="& strLineColor &" style=""text-align:right""><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>" & formatdatetime(rsData("datein"),vbLongTime) & "</Label></td>"
				htmlDataRow = htmlDataRow & "<td Title='"& strTitle &"' class="& strLineColor &"  style=""text-align:left""><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>&nbsp;"  & strVehical  & "</Label></td></tr>"

				htmlDataRow = htmlDataRow & "<tr><td Title='"& strTitle &"' class="& strLineColor &"  style=""text-align:right""><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>" & formatdatetime(rsData("esttime"),vbLongTime) & "</Label></td>"
				strService = ""
				strSQL = "Select * From RECItem (NOLOCK) WHERE RECID=" & rsData("recid") &" and RECItem.LocationID="& request("LocationID") 
				If DBOpenRecordset(db,rsData2,strSQL) Then
					DO While NOT rsData2.eof 
						Select Case rsData2("Prodid")
						Case  1
							strService = "Mini Mammoth"
						Case  2
							strService = "Mammoth"
						Case  3
							strService = "Mega Mammoth"
						Case  293
							strService = "Ultra Mammoth"
						Case  464
							strService = "Mini Mammoth"
						Case 463
							strService = "Mammoth"
						Case  462
							strService = "Mega Mammoth"
						Case  461
							strService = "Ultra Mammoth"
						Case  15
							strService = trim(strService)+" NoENG"
						Case  5
							strService = trim(strService)+" *A"
						Case  7
							strService = trim(strService)+" *B"
						Case  8
							strService = trim(strService)+" *C"
						Case  9
							strService = trim(strService)+" *D"
						Case  10
							strService = trim(strService)+" *E"
						End Select
						rsData2.MoveNext
					Loop
				END IF
				strService = trim(strService)+" "+trim(strUPCHARGE)
				htmlDataRow = htmlDataRow & "<td Title='"& strTitle &"' class="& strLineColor &"  style=""text-align:left""><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>&nbsp;" & strService & "</Label></td><tr><tr><td></td>"
				htmlDataRow = htmlDataRow & "</tr>"
                htmlDataRow = htmlDataRow & "</table>"
				htmlDataRow = htmlDataRow & "</td>"
				htmlDataRow = htmlDataRow & "<td>"
                htmlDataRow = htmlDataRow & "<table style=""width: 70px; border-collapse: collapse;"" class=""data"">"

				htmlDataRow = htmlDataRow & "<td style=""text-align:center; background-color:green; color:yellow;height:44px""><b><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnPay & """>Pay</Label></b></td>"


                htmlDataRow = htmlDataRow & "</table>"
				htmlDataRow = htmlDataRow & "</td>"
				htmlDataRow = htmlDataRow & "</tr>"


				htmlDataRow = htmlDataRow & "<tr><td></td></tr>"


				intLine=intLine+1
				rsData.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=7 align=""center"" Class=""data"">No Vehicals.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=7 align=""center"" Class=""data"">No Vehicals.</td></tr>"
	END IF
    htmlDataRow = htmlDataRow & "</table>"

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