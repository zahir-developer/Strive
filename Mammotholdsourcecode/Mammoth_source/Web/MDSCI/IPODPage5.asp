<%@  language="VBSCRIPT" %>
<%
'********************************************************************
Option Explicit
Response.Expires = 0
Response.Buffer = True
response.ContentType = "application/json"
'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incDatabase.asp"-->
<%
Dim dbMain,strSQL,rsData,strSQL2,rsData2,Jout,blnUpdatePass,strAddService
Dim intUserID, intRecID, strDept, strUPC, strType, strWash, strAir, strUpCharge,strNoEng
dim intDayCnt,dtesttime,strEstMin,intUPClength,intProdID,strPrice,strComm,intrecItemID
Dim  intClientID,newVehID,intMake,strVmodel,intmodel,intColor,intLine,strTag,intNocharge,isCeramic, intCType

Set dbMain =  OpenConnection
blnUpdatePass = 1
intCType = 0
'strAddService = trim(UCase(Request("AddService")))
 



strSQL = "SELECT UserID,  Dept, UPC, Type, AddService, Wash, Air, UpCharge,NoEng,Make,Model,VModel,Color,Tag FROM ScanIn WHERE (ScanIn.LocationID = "& Application("LocationID") &")"
if DBOpenRecordset(dbMain,rsData,strSQL) Then
	if not rsData.EOF Then
        intUserID = rsData("UserID")
        strDept = rsData("Dept")
        strUPC = rsData("UPC")
        strType = rsData("Type")
        strAddService = rsData("AddService")
        strWash = rsData("Wash")
        strAir = rsData("Air")
        strUpCharge = rsData("UpCharge")
        strNoEng = rsData("NoEng")
        intMake = rsData("Make")
        strVModel = rsData("VModel")
        intmodel = rsData("Model")
        intColor = rsData("Color")
        strTag = rsData("Tag")
 
 '******* start of line ********'   
     
		strSQL = "Select recID=IsNull(Max(recID),0) + 1 From Rec WHERE (LocationID = "& Application("LocationID") &")"
		if DBOpenRecordset(dbMain,rsData,strSQL) Then
			intrecID = rsData("recID")
		end if

        strSQL= " Update ScanIn set RecID = '" & intrecID &"' WHERE (LocationID = "& Application("LocationID") &")"
        If NOT (dbExec(dbMain,strSQL)) Then
		    blnUpdatePass = 0
        End If



		strSQL = "Select DayCnt=IsNull(Max(DayCnt),0) + 1 From Rec"&_
				" where year(datein)='" & year(now()) &"'"&_
				" AND month(datein)='" & Month(now()) &"'"&_
				" AND Day(datein)='" & Day(now()) &"' AND (LocationID = "& Application("LocationID") &")"
		if DBOpenRecordset(dbMain,rsData,strSQL) Then
			intDayCnt = rsData("DayCnt")
		end if


        if len(trim(strUPC)) >= 7 then 
 
   	        strSQL = "SELECT top (1) clientID,vehid, make,Vmodel, model, Color FROM vehical where ltrim(rtrim(upc))='" & ltrim(rtrim(strUPC)) &"'"
	        if DBOpenRecordset(dbMain,rsData,strSQL) Then
		        if not rsData.eof then
			        intClientID = rsData("clientID")
			        newVehID = rsData("vehid")
			        intMake = rsData("make")
			        strVmodel = rsData("Vmodel")
			        intmodel = rsData("model")
			        intColor = rsData("Color")
					IF intClientID > 0 then
						strSQL = "Select Ctype From Client where ClientID="&intClientID
						if DBOpenRecordset(dbMain,rsData,strSQL) Then
							intCType = rsData("Ctype")
		 				end if
						IF intCType = 2 then ' Monthly
							strSQL = "Select isnull(CustAcc.Status,0) as Status From CustAcc where ClientID="&intClientID&" and vehid="&newVehID
							if DBOpenRecordset(dbMain,rsData,strSQL) Then
								if rsData("Status") = 0 then
									intCType = 0
								else
									intCType = 2
								end if
		 					end if
						end if
					end if
		        else
			        intClientID = 0 'Drive up
			        newVehID = 0

		            strSQL = "Select vehid=IsNull(Max(vehid),0) + 1 From vehical"
		            if DBOpenRecordset(dbMain,rsData,strSQL) Then
			            newVehID = rsData("vehid")
		            end if

   	                strSQL= " Insert into vehical(vehid, upc, tag, clientid, vehnum, make, model, Vmodel, Color,LocationID)Values(" & _
				            newVehID & ", " & _
				            "'"& strUPC &"'," & _
				            "'"& strTag &"'," & _
				             "0, " & _
				             "1, " & _
				            intMake & "," & _
				            intmodel & ", " & _
				            "'"& strVModel &"'," & _
				            intColor &"," & _
				            Application("LocationID") & ") " 
	                if not (dbExec(dbMain,strSQL)) Then
		                blnUpdatePass = 0
 		            end if


		        end if
	        end if
        else
			intClientID = 0 'Drive up
	        newVehID = 0
 		    strSQL = "Select vehid=IsNull(Max(vehid),0) + 1 From vehical"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    newVehID = rsData("vehid")
		    end if

   	        strSQL= " Insert into vehical(vehid, upc, tag, clientid, vehnum, make, model, Vmodel, Color,LocationID)Values(" & _
				    newVehID & ", " & _
				    "'"& strUPC &"'," & _
				    "'"& strTag &"'," & _
				        "0, " & _
				        "1, " & _
				    intMake & "," & _
				    intmodel & ", " & _
				    "'"& strVModel &"'," & _
		            intColor &"," & _
		            Application("LocationID") & ") " 
	        if not (dbExec(dbMain,strSQL)) Then
		        blnUpdatePass = 0
 		    end if

  
  
       end if


        strSQL = "{call stp_getCurrWaitTime "& Application("LocationID") &"}"     
        if not DBExec(dbMain, strSQL) then
		    blnUpdatePass = 0
        end if


		strSQL = "SELECT CurrentWaitTime FROM stats WHERE (LocationID = "& Application("LocationID") &")"
		if DBOpenRecordset(dbMain,rsData,strSQL) Then
			if instr(1,rsData("CurrentWaitTime")," Min")>0 then		
				dtesttime = dateadd("n",int(replace(rsData("CurrentWaitTime")," Min","")),NOW())
            	strEstMin = rsData("CurrentWaitTime")
			else
				dtesttime = dateadd("n",25,NOW())
            	strEstMin = rsData("CurrentWaitTime")
			end if
		end if

 		if strDept = 1 then
            intLine = 1
        else
            intLine = 4
        end if

   	    strSQL= " Insert into REC(LocationID,recID,clientid, esttime, vehID, VehMan,VModel, VehMod, VehColor,"&_
             "  datein,SalesRep,DayCnt,estMin,line)Values(" & _
				Application("LocationID") & ", " & _
				intrecID & ", " & _
				intclientid & ", " & _
				"'"& dtesttime &"'," & _
				newVehID & ", " & _
				intMake & ",'" & _
				strVModel & "', " & _
				intmodel & ", " & _
				intColor & ", " & _
				"'"& now() &"'," & _
				intUserID & "," & _
				intDayCnt & "," & _
				"'"& strEstMin &"'," & _
  				intLine & ") " 
	    if not (dbExec(dbMain,strSQL)) Then
		    blnUpdatePass = 0
 		end if

		intProdID = 0
		if strDept = 1 then
			isCeramic = 0
			IF Application("LocationID") = 3 then
				Select Case strWash
					Case 1
						intProdID = 461
					Case 2
						intProdID = 462
					Case 3
						intProdID = 463
					Case 4
						intProdID = 464
					Case 5
						intProdID = 319
				END Select
			else
				Select Case strWash
					Case 1
						intProdID = 293
					Case 2
						intProdID = 3
					Case 3
						intProdID = 2
					Case 4
						intProdID = 1
					Case 5
						intProdID = 319
				END Select
			end if
		else
			Select Case strWash
				Case 1
					intProdID = 6
					isCeramic = 0
				Case 2
					intProdID = 11
					isCeramic = 1
				Case 3
					intProdID = 12
					isCeramic = 0
				Case 4
					intProdID = 13
					isCeramic = 0
				Case 5
					intProdID = 14
					isCeramic = 1
				Case 6
					intProdID = 39
					isCeramic = 1
				Case 7
					intProdID = 40
					isCeramic = 0
				Case 8
					intProdID = 74
					isCeramic = 0
				Case 9
					intProdID = 75
					isCeramic = 1
			END Select
        end if
        if intProdID > 0 then
            strSQL = "Select Price,Comm From product where (Dept="& strDept &")  AND (cat ="& strDept &") and (ProdID="& intProdID &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    strPrice = rsData("Price")
				    strComm = rsData("Comm")
			    else
				    strPrice = 0.0
				    strComm = 0.0
			    end if
		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if

		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				    intProdID & ", " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		    blnUpdatePass = 0
 		    end if
        end if

		intProdID = 0
		if strDept = 1 then
        	Select Case strUPCHARGE
				Case 1
					intProdID = 5
				Case 2
					intProdID = 7
				Case 3
					intProdID = 8
				Case 4
					intProdID = 9
				Case 5
					intProdID = 10
				Case 6
					intProdID = 409
				Case 7
					intProdID = 470
			END Select
		else
			Select Case strUPCHARGE
				Case 1
					intProdID = 16
				Case 2
					intProdID = 17
				Case 3
					intProdID = 18
				Case 4
					intProdID = 19
				Case 5
					intProdID = 20
				Case 6
					intProdID = 469
				Case 7
					intProdID = 471
			END Select
		end if

		if intProdID > 0 then
			if intProdID = 16 or intProdID = 17 or intProdID = 18 or intProdID = 19 or intProdID = 20 or intProdID = 469 or intProdID = 471 then
				strSQL = "Select CASE WHEN "& isCeramic &" = 1 then Product.Price2 ELSE Product.Price END as Price,"&_
					" CASE WHEN "& isCeramic &" = 1 then Product.Comm2 ELSE Product.Comm END as Comm From product where (ProdID="& intProdID &")"
			else
				strSQL = "Select Price,Comm From product where (ProdID="& intProdID &")"
			end if


		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    strPrice = rsData("Price")
				    strComm = rsData("Comm")
			    else
				    strPrice = 0.0
				    strComm = 0.0
			    end if
		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				    intProdID & ", " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		    blnUpdatePass = 0
 		    end if
        end if       

		intProdID = 0
        if strAir > 0 then
            select case strAir
                case 1
                    intProdID = 60
                case 2
                    intProdID = 62
                case 3
                    intProdID = 63
                case 4
                    intProdID = 64
                case 5
                    intProdID = 69
               end select 
	       if intProdID > 0 then
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				        intrecID & ", " & _
				        intrecItemID & ", " & _
				        intProdID & ", " & _
				        "0.0," & _
				        "0.0,1)" 
		        if not (dbExec(dbMain,strSQL)) Then
		            blnUpdatePass = 0
 		        end if
            end if
        end if
		intProdID = 0
		if strDept = 2 then
            if strNoEng = 0 then
		        strSQL = "Select Price,Comm From product where (ProdID=15)"
		        if DBOpenRecordset(dbMain,rsData,strSQL) Then
			        if not rsData.eof then
				        strPrice = rsData("Price")
				        strComm = rsData("Comm")
			        else
				        strPrice = 0.0
				        strComm = 0.0
			        end if
		        end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				        intrecID & ", " & _
				        intrecItemID & ", " & _
				        "15, " & _
				        strPrice &"," & _
				        strComm &",1)" 
		        if not (dbExec(dbMain,strSQL)) Then
		            blnUpdatePass = 0
 		        end if
            end if
        end if

        'Clean Inside Rims
        If mid(strAddService,1,1)="1" then    
            intNocharge = 0
		    strSQL = "SELECT CASE WHEN fname LIKE '%rims%' THEN 1 ELSE 0 END AS nocharge FROM dbo.client WHERE (clientid = "& intClientID &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    intNocharge = rsData("nocharge")
			    else
				    intNocharge = 0
			    end if
		    end if
            if intNocharge = 0 then
		        strSQL = "Select Price,Comm From product where (ProdID=35)"
		        if DBOpenRecordset(dbMain,rsData,strSQL) Then
			        if not rsData.eof then
				        strPrice = rsData("Price")
				        strComm = rsData("Comm")
			        else
				        strPrice = 0.0
				        strComm = 0.0
			        end if
		        end if
            else
				strPrice = 0.0
				strComm = 0.0
            end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				    "35, " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		        blnUpdatePass = 0
 		    end if
        end if


         'Shampoo Floormat
        If mid(strAddService,2,1)="1" then    
		    strSQL = "Select Price,Comm From product where (ProdID=27)"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    strPrice = rsData("Price")
				    strComm = rsData("Comm")
			    else
				    strPrice = 0.0
				    strComm = 0.0
			    end if
		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				    "27, " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		        blnUpdatePass = 0
   		    end if
        end if
         If mid(strAddService,2,1)="2" then    
		    strSQL = "Select Price,Comm From product where (ProdID=47)"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    strPrice = rsData("Price")
				    strComm = rsData("Comm")
			    else
				    strPrice = 0.0
				    strComm = 0.0
			    end if
		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				    "47, " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		        blnUpdatePass = 0
 		    end if
        end if
        If mid(strAddService,2,1)="3" then    
		    strSQL = "Select Price,Comm From product where (ProdID=48)"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    strPrice = rsData("Price")
				    strComm = rsData("Comm")
			    else
				    strPrice = 0.0
				    strComm = 0.0
			    end if
		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				    "48, " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		        blnUpdatePass = 0
 		    end if
        end if
        If mid(strAddService,2,1)="4" then    
		    strSQL = "Select Price,Comm From product where (ProdID=447)"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    strPrice = rsData("Price")
				    strComm = rsData("Comm")
			    else
				    strPrice = 0.0
				    strComm = 0.0
			    end if
		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				    "447, " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		        blnUpdatePass = 0
 		    end if
        end if
        'Shampoo Carpets
        If mid(strAddService,3,1)="1" then      
		    strSQL = "Select Price,Comm From product where (ProdID=119)"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    strPrice = rsData("Price")
				    strComm = rsData("Comm")
			    else
				    strPrice = 0.0
				    strComm = 0.0
			    end if
		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				    "119, " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		        blnUpdatePass = 0
		    end if
        end if
        If mid(strAddService,3,1)="2" then      
		    strSQL = "Select Price,Comm From product where (ProdID=120)"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    strPrice = rsData("Price")
				    strComm = rsData("Comm")
			    else
				    strPrice = 0.0
				    strComm = 0.0
			    end if
		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				    "120, " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		        blnUpdatePass = 0
		    end if
        end if
        If mid(strAddService,3,1)="3" then      
		    strSQL = "Select Price,Comm From product where (ProdID=121)"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    strPrice = rsData("Price")
				    strComm = rsData("Comm")
			    else
				    strPrice = 0.0
				    strComm = 0.0
			    end if
		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				    "121, " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		        blnUpdatePass = 0
		    end if
        end if
        If mid(strAddService,3,1)="4" then      
		    strSQL = "Select Price,Comm From product where (ProdID=25)"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    strPrice = rsData("Price")
				    strComm = rsData("Comm")
			    else
				    strPrice = 0.0
				    strComm = 0.0
			    end if
		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				    "25, " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		        blnUpdatePass = 0
		    end if
        end if

 
 
 
        'Shampoo Seat
        If mid(strAddService,4,1)="1" then         
		    strSQL = "Select Price,Comm From product where (ProdID=131)"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    strPrice = rsData("Price")
				    strComm = rsData("Comm")
			    else
				    strPrice = 0.0
				    strComm = 0.0
			    end if
		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				     "131, " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		        blnUpdatePass = 0
		    end if
        end if
        If mid(strAddService,4,1)="2" then         
		    strSQL = "Select Price,Comm From product where (ProdID=127)"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    strPrice = rsData("Price")
				    strComm = rsData("Comm")
			    else
				    strPrice = 0.0
				    strComm = 0.0
			    end if
		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				     "127, " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		        blnUpdatePass = 0
		    end if
        end if
        If mid(strAddService,4,1)="3" then         
		    strSQL = "Select Price,Comm From product where (ProdID=128)"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    strPrice = rsData("Price")
				    strComm = rsData("Comm")
			    else
				    strPrice = 0.0
				    strComm = 0.0
			    end if
		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				     "128, " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		        blnUpdatePass = 0
		    end if
        end if
        If mid(strAddService,4,1)="4" then         
		    strSQL = "Select Price,Comm From product where (ProdID=129)"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    strPrice = rsData("Price")
				    strComm = rsData("Comm")
			    else
				    strPrice = 0.0
				    strComm = 0.0
			    end if
		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				     "129, " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		        blnUpdatePass = 0
		    end if
        end if

        'Shampoo Spot 
        If mid(strAddService,5,1)="1" then    
		    strSQL = "Select Price,Comm From product where (ProdID=187)"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof then
				    strPrice = rsData("Price")
				    strComm = rsData("Comm")
			    else
			    strPrice = 0.0
				    strComm = 0.0
			    end if
		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
				    intrecID & ", " & _
				    intrecItemID & ", " & _
				    "187, " & _
				    strPrice &"," & _
				    strComm &",1)" 
		    if not (dbExec(dbMain,strSQL)) Then
		        blnUpdatePass = 0
		    end if
        end if

        ' Clean Leather Seats
         If mid(strAddService,6,1)="1" then  
 		    strSQL = "Select Price,Comm From product where (ProdID=122)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "122, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
         If mid(strAddService,6,1)="2" then  
 		    strSQL = "Select Price,Comm From product where (ProdID=123)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "123, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
         If mid(strAddService,6,1)="3" then  
 		    strSQL = "Select Price,Comm From product where (ProdID=138)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "138, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Clean Door Panel
        If mid(strAddService,7,1)="1" then      
 		    strSQL = "Select Price,Comm From product where (ProdID=36)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "36, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
         If mid(strAddService,7,1)="2" then      
 		    strSQL = "Select Price,Comm From product where (ProdID=42)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "42, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
        If mid(strAddService,7,1)="3" then      
 		    strSQL = "Select Price,Comm From product where (ProdID=41)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "41, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
 
        'Dog Hair
         If mid(strAddService,8,1)="1" then              
 		    strSQL = "Select Price,Comm From product where (ProdID=444)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "444, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
         If mid(strAddService,8,1)="2" then              
 		    strSQL = "Select Price,Comm From product where (ProdID=442)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "442, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
         If mid(strAddService,8,1)="3" then              
 		    strSQL = "Select Price,Comm From product where (ProdID=448)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "448, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Cond. Leather Seats
         If mid(strAddService,9,1)="1" then  
 		    strSQL = "Select Price,Comm From product where (ProdID=57)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "57, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
        If mid(strAddService,9,1)="2" then  
 		    strSQL = "Select Price,Comm From product where (ProdID=126)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "126, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
        If mid(strAddService,9,1)="3" then  
 		    strSQL = "Select Price,Comm From product where (ProdID=148)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "148, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

          'Condition Dash      
         If mid(strAddService,10,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=249)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "249, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Condition Door (per)
         If mid(strAddService,11,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=141)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "141, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
         If mid(strAddService,11,1)="2" then
 		    strSQL = "Select Price,Comm From product where (ProdID=142)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "142, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
         If mid(strAddService,11,1)="3" then
 		    strSQL = "Select Price,Comm From product where (ProdID=143)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "143, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
         If mid(strAddService,11,1)="4" then
 		    strSQL = "Select Price,Comm From product where (ProdID=144)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "144, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Excessive Mud       
         If mid(strAddService,12,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=423)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "423, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
        If mid(strAddService,12,1)="2" then
 		    strSQL = "Select Price,Comm From product where (ProdID=425)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "425, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
        If mid(strAddService,12,1)="3" then
 		    strSQL = "Select Price,Comm From product where (ProdID=424)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "424, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Silicone Ext Rubber 
         If mid(strAddService,13,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=428)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "428, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
        If mid(strAddService,13,1)="2" then
 		    strSQL = "Select Price,Comm From product where (ProdID=429)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "429, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if


        ' Detail Add-Ons      
        If mid(strAddService,14,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=179)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "179, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
        If mid(strAddService,14,1)="2" then
 		    strSQL = "Select Price,Comm From product where (ProdID=222)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "222, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Clean Engine        
         If mid(strAddService,15,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=32)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "32, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Ultra Clrcoat prtect
         If mid(strAddService,16,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=294)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "294, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'NO Silicone         
         If mid(strAddService,17,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=67)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "67, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'NO Brush/ NO Sapillo
         If mid(strAddService,18,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=68)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "68, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Wash Mitts          
         If mid(strAddService,19,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=426)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "426, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if
         If mid(strAddService,19,1)="2" then
 		    strSQL = "Select Price,Comm From product where (ProdID=427)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "427, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Rain X Protection   
         If mid(strAddService,20,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=22)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "22, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Clean Truck Bed     
         If mid(strAddService,21,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=37)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "37, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Bio Oder Remover    
         If mid(strAddService,22,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=166)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "166, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Sticker removal     
         If mid(strAddService,23,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=316)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "316, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Ozone treatment     
         If mid(strAddService,24,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=325)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "325, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Headliner Cleaned   
         If mid(strAddService,25,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=284)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "284, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Fabric Sealer       
         If mid(strAddService,26,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=24)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "24, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Vacuum Trunk        
         If mid(strAddService,27,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=21)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "21, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if

         'Headlamp Restoration
         If mid(strAddService,28,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=419)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "419, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 		        blnUpdatePass = 0
		    end if
         end if


        'Plastic Restoration 
         If mid(strAddService,29,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=420)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "420, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			        blnUpdatePass = 0
	    end if
         end if

         'Remove & Clean Bra  
         If mid(strAddService,30,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=314)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "314, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			    blnUpdatePass = 0
 		    end if
         end if
 
      ' DAILY DUTIES     
         If mid(strAddService,31,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=393)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "393, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			    blnUpdatePass = 0
 		    end if
         end if

       'PREP VEHICLES           
         If mid(strAddService,32,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=391)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "391, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			    blnUpdatePass = 0
 		    end if
         end if

      ' PHOTO OF VEHICLE     
         If mid(strAddService,33,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=392)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "392, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			    blnUpdatePass = 0
 		    end if
         end if
      ' Clay Bar     
         If mid(strAddService,34,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=53)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "53, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			    blnUpdatePass = 0
 		    end if
         end if
        If mid(strAddService,34,1)="2" then
 		    strSQL = "Select Price,Comm From product where (ProdID=54)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "54, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			    blnUpdatePass = 0
 		    end if
         end if
        If mid(strAddService,34,1)="3" then
 		    strSQL = "Select Price,Comm From product where (ProdID=160)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "160, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			    blnUpdatePass = 0
 		    end if
         end if
        If mid(strAddService,34,1)="4" then
 		    strSQL = "Select Price,Comm From product where (ProdID=55)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "55, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			    blnUpdatePass = 0
 		    end if
         end if
        If mid(strAddService,34,1)="5" then
 		    strSQL = "Select Price,Comm From product where (ProdID=275)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "275, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			    blnUpdatePass = 0
 		    end if
         end if


      ' Wet Sand     
         If mid(strAddService,35,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=156)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "156, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			    blnUpdatePass = 0
 		    end if
         end if
         If mid(strAddService,35,1)="2" then
 		    strSQL = "Select Price,Comm From product where (ProdID=157)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "157, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			    blnUpdatePass = 0
 		    end if
         end if
         If mid(strAddService,35,1)="3" then
 		    strSQL = "Select Price,Comm From product where (ProdID=158)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "158, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			    blnUpdatePass = 0
 		    end if
         end if
      ' Buffout     
         If mid(strAddService,36,1)="1" then
 		    strSQL = "Select Price,Comm From product where (ProdID=134)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "134, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			    blnUpdatePass = 0
 		    end if
         end if
         If mid(strAddService,36,1)="2" then
 		    strSQL = "Select Price,Comm From product where (ProdID=132)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "132, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			    blnUpdatePass = 0
 		    end if
         end if
         If mid(strAddService,36,1)="3" then
 		    strSQL = "Select Price,Comm From product where (ProdID=133)"
 		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
 			    if not rsData.eof then
 				    strPrice = rsData("Price")
 				    strComm = rsData("Comm")
 			    else
 				    strPrice = 0.0
 				    strComm = 0.0
 			    end if
 		    end if
		    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem WHERE (LocationID = "& Application("LocationID") &")"
		    if DBOpenRecordset(dbMain,rsData,strSQL) Then
			    intrecItemID = rsData("recItemID")
		    end if
		    strSQL= " Insert into RECItem(LocationID,recID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				    Application("LocationID") & ", " & _
 				    intrecID & ", " & _
 				    intrecItemID & ", " & _
 				    "133, " & _
 				    strPrice &"," & _
 				    strComm &",1)" 
 		    if not (dbExec(dbMain,strSQL)) Then
 			    blnUpdatePass = 0
 		    end if
         end if
        if intCType = 0 then
	        strSQL= " Insert into PrintTicket(LocationID,recID,Printer,Report)Values(" & Application("LocationID") & "," & intrecID & ",1,'Ticket')" 
	        If NOT (dbExec(dbMain,strSQL)) Then
			            blnUpdatePass = 0
 	        End If
        End IF
 
 
'******* end of line ********'
		blnUpdatePass = 1

    else
        blnUpdatePass = 2
    end if
else
    blnUpdatePass = 3
end if


Jout = "{""success"":"& blnUpdatePass &"}"
response.write(Jout)
Call CloseConnection(dbMain)
%>