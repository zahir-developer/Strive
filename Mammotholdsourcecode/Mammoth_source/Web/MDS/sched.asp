<%@  language="VBSCRIPT" %>
<%
'********************************************************************
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
    'Response.write("coming");
	Dim dbMain,strSQL,rsData,strImage,LocationID,LoginID
	Set dbMain =  OpenConnection
    LocationID = request("LocationID")
    LoginID = request("LoginID")
	
	'strLocName = Request.QueryString("strLocName")
'********************************************************************
' HTML
'********************************************************************<%
Dim dDate     ' Date we're displaying calendar for
Dim iDIM      ' Days In Month
Dim iDOW      ' Day Of Week that month starts on
Dim iCurrent  ' Variable we use to hold current day of month as we write table
Dim iPosition ' Variable we use to hold current position in table


If IsDate(Request.QueryString("date")) Then
	dDate = CDate(Request.QueryString("date"))
Else
	If IsDate(Request.QueryString("month") & "-" & Request.QueryString("day") & "-" & Request.QueryString("year")) Then
		dDate = CDate(Request.QueryString("month") & "-" & Request.QueryString("day") & "-" & Request.QueryString("year"))
	Else
		dDate = Date()
		' The annoyingly bad solution for those of you running IIS3
		If Len(Request.QueryString("month")) <> 0 Or Len(Request.QueryString("day")) <> 0 Or Len(Request.QueryString("year")) <> 0 Or Len(Request.QueryString("date")) <> 0 Then
			Response.Write "The date you picked was not a valid date.  The calendar was set to today's date.<BR><BR>"
		End If
		' The elegant solution for those of you running IIS4
		'If Request.QueryString.Count <> 0 Then Response.Write "The date you picked was not a valid date.  The calendar was set to today's date.<BR><BR>"
	End If
End If

'Now we've got the date.  Now get Days in the choosen month and the day of the week it starts on.
iDIM = GetDaysInMonth(Month(dDate), Year(dDate))
iDOW = GetWeekdayMonthStartsOn(dDate)


