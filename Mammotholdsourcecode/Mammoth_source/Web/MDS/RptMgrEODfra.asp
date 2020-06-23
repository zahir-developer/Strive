<%@ LANGUAGE="VBSCRIPT" %>
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
	Dim dbMain,txtRptDate,rsData,strSQL,rsData2,strSQL2,rsData3,strSQL3,rsData4,strSQL4,cnt,tcnt
	Dim dtCdatetime,strHours,cnt2,intCaction,strTotalHrs,strCounts,strTotalCnt,strTotalAct,strTotalComm
	dim strTotalPaid,intDrawer,intCnts,intTotalPay,strEditEOD,strHoursPerWeek
    dim strCdatetime,strCaction,Cdatetime1,Cdatetime2, Caction2,Cdatetime3, Caction3
    dim Cdatetime4,Cdatetime5,Cdatetime6,Cdatetime7, Cdatetime8
    dim intClockID1,intClockID2,intClockID3,intClockID4
    dim intClockID5,intClockID6,intClockID7,intClockID8,LocationID,LoginID



	Set dbMain = OpenConnection	
    LocationID = Request("LocationID")
        LoginID = Request("LoginID")

txtRptDate = Request("txtRptDate")
'********************************************************************
' HTML
'********************************************************************
%>
<head>
<title></title>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
</head>
<body class="pgBody">
<Input type="hidden" name="LocationID" value="<%=LocationID%>" />
<Input type="hidden" name="LoginID" value="<%=LoginID%>" />
<Input  name=txtRptDate type="hidden"  value="<%=txtRptDate%>" />
<div align="left">
<table  cellspacing="0" border="2" cellpadding="1" width="100%">
	<tr>
		<td colspan=5 align=right>&nbsp;</td>
		<td align=center width="6%"><label class=control>&nbsp;In&nbsp;</label></td>
		<td align=center width="6%"><label class=control>&nbsp;Out&nbsp;</label></td>
		<td align=center width="6%"><label class=control>&nbsp;In&nbsp;</label></td>
		<td align=center width="6%"><label class=control>&nbsp;Out&nbsp;</label></td>
		<td align=center width="6%"><label class=control>&nbsp;In&nbsp;</label></td>
		<td align=center width="6%"><label class=control>&nbsp;Out&nbsp;</label></td>
		<td align=center width="6%"><label class=control>&nbsp;In&nbsp;</label></td>
		<td align=center width="6%"><label class=control>&nbsp;Out&nbsp;</label></td>
	</tr>
<%	strSQL=" SELECT DISTINCT TimeClock.UserID, LM_Users.LastName,LM_Users.FirstName + ' ' + LM_Users.LastName AS Employee"&_
	" FROM TimeClock (NOLOCK) INNER JOIN LM_Users(NOLOCK) ON TimeClock.UserID = LM_Users.UserID"&_
	" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate) &"')"&_
	" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate) &"')"&_
	" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate) &"')"&_
	" AND CAST(LM_Users.Salery AS Numeric) = 0 AND TimeClock.CType <> 9"&_ 
    " and LM_Users.LocationID="& LocationID &_
    " and TimeClock.LocationID="& LocationID &_
	" ORDER BY LM_Users.LastName"
	Cnt = 1
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		DO While NOT rsData.eof 
%>
	<tr>
		<%	strSQL2=" SELECT UserID, Cdatetime, Caction"&_
					" FROM TimeClock(NOLOCK)"&_
					" WHERE (DATEPART(Month, TimeClock.Cdatetime) = '"& Month(txtRptDate) &"')"&_
					" AND (DATEPART(Day, TimeClock.Cdatetime) = '"& Day(txtRptDate) &"')"&_
					" AND (DATEPART(Year, TimeClock.Cdatetime) = '"& Year(txtRptDate) &"')"&_
					" AND (UserID = "& rsData("UserID") &") AND TimeClock.CType <> 9 and TimeClock.LocationID="& LocationID &" ORDER BY Cdatetime, Caction"
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
			IF CNT2 = 3 then
				strHours = strHours + DateDiff("n",dtCdatetime,now())
			END IF
			IF CNT2 = 5 then
				strHours = strHours + DateDiff("n",dtCdatetime,now())
			END IF
			IF CNT2 = 7 then
				strHours = strHours + DateDiff("n",dtCdatetime,now())
			END IF
			strHours = round(strHours/60,2)
			strTotalHrs = strTotalHrs+strHours

		IF intCaction = 0 then%>
		<td align="right" width="3%"><label class=redcontrol><%=cnt%>&nbsp;</label></td>
		<td align="left" width="25%"><label class=redcontrol >&nbsp;<%=rsData("Employee")%>&nbsp;</label></td>
		<td align="right" width="6%"><label class=redcontrol><%=formatnumber(strHours,2)%>&nbsp;</label></td>
		<td align="left" width="3%"><label  class=redcontrol>On&nbsp;</label></td>
		<% ELSE %>
		<td align="right" width="3%"><label class=control><%=cnt%>&nbsp;</label></td>
		<td align="left" width="25%"><label class=control >&nbsp;<%=rsData("Employee")%>&nbsp;</label></td>
		<td align="right" width="9%"><label class=control><%=formatnumber(strHours,2)%>&nbsp;</label></td>
		<td align="left" width="3%"><label  class=control>Off&nbsp;</label></td>
		<% END IF %>
