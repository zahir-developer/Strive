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
	Dim dbMain,txtRptDate,rsData,strSQL,rsData2,strSQL2,cnt,LocationID,LoginID,strList1,strList2
	Dim intWeek,intYear,txtRptDate1,txtRptDate2,txtRptDate3,txtRptDate4,txtRptDate5,txtRptDate6,txtRptDate7
	Dim dtCdatetime,strHours,cnt2,intCaction,strTotalHrs,strCounts,strTotalCnt,strTotalAct,strTotalComm
	dim strTotalPaid,intDrawer,intCnts,intTotalPay,i,strGtotalHrs,strGwash,strGDetail
	dim strDiff,strCOChecks,strCOCreditCards,strCOCreditCards2,strCOCreditCards3,strCOCreditCards4,strCOPayouts
	dim strGDrawer,strGCOChecks,strGCOCreditCards,strGCOCreditCards2,strGCOCreditCards3,strGCOCreditCards4,strGCOPayouts
	dim strgTotal,strgsales,strgtax,strGGTotal,strGcash,strgcheck,strGcharge,strGaccount,strGgiftcard,strgtotalpaid,strgDiff,strgcashback
	Set dbMain = OpenConnection	

    LocationID = Request("LocationID")
    LoginID = Request("LoginID")

	intWeek = Request("cboWeek")
	intYear = Request("intYear")

	IF intWeek < 1 then
		intWeek = DatePart("WW",date(),1,3)+1 

		intYear = Year(date())
		i = WeekDay(date(),1)
		cnt = 1-i
		txtRptDate1 = DateAdd("d",cnt,Date())
	ELSE
	
		strSQL="SELECT weekof FROM TimeSheet where YearNo="& intYear &" and WeekNo="&intWeek & " And LocationID="& LocationID
		If dbOpenRecordSet(dbMain,rsData,strSQL) Then
			IF Not rsData.EOF THEN
				txtRptDate1 = rsData("weekof")
			END IF
		END IF
	END IF
		txtRptDate2 = DateAdd("d",1,txtRptDate1)
		txtRptDate3 = DateAdd("d",1,txtRptDate2)
		txtRptDate4 = DateAdd("d",1,txtRptDate3)
		txtRptDate5 = DateAdd("d",1,txtRptDate4)
		txtRptDate6 = DateAdd("d",1,txtRptDate5)
		txtRptDate7 = DateAdd("d",1,txtRptDate6)



	strTotalCnt = 0
	strTotalAct = 0
	strTotalPaid = 0
	intTotalPay = 0
	strGTotalHrs = 0
	strGwash = 0
	strGDetail = 0
	strDiff = 0
	strCOChecks = 0
	strCOCreditCards = 0
	strCOCreditCards2 = 0
	strCOCreditCards3 = 0
	strCOCreditCards4 = 0
	strCOPayouts = 0
	strGDrawer = 0
	strGCOChecks = 0
	strGCOCreditCards = 0
	strGCOCreditCards2 = 0
	strGCOCreditCards3 = 0
	strGCOCreditCards4 = 0
	strGCOPayouts = 0
	
	strgTotal = 0
	strgsales = 0
	strgtax = 0
	strGGTotal = 0
	strGcash = 0
	strgcheck = 0
	strGcharge = 0
	strGaccount = 0
	strGgiftcard = 0
	strgtotalpaid = 0
	strgDiff = 0
	strgcashback = 0
   strList1 = ""
	strSQL = "SELECT Product.ProdID FROM Product(NOLOCK) WHERE (Product.cat = 1) ORDER BY Product.ProdID"
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		DO While NOT rsData.eof 
            strList1 = strList1 & rsData("ProdID") &","
			rsData.MoveNext
		Loop
	END IF
	Set rsData = Nothing
    strList1 = left(strList1, len(strList1)-1)
   strList2 = ""
	strSQL = "SELECT Product.ProdID FROM Product(NOLOCK) WHERE (Product.cat = 2) ORDER BY Product.ProdID"
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		DO While NOT rsData.eof 
            strList2 = strList2 & rsData("ProdID") &","
			rsData.MoveNext
		Loop
	END IF
	Set rsData = Nothing
    strList2 = left(strList2, len(strList2)-1)
	
	
'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
    <title></title>
    <link rel="stylesheet" href="main.css" type="text/css" />
</head>
<body class="pgBody">
    <form name="frmMain" method="POST" action="RptWeekly.asp">
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" value="<%=LoginID%>" />
        <input type="hidden" name="FormAction" value="">
        <input type="hidden" name="intWeek" value="<%=intWeek%>">
        <div style="text-align: center">

            <table cellspacing="0" cellpadding="0" class="tblcaption" width="900">
                <tr>
                    <td align="Left" class="tdcaption" background="images/header.jpg" width="270">Weekly Sales Report</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="2" width="900">
                <tr>
                    <td>
                        <label class="control">
                            Report Week:&nbsp;
                        </label>
                        <select name="cboWeek" tabindex="1">
                            <%Call LoadWeeks(dbMain,intWeek,intYear,LocationID)%>
                        </select>
                        <label class="control">
                            Report Year:&nbsp;
                        </label>
                        <input tabindex="2" name="intYear" size="4" title="Enter Report Week" datatype="text" value="<%=intYear%>">&nbsp;
			<button name="btnUpdate" style="width: 75px" type="button">Update</button>
                    </td>
                    <td style="width: 25%; text-align: center">
                        <label id="LoadText" style="color: red;">Loading...</label></td>
                    <td style="text-align: right">

                        <button name="btnDone" style="width: 75px" type="button" onclick="SubmitForm()">Done</button>
                    </td>

                </tr>
            </table>
            <table cellspacing="0" width="800">
                <tr valign="top">
                    <td>
                        <table cellspacing="0" border="2" cellpadding="1" width="100">
                            <tr>
                                <td align="center" colspan="2">
                                    <label class="control">&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">Date:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">Total Hrs:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">Washes:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">Details:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">Drawers:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;Checks&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;BC&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;AMX&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;Dinners&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;Discover&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;Pay Outs&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;Total:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;Sales:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;Tax:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;GTotal:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;Cash:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;Check:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;Charge:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;Account:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;Gift Cards:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;Total Paid:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;Diff:&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;Cash Back:&nbsp;</label></td>
                            </tr>


                        </table>
                    </td>
                    <td valign="top">

                        <table cellspacing="0" border="2" cellpadding="1" width="100">
                            <% 
	strHours = 0
	strTotalHrs = 0
	strCounts = 0
	strTotalCnt = 0
	strTotalPaid = 0
	intTotalPay=0
                            %>
                            <tr>
                                <td align="center" colspan="2">
                                    <label class="control">&nbsp;Sunday&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <label class="control">&nbsp;<%=txtRptDate1%>&nbsp;</label></td>
                            </tr>

	<%	strSQL=" SELECT DISTINCT TimeClock.UserID "&_
	" FROM TimeClock (NOLOCK) INNER JOIN LM_Users(NOLOCK) ON TimeClock.UserID = LM_Users.UserID "&_
	" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate1) &"')"&_
	" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate1) &"')"&_
	" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate1) &"')"&_
	" AND CAST(LM_Users.Salery AS Numeric) = 0 AND TimeClock.CType <> 9 And (TimeClock.LocationID="& LocationID & ")"
'response.write "=>" & strSQL &"<=" & "<br />"
'response.end

	Cnt = 1
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		DO While NOT rsData.eof 
			strSQL2=" SELECT UserID, Cdatetime, Caction"&_
					" FROM TimeClock (NOLOCK)"&_
					" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate1) &"')"&_
					" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate1) &"')"&_
					" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate1) &"')"&_
					" AND (UserID = "& rsData("UserID") &") And (LocationID="& LocationID &") ORDER BY Cdatetime, Caction"
