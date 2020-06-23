<%
'   Author: MS
'												
'	Assumes:									
'		- The recordset is already created with an adOpenStatic cursDataor.
'		  Example:  Set rsData = Server.CreateObject("ADODB.Recordset") 
'			        rsData.Open strSQL, db, adOpenStatic
'		- The recordset is called rsData			
'---------------------------------------------------------------------------------
'Now supports multiple I frames on same parent page. Added 4/20/99 by Heath Parodi
'---------------------------------------------------------------------------------

Sub StartNav(rsData, strSQL, intNavPgSize, intNavPage)

	'*** Check to see if the intNavPgSize setting has been set.  If not then check to see if it's been passed from a previous
	'*** page in the request object.  If there's still no value there, then use 15 as the default number of records to display
	'*** on one page.
'	If Trim(intNavPgSize) = "" Or IsNull(intNavPgSize) Then 
'		If Trim(Request("intNavPgSize")) = "" Or IsNull(Request("intNavPgSize")) Then
'			intNavPgSize = 15
'		Else
'			intNavPgSize = CInt(Trim(Request("intNavPgSize")))
'		End If
'	End If

	'*** If the current page is not passed from the request object, then this is the firsDatat time that this recordset has 
	'***been displayed (therefore, display page one). 
	If Trim(Request("intNavPage")) = "" Or IsNull(Request("intNavPage")) Then 
		intNavPage = 1
	Else
		intNavPage = Trim(Request("intNavPage"))
	End If

	'*** Move to the right page
	IF not rsData.Eof Then
		rsData.AbsolutePage = Cint(intNavPage)
	End if

End Sub


Function NavData(db, strSQL, intNavPgSize, intNavPage, intNavNumRecs)
	Dim rsData	  '*** The recordset used to build the table.
	Dim strData   '*** The actual table rows html that is built and returned.
	Dim intCount  '*** Counter for the records being displayed.
	Const adOpenStatic = 3								  '*** ADO constant (must open static).
	intCount = 1												'*** Initialize the record counter variable.

	Set rsData = Server.CreateObject("ADODB.Recordset") 
	If strSQL="" then NavData=""

' Response.Write strsql

	rsData.Open strSQL, db, adOpenStatic

	rsData.PageSize = CInt(intNavPgSize)
	intNavNumRecs = rsData.RecordCount			'*** The number of records in the recordset.

	Call StartNav(rsData, strSQL, intNavPgSize, intNavPage)

	If rsData.EOF then
		strData = "<tr><td colspan=20 align=""center"" Class=""data"">No records were found.</td></tr>"
	End IF	

	Do While not rsData.EOF
		strData = strData & DoDataRow(db, rsData, intCount)		

		'*** Check to see if the record counter variable is greater than the page size.  If so, then exit loop.
		If CInt(intCount) >= CInt(intNavPgSize) Or rsData.EOF Then Exit Do

		'*** Increment the record counter variable
		intCount = intCount + 1
		
		rsData.MoveNext
	Loop

	NavData = strData

End Function


'*** Creates the navigation buttons for the bottom of your data table
Function NavFooter(strSQL, intNavPgSize, intNavPage, intNavNumRecs,strFrameName)
	Dim strFtr, strNavQueryString, strNavQString, intNavPgX, intNavPgNumsToShow, strNavPageFile, intNavPgCount
	Dim navx, tempnavx, intFirstPgNum,strQuery

	'*** Determine the total number of pages
	intNavPgCount = int(intNavNumRecs / intNavPgSize)
	If intNavNumRecs Mod intNavPgSize > 0 then intNavPgCount = intNavPgCount + 1
	If intNavPgCount = 0 then intNavPgCount = 1

	'*** Get the the URL of this page
	strNavPageFile = Request.ServerVariables("URL")

	'*** Trim off the domain name and/or subfoldersData, get only the file name.
	Do While Instr(strNavPageFile, "/") > 0
		strNavPageFile = Mid(strNavPageFile, Instr(strNavPageFile, "/") + 1, Len(strNavPageFile))
	Loop

	'*** Make a concatenated string of all the form variables.
'	For Each navx in Request.Form
'		tempnavx = tempnavx & "&" & navx & "=" & Server.URLEncode(Request.Form(navx))
'	Next

	'*** Add the form variables string to the end of the filename. (Make sure you 
	'*** don't have too many form variables because the query string has a character limit.)