<!--Hours to date for week  sun - sat  -->
<%
strHoursPerWeek = 0    
strSQL3="{call stp_EODHoursPerWeek('"& txtRptDate &"',"& rsData("userID")& ","& LocationID & ")}"
			If DBOpenRecordset(dbMain,rsData3,strSQL3) Then
				IF NOT rsData3.eof then
				    strHoursPerWeek = rsData3(0)
				END IF

		    END IF

			If isnull(strHoursPerWeek) then
				strHoursPerWeek =-99.99
			end if

 %>
 

		<%IF formatnumber(strHoursPerWeek,2) > 40.00 THEN %>
		<td align="right" width="6%"><label class=cridicalcontrol><%=formatnumber(strHoursPerWeek,2)%>&nbsp;</label></td>
		<%ELSEIF formatnumber(strHoursPerWeek,2)  > 38.00  THEN %>
		<td align="right" width="6%"><label class=warncontrol><%=formatnumber(strHoursPerWeek,2)%>&nbsp;</label></td>
		<%ELSEIF formatnumber(strHoursPerWeek,2)  > 30.00 THEN %>
		<td align="right" width="6%"><label class=yelcontrol><%=formatnumber(strHoursPerWeek,2)%>&nbsp;</label></td>
		<%ELSE %>
		<td align="right" width="6%"><label class=control><%=formatnumber(strHoursPerWeek,2)%>&nbsp;</label></td>
		<% End IF %>

        <!-- Current day clock in/out -->
        <%  
        
        tcnt = 1
        intClockID1=0  
        intClockID2=0  
        intClockID3=0  
        intClockID4=0  
        intClockID5=0  
        intClockID6=0  
        intClockID7=0  
        intClockID8=0  
        strCaction = ""
        strCdatetime = ""
        Cdatetime1 = ""
        Cdatetime2 = ""
        Cdatetime3 = ""
        Cdatetime4 = ""
        Cdatetime5 = ""
        Cdatetime6 = ""
        Cdatetime7 = ""
        Cdatetime8 = ""

 
        strSQL4=" SELECT ClockID,Caction,Cdatetime FROM TimeClock with (nolock)"&_
                " WHERE (UserID = "& rsData("userID")& ") and (TimeClock.LocationID="& LocationID &") AND (YEAR(Cdatetime) = YEAR('"& txtRptDate &"')) AND (MONTH(Cdatetime) = MONTH('"& txtRptDate &"')) AND (DAY(Cdatetime) = DAY('"& txtRptDate &"'))"&_
                " ORDER BY Cdatetime"
			If DBOpenRecordset(dbMain,rsData4,strSQL4) Then
				DO While NOT rsData4.eof 
                    strCaction =  rsData4(1)
                    strCdatetime =  rsData4(2)

                    IF tcnt = 8 then
                        intClockID8 =  rsData4(0)
                       IF strCaction = 0 then
                            Cdatetime8 = "Error"
                        ELSE
                            Cdatetime8 = strCdatetime
                        END IF
                    End If
                    IF tcnt = 7 then
                       IF strCaction = 1 then
                            tcnt = 8
                            Cdatetime8 = strCdatetime
                            intClockID8 =  rsData4(0)
                      ELSE
                            Cdatetime7 = strCdatetime
                            intClockID7 =  rsData4(0)
                       END IF
                    End If
                    IF tcnt = 6 then
                      IF strCaction = 0 then
                            tcnt = 7
                            intClockID7 =  rsData4(0)
                            Cdatetime7 = strCdatetime
                        ELSE
                            Cdatetime6 = strCdatetime
                            intClockID6 =  rsData4(0)
                        END IF
                    End If
                    IF tcnt = 5 then
                        IF strCaction = 1 then
                            tcnt = 6
                            Cdatetime6 = strCdatetime
                            intClockID6 =  rsData4(0)
                        ELSE
                            Cdatetime5 = strCdatetime
                            intClockID5 =  rsData4(0)
                        END IF
                    End If
                    IF tcnt = 4 then
                        IF strCaction = 0 then
                            tcnt = 5
                            Cdatetime5 = strCdatetime
                            intClockID5 =  rsData4(0)
                        ELSE
                            Cdatetime4 = strCdatetime
                            intClockID4 =  rsData4(0)
                       END IF
                    End If
                    IF tcnt = 3 then
                        IF strCaction = 1 then
                            tcnt = 4
                            Cdatetime4 = strCdatetime
                             intClockID4 =  rsData4(0)
                        ELSE
                            Cdatetime3 = strCdatetime
                            intClockID3 =  rsData4(0)
                       END IF
                    End If
                    IF tcnt = 2 then
                        IF strCaction = 0 then
                            tcnt = 3
                            Cdatetime3 = strCdatetime
                            intClockID3 =  rsData4(0)
                       ELSE
                            Cdatetime2 = strCdatetime
                            intClockID2 =  rsData4(0)
                       END IF
                    End If
                   IF tcnt = 1 then
                        IF strCaction = 1 then
                            tcnt = 2
                            Cdatetime2 = strCdatetime
                            intClockID2 =  rsData4(0)
                       ELSE
                            Cdatetime1 = strCdatetime
                            intClockID1 =  rsData4(0)
                        END IF
                    End If
		        tcnt = tcnt+1
		        rsData4.MoveNext
				Loop
		    END IF
        %>

        <% IF isdate(Cdatetime1) then %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,<%=intClockID1%>,1,<%=Cdatetime1%>,<%=txtRptDate%>')"><label class="Input"><%=formatdatetime(Cdatetime1,4)%></label></td>
        <% ELSE %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,0,1,,<%=txtRptDate%>')" ><label class="Input">&nbsp;</label></td>
        <% End If %>
        <% IF isdate(Cdatetime2) then %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,<%=intClockID2%>,2,<%=Cdatetime2%>,<%=txtRptDate%>')" ><label class="Input"><%=formatdatetime(Cdatetime2,4)%></label></td>
        <% ELSE %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,0,2,,<%=txtRptDate%>')" ><label class="Input">&nbsp;</label></td>
        <% End If %>
        <% IF isdate(Cdatetime3) then %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,<%=intClockID3%>,3,<%=Cdatetime3%>,<%=txtRptDate%>')" ><label class="Input"><%=formatdatetime(Cdatetime3,4)%></label></td>
        <% ELSE %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,0,3,,<%=txtRptDate%>')" ><label class="Input">&nbsp;</label></td>
        <% End If %>
        <% IF isdate(Cdatetime4) then %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,<%=intClockID4%>,4,<%=Cdatetime4%>,<%=txtRptDate%>')" ><label class="Input"><%=formatdatetime(Cdatetime4,4)%></label></td>
        <% ELSE %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,0,4,,<%=txtRptDate%>')" ><label class="Input">&nbsp;</label></td>
        <% End If %>
        <% IF isdate(Cdatetime5) then %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,<%=intClockID5%>,5,<%=Cdatetime5%>,<%=txtRptDate%>')" ><label class="Input"><%=formatdatetime(Cdatetime5,4)%></label></td>
        <% ELSE %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,0,5,,<%=txtRptDate%>')" ><label class="Input">&nbsp;</label></td>
        <% End If %>
        <% IF isdate(Cdatetime6) then %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,<%=intClockID6%>,6,<%=Cdatetime6%>,<%=txtRptDate%>')" ><label class="Input"><%=formatdatetime(Cdatetime6,4)%></label></td>
        <% ELSE %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,0,6,,<%=txtRptDate%>')" ><label class="Input">&nbsp;</label></td>
        <% End If %>
        <% IF isdate(Cdatetime7) then %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,<%=intClockID7%>,7,<%=Cdatetime7%>,<%=txtRptDate%>')" ><label class="Input"><%=formatdatetime(Cdatetime7,4)%></label></td>
        <% ELSE %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,0,7,,<%=txtRptDate%>')" ><label class="Input">&nbsp;</label></td>
        <% End If %>
        <% IF isdate(Cdatetime8) then %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,<%=intClockID8%>,8,<%=Cdatetime8%>,<%=txtRptDate%>')" ><label class="Input"><%=formatdatetime(Cdatetime8,4)%></label></td>
        <% ELSE %>
		<td align=right width="6%" style="background-color:White;cursor:pointer" onclick="RptEODTimeEdit('<%=rsData("userID")%>,0,8,,<%=txtRptDate%>')" ><label class="Input">&nbsp;</label></td>
        <% End If %>



	</tr>
