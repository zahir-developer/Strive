<%@  language="VBSCRIPT" %>
<%
Option Explicit
Response.Expires = 0
Response.Buffer = True
Server.ScriptTimeout = 90000

'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incCommon.asp"-->
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
	Dim dbMain,txtRptDate,rsData,strSQL,rsData2,strSQL2,rsData3,strSQL3,cnt,LocationID,LoginID
	Dim dtCdatetime,strHours,cnt2,intCaction,strTotalHrs,strCounts,strTotalCnt,strTotalAct,strTotalComm
	dim strTotalPaid,intDrawer,intCnts,intTotalPay,strEditEOD,strHoursPerWeek,strCurrentStatus
	Set dbMain = OpenConnection	

    	
    LocationID = Request("LocationID")
    LoginID = Request("LoginID")


'	strSQL = "exec stp_Labor "& LocationID      
'	IF not DBExec(dbMain, strSQL) then
'		Response.Write gstrmsg
'		Response.end
'	END IF


	Select case Request("FormAction")
		Case "btnStatUpdate"
            strSQL ="Update LM_Locations set CurrentStatus='"& trim(replace(request("strCurrentStatus"),",","")) &"' WHERE (LocationID = "& LocationID &")"
			if not DBExec(dbMain,strSQL) then
			    response.write gstrmsg
			    response.end
			end if
            strSQL ="Update stats set CurrentStatus='"& trim(replace(request("strCurrentStatus"),",","")) &"' WHERE (LocationID = "& LocationID &")"
			if not DBExec(dbMain,strSQL) then
			    response.write gstrmsg
			    response.end
			end if
            if instr(1,request("strCurrentStatus"),"Closed") > 0 then
            strSQL ="Update stats set CurrentWaitTime='"& trim(replace(request("strCurrentStatus"),",","")) &"' WHERE (LocationID = "& LocationID &")"
			if not DBExec(dbMain,strSQL) then
			    response.write gstrmsg
			    response.end
			end if

            end if
			'Response.Redirect "admDisplayReport.asp?rpt=Reports/EOD.rpt&@RptDate=" & Request("txtRptDate") & "&strLoc=admWelcome.asp"
	END SELECT


txtRptDate = Request("txtRptDate")
IF not isdate(txtRptDate) then
	txtRptDate = formatdatetime(NOW(),vbShortDate)
END IF

strTotalCnt = 0
strTotalAct = 0
strTotalPaid = 0
intTotalPay = 0
'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
    <title></title>
    <link rel="stylesheet" href="main.css" type="text/css">
</head>
<body class="pgBody">
    <form name="frmMain" method="POST" action="RptMgrEOD.Asp?LocationID=<%=LocationID%>">
        <center>
<Input type="hidden" name="LocationID" value="<%=LocationID%>" />
<Input type="hidden" name="LoginID" value="<%=LoginID%>" />
<input type="hidden" name="FormAction" />
            <%
    strSQL = "SELECT ProStat.Washes, ProStat.Washers, ProStat.Details, Case When ProStat.TotalHours = 0 OR ProStat.Washes=0 then 0 else  CAST(ROUND(ProStat.Washes / ProStat.TotalHours * 100, 0) AS int) end AS Score, isnull(ProStat.PerRev,0.0) as PerRev, stats.CurrentWaitTime FROM dbo.ProStat INNER JOIN dbo.stats ON dbo.ProStat.LocationID = dbo.stats.LocationID WHERE (ProStat.ProDate = '"& Right("00"+cstr(Month(date())),2)+Right("00"+cstr(Day(date())),2)+Right(cstr(year(date())),2) &"') AND (ProStat.LocationID = "& LocationID &")"
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof  Then
            %>
            <table style="width: 70%; height: 18px" border="0">
                <tr>
                    <td style="text-align: center; vertical-align: middle">&nbsp;&nbsp;Washes &nbsp;<label class="data"><%=rsData("Washes")%></label>&nbsp;
                    </td>
                    <td style="text-align: center; vertical-align: middle">&nbsp;&nbsp;Wash-Emp&nbsp;<label class="data"><%=rsData("Washers")%></label>&nbsp;
                    </td>
                    <td style="text-align: center; vertical-align: middle">&nbsp;&nbsp;Details &nbsp;<label class="data"><%=rsData("Details")%></label>&nbsp;
                    </td>
                    <td style="text-align: center; vertical-align: middle">&nbsp;&nbsp;Score &nbsp;
                    
                        <% IF cdbl(rsData("Score")) < 74 then %>
                        <label class="reddata"><%=rsData("Score")%></label>&nbsp;
                        <% ELSEIF cdbl(rsData("Score")) >= 75 and cdbl(Session("Score")) <= 79 then %>
                        <label class="YellowData"><%=rsData("Score")%></label>&nbsp;
                        <% ELSEIF cdbl(rsData("Score")) >= 80 and cdbl(Session("Score")) <= 85 then %>
                        <label class="Data"><%=rsData("Score")%></label>&nbsp;
                        <% ELSE %>
                        <label class="GRNData"><%=rsData("Score")%></label>&nbsp;
                        <% End IF %>

                    </td>


                    <% IF cdbl(rsData("PerRev")) > 0.0 then %>
                    <td style="text-align: center; vertical-align: middle">&nbsp;&nbsp;P/L&nbsp;<label class="data"><%=rsData("PerRev")%></label>&nbsp;
                    </td>
                    <% Else %>
                    <td style="text-align: center; vertical-align: middle">&nbsp;&nbsp;P/L&nbsp;<label class="reddata"><%=rsData("PerRev")%></label>&nbsp;
                    </td>
                    <% END IF %>
                    <td style="text-align: center; vertical-align: middle">&nbsp;&nbsp;Wait&nbsp;<label class="data"><%=rsData("CurrentWaitTime")%></label>
                        &nbsp;
                    </td>
                </tr>
            </table>



            <%
                end if
            end if
            %>

