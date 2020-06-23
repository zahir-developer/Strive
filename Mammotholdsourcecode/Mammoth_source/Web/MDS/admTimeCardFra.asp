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
Dim dbMain,hdnUserID,strSQL, RS,cnt,hdnSheetID,intPaid,strcardname,strSalery,LocationID,LoginID
Set dbMain =  OpenConnection



Dim dtSun,dtMon,dtTue,dtWed,dtThu,dtFri,dtSat,strFDW,dtFDW,dtPayDate
Dim strSunIDI1,strSunIDO1,strSunIDI2,strSunIDO2,strSunIDI3,strSunIDO3,strSunIDI4,strSunIDO4,strSunIDDA,strSunIDTo
Dim strMonIDI1,strMonIDO1,strMonIDI2,strMonIDO2,strMonIDI3,strMonIDO3,strMonIDI4,strMonIDO4,strMonIDDA,strMonIDTo
Dim strTueIDI1,strTueIDO1,strTueIDI2,strTueIDO2,strTueIDI3,strTueIDO3,strTueIDI4,strTueIDO4,strTueIDDA,strTueIDTo
Dim strWedIDI1,strWedIDO1,strWedIDI2,strWedIDO2,strWedIDI3,strWedIDO3,strWedIDI4,strWedIDO4,strWedIDDA,strWedIDTo
Dim strThuIDI1,strThuIDO1,strThuIDI2,strThuIDO2,strThuIDI3,strThuIDO3,strThuIDI4,strThuIDO4,strThuIDDA,strThuIDTo
Dim strFriIDI1,strFriIDO1,strFriIDI2,strFriIDO2,strFriIDI3,strFriIDO3,strFriIDI4,strFriIDO4,strFriIDDA,strFriIDTo
Dim strSatIDI1,strSatIDO1,strSatIDI2,strSatIDO2,strSatIDI3,strSatIDO3,strSatIDI4,strSatIDO4,strSatIDDA,strSatIDTo

Dim strSunI1,strSunO1,strSunI2,strSunO2,strSunI3,strSunO3,strSunI4,strSunO4,strSunDA,strSunTo,strSunWt,strSunPT
Dim strMonI1,strMonO1,strMonI2,strMonO2,strMonI3,strMonO3,strMonI4,strMonO4,strMonDA,strMonTo,strMonWt,strMonPT
Dim strTueI1,strTueO1,strTueI2,strTueO2,strTueI3,strTueO3,strTueI4,strTueO4,strTueDA,strTueTo,strTueWt,strTuePT
Dim strWedI1,strWedO1,strWedI2,strWedO2,strWedI3,strWedO3,strWedI4,strWedO4,strWedDA,strWedTo,strWedWt,strWedPT
Dim strThuI1,strThuO1,strThuI2,strThuO2,strThuI3,strThuO3,strThuI4,strThuO4,strThuDA,strThuTo,strThuWt,strThuPT
Dim strFriI1,strFriO1,strFriI2,strFriO2,strFriI3,strFriO3,strFriI4,strFriO4,strFriDA,strFriTo,strFriWt,strFriPT
Dim strSatI1,strSatO1,strSatI2,strSatO2,strSatI3,strSatO3,strSatI4,strSatO4,strSatDA,strSatTo,strSatWt,strSatPT

Dim strSunIT1,strSunOT1,strSunIT2,strSunOT2,strSunIT3,strSunOT3,strSunIT4,strSunOT4
Dim strMonIT1,strMonOT1,strMonIT2,strMonOT2,strMonIT3,strMonOT3,strMonIT4,strMonOT4
Dim strTueIT1,strTueOT1,strTueIT2,strTueOT2,strTueIT3,strTueOT3,strTueIT4,strTueOT4
Dim strWedIT1,strWedOT1,strWedIT2,strWedOT2,strWedIT3,strWedOT3,strWedIT4,strWedOT4
Dim strThuIT1,strThuOT1,strThuIT2,strThuOT2,strThuIT3,strThuOT3,strThuIT4,strThuOT4
Dim strFriIT1,strFriOT1,strFriIT2,strFriOT2,strFriIT3,strFriOT3,strFriIT4,strFriOT4
Dim strSatIT1,strSatOT1,strSatIT2,strSatOT2,strSatIT3,strSatOT3,strSatIT4,strSatOT4

hdnSheetID = Request("hdnSheetID")
hdnUserID = Request("hdnUserID")
LocationID = request("LocationID")
LoginID = request("LoginID")

IF Request("formaction")="SaveData" then
	Call SaveData(dbMain)
END IF

strSQL =" SELECT FirstName + ' ' + LastName AS cardname,salery  FROM LM_Users WHERE LM_Users.userid ="& hdnUserID  &" and LocationID=" & LocationID
IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
	IF NOT rs.EOF then
		strcardname = rs("cardname")
		IF isnull(rs("salery")) then
		strSalery = 0.00
		ELSE
		strSalery = rs("salery")
		END IF
	END IF
END IF
strSQL =" SELECT weekof FROM TimeSheet WHERE TimeSheet.SheetID ="& hdnSheetID  &" and LocationID=" & LocationID
IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
	IF NOT rs.EOF then
		dtFDW = rs(0)	
	END IF
END IF
dtSun = dtFDW

cnt=1
strSQL = "SELECT ClockID,Cdatetime,Ctype,Paid FROM timeClock WHERE userid=" & hdnUserID  &" and LocationID=" & LocationID &_
		" and DatePart(Month,CDatetime) = '"& Month(dtSun) &"'"&_
		" and DatePart(Day,CDatetime) = '"& Day(dtSun) &"'"&_
		" and DatePart(Year,CDatetime) = '"& Year(dtSun) &"'"&_
		" Order by CDatetime asc"
    IF dbOpenStaticRecordset(dbMain,rs,strSQL) then   
	IF NOT 	rs.EOF then
		intPaid = rs("Paid")
		Do while Not rs.EOF
			Select Case cnt
				Case 1
					strSunIT1 = rs("Ctype")
					strSunIDI1 = rs("ClockID")
					strSunI1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 2
					strSunOT1 = rs("Ctype")
					strSunIDO1 = rs("ClockID")
					strSunO1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 3
					strSunIT2 = rs("Ctype")
					strSunIDI2 = rs("ClockID")
					strSunI2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 4
					strSunOT2 = rs("Ctype")
					strSunIDO2 = rs("ClockID")
					strSunO2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 5
					strSunIT3 = rs("Ctype")
					strSunIDI3 = rs("ClockID")
					strSunI3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 6
					strSunOT3 = rs("Ctype")
					strSunIDO3 = rs("ClockID")
					strSunO3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 7
					strSunIT4 = rs("Ctype")
					strSunIDI4 = rs("ClockID")
					strSunI4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 8
					strSunOT4 = rs("Ctype")
					strSunIDO4 = rs("ClockID")
					strSunO4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
			End Select
			rs.MoveNext
			cnt = cnt+1
		Loop
	END IF
