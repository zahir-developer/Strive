<%
'********************************************************************
'This checks to see if the User has Rights to access the current page
'********************************************************************
Dim gStrTitle '//global variable for page title
Sub CheckAccess(db)
Dim strSQL, RS, iURLID, strURL,iProfileID
iProfileID = Session("ProfileID")
If ISNULL(iProfileID) or Trim(iProfileID)= "" Then
	response.clear
	Response.Redirect "admSessionExpired.asp"
End IF
Session("LastCheckAccessTime") = Now()
Call GetCurURL(strURL)
If iProfileID <> 0 Then
		strSQL = "SELECT	admURL.* ,admProfileDef.*, admURLTag.*, admURL.Description AS PageTitle " & _
				" FROM		admURL , admURLTag , admProfiledef  " & _
				" WHERE		admURL.URL = '" & Trim(strURL) & "'" & _
				" AND		admURL.URLID=admURLTag.URLID " & _
				" AND		ProfileID=" & iProfileID & _
				" AND		admProfileDef.TagID=admURLTag.TagID "
				If DBOpenRecordset(db,rs,strSQL) Then
					If RS.EOF Then
						Response.Redirect "admAccessViolation.asp?UserName=" & Server.URLEncode(Session("UserName")) & "&URL=" & Trim(strUrl)
					Else
						gstrTitle = RS("PageTitle")
						Exit Sub
					End IF
				End If
Else
	strSQL = "SELECT	admURL.*" & _
			" FROM		admURL" & _
			" WHERE		admURL.URL= '" & Trim(strURL) & "'"
			If DBOpenRecordset(db,rs,strSQL) Then
				If Not(RS.EOF) Then
					gstrTitle = RS("Description")
				End If
			End If
End If
End Sub

'***************************************************
'This builds the Client Side Script for Showing Tags
'***************************************************
Function CheckRights(db, iCount)
Dim strSQL, RS, strURL, temp, intCounter
Call CheckAccess(db)
Call GetCurURL(strURL)
If Session("ProfileID") <> 0 Then
	strSQL = "SELECT admURL.* ,admProfileDef.*, admURLTag.* " & _
	" FROM admURL, admURLTag, admProfiledef " & _
	" WHERE admURL.URL='" & Trim(strURL) & "'" & _
	" AND admURL.URLID=admURLTag.URLID " & _
	" AND ProfileID=" & Session("ProfileID") & _
	" AND admProfileDef.TagID=admURLTag.TagID "
Else
	strSQL = "SELECT admURL.*, admURLTag.*" & _
	" FROM admURL, admURLTag" & _
	" WHERE admURL.URL='" & Trim(strURL) & "'" & _
	" AND admURL.URLID=admURLTag.URLID "
End If
	If DBOpenRecordset(db,rs,strSQL) Then
		Do While Not RS.EOF
			If UCASE(RS("TagName")) <> "_BODY" Then
				intCounter = 1
				Do While intCounter <= iCount
					temp= vbCRLF & "If UCASE(objT.GetAttribute(""" & "Name" & """)) =""" & UCASE(RS("TagName")) &  intCounter & """ Then " & vbCRLF & _
								" Document.All(""" & RS("TagName") & intCounter & """).style.Display=""BLOCK""" & vbCRLF & _
								" End IF " & vbCrLf 
					CheckRights = CheckRights + temp
					intCounter = intCounter + 1
				Loop
			End If
			RS.MoveNext
		Loop
	End If
Response.write "Dim objT" & VBCRLF
Response.Write " For Each objT in Document.All.Tags(""" & "a" & """) " & VBCRLF & CheckRights & VBCRLF & " Next " & VBCRLF
End Function

'***************************************************
'This gets the URL of current page
'***************************************************
Sub GetCurURL(strURL)
	strURL = Request.ServerVariables("URL")
	If Instr(strURL, "/") Then
		strURL = Mid(strURL, InstrRev(strURL, "/") +1)
		If Instr(strURL, "?") Then
			strURL = Left(strURL, Instr(strURL, "?") -1)
		End If
	Else
		If Instr(strURL, "?") Then
			strURL = Left(strURL, Instr(strURL, "?") -1)
		End If
	End If
End Sub

%>
