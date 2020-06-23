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

dim dbMain,intuserid,intCstat,intCType,strCType,LocationID

Set dbMain =  OpenConnection

intuserid = request("userid")
intCstat = request("Cstat")
intCType = request("CType")
LocationID = Request("LocationID")


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
<form method="POST" name="frmMain" action="TimeSetfra.asp"> 
<Input type="hidden" name="LocationID" value="<%=LocationID%>" />
<input type=hidden name=intuserid value="<%=intuserid%>">
<input type=hidden name=intCstat value="<%=intCstat%>">
<input type=hidden name=intCType value="<%=intCType%>">
<div align=center>
<table>
		<tr>
			<td align=right valign="center">&nbsp;</td>
		</tr>
		<tr>
			<td align=right valign="center">&nbsp;</td>
		</tr>
	<tr>
</table>
			<% IF intCstat = 0 THEN %>
<table>
		<tr>

		<td align="right" class="control" valign="center" style="font:bold 28px arial" nowrap>As:&nbsp;&nbsp;&nbsp;</td>
		<td align="left" class="control" valign="center" nowrap> 
		<Select name="cboCtype" tabindex=1 style="font:bold 28px arial" >
			<%Call LoadList(dbMain,6, intCtype)%>		
		</select>
		</td>

		<td align=right valign="center">&nbsp;&nbsp;&nbsp;			
		<button  name="btnSetTime" align="right" style="height:60;width:120;font:bold 18px arial" >Clock In</button>
		</td>

	</tr>
</table>
			<% ELSE %>
<table>
	<tr>
		<td align=right valign="center">			
			<button  name="btnSetTime" align="right" style="height:60;width:120;font:bold 18px arial" >Clock Out</button>
		</td>
	</tr>
</table>
			<% End If %>
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
<script language="VBSCRIPT">
Option Explicit

Sub Window_OnLoad()
End Sub

Sub btnSetTime_OnClick()
	IF frmMain.intCstat.value = 0 THEN
	parent.frmMain.intCtype.value=frmMain.cboCtype.value
	ELSE
	parent.frmMain.intCtype.value=frmMain.intCtype.value
	END IF
	parent.frmMain.FormAction.value="btnSetTime"
	parent.frmMain.submit()
End Sub

</script>

<%
	Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
 

