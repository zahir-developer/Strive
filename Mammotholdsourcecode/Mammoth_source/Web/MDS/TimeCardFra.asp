<%@ LANGUAGE="VBSCRIPT" %>
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
Dim dbMain,intuserid,strSQL, RS,cnt,LocationID
Set dbMain =  OpenConnection

Dim dtSun,dtMon,dtTue,dtWed,dtThu,dtFri,dtSat,strFDW,dtFDW,dtPayDate
Dim strSunI1,strSunO1,strSunI2,strSunO2,strSunI3,strSunO3,strSunI4,strSunO4,strSunDA,strSunTo
Dim strMonI1,strMonO1,strMonI2,strMonO2,strMonI3,strMonO3,strMonI4,strMonO4,strMonDA,strMonTo
Dim strTueI1,strTueO1,strTueI2,strTueO2,strTueI3,strTueO3,strTueI4,strTueO4,strTueDA,strTueTo
Dim strWedI1,strWedO1,strWedI2,strWedO2,strWedI3,strWedO3,strWedI4,strWedO4,strWedDA,strWedTo
Dim strThuI1,strThuO1,strThuI2,strThuO2,strThuI3,strThuO3,strThuI4,strThuO4,strThuDA,strThuTo
Dim strFriI1,strFriO1,strFriI2,strFriO2,strFriI3,strFriO3,strFriI4,strFriO4,strFriDA,strFriTo
Dim strSatI1,strSatO1,strSatI2,strSatO2,strSatI3,strSatO3,strSatI4,strSatO4,strSatDA,strSatTo

Dim strSunIT1,strSunOT1,strSunIT2,strSunOT2,strSunIT3,strSunOT3,strSunIT4,strSunOT4
Dim strMonIT1,strMonOT1,strMonIT2,strMonOT2,strMonIT3,strMonOT3,strMonIT4,strMonOT4
Dim strTueIT1,strTueOT1,strTueIT2,strTueOT2,strTueIT3,strTueOT3,strTueIT4,strTueOT4
Dim strWedIT1,strWedOT1,strWedIT2,strWedOT2,strWedIT3,strWedOT3,strWedIT4,strWedOT4
Dim strThuIT1,strThuOT1,strThuIT2,strThuOT2,strThuIT3,strThuOT3,strThuIT4,strThuOT4
Dim strFriIT1,strFriOT1,strFriIT2,strFriOT2,strFriIT3,strFriOT3,strFriIT4,strFriOT4
Dim strSatIT1,strSatOT1,strSatIT2,strSatOT2,strSatIT3,strSatOT3,strSatIT4,strSatOT4

intuserid = request("userid")
LocationID = Request("LocationID")

strFDW = Weekday(Date)-1
dtFDW = Date()-strFDW
dtSun = dtFDW
cnt=1
strSQL = "SELECT Cdatetime FROM timeClock WHERE userid=" & intuserid &_
		" and DatePart(Month,CDatetime) = '"& Month(dtSun) &"'"&_
		" and DatePart(Day,CDatetime) = '"& Day(dtSun) &"'"&_
		" and DatePart(Year,CDatetime) = '"& Year(dtSun) &"'"&_
        " and LocationID="& LocationID &_
		" Order by CDatetime asc"
    IF dbOpenStaticRecordset(dbMain,rs,strSQL) then   
	IF NOT 	rs.EOF then
		Do while Not rs.EOF
			Select Case cnt
				Case 1
					strSunI1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 2
					strSunO1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 3
					strSunI2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 4
					strSunO2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 5
					strSunI3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 6
					strSunO3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 7
					strSunI4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 8
					strSunO4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
			End Select
			rs.MoveNext
			cnt = cnt+1
		Loop
	END IF