'response.write "=>" & strSQL2 & "<br />"
'response.end
			Cnt2 = 0
			If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
				DO While NOT rsData2.eof 
					Cnt2=cnt2+1
					IF cnt2=1 then
						intCaction = rsData2("Caction")
						dtCdatetime = rsData2("Cdatetime")
						strHours = 0
					ELSE
						IF rsData2("Caction") = 1 then
							intCaction = rsData2("Caction")
							strHours = strHours + DateDiff("n",dtCdatetime,rsData2("Cdatetime"))
						ELSE
							intCaction = rsData2("Caction")
							dtCdatetime = rsData2("Cdatetime")
						END IF					
					END IF
					rsData2.MoveNext
				Loop
			END IF
			Set rsData2 = Nothing
			IF CNT2 = 1 then
				strHours = strHours + DateDiff("n",dtCdatetime,now())
			END IF
			strHours = round(strHours/60,2)
			strTotalHrs = strTotalHrs+strHours
			cnt = cnt+1
			rsData.MoveNext
		Loop
	END IF
	Set rsData = Nothing
	strGTotalHrs = strTotalHrs

                            %>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control"><%=formatnumber(strTotalHrs,2)%>&nbsp;</label></td>
                            </tr>


                            <%
	strCounts = 0
	strTotalCnt = 0
 'response.write "=>" & strList &"<=" & "<br />"
		strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
			" INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
			" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate1) &"')"&_
			" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate1) &"')"&_
			" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate1) &"')"&_
			" AND (RECITEM.ProdID IN ("& strList1 &")) And (REC.LocationID="& LocationID &") And (RECITEM.LocationID="& LocationID &") and (Rec.status >= 70 )"

