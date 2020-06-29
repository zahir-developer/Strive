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
<!--#include file="incCommon.asp"-->
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

Dim dbMain, strLoc,hdnFilterBy
Dim Maxdate, Mindate,strSQL,rsData,intWeekNo,intYearNo,intNextWeek,dtNextDate,i,strPayRate
dim intFDW,dtWeekof,dtLastWeekof,strSQL2,rsData2,MaxSQL,intSheetID,hdnSheetID,hdnUserID
dim strDTotal,strWeekof,strUnifAmt,strCollAmt,strAdjAmt,strUnifbal,strCollbal,strAdjbal,LocationID,LoginID, hasTimeSheet
Set dbMain =  OpenConnection
hdnFilterBy = Request("hdnFilterBy")
hdnSheetID = Request("hdnSheetID")
hdnUserID = Request("hdnUserID")
strPayRate = 0.0
strDTotal = 0.0
LocationID = request("LocationID")
LoginID = request("LoginID")

IF Request("formaction")="btnAddName" then
	Call btnAddName(dbMain)
END IF
IF Request("formaction")="btnPayAll" then
	Call btnPayAll(dbMain)
END IF

IF LEN(hdnSheetID) > 0 then
	strSQL = " Select Weekof from Timesheet (NOLOCK) where Timesheet.SheetID ="& hdnSheetID &" and Timesheet.LocationID=" & LocationID
	If dbOpenRecordSet(dbmain,rsData,strSQL) Then
		strWeekof = FormatDateTime(rsData(0),vbshortdate)
	End if
	Set rsData = Nothing
end if
strSQL = " Select ISNULL(min(cdatetime),"& date()& ") from TimeClock (NOLOCK) where TimeClock.Paid =0 and TimeClock.LocationID=" & LocationID
If dbOpenRecordSet(dbmain,rsData,strSQL) Then
	Mindate =FormatDateTime(rsData(0),vbshortdate)
End if
Set rsData = Nothing
strSQL = " Select ISNULL(Max(cdatetime),"& date()& ") from TimeClock (NOLOCK) where TimeClock.Paid =0  and TimeClock.LocationID=" & LocationID
If dbOpenRecordSet(dbmain,rsData,strSQL) Then
	Maxdate=FormatDateTime(rsData(0),vbshortdate)
End if
Set rsData = Nothing