End If
strSunDA = timediff(strSunO1,strSunI1)+timediff(strSunO2,strSunI2)+timediff(strSunO3,strSunI3)+timediff(strSunO4,strSunI4)
strSunTo = round(strSunDA,2)
dtMon = dtFDW+1
cnt=1
strSQL = "SELECT Cdatetime FROM timeClock WHERE userid=" & intuserid &_
		" and DatePart(Month,CDatetime) = '"& Month(dtMon) &"'"&_
		" and DatePart(Day,CDatetime) = '"& Day(dtMon) &"'"&_
		" and DatePart(Year,CDatetime) = '"& Year(dtMon) &"'"&_
        " and LocationID="& LocationID &_
		" Order by CDatetime asc"
    IF dbOpenStaticRecordset(dbMain,rs,strSQL) then   
	IF NOT 	rs.EOF then
		Do while Not rs.EOF
			Select Case cnt
				Case 1
					strMonI1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 2
					strMonO1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 3
					strMonI2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 4
					strMonO2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 5
					strMonI3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 6
					strMonO3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 7
					strMonI4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 8
					strMonO4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
			End Select
			rs.MoveNext
			cnt = cnt+1
		Loop
	END IF
End If
strMonDA = timediff(strMonO1,strMonI1)+timediff(strMonO2,strMonI2)+timediff(strMonO3,strMonI3)+timediff(strMonO4,strMonI4)
strMonTo = round(strMonDA,2)+strSunTo
dtTue = dtFDW+2
cnt=1
strSQL = "SELECT Cdatetime FROM timeClock WHERE userid=" & intuserid &_
		" and DatePart(Month,CDatetime) = '"& Month(dtTue) &"'"&_
		" and DatePart(Day,CDatetime) = '"& Day(dtTue) &"'"&_
		" and DatePart(Year,CDatetime) = '"& Year(dtTue) &"'"&_
        " and LocationID="& LocationID &_
		" Order by CDatetime asc"
    IF dbOpenStaticRecordset(dbMain,rs,strSQL) then   
	IF NOT 	rs.EOF then
		Do while Not rs.EOF
			Select Case cnt
				Case 1
					strTueI1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 2
					strTueO1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 3
					strTueI2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 4
					strTueO2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 5
					strTueI3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 6
					strTueO3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 7
					strTueI4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 8
					strTueO4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
			End Select
			rs.MoveNext
			cnt = cnt+1
		Loop
	END IF
End If
strTueDA = timediff(strTueO1,strTueI1)+timediff(strTueO2,strTueI2)+timediff(strTueO3,strTueI3)+timediff(strTueO4,strTueI4)
strTueTo = round(strTueDA,2)+strMonTo
dtWed = dtFDW+3
cnt=1
strSQL = "SELECT Cdatetime FROM timeClock WHERE userid=" & intuserid &_
		" and DatePart(Month,CDatetime) = '"& Month(dtWed) &"'"&_
		" and DatePart(Day,CDatetime) = '"& Day(dtWed) &"'"&_
		" and DatePart(Year,CDatetime) = '"& Year(dtWed) &"'"&_
        " and LocationID="& LocationID &_
		" Order by CDatetime asc"
    IF dbOpenStaticRecordset(dbMain,rs,strSQL) then   
	IF NOT 	rs.EOF then
		Do while Not rs.EOF
			Select Case cnt
				Case 1
					strWedI1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 2
					strWedO1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 3
					strWedI2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 4
					strWedO2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 5
					strWedI3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 6
					strWedO3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 7
					strWedI4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 8
					strWedO4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
			End Select
			rs.MoveNext
			cnt = cnt+1
		Loop
	END IF
End If
strWedDA = timediff(strWedO1,strWedI1)+timediff(strWedO2,strWedI2)+timediff(strWedO3,strWedI3)+timediff(strWedO4,strWedI4)
strWedTo = round(strWedDA,2)+strTueTo
dtThu = dtFDW+4
cnt=1
strSQL = "SELECT Cdatetime FROM timeClock WHERE userid=" & intuserid &_
		" and DatePart(Month,CDatetime) = '"& Month(dtThu) &"'"&_
		" and DatePart(Day,CDatetime) = '"& Day(dtThu) &"'"&_
		" and DatePart(Year,CDatetime) = '"& Year(dtThu) &"'"&_
        " and LocationID="& LocationID &_
		" Order by CDatetime asc"
    IF dbOpenStaticRecordset(dbMain,rs,strSQL) then   
	IF NOT 	rs.EOF then
		Do while Not rs.EOF
			Select Case cnt
				Case 1
					strThuI1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 2
					strThuO1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 3
					strThuI2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 4
					strThuO2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 5
					strThuI3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 6
					strThuO3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 7
					strThuI4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 8
					strThuO4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
			End Select
			rs.MoveNext
			cnt = cnt+1
		Loop
	END IF
