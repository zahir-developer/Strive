<Script Language=VBScript>

Sub AlertUser(smsg)
		If smsg <> "" then 
			MsgBox smsg,48
		End If
End Sub

</script>

<%

Sub Window_OnloadHandler(dbMain)
	Dim  rs, strSQL
	Call CheckRights(dbMain)
		
		Response.write " Call Top.Frames(""Header"").ExecScript(""WriteTitle('" + ReplaceCR(gstrtitle) +  "')"",""VBScript"")" & VBCRLF
		Response.write " Call AlertUser(""" & ReplaceCR( gstrmsg) & """)  " & VBCRLF 
	
		Session("intWashes") = 0
		strSQL =" SELECT isnull(SUM(1),0) AS cnt FROM REC(NOLOCK)"&_
		" INNER JOIN RECITEM(NOLOCK) ON  REC.recid = RECITEM.recId"&_
		" INNER JOIN Product ON RECITEM.ProdID = Product.ProdID"&_
		" WHERE (DATEPART(Month, REC.CloseDte) = '" & Month(date()) & "')"&_
		" AND (DATEPART(Day, REC.CloseDte) = '" & day(date()) & "')"&_
		" AND (DATEPART(Year, REC.CloseDte) = '" & year(date()) & "')"&_
		" AND (REC.Status >= 70) AND (Product.cat = 1)"
		 
		IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
			IF NOT rs.EOF then
				Session("intWashes") = rs(0)	
			END IF
		END IF

		Response.write " Call Top.Frames(""Header"").ExecScript(""WriteWashesData('" + ReplaceCR(Session("intWashes")) +  "')"",""VBScript"")" & VBCRLF

		Session("intDetails") = 0
		strSQL =" SELECT isnull(SUM(1),0) AS cnt FROM REC(NOLOCK)"&_
		" INNER JOIN RECITEM(NOLOCK) ON  REC.recid = RECITEM.recId"&_
		" INNER JOIN Product ON RECITEM.ProdID = Product.ProdID"&_
		" WHERE (DATEPART(Month, REC.CloseDte) = '" & Month(date()) & "')"&_
		" AND (DATEPART(Day, REC.CloseDte) = '" & day(date()) & "')"&_
		" AND (DATEPART(Year, REC.CloseDte) = '" & year(date()) & "')"&_
		" AND (REC.Status >= 70) AND (Product.cat = 2)"
		 
		IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
			IF NOT rs.EOF then
				Session("intDetails") = rs(0)	
			END IF
		END IF
		Response.write " Call Top.Frames(""Header"").ExecScript(""WriteDetailsData('" + ReplaceCR(Session("intDetails")) +  "')"",""VBScript"")" & VBCRLF

		Session("intWashers") = 0
		Session("intDetailers") = 0

		strSQL = "{call stp_Washers}"     
		Call DBExec(dbMain, strSQL)
		strSQL =" SELECT washers,detailers FROM stats(NOLOCK)"
		IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
			IF NOT rs.EOF then
				Session("intWashers") = rs("washers")	
				Session("intDetailers") = rs("detailers")	
			END IF
		END IF
		Response.write " Call Top.Frames(""Header"").ExecScript(""WriteWashersData('" + ReplaceCR(Session("intWashers")) +  "')"",""VBScript"")" & VBCRLF
		Response.write " Call Top.Frames(""Header"").ExecScript(""WriteDetailersData('" + ReplaceCR(Session("intDetailers")) +  "')"",""VBScript"")" & VBCRLF
	
End Sub


'***************************************************
'This builds the Client Side Script for Showing Tags
'***************************************************
Dim gstrTitle,dbmain
Function CheckRights(dbMain)
Dim strSQL, RS, strURL,temp,iURLID,iProfileID

'Response.Write strURL
'Call CheckAccess(dbMain)
iProfileID = Session("ProfileID")
iURLID = GetCurURL(strURL)
If ISNULL(iProfileID) or Trim(iProfileID)= "" Then
	response.clear
	Response.Redirect "admSessionExpired.asp"
End IF
Session("LastCheckAccessTime") = Now()
'Response.Write "LOOK!" & iprofileID
If  iProfileID <> 0 Then
		strSQL = "SELECT	LM_URL.* ,LM_ProfileDef.*, LM_URLTag.*, LM_URL.Description AS PageTitle " & _
				" FROM		LM_URL , LM_URLTag , LM_ProfileDef  " & _
				" WHERE		LM_URL.URLID =" & iURLID  & _
				" AND		LM_URL.URLID=LM_URLTag.URLID " & _
				" AND		ProfileID=" & iProfileID & _
				" AND		LM_ProfileDef.TagID=LM_URLTag.TagID "
				
				If DBOpenRecordset(dbMain,rs,strSQL) Then
					If RS.EOF Then
						Response.Redirect "admAccessViolation.asp?UserName=" & Server.URLEncode(Session("UserName")) & "&URL=" & Trim(strUrl)
					Else
						gstrTitle = RS("PageTitle")
				
					End IF
				End If
Else
	strSQL = "SELECT	LM_URL.* " & _
			" FROM		LM_URL" & _
			" WHERE		LM_URL.URLID="  & iURLID

			If DBOpenRecordset(dbMain,rs,strSQL) Then
				If Not(RS.EOF) Then
					gstrTitle = RS("Description")
				End If
			End If
End If
set rs = nothing

If iProfileID <> 0 Then
	strSQL = "  SELECT LM_URL.* ,LM_ProfileDef.*, LM_URLTag.* " & _
	"  FROM LM_URL, LM_URLTag, LM_ProfileDef  " & _
	"  WHERE LM_URL.URLID=" & iURLID & _
	"  AND LM_URL.URLID=LM_URLTag.URLID " & _
	"  AND ProfileID=" & Session("ProfileID") & _
	"  AND LM_ProfileDef.TagID=LM_URLTag.TagID "

Else
	strSQL = "SELECT LM_URL.*, LM_URLTag.*" & _
	" FROM LM_URL, LM_URLTag" & _
	" WHERE LM_URL.URLID=" & iURLID  & _
	" AND LM_URL.URLID=LM_URLTag.URLID "
	
End If

	If DBOpenRecordset(dbMain,rs,strSQL) Then
	
		Do While Not RS.EOF
		If UCASE(RS("TagName")) <> "_BODY" Then 
			temp= vbCRLF & "If UCASE(objT.GetAttribute(""" & "Name" & """)) =""" & UCASE(RS("TagName")) & """ Then " & vbCRLF & _
							" Document.All(""" & RS("TagName") & """).style.Display=""inline""" & vbCRLF & _
							" End IF " & vbCrLf 
			CheckRights = CheckRights + temp
		End If
		RS.MoveNext
		Loop