IF len(trim(hdnUserID)) > 0 then
	strSQL = " Select isnull(PayRate,0.0) as PayRate from LM_USERS (NOLOCK) where UserID ="& hdnUserID  &" and LocationID=" & LocationID
	If dbOpenRecordSet(dbmain,rsData,strSQL) Then
		IF NOT rsData.eof then
			strPayRate=rsData(0)
		ELSE
			strPayRate=0.0
		END IF
	End if
	Set rsData = Nothing
	strSQL = " Select Sum(ComAmt) as DTotal from DetailComp (NOLOCK)"&_
			",timeSheet (NOLOCK)"&_
			" WHERE (TimeSheet.SheetID = "& hdnSheetID &")"&_
			" AND (DetailComp.CdateTime BETWEEN TimeSheet.weekof AND DATEADD(day, 7, TimeSheet.weekof))"&_
			" AND (DetailComp.UserID = "& hdnUserID &") and (DetailComp.LocationID=" & LocationID &")"&_
            " AND (TimeSheet.LocationID=" & LocationID &")"
	If dbOpenRecordSet(dbmain,rsData,strSQL) Then
		IF NOT rsData.eof then
			strDTotal=rsData(0)
		ELSE
			strDTotal=0.0
		END IF
	End if
	Set rsData = Nothing
	
	strSQL =" SELECT sum(actamt) as Bal From UserUnif (Nolock) where UserID=" & hdnUserID   &" and LocationID=" & LocationID &" group by UserID"
	IF dbOpenStaticRecordset(dbmain, rsData, strSQL) then   
		IF NOT 	rsData.EOF then
		strUnifbal = rsData("Bal")
		ELSE
		strUnifbal = 0.0
		END IF
	END IF
	Set rsData = Nothing
	strSQL =" SELECT sum(actamt) as Bal From UserCol (Nolock) where UserID=" & hdnUserID   &" and LocationID=" & LocationID &" group by UserID"
	IF dbOpenStaticRecordset(dbmain, rsData, strSQL) then   
		IF NOT 	rsData.EOF then
		strCollbal = rsData("Bal")
		ELSE
		strCollbal = 0.0
		END IF
	END IF
	Set rsData = Nothing
	strSQL =" SELECT sum(actamt) as Bal From UserAdj (Nolock) where UserID=" & hdnUserID   &" and LocationID=" & LocationID &" group by UserID"
	IF dbOpenStaticRecordset(dbmain, rsData, strSQL) then   
		IF NOT 	rsData.EOF then
		strAdjbal = rsData("Bal")
		ELSE
		strAdjbal = 0.0
		END IF
	END IF
	Set rsData = Nothing
	strSQL =" SELECT sum(actamt) as Bal From UserUnif (Nolock) where UserID=" & hdnUserID  &" and LocationID=" & LocationID & " and sheetID="& hdnSheetID &" group by UserID"
	IF dbOpenStaticRecordset(dbmain, rsData, strSQL) then   
		IF NOT 	rsData.EOF then
		strUnifAmt = rsData("Bal")
		ELSE
		strUnifAmt = 0.0
		END IF
	END IF
	Set rsData = Nothing
	strSQL =" SELECT sum(actamt) as Bal From UserCol (Nolock) where UserID=" & hdnUserID  &" and LocationID=" & LocationID & " and sheetID="& hdnSheetID &" group by UserID"
	IF dbOpenStaticRecordset(dbmain, rsData, strSQL) then   
		IF NOT 	rsData.EOF then
		strCollAmt = rsData("Bal")
		ELSE
		strCollAmt = 0.0
		END IF
	END IF
	Set rsData = Nothing
	strSQL =" SELECT sum(actamt) as Bal From UserAdj (Nolock) where UserID=" & hdnUserID  &" and LocationID=" & LocationID & " and sheetID="& hdnSheetID &" group by UserID"
	IF dbOpenStaticRecordset(dbmain, rsData, strSQL) then   
		IF NOT 	rsData.EOF then
		strAdjAmt = rsData("Bal")
		ELSE
		strAdjAmt = 0.0
		END IF
	END IF
	Set rsData = Nothing

END IF
hasTimeSheet = 0
	strSQL =" SELECT SheetID FROM dbo.TimeSheet WHERE (LocationID = " & LocationID & ") AND (yearno = YEAR(GETDATE())) AND (Weekno = DATEPART(wk, GETDATE()) + 1)"
	IF dbOpenStaticRecordset(dbmain, rsData, strSQL) then   
		IF NOT rsData.EOF then
		    hasTimeSheet = rsData("SheetID")
		END IF
	END IF
	Set rsData = Nothing


