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
	Dim dbMain,strSQL,htmlTable,dtDate,intreg,LocationID,LoginID
	Set dbMain = OpenConnection
    LocationID = request("LocationID")
    LoginID = request("LoginID")

	intreg= 1

dtDate = request("dtDate")
'********************************************************************
' HTML
'********************************************************************
%>
<html>

<head>
<link rel="stylesheet" href="main.css" type="text/css">
<title></title>
</head>
<body class="pgbody" >
<input type="hidden" name="dtDate" ID="dtDate" value="<%=dtDate%>"/>
<input type="hidden" name="filterby" ID="filterby" value="<%=Request("FilterBy")%>"/>
<input type="hidden" name="LocationID" ID="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" ID="LoginID" value="<%=LoginID%>" />
<input type="hidden" name="intreg" ID="intreg" value="<%=intreg%>"/>
 <div style="text-align:center">
    <table  style="width: 250px; border-collapse: collapse;" class="data">
      <tr>
        <td colspan=5 class="header" style="text-aligncenter; vertical-align:bottom">Bay #1</td>
      </tr>
      <tr>
         <td style="width:80px; text-align:center; vertical-align:bottom; " class="header">Time In</td>
       <td style="width:170px; text-align:center; vertical-align:bottom; " class="header">Client/Phone</td>
      </tr>
      <tr>
         <td style="width:80px; text-align:center; vertical-align:bottom; " class="header">Due Time</td>
       <td style="width:170px; text-align:center; vertical-align:bottom; " class="header">Service</td>
      <tr>
         <td style="width:80px; text-align:center; vertical-align:bottom; " class="header">Ticket #</td>
       <td style="width:170px; text-align:center; vertical-align:bottom; " class="header">Outside Service</td>
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
	Dim strDlg, arrDlg
	strDlg = ShowModalDialog("NewDetailDlg.asp?intrecid=" & intrecid &"&dtDate="& document.all("dtDate").value &"&LocationID="& document.all("LocationID").value  &"&LoginID="& document.all("LoginID").value,,"center:1;dialogwidth:950px;dialogheight:750px;")
	IF Len(strDlg) >0 then
		parent.location.href = "Cashout.asp?intRecID="&strDlg&"&intreg="&intreg &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	ELSE
		parent.SchedFra1.location.href = "SchedFra1.asp?dtDate="& document.all("dtDate").value &"&LocationID=" & document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
		parent.SchedFra2.location.href = "SchedFra2.asp?dtDate="& document.all("dtDate").value &"&LocationID=" & document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
		parent.SchedFra3.location.href = "SchedFra3.asp?dtDate="& document.all("dtDate").value &"&LocationID=" & document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
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
    Dim strStarttime,strEndtime,strHour,intOffset,strService2,strLineColor2
    Dim strReceivedDate, strDisplayLab, strStatus,strOnClick,strDueLineColor
 	Set db = OpenConnection
   
strSQL = " SELECT REC.recid,REC.DayCnt, REC.esttime, REC.datein, REC.Status, "&_
			" LM_ListItem.ListDesc AS vehman, LM_ListItem1.ListDesc AS vehmod, "&_
			" LM_ListItem2.ListDesc AS vehcolor,REC.VModel,"&_
			" client.fname + ' ' + client.lname AS Name, client.Phone, dbo.Product.Descript"&_
			" FROM REC (NOLOCK)"&_
			" INNER JOIN LM_ListItem(NOLOCK) ON REC.VehMan = LM_ListItem.ListValue AND LM_ListItem.ListType = 3"&_
			" INNER JOIN LM_ListItem LM_ListItem1(NOLOCK) ON REC.VehMod = LM_ListItem1.ListValue AND LM_ListItem1.ListType = 4"&_
			" INNER JOIN LM_ListItem LM_ListItem2(NOLOCK) ON REC.VehColor = LM_ListItem2.ListValue AND LM_ListItem2.ListType = 5 " & _
            " INNER JOIN dbo.RECITEM(NOLOCK) ON dbo.REC.LocationID = dbo.RECITEM.LocationID AND dbo.REC.recid = dbo.RECITEM.recId "&_
            " INNER JOIN dbo.Product(NOLOCK) ON dbo.RECITEM.ProdID = dbo.Product.ProdID AND dbo.Product.cat = 2 "&_
			" LEFT OUTER JOIN client(NOLOCK) ON REC.ClientID = client.ClientID"&_
			" WHERE (day(REC.Datein) = day('"& request("dtDate") &"')"&_
			" AND month(REC.Datein) = month('"& request("dtDate") &"')"&_
			" AND year(REC.Datein) = year('"& request("dtDate") &"'))"&_
			" AND REC.Line = 4"&_
            " and REC.LocationID="& request("LocationID") &_
			" Order By REC.Datein asc"

