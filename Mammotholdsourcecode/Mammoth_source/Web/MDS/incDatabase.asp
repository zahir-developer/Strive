<%
Dim strResult
'Dim gStrTitle
Dim gstrMsg,gblnErrFlag

Function OpenConnection()

    'Response.Write(Application("ConnectionString"))
	Dim db,j,strTemp,sMsg
	On Error Resume Next
	Set db = CreateObject("ADODB.Connection")
	'Response.write("before open<br>")
	db.Open Application("ConnectionString")
   'Response.write("after open<br>")
	db.Errors.Clear
	Set OpenConnection = db
     'Response.Write("<br>working")
End Function


Sub CloseConnection(db)
	db.Close
	Set db = Nothing
End Sub

Function DBExec(db, sSQL)
	Dim J, strTemp
	If db.State = 0  Then
			strTemp = strTemp + "No connection was available to process your command." & vbCRLF 
			gstrmsg = strTemp & "SQL Statment:" & vbCRLF & sSQL & vbCRLF
			gblnErrflag = True
			DBExec = False
			Exit Function
	End If
	On error resume next
	db.Execute(sSQL)
	If db.Errors.count > 0 then
		for j = 1 to db.errors.count
			strTemp = strTemp + "An error has occurred." & vbCRLF & "Error Description: (" & _ 
					db.Errors(j-1).Number & ")" & vbCRLF &  db.Errors(j-1).Description & vbCRLF
			j = j + 1
		next
		db.Errors.Clear

		If not gblnErrFlag Then 'This allows for getting the first error on the page instead of the last
			gstrmsg = strTemp & "SQL Statment:" & vbCRLF & sSQL & vbCRLF
			gblnErrflag = True
		End If
		DBExec = False
	Else
		DBExec = True
	End If
End Function

Function DBOpenRecordset(db,rs,sSQL)
    'Response.write("dbopenrecordset")
	Dim j, sMsg, rsLocal,strTemp
	On error resume next
	If db.State = 0  Then
			strTemp = strTemp + "No connection was available to process your command." & vbCRLF 
			gstrmsg = strTemp & "SQL Statment:" & vbCRLF & sSQL & vbCRLF
			gblnErrflag = True
			DBopenRecordset = False
			Exit Function
	End If
	Set rsLocal = db.Execute(sSQL)
	If db.Errors.count > 0 then
		for j = 1 to db.errors.count
			sMsg = sMsg + "An error has occurred." & vbCRLF & " Error Description: (" & _ 
					 db.Errors(j-1).Number & ")" & vbCRLF & db.Errors(j-1).Description & vbCRLF 
			j = j + 1
		next
		db.Errors.Clear
		
		If not gblnErrFlag Then 'This allows for getting the first error on the page instead of the last
			gstrMsg = sMsg & "SQL Statment:" & vbCRLF & sSQL & vbCRLF
			gblnErrflag = True
		End If
		DBOpenRecordset = False
	Else
		Set rs = rsLocal
		DBOpenRecordset = True
	End If
End Function

Function DBOpenDynamicRecordset(db,rs,sSQL)
	Dim j, sMsg, rsLocal,strTemp
	If db.State = 0  Then
			strTemp = strTemp + "No connection was available to process your command." & vbCRLF 
			gstrmsg = strTemp & "SQL Statment:" & vbCRLF & sSQL & vbCRLF
			gblnErrflag = True
			DBOpenDynamicRecordset = False
			Exit Function
	End If
	On error resume next
	Set rsLocal = CreateObject("ADODB.RecordSet")
	rsLocal.Open sSQL,db,2

	If db.Errors.count > 0 then
		for j = 1 to db.errors.count
			sMsg = sMsg + "An error has occurred." & vbCRLF & " Error Description: (" & _ 
					 db.Errors(j-1).Number & ")" & vbCRLF & db.Errors(j-1).Description & vbCRLF & vbCRLF
			j = j + 1
		next
		db.Errors.Clear
		
		If not gblnErrFlag Then 'This allows for getting the first error on the page instead of the last
			gstrMsg = sMsg & "SQL Statment:" & vbCRLF & sSQL
			gblnErrflag = True
		End If
		
		DBOpenDynamicRecordset = False
	Else
		Set rs = rsLocal
		DBOpenDynamicRecordset = True
	End If