DIM intT1B1,intT2B1,intT3B1,intT4B1,intT5B1,intT6B1,intT7B1,intT8B1,intT9B1,intT10B1
DIM intT11B1,intT12B1,intT13B1,intT14B1,intT15B1,intT16B1,intT17B1,intT18B1,intT19B1,intT20B1
DIM intT21B1,intT22B1,intT23B1,intT24B1
DIM intT1B2,intT2B2,intT3B2,intT4B2,intT5B2,intT6B2,intT7B2,intT8B2,intT9B2,intT10B2
DIM intT11B2,intT12B2,intT13B2,intT14B2,intT15B2,intT16B2,intT17B2,intT18B2,intT19B2,intT20B2
DIM intT21B2,intT22B2,intT23B2,intT24B2
DIM intT1B3,intT2B3,intT3B3,intT4B3,intT5B3,intT6B3,intT7B3,intT8B3,intT9B3,intT10B3
DIM intT11B3,intT12B3,intT13B3,intT14B3,intT15B3,intT16B3,intT17B3,intT18B3,intT19B3,intT20B3
DIM intT21B3,intT22B3,intT23B3,intT24B3
			intT1B1 = False
			intT2B1 = False
			intT3B1 = False
			intT4B1 = False
			intT5B1 = False
			intT6B1 = False
			intT7B1 = False
			intT8B1 = False
			intT9B1 = False
			intT10B1 = False
			intT11B1 = False
			intT12B1 = False
			intT13B1 = False
			intT14B1 = False
			intT15B1 = False
			intT16B1 = False
			intT17B1 = False
			intT18B1 = False
			intT19B1 = False
			intT20B1 = False
			intT21B1 = False
			intT22B1 = False
			intT23B1 = False
			intT24B1 = False
			intT1B2 = False
			intT2B2 = False
			intT3B2 = False
			intT4B2 = False
			intT5B2 = False
			intT6B2 = False
			intT7B2 = False
			intT8B2 = False
			intT9B2 = False
			intT10B2 = False
			intT11B2 = False
			intT12B2 = False
			intT13B2 = False
			intT14B2 = False
			intT15B2 = False
			intT16B2 = False
			intT17B2 = False
			intT18B2 = False
			intT19B2 = False
			intT20B2 = False
			intT21B2 = False
			intT22B2 = False
			intT23B2 = False
			intT24B2 = False
			intT1B3 = False
			intT2B3 = False
			intT3B3 = False
			intT4B3 = False
			intT5B3 = False
			intT6B3 = False
			intT7B3 = False
			intT8B3 = False
			intT9B3 = False
			intT10B3 = False
			intT11B3 = False
			intT12B3 = False
			intT13B3 = False
			intT14B3 = False
			intT15B3 = False
			intT16B3 = False
			intT17B3 = False
			intT18B3 = False
			intT19B3 = False
			intT20B3 = False
			intT21B3 = False
			intT22B3 = False
			intT23B3 = False
			intT24B3 = False

	strSQL="SELECT REC.datein, REC.esttime FROM REC(NOLOCK)"&_
			" WHERE (day(REC.Datein) = day('"& dDate &"')"&_
			" AND MONTH(REC.Datein) = MONTH('"& dDate &"')"&_
			" AND YEAR(REC.Datein) = YEAR('"& dDate &"'))"&_
			" AND REC.Line = 4 and LocationID="& LocationID &" ORDER BY REC.Datein ASC"

	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		If Not rsData.EOF Then
			DO While not rsData.EOF
				intT1B1 = TimeCheck(intT1B1,rsData("datein"),rsData("esttime"),"700")
				intT2B1 = TimeCheck(intT2B1,rsData("datein"),rsData("esttime"),"750")
				intT3B1 = TimeCheck(intT3B1,rsData("datein"),rsData("esttime"),"800")
				intT4B1 = TimeCheck(intT4B1,rsData("datein"),rsData("esttime"),"850")
				intT5B1 = TimeCheck(intT5B1,rsData("datein"),rsData("esttime"),"900")
				intT6B1 = TimeCheck(intT6B1,rsData("datein"),rsData("esttime"),"950")
				intT7B1 = TimeCheck(intT7B1,rsData("datein"),rsData("esttime"),"1000")
				intT8B1 = TimeCheck(intT8B1,rsData("datein"),rsData("esttime"),"1050")
				intT9B1 = TimeCheck(intT9B1,rsData("datein"),rsData("esttime"),"1100")
				intT10B1 = TimeCheck(intT10B1,rsData("datein"),rsData("esttime"),"1150")
				intT11B1 = TimeCheck(intT11B1,rsData("datein"),rsData("esttime"),"1200")
				intT12B1 = TimeCheck(intT12B1,rsData("datein"),rsData("esttime"),"1250")
				intT13B1 = TimeCheck(intT13B1,rsData("datein"),rsData("esttime"),"1300")
				intT14B1 = TimeCheck(intT14B1,rsData("datein"),rsData("esttime"),"1350")
				intT15B1 = TimeCheck(intT15B1,rsData("datein"),rsData("esttime"),"1400")
				intT16B1 = TimeCheck(intT16B1,rsData("datein"),rsData("esttime"),"1450")
				intT17B1 = TimeCheck(intT17B1,rsData("datein"),rsData("esttime"),"1500")
				intT18B1 = TimeCheck(intT18B1,rsData("datein"),rsData("esttime"),"1550")
				intT19B1 = TimeCheck(intT19B1,rsData("datein"),rsData("esttime"),"1600")
				intT20B1 = TimeCheck(intT20B1,rsData("datein"),rsData("esttime"),"1650")
				intT21B1 = TimeCheck(intT21B1,rsData("datein"),rsData("esttime"),"1700")
				intT22B1 = TimeCheck(intT22B1,rsData("datein"),rsData("esttime"),"1750")
				intT23B1 = TimeCheck(intT23B1,rsData("datein"),rsData("esttime"),"1800")
				intT24B1 = TimeCheck(intT24B1,rsData("datein"),rsData("esttime"),"1850")
				rsData.MoveNext
			Loop	
		End If
	End If
	Set rsData = Nothing
	strSQL="SELECT REC.datein, REC.esttime FROM REC(NOLOCK)"&_
			" WHERE (day(REC.Datein) = day('"& dDate &"')"&_
			" AND MONTH(REC.Datein) = MONTH('"& dDate &"')"&_
			" AND YEAR(REC.Datein) = YEAR('"& dDate &"'))"&_
			" AND REC.Line = 5 and LocationID="& LocationID &" ORDER BY REC.Datein ASC"

	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		If Not rsData.EOF Then
			DO While not rsData.EOF
				intT1B2 = TimeCheck(intT1B2,rsData("datein"),rsData("esttime"),"700")
				intT2B2 = TimeCheck(intT2B2,rsData("datein"),rsData("esttime"),"750")
				intT3B2 = TimeCheck(intT3B2,rsData("datein"),rsData("esttime"),"800")
				intT4B2 = TimeCheck(intT4B2,rsData("datein"),rsData("esttime"),"850")
				intT5B2 = TimeCheck(intT5B2,rsData("datein"),rsData("esttime"),"900")
				intT6B2 = TimeCheck(intT6B2,rsData("datein"),rsData("esttime"),"950")
				intT7B2 = TimeCheck(intT7B2,rsData("datein"),rsData("esttime"),"1000")
				intT8B2 = TimeCheck(intT8B2,rsData("datein"),rsData("esttime"),"1050")
				intT9B2 = TimeCheck(intT9B2,rsData("datein"),rsData("esttime"),"1100")
				intT10B2 = TimeCheck(intT10B2,rsData("datein"),rsData("esttime"),"1150")
				intT11B2 = TimeCheck(intT11B2,rsData("datein"),rsData("esttime"),"1200")
				intT12B2 = TimeCheck(intT12B2,rsData("datein"),rsData("esttime"),"1250")
				intT13B2 = TimeCheck(intT13B2,rsData("datein"),rsData("esttime"),"1300")
				intT14B2 = TimeCheck(intT14B2,rsData("datein"),rsData("esttime"),"1350")
				intT15B2 = TimeCheck(intT15B2,rsData("datein"),rsData("esttime"),"1400")
				intT16B2 = TimeCheck(intT16B2,rsData("datein"),rsData("esttime"),"1450")
				intT17B2 = TimeCheck(intT17B2,rsData("datein"),rsData("esttime"),"1500")
				intT18B2 = TimeCheck(intT18B2,rsData("datein"),rsData("esttime"),"1550")
				intT19B2 = TimeCheck(intT19B2,rsData("datein"),rsData("esttime"),"1600")
				intT20B2 = TimeCheck(intT20B2,rsData("datein"),rsData("esttime"),"1650")
				intT21B2 = TimeCheck(intT21B2,rsData("datein"),rsData("esttime"),"1700")
				intT22B2 = TimeCheck(intT22B2,rsData("datein"),rsData("esttime"),"1750")
				intT23B2 = TimeCheck(intT23B2,rsData("datein"),rsData("esttime"),"1800")
				intT24B2 = TimeCheck(intT24B2,rsData("datein"),rsData("esttime"),"1850")
				rsData.MoveNext
			Loop	
		End If
	End If
	Set rsData = Nothing
	strSQL="SELECT REC.datein, REC.esttime FROM REC(NOLOCK)"&_
			" WHERE (day(REC.Datein) = day('"& dDate &"')"&_
			" AND MONTH(REC.Datein) = MONTH('"& dDate &"')"&_
			" AND YEAR(REC.Datein) = YEAR('"& dDate &"'))"&_
			" AND REC.Line = 6 and LocationID="& LocationID &" ORDER BY REC.Datein ASC"

	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		If Not rsData.EOF Then
			DO While not rsData.EOF
				intT1B3 = TimeCheck(intT1B3,rsData("datein"),rsData("esttime"),"700")
				intT2B3 = TimeCheck(intT2B3,rsData("datein"),rsData("esttime"),"750")
				intT3B3 = TimeCheck(intT3B3,rsData("datein"),rsData("esttime"),"800")
				intT4B3 = TimeCheck(intT4B3,rsData("datein"),rsData("esttime"),"850")
				intT5B3 = TimeCheck(intT5B3,rsData("datein"),rsData("esttime"),"900")
				intT6B3 = TimeCheck(intT6B3,rsData("datein"),rsData("esttime"),"950")
				intT7B3 = TimeCheck(intT7B3,rsData("datein"),rsData("esttime"),"1000")
				intT8B3 = TimeCheck(intT8B3,rsData("datein"),rsData("esttime"),"1050")
				intT9B3 = TimeCheck(intT9B3,rsData("datein"),rsData("esttime"),"1100")
				intT10B3 = TimeCheck(intT10B3,rsData("datein"),rsData("esttime"),"1150")
				intT11B3 = TimeCheck(intT11B3,rsData("datein"),rsData("esttime"),"1200")
				intT12B3 = TimeCheck(intT12B3,rsData("datein"),rsData("esttime"),"1250")
				intT13B3 = TimeCheck(intT13B3,rsData("datein"),rsData("esttime"),"1300")
				intT14B3 = TimeCheck(intT14B3,rsData("datein"),rsData("esttime"),"1350")
				intT15B3 = TimeCheck(intT15B3,rsData("datein"),rsData("esttime"),"1400")
				intT16B3 = TimeCheck(intT16B3,rsData("datein"),rsData("esttime"),"1450")
				intT17B3 = TimeCheck(intT17B3,rsData("datein"),rsData("esttime"),"1500")
				intT18B3 = TimeCheck(intT18B3,rsData("datein"),rsData("esttime"),"1550")
				intT19B3 = TimeCheck(intT19B3,rsData("datein"),rsData("esttime"),"1600")
				intT20B3 = TimeCheck(intT20B3,rsData("datein"),rsData("esttime"),"1650")
				intT21B3 = TimeCheck(intT21B3,rsData("datein"),rsData("esttime"),"1700")
				intT22B3 = TimeCheck(intT22B3,rsData("datein"),rsData("esttime"),"1750")
				intT23B3 = TimeCheck(intT23B3,rsData("datein"),rsData("esttime"),"1800")
				intT24B3 = TimeCheck(intT24B3,rsData("datein"),rsData("esttime"),"1850")
				rsData.MoveNext
			Loop	
		End If
	End If
	Set rsData = Nothing