<table cellspacing="0" cellpadding="0" class="tblcaption" width="100%">
	<tr>
		<td align="left" width=20>&nbsp;&nbsp;&nbsp;</td>
		<td align="Left" class="tdcaption" background="images/header.jpg" width="150">&nbsp;&nbsp;End of Day</td>
		<td align="right">&nbsp;</td>
	</tr>
 </table>
 <table cellspacing="0" cellpadding="2" width="768">
	<tr>
		<td><label class=control>Report Date:&nbsp; 
	        <Input tabindex=50  name=txtRptDate size=14 title="Enter Report Date" datatype="RD"  value="<%=txtRptDate%>">
			<button name="btnUpdate" style="WIDTH: 75px" type=button>Update</button>
	     </td>	
<td>
<% strSQL="SELECT ltrim(rtrim(CurrentStatus)) as CurrentStatus FROM LM_Locations Where (LocationID = "& LocationID &")" 
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		if NOT rsData.eof then
         strCurrentStatus = rsData("CurrentStatus")
        end if
    end if
%>

<input type="radio" name="strCurrentStatus" onclick="StatusUpdate()" value="Open" <%=IIF(strCurrentStatus="Open", " Checked ", "" )%>  /><label class="control">Open</label>
<input type="radio" name="strCurrentStatus" onclick="StatusUpdate()" value="Closed/Weather" <%=IIF(strCurrentStatus="Closed/Weather", " Checked ", "" )%>  /><label class="control">Closed/Weather</label>
<input type="radio" name="strCurrentStatus" onclick="StatusUpdate()" value="Closed" <%=IIF(strCurrentStatus="Closed", " Checked ", "" )%>  /><label class="control">Closed</label>
</td>
		<td align="right">

<button name="btnDone" style="WIDTH: 75px" type=button OnClick="SubmitForm()">Done</button>
	     </td>	

	</tr>
</table>
<table border="0" cellspacing="0" cellpadding="0" width="100%" >
  <tr valign=top>
        <td width="70%">
<!-- Left Side Wash Details and Hours -->
<table  border="0" cellspacing="0" cellpadding="0" width="100%">
	<tr>