'response.write "=>" & strSQL2 &"<=" & "<br />"
'response.end
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
		strGwash = strTotalCnt
                            %>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
                            </tr>
                            <%
	strCounts = 0
	strTotalCnt = 0

		strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
			" INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
			" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate1) &"')"&_
			" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate1) &"')"&_
			" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate1) &"')"&_
			" AND (RECITEM.ProdID in ("& strList2 &")) and (Rec.status >= 70)  And (Rec.LocationID="& LocationID &") And (RECITEM.LocationID="& LocationID &")"
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
		strGDetail = strTotalCnt
                            %>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
                            </tr>


                            <tr>
                                <%
	strCounts = 0
	strTotalCnt = 0
	strTotalPaid= 0
	strDiff = 0
	strCOChecks = 0
	strCOCreditCards = 0
	strCOCreditCards2 = 0
	strCOCreditCards3 = 0
	strCOCreditCards4 = 0
	strCOPayouts = 0

	strSQL = "SELECT isnull(CITotal,0) as CITotal,isnull(COTotal,0) as COTotal,(isnull(COTotal,0)-isnull(CITotal,0)) as Diff,isnull(COChecks,0) as COChecks,isnull(COCreditCards,0)as COCreditCards,"&_
	" isnull(COCreditCards2,0)as COCreditCards2,"&_
	" isnull(COCreditCards3,0)as COCreditCards3,"&_
	" isnull(COCreditCards4,0)as COCreditCards4,"&_
	" isnull(COPayouts,0)as COPayouts"&_
	" FROM cashd (NOLOCK)"&_
	" WHERE ndate = '"& txtRptDate1 &"' And LocationID="& LocationID
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		do while NOT rsData.eof  
				strCounts = strCounts + (rsData("Diff")+rsData("COChecks")+rsData("COCreditCards")+rsData("COCreditCards2")+rsData("COCreditCards3")+rsData("COCreditCards4"))-rsData("COPayouts")
				intTotalPay = intTotalPay+strCounts
				strDiff = strDiff + rsData("Diff")
				strCOChecks = strCOChecks + rsData("COChecks")
				strCOCreditCards =  strCOCreditCards + rsData("COCreditCards")
			strCOCreditCards2 = strCOCreditCards2 + rsData("COCreditCards2")
				strCOCreditCards3 = strCOCreditCards3 + rsData("COCreditCards3")
				strCOCreditCards4 = strCOCreditCards4 + rsData("COCreditCards4")
				strCOPayouts = strCOPayouts + rsData("COPayouts")
		rsData.MoveNext
		loop
	END IF
	Set rsData = Nothing

	strGDrawer = strGDrawer + strDiff
	strgCOChecks = strgCOChecks +strCOChecks
	strgCOCreditCards =  strgCOCreditCards + strCOCreditCards
	strgCOCreditCards2 = strgCOCreditCards2 + strCOCreditCards2
	strgCOCreditCards3 = strgCOCreditCards3 + strCOCreditCards3
	strgCOCreditCards4 = strgCOCreditCards4 + strCOCreditCards4
	strgCOPayouts = strgCOPayouts + strCOPayouts
	strgTotal = strgTotal + strCounts


                                %>
                                <td style="text-align: right">
                                    <label class="control"><%=formatCurrency(strDiff,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control"><%=formatCurrency(strCOChecks,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control"><%=formatCurrency(strCOCreditCards,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control"><%=formatCurrency(strCOCreditCards2,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control"><%=formatCurrency(strCOCreditCards3,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control"><%=formatCurrency(strCOCreditCards4,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control"><%=formatCurrency(strCOPayouts,2)%>&nbsp;</label></td>
                            </tr>



                            <tr>
                                <td style="text-align: right">
                                    <label class="control"><%=formatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(Totalamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate1) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate1) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate1) &"')  And LocationID="& LocationID
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
		strgsales = strgsales + strCounts
                                %>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(Tax) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate1) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate1) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate1) &"')  And LocationID="& LocationID
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
		strgtax = strgtax + strCounts
                                %>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(gTotal) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate1) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate1) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate1) &"')  And LocationID="& LocationID
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
		strGGTotal = strGGTotal +strCounts
                                %>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(cashamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate1) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate1) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate1) &"')  And LocationID="& LocationID
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
			strGcash = strGcash + strCounts
                                %>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(Checkamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate1) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate1) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate1) &"')  And LocationID="& LocationID
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
			strGCheck = strGCheck + strCounts
                                %>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>

                            <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(chargeamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate1) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate1) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate1) &"')  And LocationID="& LocationID

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
			strGCharge = strGCharge + strCounts
                            %>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>
                            <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(Accamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate1) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate1) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate1) &"')  And LocationID="& LocationID

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
			strGAccount = strGAccount + strCounts
                            %>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>
                            <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(GiftCardamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate1) &"')"&_
					" AND (DATEPART(Day, REC.CloseDte) = '"& Day(txtRptDate1) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate1) &"')  And LocationID="& LocationID

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
			strGGiftCard = strGGiftCard + strCounts
			strgTotalPaid = strgTotalPaid + strTotalPaid
			strGdiff = strGdiff + strTotalPaid-intTotalPay
                            %>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control"><%=FormatCurrency(strTotalPaid,2)%>&nbsp;</label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <label class="control"><%=FormatCurrency((strTotalPaid-intTotalPay),2)%>&nbsp;</label></td>
                            </tr>
                            <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(CashBack) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate1) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate1) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate1) &"')  And LocationID="& LocationID

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
			strGCashBack = strGCashBack + strCounts
                            %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                </tr>
            </table>
            </td>
		<td valign="top">
            <table cellspacing="0" border="2" cellpadding="1" width="100">

                <% 
	strHours = 0
	strTotalHrs = 0
	strCounts = 0
	strTotalCnt = 0
	strTotalPaid = 0
	intTotalPay=0
                %>
                <tr>
                    <td align="center" colspan="2">
                        <label class="control">&nbsp;Monday&nbsp;</label></td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <label class="control">&nbsp;<%=txtRptDate2%>&nbsp;</label></td>
                </tr>

                <%	strSQL=" SELECT DISTINCT TimeClock.UserID"&_
	" FROM TimeClock (NOLOCK) INNER JOIN LM_Users(NOLOCK) ON TimeClock.UserID = LM_Users.UserID"&_
	" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate2) &"')"&_
	" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate2) &"')"&_
	" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate2) &"')"&_
	" AND CAST(LM_Users.Salery AS Numeric) = 0 AND TimeClock.CType <> 9  And (TimeClock.LocationID="& LocationID & ")" 
	Cnt = 1
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		DO While NOT rsData.eof 
			strSQL2=" SELECT UserID, Cdatetime, Caction"&_
					" FROM TimeClock(NOLOCK)"&_
					" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate2) &"')"&_
					" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate2) &"')"&_
					" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate2) &"')"&_
					" AND (UserID = "& rsData("UserID") &") And TimeClock.LocationID="& LocationID & " ORDER BY Cdatetime, Caction"
			Cnt2 = 0
			If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
				DO While NOT rsData2.eof 
					Cnt2=cnt2+1
					IF cnt2=1 then
						intCaction = rsData2("Caction")
						dtCdatetime = rsData2("Cdatetime")
						strHours = 0
					ELSE
						IF rsData2("Caction") = 1 then
							intCaction = rsData2("Caction")
							strHours = strHours + DateDiff("n",dtCdatetime,rsData2("Cdatetime"))
						ELSE
							intCaction = rsData2("Caction")
							dtCdatetime = rsData2("Cdatetime")
						END IF					
					END IF
					rsData2.MoveNext
				Loop
			END IF
			Set rsData2 = Nothing
			IF CNT2 = 1 then
				strHours = strHours + DateDiff("n",dtCdatetime,now())
			END IF
			strHours = round(strHours/60,2)
			strTotalHrs = strTotalHrs+strHours
			cnt = cnt+1
			rsData.MoveNext
		Loop
	END IF
	Set rsData = Nothing
	strGTotalHrs = strGTotalHrs+strTotalHrs

                %>
                <tr>
                    <td style="text-align: right">
                        <label class="control"><%=formatnumber(strTotalHrs,2)%>&nbsp;</label></td>
                </tr>
                <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
			" INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
			" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate2) &"')"&_
			" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate2) &"')"&_
			" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate2) &"')"&_
			" AND (RECITEM.ProdID IN ("& strList1 &")) And (REC.LocationID="& LocationID &") And (RECITEM.LocationID="& LocationID &") and (Rec.status >= 70 )"
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
		strGwash = strGwash + strTotalCnt
                %>
                <tr>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
                </tr>
                <%
	strCounts = 0
	strTotalCnt = 0

				strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
					" INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate2) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate2) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate2) &"')"&_
			" AND (RECITEM.ProdID IN ("& strList2 &")) And (REC.LocationID="& LocationID &") And (RECITEM.LocationID="& LocationID &") and (Rec.status >= 70 )"
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
		strGdetail = strGdetail + strTotalCnt
                %>
                <tr>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
                </tr>
                <%
	strCounts = 0
	strTotalCnt = 0
	strDiff = 0
	strCOChecks = 0
	strCOCreditCards = 0
	strCOCreditCards2 = 0
	strCOCreditCards3 = 0
	strCOCreditCards4 = 0
	strCOPayouts = 0

	strSQL = "SELECT isnull(CITotal,0) as CITotal,isnull(COTotal,0) as COTotal,(isnull(COTotal,0)-isnull(CITotal,0)) as Diff,isnull(COChecks,0) as COChecks,isnull(COCreditCards,0)as COCreditCards,"&_
	" isnull(COCreditCards2,0)as COCreditCards2,"&_
	" isnull(COCreditCards3,0)as COCreditCards3,"&_
	" isnull(COCreditCards4,0)as COCreditCards4,"&_
	" isnull(COPayouts,0)as COPayouts"&_
	" FROM cashd (NOLOCK)"&_
	" WHERE ndate = '"& txtRptDate2 &"' And LocationID="& LocationID
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		do while NOT rsData.eof  
				strCounts = strCounts + (rsData("Diff")+rsData("COChecks")+rsData("COCreditCards")+rsData("COCreditCards2")+rsData("COCreditCards3")+rsData("COCreditCards4"))-rsData("COPayouts")
				intTotalPay = intTotalPay+strCounts
				strDiff = strDiff + rsData("Diff")
				strCOChecks = strCOChecks + rsData("COChecks")
				strCOCreditCards =  strCOCreditCards + rsData("COCreditCards")
			strCOCreditCards2 = strCOCreditCards2 + rsData("COCreditCards2")
				strCOCreditCards3 = strCOCreditCards3 + rsData("COCreditCards3")
				strCOCreditCards4 = strCOCreditCards4 + rsData("COCreditCards4")
				strCOPayouts = strCOPayouts + rsData("COPayouts")
		rsData.MoveNext
		loop
	END IF
	Set rsData = Nothing
	strGDrawer = strGDrawer + strDiff
	strgCOChecks = strgCOChecks +strCOChecks
	strgCOCreditCards =  strgCOCreditCards + strCOCreditCards
	strgCOCreditCards2 = strgCOCreditCards2 + strCOCreditCards2
	strgCOCreditCards3 = strgCOCreditCards3 + strCOCreditCards3
	strgCOCreditCards4 = strgCOCreditCards4 + strCOCreditCards4
	strgCOPayouts = strgCOPayouts + strCOPayouts
	strgTotal = strgTotal + strCounts
                %>
                <td style="text-align: right">
                    <label class="control"><%=formatCurrency(strDiff,2)%>&nbsp;</label></td>
                </tr>
	<tr>
        <td style="text-align: right">
            <label class="control"><%=formatCurrency(strCOChecks,2)%>&nbsp;</label></td>
    </tr>
                <tr>
                    <td style="text-align: right">
                        <label class="control"><%=formatCurrency(strCOCreditCards,2)%>&nbsp;</label></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <label class="control"><%=formatCurrency(strCOCreditCards2,2)%>&nbsp;</label></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <label class="control"><%=formatCurrency(strCOCreditCards3,2)%>&nbsp;</label></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <label class="control"><%=formatCurrency(strCOCreditCards4,2)%>&nbsp;</label></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <label class="control"><%=formatCurrency(strCOPayouts,2)%>&nbsp;</label></td>
                </tr>



                <tr>
                    <td style="text-align: right">
                        <label class="control"><%=formatCurrency(strCounts,2)%>&nbsp;</label></td>
                </tr>
                <tr>
                    <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(Totalamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate2) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate2) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate2) &"') And LocationID="& LocationID
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
		strgsales = strgsales + strCounts
                    %>
                <tr>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                </tr>
                <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(Tax) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate2) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate2) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate2) &"') And LocationID="& LocationID
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
		strgtax = strgtax + strCounts
                %>

                <tr>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                </tr>
                <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(gTotal) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate2) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate2) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate2) &"') And LocationID="& LocationID
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
		strGGTotal = strGGTotal +strCounts
                %>
                <tr>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                </tr>
                <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(cashamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate2) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate2) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate2) &"') And LocationID="& LocationID
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
			strGcash = strGcash + strCounts
                %>
                <tr>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                </tr>
                <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(Checkamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate2) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate2) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate2) &"') And LocationID="& LocationID
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
			strGCheck = strGCheck + strCounts
                %>
                <tr>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                </tr>

                <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(chargeamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate2) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate2) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate2) &"') And LocationID="& LocationID

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
			strGcharge = strGcharge + strCounts
                %>
                <tr>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                </tr>
                <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(Accamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate2) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate2) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate2) &"') And LocationID="& LocationID

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
			strGAccount = strGAccount + strCounts
                %>
                <tr>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                </tr>
                <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(GiftCardamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate2) &"')"&_
					" AND (DATEPART(Day, REC.CloseDte) = '"& Day(txtRptDate2) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate2) &"') And LocationID="& LocationID

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
			strGGiftCard = strGGiftCard + strCounts
			strgTotalPaid = strgTotalPaid + strTotalPaid
			strGdiff = strGdiff + strTotalPaid-intTotalPay
                %>
                <tr>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <label class="control"><%=FormatCurrency(strTotalPaid,2)%>&nbsp;</label></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <label class="control"><%=FormatCurrency((strTotalPaid-intTotalPay),2)%>&nbsp;</label></td>
                </tr>
                <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(CashBack) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate2) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate2) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate2) &"') And LocationID="& LocationID

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
			strGCashBack = strGCashBack + strCounts
                %>
                <tr>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                </tr>





            </table>
        </td>
            <td valign="top">
                <table cellspacing="0" border="2" cellpadding="1" width="100">

                    <% 
	strHours = 0
	strTotalHrs = 0
	strCounts = 0
	strTotalCnt = 0
	strTotalPaid = 0
	intTotalPay=0
                    %>


                    <tr>
                        <td align="center" colspan="2">
                            <label class="control">&nbsp;Tuesday&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <label class="control">&nbsp;<%=txtRptDate3%>&nbsp;</label></td>
                    </tr>

                    <%	strSQL=" SELECT DISTINCT TimeClock.UserID"&_
	" FROM TimeClock (NOLOCK) INNER JOIN LM_Users(NOLOCK) ON TimeClock.UserID = LM_Users.UserID"&_
	" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate3) &"')"&_
	" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate3) &"')"&_
	" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate3) &"')"&_
	" AND CAST(LM_Users.Salery AS Numeric) = 0 AND TimeClock.CType <> 9  And (TimeClock.LocationID="& LocationID &")" 
	Cnt = 1
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		DO While NOT rsData.eof 
			strSQL2=" SELECT UserID, Cdatetime, Caction"&_
					" FROM TimeClock(NOLOCK)"&_
					" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate3) &"')"&_
					" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate3) &"')"&_
					" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate3) &"')"&_
					" AND (UserID = "& rsData("UserID") &") And TimeClock.LocationID="& LocationID &"ORDER BY Cdatetime, Caction"
			Cnt2 = 0
			If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
				DO While NOT rsData2.eof 
					Cnt2=cnt2+1
					IF cnt2=1 then
						intCaction = rsData2("Caction")
						dtCdatetime = rsData2("Cdatetime")
						strHours = 0
					ELSE
						IF rsData2("Caction") = 1 then
							intCaction = rsData2("Caction")
							strHours = strHours + DateDiff("n",dtCdatetime,rsData2("Cdatetime"))
						ELSE
							intCaction = rsData2("Caction")
							dtCdatetime = rsData2("Cdatetime")
						END IF					
					END IF
					rsData2.MoveNext
				Loop
			END IF
			Set rsData2 = Nothing
			IF CNT2 = 1 then
				strHours = strHours + DateDiff("n",dtCdatetime,now())
			END IF
			strHours = round(strHours/60,2)
			strTotalHrs = strTotalHrs+strHours
			cnt = cnt+1
			rsData.MoveNext
		Loop
	END IF
	Set rsData = Nothing
	strGTotalHrs = strGTotalHrs+strTotalHrs
                    %>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatnumber(strTotalHrs,2)%>&nbsp;</label></td>
                    </tr>


                    <%
				strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
					" INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate3) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate3) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate3) &"')"&_
			" AND (RECITEM.ProdID IN ("& strList1 &")) And (REC.LocationID="& LocationID &") And (RECITEM.LocationID="& LocationID &") and (Rec.status >= 70 )"
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
		strGwash = strGwash + strTotalCnt
                    %>
                    <tr>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
                    </tr>
                    <%
	strCounts = 0
	strTotalCnt = 0

				strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
					" INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate3) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate3) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate3) &"')"&_
			" AND (RECITEM.ProdID IN ("& strList2 &")) And (REC.LocationID="& LocationID &") And (RECITEM.LocationID="& LocationID &") and (Rec.status >= 70 )"
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
		strGdetail = strGdetail + strTotalCnt
                    %>
                    <tr>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
                    </tr>


                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
	strDiff = 0
	strCOChecks = 0
	strCOCreditCards = 0
	strCOCreditCards2 = 0
	strCOCreditCards3 = 0
	strCOCreditCards4 = 0
	strCOPayouts = 0

	strSQL = "SELECT isnull(CITotal,0) as CITotal,isnull(COTotal,0) as COTotal,(isnull(COTotal,0)-isnull(CITotal,0)) as Diff,isnull(COChecks,0) as COChecks,isnull(COCreditCards,0)as COCreditCards,"&_
	" isnull(COCreditCards2,0)as COCreditCards2,"&_
	" isnull(COCreditCards3,0)as COCreditCards3,"&_
	" isnull(COCreditCards4,0)as COCreditCards4,"&_
	" isnull(COPayouts,0)as COPayouts"&_
	" FROM cashd (NOLOCK)"&_
	" WHERE ndate = '"& txtRptDate3 &"'  And LocationID="& LocationID
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		do while NOT rsData.eof  
				strCounts = strCounts + (rsData("Diff")+rsData("COChecks")+rsData("COCreditCards")+rsData("COCreditCards2")+rsData("COCreditCards3")+rsData("COCreditCards4"))-rsData("COPayouts")
				intTotalPay = intTotalPay+strCounts
				strDiff = strDiff + rsData("Diff")
				strCOChecks = strCOChecks + rsData("COChecks")
				strCOCreditCards =  strCOCreditCards + rsData("COCreditCards")
			strCOCreditCards2 = strCOCreditCards2 + rsData("COCreditCards2")
				strCOCreditCards3 = strCOCreditCards3 + rsData("COCreditCards3")
				strCOCreditCards4 = strCOCreditCards4 + rsData("COCreditCards4")
				strCOPayouts = strCOPayouts + rsData("COPayouts")
		rsData.MoveNext
		loop
	END IF
	Set rsData = Nothing
	strGDrawer = strGDrawer + strDiff
	strgCOChecks = strgCOChecks +strCOChecks
	strgCOCreditCards =  strgCOCreditCards + strCOCreditCards
	strgCOCreditCards2 = strgCOCreditCards2 + strCOCreditCards2
	strgCOCreditCards3 = strgCOCreditCards3 + strCOCreditCards3
	strgCOCreditCards4 = strgCOCreditCards4 + strCOCreditCards4
	strgCOPayouts = strgCOPayouts + strCOPayouts
	strgTotal = strgTotal + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strDiff,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOChecks,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards2,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards3,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards4,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOPayouts,2)%>&nbsp;</label></td>
                    </tr>



                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(Totalamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate3) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate3) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate3) &"') And LocationID="& LocationID
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
		strgsales = strgsales + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(Tax) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate3) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate3) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate3) &"') And LocationID="& LocationID
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
		strgtax = strgtax + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(gTotal) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate3) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate3) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate3) &"') And LocationID="& LocationID
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
		strGGTotal = strGGTotal +strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(cashamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate3) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate3) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate3) &"') And LocationID="& LocationID
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
			strGcash = strGCash + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(Checkamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate3) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate3) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate3) &"') And LocationID="& LocationID
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
			strGCheck = strGCheck + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>

                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(chargeamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate3) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate3) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate3) &"') And LocationID="& LocationID

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
			strGcharge = strGcharge + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(Accamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate3) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate3) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate3) &"') And LocationID="& LocationID

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
			strGAccount = strGAccount + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(GiftCardamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate3) &"')"&_
					" AND (DATEPART(Day, REC.CloseDte) = '"& Day(txtRptDate3) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate3) &"') And LocationID="& LocationID

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
			strGGiftCard = strGGiftCard + strCounts
			strgTotalPaid = strgTotalPaid + strTotalPaid
			strGdiff = strGdiff + strTotalPaid-intTotalPay
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=FormatCurrency(strTotalPaid,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=FormatCurrency((strTotalPaid-intTotalPay),2)%>&nbsp;</label></td>
                    </tr>
                    <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(CashBack) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate3) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate3) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate3) &"') And LocationID="& LocationID

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
			strGCashBack = strGCashBack + strCounts
                    %>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table cellspacing="0" border="2" cellpadding="1" width="100">
                    <% 
	strHours = 0
	strTotalHrs = 0
	strCounts = 0
	strTotalCnt = 0
	strTotalPaid = 0
	intTotalPay=0
                    %>

                    <tr>
                        <td align="center" colspan="2">
                            <label class="control">&nbsp;Wednesday&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <label class="control">&nbsp;<%=txtRptDate4%>&nbsp;</label></td>
                    </tr>

                    <%	strSQL=" SELECT DISTINCT TimeClock.UserID"&_
	" FROM TimeClock (NOLOCK) INNER JOIN LM_Users(NOLOCK) ON TimeClock.UserID = LM_Users.UserID"&_
	" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate4) &"')"&_
	" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate4) &"')"&_
	" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate4) &"')"&_
	" AND CAST(LM_Users.Salery AS Numeric) = 0 AND TimeClock.CType <> 9  And (TimeClock.LocationID="& LocationID &")"
	Cnt = 1
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		DO While NOT rsData.eof 
			strSQL2=" SELECT UserID, Cdatetime, Caction"&_
					" FROM TimeClock(NOLOCK)"&_
					" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate4) &"')"&_
					" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate4) &"')"&_
					" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate4) &"')"&_
					" AND (UserID = "& rsData("UserID") &")  And LocationID="& LocationID &" ORDER BY Cdatetime, Caction"
			Cnt2 = 0
			If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
				DO While NOT rsData2.eof 
					Cnt2=cnt2+1
					IF cnt2=1 then
						intCaction = rsData2("Caction")
						dtCdatetime = rsData2("Cdatetime")
						strHours = 0
					ELSE
						IF rsData2("Caction") = 1 then
							intCaction = rsData2("Caction")
							strHours = strHours + DateDiff("n",dtCdatetime,rsData2("Cdatetime"))
						ELSE
							intCaction = rsData2("Caction")
							dtCdatetime = rsData2("Cdatetime")
						END IF					
					END IF
					rsData2.MoveNext
				Loop
			END IF
			Set rsData2 = Nothing
			IF CNT2 = 1 then
				strHours = strHours + DateDiff("n",dtCdatetime,now())
			END IF
			strHours = round(strHours/60,2)
			strTotalHrs = strTotalHrs+strHours
			cnt = cnt+1
			rsData.MoveNext
		Loop
	END IF
	Set rsData = Nothing
	strGTotalHrs = strGTotalHrs+strTotalHrs
                    %>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatnumber(strTotalHrs,2)%>&nbsp;</label></td>
                    </tr>


                    <%
	strCounts = 0
	strTotalCnt = 0
				strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
					" INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate4) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate4) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate4) &"')"&_
			" AND (RECITEM.ProdID IN ("& strList1 &")) And (REC.LocationID="& LocationID &") And (RECITEM.LocationID="& LocationID &") and (Rec.status >= 70 )"
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
		strGwash = strGwash + strTotalCnt
                    %>
                    <tr>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
                    </tr>
                    <%
	strCounts = 0
	strTotalCnt = 0

				strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
					" INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate4) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate4) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate4) &"')"&_
			" AND (RECITEM.ProdID IN ("& strList2&")) And (REC.LocationID="& LocationID &") And (RECITEM.LocationID="& LocationID &") and (Rec.status >= 70 )"
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
		strGdetail = strGdetail + strTotalCnt
                    %>
                    <tr>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
                    </tr>


                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
	strDiff = 0
	strCOChecks = 0
	strCOCreditCards = 0
	strCOCreditCards2 = 0
	strCOCreditCards3 = 0
	strCOCreditCards4 = 0
	strCOPayouts = 0

	strSQL = "SELECT isnull(CITotal,0) as CITotal,isnull(COTotal,0) as COTotal,(isnull(COTotal,0)-isnull(CITotal,0)) as Diff,isnull(COChecks,0) as COChecks,isnull(COCreditCards,0) as COCreditCards,"&_
	" isnull(COCreditCards2,0)as COCreditCards2,"&_
	" isnull(COCreditCards3,0)as COCreditCards3,"&_
	" isnull(COCreditCards4,0)as COCreditCards4,"&_
	" isnull(COPayouts,0)as COPayouts"&_
	" FROM cashd (NOLOCK)"&_
	" WHERE ndate = '"& txtRptDate4 &"' And LocationID="& LocationID
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		do while NOT rsData.eof  
				strCounts = strCounts + (rsData("Diff")+rsData("COChecks")+rsData("COCreditCards")+rsData("COCreditCards2")+rsData("COCreditCards3")+rsData("COCreditCards4"))-rsData("COPayouts")
				intTotalPay = intTotalPay+strCounts
				strDiff = strDiff + rsData("Diff")
				strCOChecks = strCOChecks + rsData("COChecks")
				strCOCreditCards =  strCOCreditCards + rsData("COCreditCards")
			strCOCreditCards2 = strCOCreditCards2 + rsData("COCreditCards2")
				strCOCreditCards3 = strCOCreditCards3 + rsData("COCreditCards3")
				strCOCreditCards4 = strCOCreditCards4 + rsData("COCreditCards4")
				strCOPayouts = strCOPayouts + rsData("COPayouts")
		rsData.MoveNext
		loop
	END IF
	Set rsData = Nothing
	strGDrawer = strGDrawer + strDiff
	strgCOChecks = strgCOChecks +strCOChecks
	strgCOCreditCards =  strgCOCreditCards + strCOCreditCards
	strgCOCreditCards2 = strgCOCreditCards2 + strCOCreditCards2
	strgCOCreditCards3 = strgCOCreditCards3 + strCOCreditCards3
	strgCOCreditCards4 = strgCOCreditCards4 + strCOCreditCards4
	strgCOPayouts = strgCOPayouts + strCOPayouts
	strgTotal = strgTotal + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strDiff,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOChecks,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards2,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards3,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards4,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOPayouts,2)%>&nbsp;</label></td>
                    </tr>



                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(Totalamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate4) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate4) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate4) &"') And Rec.LocationID="& LocationID
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
		strgsales = strgsales + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(Tax) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate4) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate4) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate4) &"') And Rec.LocationID="& LocationID
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
		strgtax = strgtax + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(gTotal) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate4) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate4) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate4) &"') And Rec.LocationID="& LocationID
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
		strGGTotal = strGGTotal +strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(cashamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate4) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate4) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate4) &"') And Rec.LocationID="& LocationID
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
			strGCash = strGCash + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
	strTotalPaid = 0
	intTotalPay = 0
	strTotalPaid = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(Checkamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate4) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate4) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate4) &"') And Rec.LocationID="& LocationID
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
			strGCheck = strGCheck + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>

                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(chargeamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate4) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate4) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate4) &"') And Rec.LocationID="& LocationID

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
			strGcharge = strGcharge + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(Accamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate4) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate4) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate4) &"') And Rec.LocationID="& LocationID

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
			strGaccount = strGaccount + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(GiftCardamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate4) &"')"&_
					" AND (DATEPART(Day, REC.CloseDte) = '"& Day(txtRptDate4) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate4) &"') And Rec.LocationID="& LocationID

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
			strGGiftCard = strGGiftCard + strCounts
			strgTotalPaid = strgTotalPaid + strTotalPaid
			strGdiff = strGdiff + strTotalPaid-intTotalPay
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=FormatCurrency(strTotalPaid,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=FormatCurrency((strTotalPaid-intTotalPay),2)%>&nbsp;</label></td>
                    </tr>
                    <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(CashBack) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate4) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate4) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate4) &"') And Rec.LocationID="& LocationID

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
			strGCashBack = strGCashBack + strCounts
                    %>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>





                </table>
            </td>
            <td valign="top">
                <table cellspacing="0" border="2" cellpadding="1" width="100">
                    <% 
	strHours = 0
	strTotalHrs = 0
	strCounts = 0
	strTotalCnt = 0
	strTotalPaid = 0
	intTotalPay=0
                    %>
                    <tr>
                        <td align="center" colspan="2">
                            <label class="control">&nbsp;Thursday&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <label class="control">&nbsp;<%=txtRptDate5%>&nbsp;</label></td>
                    </tr>

                    <%	strSQL=" SELECT DISTINCT TimeClock.UserID"&_
	" FROM TimeClock (NOLOCK) INNER JOIN LM_Users(NOLOCK) ON TimeClock.UserID = LM_Users.UserID"&_
	" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate5) &"')"&_
	" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate5) &"')"&_
	" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate5) &"')"&_
	" AND CAST(LM_Users.Salery AS Numeric) = 0 AND TimeClock.CType <> 9 And (TimeClock.LocationID="& LocationID &")"
	Cnt = 1
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		DO While NOT rsData.eof 
			strSQL2=" SELECT UserID, Cdatetime, Caction"&_
					" FROM TimeClock(NOLOCK)"&_
					" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate5) &"')"&_
					" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate5) &"')"&_
					" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate5) &"')"&_
					" AND (UserID = "& rsData("UserID") &") ORDER BY Cdatetime, Caction"
			Cnt2 = 0
			If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
				DO While NOT rsData2.eof 
					Cnt2=cnt2+1
					IF cnt2=1 then
						intCaction = rsData2("Caction")
						dtCdatetime = rsData2("Cdatetime")
						strHours = 0
					ELSE
						IF rsData2("Caction") = 1 then
							intCaction = rsData2("Caction")
							strHours = strHours + DateDiff("n",dtCdatetime,rsData2("Cdatetime"))
						ELSE
							intCaction = rsData2("Caction")
							dtCdatetime = rsData2("Cdatetime")
						END IF					
					END IF
					rsData2.MoveNext
				Loop
			END IF
			Set rsData2 = Nothing
			IF CNT2 = 1 then
				strHours = strHours + DateDiff("n",dtCdatetime,now())
			END IF
			strHours = round(strHours/60,2)
			strTotalHrs = strTotalHrs+strHours
			cnt = cnt+1
			rsData.MoveNext
		Loop
	END IF
	Set rsData = Nothing
	strGTotalHrs = strGTotalHrs+strTotalHrs
                    %>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatnumber(strTotalHrs,2)%>&nbsp;</label></td>
                    </tr>


                    <%
				strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
					" INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate5) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate5) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate5) &"')"&_
			" AND (RECITEM.ProdID IN ("& strList1 &")) And (REC.LocationID="& LocationID &") And (RECITEM.LocationID="& LocationID &") and (Rec.status >= 70 )"
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
		strGwash = strGwash + strTotalCnt
                    %>
                    <tr>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
                    </tr>
                    <%
	strCounts = 0
	strTotalCnt = 0

				strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
					" INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate5) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate5) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate5) &"')"&_
			" AND (RECITEM.ProdID IN ("& strList2 &")) And (REC.LocationID="& LocationID &") And (RECITEM.LocationID="& LocationID &") and (Rec.status >= 70 )"
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
		strGdetail = strGdetail + strTotalCnt
                    %>
                    <tr>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
                    </tr>


                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
	strDiff = 0
	strCOChecks = 0
	strCOCreditCards = 0
	strCOCreditCards2 = 0
	strCOCreditCards3 = 0
	strCOCreditCards4 = 0
	strCOPayouts = 0

	strSQL = "SELECT isnull(CITotal,0) as CITotal,isnull(COTotal,0) as COTotal,(isnull(COTotal,0)-isnull(CITotal,0)) as Diff,isnull(COChecks,0) as COChecks,isnull(COCreditCards,0)as COCreditCards,"&_
	" isnull(COCreditCards2,0)as COCreditCards2,"&_
	" isnull(COCreditCards3,0)as COCreditCards3,"&_
	" isnull(COCreditCards4,0)as COCreditCards4,"&_
	" isnull(COPayouts,0)as COPayouts"&_
	" FROM cashd (NOLOCK)"&_
	" WHERE ndate = '"& txtRptDate5 &"' And LocationID="& LocationID
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		do while NOT rsData.eof  
				strCounts = strCounts + (rsData("Diff")+rsData("COChecks")+rsData("COCreditCards")+rsData("COCreditCards2")+rsData("COCreditCards3")+rsData("COCreditCards4"))-rsData("COPayouts")
				intTotalPay = intTotalPay+strCounts
				strDiff = strDiff + rsData("Diff")
				strCOChecks = strCOChecks + rsData("COChecks")
				strCOCreditCards =  strCOCreditCards + rsData("COCreditCards")
			strCOCreditCards2 = strCOCreditCards2 + rsData("COCreditCards2")
				strCOCreditCards3 = strCOCreditCards3 + rsData("COCreditCards3")
				strCOCreditCards4 = strCOCreditCards4 + rsData("COCreditCards4")
				strCOPayouts = strCOPayouts + rsData("COPayouts")
		rsData.MoveNext
		loop
	END IF
	Set rsData = Nothing
	strGDrawer = strGDrawer + strDiff
	strgCOChecks = strgCOChecks +strCOChecks
	strgCOCreditCards =  strgCOCreditCards + strCOCreditCards
	strgCOCreditCards2 = strgCOCreditCards2 + strCOCreditCards2
	strgCOCreditCards3 = strgCOCreditCards3 + strCOCreditCards3
	strgCOCreditCards4 = strgCOCreditCards4 + strCOCreditCards4
	strgCOPayouts = strgCOPayouts + strCOPayouts
	strgTotal = strgTotal + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strDiff,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOChecks,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards2,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards3,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards4,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOPayouts,2)%>&nbsp;</label></td>
                    </tr>



                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(Totalamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate5) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate5) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate5) &"') And LocationID="& LocationID
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
		strgsales = strgsales + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(Tax) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate5) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate5) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate5) &"') And LocationID="& LocationID
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
		strgtax = strgtax + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(gTotal) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate5) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate5) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate5) &"') And LocationID="& LocationID
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
		strGGTotal = strGGTotal +strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(cashamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate5) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate5) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate5) &"') And LocationID="& LocationID
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
			strGCash = strGCash + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(Checkamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate5) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate5) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate5) &"') And LocationID="& LocationID
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
			strGCheck = strGCheck + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>

                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(chargeamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate5) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate5) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate5) &"') And LocationID="& LocationID

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
			strGcharge = strGcharge + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(Accamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate5) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate5) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate5) &"') And LocationID="& LocationID

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
			strGaccount = strGaccount + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(GiftCardamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate5) &"')"&_
					" AND (DATEPART(Day, REC.CloseDte) = '"& Day(txtRptDate5) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate5) &"') And LocationID="& LocationID

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
			strGGiftCard = strGGiftCard + strCounts
			strgTotalPaid = strgTotalPaid + strTotalPaid
			strGdiff = strGdiff + strTotalPaid-intTotalPay
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=FormatCurrency(strTotalPaid,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=FormatCurrency((strTotalPaid-intTotalPay),2)%>&nbsp;</label></td>
                    </tr>
                    <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(CashBack) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate5) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate5) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate5) &"') And LocationID="& LocationID

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
			strGCashBack = strGCashBack + strCounts
                    %>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>





                </table>
            </td>
            <td valign="top">
                <table cellspacing="0" border="2" cellpadding="1" width="100">
                    <% 
	strHours = 0
	strTotalHrs = 0
	strCounts = 0
	strTotalCnt = 0
	strTotalPaid = 0
	intTotalPay=0
                    %>
                    <tr>
                        <td align="center" colspan="2">
                            <label class="control">&nbsp;Friday&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <label class="control">&nbsp;<%=txtRptDate6%>&nbsp;</label></td>
                    </tr>

                    <%	strSQL=" SELECT DISTINCT TimeClock.UserID"&_
	" FROM TimeClock (NOLOCK) INNER JOIN LM_Users(NOLOCK) ON TimeClock.UserID = LM_Users.UserID"&_
	" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate6) &"')"&_
	" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate6) &"')"&_
	" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate6) &"')"&_
	" AND CAST(LM_Users.Salery AS Numeric) = 0 AND TimeClock.CType <> 9 And (TimeClock.LocationID="& LocationID &")"
	Cnt = 1
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		DO While NOT rsData.eof 
			strSQL2=" SELECT UserID, Cdatetime, Caction"&_
					" FROM TimeClock(NOLOCK)"&_
					" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate6) &"')"&_
					" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate6) &"')"&_
					" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate6) &"')"&_
					" AND (UserID = "& rsData("UserID") &") And LocationID="& LocationID &" ORDER BY Cdatetime, Caction"
			Cnt2 = 0
			If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
				DO While NOT rsData2.eof 
					Cnt2=cnt2+1
					IF cnt2=1 then
						intCaction = rsData2("Caction")
						dtCdatetime = rsData2("Cdatetime")
						strHours = 0
					ELSE
						IF rsData2("Caction") = 1 then
							intCaction = rsData2("Caction")
							strHours = strHours + DateDiff("n",dtCdatetime,rsData2("Cdatetime"))
						ELSE
							intCaction = rsData2("Caction")
							dtCdatetime = rsData2("Cdatetime")
						END IF					
					END IF
					rsData2.MoveNext
				Loop
			END IF
			Set rsData2 = Nothing
			IF CNT2 = 1 then
				strHours = strHours + DateDiff("n",dtCdatetime,now())
			END IF
			strHours = round(strHours/60,2)
			strTotalHrs = strTotalHrs+strHours
			cnt = cnt+1
			rsData.MoveNext
		Loop
	END IF
	Set rsData = Nothing
	strGTotalHrs = strGTotalHrs+strTotalHrs
                    %>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatnumber(strTotalHrs,2)%>&nbsp;</label></td>
                    </tr>


                    <%
				strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
					" INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate6) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate6) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate6) &"')"&_
			" AND (RECITEM.ProdID IN ("& strList1 &")) And (REC.LocationID="& LocationID &") And (RECITEM.LocationID="& LocationID &") and (Rec.status >= 70 )"
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
		strGwash = strGwash + strTotalCnt
                    %>
                    <tr>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
                    </tr>
                    <%
	strCounts = 0
	strTotalCnt = 0

				strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
					" INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate6) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate6) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate6) &"')"&_
			" AND (RECITEM.ProdID IN ("& strList2 &")) And (REC.LocationID="& LocationID &") And (RECITEM.LocationID="& LocationID &") and (Rec.status >= 70 )"
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
		strGdetail = strGdetail + strTotalCnt
                    %>
                    <tr>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
                    </tr>


                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
	strDiff = 0
	strCOChecks = 0
	strCOCreditCards = 0
	strCOCreditCards2 = 0
	strCOCreditCards3 = 0
	strCOCreditCards4 = 0
	strCOPayouts = 0

	strSQL = "SELECT isnull(CITotal,0) as CITotal,isnull(COTotal,0) as COTotal,(isnull(COTotal,0)-isnull(CITotal,0)) as Diff,isnull(COChecks,0) as COChecks,isnull(COCreditCards,0)as COCreditCards,"&_
	" isnull(COCreditCards2,0)as COCreditCards2,"&_
	" isnull(COCreditCards3,0)as COCreditCards3,"&_
	" isnull(COCreditCards4,0)as COCreditCards4,"&_
	" isnull(COPayouts,0)as COPayouts"&_
	" FROM cashd (NOLOCK)"&_
	" WHERE ndate = '"& txtRptDate6 &"' And LocationID="& LocationID
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		do while NOT rsData.eof  
				strCounts = strCounts + (rsData("Diff")+rsData("COChecks")+rsData("COCreditCards")+rsData("COCreditCards2")+rsData("COCreditCards3")+rsData("COCreditCards4"))-rsData("COPayouts")
				intTotalPay = intTotalPay+strCounts
				strDiff = strDiff + rsData("Diff")
				strCOChecks = strCOChecks + rsData("COChecks")
				strCOCreditCards =  strCOCreditCards + rsData("COCreditCards")
			strCOCreditCards2 = strCOCreditCards2 + rsData("COCreditCards2")
				strCOCreditCards3 = strCOCreditCards3 + rsData("COCreditCards3")
				strCOCreditCards4 = strCOCreditCards4 + rsData("COCreditCards4")
				strCOPayouts = strCOPayouts + rsData("COPayouts")
		rsData.MoveNext
		loop
	END IF
	Set rsData = Nothing
	strGDrawer = strGDrawer + strDiff
	strgCOChecks = strgCOChecks +strCOChecks
	strgCOCreditCards =  strgCOCreditCards + strCOCreditCards
	strgCOCreditCards2 = strgCOCreditCards2 + strCOCreditCards2
	strgCOCreditCards3 = strgCOCreditCards3 + strCOCreditCards3
	strgCOCreditCards4 = strgCOCreditCards4 + strCOCreditCards4
	strgCOPayouts = strgCOPayouts + strCOPayouts
	strgTotal = strgTotal + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strDiff,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOChecks,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards2,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards3,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards4,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOPayouts,2)%>&nbsp;</label></td>
                    </tr>



                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(Totalamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate6) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate6) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate6) &"') And LocationID="& LocationID
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
		strgsales = strgsales + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(Tax) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate6) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate6) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate6) &"') And LocationID="& LocationID
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
		strgtax = strgtax + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(gTotal) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate6) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate6) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate6) &"') And LocationID="& LocationID
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
		strGGTotal = strGGTotal +strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(cashamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate6) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate6) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate6) &"') And LocationID="& LocationID
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
			strGCash = strGCash + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(Checkamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate6) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate6) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate6) &"') And LocationID="& LocationID
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
			strGCheck = strGCheck + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>

                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(chargeamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate6) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate6) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate6) &"') And LocationID="& LocationID

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
			strGcharge = strGcharge + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(Accamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate6) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate6) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate6) &"') And LocationID="& LocationID

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
			strGaccount = strGaccount + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(GiftCardamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate6) &"')"&_
					" AND (DATEPART(Day, REC.CloseDte) = '"& Day(txtRptDate6) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate6) &"') And LocationID="& LocationID

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
			strGGiftCard = strGGiftCard + strCounts
			strgTotalPaid = strgTotalPaid + strTotalPaid
			strGdiff = strGdiff + strTotalPaid-intTotalPay
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=FormatCurrency(strTotalPaid,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=FormatCurrency((strTotalPaid-intTotalPay),2)%>&nbsp;</label></td>
                    </tr>
                    <%
	strCounts = 0
	strTotalCnt = 0
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
		strSQL2 = "SELECT sum(CashBack) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate6) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate6) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate6) &"') And LocationID="& LocationID

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
			strGCashBack = strGCashBack + strCounts
                    %>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>





                </table>
            </td>
            <td valign="top">
                <table cellspacing="0" border="2" cellpadding="1" width="100">
                    <% 
	strHours = 0
	strTotalHrs = 0
	strCounts = 0
	strTotalCnt = 0
	strTotalPaid = 0
	intTotalPay=0
                    %>
                    <tr>
                        <td align="center" colspan="2">
                            <label class="control">&nbsp;Saturday&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <label class="control">&nbsp;<%=txtRptDate7%>&nbsp;</label></td>
                    </tr>

                    <%	strSQL=" SELECT DISTINCT TimeClock.UserID"&_
	" FROM TimeClock (NOLOCK) INNER JOIN LM_Users(NOLOCK) ON TimeClock.UserID = LM_Users.UserID"&_
	" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate7) &"')"&_
	" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate7) &"')"&_
	" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate7) &"')"&_
	" AND CAST(LM_Users.Salery AS Numeric) = 0 AND TimeClock.CType <> 9 And (TimeClock.LocationID="& LocationID &")"
	Cnt = 1
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		DO While NOT rsData.eof 
			strSQL2=" SELECT UserID, Cdatetime, Caction"&_
					" FROM TimeClock(NOLOCK)"&_
					" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate7) &"')"&_
					" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate7) &"')"&_
					" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate7) &"')"&_
					" AND (UserID = "& rsData("UserID") &") And LocationID="& LocationID &" ORDER BY Cdatetime, Caction"
			Cnt2 = 0
			If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
				DO While NOT rsData2.eof 
					Cnt2=cnt2+1
					IF cnt2=1 then
						intCaction = rsData2("Caction")
						dtCdatetime = rsData2("Cdatetime")
						strHours = 0
					ELSE
						IF rsData2("Caction") = 1 then
							intCaction = rsData2("Caction")
							strHours = strHours + DateDiff("n",dtCdatetime,rsData2("Cdatetime"))
						ELSE
							intCaction = rsData2("Caction")
							dtCdatetime = rsData2("Cdatetime")
						END IF					
					END IF
					rsData2.MoveNext
				Loop
			END IF
			Set rsData2 = Nothing
			IF CNT2 = 1 then
				strHours = strHours + DateDiff("n",dtCdatetime,now())
			END IF
			strHours = round(strHours/60,2)
			strTotalHrs = strTotalHrs+strHours
			cnt = cnt+1
			rsData.MoveNext
		Loop
	END IF
	Set rsData = Nothing
	strGTotalHrs = strGTotalHrs+strTotalHrs
                    %>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatnumber(strTotalHrs,2)%>&nbsp;</label></td>
                    </tr>


                    <%
				strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
					" INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate7) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate7) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate7) &"')"&_
			" AND (RECITEM.ProdID IN ("& strList1 &")) And (REC.LocationID="& LocationID &") And (RECITEM.LocationID="& LocationID &") and (Rec.status >= 70 )"
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
		strGwash = strGwash + strTotalCnt
                    %>
                    <tr>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
                    </tr>
                    <%
	strCounts = 0
	strTotalCnt = 0

				strSQL2=" SELECT SUM(1) AS cnt FROM REC(NOLOCK)"&_
					" INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate7) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate7) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate7) &"')"&_
			" AND (RECITEM.ProdID IN ("& strList2 &")) And (REC.LocationID="& LocationID &") And (RECITEM.LocationID="& LocationID &") and (Rec.status >= 70 )"
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
		strGdetail = strGdetail + strTotalCnt
                    %>
                    <tr>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=strTotalCnt%>&nbsp;</label></td>
                    </tr>


                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
	strDiff = 0
	strCOChecks = 0
	strCOCreditCards = 0
	strCOCreditCards2 = 0
	strCOCreditCards3 = 0
	strCOCreditCards4 = 0
	strCOPayouts = 0

	strSQL = "SELECT isnull(CITotal,0) as CITotal,isnull(COTotal,0) as COTotal,(isnull(COTotal,0)-isnull(CITotal,0)) as Diff,isnull(COChecks,0) as COChecks,isnull(COCreditCards,0)as COCreditCards,"&_
	" isnull(COCreditCards2,0)as COCreditCards2,"&_
	" isnull(COCreditCards3,0)as COCreditCards3,"&_
	" isnull(COCreditCards4,0)as COCreditCards4,"&_
	" isnull(COPayouts,0)as COPayouts"&_
	" FROM cashd (NOLOCK)"&_
	" WHERE ndate = '"& txtRptDate7 &"'  And LocationID="& LocationID
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		do while NOT rsData.eof  
				strCounts = strCounts + (rsData("Diff")+rsData("COChecks")+rsData("COCreditCards")+rsData("COCreditCards2")+rsData("COCreditCards3")+rsData("COCreditCards4"))-rsData("COPayouts")
				intTotalPay = intTotalPay+strCounts
				strDiff = strDiff + rsData("Diff")
				strCOChecks = strCOChecks + rsData("COChecks")
				strCOCreditCards =  strCOCreditCards + rsData("COCreditCards")
			strCOCreditCards2 = strCOCreditCards2 + rsData("COCreditCards2")
				strCOCreditCards3 = strCOCreditCards3 + rsData("COCreditCards3")
				strCOCreditCards4 = strCOCreditCards4 + rsData("COCreditCards4")
				strCOPayouts = strCOPayouts + rsData("COPayouts")
		rsData.MoveNext
		loop
	END IF
	Set rsData = Nothing
	strGDrawer = strGDrawer + strDiff
	strgCOChecks = strgCOChecks +strCOChecks
	strgCOCreditCards =  strgCOCreditCards + strCOCreditCards
	strgCOCreditCards2 = strgCOCreditCards2 + strCOCreditCards2
	strgCOCreditCards3 = strgCOCreditCards3 + strCOCreditCards3
	strgCOCreditCards4 = strgCOCreditCards4 + strCOCreditCards4
	strgCOPayouts = strgCOPayouts + strCOPayouts
	strgTotal = strgTotal + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strDiff,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOChecks,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards2,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards3,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOCreditCards4,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCOPayouts,2)%>&nbsp;</label></td>
                    </tr>



                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(Totalamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate7) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate7) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate7) &"') And LocationID="& LocationID
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
		strgsales = strgsales + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(Tax) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate7) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate7) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate7) &"') And LocationID="& LocationID
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
		strgtax = strgtax + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(gTotal) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate7) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate7) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate7) &"') And LocationID="& LocationID
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
		strGGTotal = strGGTotal +strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT Sum(cashamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate7) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate7) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate7) &"') And LocationID="& LocationID
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
			strGCash = strGCash + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(Checkamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate7) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate7) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate7) &"') And LocationID="& LocationID
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
			strGCheck = strGCheck + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>

                    <tr>
                        <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(chargeamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate7) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate7) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate7) &"') And LocationID="& LocationID

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
			strGcharge = strGcharge + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(Accamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate7) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate7) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate7) &"') And LocationID="& LocationID

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
			strGaccount = strGaccount + strCounts
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(GiftCardamt) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate7) &"')"&_
					" AND (DATEPART(Day, REC.CloseDte) = '"& Day(txtRptDate7) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate7) &"') And LocationID="& LocationID

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
			strGGiftCard = strGGiftCard + strCounts
			strgTotalPaid = strgTotalPaid + strTotalPaid
			strGdiff = strGdiff + strTotalPaid-intTotalPay
                        %>
                        <td style="text-align: right">
                            <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=FormatCurrency(strTotalPaid,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=FormatCurrency((strTotalPaid-intTotalPay),2)%>&nbsp;</label></td>
                    </tr>
                    <%
		'Sum(accamt), tax, gtotal, cashamt, chargeamt, cashback,totalamt
	strCounts = 0
	strTotalCnt = 0
		strSQL2 = "SELECT sum(CashBack) as cnt"&_
					" FROM REC(NOLOCK)"&_
					" WHERE (DATEPART(Month, REC.Closedte) = '"& Month(txtRptDate7) &"')"&_
					" AND (DATEPART(Day, REC.Closedte) = '"& Day(txtRptDate7) &"')"&_
					" AND (DATEPART(Year, REC.Closedte) = '"& Year(txtRptDate7) &"') And LocationID="& LocationID

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
			strGCashBack = strGCashBack + strCounts
                    %>
                    <td style="text-align: right">
                        <label class="control">&nbsp;<%=FormatCurrency(strCounts,2)%>&nbsp;</label></td>
                    </tr>



                </table>
            </td>
            <td valign="top">
                <table cellspacing="0" border="2" cellpadding="1" width="100">
                    <tr>
                        <td align="center" colspan="2">
                            <label class="redcontrol">&nbsp;Totals</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control">&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatnumber(strGTotalHrs,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=strGwash%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=strGdetail%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strGDrawer,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strgCOChecks,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strgCOCreditCards,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strgCOCreditCards2,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strgCOCreditCards3,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strgCOCreditCards4,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strgCOPayouts,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strgTotal,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strgsales,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strgtax,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strGGTotal,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strGCash,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strGcheck,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strGcharge,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strGaccount,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strGGiftCard,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strgTotalPaid,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strGdiff,2)%>&nbsp;</label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <label class="control"><%=formatCurrency(strGCashBack,2)%>&nbsp;</label></td>
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
' Client-Side Functions
'********************************************************************
%>
<script type="text/vbscript">
Option Explicit
Dim obj
Sub Window_Onload()
    LoadText.innerHTML="" 
End Sub




Sub btnUpdate_OnClick()
	If len(frmMain.intYear.value)>0 and len(frmMain.cboWeek.value)>0  then
        LoadText.innerHTML="Loading..." 
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
Function LoadWeeks(db,var,var2,LocationID)
	Dim strSQL,RS,strSel,temp,intType,blnDeleted,rsData2,strSQL2
	blnDeleted =False
	strSQL="SELECT weekNo, yearNo FROM TimeSheet where YearNo="&Var2 &" and LocationID="&LocationID

	If dbOpenRecordSet(db,rs,strSQL) Then

        IF  RS.EOF then
%>
<option value="1" selected>1</option>
<%
        
        ELSE
		    Do While Not RS.EOF
					    If Trim(var) = Trim(RS("weekNo")) Then
						    blnDeleted = true
						    strSel = "selected" 
%>
<option value="<%=RS("weekNo")%>" <%=strSel%>><%=RS("weekNo")%></option>
<%
					    Else
						    strSel = ""
%>
<option value="<%=RS("weekNo")%>" <%=strSel%>><%=RS("weekNo")%></option>
<%
					    End IF
			    RS.MoveNext
		    Loop
		END IF
	End If
End Function

%>