%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css" />
    <title></title>
</head>
<body class="pgbody">
    <form action="sched.asp" method="get" id="form1" name="form1">
        <center>
<Input type="hidden" name="dDate" value=<%=dDate%> />
<Input type="hidden" name="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" ID="LoginID" value="<%=LoginID%>" />
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

<table style="HEIGHT:100;WIDTH: 100%" border=0>
<table  width="5" BORDER=0 CELLSPACING=0 CELLPADDING=2>
<tr><td>&nbsp;</td>
</tr>
<tr>
<td>
<TABLE width="370" BORDER=0 CELLSPACING=0 CELLPADDING=2>
	<TR>
	<td colspan=7>
<TABLE BORDER=0 CELLSPACING=0 CELLPADDING=0 width="370">
<tr>
<td align=middle>
<SELECT NAME="month">
	<OPTION VALUE=1 <%=IIF(Month(dDate)=1,"SELECTED","")%>>January</OPTION>
	<OPTION VALUE=2 <%=IIF(Month(dDate)=2,"SELECTED","")%>>February</OPTION>
	<OPTION VALUE=3 <%=IIF(Month(dDate)=3,"SELECTED","")%>>March</OPTION>
	<OPTION VALUE=4 <%=IIF(Month(dDate)=4,"SELECTED","")%>>April</OPTION>
	<OPTION VALUE=5 <%=IIF(Month(dDate)=5,"SELECTED","")%>>May</OPTION>
	<OPTION VALUE=6 <%=IIF(Month(dDate)=6,"SELECTED","")%>>June</OPTION>
	<OPTION VALUE=7 <%=IIF(Month(dDate)=7,"SELECTED","")%>>July</OPTION>
	<OPTION VALUE=8 <%=IIF(Month(dDate)=8,"SELECTED","")%>>August</OPTION>
	<OPTION VALUE=9 <%=IIF(Month(dDate)=9,"SELECTED","")%>>September</OPTION>
	<OPTION VALUE=10 <%=IIF(Month(dDate)=10,"SELECTED","")%>>October</OPTION>
	<OPTION VALUE=11 <%=IIF(Month(dDate)=11,"SELECTED","")%>>November</OPTION>
	<OPTION VALUE=12 <%=IIF(Month(dDate)=12,"SELECTED","")%>>December</OPTION>