End If
strSunWt = IIF(strSunIT1<>9,timediff(strSunO1,strSunI1),0)+  IIF(strSunIT2<>9,timediff(strSunO2,strSunI2),0)+IIF(strSunIT3<>9,timediff(strSunO3,strSunI3),0)+IIF(strSunIT4<>9,timediff(strSunO4,strSunI4),0)
strSunPT = round(strSunWt,2)
strSunDA = timediff(strSunO1,strSunI1)+timediff(strSunO2,strSunI2)+timediff(strSunO3,strSunI3)+timediff(strSunO4,strSunI4)
strSunTo = round(strSunDA,2)
dtMon = dtFDW+1
cnt=1
strSQL = "SELECT ClockID,Cdatetime,Ctype,Paid FROM timeClock WHERE userid=" & hdnUserID  &" and LocationID=" & LocationID &_
		" and DatePart(Month,CDatetime) = '"& Month(dtMon) &"'"&_
		" and DatePart(Day,CDatetime) = '"& Day(dtMon) &"'"&_
		" and DatePart(Year,CDatetime) = '"& Year(dtMon) &"'"&_
		" Order by CDatetime asc"
    IF dbOpenStaticRecordset(dbMain,rs,strSQL) then   
	IF NOT 	rs.EOF then
		intPaid = rs("Paid")
		Do while Not rs.EOF
			Select Case cnt
				Case 1
					strMonIT1 = rs("Ctype")
					strMonIDI1 = rs("ClockID")
					strMonI1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 2
					strMonOT1 = rs("Ctype")
					strMonIDO1 = rs("ClockID")
					strMonO1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 3
					strMonIT2 = rs("Ctype")
					strMonIDI2 = rs("ClockID")
					strMonI2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 4
					strMonOT2 = rs("Ctype")
					strMonIDO2 = rs("ClockID")
					strMonO2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 5
					strMonIT3 = rs("Ctype")
					strMonIDI3 = rs("ClockID")
					strMonI3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 6
					strMonOT3 = rs("Ctype")
					strMonIDO3 = rs("ClockID")
					strMonO3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 7
					strMonIT4 = rs("Ctype")
					strMonIDI4 = rs("ClockID")
					strMonI4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 8
					strMonOT4 = rs("Ctype")
					strMonIDO4 = rs("ClockID")
					strMonO4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
			End Select
			rs.MoveNext
			cnt = cnt+1
		Loop
	END IF
End If
strMonWt = IIF(strMonIT1<>9,timediff(strMonO1,strMonI1),0)+  IIF(strMonIT2<>9,timediff(strMonO2,strMonI2),0)+IIF(strMonIT3<>9,timediff(strMonO3,strMonI3),0)+IIF(strMonIT4<>9,timediff(strMonO4,strMonI4),0)
strMonPT = round(strMonWt,2)+strSunPT
strMonDA = timediff(strMonO1,strMonI1)+timediff(strMonO2,strMonI2)+timediff(strMonO3,strMonI3)+timediff(strMonO4,strMonI4)
strMonTo = round(strMonDA,2)+strSunTo
dtTue = dtFDW+2
cnt=1
strSQL = "SELECT ClockID,Cdatetime,Ctype,Paid FROM timeClock WHERE userid=" & hdnUserID &" and LocationID=" & LocationID &_
		" and DatePart(Month,CDatetime) = '"& Month(dtTue) &"'"&_
		" and DatePart(Day,CDatetime) = '"& Day(dtTue) &"'"&_
		" and DatePart(Year,CDatetime) = '"& Year(dtTue) &"'"&_
		" Order by CDatetime asc"
    IF dbOpenStaticRecordset(dbMain,rs,strSQL) then   
	IF NOT 	rs.EOF then
		intPaid = rs("Paid")
		Do while Not rs.EOF
			Select Case cnt
				Case 1
					strTueIT1 = rs("Ctype")
					strTueIDI1 = rs("ClockID")
					strTueI1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 2
					strTueOT1 = rs("Ctype")
					strTueIDO1 = rs("ClockID")
					strTueO1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 3
					strTueIT2 = rs("Ctype")
					strTueIDI2 = rs("ClockID")
					strTueI2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 4
					strTueOT2 = rs("Ctype")
					strTueIDO2 = rs("ClockID")
					strTueO2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 5
					strTueIT3 = rs("Ctype")
					strTueIDI3 = rs("ClockID")
					strTueI3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 6
					strTueOT3 = rs("Ctype")
					strTueIDO3 = rs("ClockID")
					strTueO3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 7
					strTueIT4 = rs("Ctype")
					strTueIDI4 = rs("ClockID")
					strTueI4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 8
					strTueOT4 = rs("Ctype")
					strTueIDO4 = rs("ClockID")
					strTueO4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
			End Select
			rs.MoveNext
			cnt = cnt+1
		Loop
	END IF
End If
strTueWt = IIF(strTueIT1<>9,timediff(strTueO1,strTueI1),0)+  IIF(strTueIT2<>9,timediff(strTueO2,strTueI2),0)+IIF(strTueIT3<>9,timediff(strTueO3,strTueI3),0)+IIF(strTueIT4<>9,timediff(strTueO4,strTueI4),0)
strTuePT = round(strTueWt,2)+strMonPT
strTueDA = timediff(strTueO1,strTueI1)+timediff(strTueO2,strTueI2)+timediff(strTueO3,strTueI3)+timediff(strTueO4,strTueI4)
strTueTo = round(strTueDA,2)+strMonTo
dtWed = dtFDW+3
cnt=1
strSQL = "SELECT ClockID,Cdatetime,Ctype,Paid FROM timeClock WHERE userid=" & hdnUserID &" and LocationID=" & LocationID &_
		" and DatePart(Month,CDatetime) = '"& Month(dtWed) &"'"&_
		" and DatePart(Day,CDatetime) = '"& Day(dtWed) &"'"&_
		" and DatePart(Year,CDatetime) = '"& Year(dtWed) &"'"&_
		" Order by CDatetime asc"
    IF dbOpenStaticRecordset(dbMain,rs,strSQL) then   
	IF NOT 	rs.EOF then
		intPaid = rs("Paid")
		Do while Not rs.EOF
			Select Case cnt
				Case 1
					strWedIT1 = rs("Ctype")
					strWedIDI1 = rs("ClockID")
					strWedI1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 2
					strWedOT1 = rs("Ctype")
					strWedIDO1 = rs("ClockID")
					strWedO1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 3
					strWedIT2 = rs("Ctype")
					strWedIDI2 = rs("ClockID")
					strWedI2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 4
					strWedOT2 = rs("Ctype")
					strWedIDO2 = rs("ClockID")
					strWedO2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 5
					strWedIT3 = rs("Ctype")
					strWedIDI3 = rs("ClockID")
					strWedI3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 6
					strWedOT3 = rs("Ctype")
					strWedIDO3 = rs("ClockID")
					strWedO3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 7
					strWedIT4 = rs("Ctype")
					strWedIDI4 = rs("ClockID")
					strWedI4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 8
					strWedOT4 = rs("Ctype")
					strWedIDO4 = rs("ClockID")
					strWedO4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
			End Select
			rs.MoveNext
			cnt = cnt+1
		Loop
	END IF
