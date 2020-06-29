   		
set oFSO = CreateObject("Scripting.FileSystemObject")

set oText = oFSO.CreateTextFile("D:\www\websites\MDS\Scripts\ReSync_log.txt", True)

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
objConnect2 = "SERVER=E6510\MDS;DSN=LocalMPOS;UID=sa;PWD=mamm0th" 
oConn2.open objConnect2

'oText.WriteLine "Rec Sync LocationID 2" 
'set rs2 = CreateObject("ADODB.Recordset")
'strSQL2 =  " SELECT LocationID, recid, clientid, esttime, vehID, VehMan, isnull(VModel,'') as VModel, VehMod, VehColor, datein, case When dateout is null then datein else dateout end as dateout, isnull(accamt,0) as accamt,  isnull(tax,0) as tax,  isnull(gtotal,0) as gtotal, isnull(cashamt,0) as cashamt,  isnull(chargeamt,0) as chargeamt,  isnull(cashback,0) as cashback,  isnull(cardtype,0) as cardtype,  isnull(totalamt,0) as totalamt, Status, SalesRep, isnull(Notes,'') as Notes, Line,"&_
'           " isnull(DayCnt,0) as DayCnt, cashoutid, CardNo, Approval, Signiture, CheckAmt, ChkNo, ChkPhone, chkDL, GiftCardAmt, GiftCardID, CloseDte, isnull(VINL5,0) as VINL5, isnull(reg,0) as reg, isnull(adv,0) as adv, isnull(QABy,0) as QABy, estMin, Labor "&_
'           " FROM [MPOS].dbo.REC WHERE (LocationID = 2) AND (recid >= 241720) AND (recid <= 252608) AND (CloseDte IS NOT NULL)"
'rs2.Open strSQL2, oConn2
'If not rs2.EOF then
'	    Do while not rs2.eof
'            strSQL = "SELECT LocationID,recid from dbo.REC Where LocationID="& rs2("LocationID") &" and RecID="& rs2("recid")
'            oRs.Open strSQL, oConn
'            If oRs.EOF then
'                oText.WriteLine "Insert: " & cstr(rs2("recid")) 
'                strSQL3 = "Insert into dbo.REC(LocationID, recid, clientid, esttime, vehID, VehMan,"&_
'                    " VModel, VehMod, VehColor, datein, dateout, accamt, tax,"&_
'                    " gtotal, cashamt, chargeamt, cashback, cardtype, totalamt,"&_
'                    " Status, SalesRep, Notes, Line, DayCnt, cashoutid,"&_
'                    " CheckAmt, ChkNo, ChkPhone, chkDL, GiftCardAmt, GiftCardID, CloseDte, reg, QABy, estMin)"&_
'                    " Values(" & rs2("LocationID") &","& rs2("recid") &","& rs2("clientid") &",'"& rs2("esttime") &"',"& rs2("vehID") &","& rs2("VehMan") &","&_
'                    " '" & rs2("VModel") &"',"& rs2("VehMod") &","& rs2("VehColor") &",'"& rs2("datein") &"','"& rs2("dateout") &"',"& rs2("accamt") &","& rs2("tax") &","&_
'                    " " & rs2("gtotal") &","& rs2("cashamt") &","& rs2("chargeamt") &","& rs2("cashback") &","& rs2("cardtype") &","& rs2("totalamt") &","&_
'                    " " & rs2("Status") &","& rs2("SalesRep") &",'"& rs2("Notes") &"',"& rs2("Line") &","& rs2("DayCnt") &","& rs2("cashoutid") &","&_
'                    " " & rs2("CheckAmt") &",'"& rs2("ChkNo") &"','"& rs2("ChkPhone") &"','"& rs2("chkDL") &"',"& rs2("GiftCardAmt") &","&_
'                    " '" & rs2("GiftCardID") &"','"& rs2("CloseDte") &"',"& rs2("reg") &","& rs2("QABy") &",'"& rs2("estMin") &"')" 
'                'oText.WriteLine strSQL3 
'                oConn.Execute strSQL3
'            'else
'            '    oText.WriteLine "Found:" & cstr(rs2("recid")) 
'            end if
'            oRs.close
'	    rs2.MoveNext	'Move to the next 
'	    Loop
'END IF
'rs2.close