</SELECT>
<SELECT NAME="day">
	<OPTION VALUE=1 <%=IIF(Day(dDate)=1,"SELECTED","")%>>&nbsp;1</OPTION>
	<OPTION VALUE=2 <%=IIF(Day(dDate)=2,"SELECTED","")%>>&nbsp;2</OPTION>
	<OPTION VALUE=3 <%=IIF(Day(dDate)=3,"SELECTED","")%>>&nbsp;3</OPTION>
	<OPTION VALUE=4 <%=IIF(Day(dDate)=4,"SELECTED","")%>>&nbsp;4</OPTION>
	<OPTION VALUE=5 <%=IIF(Day(dDate)=5,"SELECTED","")%>>&nbsp;5</OPTION>
	<OPTION VALUE=6 <%=IIF(Day(dDate)=6,"SELECTED","")%>>&nbsp;6</OPTION>
	<OPTION VALUE=7 <%=IIF(Day(dDate)=7,"SELECTED","")%>>&nbsp;7</OPTION>
	<OPTION VALUE=8 <%=IIF(Day(dDate)=8,"SELECTED","")%>>&nbsp;8</OPTION>
	<OPTION VALUE=9 <%=IIF(Day(dDate)=9,"SELECTED","")%>>&nbsp;9</OPTION>
	<OPTION VALUE=10 <%=IIF(Day(dDate)=10,"SELECTED","")%>>10</OPTION>
	<OPTION VALUE=11 <%=IIF(Day(dDate)=11,"SELECTED","")%>>11</OPTION>
	<OPTION VALUE=12 <%=IIF(Day(dDate)=12,"SELECTED","")%>>12</OPTION>
	<OPTION VALUE=13 <%=IIF(Day(dDate)=13,"SELECTED","")%>>13</OPTION>
	<OPTION VALUE=14 <%=IIF(Day(dDate)=14,"SELECTED","")%>>14</OPTION>
	<OPTION VALUE=15 <%=IIF(Day(dDate)=15,"SELECTED","")%>>15</OPTION>
	<OPTION VALUE=16 <%=IIF(Day(dDate)=16,"SELECTED","")%>>16</OPTION>
	<OPTION VALUE=17 <%=IIF(Day(dDate)=17,"SELECTED","")%>>17</OPTION>
	<OPTION VALUE=18 <%=IIF(Day(dDate)=18,"SELECTED","")%>>18</OPTION>
	<OPTION VALUE=19 <%=IIF(Day(dDate)=19,"SELECTED","")%>>19</OPTION>
	<OPTION VALUE=20 <%=IIF(Day(dDate)=20,"SELECTED","")%>>20</OPTION>
	<OPTION VALUE=21 <%=IIF(Day(dDate)=21,"SELECTED","")%>>21</OPTION>
	<OPTION VALUE=22 <%=IIF(Day(dDate)=22,"SELECTED","")%>>22</OPTION>
	<OPTION VALUE=23 <%=IIF(Day(dDate)=23,"SELECTED","")%>>23</OPTION>
	<OPTION VALUE=24 <%=IIF(Day(dDate)=24,"SELECTED","")%>>24</OPTION>
	<OPTION VALUE=25 <%=IIF(Day(dDate)=25,"SELECTED","")%>>25</OPTION>
	<OPTION VALUE=26 <%=IIF(Day(dDate)=26,"SELECTED","")%>>26</OPTION>
	<OPTION VALUE=27 <%=IIF(Day(dDate)=27,"SELECTED","")%>>27</OPTION>
	<OPTION VALUE=28 <%=IIF(Day(dDate)=28,"SELECTED","")%>>28</OPTION>
	<OPTION VALUE=29 <%=IIF(Day(dDate)=29,"SELECTED","")%>>29</OPTION>
	<OPTION VALUE=30 <%=IIF(Day(dDate)=30,"SELECTED","")%>>30</OPTION>
	<OPTION VALUE=31 <%=IIF(Day(dDate)=31,"SELECTED","")%>>31</OPTION>