End If
strThuDA = timediff(strThuO1,strThuI1)+timediff(strThuO2,strThuI2)+timediff(strThuO3,strThuI3)+timediff(strThuO4,strThuI4)
strThuTo = round(strThuDA,2)+strWedTo
dtFri = dtFDW+5
cnt=1
strSQL = "SELECT Cdatetime FROM timeClock WHERE userid=" & intuserid &_
		" and DatePart(Month,CDatetime) = '"& Month(dtFri) &"'"&_
		" and DatePart(Day,CDatetime) = '"& Day(dtFri) &"'"&_
		" and DatePart(Year,CDatetime) = '"& Year(dtFri) &"'"&_
        " and LocationID="& LocationID &_
		" Order by CDatetime asc"
    IF dbOpenStaticRecordset(dbMain,rs,strSQL) then   
	IF NOT 	rs.EOF then
		Do while Not rs.EOF
			Select Case cnt
				Case 1
					strFriI1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 2
					strFriO1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 3
					strFriI2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 4
					strFriO2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 5
					strFriI3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 6
					strFriO3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 7
					strFriI4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 8
					strFriO4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
			End Select
			rs.MoveNext
			cnt = cnt+1
		Loop
	END IF
End If
strFriDA = timediff(strFriO1,strFriI1)+timediff(strFriO2,strFriI2)+timediff(strFriO3,strFriI3)+timediff(strFriO4,strFriI4)
strFriTo = round(strFriDA,2)+strThuTo
dtSat = dtFDW+6
cnt=1
strSQL = "SELECT Cdatetime FROM timeClock WHERE userid=" & intuserid &_
		" and DatePart(Month,CDatetime) = '"& Month(dtSat) &"'"&_
		" and DatePart(Day,CDatetime) = '"& Day(dtSat) &"'"&_
		" and DatePart(Year,CDatetime) = '"& Year(dtSat) &"'"&_
        " and LocationID="& LocationID &_
		" Order by CDatetime asc"
    IF dbOpenStaticRecordset(dbMain,rs,strSQL) then   
	IF NOT 	rs.EOF then
		Do while Not rs.EOF
			Select Case cnt
				Case 1
					strSatI1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 2
					strSatO1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 3
					strSatI2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 4
					strSatO2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 5
					strSatI3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 6
					strSatO3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 7
					strSatI4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 8
					strSatO4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
			End Select
			rs.MoveNext
			cnt = cnt+1
		Loop
	END IF
End If
strSatDA = timediff(strSatO1,strSatI1)+timediff(strSatO2,strSatI2)+timediff(strSatO3,strSatI3)+timediff(strSatO4,strSatI4)
strSatTo = round(strSatDA,2)+strFriTo
dtPayDate = dtFDW+12
'********************************************************************
'HTML
'********************************************************************
%>
<html>
<head>
<link rel="stylesheet" href="main.css" type="text/css">
<title></title>
</head>
<body class=pgbody>
<input type=hidden name=intuserid value="<%=intuserid%>">
<Input type="hidden" name="LocationID" value="<%=LocationID%>" />
<table cellspacing="0" width="700" class="Data">
	<tr>
		<td align="right" class="control" nowrap>Pay Period:&nbsp;&nbsp;</td>
		<td align="left" class="control" nowrap><Label class=control><%=dtSun%>&nbsp;&nbsp;to&nbsp;&nbsp;<%=dtSat%></label></td>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="right" class="control" nowrap>&nbsp;</td>
		<td align="right" class="control" nowrap>Pay Date:&nbsp;&nbsp;</td>
		<td align="left" class="control" nowrap><Label class=control><%=dtPayDate%></label></td>
	</tr>