if hasTimeSheet=0 then
    i = datediff("d",Mindate,Maxdate,1,3)
    'Response.Write Mindate&"<BR>"
    'Response.Write Maxdate&"<BR>"
    'Response.Write i&"<BR>"
    'Response.end
    dtLastWeekof = Mindate
    dtnextdate = Mindate
    DO While i > 0
	    intFDW = Weekday(dtnextdate)-1
	    intWeekNo = DatePart("WW",dtnextdate,1,3)+1 ' First Day of week (1)Sunday, First Week of year (3)First full week
	    intYearNo = DatePart("YYYY",dtnextdate,1,3)
	    dtWeekof = dateadd("D",-intFDW,dtnextdate)
	    IF dtWeekof <> dtLastWeekof then
		    strSQL = " Select SheetID from TimeSheet (NOLOCK) where Weekof ='" & dtWeekof &"' and LocationID=" & LocationID
		    If dbOpenRecordSet(dbmain,rsData,strSQL) Then
			    IF rsData.EOF then

				    MaxSQL = " Select ISNULL(Max(SheetID),0)+1 from TimeSheet (NOLOCK) where LocationID=" & LocationID
				    If dbOpenRecordSet(dbmain,rsData2,MaxSQL) Then
					    intSheetID=rsData2(0)
				    End if
				    Set rsData2 = Nothing
				    strSQL2="Insert into TimeSheet(sheetid,LocationID,WeekNo,YearNo,Weekof,status,empCnt,totalamt)"&_
						    " Values("&_
						    intSheetID & ","&_
						    LocationID & ","&_
						    intWeekNo & ","&_
						    intYearNo & ","&_
						    "'" & dtWeekof & "',"&_
						    "0,0,0)"
				    IF NOT DBExec(dbMain, strSQL2) then
					    Response.Write strSQL2
					    Response.End
				    END IF
			    END IF
		    End if
	    END IF
	    dtLastWeekof = dtWeekof
	    dtnextdate = dateadd("D",1,dtnextdate)
	    i=i-1
    LOOP 
    IF Weekday(date()) = 1 then
	    strSQL = " Select SheetID from TimeSheet (NOLOCK) where Weekof ='" & date() &"' and LocationID=" & LocationID
	    If dbOpenRecordSet(dbmain,rsData,strSQL) Then
		    IF rsData.EOF then

			    MaxSQL = " Select ISNULL(Max(SheetID),0)+1 from TimeSheet (NOLOCK) where LocationID=" & LocationID
			    If dbOpenRecordSet(dbmain,rsData2,MaxSQL) Then
				    intSheetID=rsData2(0)
			    End if
			    Set rsData2 = Nothing
			    strSQL2="Insert into TimeSheet(sheetid,LocationID,WeekNo,YearNo,Weekof,status,empCnt,totalamt)"&_
					    " Values("&_
					    intSheetID & ","&_
					    LocationID & ","&_
					    intWeekNo+1 & ","&_
					    intYearNo & ","&_
					    "'" & date() & "',"&_
					    "0,0,0)"
			    IF NOT DBExec(dbMain, strSQL2) then
				    Response.Write strSQL2
				    Response.End
			    END IF
		    END IF
	    End if
    END IF
end if



'********************************************************************
' HTML
'********************************************************************
%>

<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css" />
    <title></title>