'oText.WriteLine "RecItem Sync LocationID 2" 
'set rs2 = CreateObject("ADODB.Recordset")
'strSQL2 =  " SELECT  RecItemID, LocationID, recId, ProdID, Comm, Price, QTY "&_
'           " FROM [MPOS].dbo.RECITEM WHERE (LocationID = 2) AND (recid >= 241720) AND (recid <= 252608)"
'rs2.Open strSQL2, oConn2
'If not rs2.EOF then
'	    Do while not rs2.eof
'            strSQL = "SELECT RecItemID,LocationID,recid from dbo.RECITEM Where RecItemID="& rs2("RecItemID") &" and LocationID="& rs2("LocationID") &" and RecID="& rs2("recid")
'            oRs.Open strSQL, oConn
'            If oRs.EOF then
'                oText.WriteLine "Insert: " & cstr(rs2("RecItemID")) 
'                strSQL3 = "Insert into dbo.RECITEM(RecItemID, LocationID, recId, ProdID, Comm, Price, QTY)"&_
'                    " Values(" & rs2("RecItemID") &","& rs2("LocationID") &","& rs2("recid") &","& rs2("ProdID")  &","& rs2("Comm")  &","& rs2("Price")  &","& rs2("QTY")  &")" 
'                'oText.WriteLine strSQL3 
'                oConn.Execute strSQL3
'            'else
'            '    oText.WriteLine "Found:" & cstr(rs2("RecItemID")) 
'            end if
'            oRs.close
'	    rs2.MoveNext	'Move to the next 
'	    Loop
'END IF
'rs2.close


'oText.WriteLine "Rec Sync LocationID 1" 
'set rs2 = CreateObject("ADODB.Recordset")
'strSQL2 =  " SELECT  LocationID, recid, clientid, esttime, vehID, VehMan, isnull(VModel,'') as VModel, VehMod, VehColor, datein, case When dateout is null then datein else dateout end as dateout, isnull(accamt,0) as accamt,  isnull(tax,0) as tax,  isnull(gtotal,0) as gtotal, isnull(cashamt,0) as cashamt,  isnull(chargeamt,0) as chargeamt,  isnull(cashback,0) as cashback,  isnull(cardtype,0) as cardtype,  isnull(totalamt,0) as totalamt, Status, SalesRep, isnull(Notes,'') as Notes, Line,"&_
'           " isnull(DayCnt,0) as DayCnt, cashoutid, CardNo, Approval, Signiture, CheckAmt, ChkNo, ChkPhone, chkDL, GiftCardAmt, GiftCardID, CloseDte, isnull(VINL5,0) as VINL5, isnull(reg,0) as reg, isnull(adv,0) as adv,  isnull(QABy,0) as QABy, estMin, Labor "&_
'           " FROM [MPOS].dbo.REC WHERE  (LocationID = 1) AND (recid >= 118561) AND (recid <= 124864) AND (CloseDte IS NOT NULL)"
'rs2.Open strSQL2, oConn2
'If not rs2.EOF then
'	    Do while not rs2.eof
'            strSQL = "SELECT LocationID,recid from dbo.REC Where LocationID="& rs2("LocationID") &" and RecID="& rs2("recid")
'            oRs.Open strSQL, oConn
'            If oRs.EOF then
'                oText.WriteLine "Insert: " & cstr(rs2("recid")) 
'                strSQL3 = "Insert into dbo.REC(LocationID, recid, clientid, esttime, vehID, VehMan,"&_
'                    " VModel, VehMod, VehColor, datein, dateout, accamt, tax,"&_
'                    " gtotal, cashamt, chargeamt, cashback, cardtype, totalamt,"&_
'                    " Status, SalesRep, Notes, Line, DayCnt, cashoutid,"&_
'                    " CheckAmt, ChkNo, ChkPhone, chkDL, GiftCardAmt, GiftCardID, CloseDte, reg, QABy, estMin)"&_
'                    " Values(" & rs2("LocationID") &","& rs2("recid") &","& rs2("clientid") &",'"& rs2("esttime") &"',"& rs2("vehID") &","& rs2("VehMan") &","&_
'                    " '" & rs2("VModel") &"',"& rs2("VehMod") &","& rs2("VehColor") &",'"& rs2("datein") &"','"& rs2("dateout") &"',"& rs2("accamt") &","& rs2("tax") &","&_
'                    " " & rs2("gtotal") &","& rs2("cashamt") &","& rs2("chargeamt") &","& rs2("cashback") &","& rs2("cardtype") &","& rs2("totalamt") &","&_
'                    " " & rs2("Status") &","& rs2("SalesRep") &",'"& rs2("Notes") &"',"& rs2("Line") &","& rs2("DayCnt") &","& rs2("cashoutid") &","&_
'                    " " & rs2("CheckAmt") &",'"& rs2("ChkNo") &"','"& rs2("ChkPhone") &"','"& rs2("chkDL") &"',"& rs2("GiftCardAmt") &","&_
'                    " '" & rs2("GiftCardID") &"','"& rs2("CloseDte") &"',"& rs2("reg") &","& rs2("QABy") &",'"& rs2("estMin") &"')" 
'                'oText.WriteLine strSQL3 
'                oConn.Execute strSQL3
'            'else
'            '    oText.WriteLine "Found:" & cstr(rs2("recid")) 
'            end if
'            oRs.close
'	    rs2.MoveNext	'Move to the next 
'	    Loop
'END IF
'rs2.close