End If
strWedWt = IIF(strWedIT1<>9,timediff(strWedO1,strWedI1),0)+  IIF(strWedIT2<>9,timediff(strWedO2,strWedI2),0)+IIF(strWedIT3<>9,timediff(strWedO3,strWedI3),0)+IIF(strWedIT4<>9,timediff(strWedO4,strWedI4),0)
strWedPT = round(strWedWt,2)+strTuePT
strWedDA = timediff(strWedO1,strWedI1)+timediff(strWedO2,strWedI2)+timediff(strWedO3,strWedI3)+timediff(strWedO4,strWedI4)
strWedTo = round(strWedDA,2)+strTueTo
dtThu = dtFDW+4
cnt=1
strSQL = "SELECT ClockID,Cdatetime,Ctype,Paid FROM timeClock WHERE userid=" & hdnUserID &" and LocationID=" & LocationID &_
		" and DatePart(Month,CDatetime) = '"& Month(dtThu) &"'"&_
		" and DatePart(Day,CDatetime) = '"& Day(dtThu) &"'"&_
		" and DatePart(Year,CDatetime) = '"& Year(dtThu) &"'"&_
		" Order by CDatetime asc"
    IF dbOpenStaticRecordset(dbMain,rs,strSQL) then   
	IF NOT 	rs.EOF then
		intPaid = rs("Paid")
		Do while Not rs.EOF
			Select Case cnt
				Case 1
					strThuIT1 = rs("Ctype")
					strThuIDI1 = rs("ClockID")
					strThuI1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 2
					strThuOT1 = rs("Ctype")
					strThuIDO1 = rs("ClockID")
					strThuO1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 3
					strThuIT2 = rs("Ctype")
					strThuIDI2 = rs("ClockID")
					strThuI2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 4
					strThuOT2 = rs("Ctype")
					strThuIDO2 = rs("ClockID")
					strThuO2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 5
					strThuIT3 = rs("Ctype")
					strThuIDI3 = rs("ClockID")
					strThuI3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 6
					strThuOT3 = rs("Ctype")
					strThuIDO3 = rs("ClockID")
					strThuO3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 7
					strThuIT4 = rs("Ctype")
					strThuIDI4 = rs("ClockID")
					strThuI4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 8
					strThuOT4 = rs("Ctype")
					strThuIDO4 = rs("ClockID")
					strThuO4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
			End Select
			rs.MoveNext
			cnt = cnt+1
		Loop
	END IF
End If
strThuWt = IIF(strThuIT1<>9,timediff(strThuO1,strThuI1),0)+  IIF(strThuIT2<>9,timediff(strThuO2,strThuI2),0)+IIF(strThuIT3<>9,timediff(strThuO3,strThuI3),0)+IIF(strThuIT4<>9,timediff(strThuO4,strThuI4),0)
strThuPT = round(strThuWt,2)+strWedPT
strThuDA = timediff(strThuO1,strThuI1)+timediff(strThuO2,strThuI2)+timediff(strThuO3,strThuI3)+timediff(strThuO4,strThuI4)
strThuTo = round(strThuDA,2)+strWedTo
dtFri = dtFDW+5
cnt=1
strSQL = "SELECT ClockID,Cdatetime,Ctype,Paid FROM timeClock WHERE userid=" & hdnUserID &" and LocationID=" & LocationID &_
		" and DatePart(Month,CDatetime) = '"& Month(dtFri) &"'"&_
		" and DatePart(Day,CDatetime) = '"& Day(dtFri) &"'"&_
		" and DatePart(Year,CDatetime) = '"& Year(dtFri) &"'"&_
		" Order by CDatetime asc"
    IF dbOpenStaticRecordset(dbMain,rs,strSQL) then   
	IF NOT 	rs.EOF then
		intPaid = rs("Paid")
		Do while Not rs.EOF
			Select Case cnt
				Case 1
					strFriIT1 = rs("Ctype")
					strFriIDI1 = rs("ClockID")
					strFriI1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 2
					strFriOT1 = rs("Ctype")
					strFriIDO1 = rs("ClockID")
					strFriO1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 3
					strFriIT2 = rs("Ctype")
					strFriIDI2 = rs("ClockID")
					strFriI2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 4
					strFriOT2 = rs("Ctype")
					strFriIDO2 = rs("ClockID")
					strFriO2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 5
					strFriIT3 = rs("Ctype")
					strFriIDI3 = rs("ClockID")
					strFriI3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 6
					strFriOT3 = rs("Ctype")
					strFriIDO3 = rs("ClockID")
					strFriO3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 7
					strFriIT4 = rs("Ctype")
					strFriIDI4 = rs("ClockID")
					strFriI4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 8
					strFriOT4 = rs("Ctype")
					strFriIDO4 = rs("ClockID")
					strFriO4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
			End Select
			rs.MoveNext
			cnt = cnt+1
		Loop
	END IF
End If
strFriWt = IIF(strFriIT1<>9,timediff(strFriO1,strFriI1),0)+  IIF(strFriIT2<>9,timediff(strFriO2,strFriI2),0)+IIF(strFriIT3<>9,timediff(strFriO3,strFriI3),0)+IIF(strFriIT4<>9,timediff(strFriO4,strFriI4),0)
strFriPT = round(strFriWt,2)+strThuPT
strFriDA = timediff(strFriO1,strFriI1)+timediff(strFriO2,strFriI2)+timediff(strFriO3,strFriI3)+timediff(strFriO4,strFriI4)
strFriTo = round(strFriDA,2)+strThuTo
dtSat = dtFDW+6
cnt=1
strSQL = "SELECT ClockID,Cdatetime,Ctype,Paid FROM timeClock WHERE userid=" & hdnUserID &" and LocationID=" & LocationID &_
		" and DatePart(Month,CDatetime) = '"& Month(dtSat) &"'"&_
		" and DatePart(Day,CDatetime) = '"& Day(dtSat) &"'"&_
		" and DatePart(Year,CDatetime) = '"& Year(dtSat) &"'"&_
		" Order by CDatetime asc"
'response.Write strsql
'response.End
IF dbOpenStaticRecordset(dbMain,rs,strSQL) then   
	IF NOT 	rs.EOF then
		intPaid = rs("Paid")
		Do while Not rs.EOF
			Select Case cnt
				Case 1
					strSatIT1 = rs("Ctype")
					strSatIDI1 = rs("ClockID")
					strSatI1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 2
					strSatOT1 = rs("Ctype")
					strSatIDO1 = rs("ClockID")
					strSatO1 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 3
					strSatIT2 = rs("Ctype")
					strSatIDI2 = rs("ClockID")
					strSatI2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 4
					strSatOT2 = rs("Ctype")
					strSatIDO2 = rs("ClockID")
					strSatO2 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 5
					strSatIT3 = rs("Ctype")
					strSatIDI3 = rs("ClockID")
					strSatI3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 6
					strSatOT3 = rs("Ctype")
					strSatIDO3 = rs("ClockID")
					strSatO3 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 7
					strSatIT4 = rs("Ctype")
					strSatIDI4 = rs("ClockID")
					strSatI4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
				Case 8
					strSatOT4 = rs("Ctype")
					strSatIDO4 = rs("ClockID")
					strSatO4 = FormatDateTime(rs("Cdatetime"),vbshortTime)
			End Select
			rs.MoveNext
			cnt = cnt+1
		Loop
	END IF
End If
strSatWt = IIF(strSatIT1<>9,timediff(strSatO1,strSatI1),0)+  IIF(strSatIT2<>9,timediff(strSatO2,strSatI2),0)+IIF(strSatIT3<>9,timediff(strSatO3,strSatI3),0)+IIF(strSatIT4<>9,timediff(strSatO4,strSatI4),0)
strSatPT = round(strSatWt,2)+strFriPT
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
<style type="text/css"><!--
select {font-family: Courier, monospace;}
--></Style>
<title></title>
</head>
<body class=pgbody>
<form method="POST" name="admTimeCardFra" action="admTimeCardFra.asp?hdnSheetID=<%=Request("hdnSheetID")%>&hdnUserID=<%=Request("hdnUserID")%>&LocationID=<%=Request("LocationID")%>&LoginID=<%=Request("LoginID")%>" >
<input type=hidden name=intPaid value="<%=intPaid%>">
<input type=hidden name=hdnUserID value="<%=hdnUserID%>">
<input type="hidden" name="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" ID="LoginID" value="<%=LoginID%>" />
<input type=hidden name=strcardname value="<%=strcardname%>">
<input type=hidden name=FormAction value=>
<% IF isnull(hdnUserID) or len(hdnUserID)=0 then %>
<table  style="width: 100%; border-collapse: collapse;" class="Data">
	<tr>
     <td colspan=12 style="text-align:center;" Class="data">No records Selected.</td>
    </tr>