<!-- Top Left Side Wash Details -->
		<td width="33%" valign=top>
            <table  cellspacing="0" border="2" cellpadding="1" width="100%">
	            <tr>
		            <td  colspan=4 align="center" class="control" nowrap>Washes:</td>
	            </tr>
            <%
		if LocationID = 3 then
		    strSQL = "SELECT Product.Descript,Product.ProdID FROM Product(NOLOCK)"&_
		    " WHERE (Product.cat = 1) and Product.ProdID not in (293,1,2,3)  ORDER BY Product.Number"
		else
		    strSQL = "SELECT Product.Descript,Product.ProdID FROM Product(NOLOCK)"&_
		    " WHERE (Product.cat = 1) and Product.ProdID not in (461,462,463,464) ORDER BY Product.Number"
		end if
		            If DBOpenRecordset(dbMain,rsData,strSQL) Then
			            DO While NOT rsData.eof 
            %>
	            <tr>
		            <td colspan=2><label class=control><%=rsData("Descript")%>&nbsp;</label></td>
		            <%	strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
					            " INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
					            " WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate) &"')"&_
					            " AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate) &"')"&_
					            " AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate) &"')"&_
                                " and REC.LocationID="& LocationID &_
                                " and RECITEM.LocationID="& LocationID &_
					            " AND (RECITEM.ProdID = "& rsData("ProdID") &") and Rec.status >= 70 "
			            If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
				            IF NOT rsData2.eof then
					            strCounts = rsData2("cnt")
				            ELSE
					            strCounts = 0
				            END IF
			            ELSE
				            strCounts = 0
			            END IF
			            Set rsData2 = Nothing
			            IF isnull(strCounts) then
				            strCounts = 0
			            END IF
			            strTotalCnt = strTotalCnt+strCounts
		            %>
		            <td align="right"><label class=control>&nbsp;<%=strCounts%>&nbsp;</label></td>
		            <%	strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
					            " INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
					            " WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate) &"')"&_
					            " AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate) &"')"&_
					            " AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate) &"')"&_
                                " and REC.LocationID="& LocationID &_
                               " and RECITEM.LocationID="& LocationID &_
					            " AND (RECITEM.ProdID = "& rsData("ProdID") &")"
			            If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
				            IF NOT rsData2.eof then
					            strCounts = rsData2("cnt")
				            ELSE
					            strCounts = 0
				            END IF
			            ELSE
				            strCounts = 0
			            END IF
			            Set rsData2 = Nothing
			            IF isnull(strCounts) then
				            strCounts = 0
			            END IF
			            strTotalAct = strTotalAct+strCounts
		            %>
		            <td align="right"><label class=redcontrol>&nbsp;<%=strCounts%>&nbsp;</label></td>
	            </tr>
            <%
				            rsData.MoveNext
			            Loop
		            END IF
		            Set rsData = Nothing

            %>
            </table>		
		</td>
		<td width="33%" valign=top>
		    <table  cellspacing="0" border="2" cellpadding="1" width="100%">
	            <tr>
		            <td  colspan=4 align="center" class="control" nowrap>Details:</td>
	            </tr>
            <%

		            strSQL = "SELECT Product.Descript,Product.ProdID FROM Product(NOLOCK)"&_
		            " WHERE (Product.cat = 2) ORDER BY Product.Number"
		            If DBOpenRecordset(dbMain,rsData,strSQL) Then
			            DO While NOT rsData.eof 
            %>
	            <tr>
		            <td colspan=2><label class=control><%=rsData("Descript")%>&nbsp;</label></td>
		            <%	strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
					            " INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
					            " WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate) &"')"&_
					            " AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate) &"')"&_
					            " AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate) &"')"&_
                                " and REC.LocationID="& LocationID &_
                               " and RECITEM.LocationID="& LocationID &_
					            " AND (RECITEM.ProdID = "& rsData("ProdID") &") and Rec.status >= 70 "
			            If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
				            IF NOT rsData2.eof then
					            strCounts = rsData2("cnt")
				            ELSE
					            strCounts = 0
				            END IF
			            ELSE
				            strCounts = 0
			            END IF
			            Set rsData2 = Nothing
			            IF isnull(strCounts) then
				            strCounts = 0
			            END IF
			            strTotalCnt = strTotalCnt + strCounts
		            %>
		            <td align="right"><label class=control>&nbsp;<%=strCounts%>&nbsp;</label></td>
			            <%	strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
					            " INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
					            " WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate) &"')"&_
					            " AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate) &"')"&_
					            " AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate) &"')"&_
                                " and REC.LocationID="& LocationID &_
                                " and RECITEM.LocationID="& LocationID &_
					            " AND (RECITEM.ProdID = "& rsData("ProdID") &") "


			            If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
				            IF NOT rsData2.eof then
					            strCounts = rsData2("cnt")
				            ELSE
					            strCounts = 0
				            END IF
			            ELSE
				            strCounts = 0
			            END IF
			            Set rsData2 = Nothing
			            IF isnull(strCounts) then
				            strCounts = 0
			            END IF
			            strTotalAct = strTotalAct + strCounts
		            %>
		            <td align="right"><label class=redcontrol>&nbsp;<%=strCounts%>&nbsp;</label></td>
            </tr>

            <%
				            rsData.MoveNext
			            Loop
		            END IF
		            Set rsData = Nothing

            %>
	            <tr>
		            <td colspan=2 align=right><label class=control>Total:&nbsp;</label></td>
		            <td  align="right"><label class=control>&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
		            <td  align="right"><label class=redcontrol>&nbsp;<%=strTotalAct%>&nbsp;</label></td>
	            </tr>

            </table>		
		</td>
		<td width="33%" valign=top>
		    <table  cellspacing="0" border="2" cellpadding="1" width="100%">
	            <tr>
		            <td  colspan=4 align="center" class="control" nowrap>Detail Info:</td>
	            </tr>
	            <tr>
		            <td><label class=control>Name&nbsp;</label></td>
		            <td><label class=control>Ticket&nbsp;</label></td>
		            <td  colspan=2><label class=control>Comm&nbsp;</label></td>
	            </tr>
            <%

		            strSQL = "SELECT LM_Users.LastName, DetailComp.recid AS Ticket,DetailComp.ComAmt"&_
		            " FROM DetailComp (NOLOCK)"&_
		            " INNER JOIN LM_Users(NOLOCK) ON DetailComp.UserID = LM_Users.UserID"&_
		            " WHERE (DATEPART(Month, DetailComp.CdateTime) = '"& Month(txtRptDate) &"')"&_
		            " AND (DATEPART(Day, DetailComp.CdateTime) = '"& Day(txtRptDate) &"')"&_
		            " AND (DATEPART(Year, DetailComp.CdateTime) = '"& Year(txtRptDate) &"')"&_
                    " and LM_Users.LocationID="& LocationID &_
                    " and DetailComp.LocationID="& LocationID 
           	
		            If DBOpenRecordset(dbMain,rsData,strSQL) Then
			            DO While NOT rsData.eof 
			            strTotalComm = strTotalComm+rsData("ComAmt")
            %>
	            <tr>
		            <td><label class=control>&nbsp;<%=rsData("LastName")%>&nbsp;</label></td>
		            <td><label class=control>&nbsp;<%=rsData("Ticket")%>&nbsp;</label></td>
		            <td  colspan=2 align="right"><label class=control><%=FormatCurrency(rsData("ComAmt"),2)%>&nbsp;</label></td>
	            </tr>
            <%
				            rsData.MoveNext
			            Loop
		            END IF
		            Set rsData = Nothing

            %>
	            <tr>
		            <td colspan=2 align=right><label class=control>Total Comm:&nbsp;</label></td>
		            <td colspan=2 align="right"><label class=control><%=FormatCurrency(strTotalComm,2)%>&nbsp;</label></td>
	            </tr>
            </table>		
		</td>
	</tr>
	<tr>
<!-- Bottom Left Side Hours -->
		<td colspan=3 align=left>
            <iframe Name="fraMain" src="admLoading.asp" scrolling=yes height="800" width="98%" frameborder="0"></iframe>
		</td>
	</tr>
</table>
<!-- End of Left Side Wash Details and Hours -->
<!-- Right Side Drawer -->

<!-- End of Right Side Drawer -->
        </td>
    </tr>
</table>
 </center>
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
Dim obj
Sub Window_Onload()
		fraMain.location.href = "RptMgrEODFra.asp?txtRptDate="& document.all("txtRptDate").value &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
End Sub

Sub StatusUpdate()
    frmMain.FormAction.value="btnStatUpdate"
    frmMain.Submit()
End Sub

Sub btnUpdate_OnClick()
	If len(frmMain.txtRptDate.value)>0  then
		frmMain.Submit()
	ELSE
		msgbox " You must enter date field"
	END IF
End Sub
Sub SubmitForm()
	dim Answer
	window.event.CancelBubble=True
	window.event.ReturnValue=False

	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	Select Case window.event.srcElement.name
		Case "btnDone"
			location.href="admWelcome.asp"
 	End Select
End Sub

</script>

<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