'	If tempnavx <> "" Then
'		If Instr(strNavPageFile, "?") > 0 Then
'			strNavPageFile = strNavPageFile & tempnavx
'		Else
'			strNavPageFile = strNavPageFile & "?" & Mid(tempnavx, 2, Len(tempnavx))
'		End If
'	End If

	'*** Add the query string to the file name
	strQuery = Replace(CStr(Request.ServerVariables("QUERY_STRING")),"intNavPage","i")
	
	If strQuery <> "" Then
		If Instr(strNavPageFile, "?") > 0 Then
			strNavPageFile = strNavPageFile & "&" & strQuery
		Else
			strNavPageFile = strNavPageFile & "?" & strQuery
		End If
	End If
	If Instr(strNavPageFile, "?") = 0 Then
		strNavPageFile = strNavPageFile + "?"
	Else
		strNavPageFile = strNavPageFile + "&"
	End If

	'*** Add the strSQL request variable to the query string
	'strNavPageFile = strNavPageFile & "?strSQL=" & Server.URLencode(Request("strSQL"))

	'*** Add the intNavPgSize variable and value to the query string
'	If InStr(strNavPageFile, "?") > 0 Then
'		strNavPageFile = strNavPageFile & "&intNavPgSize=" & intNavPgSize
'	Else
'		strNavPageFile = strNavPageFile & "?intNavPgSize=" & intNavPgSize
'	End If

	'*** Show the record count
	strFtr = strFtr & "<table border=0 cellspacing=0 cellpadding=0 width=768><tr>"
	strFtr = strFtr & "<td class=header align="left">"
	strFtr = strFtr & "&nbsp;Count: " & intNavNumRecs

	'*** Display hyperlinks to pages if there's more than one page.
	strFtr = strFtr & "</td><td class=header align=center>"

	intNavPgNumsToShow = 10   '*** The number of page numbers to show
	intFirstPgNum = Cint((intNavPage - 5) / intNavPgNumsToShow) * 10 + 1
	If intFirstPgNum < 1 then intFirstPgNum = 1

	'*** Show the 'Prev'
	If intFirstPgNum = 1 Then
		strFtr = strFtr & "<a class=lnkDead href=""""#!"""">PREV</a>&nbsp;&nbsp;"
	Else
		strFtr = strFtr & "<a  class=lnkHeader target=" & strFrameName & " href=""""" & strNavPageFile & "intNavPage="  & intFirstPgNum - intNavPgNumsToShow & """"">PREV</a>&nbsp;&nbsp;"
	End If

	'*** List out all the pages.  Make every page a hyperlink except for the current page.
	intNavPgX = intFirstPgNum    '*** Page your showing in the loop show
	
	For intNavPgX = intFirstPgNum to (intFirstPgNum + (intNavPgNumsToShow-1))
		If cint(intNavPgX) = cint(intNavPage) Then
		
			strFtr = strFtr & "<a class=""""lnkSelect"""" href=""""#!"""">&nbsp;" & intNavPgX & "&nbsp;</a>"
		Elseif intNavPgX <= intNavPgCount then
			strFtr = strFtr & "<a  target=" & strFrameName & " class=lnkHeader href=""""" & strNavPageFile & "intNavPage="  & intNavPgX & """"">&nbsp;" & intNavPgX & "&nbsp;</A>"
		Else
			strFtr = strFtr & "<a class=lnkDead href=""""#!"""">&nbsp;" & intNavPgX & "&nbsp;</A>"
		End If
	Next

	'*** Show the 'Next'
	If intNavPgX > intNavPgCount then
	
		strFtr = strFtr & "<a class=lnkDead href=""""#!"""">&nbsp;&nbsp;NEXT</a>"
	Else
		strFtr = strFtr & "<a target=" & strFrameName & " class=lnkHeader href=""""" & strNavPageFile & "intNavPage="  & intFirstPgNum + intNavPgNumsToShow & """"">&nbsp;&nbsp;NEXT</a>"
	End If
	strFtr = strFtr & "</td>"

	'*** Show the page count
	strFtr = strFtr & "<td align=right class=header>Page " & intNavPage & " of <a target=" & strFrameName & " class=lnkHeader href=""""" & strNavPageFile & "intNavPage="  & intNavPgCount & """"">&nbsp;" & intNavPgCount & "&nbsp;</A></td>"

	strFtr = strFtr & "</tr>"
	strFtr = strFtr & "</table>"

	NavFooter = strFtr

End Function
%>