</table>
<% Else %>
<input type=hidden name=strSunIDI1 value="<%=strSunIDI1%>">
<input type=hidden name=strSunIDO1 value="<%=strSunIDO1%>">
<input type=hidden name=strSunIDI2 value="<%=strSunIDI2%>">
<input type=hidden name=strSunIDO2 value="<%=strSunIDO2%>">
<input type=hidden name=strSunIDI3 value="<%=strSunIDI3%>">
<input type=hidden name=strSunIDO3 value="<%=strSunIDO3%>">
<input type=hidden name=strSunIDI4 value="<%=strSunIDI4%>">
<input type=hidden name=strSunIDO4 value="<%=strSunIDO4%>">
<input type=hidden name=strMonIDI1 value="<%=strMonIDI1%>">
<input type=hidden name=strMonIDO1 value="<%=strMonIDO1%>">
<input type=hidden name=strMonIDI2 value="<%=strMonIDI2%>">
<input type=hidden name=strMonIDO2 value="<%=strMonIDO2%>">
<input type=hidden name=strMonIDI3 value="<%=strMonIDI3%>">
<input type=hidden name=strMonIDO3 value="<%=strMonIDO3%>">
<input type=hidden name=strMonIDI4 value="<%=strMonIDI4%>">
<input type=hidden name=strMonIDO4 value="<%=strMonIDO4%>">
<input type=hidden name=strTueIDI1 value="<%=strTueIDI1%>">
<input type=hidden name=strTueIDO1 value="<%=strTueIDO1%>">
<input type=hidden name=strTueIDI2 value="<%=strTueIDI2%>">
<input type=hidden name=strTueIDO2 value="<%=strTueIDO2%>">
<input type=hidden name=strTueIDI3 value="<%=strTueIDI3%>">
<input type=hidden name=strTueIDO3 value="<%=strTueIDO3%>">
<input type=hidden name=strTueIDI4 value="<%=strTueIDI4%>">
<input type=hidden name=strTueIDO4 value="<%=strTueIDO4%>">
<input type=hidden name=strWedIDI1 value="<%=strWedIDI1%>">
<input type=hidden name=strWedIDO1 value="<%=strWedIDO1%>">
<input type=hidden name=strWedIDI2 value="<%=strWedIDI2%>">
<input type=hidden name=strWedIDO2 value="<%=strWedIDO2%>">
<input type=hidden name=strWedIDI3 value="<%=strWedIDI3%>">
<input type=hidden name=strWedIDO3 value="<%=strWedIDO3%>">
<input type=hidden name=strWedIDI4 value="<%=strWedIDI4%>">
<input type=hidden name=strWedIDO4 value="<%=strWedIDO4%>">
<input type=hidden name=strThuIDI1 value="<%=strThuIDI1%>">
<input type=hidden name=strThuIDO1 value="<%=strThuIDO1%>">
<input type=hidden name=strThuIDI2 value="<%=strThuIDI2%>">
<input type=hidden name=strThuIDO2 value="<%=strThuIDO2%>">
<input type=hidden name=strThuIDI3 value="<%=strThuIDI3%>">
<input type=hidden name=strThuIDO3 value="<%=strThuIDO3%>">
<input type=hidden name=strThuIDI4 value="<%=strThuIDI4%>">
<input type=hidden name=strThuIDO4 value="<%=strThuIDO4%>">
<input type=hidden name=strFriIDI1 value="<%=strFriIDI1%>">
<input type=hidden name=strFriIDO1 value="<%=strFriIDO1%>">
<input type=hidden name=strFriIDI2 value="<%=strFriIDI2%>">
<input type=hidden name=strFriIDO2 value="<%=strFriIDO2%>">
<input type=hidden name=strFriIDI3 value="<%=strFriIDI3%>">
<input type=hidden name=strFriIDO3 value="<%=strFriIDO3%>">
<input type=hidden name=strFriIDI4 value="<%=strFriIDI4%>">
<input type=hidden name=strFriIDO4 value="<%=strFriIDO4%>">
<input type=hidden name=strSatIDI1 value="<%=strSatIDI1%>">
<input type=hidden name=strSatIDO1 value="<%=strSatIDO1%>">
<input type=hidden name=strSatIDI2 value="<%=strSatIDI2%>">
<input type=hidden name=strSatIDO2 value="<%=strSatIDO2%>">
<input type=hidden name=strSatIDI3 value="<%=strSatIDI3%>">
<input type=hidden name=strSatIDO3 value="<%=strSatIDO3%>">
<input type=hidden name=strSatIDI4 value="<%=strSatIDI4%>">
<input type=hidden name=strSatIDO4 value="<%=strSatIDO4%>">

<input type=hidden name=strSunWt value="<%=strSunWt%>">
<input type=hidden name=strSunPt value="<%=strSunPt%>">
<input type=hidden name=strMonWt value="<%=strMonWt%>">
<input type=hidden name=strMonPt value="<%=strMonPt%>">
<input type=hidden name=strTueWt value="<%=strTueWt%>">
<input type=hidden name=strTuePt value="<%=strTuePt%>">
<input type=hidden name=strWedWt value="<%=strWedWt%>">
<input type=hidden name=strWedPt value="<%=strWedPt%>">
<input type=hidden name=strThuWt value="<%=strThuWt%>">
<input type=hidden name=strThuPt value="<%=strThuPt%>">
<input type=hidden name=strFriWt value="<%=strFriWt%>">
<input type=hidden name=strFriPt value="<%=strFriPt%>">
<input type=hidden name=strSatWt value="<%=strSatWt%>">
<input type=hidden name=strSatPt value="<%=strSatPt%>">

<input type=hidden name=strSalery value="<%=strSalery%>">
<input type=hidden name=strSatTO value="<%=strSatTO%>">
<table style="width: 100%; border-collapse: collapse;">
	<tr>
	<td style="text-align:right; white-space:nowrap" class="control">Use 24:00 Time enter 0 to delete</td>
	<td style="text-align:right; white-space:nowrap" class="control"><b><%=strcardname%></b></td>
	<td align=right>			
			<% IF intPaid = 0 then %>
			<button  name="btnSave" style="width:100px; text-align:center">Save Card</button>
			<% ELSE %>
			<button  name="btnSave" class="buttondead" style="width:100px; text-align:center">Save Card</button>
			<% END IF %>
		</td>
	</tr>
