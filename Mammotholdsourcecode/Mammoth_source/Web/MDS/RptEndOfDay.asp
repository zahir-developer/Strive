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
	Dim dbMain,txtRptDate,rsData,strSQL,rsData2,strSQL2,rsData3,strSQL3,cnt
	Dim dtCdatetime,strHours,cnt2,intCaction,strTotalHrs,strCounts,strTotalCnt,strTotalAct,strTotalComm
	dim strTotalPaid,intDrawer,intCnts,intTotalPay,strEditEOD,strHoursPerWeek,LocationID,LoginID
	Set dbMain = OpenConnection	

LocationID = Request("LocationID")
LoginID = Request("LoginID")

'If LocationID = 3 then
'	strSQL = "exec stp_Labor " & LocationID     
'	IF not DBExec(dbMain, strSQL) then
'		Response.Write gstrmsg
'		Response.end
'	END IF
'end if

	Select case Request("FormAction")
		Case "btnPrint"
			'Response.Redirect "admDisplayReport.asp?rpt=Reports/EOD.rpt&@RptDate=" & Request("txtRptDate") & "&LocationID="&Request("LocationID")
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
    <div style="text-align:center">
        <form name="frmMain" method="post" action="RptEndofDay.Asp">
            <input type="hidden" name="LocationID" value="<%=LocationID%>" />
            <input type="hidden" name="LoginID" value="<%=LoginID%>" />
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
                    <td align="left" width="20">&nbsp;&nbsp;&nbsp;</td>
                    <td align="Left" class="tdcaption" background="images/header.jpg" width="150">&nbsp;&nbsp;End of Day</td>
                    <td align="right">&nbsp;</td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="2" width="768">
                <tr>
                    <td>
                        <label class="control">
                            Report Date:&nbsp; 
	        <input tabindex="50" name="txtRptDate" size="14" title="Enter Report Date" datatype="RD" value="<%=txtRptDate%>">
                            <button name="btnUpdate" style="width: 75px" type="button">Update</button>
                    </td>
                    <td align="right">

                        <button name="btnDone" style="width: 75px" type="button" onclick="SubmitForm()">Done</button>
                    </td>

                </tr>
            </table>
            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr valign="top">
                    <td width="70%">
                        <!-- Left Side Wash Details and Hours -->
                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <!-- Top Left Side Wash Details -->
                                <td width="33%" valign="top">
                                    <table cellspacing="0" border="2" cellpadding="1" width="100%">
                                        <tr>
                                            <td colspan="4" align="center" class="control" nowrap>Washes:</td>
                                        </tr>
                                        <%
		            strSQL = "SELECT Product.Descript,Product.ProdID FROM Product(NOLOCK)"&_
		            " WHERE (Product.cat = 1) ORDER BY Product.Number"
		            If DBOpenRecordset(dbMain,rsData,strSQL) Then
			            DO While NOT rsData.eof 
                                        %>
                                        <tr>
                                            <td colspan="2">
                                                <label class="control"><%=rsData("Descript")%>&nbsp;</label></td>
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
                                            <td align="right">
                                                <label class="control">&nbsp;<%=strCounts%>&nbsp;</label></td>
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
                                            <td align="right">
                                                <label class="redcontrol">&nbsp;<%=strCounts%>&nbsp;</label></td>
                                        </tr>
                                        <%
				            rsData.MoveNext
			            Loop
		            END IF
		            Set rsData = Nothing

                                        %>
                                    </table>
                                </td>
                                <td width="33%" valign="top">
                                    <table cellspacing="0" border="2" cellpadding="1" width="100%">
                                        <tr>
                                            <td colspan="4" align="center" class="control" nowrap>Details:</td>
                                        </tr>
                                        <%

		            strSQL = "SELECT Product.Descript,Product.ProdID FROM Product(NOLOCK)"&_
		            " WHERE (Product.cat = 2) ORDER BY Product.Number"
		            If DBOpenRecordset(dbMain,rsData,strSQL) Then
			            DO While NOT rsData.eof 
                                        %>
                                        <tr>
                                            <td colspan="2">
                                                <label class="control"><%=rsData("Descript")%>&nbsp;</label></td>
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
                                            <td align="right">
                                                <label class="control">&nbsp;<%=strCounts%>&nbsp;</label></td>
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
                                            <td align="right">
                                                <label class="redcontrol">&nbsp;<%=strCounts%>&nbsp;</label></td>
                                        </tr>

                                        <%
				            rsData.MoveNext
			            Loop
		            END IF
		            Set rsData = Nothing

                                        %>
                                        <tr>
                                            <td colspan="2" align="right">
                                                <label class="control">Total:&nbsp;</label></td>
                                            <td align="right">
                                                <label class="control">&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
                                            <td align="right">
                                                <label class="redcontrol">&nbsp;<%=strTotalAct%>&nbsp;</label></td>
                                        </tr>

                                    </table>
                                </td>
                                <td width="33%" valign="top">
                                    <table cellspacing="0" border="2" cellpadding="1" width="100%">
                                        <tr>
                                            <td colspan="4" align="center" class="control" nowrap>Detail Info:</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="control">Name&nbsp;</label></td>
                                            <td>
                                                <label class="control">Ticket&nbsp;</label></td>
                                            <td colspan="2">
                                                <label class="control">Comm&nbsp;</label></td>
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
                                            <td>
                                                <label class="control">&nbsp;<%=rsData("LastName")%>&nbsp;</label></td>
                                            <td>
                                                <label class="control">&nbsp;<%=rsData("Ticket")%>&nbsp;</label></td>
                                            <td colspan="2" align="right">
                                                <label class="control"><%=FormatCurrency(rsData("ComAmt"),2)%>&nbsp;</label></td>
                                        </tr>
                                        <%
				            rsData.MoveNext
			            Loop
		            END IF
		            Set rsData = Nothing

                                        %>
                                        <tr>
                                            <td colspan="2" align="right">
                                                <label class="control">Total Comm:&nbsp;</label></td>
                                            <td colspan="2" align="right">
                                                <label class="control"><%=FormatCurrency(strTotalComm,2)%>&nbsp;</label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <!-- Bottom Left Side Hours -->
                                <td colspan="3" align="left">
                                    <iframe name="fraMain" src="admLoading.asp" scrolling="yes" height="800" width="98%" frameborder="0"></iframe>
                                </td>
                            </tr>
                        </table>
                        <!-- End of Left Side Wash Details and Hours -->
                    </td>
                    <td width="30%">
                        <!-- Right Side Drawer -->
                        <table border="1" cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td align="Center" class="control" colspan="5" nowrap><b>Drawer</b></td>
                            </tr>
                            <tr>
                                <td align="Center" class="control" nowrap>&nbsp;</td>


                                <%
		intDrawer = 1
		DO While intDrawer < 3
			strSQL = "SELECT CIUserID,COUserID"&_
			" FROM cashd (NOLOCK)"&_
			" WHERE ndate = '"& txtRptDate &"' and DrawerNo=" & intDrawer &" and LocationID=" & LocationID
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof  Then
					IF isnull(rsData("CIUserID")) or LEN(rsData("CIUserID"))=0 then					
                                %>
                                <td align="right">
                                    <label class="data">&nbsp;#<%=intDrawer%>&nbsp;</label></td>
                                <%
					ELSE
	
						IF isnull(rsData("COUserID")) then

                                %>
                                <td align="right">
                                    <label class="redData">&nbsp;#<%=intDrawer%>&nbsp;</label></td>
                                <%
						ELSE
                                %>
                                <td align="right">
                                    <label class="GRNData">&nbsp;#<%=intDrawer%>&nbsp;</label></td>
                                <%
						END IF
					END IF
				ELSE
                                %>
                                <td align="right">
                                    <label class="data">&nbsp;#<%=intDrawer%>&nbsp;</label></td>
                                <%
				END IF
			END IF
			Set rsData = Nothing
			intDrawer = intDrawer+1
		Loop
                                %>


                                <td align="Center" class="control" nowrap>Total&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="Center" class="control" colspan="5" nowrap><b>Coins</b></td>
                            </tr>
                            <tr>
                                <td align="right" class="control" nowrap>Pennies:&nbsp;</td>
                                <%
		strCounts = 0
		intDrawer = 1
		intCnts = 0
		DO While intDrawer < 3
			strSQL = "SELECT COp as cnts,CORp as cnts2"&_
			" FROM cashd (NOLOCK)"&_
			" WHERE ndate = '"& txtRptDate &"' and DrawerNo=" & intDrawer &" and LocationID=" & LocationID
		
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof  Then
					intCnts = rsData("cnts")+(rsData("cnts2")*50)

				strCounts = strCounts+intCnts
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=intCnts%>&nbsp;</label></td>
                                <%
				ELSE
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;0&nbsp;</label></td>
                                <%
				END IF
			END IF
			Set rsData = Nothing
			intDrawer = intDrawer+1
		Loop
                                %>


                                <td align="right">
                                    <label class="control">&nbsp;<%=strCounts%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="right" class="control" nowrap>Nickels:&nbsp;</td>
                                <%
		strCounts = 0
		intDrawer = 1
		intCnts = 0
		DO While intDrawer < 3
			strSQL = "SELECT COn as cnts,CORn as cnts2"&_
			" FROM cashd (NOLOCK)"&_
			" WHERE ndate = '"& txtRptDate &"' and DrawerNo=" & intDrawer &" and LocationID=" & LocationID
		
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof  Then
					intCnts = rsData("cnts")+(rsData("cnts2")*40)

				strCounts = strCounts+intCnts
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=intCnts%>&nbsp;</label></td>
                                <%
				ELSE
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;0&nbsp;</label></td>
                                <%
				END IF
			END IF
			Set rsData = Nothing
			intDrawer = intDrawer+1
		Loop
                                %>


                                <td align="right">
                                    <label class="control">&nbsp;<%=strCounts%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="right" class="control" nowrap>Dimes:&nbsp;</td>
                                <%
		strCounts = 0
		intDrawer = 1
		intCnts = 0
		DO While intDrawer < 3
			strSQL = "SELECT COd as cnts,CORd as cnts2"&_
			" FROM cashd (NOLOCK)"&_
			" WHERE ndate = '"& txtRptDate &"' and DrawerNo=" & intDrawer &" and LocationID=" & LocationID
		
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof  Then
					intCnts = rsData("cnts")+(rsData("cnts2")*50)

				strCounts = strCounts+intCnts
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=intCnts%>&nbsp;</label></td>
                                <%
				ELSE
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;0&nbsp;</label></td>
                                <%
				END IF
			END IF
			Set rsData = Nothing
			intDrawer = intDrawer+1
		Loop
                                %>


                                <td align="right">
                                    <label class="control">&nbsp;<%=strCounts%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="right" class="control" nowrap>Quarters:&nbsp;</td>
                                <%
		strCounts = 0
		intDrawer = 1
		intCnts = 0
		DO While intDrawer < 3
			strSQL = "SELECT COq as cnts,CORq as cnts2"&_
			" FROM cashd (NOLOCK)"&_
			" WHERE ndate = '"& txtRptDate &"' and DrawerNo=" & intDrawer &" and LocationID=" & LocationID
		
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof  Then
					intCnts = rsData("cnts")+(rsData("cnts2")*40)

				strCounts = strCounts+intCnts
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=intCnts%>&nbsp;</label></td>
                                <%
				ELSE
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;0&nbsp;</label></td>
                                <%
				END IF
			END IF
			Set rsData = Nothing
			intDrawer = intDrawer+1
		Loop
                                %>


                                <td align="right">
                                    <label class="control">&nbsp;<%=strCounts%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="right" class="control" nowrap>Half:&nbsp;</td>
                                <%
		strCounts = 0
		intDrawer = 1
		intCnts = 0
		DO While intDrawer < 3
			strSQL = "SELECT COh as cnts"&_
			" FROM cashd (NOLOCK)"&_
			" WHERE ndate = '"& txtRptDate &"' and DrawerNo=" & intDrawer &" and LocationID=" & LocationID
		
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof  Then
				strCounts = strCounts+rsData("cnts")
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=rsData("cnts")%>&nbsp;</label></td>
                                <%
				ELSE
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;0&nbsp;</label></td>
                                <%
				END IF
			END IF
			Set rsData = Nothing
			intDrawer = intDrawer+1
		Loop
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=strCounts%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="Center" class="control" colspan="5" nowrap><b>Bills</b></td>
                            </tr>
                            <tr>
                                <td align="right" class="control" nowrap>1s:&nbsp;</td>
                                <%
		strCounts = 0
		intDrawer = 1
		intCnts = 0
		DO While intDrawer < 3
			strSQL = "SELECT CO1 as cnts"&_
			" FROM cashd (NOLOCK)"&_
			" WHERE ndate = '"& txtRptDate &"' and DrawerNo=" & intDrawer &" and LocationID=" & LocationID
		
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof  Then
					intCnts = rsData("cnts")

				strCounts = strCounts+intCnts
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=intCnts%>&nbsp;</label></td>
                                <%
				ELSE
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;0&nbsp;</label></td>
                                <%
				END IF
			END IF
			Set rsData = Nothing
			intDrawer = intDrawer+1
		Loop
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=strCounts%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="right" class="control" nowrap>5s:&nbsp;</td>
                                <%
		strCounts = 0
		intDrawer = 1
		intCnts = 0
		DO While intDrawer < 3
			strSQL = "SELECT CO5 as cnts"&_
			" FROM cashd (NOLOCK)"&_
			" WHERE ndate = '"& txtRptDate &"' and DrawerNo=" & intDrawer &" and LocationID=" & LocationID
		
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof  Then
					intCnts = rsData("cnts")

				strCounts = strCounts+intCnts
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=intCnts%>&nbsp;</label></td>
                                <%
				ELSE
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;0&nbsp;</label></td>
                                <%
				END IF
			END IF
			Set rsData = Nothing
			intDrawer = intDrawer+1
		Loop
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=strCounts%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="right" class="control" nowrap>10s:&nbsp;</td>
                                <%
		strCounts = 0
		intDrawer = 1
		intCnts = 0
		DO While intDrawer < 3
			strSQL = "SELECT CO10 as cnts"&_
			" FROM cashd (NOLOCK)"&_
			" WHERE ndate = '"& txtRptDate &"' and DrawerNo=" & intDrawer &" and LocationID=" & LocationID
		
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof  Then
					intCnts = rsData("cnts")

				strCounts = strCounts+intCnts
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=intCnts%>&nbsp;</label></td>
                                <%
				ELSE
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;0&nbsp;</label></td>
                                <%
				END IF
			END IF
			Set rsData = Nothing
			intDrawer = intDrawer+1
		Loop
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=strCounts%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="right" class="control" nowrap>20s:&nbsp;</td>
                                <%
		strCounts = 0
		intDrawer = 1
		intCnts = 0
		DO While intDrawer < 3
			strSQL = "SELECT CO20 as cnts"&_
			" FROM cashd (NOLOCK)"&_
			" WHERE ndate = '"& txtRptDate &"' and DrawerNo=" & intDrawer &" and LocationID=" & LocationID
		
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof  Then
					intCnts = rsData("cnts")

				strCounts = strCounts+intCnts
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=intCnts%>&nbsp;</label></td>
                                <%
				ELSE
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;0&nbsp;</label></td>
                                <%
				END IF
			END IF
			Set rsData = Nothing
			intDrawer = intDrawer+1
		Loop
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=strCounts%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="right" class="control" nowrap>50s:&nbsp;</td>
                                <%
		strCounts = 0
		intDrawer = 1
		intCnts = 0
		DO While intDrawer < 3
			strSQL = "SELECT CO50 as cnts"&_
			" FROM cashd (NOLOCK)"&_
			" WHERE ndate = '"& txtRptDate &"' and DrawerNo=" & intDrawer &" and LocationID=" & LocationID
		
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof  Then
					intCnts = rsData("cnts")

				strCounts = strCounts+intCnts
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=intCnts%>&nbsp;</label></td>
                                <%
				ELSE
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;0&nbsp;</label></td>
                                <%
				END IF
			END IF
			Set rsData = Nothing
			intDrawer = intDrawer+1
		Loop
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=strCounts%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="right" class="control" nowrap>100s:&nbsp;</td>
                                <%
		strCounts = 0
		intDrawer = 1
		intCnts = 0
		DO While intDrawer < 3
			strSQL = "SELECT CO100 as cnts"&_
			" FROM cashd (NOLOCK)"&_
			" WHERE ndate = '"& txtRptDate &"' and DrawerNo=" & intDrawer &" and LocationID=" & LocationID
		
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof  Then
					intCnts = rsData("cnts")

				strCounts = strCounts+intCnts
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=intCnts%>&nbsp;</label></td>
                                <%
				ELSE
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;0&nbsp;</label></td>
                                <%
				END IF
			END IF
			Set rsData = Nothing
			intDrawer = intDrawer+1
		Loop
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;<%=strCounts%>&nbsp;</label></td>
                            </tr>
                        </table>


                        <table cellspacing="0" border="2" cellpadding="0" width="100%">
                            <tr>
                                <td align="Center" class="control" nowrap>Drawer</td>
                                <td align="Center" class="control" nowrap>IN&nbsp;</td>
                                <td align="Center" class="control" nowrap>OUT&nbsp;</td>
                                <td align="Center" class="control" nowrap>Diff&nbsp;</td>
                            </tr>
                            <tr>
                                <%
		strCounts = 0
		intDrawer = 1
		DO While intDrawer < 3
			strSQL = "SELECT CITotal,COTotal,(COTotal-CITotal) as Diff,isnull(COChecks,0) as COChecks,isnull(COCreditCards,0)as COCreditCards,"&_
			" isnull(COCreditCards2,0)as COCreditCards2,"&_
			" isnull(COCreditCards3,0)as COCreditCards3,"&_
			" isnull(COCreditCards4,0)as COCreditCards4,"&_
			" isnull(COPayouts,0)as COPayouts"&_
			" FROM cashd (NOLOCK)"&_
			" WHERE ndate = '"& txtRptDate &"' and DrawerNo=" & intDrawer &" and LocationID=" & LocationID
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof  Then
					IF NOT isnull(rsData("CITotal")) AND NOT isnull(rsData("COTotal")) then					
		strCounts = (rsData("Diff")+rsData("COChecks")+rsData("COCreditCards")+rsData("COCreditCards2")+rsData("COCreditCards3")+rsData("COCreditCards4"))-rsData("COPayouts")

		intTotalPay = strCounts
                                %>
                                <td align="right">
                                    <label class="control">&nbsp;#<%=intDrawer%>&nbsp;</label></td>
                                <td align="right">
                                    <label class="control"><%=formatCurrency(rsData("CITotal"),2)%>&nbsp;</label></td>
                                <td align="right">
                                    <label class="control"><%=formatCurrency(rsData("COTotal"),2)%>&nbsp;</label></td>
                                <td align="right">
                                    <label class="control"><%=formatCurrency(rsData("Diff"),2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3">
                                    <label class="control">&nbsp;Checks&nbsp;</label></td>
                                <td align="right">
                                    <label class="control"><%=formatCurrency(rsData("COChecks"),2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3">
                                    <label class="control">&nbsp;BC&nbsp;</label></td>
                                <td align="right">
                                    <label class="control"><%=formatCurrency(rsData("COCreditCards"),2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3">
                                    <label class="control">&nbsp;AMX&nbsp;</label></td>
                                <td align="right">
                                    <label class="control"><%=formatCurrency(rsData("COCreditCards2"),2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3">
                                    <label class="control">&nbsp;Dinners&nbsp;</label></td>
                                <td align="right">
                                    <label class="control"><%=formatCurrency(rsData("COCreditCards3"),2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3">
                                    <label class="control">&nbsp;Discover&nbsp;</label></td>
                                <td align="right">
                                    <label class="control"><%=formatCurrency(rsData("COCreditCards4"),2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3">
                                    <label class="control">&nbsp;Pay Outs&nbsp;</label></td>
                                <td align="right">
                                    <label class="control"><%=formatCurrency(rsData("COPayouts"),2)%>&nbsp;</label></td>
                            </tr>



                            <%
					END IF
				END IF
			END IF
			Set rsData = Nothing
			intDrawer = intDrawer+1
		Loop
                            %>

                            <tr>
                                <td align="right" colspan="3">
                                    <label class="control">&nbsp;Total:&nbsp;</label></td>
                                <td align="right">
                                    <label class="control"><%=formatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>

                        </table>


                        <table cellspacing="0" border="2" cellpadding="0" width="100%">
                            <tr>
                                <td colspan="4" align="center" class="control" nowrap>Sales:</td>
                            </tr>
                            <tr>
                                <%
		strSQL2 = "SELECT Sum(Totalamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate) &"')"&_
					" AND (REC.LocationID = '"& LocationID &"')"
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
                                %>
                                <td>
                                    <label class="control">Total&nbsp;</label></td>
                                <td align="right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <%
		strSQL2 = "SELECT Sum(Tax) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate) &"')"&_
					" AND (REC.LocationID = '"& LocationID &"')"
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
                                %>
                                <td>
                                    <label class="control">Tax&nbsp;</label></td>
                                <td align="right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <%
		strSQL2 = "SELECT Sum(gTotal) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate) &"')"&_
					" AND (REC.LocationID = '"& LocationID &"')"
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
                                %>
                                <td align="right">
                                    <label class="control">Grand Total:&nbsp;</label></td>
                                <td align="right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <%
		strSQL2 = "SELECT Sum(cashamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate) &"')"&_
					" AND (REC.LocationID = '"& LocationID &"')"
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
			strTotalPaid = strTotalPaid + strCounts
                                %>
                                <td>
                                    <label class="control">Cash&nbsp;</label></td>
                                <td align="right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(Checkamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate) &"')"&_
					" AND (REC.LocationID = '"& LocationID &"')"
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
			strTotalPaid = strTotalPaid + strCounts
                                %>
                                <td>
                                    <label class="control">Check&nbsp;</label></td>
                                <td align="right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>

                            <tr>
                                <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(chargeamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate) &"')"&_
					" AND (REC.LocationID = '"& LocationID &"')"

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
			strTotalPaid = strTotalPaid + strCounts
                                %>
                                <td>
                                    <label class="control">Charge Cards&nbsp;</label></td>
                                <td align="right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(Accamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate) &"')"&_
					" AND (REC.LocationID = '"& LocationID &"')"

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
			strTotalPaid = strTotalPaid + strCounts
                                %>
                                <td>
                                    <label class="control">Account&nbsp;</label></td>
                                <td align="right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(GiftCardamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate) &"')"&_
					" AND (DATEPART(Day, REC.CloseDte) = '"& Day(txtRptDate) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate) &"')"&_
					" AND (REC.LocationID = '"& LocationID &"')"

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
			strTotalPaid = strTotalPaid + strCounts
                                %>
                                <td>
                                    <label class="control">Gift Cards&nbsp;</label></td>
                                <td align="right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>






                            <tr>
                                <td align="right">
                                    <label class="control">Total Paid:&nbsp;</label></td>
                                <td align="right">
                                    <label class="control"><%=FormatCurrency(strTotalPaid,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <label class="control">Diff:&nbsp;</label></td>
                                <td align="right">
                                    <label class="control"><%=FormatCurrency((strTotalPaid-intTotalPay),2)%>&nbsp;</label></td>
                            </tr>
                            <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(CashBack) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate) &"')"&_
					" AND (REC.LocationID = '"& LocationID &"')"

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
                            %>
                        <td>
                            <label class="control">Cash Back&nbsp;</label></td>
                    <td align="right">
                        <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                </tr>
            </table>

            </td>

	</tr>
</table>
            <!-- End of Right Side Drawer -->
            </td>
    </tr>
</table>
        </form>
    </div>
</body>
</html>


<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">
Option Explicit
Dim obj
Sub Window_Onload()
		fraMain.location.href = "RptEndOfDayFra.asp?txtRptDate="& document.all("txtRptDate").value & "&LocationID="&document.all("LocationID").value &"&LoginID="&document.all("LoginID").value
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