'oText.WriteLine "RecItem Sync LocationID 1" 
'set rs2 = CreateObject("ADODB.Recordset")
'strSQL2 =  " SELECT RecItemID, LocationID, recId, ProdID, Comm, Price, QTY "&_
'           " FROM [MPOS].dbo.RECITEM WHERE (LocationID = 1) AND (recid >= 118561) AND (recid <= 124864)"
'rs2.Open strSQL2, oConn2
'If not rs2.EOF then
'	    Do while not rs2.eof
'            strSQL = "SELECT RecItemID,LocationID,recid from dbo.RECITEM Where RecItemID="& rs2("RecItemID") &" and LocationID="& rs2("LocationID") &" and RecID="& rs2("recid")
'            oRs.Open strSQL, oConn
'            If oRs.EOF then
'                oText.WriteLine "Insert: " & cstr(rs2("RecItemID")) 
'                strSQL3 = "Insert into dbo.RECITEM(RecItemID, LocationID, recId, ProdID, Comm, Price, QTY)"&_
'                    " Values(" & rs2("RecItemID") &","& rs2("LocationID") &","& rs2("recid") &","& rs2("ProdID")  &","& rs2("Comm")  &","& rs2("Price")  &","& rs2("QTY")  &")" 
'                'oText.WriteLine strSQL3 
'                oConn.Execute strSQL3
'           ' else
'            '    oText.WriteLine "Found:" & cstr(rs2("RecItemID")) 
'            end if
'            oRs.close
'	    rs2.MoveNext	'Move to the next 
'	    Loop
'END IF
'rs2.close