End Function

Function DBOpenStaticRecordset(db,rs,sSQL)

'Response.write(db.State)
	Dim j, sMsg, rsLocal,strTemp
	If db.State = 0  Then
			strTemp = strTemp + "No connection was available to process your command." & vbCRLF 
			gstrmsg = strTemp & "SQL Statment:" & vbCRLF & sSQL & vbCRLF
			gblnErrflag = True
			DBOpenStaticRecordset = False
			Exit Function
	End If
	On error resume next
	Set rsLocal = CreateObject("ADODB.RecordSet")
	rsLocal.Open sSQL,db,3

	If db.Errors.count > 0 then
		for j = 1 to db.errors.count
			sMsg = sMsg + "An error has occurred." & vbCRLF & " Error Description: (" & _ 
					 db.Errors(j-1).Number & ")" & vbCRLF & db.Errors(j-1).Description & vbCRLF & vbCRLF
			j = j + 1
		next
		db.Errors.Clear
		
		If not gblnErrFlag Then 'This allows for getting the first error on the page instead of the last
			gstrMsg = sMsg & "SQL Statment:" & vbCRLF & sSQL
			gblnErrflag = True
		End If
		
		DBOpenStaticRecordset = False
	Else
		Set rs = rsLocal
		DBOpenStaticRecordset = True
	End If
End Function

Function NullNum(var)
    If var <> "" Then
		NullNum = var
    Else
		NullNum = "NULL"
    End If
End Function
Function NullZero(var)
	If IsNull(var) then
		NullZero = 0.0
	Else
		If Trim(var) = "" Then
			NullZero =0.0
		Else
			NullZero = var
		End If
	End If
End Function
Function NullText(var)
    If var <> "" Then
		NullText = "'" & var & "'"
    Else
		NullText = "NULL"
    End If
End Function