</table>
<table cellspacing="0" width="700" class="Data">
	<tr >
     <td colspan=12 class="Header" align="center" width="700">Time Card</td>
    </tr>
	<tr>
     <td class="Header" align="center" width="50">Day</td>
     <td class="Header" align="center" width="90">Date</td>
     <td class="Header" align="center" width="100">In</td>
     <td class="Header" align="center" width="100">Out</td>
     <td class="Header" align="center" width="100">In</td>
     <td class="Header" align="center" width="100">Out</td>
     <td class="Header" align="center" width="100">In</td>
     <td class="Header" align="center" width="100">Out</td>
     <td class="Header" align="center" width="100">In</td>
     <td class="Header" align="center" width="100">Out</td>
     <td class="Header" align="center" width="80">Daily</td>
     <td class="Header" align="center" width="80">Totals</td>
    </tr>
	<tr>
     <td class="Header" align="left" width="50">Sun</td>
     <td class="data" align="left" width="90"><Label class=control><%=dtSun%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSunI1)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSunO1)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSunI2)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSunO2)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSunI3)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSunO3)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSunI4)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSunO4)%></label></td>
     <td class="data" align="left" width="80"><Label class=control><%=FormatNumber(strSunDA,2)%></label></td>
     <td class="data" align="left" width="80"><Label class=control><%=FormatNumber(strSunTO,2)%></label></td>
    </tr>
	<tr>
     <td class="Header" align="left" width="50">Mon</td>
     <td class="data" align="left" width="90"><Label class=control><%=dtMon%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strMonI1)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strMonO1)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strMonI2)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strMonO2)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strMonI3)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strMonO3)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strMonI4)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strMonO4)%></label></td>
     <td class="data" align="left" width="80"><Label class=control><%=FormatNumber(strMonDA,2)%></label></td>
     <td class="data" align="left" width="80"><Label class=control><%=FormatNumber(strMonTO,2)%></label></td>
   </tr>
	<tr>
     <td class="Header" align="left" width="50">Tue</td>
     <td class="data" align="left" width="50"><Label class=control><%=dtTue%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strTueI1)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strTueO1)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strTueI2)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strTueO2)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strTueI3)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strTueO3)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strTueI4)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strTueO4)%></label></td>
     <td class="data" align="left" width="80"><Label class=control><%=FormatNumber(strTueDA,2)%></label></td>
     <td class="data" align="left" width="80"><Label class=control><%=FormatNumber(strTueTO,2)%></label></td>
    </tr>
	<tr>
     <td class="Header" align="left" width="50">Wed</td>
     <td class="data" align="left" width="90"><Label class=control><%=dtWed%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strWedI1)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strWedO1)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strWedI2)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strWedO2)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strWedI3)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strWedO3)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strWedI4)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strWedO4)%></label></td>
     <td class="data" align="left" width="80"><Label class=control><%=FormatNumber(strWedDA,2)%></label></td>
     <td class="data" align="left" width="80"><Label class=control><%=FormatNumber(strWedTO,2)%></label></td>
  </tr>
	<tr>
     <td class="Header" align="left" width="50">Thu</td>
     <td class="data" align="left" width="90"><Label class=control><%=dtThu%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strThuI1)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strThuO1)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strThuI2)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strThuO2)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strThuI3)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strThuO3)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strThuI4)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strThuO4)%></label></td>
     <td class="data" align="left" width="80"><Label class=control><%=FormatNumber(strThuDA,2)%></label></td>
     <td class="data" align="left" width="80"><Label class=control><%=FormatNumber(strThuTO,2)%></label></td>
   </tr>
	<tr>
     <td class="Header" align="left" width="50">Fri</td>
     <td class="data" align="left" width="90"><Label class=control><%=dtFri%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strFriI1)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strFriO1)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strFriI2)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strFriO2)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strFriI3)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strFriO3)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strFriI4)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strFriO4)%></label></td>
     <td class="data" align="left" width="80"><Label class=control><%=FormatNumber(strFriDA,2)%></label></td>
     <td class="data" align="left" width="80"><Label class=control><%=FormatNumber(strFriTO,2)%></label></td>
    </tr>
	<tr>
     <td class="Header" align="left" width="50">Sat</td>
     <td class="data" align="left" width="90"><Label class=control><%=dtSat%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSatI1)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSatO1)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSatI2)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSatO2)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSatI3)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSatO3)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSatI4)%></label></td>
     <td class="data" align="right" width="100"><Label class=control><%=DispTime(strSatO4)%></label></td>
     <td class="data" align="left" width="80"><Label class=control><%=FormatNumber(strSatDA,2)%></label></td>
     <td class="data" align="left" width="80"><Label class=control><%=FormatNumber(strSatTO,2)%></label></td>
    </tr>

	<tr>
     <td colspan=12 class="Header" align="center" width="700">&nbsp;</td>
    </tr>
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
End Sub

