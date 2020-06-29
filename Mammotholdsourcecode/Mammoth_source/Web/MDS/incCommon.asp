<%'************** Dirty Page Functions ******************* %>
<Script Language=VBScript>
Dim blnDirtyFlag
Sub SetDirty
	Dim obj
	set obj = window.event.srcElement
	If  IsNull(obj.GetAttribute("DirtyCheck")) Then
		Exit Sub
	End If

	If UCase(obj.DirtyCheck) = "TRUE" Then
		If window.event.type = "click" then
			If UCase(obj.TagName) = "BUTTON" Then
				If UCase(obj.name) ="BTNSAVE" then
					IF not blnDirtyFlag Then
						Window.Event.returnValue = False
					End If
					
					blnDirtyFlag = False

				Else
					blnDirtyFlag = True
				End If	
			End If
			If Instr(".checkbox.radio.select-one.select-multiple.button.", "."+obj.type+".") > 0 then
					blnDirtyFlag = True 
			End If
			 
		else
			If Instr(".text.password.select-one.select-multiple.", "."+obj.type+".") > 0 then
					blnDirtyFlag = True 
			End If
			If UCASE(obj.TagName) = "TEXTAREA" Then
				blnDirtyFlag = True
			End IF
		End If
	End If
	If blnDirtyFlag Then
		If  not document.All("btnSave").disabled Then
			document.All("btnSave").ClassName=""
		Else
			blnDirtyFlag=false
		End if
	Else
		document.All("btnSave").ClassName="buttondead"
	End If
End Sub

Sub DirtyCheck
	If blnDirtyFlag Then
		Window.Event.returnValue = "You have not saved your changes. Press Cancel and Save your changes or press OK to ignore your changes."
	End If
End Sub

Sub ResetDirty
	blnDirtyFlag = False
End Sub

Sub MakeDirty
	blnDirtyFlag = TRUE
	document.All("btnSave").ClassName=""
End Sub

<%'************************* END *******************%>

	function Buttonhigh()
		'exit Function
		Set obj = window.event.SrcElement 
		
		If UCase(obj.classname) <> "BUTTONDEAD" then
			obj.style.color = "yellow"
		End if
	End Function

	
	Function buttonlow()
		exit Function
		Set obj = window.event.SrcElement 
		
	     If UCase(obj.classname) <> "BUTTONDEAD" then
			obj.style.color = "white"
		End if
	End Function

Sub DisplayTitle()
	Call Top.Frames("Header").ExecScript("WriteTitle('" + Document.title +  "')","VBScript")
End Sub

<%
'*****************************************************************************************
'This is a data type validation function. Requires (DataType="") with in your input tags  
'D = Date | N=Number | R = Required Feild | RD = Required & Date | RN = Required & Number 
'*****************************************************************************************
%>