</head>
<body class="pgbody" onclick="SetDirty" onkeyup="SetDirty()">
    <form method="post" name="frmMain" action="admTime.asp">
        <input type="hidden" name="FormAction" />
        <input type="hidden" name="hdnFilterBy" value="<%=hdnFilterBy%>">
        <input type="hidden" name="hdnSheetID" value="<%=hdnSheetID%>">
        <input type="hidden" name="hdnUserID" value="<%=hdnUserID%>">
        <input type="hidden" name="strPayRate" value="<%=strPayRate%>">
        <input type="hidden" name="strDTotal" value="<%=strDTotal%>">
        <input type="hidden" name="strWeekof" value="<%=strWeekof%>">
        <input type="hidden" name="strUnifBal" value="<%=strUnifBal%>">
        <input type="hidden" name="strCollBal" value="<%=strCollBal%>">
        <input type="hidden" name="strAdjBal" value="<%=strAdjBal%>">
        <input type="hidden" name="strUnifAmt" value="<%=strUnifAmt%>">
        <input type="hidden" name="strCollAmt" value="<%=strCollAmt%>">
        <input type="hidden" name="strAdjAmt" value="<%=strAdjAmt%>">
        <input type="hidden" name="strLoc" value="<%=strLoc%>">
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
        <div style="text-align: center">
            <table class="tblcaption" style="width: 768px; border-collapse: collapse;">
                <tr>
                    <td style="text-align: left;">
                        <iframe name="fraWeekof" src="admLoading.asp" style="height: 90px; width: 400px"></iframe>
                    </td>
                    <td style="text-align: center;">&nbsp;
                    </td>
                    <td style="text-align: center;">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        <iframe name="fraName" src="admLoading.asp" style="height: 120px; width: 400px"></iframe>
                    </td>
                    <td>
                        <table style="width: 400px; border-collapse: collapse;">
                            <tr>
                                <%IF len(trim(strWeekof)) > 0 then %>
                                <td style="text-align: center; white-space: nowrap;" class="control">Week of: <%=strWeekof%>&nbsp;</td>
                                <% END IF %>
                            </tr>
                            <tr>
                                <td style="text-align: left; white-space: nowrap;" class="control">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: center; white-space: nowrap;">
                                    <%IF hdnSheetID > 0 then %>
                                    <select tabindex="2" name="intName">
                                        <% Call LoadName(dbMain,hdnSheetID) %>
                                    </select>
                                    <button name="btnAddName" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" type="submit" style="width: 100px; text-align: center;" onclick="SubmitForm()">Add Name</button>
                                    <% END IF %>
                            </tr>
                            <tr>
                                <td style="text-align: left; white-space: nowrap;" class="control">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table style="width: 950px; border-collapse: collapse;">
                <tr>
                    <td style="width: 15px" class="tabgap">&nbsp;</td>
                    <td class="tabselect" style="width: 150px; text-align: center; white-space: nowrap"><a name="TimeCard" href="#1" class="control" onclick="ChangeTab()">Time Card.</a> </td>
                    <td class="tabgap">&nbsp;</td>
                    <td class="tab" style="text-align: center; white-space: nowrap"><a name="Details" href="#1" class="control" onclick="ChangeTab()">Details.</a> </td>
                    <td class="tabgap">&nbsp;</td>
                    <td class="tab" style="text-align: center; white-space: nowrap"><a name="Uniforms" href="#1" class="control" onclick="ChangeTab()">Uniforms.</a> </td>
                    <td class="tabgap">&nbsp;</td>
                    <td class="tab" style="text-align: center; white-space: nowrap"><a name="Collision" href="#1" class="control" onclick="ChangeTab()">Collision.</a></td>
                    <td class="tabgap">&nbsp;</td>
                    <td class="tab" style="text-align: center; white-space: nowrap"><a name="Adjustments" href="#1" class="control" onclick="ChangeTab()">Adjustments.</a></td>
                    <td class="tabgap">&nbsp;</td>
                    <td style="width: 300px" class="tabgap">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td class="tabgap">&nbsp;</td>
                    <td style="width: 300px" class="tabgap">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td class="tabgap">&nbsp;</td>
                    <td style="width: 300px" class="tabgap">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td class="tabgap">&nbsp;</td>
                    <td style="text-align: right; white-space: nowrap">&nbsp;
                    </td>
                </tr>
            </table>
            <iframe name="fraMain2" src="admLoading.asp" scrolling="yes" style="height: 218px; width: 976px" frameborder="0"></iframe>
            <table class="tblcaption" style="width: 950px; border-collapse: collapse;">
                <tr>
                    <td>
                        <table style="width: 300px; border-collapse: collapse;">
                            <tr>
                                <td colspan="2" style="text-align: center; white-space: nowrap" class="control"><b>Balance Due</b></td>
                                <td style="text-align: right; white-space: nowrap">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right; white-space: nowrap" class="control">Uniforms:&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">
                                    <label class="control" id="lblUnifBal"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; white-space: nowrap" class="control">Collisions:&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">
                                    <label class="control" id="lblCollBal"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; white-space: nowrap" class="control">Adjustments:&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">
                                    <label class="control" id="lblAdjBal"></label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table style="width: 350px; border-collapse: collapse;">
                            <tr>
                                <td style="text-align: right; white-space: nowrap">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table style="width: 300px; border-collapse: collapse;">
                            <tr>
                                <td style="text-align: right; white-space: nowrap" class="control">Rate:&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control"><%=strPayRate%>&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">Hours:&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">
                                    <label class="control" id="lblhours"></label>
                                </td>
                                <td style="text-align: right; white-space: nowrap" class="control">Total:&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">
                                    <label class="control" id="lblTotal"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">Bonus Hrs:&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">
                                    <label class="control" id="lblBonusHrs"></label>
                                </td>
                                <td style="text-align: right; white-space: nowrap" class="control">Bonus:&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">
                                    <label class="control" id="lblBonus"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">Details:&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">
                                    <label class="control" id="lblDTotal"></label>
                                </td>


                            </tr>
                            <tr>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">Uniforms:&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">
                                    <label class="control" id="lblUnifAmt"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">Collisions:&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">
                                    <label class="control" id="lblCollAmt"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">Adjustments:&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">
                                    <label class="control" id="lblAdjAmt"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">Grand Total:&nbsp;</td>
                                <td style="text-align: right; white-space: nowrap" class="control">
                                    <label class="control" id="lblGTotal"></label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