Function SQLReplace(SQLField)
	Dim strText
	strText=SQLField
	IF len(trim(strText))>0 then
		strText=Replace(strText, "'", "''")
		strText=Replace(strText, """", """""")
		strText=Trim(strText)
	END IF
	SQLReplace=strText
End Function


'********************************************************************
' Load Lists Combos
'********************************************************************
Function LoadList(db,Value,var)
	Dim strSQL,RS,strSel,temp,intType,blnDeleted,strSQL2,rsData2
	blnDeleted =False
	strSQL="	SELECT * FROM LM_ListItem WHERE ListType=" & Value & " AND Active=1 ORDER BY ItemOrder"
	If dbOpenRecordSet(db,rs,strSQL) Then
		Do While Not RS.EOF
					If Trim(var) = Trim(RS("ListValue")) Then
						blnDeleted = true
						strSel = "selected" 
						%>
					<option Value="<%=RS("ListValue")%>" <%=strSel%>><%=RS("ListDesc")%></option>
					<%
					Else
						strSel = ""
							If CLong(rs("Active")) = 1 OR  Cbool(rs("Active")) = true Then
									%>
									<option Value="<%=RS("ListValue")%>" <%=strSel%>><%=RS("ListDesc")%></option>
									<%
							End If
					End IF
			RS.MoveNext
		Loop
	End If
		If  Len(trim(var)) > 0 AND blnDeleted=false Then
				strSQL2 = " Select count(*) From LM_ListItem WHERE ListValue='" & var & "'"
					If dbOpenRecordSet(db,rsData2,strSQL2) Then
							If  CLong(rsData2(0)) > 0 Then
										strSel = "selected" 
										%>
												<option class="deleted"  Value="<%=var%>" <%=strSel%>><%=var%> - (Inactive)</option>
										<%
							Else
										strSel = "selected" 
										%>
												<option class="deleted"  Value="<%=var%>" <%=strSel%>><%=var%> - (Deleted)</option>
										<%
							End If
					End If
		End If
	
End Function

Function LoadListA(db,Value,var)
	Dim strSQL,RS,strSel,temp,intType,blnDeleted,strSQL2,rsData2
	blnDeleted =False
	strSQL="	SELECT * FROM LM_ListItem WHERE ListType=" & Value & " AND Active=1 ORDER BY ListDesc"
	If dbOpenRecordSet(db,rs,strSQL) Then
		Do While Not RS.EOF
					If Trim(var) = Trim(RS("ListValue")) Then
						blnDeleted = true
						strSel = "selected" 
						%>
					<option Value="<%=RS("ListValue")%>" <%=strSel%>><%=RS("ListDesc")%></option>
					<%
					Else
						strSel = ""
							If CLong(rs("Active")) = 1 OR  Cbool(rs("Active")) = true Then
									%>
									<option Value="<%=RS("ListValue")%>" <%=strSel%>><%=RS("ListDesc")%></option>
									<%
							End If
					End IF
			RS.MoveNext
		Loop
	End If
		If  Len(trim(var)) > 0 AND blnDeleted=false Then
				strSQL2 = " Select count(*) From LM_ListItem WHERE ListValue='" & var & "'"
					If dbOpenRecordSet(db,rsData2,strSQL2) Then
							If  CLong(rsData2(0)) > 0 Then
										strSel = "selected" 
										%>
												<option class="deleted"  Value="<%=var%>" <%=strSel%>><%=var%> - (Inactive)</option>
										<%
							Else
										strSel = "selected" 
										%>
												<option class="deleted"  Value="<%=var%>" <%=strSel%>><%=var%> - (Deleted)</option>
										<%
							End If
					End If
		End If
	
End Function

'********************************************************************
' Load Lists Combos
'********************************************************************
Sub LoadTruncList(db,strVAR,intType,intTrunc)
	Dim strSQL, rs,strSel,strDesc
	strSQL ="	SELECT	 * " _ 
		+	"	FROM	LM_List " _
		+   "   Where ListType=" & intType & " Order BY ListDesc"
	If dbOpenRecordSet(db,rs,strSQL) Then
		Do While Not RS.EOF
			If Not(IsNull(strVar)) Then
				If Trim(RS("ListValue")) = Trim(strVAR) Then
					strSel = "selected"
				Else
					strSel= ""
				End If
			End IF
			If Len(Trim(rs("ListDesc"))) >= intTrunc Then
				strDesc = Left(Trim(rs("ListDesc")),intTrunc - 3) & "..."
			Else
				StrDesc = Trim(rs("ListDesc"))
			End If
%>
			<Option title="<%=Trim(rs("ListDesc"))%>" value="<%=Trim(rs("ListValue"))%>"  <%=strSel%> ><%=strDesc%></option>
<%		Rs.MoveNext
		Loop
	End If
End Sub

Sub ListLookup(db,strVAR,intType,intTrunc)
	Dim strSQL, rs,strSel,strDesc
	If Not IsNull(strVar) Then
		strSQL ="	SELECT	* " _ 
			&	"	FROM	LM_List " _
			&   "   Where ListType=" & intType _
			& " and Lower(ListValue) = Lower('" & Replace(strVar,"'","''") & "')"
			'Response.Write strSQL
		If dbOpenRecordSet(db,rs,strSQL) Then
			If Not RS.EOF Then
				If Len(Trim(rs("ListDesc"))) >= intTrunc Then
					strDesc = Left(Trim(rs("ListDesc")),intTrunc - 3) & "..."
				Else
					StrDesc = Trim(rs("ListDesc"))
				End If
				Response.Write strDesc
			End If
		End If
	End If
End Sub
'********************************************************************
' Process Date
'********************************************************************
Function ProcessDate(var)
	If Trim(var) = "" Then
		ProcessDate ="NULL"
	Else
		If var <> "NULL" Then
			ProcessDate = "'" & var & "'" 
		End If
	End If
End Function
'********************************************************************
' Process Date
'********************************************************************
Function CLong(intVal)
	If IsNull(intVal) Then
		CLong = Null
	ElseIf Len(Trim(intVal)) = 0 Then
		CLong = Null
	Else
			On Error Resume Next
			CLong = CLng(intVal)
	End If
End Function

Function dbMaxInsert(db,rs,strSQL,ID)
	Dim i
	For i = 0 to 5	
		If dbOpenRecordset(db,rs,strSQL) Then
			ID = rs(0)
			Exit For
		End If
	Next
End Function

Function dbInsertMax(db,rs,MaxSQL,strSQL,ID)
	Dim i
	dbInsertMax = True
	For i = 0 to 5	
		If dbOpenRecordset(db,rs,MaxSQL) Then
			If  rs.eof Then
				ID = 1 
			Else	
				ID = CLong(rs(0).value)
			End if
		End If
		If dbExec(db,Replace(strSQL,"~|~",ID)) Then 
			Exit Function
		End If 
	Next
	dbInsertMax = False
End Function

Function RoundIt(var,sigfig,resol)
Dim c,tp,pos,tempstr,str,extStr,last,ln,mult,z,i,prev,check,n,b,temp,tmp,sgn
'var=4.59999990463257
If Instr(var,"E")<>0 then
	mult=cCOADouble(Mid(var,Instr(var,"E")+1))
	If mult<0 then b=resol
	If mult>0 then b=sigfig
	temp=Left(var,Instr(var,"E")-1)
	temp=Left(var,b+1)
	var=temp*10^mult
Else
	If Instr(var,".")<>0 then
		var=Left(var,Instr(var,".")-1) & "." & Left(Mid(var,Instr(var,".")+1),resol+1)
	End if
End if

Var=cCOADouble(var)
	If var>1 then
		pos=Instr(var,".")
		If pos=0 then
			extstr=Left(var,sigfig+1)
			str=Left(var,sigfig)
			ln=Len(var)
			mult=10^(ln-len(str))  
			If len(var)=len(str) then
					RoundIt=var
			Else
				If len(str)=len(extstr) then
					RoundIt=cCOADouble(str)*mult
				Else
					str=Five(extstr,str)
					RoundIt=cCOADouble(str)*mult
					End If 
			End if
		Else
			tempstr=Left(var,Instr(var,".")-1) & mid(var,Instr(var,".")+1)
			extstr=Left(tempstr,sigfig+1)
			str=Left(tempstr,sigfig)
			ln=Len(tempstr)
			mult=10^(ln-len(str))  
			If len(tempstr)=len(str) then
					RoundIt=Left(tempstr,pos-1) & "." & mid(tempstr,pos)
			Else
				If len(str)=len(extstr) then
					RoundIt=cCOADouble(str)*mult
					RoundIt=Left(tempstr,pos-1) & "." & mid(tempstr,pos)
				Else
					str=Five(extstr,str)
					RoundIt=cCOADouble(str)*mult
					RoundIt=Left(RoundIt,pos-1) & "." & mid(RoundIt,pos)
				End If 
			End if
		End if

	ElseIf var<1 then
		str=Mid(var,Instr(var,".")+1)
		str=Left(str,resol)
		mult=CInt(Len(str)-len(cCOADouble(str)))
		mult=10^-(mult)

		If cCOADouble(str)=0 then 
			RoundIt=0
			Exit Function
		Else
			str=cCOADouble(str)
		End if

			extstr=Left(str,sigfig+1)
			str=Left(str,sigfig)

			If len(str)=len(extstr) then
				str="0." & str
				RoundIt=cCOADouble(str)*mult
			Else
				str=Five(extstr,str)
				str="0." & str
				RoundIt=cCOADouble(str)*mult
			End If 
	ElseIf var=1 then
		RoundIt=var
	End If 
End Function

Function Five(extstr,str)

Dim last,prev
		last=CInt(Right(extstr,1))
		prev=CInt(right(Str,1))
		If last>5 then
			prev=prev+1
			Five=cCOADouble(Left(Str,Len(str)-1) & Cstr(prev))
		ElseIf CInt(last)=5 then
			Five=cCOADouble(Str)
		ElseIf CInt(last)<5 then
			If last MOD 2 = 0 then
				Five=cCOADouble(Str)
			Else
				If prev<>0 THEN
					prev=prev-1
				ELSE
					prev=0
				END IF
				Five=cCOADouble(Left(Str,Len(str)-1) & Cstr(prev))
			End if
		End if
End Function

Function cCOADouble(PassedVal)
	If Isnull(PassedVal) Or Not(IsNumeric(PassedVal)) Then
		cCOADouble = 0
	Else
		cCOADouble = CDbl(PassedVal)
	End If
End Function
%>
