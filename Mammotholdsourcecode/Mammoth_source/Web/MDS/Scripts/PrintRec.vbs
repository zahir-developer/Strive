' Symantec ScriptBlocking Authenticated File
' A8691045-2DC7-44B1-8E4B-DB9A6240A7D5

' Batchque Script Page
' 
' **//  Objects
	Dim oApp, oRpt, oWinFax, oEmail, oFSO, oText, oPdfFSO, oPdfFolder, oFiles, oPdf, oFile
	Dim app, avdoc, pageview, oRptTestOil, oRptLocal
	
' **//   Page Variables 
	Dim ReportPath, ReportName, strNotifyEmail
	Dim PrinterDriver, PrinterName, PrinterPort


' **//   Database Query Variables
	Dim strDSN, strUID, strPWD,intRecID
	Dim oConn, objConnect 
	Dim strSQL, rsData, strSQL1, rsData1, oRs
	
' **//   Crystal Report Variables
	Dim rptPath, rptName, oRptOptions, options
	Dim crTable, objSection, objRptObject, CRSubreports, crTableSub
	Dim ParamDefCollection, Param, MyParam, newparam, blnParamcomplete
	Dim MyParam2, MyParam3, NewParam2, NewParam3 


    strRDBMS = "SQL Server Native Client 11.0" 
	strDatabase = "MPOS" 
	strServer = "tcp:mpos-prod.database.windows.net,1433" 
	strUser = "MPOSAdmin@mpos-prod" 
	strPwd = "Mp0sL3tM3!n" 
	strConnectionString = "Driver={" & strRDBMS & "};Server=" & strServer & ";Database=" & strDatabase & ";UID=" & strUser & ";PWD=" & strPwd & ";DSN=;Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;"



	PrinterPort = "Ne02:"
	PrinterDriver = "winspool"
	PrinterName = "Star TSP100 Cutter (TSP143)"

	   		
set oFSO = CreateObject("Scripting.FileSystemObject")

set oText = oFSO.CreateTextFile("D:\www\websites\MDS\Scripts\PrintRecLog.txt", True)

oText.WriteLine "******************************Begin Script******************************"
oText.WriteLine Now()
'==============================================================='
' SET VARIABLES TO BE USED FOR THE PAGE							'
'==============================================================='
' Report Name and Path
ReportName = "Receipt.rpt"
ReportPath = "D:\www\websites\MDS\Reports\"

'==============================================================='
' CREATE THE CRYSTAL REPORTS APPLICATION OBJECT					'                                     
'==============================================================='
set oConn= CreateObject("ADODB.Connection")
objConnect = strConnectionString

oConn.open objConnect
Set oRs = CreateObject("ADODB.Recordset")

strSQL = "SELECT top(1) RecID  from PrintRec where locationID=1"
oRs.Open strSQL, oConn

IF not oRs.eof then
	intRecID = oRs("RecID")

    If Not IsObject (oApp) Then                              
	    Set oApp = CreateObject("CrystalRuntime.Application.10")
    End If                                                                
	If not IsObject (oApp) Then
		oText.WriteLine "Error - Crystal Object Failed"
	Else		
		oText.WriteLine "Crystal Object Created"
	    ' OPEN THE REPORT
	    If isObject(oRpt) then
	    	set oRpt = nothing
	    End If
		oText.WriteLine "Report: " & ReportPath & ReportName
	    Set oRpt = oApp.OpenReport(ReportPath & ReportName, 1)
		If not IsObject (oRpt) Then
			oText.WriteLine "Error - Crystal Report Failed"
		Else
			oText.WriteLine "Crystal Report Created"
		    ' Turn off specific report error messages                            
		    'Set oRptOptions = oRpt.Options                             
		    oRpt.MorePrintEngineErrorMessages = False
		    oRpt.EnableParameterPrompting = False
		    '==============================================================='
		    ' Logon and set passwords for all report and subreport tables	'
		    '==============================================================='
			'set options =  oApp.options
			'options.MatchLogonInfo = 1
		    'Get DSN,UserID,and Password
			For each crtable in oRpt.Database.Tables
		   		crtable.SetLogonInfo Cstr(strDSN),,CStr(strUID),CStr(strPWD)
			Next
			'open the subreport(s)
			'==============================================================='
			' PARAMETER FIELDS												'
			'==============================================================='
			Set ParamDefCollection = oRpt.ParameterFields
			Set Param = ParamDefCollection
	    	set MyParam1 = Param.Item(1)
			NewParam1 = intRecID
			Call Myparam1.SetCurrentValue(CDbl(newparam1),Myparam1.ValueType)

			set MyParam2 = Param.Item(2)
			NewParam2 = intLocationID
			Call Myparam2.SetCurrentValue(CDbl(newparam2),Myparam2.ValueType)
			blnParamComplete = True
			If blnParamComplete Then
				'On Error Resume Next                                                  
				oRpt.ReadRecords
			End If
	   		'==============================================================='
	   		' IF IS Printing												'
	   		'==============================================================='
			'Print to the Printer Driver
			oText.WriteLine "Printed Report for RecID: " & intRecID 

			oRpt.SelectPrinter PrinterDriver, PrinterName, PrinterPort
			oRpt.PrintOut False , 1, False
		End If						
	End If
strSQL = "Delete PrintRec WHERE RecID = " & intRecID &" and LocationID=1"
oConn.Execute strSQL

oText.WriteLine "Done Printing Report for RecID: " & intRecID 
END IF
oText.WriteLine "FINI"
oText.WriteLine Now()
oRs.close

'Close connection to the database
oConn.Close