<%

'********************************************************************
' Time-Side Functions
'********************************************************************
%>
<script type="text/VBSCRIPT">
Option Explicit
Sub Window_Onload()
		lblTotal.innerText = "$0.00"
		lblGTotal.innerText = "$0.00"
		lblDTotal.innerText = "$0.00"
		lblBonus.innerText = "$0.00"
		lblhours.innerText = "0.00"
		IF len(trim(document.all("strDTotal").value))>0 then
			lblDTotal.innerText = formatcurrency(document.all("strDTotal").value,2)
		ELSE
			lblDTotal.innerText = "$0.00"
		END IF
		IF len(trim(document.all("strUnifBal").value))>0 then
			lblUnifBal.innerText = formatcurrency(document.all("strUnifBal").value,2)
		ELSE
			lblUnifBal.innerText = "$0.00"
		END IF
		IF len(trim(document.all("strCollBal").value))>0 then
			lblCollBal.innerText = formatcurrency(document.all("strCollBal").value,2)
		ELSE
			lblCollBal.innerText = "$0.00"
		END IF
		IF len(trim(document.all("strAdjBal").value))>0 then
			lblAdjBal.innerText = formatcurrency(document.all("strAdjBal").value,2)
		ELSE
			lblAdjBal.innerText = "$0.00"
		END IF
		IF len(trim(document.all("strUnifAmt").value))>0 then
			lblUnifAmt.innerText = formatcurrency(document.all("strUnifAmt").value,2)
		ELSE
			lblUnifAmt.innerText = "$0.00"
		END IF
		IF len(trim(document.all("strCollAmt").value))>0 then
			lblCollAmt.innerText = formatcurrency(document.all("strCollAmt").value,2)
		ELSE
			lblCollAmt.innerText = "$0.00"
		END IF
		IF len(trim(document.all("strAdjAmt").value))>0 then
			lblAdjAmt.innerText = formatcurrency(document.all("strAdjAmt").value,2)
		ELSE
			lblAdjAmt.innerText = "$0.00"
		END IF
		TimeCard.ParentElement.className = "tabSelect"	
		Details.ParentElement.className = "tab"	
		Uniforms.ParentElement.className = "tab"	
		Collision.ParentElement.className = "tab"
		Adjustments.ParentElement.className = "tab"

		fraMain2.location.href = "admTimeCardFra.asp?hdnSheetID=<%=Request("hdnSheetID")%>&hdnUserID=<%=Request("hdnUserID")%>&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value 
		fraWeekof.location.href = "admTimeWeekFra.asp?LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value 
		fraName.location.href = "admTimeNameFra.asp?hdnSheetID=<%=Request("hdnSheetID")%>&hdnUserID=<%=Request("hdnUserID")%>&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value 

End Sub


