'// Generic validate function
'// INPUT Parameters:
' DataType(N,T,D)
' For Numerics:
' LimitMin If Decimal Place in LimitMin, Then Indicates precision
' LimitMax
' Format
Function Validate()
	Dim ObjElement,intPrecision,strUpperLimit,strLowerLimit,strDesc
	
	For Each objelement in Document.All

		If IsNull(objelement.Getattribute("Desc")) Then
			strDesc = "selected input field"
		Else
			strDesc = objElement.Desc
		End If

		If ObjElement.TagName = "INPUT" or ObjElement.TagName = "TEXTAREA" Then
			If objElement.GetAttribute("ClassName") <> "inputdata" then
				Select Case objelement.Getattribute("DataType")
					Case "N":
						'MsgBox objElement.GetAttribute("Required")
						If Not (Isnumeric(objElement.Value)) AND IsNull(objElement.GetAttribute("Required")) Then
							MsgBox "Please enter a valid number for the " & strDesc & ".",48,"Validation Error"
							On Error Resume Next
							objElement.Focus
							objelement.Select
							On Error Goto 0
							Validate = False
							Exit Function
						End If
						If Not(Isnumeric(objElement.Value)) And Len(Trim(objElement.Value)) > 0 Then
							MsgBox "Please enter a valid number for the " & strDesc & ".",48,"Validation Error"
							On Error Resume Next
							objElement.Focus
							objelement.Select
							On Error Goto 0
							Validate = False
							Exit Function
						ElseIf Len(Trim(objElement.Value)) > 0 Then
							If IsNumeric(objelement.Getattribute("LimitMin")) Then
								If CDbl(objElement.Value) < CDbl(objelement.Getattribute("LimitMin")) Then
									If IsNumeric(objelement.Getattribute("LimitMax")) Then
										strUpperLimit = objelement.Getattribute("LimitMax")
									Else
										strUpperLimit = "No Upper Limit"
									End If
									MsgBox "The number you have entered for the " & strDesc & " is outside the acceptable limits(" & objelement.Getattribute("LimitMin") & "-" & strUpperLimit & ")",48,"Validation Error"
									On Error Resume Next
									objElement.Focus
									objelement.Select
									On Error Goto 0
									Validate = False
									Exit Function
								End If
							End If
							If IsNumeric(objelement.Getattribute("LimitMax")) Then
								If CDbl(objElement.Value) > CDbl(objelement.Getattribute("LimitMax")) Then
									If IsNumeric(objelement.Getattribute("LimitMin")) Then
										strLowerLimit = objelement.Getattribute("LimitMin")
									Else
										strLowerLimit = "No Lower Limit"
									End If
									MsgBox "The number you have entered for the " & strDesc & " is outside the acceptable limits(" & strLowerLimit & "-" & objelement.Getattribute("LimitMax") & ")",48,"Validation Error"
									On Error Resume Next
									objElement.Focus
									objelement.Select
									On Error Goto 0
									Validate = False
									Exit Function
								End If
							End If
							If Instr(objelement.Getattribute("LimitMin"),".") > 0  Then
								intPrecision = Len(objelement.Getattribute("LimitMin")) - Instr(objelement.Getattribute("LimitMin"),".")
							Else 
								intPrecision = 0
							End If

							If Instr(objelement.Value,".") > 0  Then
								If CInt(IntPrecision) < CInt(Len(Trim(ObjElement.Value)) - Instr(objelement.Value,".")) Then
									MsgBox "The " & strDesc & " is limited to " & intPrecision & " places to the right of the decimal point." ,48,"Validation Error"
									On Error Resume Next
									objElement.Focus
									objelement.Select
									On Error Goto 0
									Validate = False
									Exit Function							
								End If
							End If
						End If
					Case "T":
						If Len(Trim(objelement.Value)) = 0 And IsNull(objElement.GetAttribute("Required")) Then
							MsgBox "Please enter a value for the " & strDesc & ".",48,"Validation Error"
							On Error Resume Next
							objElement.Focus
							objelement.Select
							On Error Goto 0
							Validate = False
							Exit Function							
						End If
						If Len(Trim(objelement.Getattribute("Format"))) > 0 And Len(Trim(objElement.Value)) > 0 Then
							If Not(IsValidFormat(objElement)) Then
								MsgBox "Please enter the value for the " & strDesc & " in the format '" & objElement.Format & "'" & vbCrLf & "(# is Numeric, X is alphanumeric, All other characters are literal).",48,"Validation Error"
								On Error Resume Next
								objElement.Focus
								objelement.Select
								On Error Goto 0
								Validate = False
								Exit Function
							End If
						End If
					Case "D":
						If Not(IsDate(objelement.Value)) Then
							If ISNull(objElement.GetAttribute("Required")) Then
								MsgBox "Please enter a valid date for the " & strDesc & ".",48,"Validation Error"
								On Error Resume Next
								objElement.Focus
								objelement.Select
								On Error Goto 0
								Validate = False
								Exit Function
							Else
								If Len(Trim(objElement.Value)) > 0 Then
									MsgBox "Please enter a valid date for the " & strDesc & ".",48,"Validation Error"
									On Error Resume Next
									objElement.Focus
									objelement.Select
									On Error Goto 0
									Validate = False
									Exit Function
								End If
							End If
						Else
							If IsDate(objelement.Getattribute("LimitMin")) Then
								If CDate(objElement.Value) < CDate(objelement.Getattribute("LimitMin")) Then
									If IsDate(objelement.Getattribute("LimitMax")) Then
										strUpperLimit = objelement.Getattribute("LimitMax")
									Else
										strUpperLimit = "No Upper Limit"
									End If
									MsgBox "The date you have entered for the " & strDesc & " is outside the acceptable limits(" & objelement.Getattribute("LimitMin") & "-" & strUpperLimit & ")",48,"Validation Error"
									On Error Resume Next
									objElement.Focus
									objelement.Select
									On Error Goto 0
									Validate = False
									Exit Function
								End If
							End If
							If IsDate(objelement.Getattribute("LimitMax")) Then
								If CDate(objElement.Value) > CDate(objelement.Getattribute("LimitMax")) Then
									If IsDate(objelement.Getattribute("LimitMin")) Then
										strLowerLimit = objelement.Getattribute("LimitMin")
									Else
										strLowerLimit = "No Lower Limit"
									End If
									MsgBox "The date you have entered for the " & strDesc & " is outside the acceptable limits(" & strLowerLimit & "-" & objelement.Getattribute("LimitMax") & ")",48,"Validation Error"
									On Error Resume Next
									objElement.Focus
									objelement.Select
									On Error Goto 0
									Validate = False
									Exit Function
								End If
							End If
						End If
				End Select
			End If
		ElseIf ObjElement.TagName = "SELECT" AND IsNull(objElement.GetAttribute("Required")) Then
			If objElement.SelectedIndex = -1 Then
				MsgBox "Please enter a valid selection from the " & strDesc & " combo.",48,"Validation Error"
				On Error Resume Next
				objElement.Focus
				objelement.Select
				On Error Goto 0
				Validate = False
				Exit Function
			End If
		End If
	Next
	Validate = True
End Function

Function IsValidFormat(objelement)
	Dim strBenchmark,strTest,I
	strBenchmark = objElement.Format
	strTest = objElement.Value
	If Len(strBenchMark) <> Len(strTest) Then
		IsValidFormat = False
		Exit Function
	Else
		For I = 1 to Len(strBenchMark)
			Select Case Mid(strBenchMark,I,1)
				Case "#":
					If Not(IsNumeric(Mid(strTest,I,1))) Then
						IsValidFormat = False
						Exit Function
					End If
				Case "N":
					If Mid(strTest,I,1) = " " Then
						IsValidFormat = False
						Exit Function
					End If
				Case Else:
					If Mid(strTest,I,1) <> Mid(strBenchMark,I,1) Then
						IsValidFormat = False
						Exit Function
					End If
			End Select
		Next
	End If
	IsValidFormat = True
End Function