</table>
<table style="width: 100%; border-collapse: collapse;" class="Data">
	<tr>
     <td class="Header" style="text-align:center; width:30px;">Day</td>
     <td class="Header" style="text-align:center; width:70px">Date</td>
     <td class="Header" style="text-align:center; width:125px">In</td>
     <td class="Header" style="text-align:center; width:125px">Out</td>
     <td class="Header" style="text-align:center; width:125px">In</td>
     <td class="Header" style="text-align:center; width:125px">Out</td>
     <td class="Header" style="text-align:center; width:125px">In</td>
     <td class="Header" style="text-align:center; width:125px">Out</td>
     <td class="Header" style="text-align:center; width:125px">In</td>
     <td class="Header" style="text-align:center; width:125px">Out</td>
     <td class="Header" style="text-align:center; width:40px">Daily</td>
     <td class="Header" style="text-align:center; width:40px">Totals</td>
     <td class="Header" style="text-align:center; width:40px">Paid</td>
    </tr>
	<tr style="vertical-align:middle">
     <td class="Header" style="text-align:left; width:30px">Sun</td>
     <td class="data" style="text-align:left; width:70px"><input type=hidden name=dtSun value="<%=dtSun%>"><Label class=control><%=dtSun%></label></td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSunI1" value="<%=strSunI1%>">
		<select name="strSunIT1">
			<% Call LoadTimeType(dbMain,strSunIT1) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSunO1" value="<%=strSunO1%>">
		<select name="strSunOT1">
			<% Call LoadTimeType(dbMain,strSunOT1) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSunI2" value="<%=strSunI2%>">
		<select name="strSunIT2">
			<% Call LoadTimeType(dbMain,strSunIT2) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSunO2" value="<%=strSunO2%>">
		<select name="strSunOT2">
			<% Call LoadTimeType(dbMain,strSunOT2) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSunI3" value="<%=strSunI3%>">
		<select name="strSunIT3">
			<% Call LoadTimeType(dbMain,strSunIT3) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSunO3" value="<%=strSunO3%>">
		<select name="strSunOT3">
			<% Call LoadTimeType(dbMain,strSunOT3) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSunI4" value="<%=strSunI4%>">
		<select name="strSunIT4">
			<% Call LoadTimeType(dbMain,strSunIT4) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSunO4" value="<%=strSunO4%>">
		<select name="strSunOT4">
			<% Call LoadTimeType(dbMain,strSunOT4) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strSunDA,2)%></label></td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strSunTO,2)%></label></td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strSunPT,2)%></label></td>
    </tr>
	<tr style="vertical-align:middle">
     <td class="Header" style="text-align:left; width:30px">Mon</td>
     <td class="data" style="text-align:left; width:70px"><input type=hidden name=dtMon value="<%=dtMon%>"><Label class=control><%=dtMon%></label></td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strMonI1" value="<%=strMonI1%>">
		<select name="strMonIT1">
			<% Call LoadTimeType(dbMain,strMonIT1) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strMonO1" value="<%=strMonO1%>">
		<select name="strMonOT1">
			<% Call LoadTimeType(dbMain,strMonOT1) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strMonI2" value="<%=strMonI2%>">
		<select name="strMonIT2">
			<% Call LoadTimeType(dbMain,strMonIT2) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strMonO2" value="<%=strMonO2%>">
		<select name="strMonOT2">
			<% Call LoadTimeType(dbMain,strMonOT2) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strMonI3" value="<%=strMonI3%>">
		<select name="strMonIT3">
			<% Call LoadTimeType(dbMain,strMonIT3) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strMonO3" value="<%=strMonO3%>">
		<select name="strMonOT3">
			<% Call LoadTimeType(dbMain,strMonOT3) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strMonI4" value="<%=strMonI4%>">
		<select name="strMonIT4">
			<% Call LoadTimeType(dbMain,strMonIT4) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strMonO4" value="<%=strMonO4%>">
		<select name="strMonOT4">
			<% Call LoadTimeType(dbMain,strMonOT4) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strMonDA,2)%></label></td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strMonTO,2)%></label></td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strMonPT,2)%></label></td>
   </tr>
	<tr>
     <td class="Header" style="text-align:left; width:30px">Tue</td>
     <td class="data" style="text-align:left; width:70px"><input type=hidden name=dtTue value="<%=dtTue%>"><Label class=control><%=dtTue%></label></td>
     <td class="data" style="text-align:left; width:125px"><input  maxlength="5" size="2" Type="date" tabindex=1 name="strTueI1" value="<%=strTueI1%>" />
		<select name="strTueIT1">
			<% Call LoadTimeType(dbMain,strTueIT1) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strTueO1" value="<%=strTueO1%>">
		<select name="strTueOT1">
			<% Call LoadTimeType(dbMain,strTueOT1) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strTueI2" value="<%=strTueI2%>">
		<select name="strTueIT2">
			<% Call LoadTimeType(dbMain,strTueIT2) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strTueO2" value="<%=strTueO2%>">
		<select name="strTueOT2">
			<% Call LoadTimeType(dbMain,strTueOT2) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strTueI3" value="<%=strTueI3%>">
		<select name="strTueIT3">
			<% Call LoadTimeType(dbMain,strTueIT3) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strTueO3" value="<%=strTueO3%>">
		<select name="strTueOT3">
			<% Call LoadTimeType(dbMain,strTueOT3) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strTueI4" value="<%=strTueI4%>">
		<select name="strTueIT4">
			<% Call LoadTimeType(dbMain,strTueIT4) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strTueO4" value="<%=strTueO4%>">
		<select name="strTueOT4">
			<% Call LoadTimeType(dbMain,strTueOT4) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strTueDA,2)%></label></td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strTueTO,2)%></label></td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strTuePT,2)%></label></td>
    </tr>
	<tr style="vertical-align:middle">
     <td class="Header" style="text-align:left; width:30px">Wed</td>
     <td class="data" style="text-align:left; width:70px"><input type=hidden name=dtWed value="<%=dtWed%>"><Label class=control><%=dtWed%></label></td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strWedI1" value="<%=strWedI1%>">
		<select name="strWedIT1">
			<% Call LoadTimeType(dbMain,strWedIT1) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strWedO1" value="<%=strWedO1%>">
		<select name="strWedOT1">
			<% Call LoadTimeType(dbMain,strWedOT1) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strWedI2" value="<%=strWedI2%>">
		<select name="strWedIT2">
			<% Call LoadTimeType(dbMain,strWedIT2) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strWedO2" value="<%=strWedO2%>">
		<select name="strWedOT2">
			<% Call LoadTimeType(dbMain,strWedOT2) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strWedI3" value="<%=strWedI3%>">
		<select name="strWedIT3">
			<% Call LoadTimeType(dbMain,strWedIT3) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strWedO3" value="<%=strWedO3%>">
		<select name="strWedOT3">
			<% Call LoadTimeType(dbMain,strWedOT3) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strWedI4" value="<%=strWedI4%>">
		<select name="strWedIT4">
			<% Call LoadTimeType(dbMain,strWedIT4) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strWedO4" value="<%=strWedO4%>">
		<select name="strWedOT4">
			<% Call LoadTimeType(dbMain,strWedOT4) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strWedDA,2)%></label></td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strWedTO,2)%></label></td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strWedPT,2)%></label></td>
  </tr>
	<tr style="vertical-align:middle">
     <td class="Header" style="text-align:left; width:30px">Thu</td>
     <td class="data" style="text-align:left; width:70px"><input type=hidden name=dtThu value="<%=dtThu%>"><Label class=control><%=dtThu%></label></td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strThuI1" value="<%=strThuI1%>">
		<select name="strThuIT1">
			<% Call LoadTimeType(dbMain,strThuIT1) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strThuO1" value="<%=strThuO1%>">
		<select name="strThuOT1">
			<% Call LoadTimeType(dbMain,strThuOT1) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strThuI2" value="<%=strThuI2%>">
		<select name="strThuIT2">
			<% Call LoadTimeType(dbMain,strThuIT2) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strThuO2" value="<%=strThuO2%>">
		<select name="strThuOT2">
			<% Call LoadTimeType(dbMain,strThuOT2) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strThuI3" value="<%=strThuI3%>">
		<select name="strThuIT3">
			<% Call LoadTimeType(dbMain,strThuIT3) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strThuO3" value="<%=strThuO3%>">
		<select name="strThuOT3">
			<% Call LoadTimeType(dbMain,strThuOT3) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strThuI4" value="<%=strThuI4%>">
		<select name="strThuIT4">
			<% Call LoadTimeType(dbMain,strThuIT4) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strThuO4" value="<%=strThuO4%>">
		<select name="strThuOT4">
			<% Call LoadTimeType(dbMain,strThuOT4) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strThuDA,2)%></label></td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strThuTO,2)%></label></td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strThuPT,2)%></label></td>
   </tr>
	<tr style="vertical-align:middle">
     <td class="Header" style="text-align:left; width:30px">Fri</td>
     <td class="data" style="text-align:left; width:70px"><input type=hidden name=dtFri value="<%=dtFri%>"><Label class=control><%=dtFri%></label></td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strFriI1" value="<%=strFriI1%>">
		<select name="strFriIT1">
			<% Call LoadTimeType(dbMain,strFriIT1) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strFriO1" value="<%=strFriO1%>">
		<select name="strFriOT1">
			<% Call LoadTimeType(dbMain,strFriOT1) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strFriI2" value="<%=strFriI2%>">
		<select name="strFriIT2">
			<% Call LoadTimeType(dbMain,strFriIT2) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strFriO2" value="<%=strFriO2%>">
		<select name="strFriOT2">
			<% Call LoadTimeType(dbMain,strFriOT2) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strFriI3" value="<%=strFriI3%>">
		<select name="strFriIT3">
			<% Call LoadTimeType(dbMain,strFriIT3) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strFriO3" value="<%=strFriO3%>">
		<select name="strFriOT3">
			<% Call LoadTimeType(dbMain,strFriOT3) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strFriI4" value="<%=strFriI4%>">
		<select name="strFriIT4">
			<% Call LoadTimeType(dbMain,strFriIT4) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strFriO4" value="<%=strFriO4%>">
		<select name="strFriOT4">
			<% Call LoadTimeType(dbMain,strFriOT4) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strFriDA,2)%></label></td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strFriTO,2)%></label></td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strFriPT,2)%></label></td>
    </tr>
	<tr style="vertical-align:middle">
     <td class="Header" style="text-align:left; width:30px">Sat</td>
     <td class="data" style="text-align:left; width:70px"><input type=hidden name=dtSat value="<%=dtSat%>"><Label class=control><%=dtSat%></label></td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSatI1" value="<%=strSatI1%>">
		<select name="strSatIT1">
			<% Call LoadTimeType(dbMain,strSatIT1) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSatO1" value="<%=strSatO1%>">
		<select name="strSatOT1">
			<% Call LoadTimeType(dbMain,strSatOT1) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSatI2" value="<%=strSatI2%>">
		<select name="strSatIT2">
			<% Call LoadTimeType(dbMain,strSatIT2) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSatO2" value="<%=strSatO2%>">
		<select name="strSatOT2">
			<% Call LoadTimeType(dbMain,strSatOT2) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSatI3" value="<%=strSatI3%>">
		<select name="strSatIT3">
			<% Call LoadTimeType(dbMain,strSatIT3) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSatO3" value="<%=strSatO3%>">
		<select name="strSatOT3">
			<% Call LoadTimeType(dbMain,strSatOT3) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSatI4" value="<%=strSatI4%>">
		<select name="strSatIT4">
			<% Call LoadTimeType(dbMain,strSatIT4) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:125px"><input maxlength="5" size="2" Type="date" tabindex=1 name="strSatO4" value="<%=strSatO4%>">
		<select name="strSatOT4">
			<% Call LoadTimeType(dbMain,strSatOT4) %>				
		</select>
	 </td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strSatDA,2)%></label></td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strSatTO,2)%></label></td>
     <td class="data" style="text-align:left; width:40px"><Label class=control><%=FormatNumber(strSatPT,2)%></label></td>
    </tr>
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
<script language="VBSCRIPT">
Option Explicit