End If
Response.write "Dim objT" & VBCRLF
Response.Write " For Each objT in Document.All " & VBCRLF & CheckRights & VBCRLF & " Next " & VBCRLF 
End Function

'********************************************************************
'This checks to see if the User has Rights to access the current page
'********************************************************************

Sub CheckAccess(dbMain, iProfileID)
Dim strSQL, RS, iURLID, strURL



If ISNULL(iProfileID) or Trim(iProfileID)= "" Then
	Response.Redirect "admLoggedOut.asp"
End IF

If iProfileID <> 0 Then
	Call GetCurURL(strURL)



		strSQL = "SELECT	LM_URL.* ,LM_ProfileDef.*, LM_URLTag.* " & _
				" FROM		LM_URL (NOLOCK), LM_URLTag (NOLOCK), LM_Profiledef (NOLOCK) " & _
				" WHERE		UPPER(LM_URL.URL)='" & Trim(UCASE(strURL)) & "'" & _
				" AND		LM_URL.URLID=LM_URLTag.URLID " & _
				" AND		ProfileID=" & iProfileID & _
				" AND		LM_ProfileDef.TagID=LM_URLTag.TagID "
				If DBOpenRecordset(dbMain,rs,strSQL) Then
					If RS.EOF Then
						Response.Redirect "admAccessViolation.asp?UserName=" & Server.URLEncode(Session("UserName"))
					Else
						Exit Sub
					End IF
				End If
End If
End Sub

'***************************************************
'This gets the URL of current page
'***************************************************
Function GetCurURL(strURL)
Dim strSQL,rs,MyPos,db


Set db =  OpenConnection
	strURL = Request.ServerVariables("URL") 
	GetCurURL =-1
	If Instr(strURL, "/")   Then
		strURL = Mid(strURL, InstrRev(strURL, "/") +1)
	End If

	'strURL = strURL & "?" & Request.ServerVariables("Query_string")

	strSQL = " Select *  From LM_vuURL WHERE URL6  = '" +  Left(strURL ,6) +  "' Order By URL Desc"
	If dbOpenRecordSet(db,rs,strSQL) Then
			Do While Not rs.eof 
				
					MyPos = InStr(1,strURL,rs("URL"),1)
					If  int(MyPos) = 1 Then

					 GetCurURL = CLong(rs("URLID"))
						Exit Do
					Else
						rs.movenext
					End If
			Loop	
			rs.Close
	End If

End Function

Function ReplaceCR(str)
	Dim strTemp
	strTemp = str
	strTemp = Replace(strTemp,"""","""""")
	strTemp = Replace(strTemp,vbCRLF, """ & vbCRLF & """ )
	strTemp = Replace(strTemp,vbCR, """ & vbCR & """ )
	strTemp = Replace(strTemp,vbLF, """ & vbLF & """ )
	ReplaceCR = strTemp
End Function
%>
