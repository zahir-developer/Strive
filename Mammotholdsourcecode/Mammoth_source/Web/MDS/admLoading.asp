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

<%

'********************************************************************
' Global Variables
'********************************************************************

Const PAGE_NAME = ""


'********************************************************************
' Main
'********************************************************************
Main

'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
Sub Main

Dim strMode
strMode = Request("Mode")
If UCase(strMode) = "WIZ" then strMode = "background-color:#FFC"

'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">

Option Explicit
	Dim Timer,intDot, intPDot
	intDot=0
	intPDot=0
	Sub AnimateDots
		'MsgBox("hey")
		If intPDot-2 >-1 then
			Document.All.Item("Dot" & intPDot-2).ClassName = "DotOff"	
		End If
		
		If intPDot-1 >-1 then
			Document.All.Item("Dot" & intPDot-1).ClassName = "DotDuller"	
		End If
		
		Document.All.Item("Dot" & intPDot).ClassName = "DotDull"	
		Document.All.Item("Dot" & intDot).ClassName = "DotOn"		
		
		intPDot = intDot
		intDot = intDot + 1
		
		If IntDot > 4 Then
			IntDot = 0
		End If
		timer = Window.setTimeout("AnimateDots", 100)
	End Sub

Sub Window_OnUnload

	Window.clearTimeout(Timer)

End Sub 
Sub Window_OnLoad
	AnimateDots
End Sub 

</script>
<style>
.HeaderText
{
font-Family:Verdana;
font-size:18pt;
color:#036;

}
.DotOff
{
font-Family:Webdings;
font-size:8pt;
color:#FFF;
//color:#9CC; 

}
.DotDullest
{
font-Family:Webdings;
font-size:8pt;
color:#0CF;

}
.DotDuller
{
font-Family:Webdings;
font-size:8pt;
color:#09C;

}
.DotDull
{
font-Family:Webdings;
font-size:8pt;
color:#069;

}
.DotOn
{
font-Family:Webdings;
font-size:8pt;
color:#036;

}
</style>
   
<html>
<head>
<title><%= PAGE_NAME %></title>
<link rel="stylesheet" href="main.css" type="text/css">
</head>

<body class="pgBody" style="cursor:wait;<%=strMode%>">
<table border="0" width="100%" height="100%"><tr><td valign="center" align="center">
<label style="color:#066;font-family:verdana;font-size:14pt">Loading</label>
<span ID="DOT0" class="DotOff">=</span>
<span ID="DOT1" class="DotOff">=</span>
<label ID="DOT2" class="DotOff">=</label>
<label ID="DOT3" style="display:none"class="DotOff">=</label>
<label ID="DOT4" style="display:none"class="DotOff">=</label> 
</td></tr>
</table>
</body>
</html>
<%
End Sub

'********************************************************************
' Server-Side Functions
'********************************************************************


%>