Sub Window_OnLoad()
	IF cdbl(document.all("strSalery").value) > 0.0 then
		parent.document.all("lblHours").innerText = document.all("strSatTo").value
		parent.document.all("lblBonusHrs").innerText = 0.0
		parent.document.all("lblBonus").innerText = formatCurrency(0.0,2)
		parent.document.all("lblTotal").innerText = formatCurrency(document.all("strSalery").value,2)
		parent.document.all("lblGTotal").innerText = formatCurrency(cdbl(parent.document.all("lblAdjAmt").innerText)+cdbl(parent.document.all("lblCollAmt").innerText)+cdbl(parent.document.all("lblUnifAmt").innerText) +cdbl(parent.document.all("lblTotal").innerText),2)
	ELSE
		IF document.all("strSatPT").value <= 40 then
		parent.document.all("lblHours").innerText = document.all("strSatPT").value
		parent.document.all("lblBonusHrs").innerText = 0.0
		parent.document.all("lblBonus").innerText = 0.0
		ELSE
		parent.document.all("lblHours").innerText = 40.0
		parent.document.all("lblBonusHrs").innerText = round(document.all("strSatPT").value-40,2)
		parent.document.all("lblBonus").innerText = formatCurrency((parent.document.all("strPayRate").value)*(document.all("strSatPT").value-40),2)
		END IF
		parent.document.all("lblTotal").innerText = formatCurrency(parent.document.all("strPayRate").value*cdbl(parent.document.all("lblHours").innerText),2)
		'parent.document.all("lblDTotal").innerText = formatCurrency(cdbl(parent.document.all("strDTotal").value),2)
		parent.document.all("lblGTotal").innerText = formatCurrency(cdbl(parent.document.all("lblAdjAmt").innerText)+cdbl(parent.document.all("lblcollAmt").innerText)+cdbl(parent.document.all("lblUnifAmt").innerText) +cdbl(parent.document.all("lblTotal").innerText) + cdbl(parent.document.all("lblDTotal").innerText)+cdbl(parent.document.all("lblBonus").innerText),2)
	END IF
End Sub

Sub btnSave_OnClick()
	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		MsgBox "Time has been Paid NO edits allowed"
	ELSE
		admTimeCardFra.FormAction.value="SaveData"
		admTimeCardFra.submit()
	End If
End Sub

</script>

<%
Call CloseConnection(dbMain)
End Sub

'********************************************************************
' Server-Side Functions
'********************************************************************