</SELECT>
<SELECT NAME="year">
<%
Dim I
For I = CInt(Year(Now()))-10 To CInt(Year(Now()))+ 10
	If I = Year(dDate) Then
		Response.Write "<Option SELECTED>" & I & "</option>" & vbCrLf
	Else
		Response.Write "<Option>" & I & "</option>" & vbCrLf
	End If
Next
%>
</SELECT>
&nbsp;
<Button TYPE="submit" id=submit1 name=submit1>Update</button>
</TD>
</TR>
</TABLE>
<TABLE width="370" BORDER=0 CELLSPACING=0 CELLPADDING=2>
	<TR>
	<td colspan=7>
	<TABLE width="100%" BORDER=0 CELLSPACING=2 CELLPADDING=2 bgcolor=MediumBlue>
	<TR>
		<TD width="15%" ALIGN="right"><A style="COLOR:#FFF;FONT-FAMILY:Verdana;FONT-SIZE:10pt;FONT-WEIGHT:bold;TEXT-DECORATION:none;" HREF="./sched.asp?date=<%= SubtractOneMonth(dDate) %>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>">&lt;&lt;</A></TD>
		<TD width="70%" ALIGN="center"><label style="COLOR:#FFF;FONT-FAMILY:Verdana;FONT-SIZE:10pt;FONT-WEIGHT:bold;"><%= MonthName(Month(dDate)) & "  " & Year(dDate) %></label></TD>
		<TD width="15%" ALIGN="left"><A style="COLOR:#FFF;FONT-FAMILY:Verdana;FONT-SIZE:10pt;FONT-WEIGHT:bold;TEXT-DECORATION:none;" HREF="./sched.asp?date=<%= AddOneMonth(dDate) %>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>">&gt;&gt;</A></TD>
	</TR>
	</table>
	</td>
	</TR>
	<TR>
		<TD class=header ALIGN="center" width="14.25%">Sun</TD>
		<TD class=header ALIGN="center" width="14.25%">Mon</TD>
		<TD class=header ALIGN="center" width="14.25%">Tue</TD>
		<TD class=header ALIGN="center" width="14.25%">Wed</TD>
		<TD class=header ALIGN="center" width="14.25%">Thu</TD>
		<TD class=header ALIGN="center" width="14.25%">Fri</TD>
		<TD class=header ALIGN="center" width="14.25%">Sat</TD>
	</TR>
<%
If iDOW <> 1 Then
	Response.Write vbTab & "<TR>" & vbCrLf
	iPosition = 1
	Do While iPosition < iDOW
		Response.Write vbTab & vbTab & "<TD class=data>&nbsp;</TD>" & vbCrLf
		iPosition = iPosition + 1
	Loop
End If
iCurrent = 1
iPosition = iDOW

	strSQL= " SELECT cast(CONVERT(char, REC.datein, 101) AS smalldatetime) as DTE,MAX(Product.Cat) as Cat" & _
			" FROM REC (NOLOCK)" & _
			" INNER JOIN RECITEM(NOLOCK) ON REC.recid = RECITEM.recId" & _
			" INNER JOIN Product(NOLOCK) ON RECITEM.ProdID = Product.ProdID" & _
			" WHERE (REC.datein >= '" & Month(dDate) & "/1/" & Year(dDate) & "') "  & _
			" AND (REC.datein < '" & IIF(CInt(Month(dDate))=12,"1",CStr(CInt(Month(dDate)) + 1)) & "/1/" & IIF(CInt(Month(dDate))=12,CStr(CInt(Year(dDate)+1)),Year(dDate)) & "') " & _
			" AND (Product.Dept = 2 or Product.Cat = 7)  and (REC.LocationID="& LocationID & ") "&_
			" GROUP BY cast(CONVERT(char, REC.datein, 101) AS smalldatetime) ORDER BY dte "