Function ClientReplace(SQLField)
	Dim strText
	strText=SQLField
	strText=Replace(strText, "'", "''")
	strText=Replace(strText, """", """""")
	strText=Trim(strText)
	ClientReplace=strText
End Function
 
Function ValDataType()
Dim item
 
ValDataType = TRUE
 
For each item in Document.All.Tags("input")
 If  NOT ISNULL(item.GetAttribute("DataType")) Then
 
  Select Case UCASE(item.DataType)
   Case "D"
    If Len(Trim(item.value)) > 0 Then
     If NOT ISDATE(item.Value) Then
      ValDataType = FALSE
      MsgBox "You have entered '" & item.value & "' in a field that requires a valid date. Please try again.",48,"Mammoth Error"
      item.Focus
      item.Select
      Exit Function
     ElseIF DatePart("yyyy",CDate(item.value)) >99 AND DatePart("yyyy",CDate(item.value)) < 1753 then 
      ValDataType = FALSE
      MsgBox "You have entered '" & item.value & "' in a field that requires a valid date. The year must be greater than 1752. Please try again.",48,"Mammoth Error"
      item.Focus
      item.Select
      Exit Function
     End IF
    End If
   Case "N"
    If Len(Trim(item.value)) > 0 Then
     If NOT IsNumeric(item.Value) Then
      ValDataType = FALSE
      MsgBox "You have entered '" & item.value & "' in a field that requires a number. Please try again.",48,"Mammoth Error"
      item.Focus
      item.Select
      Exit Function
     End IF
    End If
   Case "R"
    If Len(Trim(item.value)) < 1 Then
      ValDataType = FALSE
      MsgBox "You have not entered data in a field that is required. Please try again.",48,"Mammoth Error"
      item.Focus
      item.Select
      Exit Function
    End If
   Case "RD"
    If Len(Trim(item.value)) < 1 Then
     ValDataType = FALSE
     MsgBox "You have not entered data in a field that is required. Please try again.",48,"Mammoth Error"
     item.Focus
     item.Select
     Exit Function
    Else
     If NOT ISDATE(item.Value) Then
      ValDataType = FALSE
      MsgBox "You have entered '" & item.value & "' in a field that requires a valid date. Please try again.",48,"Mammoth Error"
      item.Focus
      item.Select
      Exit Function
     ElseIF DatePart("yyyy",CDate(item.value)) >99 AND DatePart("yyyy",CDate(item.value)) < 1753 then 
      ValDataType = FALSE
      MsgBox "You have entered '" & item.value & "' in a field that requires a valid date. The year must be greater than 1752. Please try again.",48,"Mammoth Error"
      item.Focus
      item.Select
      Exit Function
     End IF
    End If
   Case "RN"
    If Len(Trim(item.value)) < 1 Then
     ValDataType = FALSE
     MsgBox "You have not entered data in a field that is required. Please try again.",48,"Mammoth Error"
     item.Focus
     item.Select
     Exit Function
    Else
     If NOT IsNumeric(item.Value) Then
      ValDataType = FALSE
      MsgBox "You have entered '" & item.value & "' in a field that requires a number. Please try again.",48,"Mammoth Error"
      item.Focus
      item.Select
      Exit Function
     End IF
    End If
  End Select
 End If 
Next
End Function

'********************************************************************
' Format Long Year Function - For the format mm/dd/yyyy
'********************************************************************
Function  ClientLongYear(DateVal)
	If IsNull(DateVal) then
		ClientLongYear= "&nbsp;"
	Else
		If  isDate(DateVal) then
			ClientLongYear = Month(DateVal) & "/" & Day(DateVal) & "/" & Year(DateVal) & " " & formatDateTime(DateVal,vbShortTime) 
		Else
			ClientLongYear = "*Invalid Date*"
		End If
	End If
End Function


</script>
<SCRIPT LANGUAGE="JAVASCRIPT">
function ClientEncode(strvar){
	return(escape(strvar))
    }

    function Buttonlow() {
       var obj = window.event.srcElement

        if (obj.className.toUpperCase() != "BUTTONDEAD") {
            obj.style.color = "white";
        }
	}

    function Buttonhigh() {
        var obj = window.event.srcElement

        if (obj.className.toUpperCase() != "BUTTONDEAD") {
            obj.style.color = "yellow";
        }

        //if(obj.classname) <> "BUTTONDEAD" then
        //obj.style.color = "yellow"
       // End if
	}
</SCRIPT>
<%
'*******************************************
'Server Side
'*******************************************

'********************************************************************
' Status Definition for Yes/No
'********************************************************************
Function YesLookUp(var)
	Select Case var
		Case "False" 
			YesLookUp = "No"
		Case "True" 
			YesLookUp = "Yes"
		Case "0" 
			YesLookUp = "No"
		Case "1" 
			YesLookUp = "Yes"
	End Select
End Function

'********************************************************************
' Status Definition for Active/Inactive
'********************************************************************
Function ActionLookUp(var)
	Select Case var
		Case "False" 
			ActionLookUp = "Inactive"
		Case "True" 
			ActionLookUp = "Active"
		Case "0" 
			ActionLookUp = "Inactive"
		Case "1" 
			ActionLookUp = "Active"
	End Select
End Function

'********************************************************************
' Blank test Function
'********************************************************************
Function BlankTest(var)
	If IsNull(var) then
		BlankTest = "&nbsp;"
	Else
		If Trim(var) = "" Then
			BlankTest ="&nbsp;"
		Else
			BlankTest = var
		End If
	End If
End Function 
'********************************************************************
' Blank test Function
'********************************************************************
Function BlankADD(var)
	If IsNull(var) then
		BlankADD = " "
	Else
		If Trim(var) = "" Then
			BlankADD =" "
		Else
			BlankADD = var
		End If
	End If
End Function 

'********************************************************************
' Null test Function
'********************************************************************
Function NullTest(var)
	If Trim(var) = "" OR IsNull(var) Then
		NullTest ="NULL"
	Else
		NullTest = var
	End If
End Function


'********************************************************************
' Immediate IF Function
'********************************************************************
Function IIf(FakeBool,TrueResult,FalseResult)
    If FakeBool Then
		IIf = TrueResult
    Else
		IIf = FalseResult
    End If    
End Function

'********************************************************************
' isNull Max Function
'********************************************************************
Function  isMax(var)
    If  Len(trim(var)) < 1 Then
		isMax = "1"
    Else
		isMax = var
    End If    
End Function


'********************************************************************
' Check Date Function - for 3 digit years
'********************************************************************
Function CheckDate(var)
	Checkdate=-1
	If NOT IsDate(var) then
		Checkdate=0
	Else
		If DatePart("yyyy",CDate(var)) >99 AND DatePart("yyyy",CDate(var)) < 1753 then Checkdate=0
	End if
End Function

'********************************************************************
' Format Long Year Function - For the format mm/dd/yyyy
'********************************************************************
Function FormatLongYear(DateVal)
	If IsNull(DateVal) then
		FormatLongYear= "&nbsp;"
	Else
		If CheckDate(DateVal) then
			FormatLongYear = Month(DateVal) & "/" & Day(DateVal) & "/" & Year(DateVal) & " " & formatDateTime(DateVal,vbLongTime) 
		Else
			FormatLongYear = "*Invalid Date*"
		End If
	End If
End Function

'********************************************************************
' Null Date Function
'********************************************************************

Function NullDate(sDate)
	If sDate = "" Then
		NullDate = "NULL"	
	Else
		NullDate = "'" & sDate & "'"
	End If
End Function





%>