Sub SaveData(dbMain)
	IF LEN(Request("strSunI1"))> 0 then
		Call Updatedata(dbMain,Request("dtSun"),Request("strSunI1"),Request("strSunIDI1"),0,Request("strSunIT1"))
	END IF
	IF LEN(Request("strSunO1"))> 0 then
		Call Updatedata(dbMain,Request("dtSun"),Request("strSunO1"),Request("strSunIDO1"),1,Request("strSunOT1"))
	END IF
	IF LEN(Request("strSunI2"))> 0 then
		Call Updatedata(dbMain,Request("dtSun"),Request("strSunI2"),Request("strSunIDI2"),0,Request("strSunIT2"))
	END IF
	IF LEN(Request("strSunO2"))> 0 then
		Call Updatedata(dbMain,Request("dtSun"),Request("strSunO2"),Request("strSunIDO2"),1,Request("strSunOT2"))
	END IF
	IF LEN(Request("strSunI3"))> 0 then
		Call Updatedata(dbMain,Request("dtSun"),Request("strSunI3"),Request("strSunIDI3"),0,Request("strSunIT3"))
	END IF
	IF LEN(Request("strSunO3"))> 0 then
		Call Updatedata(dbMain,Request("dtSun"),Request("strSunO3"),Request("strSunIDO3"),1,Request("strSunOT3"))
	END IF
	IF LEN(Request("strSunI4"))> 0 then
		Call Updatedata(dbMain,Request("dtSun"),Request("strSunI4"),Request("strSunIDI4"),0,Request("strSunIT4"))
	END IF
	IF LEN(Request("strSunO4"))> 0 then
		Call Updatedata(dbMain,Request("dtSun"),Request("strSunO4"),Request("strSunIDO4"),1,Request("strSunOT4"))
	END IF
	' Mon
	IF LEN(Request("strMonI1"))> 0 then
		Call Updatedata(dbMain,Request("dtMon"),Request("strMonI1"),Request("strMonIDI1"),0,Request("strMonIT1"))
	END IF
	IF LEN(Request("strMonO1"))> 0 then
		Call Updatedata(dbMain,Request("dtMon"),Request("strMonO1"),Request("strMonIDO1"),1,Request("strMonOT1"))
	END IF
	IF LEN(Request("strMonI2"))> 0 then
		Call Updatedata(dbMain,Request("dtMon"),Request("strMonI2"),Request("strMonIDI2"),0,Request("strMonIT2"))
	END IF
	IF LEN(Request("strMonO2"))> 0 then
		Call Updatedata(dbMain,Request("dtMon"),Request("strMonO2"),Request("strMonIDO2"),1,Request("strMonOT2"))
	END IF
	IF LEN(Request("strMonI3"))> 0 then
		Call Updatedata(dbMain,Request("dtMon"),Request("strMonI3"),Request("strMonIDI3"),0,Request("strMonIT3"))
	END IF
	IF LEN(Request("strMonO3"))> 0 then
		Call Updatedata(dbMain,Request("dtMon"),Request("strMonO3"),Request("strMonIDO3"),1,Request("strMonOT3"))
	END IF
	IF LEN(Request("strMonI4"))> 0 then
		Call Updatedata(dbMain,Request("dtMon"),Request("strMonI4"),Request("strMonIDI4"),0,Request("strMonIT4"))
	END IF
	IF LEN(Request("strMonO4"))> 0 then
		Call Updatedata(dbMain,Request("dtMon"),Request("strMonO4"),Request("strMonIDO4"),1,Request("strMonOT4"))
	END IF
	' Tue
	IF LEN(Request("strTueI1"))> 0 then
		Call Updatedata(dbMain,Request("dtTue"),Request("strTueI1"),Request("strTueIDI1"),0,Request("strTueIT1"))
	END IF
	IF LEN(Request("strTueO1"))> 0 then
		Call Updatedata(dbMain,Request("dtTue"),Request("strTueO1"),Request("strTueIDO1"),1,Request("strTueOT1"))
	END IF
	IF LEN(Request("strTueI2"))> 0 then
		Call Updatedata(dbMain,Request("dtTue"),Request("strTueI2"),Request("strTueIDI2"),0,Request("strTueIT2"))
	END IF
	IF LEN(Request("strTueO2"))> 0 then
		Call Updatedata(dbMain,Request("dtTue"),Request("strTueO2"),Request("strTueIDO2"),1,Request("strTueOT2"))
	END IF
	IF LEN(Request("strTueI3"))> 0 then
		Call Updatedata(dbMain,Request("dtTue"),Request("strTueI3"),Request("strTueIDI3"),0,Request("strTueIT3"))
	END IF
	IF LEN(Request("strTueO3"))> 0 then
		Call Updatedata(dbMain,Request("dtTue"),Request("strTueO3"),Request("strTueIDO3"),1,Request("strTueOT3"))
	END IF
	IF LEN(Request("strTueI4"))> 0 then
		Call Updatedata(dbMain,Request("dtTue"),Request("strTueI4"),Request("strTueIDI4"),0,Request("strTueIT4"))
	END IF
	IF LEN(Request("strTueO4"))> 0 then
		Call Updatedata(dbMain,Request("dtTue"),Request("strTueO4"),Request("strTueIDO4"),1,Request("strTueOT4"))
	END IF
	' Wed
	IF LEN(Request("strWedI1"))> 0 then
		Call Updatedata(dbMain,Request("dtWed"),Request("strWedI1"),Request("strWedIDI1"),0,Request("strWedIT1"))
	END IF
	IF LEN(Request("strWedO1"))> 0 then
		Call Updatedata(dbMain,Request("dtWed"),Request("strWedO1"),Request("strWedIDO1"),1,Request("strWedOT1"))
	END IF
	IF LEN(Request("strWedI2"))> 0 then
		Call Updatedata(dbMain,Request("dtWed"),Request("strWedI2"),Request("strWedIDI2"),0,Request("strWedIT2"))
	END IF
	IF LEN(Request("strWedO2"))> 0 then
		Call Updatedata(dbMain,Request("dtWed"),Request("strWedO2"),Request("strWedIDO2"),1,Request("strWedOT2"))
	END IF
	IF LEN(Request("strWedI3"))> 0 then
		Call Updatedata(dbMain,Request("dtWed"),Request("strWedI3"),Request("strWedIDI3"),0,Request("strWedIT3"))
	END IF
	IF LEN(Request("strWedO3"))> 0 then
		Call Updatedata(dbMain,Request("dtWed"),Request("strWedO3"),Request("strWedIDO3"),1,Request("strWedOT3"))
	END IF
	IF LEN(Request("strWedI4"))> 0 then
		Call Updatedata(dbMain,Request("dtWed"),Request("strWedI4"),Request("strWedIDI4"),0,Request("strWedIT4"))
	END IF
	IF LEN(Request("strWedO4"))> 0 then
		Call Updatedata(dbMain,Request("dtWed"),Request("strWedO4"),Request("strWedIDO4"),1,Request("strWedOT4"))
	END IF
	' Thu
	IF LEN(Request("strThuI1"))> 0 then
		Call Updatedata(dbMain,Request("dtThu"),Request("strThuI1"),Request("strThuIDI1"),0,Request("strThuIT1"))
	END IF
	IF LEN(Request("strThuO1"))> 0 then
		Call Updatedata(dbMain,Request("dtThu"),Request("strThuO1"),Request("strThuIDO1"),1,Request("strThuOT1"))
	END IF
	IF LEN(Request("strThuI2"))> 0 then
		Call Updatedata(dbMain,Request("dtThu"),Request("strThuI2"),Request("strThuIDI2"),0,Request("strThuIT2"))
	END IF
	IF LEN(Request("strThuO2"))> 0 then
		Call Updatedata(dbMain,Request("dtThu"),Request("strThuO2"),Request("strThuIDO2"),1,Request("strThuOT2"))
	END IF
	IF LEN(Request("strThuI3"))> 0 then
		Call Updatedata(dbMain,Request("dtThu"),Request("strThuI3"),Request("strThuIDI3"),0,Request("strThuIT3"))
	END IF
	IF LEN(Request("strThuO3"))> 0 then
		Call Updatedata(dbMain,Request("dtThu"),Request("strThuO3"),Request("strThuIDO3"),1,Request("strThuOT3"))
	END IF
	IF LEN(Request("strThuI4"))> 0 then
		Call Updatedata(dbMain,Request("dtThu"),Request("strThuI4"),Request("strThuIDI4"),0,Request("strThuIT4"))
	END IF
	IF LEN(Request("strThuO4"))> 0 then
		Call Updatedata(dbMain,Request("dtThu"),Request("strThuO4"),Request("strThuIDO4"),1,Request("strThuOT4"))
	END IF
	' Fri
	IF LEN(Request("strFriI1"))> 0 then
		Call Updatedata(dbMain,Request("dtFri"),Request("strFriI1"),Request("strFriIDI1"),0,Request("strFriIT1"))
	END IF
	IF LEN(Request("strFriO1"))> 0 then
		Call Updatedata(dbMain,Request("dtFri"),Request("strFriO1"),Request("strFriIDO1"),1,Request("strFriOT1"))
	END IF
	IF LEN(Request("strFriI2"))> 0 then
		Call Updatedata(dbMain,Request("dtFri"),Request("strFriI2"),Request("strFriIDI2"),0,Request("strFriIT2"))
	END IF
	IF LEN(Request("strFriO2"))> 0 then
		Call Updatedata(dbMain,Request("dtFri"),Request("strFriO2"),Request("strFriIDO2"),1,Request("strFriOT2"))
	END IF
	IF LEN(Request("strFriI3"))> 0 then
		Call Updatedata(dbMain,Request("dtFri"),Request("strFriI3"),Request("strFriIDI3"),0,Request("strFriIT3"))
	END IF
	IF LEN(Request("strFriO3"))> 0 then
		Call Updatedata(dbMain,Request("dtFri"),Request("strFriO3"),Request("strFriIDO3"),1,Request("strFriOT3"))
	END IF
	IF LEN(Request("strFriI4"))> 0 then
		Call Updatedata(dbMain,Request("dtFri"),Request("strFriI4"),Request("strFriIDI4"),0,Request("strFriIT4"))
	END IF
	IF LEN(Request("strFriO4"))> 0 then
		Call Updatedata(dbMain,Request("dtFri"),Request("strFriO4"),Request("strFriIDO4"),1,Request("strFriOT4"))
	END IF
	' Sat
	IF LEN(Request("strSatI1"))> 0 then
		Call Updatedata(dbMain,Request("dtSat"),Request("strSatI1"),Request("strSatIDI1"),0,Request("strSatIT1"))
	END IF
	IF LEN(Request("strSatO1"))> 0 then
		Call Updatedata(dbMain,Request("dtSat"),Request("strSatO1"),Request("strSatIDO1"),1,Request("strSatOT1"))
	END IF
	IF LEN(Request("strSatI2"))> 0 then
		Call Updatedata(dbMain,Request("dtSat"),Request("strSatI2"),Request("strSatIDI2"),0,Request("strSatIT2"))
	END IF
	IF LEN(Request("strSatO2"))> 0 then
		Call Updatedata(dbMain,Request("dtSat"),Request("strSatO2"),Request("strSatIDO2"),1,Request("strSatOT2"))
	END IF
	IF LEN(Request("strSatI3"))> 0 then
		Call Updatedata(dbMain,Request("dtSat"),Request("strSatI3"),Request("strSatIDI3"),0,Request("strSatIT3"))
	END IF
	IF LEN(Request("strSatO3"))> 0 then
		Call Updatedata(dbMain,Request("dtSat"),Request("strSatO3"),Request("strSatIDO3"),1,Request("strSatOT3"))
	END IF
	IF LEN(Request("strSatI4"))> 0 then
		Call Updatedata(dbMain,Request("dtSat"),Request("strSatI4"),Request("strSatIDI4"),0,Request("strSatIT4"))
	END IF
	IF LEN(Request("strSatO4"))> 0 then
		Call Updatedata(dbMain,Request("dtSat"),Request("strSatO4"),Request("strSatIDO4"),1,Request("strSatOT4"))
	END IF