'oText.WriteLine "Client Sync LocationID 2" 
'set rs2 = CreateObject("ADODB.Recordset")
'strSQL2 =  " SELECT clientid, LocationID, fname, REPLACE(lname, '''', '`') AS lname, addr1, addr2, city, st, zip, Ctype, isnull(C_Corp,0) as C_Corp, Phone, Phone2, PhoneType, Phone2Type,"&_
'            " Email, CASE WHEN isnull(NoEmail, 0) = 1 THEN 1 ELSE 0 END  as NoEmail, isnull(Score,'') as Score,  isnull(status,0) as status, CASE WHEN isnull(account, 0) = 1 THEN 1 ELSE 0 END  as account, StartDT, isnull(Notes,'') as Notes, isnull(RecNote,'') as RecNote"&_
'           " FROM [MPOS].dbo.client WHERE (LocationID = 2) order by clientid"
'rs2.Open strSQL2, oConn2
'If not rs2.EOF then
'	    Do while not rs2.eof
'            strSQL = "SELECT clientid from dbo.client Where clientid="& rs2("clientid")
'            oRs.Open strSQL, oConn
'            If oRs.EOF then
'                oText.WriteLine "Insert: " & cstr(rs2("clientid")) 
'                strSQL3 = "Insert into dbo.client( clientid, LocationID, fname, lname, addr1, addr2, city, st, zip, Ctype, C_Corp, Phone, Phone2, PhoneType, Phone2Type,"&_
'                    " Email, NoEmail, Score, status, account, StartDT, Notes, RecNote)"&_
'                    " Values(" & rs2("clientid") &","& rs2("LocationID") &",'"& rs2("fname") &"','"& rs2("lname") &"','"& rs2("addr1") &"','"& rs2("addr2") &"','"& rs2("city") &"',"&_
'                    " '" & rs2("st") &"','"& rs2("zip") &"',"& rs2("Ctype") &","& rs2("C_Corp") &",'"& rs2("Phone") &"','"& rs2("Phone2") &"','"& rs2("PhoneType") &"','"& rs2("Phone2Type") &"',"&_
'                    " '" & rs2("Email") &"',"& rs2("NoEmail") &",'"& rs2("Score") &"',"& rs2("status") &","& rs2("account") &",'"& rs2("StartDT") &"','"& trim(rs2("Notes")) &"','"& trim(rs2("RecNote"))  &"')" 
'                oText.WriteLine strSQL3 
'                oConn.Execute strSQL3
'            'else
'            '    oText.WriteLine "Found:" & cstr(rs2("clientid")) 
'            end if
'            oRs.close
'	    rs2.MoveNext	'Move to the next 
'	    Loop
'END IF
'rs2.close


'oText.WriteLine "CustAcc Sync LocationID 2" 
'set rs2 = CreateObject("ADODB.Recordset")
'strSQL2 =  " SELECT CustAccID, LocationID, ClientID, VehID, CurrentAmt, ActiveDte, LastUpdate, LastUpdateBy, Type, Status, MonthlyCharge, Limit"&_
'           " FROM [MPOS].dbo.CustAcc WHERE (LocationID = 2) AND (VehID IS NOT NULL) order by CustAccID"
'rs2.Open strSQL2, oConn2
'If not rs2.EOF then
'	    Do while not rs2.eof
'            strSQL = "SELECT CustAccID,clientid from dbo.CustAcc Where CustAccID="& rs2("CustAccID") &" and clientid="& rs2("clientid")
'            oRs.Open strSQL, oConn
'            If oRs.EOF then
'                oText.WriteLine "Insert: " & cstr(rs2("CustAccID")) 
'                strSQL3 = "Insert into dbo.CustAcc( CustAccID, LocationID, ClientID, VehID, CurrentAmt, ActiveDte, LastUpdate, LastUpdateBy, Type, Status, MonthlyCharge, Limit)"&_
'                    " Values(" & rs2("CustAccID") &","& rs2("LocationID") &","& rs2("ClientID") &","& rs2("VehID") &","& rs2("CurrentAmt") &",'"& rs2("ActiveDte") &"','"& rs2("LastUpdate") &"',"& rs2("LastUpdateBy") &","& rs2("Type") &","& rs2("Status") &","& rs2("MonthlyCharge") &","& rs2("Limit") &")" 
'                'oText.WriteLine strSQL3 
'                oConn.Execute strSQL3
'            'else
'            '    oText.WriteLine "Found:" & cstr(rs2("CustAccID")) 
'            end if
'            oRs.close
'	    rs2.MoveNext	'Move to the next 
'	    Loop
'END IF
'rs2.close


'oText.WriteLine "CustAccHist Sync LocationID 2" 
'set rs2 = CreateObject("ADODB.Recordset")
'strSQL2 =  " SELECT CustAccID, CustAccTID, TXLocationID, TXCustID, TXRecID, TXType, TXAmt, TXDte, TXuser"&_
'           " FROM [MPOS].dbo.CustAccHist WHERE (TXLocationID = 2) AND (TXRecID >= 241720) AND (TXRecID <= 252608) order by CustAccTID"
'rs2.Open strSQL2, oConn2
'If not rs2.EOF then
'	    Do while not rs2.eof
'            strSQL = "SELECT CustAccID,CustAccTID,TXCustID, TXRecID from dbo.CustAccHist Where CustAccID="& rs2("CustAccID") &" and CustAccTID="& rs2("CustAccTID") &" and TXCustID="& rs2("TXCustID") &" and TXRecID="& rs2("TXRecID")
'            oRs.Open strSQL, oConn
'            If oRs.EOF then
'                oText.WriteLine "Insert: " & cstr(rs2("CustAccTID")) 
'                strSQL3 = "Insert into dbo.CustAccHist( CustAccID, CustAccTID, TXLocationID, TXCustID, TXRecID, TXType, TXAmt, TXDte, TXuser)"&_
'                    " Values(" & rs2("CustAccID") &","& rs2("CustAccTID") &","& rs2("TXLocationID") &","& rs2("TXCustID") &","& rs2("TXRecID") &",'"& rs2("TXType") &"',"& rs2("TXAmt") &",'"& rs2("TXDte") &"',"& rs2("TXuser")  &")" 
'                'oText.WriteLine strSQL3 
'                oConn.Execute strSQL3
'            else
'                oText.WriteLine "Found:" & cstr(rs2("CustAccTID")) 
'            end if
'            oRs.close
'	    rs2.MoveNext	'Move to the next 
'	    Loop
'END IF
'rs2.close


oText.WriteLine "FINI"
oText.WriteLine Now()

