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
<!--#include file="incCommon.asp"-->
<%
Main
Sub Main
	Dim dbMain, intuserID,strSQL, RS,cnt,dtCdatetime,intAct,strcardname,blsave,intClockID,txtRptDate,LocationID,LoginID
	Dim dtTime
	Set dbMain =  OpenConnection

	intuserID = Request("intuserID")
	LocationID = Request("LocationID")
	LoginID = Request("LoginID")
	intClockID = Request("intClockID")
	dtCdatetime = Request("dtCdatetime")
	txtRptDate = Request("txtRptDate")

   IF  Request("intpunch") = 1 or Request("intpunch") = 3 or Request("intpunch") = 5 or Request("intpunch") = 7 then
        intAct = 0
    ELSE
        intAct = 1
    END IF


    strSQL =" SELECT FirstName + ' ' + LastName AS cardname"&_
        " FROM LM_Users"&_
        " WHERE LM_Users.userid ="& intuserID &" and LocationID=" & LocationID
    IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
        IF NOT rs.EOF then
	        strcardname = rs("cardname")
            IF isdate(dtCdatetime) then
                dtTime = formatDatetime(dtCdatetime,4)
            ELSE
                dtTime  = formatDatetime(date(),4)
            END IF
        END IF
    END IF
'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<link rel="stylesheet" href="main.css" type="text/css">
<Title>EOD Time Edit</title>
</head>
<body class="pgbody">
<form name="frmMain" method="post" >
<input type=hidden name=intClockID value="<%=intClockID%>">
<input type=hidden name=LocationID value="<%=LocationID%>">
<input type=hidden name=LoginID value="<%=LoginID%>">
<input type=hidden name=intuserID value="<%=intuserID%>">
<input type=hidden name=dtCdatetime value="<%=dtCdatetime%>">
<input type=hidden name=txtRptDate value="<%=txtRptDate%>">
<input type=hidden name=intAct value="<%=intAct%>">
<input type=hidden name=FormAction value=>

<div style="text-align:center">

<br />
<table border="0" width="100%" cellspacing="0" cellpadding="4">
	<tr>
         <td class="Header" align="center" ><%=strcardname%></td>
    </tr>
</table>
<br />
<table>
	<tr>
         <td class="control" align="right" >Punch:</td>
        <% IF intAct = 0 then %>
         <td class="control" align="left" ><b>IN</b></td>
        <% ELSE %>
         <td class="control" align="left" ><b>OUT</b></td>
        <% End IF %>

    </tr>
</table>
<br />
<table>
	<tr>
         <td class="control" align="right" >Time:</td>
         <td class="data" align="left" ><Input  name=dtTime size="5" datatype="time"  value="<%=dtTime%>"></td>
    </tr>
</table>
<br />

<table>
   <tr>
      <td align="center" colspan="3">
	  <button name="btnSave" class="button" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" style="width:75">Save</button>&nbsp;&nbsp;&nbsp;
        <% IF intClockID > 0 then %>
	  <button name="btnRemove" class="button" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" style="width:75">Remove</button>&nbsp;&nbsp;&nbsp;
        <% END IF %>
	  <button name="btnCancel" class="button" tabindex=16 onmouseover="ButtonHigh()" onmouseout="ButtonLow()" style="width:75">Cancel</button>
    </td>
	</tr>		
</table>
</div>
</form>
</body>
</html>
<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">
Option Explicit

Sub Window_Onload()
End Sub


Sub btnCancel_OnClick()
	Window.close
End Sub

Sub btnRemove_OnClick()
	Dim strEOD,Answer
	Answer = MsgBox("WARNING! WARNING! WARNING! WARNING!" & chr(13) & chr(13) &  "Are you sure you want to remove this clock punch ?" & chr(13) & chr(13) & "WARNING! WARNING! WARNING! WARNING!",276,"Confirm Delete")
	If Answer = 6 then
	    strEOD = ShowModalDialog("RptEODTimeEditDel2.asp?intclockID=" & document.all("intClockID").value &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,,"center:1;dialogleft:210px;dialogtop:150px; dialogwidth:450px;dialogheight:125px;")
	    Window.close
	Else
		Exit Sub
	End if
End Sub


Sub btnSave_OnClick()
	Dim validmsg,inmin,outmin,strEOD
	validmsg  = ""

	IF len(document.all("dtTime").value)>0 then
		IF len(document.all("dtTime").value) < 5 or instr(3,document.all("dtTime").value,":")=0 then
			msgbox "Time format incorrect HH:MM"
				document.all("dtTime").innertext = ""
				document.all("dtTime").value =""
			exit sub
		END IF
		IF  int(left(ltrim(document.all("dtTime").value),instr(1,ltrim(document.all("dtTime").value),":")-1)) < 5 then
			validmsg  = "In time is before 5 AM"
		END IF
	ELSE
		validmsg  = "Please Enter Correct Time"
	END IF

	IF len(validmsg) = 0 then
	    strEOD = ShowModalDialog("RptEODTimeEditAdd2.asp?intclockID=" & document.all("intClockID").value &"&intuserID=" & document.all("intuserID").value &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value  &"&dtTime=" & document.all("dtTime").value &"&intAct=" & document.all("intAct").value &"&txtRptDate=" & document.all("txtRptDate").value ,,"center:1;dialogleft:210px;dialogtop:150px; dialogwidth:450px;dialogheight:125px;")
	    Window.close
	ELSE
		msgbox validmsg
	END IF
End Sub

</script>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
Sub SaveData(dbMain)
	Dim strSQL,rsData,MaxSQL,intClockID
    dim varDate,varTime,intAct

    varDate = formatdatetime(Request("txtRptDate"),2)
    varTime = Request("dtTime")
    IF  Request("intpunch") = 1 or Request("intpunch") = 3 or Request("intpunch") = 5 or Request("intpunch") = 7 then
        intAct = 0
    ELSE
        intAct = 1
    END IF


	IF ISNULL(Request("intClockID")) or LEN(Request("intClockID"))=0 or Request("intClockID")=0 then
		MaxSQL = " Select ISNULL(Max(ClockID),0)+1 from timeclock (NOLOCK) where LocationID=" &  Request("LocationID")
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			intClockID=rsData(0)
		End if
		Set rsData = Nothing
		strSQL=" Insert into timeclock(ClockID,UserID,LocationID,Cdatetime,CAction,CType,EditBy,EditDate,Paid)Values(" & _
					intClockID & ", " & _
					Request("intuserID") & ", " & _
					Request("LocationID") & ", " & _
					"'" & varDate &" "& varTime  & "', " & _
					intAct & ", " & _
					"1, " & _
					Request("LoginID") &", " & _
					"'" & Date() & "',0)"
		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
	ELSE
		strSQL= " Update timeclock SET "&_
			" Cdatetime = '"& replace(varDate,"'","") &" "& varTime &"',"&_
			" CAction = "& intAct &","&_
			" Editby = "& Request("LoginID") &","&_
			" Editdate = '"& Now() & "'"&_
			" Where ClockID = " & Request("intClockID") &" and LocationId=" &  Request("LocationID")
		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
	END IF
END SUB


Function IIF(var1,var2,var3)
	IF var1 then
		IIF = var2
	ELSE
		IIF = var3
	END IF
End Function
%>
