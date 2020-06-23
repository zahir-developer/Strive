   		
set oFSO = CreateObject("Scripting.FileSystemObject")

set oText = oFSO.CreateTextFile("D:\www\websites\MDS\Scripts\UpdateStats_log.txt", True)

oText.WriteLine "******************************Begin Script******************************"
oText.WriteLine Now()


    strRDBMS = "SQL Server" 
	strDatabase = "MPOS" 
	strServer = "tcp:mpos-prod.database.windows.net,1433" 
	strUser = "MPOSAdmin@mpos-prod" 
	strPwd = "Mp0sL3tM3!n" 
	strConnectionString = "Driver={" & strRDBMS & "};Server=" & strServer & ";Database=" & strDatabase & ";UID=" & strUser & ";PWD=" & strPwd & ";DSN=;Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;"

set oConn= CreateObject("ADODB.Connection")
objConnect = strConnectionString

oConn.open objConnect
Set oRs = CreateObject("ADODB.Recordset")

set oConn2= CreateObject("ADODB.Connection")
objConnect2 = "DSN=MDS02;UID=sa;PWD=mamm0th" 

oConn2.open objConnect2

set rs2 = CreateObject("ADODB.Recordset")

strSQL = "SELECT clientid,Ctype,account,status, C_Corp FROM client"
oRs.Open strSQL, oConn

If not oRs.EOF then
	    Do while not oRs.eof

            'oText.WriteLine cstr(oRs("clientid"))
            
            strSQL2 = "SELECT Ctype,account,status, C_Corp FROM client where clientid=" & oRs("clientid")
            rs2.Open strSQL2, oConn2

            If not rs2.EOF then
	                Do while not rs2.eof
                        if  oRs("status")<>rs2("status") or  oRs("C_Corp")<>rs2("C_Corp") then
                            'oText.WriteLine rs2("Ctype") & " | " & rs2("account")
                            'if rs2("account") = "True" then
                            '    blaccount = 1
                            'else
                            '    blaccount = 0
                            'end if
                            
                            
                            strSQL3 = "update client set status="& rs2("status") &",C_Corp="& rs2("C_Corp") &" where clientid=" & oRs("clientid")
                            oConn.Execute strSQL3


                        end if
	                rs2.MoveNext	'Move to the next 
	                Loop
            END IF
            rs2.close

	    oRs.MoveNext	'Move to the next 
	    Loop
END IF


oText.WriteLine "FINI"
oText.WriteLine Now()

