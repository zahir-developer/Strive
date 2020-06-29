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
Dim strTargetFile,fso,TargetFile,LocationID
	strTargetFile=Request("TargetFile")
    LocationID = request("LocationID")

	If Len(Trim(strTargetFile)) > 0 Then
		Set fso = CreateObject("Scripting.FileSystemObject")
		'Response.Write server.mappath("/mammoth") & "\" & Replace(Request("TargetFile"),"/","\")
		Set TargetFile = fso.GetFile(server.mappath("/mds02") & "\" & Replace(Request("TargetFile"),"/","\"))
		Response.Redirect Request("TargetFile")
	End If	


'********************************************************************
'HTML
'********************************************************************
%>
<html>
<head>
<link rel="stylesheet" href="main.css" type="text/css">
<title></title>
</head>
<body class=pgframe>
<input type="hidden" name="TargetFile" value="<%=TargetFile%>">
    <input type="hidden" name="LocationID" value="<%=LocationID%>" />
<table cellspacing="0" width="752" class="Data">
	<tr>
    <td class="Header" align="center" width="42">Doc #</td>
    <td class="Header" align="center" width="150">File Name</td>
    <td class="Header" align="center" width="100" >Size</td>
    <td class="Header" align="center" width="100" >Date Created</td>
    <td class="Header" align="center" width="100" >Last Modified</td>
    <td class="Header" align="center" width="150" >Type</td>
    </tr>
<%=DoDataRow()%>

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
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
Dim ExtraDir,fso,fil,ts,strSQL,strTargetFile,strPath,intUserID,Directory,obj,LocationID
Dim htmlTable,intCount
	intUserID=Request("intUserID")
        LocationID = request("LocationID")

	strPath = Request.ServerVariables("SCRIPT_NAME")
	strPath = Left(strPath,InstrRev(strPath,"/") - 1)
	strPath = Server.MapPath(strPath)
	ExtraDir = strPath & "\docs\" & CSTR(LocationID)&"\" & CSTR(intUserID)&"\"

	Set fso = Server.CreateObject("Scripting.FileSystemObject")
	intCount = 0
	On Error Resume Next
	Set Directory = fso.GetFolder(ExtraDir)

	If Err.number > 0 Then
			htmlTable = htmlTable & "<tr><td bgColor=""#ffffcc"" class=data align=center colspan=""3"">No Documents are available.</td>"
			htmlTable = htmlTable & "</tr>"
	Else	
		For each obj in Directory.Files
			intCount = intCount + 1
			htmlTable = htmlTable & "<tr><td bgColor=""#ffffcc"" class=data align=right>" &  intCount & "&nbsp;</td>"
			htmlTable = htmlTable & "<td bgColor=""#ffffcc"" class=data align=left><a target=_new href='admDocListFra.asp?Targetfile=Docs/"& LocationID &"/"& intUserID &"/"& obj.Name & "'>" &  obj.Name & "</a></td>"
			htmlTable = htmlTable & "<td bgColor=""#ffffcc"" class=data align=left>" &  FormatNumber(obj.Size,0,False,False,True) & " bytes</td>"
			htmlTable = htmlTable & "<td bgColor=""#ffffcc"" class=data align=left>" &  obj.DateCreated & "</td>"
			htmlTable = htmlTable & "<td bgColor=""#ffffcc"" class=data align=left>" &  obj.DateLastModified & "</td>"
			htmlTable = htmlTable & "<td bgColor=""#ffffcc"" class=data align=left>" &  obj.Type & "</td>"
			htmlTable = htmlTable & "</tr>"
		Next
		If intCount = 0 Then
			htmlTable = htmlTable & "<tr><td bgColor=""#ffffcc"" class=data align=center colspan=""6"">No Documents are available.</td>"
			htmlTable = htmlTable & "</tr>"
		End If
	End If
	On Error Goto 0
	DoDataRow = htmlTable
End Function
%>
 