''Response.end

End Sub

Sub updatedata(dbMain,varDate,varTime,intClockID,intAct,intCtype)
	Dim strSQL,rsData,MaxSQL

'Response.Write varDate&"<BR>"
'Response.Write varTime&"<BR>"
'Response.Write intClockID&"<BR>"
'Response.Write intAct&"<BR>"
'Response.Write intCtype&"<BR>"

	IF ISNULL(intCtype) or LEN(intCtype)=0 then
		intCtype = 0
	END IF
	IF ISNULL(intClockID) or LEN(intClockID)=0 then
		MaxSQL = " Select ISNULL(Max(ClockID),0)+1 from timeclock (NOLOCK) where (timeclock.LocationID = "& Request("LocationID") &")"
		If dbOpenStaticRecordset(dbmain,rsData,MaxSQL) Then
			intClockID=rsData(0)
		End if
		Set rsData = Nothing
		strSQL=" Insert into timeclock(ClockID,LocationID,UserID,Cdatetime,CAction,CType,EditBy,EditDate,Paid)Values(" & _
					intClockID & ", " & _
					Request("LocationID") & ", " & _
					Request("hdnuserid") & ", " & _
					"'" & varDate &" "& varTime  & "', " & _
					intAct & ", " & _
					intCtype & ", " & _
					Request("LoginID") & ", " & _
					"'" & Date() & "',0)"
		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
	ELSE
		IF LEN(ltrim(varTime)) = 0 or (LEN(ltrim(varTime)) > 0 and (varTime = "0" or varTime = "00"or varTime = "000" or varTime = "0000" or varTime = "00000")) then
			strSQL= " delete timeclock Where ClockID = " & intClockID & " AND (timeclock.LocationID = "& Request("LocationID") &")"
		ELSE
			strSQL= " Update timeclock SET "&_
				" CAction = "& intAct &","&_
				" Cdatetime = '"& varDate &" "& varTime &"',"&_
				" CType = "& intCtype &","&_
				" Editby = "& Request("LoginID") &","&_
				" Editdate = '"& Now() & "'"&_
				" Where ClockID = " & intClockID &" AND (timeclock.LocationID = "& Request("LocationID") &")"
		END IF
		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
	END IF
'Response.Write strSQL&"<BR>"
'Response.End
End Sub



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

Sub LoadTimeType(db,var)
	Dim strSQL,RS,strSel
				%>
				<option Value=" "></option>
				<%
	strSQL="SELECT ListValue, LEFT(ListDesc, 2) AS ListDesc FROM LM_ListItem WHERE listType = 6 order by ItemOrder"

	If dbOpenStaticRecordset(db,rs,strSQL) Then
		Do While Not RS.EOF
			If Trim(var) = Trim(RS("ListValue")) Then
				strSel = "selected" 
				%>
				<option Value="<%=RS("ListValue")%>" <%=strSel%>><%=RS("ListDesc")%></option>
				<%
			Else
				strSel = ""
				%>
				<option Value="<%=RS("ListValue")%>" <%=strSel%>><%=RS("ListDesc")%></option>
				<%
			End IF
			RS.MoveNext
		Loop
	End If
End Sub

Function IIF(var1,var2,var3)
	IF var1 then
		IIF = var2
	ELSE
		IIF = var3
	END IF
End Function

%>
 