<%
			cnt = cnt+1
			rsData.MoveNext
		Loop
	END IF
	Set rsData = Nothing
%>
	<tr>
		<td colspan=2 align=right><label class=control>Total Hrs:&nbsp;</label></td>
		<td colspan=3><label align="right" class=control><%=formatnumber(strTotalHrs,2)%>&nbsp;</label></td>
		<td colspan=8 align=right>&nbsp;&nbsp;</td>
	</tr>

</table>
</div>
</body>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">
Option Explicit
Dim obj

Sub RptEODTimeEdit(var)
	Dim strEOD,intuserid,intpunch,arrvar,dtCdatetime,intClockID,txtRptDate
	arrvar=split(var,",")
	intuserid=arrvar(0)
	intClockID=arrvar(1)
	intpunch=arrvar(2)
	dtCdatetime=arrvar(3)
	txtRptDate=arrvar(4)
	window.event.cancelBubble = false

	strEOD = ShowModalDialog("RptEODTimeEditDlg2.asp?intuserID=" & intuserid &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value & "&intClockID=" & intClockID &"&dtCdatetime=" & dtCdatetime & "&intpunch=" & intpunch& "&txtRptDate=" & txtRptDate ,,"center:1;dialogleft:210px;dialogtop:150px; dialogwidth:450px;dialogheight:225px;")

    Parent.fraMain.location.href = "RptMgrEODFra.asp?txtRptDate="& document.all("txtRptDate").value &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value

END SUB


</script>

<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