'Response.Write strSQL
'Response.End
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
				strVehical = SQLReplace(trim(rsData("Name"))&" "&trim(rsData("Phone")))
				strDueLineColor = "data" 
				strLineColor = "data" 
				strLineColor2 = "data" 
				 strStatus= trim(rsData("Status"))
				 strService =""
				 strService2 = ""
				IF (rsData("status")) = 10 THEN
					strLineColor = "statusblue"
				END IF
				IF (rsData("status")) = 20 THEN
					strLineColor = "statusyellow"
				END IF

				'IF rsData("esttime") <  now() THEN
				'	strLineColor = "statusRed"
				'end if
				IF Trim(rsData("status")) = 50 THEN
					strLineColor = "statusgreen"
				end if
				IF datediff("h",rsData("esttime"),now())>1 then
					strDueLineColor = "statusRed"
				end if


				strTitle = SQLReplace((trim(rsData("vehcolor")))&" "&(trim(rsData("vehman")))&" "&trim(rsData("VModel"))&" "&trim(rsData("Descript")))
				
				htmlDataRow = htmlDataRow & "<tr><td Title='"& strTitle &"' class="& strLineColor &" align=Right><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>" & formatdatetime(rsData("datein"),vbLongTime) & "</Label></td>"
				htmlDataRow = htmlDataRow & "<td Title='"& strTitle &"' class="& strLineColor &"  align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>"  & strVehical  & "</Label></td></tr>"
				htmlDataRow = htmlDataRow & "<tr><td Title='"& strTitle &"' class="& strDueLineColor &"  align=Right><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>" & formatdatetime(rsData("esttime"),vbLongTime) & "</Label></td>"
				strService = ""
				strSQL = "SELECT RECITEM.ProdID, Product.cat, Product.Descript"&_
				" FROM RECITEM(NOLOCK) INNER JOIN Product(NOLOCK) ON RECITEM.ProdID = Product.ProdID"&_
				" WHERE RECID=" & rsData("recid")  &" and RECID.LocationID="& request("LocationID") &" AND Product.Descript <> 'None'"
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
						IF rsData2("cat") = 7 then
							strService2 = trim(strService2)+ltrim(rsData2("Descript"))
							strLineColor2 = "StatusDarkOrange"
						END IF

						rsData2.MoveNext
					Loop
				END IF
				strService = trim(strService)+" "+trim(strUPCHARGE)
				htmlDataRow = htmlDataRow & "<td Title='"& strTitle &"' class="& strDueLineColor &"  align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>&nbsp;" & SQLReplace((trim(rsData("vehcolor")))&" "&(trim(rsData("vehman")))&" "&trim(rsData("VModel"))) & "</Label></td>"

				htmlDataRow = htmlDataRow & "<tr><td Title='"& strTitle &"' class="& strLineColor2 &"  align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>"  & rsData("recid")  & "</Label></td>"


				htmlDataRow = htmlDataRow & "<td Title='"& strTitle &"' class="& strLineColor2 &"  align=left><Label style=""cursor:hand"" class=blkdata OnClick=""" & strOnClick & """>" & trim(rsData("Descript")) & "</Label></td><tr><tr><td></td></tr>"



				intLine=intLine+1
				rsData.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=7 align=""center"" Class=""data"">No Vehicals.</td></tr>"
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