Sub ChangeTab
	If UCase(window.event.srcElement.ClassName) = "LNKDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	If UCase(window.event.srcElement.parentElement.className) = "TABSELECT" Then  exit sub
	Select Case window.event.srcElement.name
		Case "TimeCard"
			TimeCard.ParentElement.className = "tabSelect"	
			Details.ParentElement.className = "tab"	
			Uniforms.ParentElement.className = "tab"	
			Collision.ParentElement.className = "tab"
			Adjustments.ParentElement.className = "tab"
			fraMain2.location.href = "admTimeCardFra.asp?hdnSheetID=<%=Request("hdnSheetID")%>&hdnUserID=<%=Request("hdnUserID")%>&LocationID=<%=Request("LocationID")%>&LoginID=<%=Request("LoginID")%>"
		Case "Details"
			TimeCard.ParentElement.className = "tab"	
			Details.ParentElement.className = "tabSelect"	
			Uniforms.ParentElement.className = "tab"	
			Collision.ParentElement.className = "tab"
			Adjustments.ParentElement.className = "tab"
			fraMain2.location.href = "admTimeDetailFra.asp?hdnSheetID=<%=Request("hdnSheetID")%>&hdnUserID=<%=Request("hdnUserID")%>&LocationID=<%=Request("LocationID")%>&LoginID=<%=Request("LoginID")%>"
		Case "Uniforms"
			TimeCard.ParentElement.className = "tab"	
			Details.ParentElement.className = "tab"	
			Uniforms.ParentElement.className = "tabSelect"	
			Collision.ParentElement.className = "tab"
			Adjustments.ParentElement.className = "tab"
			fraMain2.location.href = "admTimeUnifFra.asp?hdnSheetID=<%=Request("hdnSheetID")%>&hdnUserID=<%=Request("hdnUserID")%>&LocationID=<%=Request("LocationID")%>&LoginID=<%=Request("LoginID")%>"
		Case "Collision"
			TimeCard.ParentElement.className = "tab"	
			Details.ParentElement.className = "tab"	
			Uniforms.ParentElement.className = "tab"	
			Collision.ParentElement.className = "tabSelect"
			Adjustments.ParentElement.className = "tab"
			fraMain2.location.href = "admTimeCollFra.asp?hdnSheetID=<%=Request("hdnSheetID")%>&hdnUserID=<%=Request("hdnUserID")%>&LocationID=<%=Request("LocationID")%>&LoginID=<%=Request("LoginID")%>"
		Case "Adjustments"
			TimeCard.ParentElement.className = "tab"	
			Details.ParentElement.className = "tab"	
			Uniforms.ParentElement.className = "tab"	
			Collision.ParentElement.className = "tab"
			Adjustments.ParentElement.className = "tabSelect"
			fraMain2.location.href = "admTimeAdjFra.asp?hdnSheetID=<%=Request("hdnSheetID")%>&hdnUserID=<%=Request("hdnUserID")%>&LocationID=<%=Request("LocationID")%>&LoginID=<%=Request("LoginID")%>"
		
	End Select
	window.focus
End Sub

Sub SubmitForm()
 window.event.CancelBubble=True
 window.event.ReturnValue=False

	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If

Select Case window.event.srcElement.name
	Case "btnNew"
		location.href="admTimeEdit.asp?hdnBPOID=0" 

	Case "btnAddName"
		If Len(frmMain.intName.value)> 0 Then 
			frmMain.FormAction.value="btnAddName"
			frmMain.submit()
		END IF
	Case "btnPayAll"
		If Len(frmMain.hdnSheetID.value)> 0 Then 
			frmMain.FormAction.value="btnPayAll"
			frmMain.submit()
		END IF
	
	Case "btnReport"
		IF Len(frmMain.hdnSheetID.value) > 0 then
			frmMain.FormAction.value="btnPrint"
			frmMain.Submit()
		ELSE
			msgbox "No Time Sheet Selected"
		END IF
	Case "btnSummary"
		IF Len(frmMain.hdnSheetID.value) > 0 then
			frmMain.FormAction.value="btnSummary"
			frmMain.Submit()
		ELSE
			msgbox "No Time Sheet Selected"
		END IF
	End Select
End Sub

</script>
<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************  &" and LocationID=" & LocationID
Sub btnPayAll(dbMain)
	Dim strSQL,rs,strSQL2
	strSQL =" SELECT DISTINCT ClockID"&_
		" FROM TimeClock(NOLOCK) ,TimeSheet(NOLOCK)"&_
		" WHERE (TimeSheet.SheetID = "& Request("hdnSheetID") &")"&_
		" AND (TimeSheet.LocationID = "& Request("LocationID") &")"&_
		" AND TimeClock.Cdatetime BETWEEN TimeSheet.weekof AND DATEADD(day, 7, TimeSheet.weekof)"
    IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF
				strSQL2=" Update timeclock set Paid = 1 WHERE ClockID = "&rs("ClockID") &" AND (timeclock.LocationID = "& Request("LocationID") &")"
				IF NOT DBExec(dbMain, strSQL2) then
					Response.Write gstrMsg
					Response.End
				END IF
				rs.MoveNext
			Loop	
		END IF
	END IF
	Set RS = Nothing
	strSQL =" SELECT DISTINCT DetailCompID"&_
		" FROM DetailComp(NOLOCK) ,TimeSheet(NOLOCK)"&_
		" WHERE (TimeSheet.SheetID = "& Request("hdnSheetID") &")"&_
		" AND (TimeSheet.LocationID = "& Request("LocationID") &")"&_
		" AND DetailComp.Cdatetime BETWEEN TimeSheet.weekof AND DATEADD(day, 7, TimeSheet.weekof)"
    IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF
				strSQL2=" Update DetailComp set Paid = 1 WHERE DetailCompID = "&rs("DetailCompID") &" AND (DetailComp.LocationID = "& Request("LocationID") &")"
				IF NOT DBExec(dbMain, strSQL2) then
					Response.Write gstrMsg
					Response.End
				END IF
				rs.MoveNext
			Loop	
		END IF
	END IF
	Set RS = Nothing
	strSQL2=" Update TimeSheet set status = 20 WHERE SheetID = "&Request("hdnSheetID") &" AND (TimeSheet.LocationID = "& Request("LocationID") &")"
	IF NOT DBExec(dbMain, strSQL2) then
		Response.Write gstrMsg
		Response.End
	END IF
