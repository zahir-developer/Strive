   		
set oFSO = CreateObject("Scripting.FileSystemObject")

set oText = oFSO.CreateTextFile("D:\www\websites\MDS\Scripts\ReSync2_log.txt", True)

oText.WriteLine "******************************Begin Script******************************"
oText.WriteLine Now()


strRDBMS = "SQL Server" 
strDatabase = "MPOS" 
strServer = "tcp:mpos-prod.database.windows.net,1433" 
strUser = "MPOSAdmin@mpos-prod" 
strPwd = "Mp0sL3tM3!n" 
strConnectionString = "Driver={" & strRDBMS & "};Server=" & strServer & ";Database=" & strDatabase & ";UID=" & strUser & ";PWD=" & strPwd & ";DSN=;Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;"

set oConn2= CreateObject("ADODB.Connection")
objConnect2 = strConnectionString 
oConn2.open objConnect2

set oConn= CreateObject("ADODB.Connection")
objConnect = strConnectionString 
oConn.open objConnect


oText.WriteLine "CustAcc Sync Fix" 
set rs2 = CreateObject("ADODB.Recordset")
strSQL2 =  "Select CustAccID, LocationID, ClientID FROM [MPOS].dbo.CustAcc with (nolock) WHERE (VehID IS NULL) and LocationID=1 ORDER BY CustAccID, LocationID DESC"
rs2.Open strSQL2, oConn2
If not rs2.EOF then
	    Do while not rs2.eof
             'oText.WriteLine "check: " & cstr(rs2("CustAccID")) &" | "& cstr(rs2("ClientID"))
            set rs = CreateObject("ADODB.Recordset")
            strSQL =  "SELECT vehID FROM [MPOS].dbo.REC WHERE (clientid = "& rs2("ClientID") &") AND (LocationID = 1) " 
            'oText.WriteLine strSQL 
            rs.Open strSQL, oConn
            If not rs.EOF then
                'oText.WriteLine "vehID Found: " & cstr(rs("vehID")) 

                set rs3 = CreateObject("ADODB.Recordset")

                strSQL3 =  "Select CustAccID, LocationID, ClientID FROM [MPOS].dbo.CustAcc with (nolock) WHERE (VehID ="& rs("vehID") &") and LocationID=1 " 
                rs3.Open strSQL3, oConn
                If  rs3.EOF then
                    'oText.WriteLine "vehID not assigned: " & cstr(rs("vehID")) 
                    strSQL4 = "Update [MPOS].dbo.CustAcc set vehID="& rs("vehID") &" where ( CustAccID =" & rs2("CustAccID") &") and (ClientID="& rs2("ClientID") &") and  (LocationID=1) and  (VehID IS NULL) "
                    'oText.WriteLine strSQL4 
                    oConn2.Execute strSQL4
                else
                    rs3.Close
                end if


            end if
            rs.Close

	    rs2.MoveNext	'Move to the next 
	    Loop
END IF
rs2.close


oText.WriteLine "FINI"
oText.WriteLine Now()