'Response.Write strSQL
'Response.End

	Call DBOpenRecordset(dbMain,rsData,strSQL) 
        Do While iCurrent <= iDIM
	        If iPosition = 1 Then
		        Response.Write vbTab & "<tr>" & vbCrLf
	        End If
	        If Not rsData.EOF Then
		        If CDate(Month(rsData(0).Value) & "/" & Day(rsData(0).Value) & "/" & Year(rsData(0).Value)) = CDate(Month(dDate) & "/" & iCurrent & "/" & Year(dDate)) Then
			        IF rsData("cat").Value = 7 then
			        strImage= "&nbsp;<img width=""12"" height=""15"" style=""border-style:none"" SRC=""images\imgOS.gif"">"
			        ELSE
			        strImage= "&nbsp;<img width=""12"" height=""15"" style=""border-style:none"" SRC=""images\imgok.gif"">"
			        END IF
			        rsData.moveNext
		        ElseIf CDate(Month(rsData(0).Value) & "/" & Day(rsData(0).Value) & "/" & Year(rsData(0).Value)) > CDate(Month(dDate) & "/" & iCurrent & "/" & Year(dDate)) Then
			        strImage = "&nbsp;&nbsp;&nbsp;&nbsp;"
			        Do While Not rsData.EOF
				        If CDate(Month(rsData(0).Value) & "/" & Day(rsData(0).Value) & "/" & Year(rsData(0).Value)) < CDate(Month(dDate) & "/" & iCurrent & "/" & Year(dDate)) Then
					        rsData.MoveNext
				        ElseIf CDate(Month(rsData(0).Value) & "/" & Day(rsData(0).Value) & "/" & Year(rsData(0).Value)) = CDate(Month(dDate) & "/" & iCurrent & "/" & Year(dDate)) Then
					        IF rsData("cat").Value = 7   then
						        strImage= "&nbsp;<img width=""12"" height=""15"" style=""border-style:none"" SRC=""images\imgOS.gif"">"
					        ELSE
						        strImage= "&nbsp;<img width=""12"" height=""15"" style=""border-style:none"" SRC=""images\imgok.gif"">"
					        END IF
					        Exit Do
				        ElseIf CDate(Month(rsData(0).Value) & "/" & Day(rsData(0).Value) & "/" & Year(rsData(0).Value)) > CDate(Month(dDate) & "/" & iCurrent & "/" & Year(dDate)) Then
					        Exit Do
				        End If
			        Loop
		        End If
	        Else
		        strImage = "&nbsp;&nbsp;&nbsp;&nbsp;"
	        End If
	        If iCurrent = Day(dDate) Then
		        Response.Write vbTab & vbTab & "<TD height=""15"" align=center style=""COLOR:#FFF;FONT-FAMILY:Verdana;FONT-SIZE:7pt;"" valign=center class=seldata>" & iCurrent & strImage & "</TD>" & vbCrLf
	        Else
		        Response.Write vbTab & vbTab & "<TD height=""15"" align=center style=""COLOR:#FFF;FONT-FAMILY:Verdana;FONT-SIZE:7pt;"" valign=center class=data><A style=""text-decoration:none"" HREF=""./sched.asp?date=" & Month(dDate) & "/" & iCurrent & "/" & Year(dDate) & "&LocationID="& LocationID &"&LoginID="& LoginID &""">"  & iCurrent & strImage & "</A></TD>" & vbCrLf
	        End If
	
	        If iPosition = 7 Then
		        Response.Write vbTab & "</TR>" & vbCrLf
		        iPosition = 0
	        End If
	
	        iCurrent = iCurrent + 1
	        iPosition = iPosition + 1
        Loop

        If iPosition <> 1 Then
	        Do While iPosition <= 7
		        Response.Write vbTab & vbTab & "<TD height=""25"" class=data>&nbsp;</TD>" & vbCrLf
		        iPosition = iPosition + 1
	        Loop
	        Response.Write vbTab & "</TR>" & vbCrLf
        End If
%>
</TABLE>
</td>
<td>
<table WIDTH=370 border=1 height=100>
	<tr>
		<td align="Left" Width=60 ><label class="control">&nbsp;</label></td>
		<td align="Left" Width=20 ><label class="control">#1</label></td>
		<td align="Left" Width=20 ><label class="control">#2</label></td>
		<td align="Left" Width=20 ><label class="control">#3</label></td>
		<td align="Left" Width=60 ><label class="control">&nbsp;</label></td>
		<td align="Left" Width=20 ><label class="control">#1</label></td>
		<td align="Left" Width=20 ><label class="control">#2</label></td>
		<td align="Left" Width=20 ><label class="control">#3</label></td>
		<td align="Left" Width=60 ><label class="control">&nbsp;</label></td>
		<td align="Left" Width=20 ><label class="control">#1</label></td>
		<td align="Left" Width=20 ><label class="control">#2</label></td>
		<td align="Left" Width=20 ><label class="control">#3</label></td>
	</tr>
	<tr>
		<td align="Left" Width=60 ><label class="control">7:00</label></td>
		<% IF intT1B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','7:00:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','7:00:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT1B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','7:00:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','7:00:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT1B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','7:00:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','7:00:00 AM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">11:00</label></td>
		<% IF intT9B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','11:00:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','11:00:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT9B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','11:00:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','11:00:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT9B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','11:00:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','11:00:00 AM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">3:00</label></td>
		<% IF intT17B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','3:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','3:00:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT17B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','3:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','3:00:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT17B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','3:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','3:00:00 PM')">OK</label></td>
		<% END IF %>
	</tr>
	<tr>
		<td align="Left" Width=60 ><label class="control">7:30</label></td>
		<% IF intT2B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','7:30:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','7:30:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT2B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','7:30:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','7:30:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT2B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','7:30:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','7:30:00 AM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">11:30</label></td>
		<% IF intT10B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','11:30:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','11:30:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT10B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','11:30:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','11:30:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT10B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','11:30:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','11:30:00 AM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">3:30</label></td>
		<% IF intT18B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','3:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','3:30:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT18B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','3:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','3:30:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT18B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','3:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','3:30:00 PM')">OK</label></td>
		<% END IF %>
	</tr>
	<tr>
		<td align="Left" Width=60 ><label class="control">8:00</label></td>
		<% IF intT3B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','8:00:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','8:00:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT3B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','8:00:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','8:00:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT3B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','8:00:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','8:00:00 AM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">12:00</label></td>
		<% IF intT11B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','12:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','12:00:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT11B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','12:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','12:00:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT11B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','12:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','12:00:00 PM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">4:00</label></td>
		<% IF intT19B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','4:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','4:00:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT19B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','4:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','4:00:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT19B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','4:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','4:00:00 PM')">OK</label></td>
		<% END IF %>
	</tr>
	<tr>
		<td align="Left" Width=60 ><label class="control">8:30</label></td>
		<% IF intT4B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','8:30:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','8:30:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT4B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','8:30:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','8:30:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT4B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','8:30:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','8:30:00 AM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">12:30</label></td>
		<% IF intT12B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','1:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','1:30:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT12B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','1:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','1:30:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT12B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','1:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','1:30:00 PM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">4:30</label></td>
		<% IF intT20B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','4:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','4:30:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT20B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','4:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','4:30:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT20B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','4:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','4:30:00 PM')">OK</label></td>
		<% END IF %>
	</tr>
	<tr>
		<td align="Left" Width=60 ><label class="control">9:00</label></td>
		<% IF intT5B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','9:00:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','9:00:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT5B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','9:00:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','9:00:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT5B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','9:00:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','9:00:00 AM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">1:00</label></td>
		<% IF intT13B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','1:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','1:00:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT13B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','1:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','1:00:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT13B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','1:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','1:00:00 PM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">5:00</label></td>
		<% IF intT21B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','5:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','5:00:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT21B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','5:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','5:00:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT21B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','5:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','5:00:00 PM')">OK</label></td>
		<% END IF %>
	</tr>
	<tr>
		<td align="Left" Width=60 ><label class="control">9:30</label></td>
		<% IF intT6B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','9:30:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','9:30:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT6B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','9:30:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','9:30:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT6B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','9:30:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','9:30:00 AM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">1:30</label></td>
		<% IF intT14B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','1:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','1:30:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT14B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','1:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','1:30:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT14B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','1:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','1:30:00 PM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">5:30</label></td>
		<% IF intT22B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','5:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','5:30:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT22B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','5:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','5:30:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT22B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','5:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','5:30:00 PM')">OK</label></td>
		<% END IF %>
	</tr>
	<tr>
		<td align="Left" Width=60 ><label class="control">10:00</label></td>
		<% IF intT7B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','10:00:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','10:00:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT7B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','10:00:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','10:00:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT7B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','10:00:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','10:00:00 AM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">2:00</label></td>
		<% IF intT15B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','2:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','2:00:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT15B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','2:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','2:00:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT15B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','2:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','2:00:00 PM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">6:00</label></td>
		<% IF intT23B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','6:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','6:00:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT23B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','6:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','6:00:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT23B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','6:00:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','6:00:00 PM')">OK</label></td>
		<% END IF %>
	</tr>
	<tr>
		<td align="Left" Width=60 ><label class="control">10:30</label></td>
		<% IF intT8B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','10:30:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','10:30:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT8B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','10:30:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','10:30:00 AM')">OK</label></td>
		<% END IF %>
		<% IF intT8B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','10:30:00 AM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','10:30:00 AM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">2:30</label></td>
		<% IF intT16B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','2:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','2:30:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT16B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','2:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','2:30:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT16B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','2:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','2:30:00 PM')">OK</label></td>
		<% END IF %>
		<td align="Left" Width=60 ><label class="control">6:30</label></td>
		<% IF intT24B1 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','6:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('4','6:30:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT24B2 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','6:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('5','6:30:00 PM')">OK</label></td>
		<% END IF %>
		<% IF intT24B3 then %>
			<td class="statusred" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','6:30:00 PM')">OK</label></td>
		<% Else %>
			<td class="statusgreen" align="Left" Width=20 ><label style="cursor:hand" OnClick="Call ShowDlgBox2('6','6:30:00 PM')">OK</label></td>
		<% END IF %>
	</tr>
</table>
</td>
	</tr>
</table>
    </form>
    <table class="tblcaption" cellspacing="0" cellpadding="0" width="768">
        <tr>
            <td align="left" class="tdcaption" background="images/header.jpg" width="300">Detail Schedule for <%=dDate%></td>
            <td align="right">
               

            </td>
        </tr>
    </table>
    <table class="tblcaption" border="1" cellspacing="0" cellpadding="0" width="768">
        <tr>
            <td>
                <iframe name="schedfra1" src="admLoading.asp" scrolling="no" height="250px" width="250" frameborder="0"></iframe>
            </td>
            <td>
                <iframe name="schedfra2" src="admLoading.asp" scrolling="no" height="250px" width="250" frameborder="0"></iframe>
            </td>
            <td>
                <iframe name="schedfra3" src="admLoading.asp" scrolling="no" height="250px" width="250" frameborder="0"></iframe>
            </td>
        </tr>
    </table>
    </center>

</body>
</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>

<script language="VBScript">
Option Explicit
Sub Window_Onload()
	schedfra1.location.href = "schedfra1.asp?dtDate="& form1.dDate.value &"&LocationID=" & form1.LocationID.value &"&LoginID=" & form1.LoginID.value
	schedfra2.location.href = "schedfra2.asp?dtDate="& form1.dDate.value &"&LocationID=" & form1.LocationID.value &"&LoginID=" & form1.LoginID.value
	schedfra3.location.href = "schedfra3.asp?dtDate="& form1.dDate.value &"&LocationID=" & form1.LocationID.value &"&LoginID=" & form1.LoginID.value
End Sub 


Sub ShowDlgBox2(intLine,inttime)
	Dim strDlg, arrDlg
	strDlg = ShowModalDialog("NewDetailDlg.asp?intrecid=0&dtDate="& form1.dDate.value &" "&inttime &"&intLine="&intLine  &"&LocationID=" & form1.LocationID.value &"&LoginID=" & form1.LoginID.value ,,"center:1;dialogwidth:950px;dialogheight:750px;")
	form1.submit
End Sub


</script>
<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************

' ***Begin Function Declaration***
Function GetDaysInMonth(iMonth, iYear)
	Select Case iMonth
		Case 1, 3, 5, 7, 8, 10, 12
			GetDaysInMonth = 31
		Case 4, 6, 9, 11
			GetDaysInMonth = 30
		Case 2
			If IsDate("February 29, " & iYear) Then
				GetDaysInMonth = 29
			Else
				GetDaysInMonth = 28
			End If
	End Select
End Function

Function GetWeekdayMonthStartsOn(dAnyDayInTheMonth)
	Dim dTemp
	dTemp = DateAdd("d", -(Day(dAnyDayInTheMonth) - 1), dAnyDayInTheMonth)
	GetWeekdayMonthStartsOn = WeekDay(dTemp)
End Function

Function SubtractOneMonth(dDate)
	SubtractOneMonth = DateAdd("m", -1, dDate)
End Function

Function AddOneMonth(dDate)
	AddOneMonth = DateAdd("m", 1, dDate)
End Function

Function TimeCheck(TimeBay,timein,timeout,thour)
	Dim H_in,H_out,M_in,M_out,Tstart,Tend
	H_in = datepart("h",timein)
	H_out = datepart("h",timeout)
	M_in = datepart("n",timein)
	M_out = datepart("n",timeout)
	Tstart = cdbl(cstr(H_in)&right("00"&cstr(M_in*100/60),2)) 
	Tend = cdbl(cstr(H_out)&right("00"&cstr(M_out*100/60),2)) 
	IF not TimeBay Then
		IF cdbl(thour) >= Tstart and cdbl(thour) <= Tend then
			TimeBay = True
		END IF
	END IF
IF TimeBay then
	TimeCheck = true
ELSE
	TimeCheck = false
END IF

End Function
' ***End Function Declaration***
%>