end sub

Sub btnAddName(dbMain)
	Dim strSQL,rsData,MaxSQL,intClockID,varDate
	strSQL = "SELECT weekof FROM TimeSheet WHERE sheetid = "&Request("hdnSheetID") &" AND (TimeSheet.LocationID = "& Request("LocationID") &")"
	If dbOpenRecordSet(dbmain,rsData,strSQL) Then
		varDate=rsData(0)
	End if
	MaxSQL = " Select ISNULL(Max(ClockID),0)+1 from timeclock (NOLOCK)  where (timeclock.LocationID = "& Request("LocationID") &")"
	If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
		intClockID=rsData(0)
	End if
	Set rsData = Nothing
	strSQL=" Insert into timeclock(ClockID,LocationID,UserID,Cdatetime,CAction,CType,EditBy,EditDate,Paid)Values(" & _
				intClockID & ", " & _
				Request("LocationID") & ", " & _
				Request("intName") & ", " & _
				"'" & varDate &" 07:30:00 AM', " & _
				0 & ", " & _
				0 & ", " & _
				Request("LoginID") & ", " & _
				"'" & Date() & "',0)"
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
	MaxSQL = " Select ISNULL(Max(ClockID),0)+1 from timeclock (NOLOCK) where (timeclock.LocationID = "& Request("LocationID") &")"
	If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
		intClockID=rsData(0)
	End if
	Set rsData = Nothing
	strSQL=" Insert into timeclock(ClockID,LocationID,UserID,Cdatetime,CAction,CType,EditBy,EditDate,Paid)Values(" & _
				intClockID & ", " & _
				Request("LocationID") & ", " & _
				Request("intName") & ", " & _
				"'" & varDate &" 16:00:00 PM', " & _
				1 & ", " & _
				0 & ", " & _
				Request("LoginID") & ", " & _
				"'" & Date() & "',0)"
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
end sub

Function LoadName(db,hdnSheetID)
	Dim strSQL,RS
	IF isnull(hdnSheetID) or len(ltrim(hdnSheetID))=0 then
		hdnSheetID = 0
	END IF
	strSQL= " SELECT UserID, FirstName, LastName "&_
			" FROM LM_Users WHERE (LM_Users.LocationID = "& Request("LocationID") &") and (LM_Users.Active=1) AND  LM_Users.userID NOT IN (SELECT userid FROM timeclock "&_
            " Inner Join TimeSheet ON TimeSheet.LocationID = LM_Users.LocationID "&_
			" WHERE TimeSheet.SheetID = "& hdnSheetID &_ 
		    " AND (TimeSheet.LocationID = "& Request("LocationID") &")"&_
			" AND (TimeClock.Cdatetime BETWEEN TimeSheet.weekof AND DATEADD(day, 7,TimeSheet.weekof)))"&_
            " Order by LastName"
%>
<option value=""></option>
<%
	If dbOpenRecordSet(db,rs,strSQL) Then
		Do While Not RS.EOF
%>
<option value="<%=RS("UserID")%>"><%=RS("FirstName")%>&nbsp;&nbsp;<%=RS("LastName")%></option>
<%
			RS.MoveNext
		Loop
	End If
End Function



%>