</script>

<%
Call CloseConnection(dbMain)
End Sub

'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
	Dim htmlDataRow,strDesc,strDescTitle,strRequester,strRequesterTitle,LocationID
	Dim db,strSQL,rs,strLoc,intListCode,rowColor,strFilter,strqaDate,strTitle
	Set db = OpenConnection

	strFilter = Request("hdnFilterBy")
	LocationID = Request("LocationID")

               
	strSQL =" SELECT Veh.vehid,Veh.tag,Veh.VehNum,Veh.Make, "&_
	" Veh.Model,Veh.vYear,"&_ 
    " Client.Fname+' '+Client.Lname AS ClinetName"&_
	" FROM Veh(Nolock) "&_ 
	" INNER JOIN Client(Nolock) ON Veh.ClientID = Client.ClientID"&_
	" WHERE Client.LocationID="& LocationID 

	If Len(strFilter) > 0 Then
		strSQL = strSQL & strFilter
	ELSE
		strSQL = strSQL & " order by Veh.ClientID,Veh.VehNum"
	End if

    IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF

			'If Len(rs("VehDesc")) > 35 Then
			'	strDesc = Left(rs("VehDesc"), 32) & "..."
			'Else
			'	strDesc = NullTest(rs("VehDesc"))
			'End If
			'strDescTitle = NullTest(rs("VehDesc"))

			''If Len(rs("RequestBy")) > 25 Then
			'	strRequester = Left(rs("RequestBy"), 22) & "..."
			'Else
			'	strRequester = NullTest(rs("RequestBy"))
			'End If
			'strRequesterTitle = NullTest(rs("RequestBy"))

				strTitle = rs("ClinetName")
				rowColor = "data"
				'If rs("Status") = 25 and datediff("d", rs("FFAC_Date"),date()) > 4 then
				'	rowColor = "StatusDarkOrange"
				'	strTitle = rs("strStatus")& " over 4 days From Finance"
				'END IF
				'If rs("Status") = 50 and datediff("d", rs("BPRE_Date"),date()) > 30 then
				'	rowColor = "StatusRed"
				'	strTitle = rs("strStatus")& " over 30 days DOP"
				'END IF
				'IF rs("qaDate")="1/1/1900" then
				'	strqaDate = ""
				'ELSE
				'	strqaDate = rs("qaDate")
				'END IF
	
				htmlDataRow = htmlDataRow & "<tr><td align=right Title=""" & strTitle & """ class="& rowColor &">" & NullTest(VehNum) & "</a></td>" 
				htmlDataRow = htmlDataRow & "<td Title=""" & strTitle & """ class="& rowColor &"><a target=body href=""VehEdit.asp?hdnvehid=" & rs("vehid") &"&hdnFilterBy="& Request("hdnFilterBy")&  """>" &  NullTest(rs("ClinetName")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td Title=""" & strDescTitle & """  class="& rowColor &">" &  NullTest(rs("tag")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td Title=""" & strRequesterTitle & """  class="& rowColor &">" &  NullTest(rs("Make")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=left Title=""" & strTitle & """  class="& rowColor &">" & NullTest(rs("Model")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=center Title=""" & strTitle & """  class="& rowColor &">" & NullTest(rs("year")) & "</td></tr>"

				rs.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=6 align=""center"" Class=""data"">No records were found.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=6 align=""center"" Class=""data"">No records were found.</td></tr>"
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
Function TimeDiff(Etime,Stime)
	dim cehrs,cshrs
	If len(Etime)=0 or len(stime)=0  then
		TimeDiff = 0.00
	Else
		cehrs = left(Etime,2) + 1-abs((((int(right(Etime,2))-60)/60)))
		cshrs = left(Stime,2) + 1-abs((((int((right(stime,2)))-60)/60)))
		TimeDiff = round(cehrs-cshrs,2)
	End If
End Function

Function DispTime(vtime)
	Dim ntime
	IF len(vtime) > 0 then
		If int(left(vtime,2)) > 12 then
			ntime = right("00"+int(left(vtime,2))-12,2)+":"+right("00"+right(vtime,2),2)+" PM"
		ELSE
			ntime = vtime+" AM"
		END IF
		DispTime = ntime
	else
		DispTime = "&nbsp;"
	END IF
End Function


%>
 

