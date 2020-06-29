<%@  language="VBSCRIPT" %>
<%
'********************************************************************
' Name: 
'********************************************************************
Option Explicit
Response.Expires = 0
Response.Buffer = True

Dim Title
Dim gstrMessage

'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incDatabase.asp"-->
<!--#include file="inccommon.asp"-->
<%
'********************************************************************
' Global Variables
'********************************************************************
Dim strNextPage

'********************************************************************
' Main
'********************************************************************
Main

'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
Sub Main

	Dim dbMain, intrecID,intClientID,dtesttime, intvehid, intVehMan, intVehMod, intVehColor 
    Dim dtdatein, intSalesRep, intStatus, txtNotes, intLine, intDayCnt
	Dim rsData, strSQL ,optDetail,optEngCln ,optUpCharge ,intrecItemID
	Dim strPrice,strComm,intAddCnt,optAddSer,dtDate,strVModel,intUPC,optAir,strUPCHARGE
	Dim rsData2, strSQL2 ,intProdID,intQty,strDescript,optArr,strEstMin,decLabor,LocationID,LoginID,blSave,hdnProdID,newProdID

'response.write "start 1 "&now()&"<br />"



	Set dbMain =  OpenConnection
	intrecID = request("intrecID")
    LocationID = request("LocationID")
    LoginID = request("LoginID")
    hdnProdID = request("hdnProdID")
    newProdID = request("newProdID")
    blSave = 0
	dtDate = request("dtDate")
	Select case Request("FormAction")
		Case "CompleteChg"
			Call SaveChg(dbMain)
			Call CompleteChg(dbMain)
		Case "SaveChg"
			Call SaveChg(dbMain)
            blSave = 1
		Case "SaveChg1"
			Call SaveChg1(dbMain)
		Case "SaveChg2"
			Call SaveChg2(dbMain)
		Case "SaveChg3"
			Call SaveChg3(dbMain)
		Case "SaveChg4"
			Call SaveChg4(dbMain)
		Case "PrintChg"
			Call PrintChg(dbMain)
		Case "DeleteChg"
			Call DeleteChg(dbMain)
           blSave = 1
		Case "UPCChg"
			Call UPCChg(dbMain)
		Case "ClientChg"
			Call ClientChg(dbMain)
		Case "AddClient"
			Call AddClient(dbMain)
		Case "VehicalChg"
			Call VehicalChg(dbMain)
		Case "VehModChg"
			Call VehModChg(dbMain)
		Case "TimeChg"
			Call TimeChg(dbMain)
	End select

	IF intrecID = 0 then

		intClientID = 2
		intvehid = 0
		intVehMan = 0
		intVehMod = 0
		strVModel = ""
		intVehColor = 0
		optDetail = 1
		optUpCharge = 0

  '  strSQL = "exec stp_Labor " & LocationID    
  '  IF not DBExec(dbMain, strSQL) then
'	    Response.Write gstrmsg
'	    Response.end
 '   END IF
	
'   strSQL = "exec stp_getCurrWaitTime " & LocationID    
 '   IF not DBExec(dbMain, strSQL) then
'	    Response.Write gstrmsg
'	    Response.end
 '   END IF
		
		strSQL = "SELECT CurrentWaitTime,Labor FROM stats with(nolock)  where LocationID="&LocationID
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
            IF NOT rsData.eof then
			    IF instr(1,rsData("CurrentWaitTime")," Min")>0 then		
				    dtesttime = dateadd("n",int(replace(rsData("CurrentWaitTime")," Min","")),NOW())
            			    strEstMin = rsData("CurrentWaitTime")
                            decLabor = rsData("Labor") 
			    else
				    dtesttime = dateadd("n",25,NOW())
            			    strEstMin = rsData("CurrentWaitTime")
                            decLabor = rsData("Labor") 

			    end if
            else
                dtesttime = dateadd("n",25,NOW())
                strEstMin = "25"
                decLabor = 0 
            end if
		End If
		dtdatein = dtDate
		intLine = 1
		strSQL = "Select recID=IsNull(Max(recID),0) + 1 From Rec with(nolock)  where LocationID="&LocationID
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			intrecID = rsData("recID")
		End If
		strSQL = "Select DayCnt=IsNull(Max(DayCnt),0) + 1 From Rec with(nolock) "&_
				" where year(Rec.datein)=" & year(dtDate) &_
				" AND month(Rec.datein)=" & Month(dtDate) &_
				" AND Day(Rec.datein)=" & Day(dtDate) &_
				" AND Rec.LocationID=" & LocationID 

		If DBOpenRecordset(dbMain,rsData,strSQL) Then
            IF NOT  rsData.eof then
			    intDayCnt = rsData("DayCnt")
            else
                intDayCnt = 1
            end if
		End If
		strSQL= " Insert into REC(recID,LocationID,ClientID, esttime, vehid, VehMan,VModel, VehMod, VehColor,"&_
				" datein, SalesRep,DayCnt,estMin,line,Labor)Values(" & _
				intrecID & ", " & _
				LocationID & ", " & _
				intClientID & ", " & _
				"'"& dtesttime &"'," & _
				intvehid & ", " & _
				intVehMan & ",'" & _
				strVModel & "', " & _
				intVehMod & ", " & _
				intVehColor & ", " & _
				"'"& now() &"'," & _
				LoginID & "," & _
				intDayCnt & "," & _
				"'"& strEstMin &"'," & _
				intLine &"," & _
				decLabor & ") " 
		If NOT (dbExec(dbMain,strSQL)) Then
			Response.Write gstrMsg
		End If
		strSQL = "Select Price,Comm From product(nolock) where ProdID=1"
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			IF NOT rsData.eof then
				strPrice = rsData("Price")
				strComm = rsData("Comm")
			ELSE
				strPrice = 0.0
				strComm = 0.0
			END IF
		End If
		strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem with(nolock)  where RecItem.LocationID="& LocationID
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			intrecItemID = rsData("recItemID")
		End If
		strSQL= " Insert into RECItem(recID,LocationID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				intrecID & ", " & _
				LocationID & ", " & _
				intrecItemID & ", " & _
				"1, " & _
				strPrice &"," & _
				strComm &",1)" 
		If NOT (dbExec(dbMain,strSQL)) Then
			Response.Write gstrMsg
		End If
		strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem with(nolock)  where RecItem.LocationID="& LocationID
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			intrecItemID = rsData("recItemID")
		End If
		strSQL= " Insert into RECItem(recID,LocationID,recItemID,Prodid,Price,Comm,Qty)Values(" & _
				intrecID & ", " & _
				LocationID & ", " & _
				intrecItemID & ", " & _
				"59,0,0,1)" 
		If NOT (dbExec(dbMain,strSQL)) Then
			Response.Write gstrMsg
		End If


	ELSE
		strSQL = "Select ClientID,esttime,vehid,VehMan,VModel,VehMod,VehColor,datein,Notes,SalesRep,Line,DayCnt,status,EstMin From REC with(nolock)  WHERE RECID=" & intRECID &" and REC.LocationID="& LocationID
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			if not rsData.eof THEN
				intClientID = rsData("ClientID")
				dtesttime = rsData("esttime")
				intvehid = rsData("vehid")
				intVehMan = rsData("VehMan")
				strVModel = rsData("VModel")
				intVehMod = rsData("VehMod")
				intVehColor = rsData("VehColor")
				dtdatein = rsData("datein")
				txtNotes = rsData("Notes")
				intSalesRep = rsData("SalesRep")
				intLine = rsData("Line")
				intDayCnt = rsData("DayCnt")
				intstatus = rsData("status")
                strEstMin = rsData("EstMin")
			END IF
		END IF
        If intvehid > 0 then
		    strSQL = "Select UPC From Vehical with(nolock)  WHERE vehid=" & intvehid
		    If DBOpenRecordset(dbMain,rsData,strSQL) Then
			    if not rsData.eof THEN
				    intUPC = rsData("upc")
			    END IF
		    END IF
        end if



		strSQL = "SELECT RECITEM.Prodid, Product.cat AS cat FROM RECITEM(nolock) "&_
		" INNER JOIN Product(NOLOCK) ON RECITEM.ProdID = Product.ProdID "&_
		" WHERE RECITEM.RECID = " & intRECID & " RECITEM.LocationID = "&LocationID
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			DO While NOT rsData.eof 
				Select Case rsData("Prodid")
				Case  1
					optDetail = 1
				case  2
					optDetail = 2
				case  3
					optDetail = 3
				case  5
					optUpCharge = 1
				case  7
					optUpCharge = 2
				case  8
					optUpCharge = 3
				case  9
					optUpCharge = 4
				case  10
					optUpCharge = 5
				case  409
					optUpCharge = 6
				case  470
					optUpCharge = 7
				End Select
				IF rsData("cat")=21 then
					optAIR = rsData("Prodid")
				END IF
				rsData.MoveNext
			Loop
		END IF
	END IF
%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css" />
    <title></title>
</head>
<body class="pgbody">
    <form method="post" name="frmMain" action="NewWashDlgFra.asp">
        <div style="text-align: center;">
            <input type="hidden" name="intRECID" value="<%=intRECID%>" />
            <input type="hidden" name="dtDate" value="<%=dtDate%>" />
            <input type="hidden" name="blSave" value="<%=blSave%>" />
            <input type="hidden" name="intClientID" value="<%=intClientID%>" />
            <input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
            <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
            <input type="hidden" name="intvehid" value="<%=intvehid%>" />
            <input type="hidden" name="intvehMod" value="<%=intvehMod%>" />
            <input type="hidden" name="dtdatein" value="<%=dtdatein%>" />
            <input type="hidden" name="dtesttime" value="<%=dtesttime%>" />
            <input type="hidden" name="intstatus" value="<%=intstatus%>" />
            <input type="hidden" name="intDayCnt" value="<%=intDayCnt%>" />
            <input type="hidden" name="strEstMin" value="<%=strEstMin%>" />
            <input type="hidden" name="hdnProdID" value="<%=hdnProdID%>" />
            <input type="hidden" name="newProdID" value="<%=newProdID%>" />
            <table border="0" style="width: 100%; border-collapse: collapse;">
                <tr>
                    <td class="control" style="text-align: left; white-space: nowrap;">#:<%=intDayCnt%>&nbsp;Ticket #:<%=intRECID%></td>
                    <td class="control" style="text-align: right; white-space: nowrap;">In :<%=dtdatein%></td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
      <td class="control" style="text-align: right; white-space: nowrap;">Est. Time:<%=dtesttime%></td>
                    <td class="control" style="text-align: right; white-space: nowrap;">Est. Min:<%=strEstMin%></td>
                </tr>

            </table>
            <hr>
            <table border="0" style="width: 100%; border-collapse: collapse;">
                <tr>
                    <td>
                        <label class="control" style="text-align: right; white-space: nowrap; vertical-align: text-top; width: 50px;">Bar Code:</label></td>
                    <td>
                        <input maxlength="8" size="8" type="text" tabindex="1" name="intUPC" value="<%=intUPC%>" onkeyup="UPCChg()"></td>
                    <td>
                        <label class="control" style="text-align: right; white-space: nowrap; vertical-align: text-top">Client:</label></td>
                    <td>
                        <label class="control" style="text-align: left; white-space: nowrap; vertical-align: text-top; width: 300px;">
                            <%
    strSQL="SELECT FName + ' ' + LName AS name FROM Client with(nolock)  where ClientID="& intClientID 
	If dbOpenRecordSet(dbMain,rsData,strSQL) Then
		IF Not rsData.EOF Then
                            %>
                            <%=rsData("name")%>
                            <%
		Else 
                            %>
		    New Add
		<%
        end if
	End If
        %></label>
                        &nbsp;&nbsp;&nbsp;
		<button class="button" onclick="SelClient()" style="width: 120px" id="button1" name="button1">Select</button>&nbsp;&nbsp;&nbsp;
		<button class="button" onclick="AddClient1()" style="width: 120px" id="button2" name="button2">Add Client</button>
                    </td>
                </tr>
            </table>
            <table border="0" style="width: 100%; border-collapse: collapse;">
                <% IF intClientID < 3 then %>
                <tr>
                    <td colspan="2">
                        <label class="control" style="text-align: right; white-space: nowrap; vertical-align: text-top; width: 50px;">Vehical:</label></td>
                    <td>
                        <label class="control" style="text-align: left; white-space: nowrap; vertical-align: text-top; width: 100px;">Type:</label></td>
                    <td>
                        <label class="control" style="text-align: left; white-space: nowrap; vertical-align: text-top; width: 100px;">Model:</label></td>
                    <td>
                        <label class="control" style="text-align: left; white-space: nowrap; vertical-align: text-top; width: 100px;">UpCharge:</label></td>
                    <td>
                        <label class="control" style="text-align: left; white-space: nowrap; vertical-align: text-top; width: 100px;">Color:</label></td>
                </tr>
                <tr>
                    <td>
                        <label class="control" style="text-align: right; white-space: nowrap; vertical-align: text-top; width: 50px;">&nbsp;</label></td>
                    <td>
                    <td style="text-align: left; white-space: nowrap;">
                        <select name="cboVehMan" tabindex="1">
                            <%Call LoadListA(dbMain,3,intVehMan)%>
                        </select></td>
                    <td style="text-align: left; white-space: nowrap;">
                        <input maxlength="40" size="20" type="text" tabindex="2" name="strVModel" value="<%=strVModel%>" /></td>
                    <td style="text-align: left; white-space: nowrap;">
                        <select name="cboVehMod" tabindex="1" onclick="VehModChg()">
                            <%Call LoadList(dbMain,4,intVehMod)%>
                        </select></td>
                    <td style="text-align: left; white-space: nowrap;">
                        <select name="cboVehColor" tabindex="1">
                            <%Call LoadListA(dbMain,5,intVehColor)%>
                        </select></td>
                </tr>
                <% ELSE %>
                <tr>
                    <td>
                        <label class="control" style="text-align: right; white-space: nowrap; vertical-align: text-top; width: 50px;">Vehical:</label></td>
                    <td colspan="3" class="control" style="text-align: left; white-space: nowrap;">
                        <select name="cbovehid" tabindex="1" onclick="VehicalChg()">
                            <%Call LoadVehical(dbMain,intClientID,intvehid)%>
                        </select>&nbsp;&nbsp;&nbsp;
		<button name="btnAddVeh" class="button" onclick="AddVeh()" style="width: 95px; text-align: center">Add Vehical</button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="control" style="text-align: right; white-space: nowrap; vertical-align: text-top; width: 50px;">&nbsp;</label></td>
                    <td>
                        <label class="control" style="text-align: left; white-space: nowrap; vertical-align: text-top; width: 100px;">Type:</label></td>
                    <td>
                        <label class="control" style="text-align: left; white-space: nowrap; vertical-align: text-top; width: 100px;">Model:</label></td>
                    <td>
                        <label class="control" style="text-align: left; white-space: nowrap; vertical-align: text-top; width: 100px;">UpCharge:</label></td>
                    <td>
                        <label class="control" style="text-align: left; white-space: nowrap; vertical-align: text-top; width: 100px;">Color:</label></td>
                </tr>
                <tr>
                    <td>
                        <label class="control" style="text-align: right; white-space: nowrap; vertical-align: text-top; width: 50px;">&nbsp;</label></td>
                    <td style="text-align: left; white-space: nowrap;">
                        <select name="cboVehMan" tabindex="1">
                            <%Call LoadListA(dbMain,3,intVehMan)%>
                        </select></td>
                    <td style="text-align: left; white-space: nowrap;">
                        <input maxlength="40" size="20" type="text" tabindex="2" name="strVModel" value="<%=strVModel%>" /></td>
                    <td style="text-align: left; white-space: nowrap;">
                        <select name="cboVehMod" tabindex="1" onclick="VehModChg()">
                            <%Call LoadList(dbMain,4,intVehMod)%>
                        </select></td>
                    <td class="control" style="text-align: left; white-space: nowrap;">
                        <select name="cboVehColor" tabindex="1">
                            <%Call LoadListA(dbMain,5,intVehColor)%>
                        </select></td>
                </tr>
                <% END IF 
                   
                %>
            </table>
            <hr>
            <table border="0" style="width: 100%; border-collapse: collapse;">
                <tr>
                    <td style="vertical-align: top">
                        <table border="0" style="width: 200px; border-collapse: collapse;">
                            <tr>
                                <td colspan="2" class="control" style="text-align: center; white-space: nowrap;">Washes:</td>
                            </tr>
                            <%
		if LocationID = 3 then
			strSQL = "SELECT Product.Descript,Product.ProdID,RECITEM.QTY FROM Product (NOLOCK)"&_
			" LEFT OUTER JOIN RECITEM(NOLOCK) ON Product.ProdID = RECITEM.ProdID"&_
			" AND RECITEM.recid =" & intRECID &_
			" AND RECITEM.LocationID =" & LocationID &_
			" WHERE (Product.cat = 1) and Product.ProdID not in (293,1,2,3) ORDER BY Product.Number"
		else
			strSQL = "SELECT Product.Descript,Product.ProdID,RECITEM.QTY FROM Product (NOLOCK)"&_
			" LEFT OUTER JOIN RECITEM(NOLOCK) ON Product.ProdID = RECITEM.ProdID"&_
			" AND RECITEM.recid =" & intRECID &_
			" AND RECITEM.LocationID =" & LocationID &_
			" WHERE (Product.cat = 1) and Product.ProdID not in (461,462,463,464) ORDER BY Product.Number"

		end if

		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			DO While NOT rsData.eof 

			optAir = "optDetail"+cstr(rsData("ProdID"))

                            %>
                            <tr>
                                <% IF len(rsData("QTY")) > 0 THEN%>
                                <td colspan="2">
                                    <input tabindex="18" type="Radio" id="optDetail" name="optDetail" value='<%=rsData("ProdID")%>' checked  onclick="CheckClick1()"><label class="data" for="optDetail"><%=rsData("Descript")%></label></td>
                                <% ELSE %>
                                <td colspan="2">
                                    <input tabindex="18" type="Radio" id="optDetail" name="optDetail" value='<%=rsData("ProdID")%>'  onclick="CheckClick1()"><label class="data" for="optDetail"><%=rsData("Descript")%></label></td>
                                <% END IF %>
                            </tr>

                            <%
				rsData.MoveNext
			Loop
		END IF
		Set rsData = Nothing
                            %>
                        </table>

                    </td>
                    <td style="vertical-align: top">
                        <table border="0" style="width: 200px; border-collapse: collapse;">
                            <tr>
                                <td colspan="2" class="control" style="text-align: center; white-space: nowrap;">Upcharges:</td>
                                <%
		strSQL = "SELECT Product.ProdID, RECITEM.QTY, "&_
		" LM_ListItem.ListDesc  AS Descript, Product.Price"&_
		" FROM Product(NOLOCK)"&_
		" INNER JOIN LM_ListItem (NOLOCK) ON Product.cat = LM_ListItem.ListValue"&_
		" AND LM_ListItem.ListType = 1"&_
		" LEFT OUTER JOIN RECITEM(NOLOCK) ON Product.ProdID = RECITEM.ProdID"&_
		" AND RECITEM.recId =" & intRECID &_
		" AND RECITEM.LocationID =" & LocationID &_
		" WHERE (Product.Descript = 'Large Vehicle') AND (Product.dept = 1)"&_
		" ORDER BY Product.Number"
                                %>
                                <tr>
                                    <td style="text-align: right; white-space: nowrap;">&nbsp;</td>
                                    <td style="text-align: left; white-space: nowrap;">
                                        <input tabindex="18" type="Radio" id="optUpCharge" name="optUpCharge" value="0" checked  onclick="CheckClick2()"><label class="data" for="optUpCharge">(None)</label></td>
                                </tr>
                                <%

		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			DO While NOT rsData.eof 

			optUpCharge = "optUpCharge"+cstr(rsData("ProdID"))
			strUPCHARGE = "("+MID(trim(rsData("Descript")),instr(1,trim(rsData("Descript")),"(")+1,1)+" - $"+cstr(rsData("Price"))+")"

                                %>
                                <tr>
                                    <td style="text-align: right; white-space: nowrap;">&nbsp;</td>

                                    <% IF len(rsData("QTY")) > 0 THEN%>
                                    <td style="text-align: left; white-space: nowrap;">
                                        <input tabindex="18" type="Radio" id="optUpCharge" name="optUpCharge" value='<%=rsData("ProdID")%>' checked  onclick="CheckClick2()"><label class="data" for="optUpCharge"><%=strUPCHARGE%></label></td>
                                    <% ELSE %>
                                    <td style="text-align: left; white-space: nowrap;">
                                        <input tabindex="18" type="Radio" id="optUpCharge" name="optUpCharge" value='<%=rsData("ProdID")%>' onclick="CheckClick2()"><label class="data" for="optUpCharge"><%=strUPCHARGE%></label></td>
                                    <% END IF %>
                                </tr>

                                <%
				rsData.MoveNext
			Loop
		END IF
		Set rsData = Nothing
                                %>
                            </tr>
                            <tr>
                        </table>
                        <table border="0" style="width: 200px; border-collapse: collapse;">

                            <tr>
                                <td colspan="2" class="control" style="text-align: center; white-space: nowrap;">Air Fresheners:</td>
                                <%
		strSQL = "SELECT Product.Descript,Product.ProdID,RECITEM.QTY FROM Product(NOLOCK)"&_
		" LEFT OUTER JOIN RECITEM(NOLOCK) ON Product.ProdID = RECITEM.ProdID"&_
		" AND RECITEM.recid =" & intRECID &_
		" AND RECITEM.LocationID =" & LocationID &_
		" WHERE (Product.cat = 21) ORDER BY Product.Number"


		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			DO While NOT rsData.eof 

			optAir = "optAir"+cstr(rsData("ProdID"))

                                %>
                                <tr>
                                    <td style="text-align: right">&nbsp;</td>

                                    <% IF len(rsData("QTY")) > 0 THEN%>
                                    <td style="text-align: left">
                                        <input tabindex="18" type="Radio" id="optAir" name="optAir" value='<%=rsData("ProdID")%>' checked  onclick="CheckClick3()"><label class="data" for="optAir"><%=rsData("Descript")%></label></td>
                                    <% ELSE %>
                                    <td style="text-align: left">
                                        <input tabindex="18" type="Radio" id="optAir" name="optAir" value='<%=rsData("ProdID")%>'  onclick="CheckClick3()"><label class="data" for="optAir"><%=rsData("Descript")%></label></td>
                                    <% END IF %>
                                </tr>

                                <%
				rsData.MoveNext
			Loop
		END IF
		Set rsData = Nothing
  
'response.write "line 1 "&now()&"<br />"

                                %>
                            </tr>



                        </table>




                    </td>
                    <td style="vertical-align: top">
                        <table border="0" style="width: 500px; border-collapse: collapse;">
                            <tr>
                                <td colspan="2" class="control" style="text-align: center; white-space: nowrap;">Additional Service:</td>
                            </tr>
                            <%
		intAddCnt = 1
		strSQL = "exec stp_OptionList " & intRECID &"," & LocationID
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			DO While NOT rsData.eof 
				intProdID=rsData("ProdID")
			    strDescript=rsData("Descript")
				intQty=rsData("QTY")
				optAddSer=rsData("optAddSer")
			    OptArr=rsData("OptArr")
                intAddCnt=rsData("AddCnt")
				IF intAddCnt=1 then
                            %>
                            <tr>
                                <td style="vertical-align: top">
                                    <table border="0" style="width: 200px; border-collapse: collapse;">
                                        <%
				END IF
				IF intAddCnt=18 then ' Column Counter
                                        %>
                                    </table>
                                </td>
                                <td style="vertical-align: top">
                                    <table border="0" style="width: 200px; border-collapse: collapse;">
                                        <%
				END IF
                                        %>
                                        <tr>
                                            <td>
                                                <input name="<%=optAddSer%>" type="checkbox" onclick="CheckClick4('<%=OptArr%>')"
                                                    <%If len(intQty) > 0 Then%>
                                                    checked
                                                    <%End If%>>
                                                <label class="data"><%=strDescript%></label></td>
                                        </tr>
                                        <%
				rsData.MoveNext
				intAddCnt = intAddCnt+1
			Loop
		END IF
		Set rsData = Nothing
 
' response.write "end "&now()

                                           
                                            
                                        %>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <hr>
            <table border="0" style="width: 98%; border-collapse: collapse;">
                <tr>
                    <td class="control" style="text-align: left; white-space: nowrap;">Note:&nbsp;</td>
                    <td class="control" style="text-align: left; white-space: nowrap;">
                        <textarea cols="100" rows="3" title="Ticket Notes." name="txtNotes"><%=txtNotes%>
			</textarea>
                    </td>
                </tr>
            </table>
            <table border="0" style="width: 98%; border-collapse: collapse;">
                <tr>
                    <td style="text-align: center" colspan="3">
                        <button name="btnPay" class="button" style="width: 100px" onclick="PayChg()">Pay</button>&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <table border="0" style="width: 98%; text-align: center; border-collapse: collapse;">
                <tr>
                    <td style="text-align: center" colspan="3">
                        <button name="btnDelete" class="button" style="width: 75px" onclick="DeleteChg()">Delete</button>&nbsp;&nbsp;&nbsp;&nbsp;
	                  <button name="btnSave" class="button" style="width: 75px" onclick="SaveChg()">Save</button>&nbsp;&nbsp;&nbsp;&nbsp;
	                  <button name="btnPrint" class="button" style="width: 75px" onclick="PrintChg()">Print</button>&nbsp;&nbsp;&nbsp;&nbsp;
	                  <button name="btnDone" class="button" style="width: 75px" onclick="DoneChg()">Done</button>
                    </td>
                </tr>
            </table>

        </div>
        <input type="hidden" name="FormAction" value="">
    </form>
</body>
</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/vbscript">
    Option Explicit


Sub Window_OnLoad()
    if document.frmmain.blSave.value = 1 then
	    window.event.cancelBubble = false
	    parent.window.close
    end if
End Sub

Sub CheckClick1()
	Dim OptSel,opt,prodid,OptDataArr,strDescript
	frmMain.FormAction.value="SaveChg1"
	frmMain.submit()
END Sub

Sub CheckClick2()
	frmMain.FormAction.value="SaveChg2"
	frmMain.submit()
END Sub

Sub CheckClick3()
	frmMain.FormAction.value="SaveChg3"
	frmMain.submit()
END Sub

Sub CheckClick4(Optarr)
	Dim OptSel,opt,prodid,OptDataArr,strDescript
	OptDataArr = Split (OptArr,"|")
	OPT = OptDataArr(0)
    prodid = OptDataArr(1)
	IF opt = "1" then
		IF window.event.srcElement.checked then
			OptSel= ShowModalDialog("NewDetailSelOpt.asp?intProdID="& prodid ,"","dialogwidth:260px;dialogheight:160px;")
			IF LEN(TRIM(OptSel))> 0 then
				window.event.srcElement.name = "optAddSer"+OptSel
                frmMain.newProdID.value=OptSel
			ELSE
			    window.event.srcElement.checked = false
			END IF
		ELSE 
			window.event.srcElement.checked = false
        END IF
	ELSE
		IF  window.event.srcElement.checked then
			window.event.srcElement.checked = false
		END IF
    END IF

	frmMain.hdnProdID.value=prodid
	frmMain.FormAction.value="SaveChg4"
	frmMain.submit()
END Sub

Sub UPCChg()
	IF len(trim(document.frmmain.intUPC.value)) =8 then
		frmMain.FormAction.value="UPCChg"
		frmMain.submit()
	END IF
End Sub

Sub SelClient()
	Dim retClient,intdata
	IF len(trim(document.frmmain.intUPC.value)) < 8 then
	    retClient= ShowModalDialog("SelClientDlg.asp?LocationID=" & document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:600px;dialogheight:350px;")
	    IF retClient > 2 and len(trim(retClient))>0 then
	    	frmmain.intClientID.value = retClient
	    	frmMain.FormAction.value="AddClient"
	    	frmMain.submit()
	    END IF
    end if
End Sub

Sub PrintChg()
	window.event.CancelBubble=True
	window.event.ReturnValue=False

	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
		frmMain.FormAction.value="PrintChg"
		frmMain.submit()
End Sub

Sub ClientChg()
	IF document.frmmain.cboClientID.value <>  document.frmmain.intClientID.value then
		frmMain.FormAction.value="ClientChg"
		frmMain.submit()
	END IF
End Sub
Sub VehicalChg()
	IF document.frmmain.cbovehid.value <>  document.frmmain.intvehid.value then
		frmMain.FormAction.value="VehicalChg"
		frmMain.submit()
	END IF
End Sub
Sub VehModChg()
	IF document.frmmain.cboVehMod.value <>  document.frmmain.intVehMod.value then
		frmMain.FormAction.value="VehModChg"
		frmMain.submit()
	END IF
End Sub

Sub SaveChg()
	window.event.CancelBubble=True
	window.event.ReturnValue=False

	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
		frmMain.FormAction.value="SaveChg"
		frmMain.submit()
End Sub

Sub AddClient1()
	Dim retClient1,intdata1
	IF trim(len(document.all("intUPC").value)) > 0 then
		retClient1= ShowModalDialog("ClientAddDlg.asp?strUPC="& frmMain.intUPC.value &"&LocationID=" & document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:600px;dialogheight:350px;")
	ELSE
		intdata1 = frmMain.intClientID.value &"|"& document.frmmain.cboVehMan.value &"|"& document.frmmain.strVModel.value &"|"& document.frmmain.cboVehMod.value &"|"& document.frmmain.cboVehColor.value
		retClient1= ShowModalDialog("NewClientDlg.asp?intdata="& intdata1 &"&LocationID=" & document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:600px;dialogheight:350px;")
	END IF
	IF retClient1 >= 3 then
		frmmain.intClientID.value = retClient1
		frmMain.FormAction.value="AddClient"
		frmMain.submit()
	else
        msgbox "Error No Client created!"
    END IF
End Sub

Sub AddVeh()
	Dim retVeh,intdata
	intdata = frmMain.intClientID.value&"|"&document.frmmain.cboVehMan.value&"|"&document.frmmain.strVModel.value&"|"&document.frmmain.cboVehMod.value&"|"&document.frmmain.cboVehColor.value
	retVeh= ShowModalDialog("NewVehDlg.asp?intdata="& intdata &"&LocationID=" & document.all("LocationID").value &"&LoginID="& document.all("LoginID").value,"","dialogwidth:600px;dialogheight:350px;")
	IF retVeh > 0 then
		frmMain.submit()
	END IF
End Sub

Sub StartedChg()
	Dim retDetailBy
	window.event.CancelBubble=True
	window.event.ReturnValue=False
	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	retDetailBy= ShowModalDialog("NewDetailByDlg.asp?intRecID="& frmMain.intRecID.value &"&LocationID=" & document.all("LocationID").value &"&LoginID="& document.all("LoginID").value,"","dialogwidth:450px;dialogheight:300px;")
	IF retDetailBy then
		frmMain.FormAction.value="StartedChg"
		frmMain.submit()
	ELSE
		msgbox "No Employee Selected"
	END IF
End Sub

Sub Assign()
	Dim AssignBy
	window.event.CancelBubble=True
	window.event.ReturnValue=False
	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	AssignBy= ShowModalDialog("NewDetailByDlg.asp?intRecID="& frmMain.intRecID.value &"&LocationID=" & document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:450px;dialogheight:300px;")
End Sub

Sub CompleteChg()
	Dim retDetailBy
	window.event.CancelBubble=True
	window.event.ReturnValue=False

	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	retDetailBy= ShowModalDialog("NewDetailByDlg.asp?intRecID="& frmMain.intRecID.value &"&LocationID=" & document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:450px;dialogheight:300px;")
	IF retDetailBy then
		frmMain.FormAction.value="CompleteChg"
		frmMain.submit()
	ELSE
		msgbox "No Employee Selected"
	END IF
End Sub

Sub PayChg()
	window.event.CancelBubble=True
	window.event.ReturnValue=False

	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
		'window.close
		parent.window.intrecID.value=frmMain.intRecID.value
		parent.window.FormAction.value="PayChg"
		parent.window.close
End Sub

Sub DeleteChg()
	Dim Answer
	window.event.cancelBubble = false
	Answer = MsgBox("Are you sure you want to Delete this Wash?",276,"Confirm Delete")
	If Answer = 6 then
		frmMain.FormAction.value="DeleteChg"
		frmMain.submit()
		'parent.window.close
	Else
		Exit Sub
	End if
End Sub
Sub DoneChg()
	window.event.cancelBubble = false
	parent.window.close
End Sub

</script>

<%
	Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************

Sub UPCChg(dbMain)
	Dim intrecID,newvehid,rsData,strSQL,intColor,strVmodel,intmodel,intmake,LocationID
	Dim strUPCHARGE,intProdID,intClientID,intUPC,optDetail,rsData2,strSQL2
	intrecID =  request("intrecID")
	LocationID =  request("LocationID")
	intUPC = request("intUPC")
	strSQL = "SELECT ClientID,vehid, make,Vmodel, model, Color FROM vehical with(nolock)  where UPC='" & intUPC &"'"
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF not rsData.eof then
		intClientID = rsData("ClientID")
		newvehid = rsData("vehid")
		intmake = rsData("make")
		strVmodel = rsData("Vmodel")
		intmodel = rsData("model")
		intColor = rsData("Color")
		END IF
	End If
	strSQL= "	UPDATE REC Set " & _
			"	ClientID=" & intClientID &"," & _
			"	vehid=" & newvehid &"," & _
			"	VehMan=" & intmake &"," & _
			"	Vmodel='" & strVmodel &"'," & _
			"	Vehmod=" & intmodel &"," & _
			"	VehColor=" & intColor & _
			"	WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If
	strSQL2= "SELECT distinct Product.ProdID "&_
		" FROM Product(NOLOCK)"&_
		" WHERE (Product.Descript = 'Large Vehicle') AND (Product.dept = 1)"
	If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
		IF NOT rsData2.eof then
			DO WHile NOT rsData2.eof
				strSQL= "Delete RECItem WHERE recID=" & intrecID &" AND ProdID=" & rsdata2("Prodid")  & " AND LocationID="& LocationID
				If NOT (dbExec(dbMain,strSQL)) Then
					Response.Write gstrMsg
					Response.end
				End If
				rsData2.MoveNext
			Loop
		END IF
	END IF
	Set rsData2 = Nothing
	IF instr(1,trim(strUPCHARGE),"(")>0 then
		strUPCHARGE = MID(trim(strUPCHARGE),instr(1,trim(strUPCHARGE),"(")+1,1)
		Select Case strUPCHARGE
			Case "A"
				intProdID = 5
			Case "B"
				intProdID = 7
			Case "C"
				intProdID = 8
			Case "D"
				intProdID = 9
			Case "E"
				intProdID = 10
			Case "F"
				intProdID = 409
			Case "G"
				intProdID = 470
		END Select
		Call SaveProd(dbMain,intProdID,intRECID)
	END IF

	strSQL = "SELECT listdesc FROM LM_ListItem(nolock) WHERE listtype = 4 AND listvalue =" & intmodel
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF not rsData.eof then
		strUPCHARGE = rsData("listdesc")
		END IF
	End If

'	strSQL2 = "select distinct Prodid from Product(nolock) where Cat=1"
'	If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
'		IF NOT rsData2.eof then
'			DO WHile NOT rsData2.eof
'				strSQL= "Delete RECItem WHERE recID=" & intrecID &"AND ProdID=" & rsdata2("Prodid")  & " AND LocationID="& LocationID
'				If NOT (dbExec(dbMain,strSQL)) Then
'					Response.Write gstrMsg
'					Response.end
'				End If
'				rsData2.MoveNext
'			Loop
'		END IF
'	END IF
'	optDetail = request("optDetail")
'	Call SaveProd(dbMain,optDetail,intRECID)


END Sub



Sub ClientChg(dbMain)
	Dim intrecID,newClientID,strSQL,rsData,intColor,strvmodel,intmodel,LocationID
	Dim intmake,intvehid,intProdID,optDetail,rsData2,strSQL2,strUPCHARGE
	intrecID =  request("intrecID")
	newClientID = request("cboClientID")
	LocationID = request("LocationID")
	strSQL= "	UPDATE REC Set " & _
			"	ClientID=" & newClientID & _
			"	WHERE recID=" & intrecID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If
	strSQL = "SELECT vehid, make, vmodel, model, Color FROM vehical with(nolock)  where VehNum=1 and ClientID=" & newClientID
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF not rsData.eof then
			intvehid = rsData("vehid")
			intmake = rsData("make")
			strvmodel = rsData("vmodel")
			intmodel = rsData("model")
			intColor = rsData("Color")
		ELSE
			intvehid = 0
			intmake = 0
			strvmodel = "Unk"
			intmodel = 0
			intColor = 0
		END IF
	End If
	strSQL= "	UPDATE REC Set " & _
			"	vehid=" & intvehid &"," & _
			"	VehMan=" & intmake &"," & _
			"	Vmodel='" & strvmodel &"'," & _
			"	Vehmod=" & intmodel &"," & _
			"	VehColor=" & intColor & _
			"	WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If

	strSQL2 = "select distinct Prodid from Product(nolock) where Cat=1"
	If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
		IF NOT rsData2.eof then
			DO WHile NOT rsData2.eof
				strSQL= "Delete RECItem WHERE recID=" & intrecID &"AND ProdID=" & rsdata2("Prodid")  & " AND LocationID="& LocationID
				If NOT (dbExec(dbMain,strSQL)) Then
					Response.Write gstrMsg
					Response.end
				End If
				rsData2.MoveNext
			Loop
		END IF
	END IF
	optDetail = request("optDetail")
	Call SaveProd(dbMain,optDetail,intRECID)

	strSQL2= "SELECT distinct Product.ProdID "&_
		" FROM Product(NOLOCK)"&_
		" WHERE (Product.Descript = 'Large Vehicle') AND (Product.dept = 1)"
	If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
		IF NOT rsData2.eof then
			DO WHile NOT rsData2.eof
				strSQL= "Delete RECItem WHERE recID=" & intrecID &"AND ProdID=" & rsdata2("Prodid")  & " AND LocationID="& LocationID
				If NOT (dbExec(dbMain,strSQL)) Then
					Response.Write gstrMsg
					Response.end
				End If
				rsData2.MoveNext
			Loop
		END IF
	END IF
	Set rsData2 = Nothing
	IF instr(1,trim(strUPCHARGE),"(")>0 then
		strUPCHARGE = MID(trim(strUPCHARGE),instr(1,trim(strUPCHARGE),"(")+1,1)
		Select Case strUPCHARGE
			Case "A"
				intProdID = 5
			Case "B"
				intProdID = 7
			Case "C"
				intProdID = 8
			Case "D"
				intProdID = 9
			Case "E"
				intProdID = 10
			Case "F"
				intProdID = 409
			Case "G"
				intProdID = 470
		END Select
		Call SaveProd(dbMain,intProdID,intRECID)
	END IF
END Sub

Sub AddClient(dbMain)
	Dim intrecID,newClientID,strSQL,rsData,intColor,strvmodel,intmodel,LocationID
	Dim intmake,intvehid,intProdID,optDetail,rsData2,strSQL2,strUPCHARGE
	intrecID =  request("intrecID")
	LocationID =  request("LocationID")
	newClientID = request("intClientID")
	strSQL= "	UPDATE REC Set " & _
			"	ClientID=" & newClientID & _
			"	WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If
	strSQL = "SELECT vehid, make, vmodel,model, Color FROM vehical with(nolock)  where VehNum=1 and ClientID=" & newClientID 
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF not rsData.eof then
			intvehid = rsData("vehid")
			intmake = rsData("make")
			strvmodel = rsData("vmodel")
			intmodel = rsData("model")
			intColor = rsData("Color")
		ELSE
			intvehid = 0
			intmake = 0
			strvmodel = "Unk"
			intmodel = 0
			intColor = 0
		END IF
	End If
	strSQL= "	UPDATE REC Set " & _
			"	vehid=" & intvehid &"," & _
			"	VehMan=" & intmake &"," & _
			"	VModel='" & strvmodel &"'," & _
			"	Vehmod=" & intmodel &"," & _
			"	VehColor=" & intColor & _
			"	WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If
	strSQL2= "SELECT distinct Product.ProdID "&_
		" FROM Product(NOLOCK)"&_
		" WHERE (Product.Descript = 'Large Vehicle') AND (Product.dept = 1)"
	If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
		IF NOT rsData2.eof then
			DO WHile NOT rsData2.eof
				strSQL= "Delete RECItem WHERE recID=" & intrecID &" AND ProdID=" & rsdata2("Prodid")  & " AND LocationID="& LocationID
				If NOT (dbExec(dbMain,strSQL)) Then
					Response.Write gstrMsg
					Response.end
				End If
				rsData2.MoveNext
			Loop
		END IF
	END IF
	Set rsData2 = Nothing
	IF instr(1,trim(strUPCHARGE),"(")>0 then
		strUPCHARGE = MID(trim(strUPCHARGE),instr(1,trim(strUPCHARGE),"(")+1,1)
		Select Case strUPCHARGE
			Case "A"
				intProdID = 5
			Case "B"
				intProdID = 7
			Case "C"
				intProdID = 8
			Case "D"
				intProdID = 9
			Case "E"
				intProdID = 10
			Case "F"
				intProdID = 409
			Case "G"
				intProdID = 470
		END Select
		Call SaveProd(dbMain,intProdID,intRECID)
	END IF
	'strSQL2 = "select distinct Prodid from Product(nolock) where Cat=1"
	'If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
	'	IF NOT rsData2.eof then
	'		DO WHile NOT rsData2.eof
	'			strSQL= "Delete RECItem WHERE recID=" & intrecID &"AND ProdID=" & rsdata2("Prodid")  & " AND LocationID="& LocationID
	'			If NOT (dbExec(dbMain,strSQL)) Then
	'				Response.Write gstrMsg
	'				Response.end
	'			End If
	'			rsData2.MoveNext
	'		Loop
	'	END IF
	'END IF
	'optDetail = request("optDetail")
	'Call SaveProd(dbMain,optDetail,intRECID)
END Sub

Sub VehModChg(dbMain)
	Dim intrecID,rsData,strSQL,intmodel,strUPCHARGE,intProdID,strVmodel,LocationID
	Dim optDetail,rsData2,strSQL2
	intrecID =  request("intrecID")
	LocationID =  request("LocationID")
	intmodel =  request("cboVehMod")
	strVmodel =  request("strVmodel")
	strSQL = "SELECT listdesc FROM LM_ListItem(nolock) WHERE listtype = 4 AND listvalue =" & intmodel
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF not rsData.eof then
		strUPCHARGE = rsData("listdesc")
		END IF
	End If
	strSQL= "	UPDATE REC Set Vehmod=" & intmodel &","&_
			"	Vmodel='" & request("strVmodel") &"'," & _
			"	VehMan=" & request("cboVehMan") &"," & _
			"	VehColor=" & request("cboVehColor")  &"," & _
			"	Notes='" & ltrim(request("txtNotes"))  &"'" & _
			" WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If
	strSQL2= "SELECT distinct Product.ProdID "&_
		" FROM Product(NOLOCK)"&_
		" WHERE (Product.Descript = 'Large Vehicle') AND (Product.dept = 1)"
	If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
		IF NOT rsData2.eof then
			DO WHile NOT rsData2.eof
				strSQL= "Delete RECItem WHERE recID=" & intrecID &"AND ProdID=" & rsdata2("Prodid")  & " AND LocationID="& LocationID
				If NOT (dbExec(dbMain,strSQL)) Then
					Response.Write gstrMsg
					Response.end
				End If
				rsData2.MoveNext
			Loop
		END IF
	END IF
	Set rsData2 = Nothing
	IF instr(1,trim(strUPCHARGE),"(")>0 then
		strUPCHARGE = MID(trim(strUPCHARGE),instr(1,trim(strUPCHARGE),"(")+1,1)
		Select Case strUPCHARGE
			Case "A"
				intProdID = 5
			Case "B"
				intProdID = 7
			Case "C"
				intProdID = 8
			Case "D"
				intProdID = 9
			Case "E"
				intProdID = 10
			Case "F"
				intProdID = 409
			Case "G"
				intProdID = 470
		END Select
		Call SaveProd(dbMain,intProdID,intRECID)
	END IF

	'strSQL2 = "select distinct Prodid from Product(nolock) where Cat=1"
	'If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
	'	IF NOT rsData2.eof then
	'		DO WHile NOT rsData2.eof
	'			strSQL= "Delete RECItem WHERE recID=" & intrecID &"AND ProdID=" & rsdata2("Prodid")  & " AND LocationID="& LocationID
	'			If NOT (dbExec(dbMain,strSQL)) Then
	'				Response.Write gstrMsg
	'				Response.end
	'			End If
	'			rsData2.MoveNext
	'		Loop
	'	END IF
	'END IF
	'optDetail = request("optDetail")
	'Call SaveProd(dbMain,optDetail,intRECID)
END Sub

Sub VehicalChg(dbMain)
	Dim intrecID,newvehid,rsData,strSQL,intColor,strVmodel,intmodel,LocationID
	Dim intmake,strUPCHARGE,intProdID,optDetail,rsData2,strSQL2
	intrecID =  request("intrecID")
	LocationID =  request("LocationID")
	newvehid = request("cbovehid")
	strSQL = "SELECT vehid, make,Vmodel, model, Color FROM vehical with(nolock)  where vehid=" & newvehid
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF not rsData.eof then
		intmake = rsData("make")
		strVmodel = rsData("Vmodel")
		intmodel = rsData("model")
		intColor = rsData("Color")
		END IF
	End If
	strSQL= "	UPDATE REC Set " & _
			"	vehid=" & newvehid &"," & _
			"	VehMan=" & intmake &"," & _
			"	Vmodel='" & strVmodel &"'," & _
			"	Vehmod=" & intmodel &"," & _
			"	VehColor=" & intColor & _
			"	WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If
	strSQL2= "SELECT distinct Product.ProdID "&_
		" FROM Product(NOLOCK)"&_
		" WHERE (Product.Descript = 'Large Vehicle') AND (Product.dept = 1)"
	If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
		IF NOT rsData2.eof then
			DO WHile NOT rsData2.eof
				strSQL= "Delete RECItem WHERE recID=" & intrecID &"AND ProdID=" & rsdata2("Prodid")  & " AND LocationID="& LocationID
				If NOT (dbExec(dbMain,strSQL)) Then
					Response.Write gstrMsg
					Response.end
				End If
				rsData2.MoveNext
			Loop
		END IF
	END IF
	Set rsData2 = Nothing
'	strSQL2 = "select distinct Prodid from Product(nolock) where Cat=1"
'	If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
'		IF NOT rsData2.eof then
'			DO WHile NOT rsData2.eof
'				strSQL= "Delete RECItem WHERE recID=" & intrecID &"AND ProdID=" & rsdata2("Prodid")  & " AND LocationID="& LocationID
'				If NOT (dbExec(dbMain,strSQL)) Then
'					Response.Write gstrMsg
'					Response.end
'				End If
'				rsData2.MoveNext
'			Loop
'		END IF
'	END IF
'	optDetail = request("optDetail")
'	Call SaveProd(dbMain,optDetail,intRECID)

	strSQL = "SELECT listdesc FROM LM_ListItem(nolock) WHERE listtype = 4 AND listvalue =" & intmodel
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF not rsData.eof then
		strUPCHARGE = rsData("listdesc")
		END IF
	End If


	IF instr(1,trim(strUPCHARGE),"(")>0 then
		strUPCHARGE = MID(trim(strUPCHARGE),instr(1,trim(strUPCHARGE),"(")+1,1)
		Select Case strUPCHARGE
			Case "A"
				intProdID = 5
			Case "B"
				intProdID = 7
			Case "C"
				intProdID = 8
			Case "D"
				intProdID = 9
			Case "E"
				intProdID = 10
			Case "F"
				intProdID = 409
			Case "G"
				intProdID = 470
		END Select
		Call SaveProd(dbMain,intProdID,intRECID)
	END IF
END Sub



Sub TimeChg(dbMain)
	Dim intrecID,rsData,strSQL,dtdatein,LocationID
	intrecID =  request("intrecID")
	dtdatein =  request("dtdatein")
	LocationID =  request("LocationID")
	strSQL= "	UPDATE REC Set datein='"& dtdatein &"' WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If
END Sub

Sub PrintChg(dbMain)
	Dim intrecID,rsData,strSQL,LocationID
	intrecID =  request("intrecID")
	LocationID =  request("LocationID")
	strSQL= " Insert into PrintTicket(recID,LocationID,Printer,Report)Values(" & intrecID & ", " & LocationID & ",1,'Ticket')" 
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If
end sub

Sub SaveChg(dbMain)
	Dim intrecID,rsData,strSQL,rsData2,strSQL2,rsData3,strSQL3,strPrice,strComm,intrecItemID,LocationID
	Dim intProdID,strUPCHARGE,optAddSer,intOpAir,optDetail,optUPCHARGE,strTaxable,strTaxAmt,intVehMan
	intrecID =  request("intrecID")
	LocationID =  request("LocationID")
	strUPCHARGE =  request("strUPCHARGE")
    intVehMan =  request("cboVehMan") 


	strSQL= "	UPDATE REC Set " & _
			"	VehMan=" & intVehMan &"," & _
			"	Vehmod=" & request("cboVehmod") &"," & _
			"	VModel='" & request("strVModel") &"'," & _
			"	VehColor=" & request("cboVehColor")  &"," & _
			"	Notes='" & ltrim(request("txtNotes"))  &"'" & _
			"	WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If

end sub

Sub SaveChg1(dbMain)
	Dim intrecID,rsData,strSQL,rsData2,strSQL2,rsData3,strSQL3,strPrice,strComm,intrecItemID,LocationID
	Dim intProdID,strUPCHARGE,optAddSer,intOpAir,optDetail,optUPCHARGE,strTaxable,strTaxAmt,intVehMan
	intrecID =  request("intrecID")
	LocationID =  request("LocationID")
	strUPCHARGE =  request("strUPCHARGE")
    intVehMan =  request("cboVehMan") 


	strSQL= "	UPDATE REC Set " & _
			"	VehMan=" & intVehMan &"," & _
			"	Vehmod=" & request("cboVehmod") &"," & _
			"	VModel='" & request("strVModel") &"'," & _
			"	VehColor=" & request("cboVehColor")  &"," & _
			"	Notes='" & ltrim(request("txtNotes"))  &"'" & _
			"	WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If

	strSQL2 = "select Prodid from Product(nolock) where Cat=1"
	If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
		IF NOT rsData2.eof then
			DO WHile NOT rsData2.eof
				strSQL= "Delete RECItem WHERE recID=" & intrecID &"AND ProdID=" & rsdata2("Prodid")  & " AND LocationID="& LocationID
				If NOT (dbExec(dbMain,strSQL)) Then
					Response.Write gstrMsg
					Response.end
				End If
				rsData2.MoveNext
			Loop
		END IF
	END IF
	Set rsData2 = Nothing
	optDetail = request("optDetail")
	Call SaveProd(dbMain,optDetail,intRECID)

end sub

Sub SaveChg2(dbMain)
	Dim intrecID,rsData,strSQL,rsData2,strSQL2,rsData3,strSQL3,strPrice,strComm,intrecItemID,LocationID
	Dim intProdID,strUPCHARGE,optAddSer,intOpAir,optDetail,optUPCHARGE,strTaxable,strTaxAmt,intVehMan
	intrecID =  request("intrecID")
	LocationID =  request("LocationID")
	strUPCHARGE =  request("strUPCHARGE")
    intVehMan =  request("cboVehMan") 


	strSQL= "	UPDATE REC Set " & _
			"	VehMan=" & intVehMan &"," & _
			"	Vehmod=" & request("cboVehmod") &"," & _
			"	VModel='" & request("strVModel") &"'," & _
			"	VehColor=" & request("cboVehColor")  &"," & _
			"	Notes='" & ltrim(request("txtNotes"))  &"'" & _
			"	WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If
	strSQL2= "SELECT distinct Product.ProdID "&_
		" FROM Product(NOLOCK)"&_
		" WHERE (Product.Descript = 'Large Vehicle') AND (Product.dept = 1)"
	If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
		IF NOT rsData2.eof then
			DO WHile NOT rsData2.eof
				strSQL= "Delete RECItem WHERE recID=" & intrecID &"AND ProdID=" & rsdata2("Prodid")  & " AND LocationID="& LocationID
				If NOT (dbExec(dbMain,strSQL)) Then
					Response.Write gstrMsg
					Response.end
				End If
				rsData2.MoveNext
			Loop
		END IF
	END IF
	Set rsData2 = Nothing
	IF request("optUpCharge")>0 then
		optUpCharge = request("optUpCharge")
		Call SaveProd(dbMain,optUpCharge,intRECID)
	END IF
end sub

Sub SaveChg3(dbMain)
	Dim intrecID,rsData,strSQL,rsData2,strSQL2,rsData3,strSQL3,strPrice,strComm,intrecItemID,LocationID
	Dim intProdID,strUPCHARGE,optAddSer,intOpAir,optDetail,optUPCHARGE,strTaxable,strTaxAmt,intVehMan
	intrecID =  request("intrecID")
	LocationID =  request("LocationID")
	strUPCHARGE =  request("strUPCHARGE")
    intVehMan =  request("cboVehMan") 


	strSQL= "	UPDATE REC Set " & _
			"	VehMan=" & intVehMan &"," & _
			"	Vehmod=" & request("cboVehmod") &"," & _
			"	VModel='" & request("strVModel") &"'," & _
			"	VehColor=" & request("cboVehColor")  &"," & _
			"	Notes='" & ltrim(request("txtNotes"))  &"'" & _
			"	WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If
	IF request("optAir") > 0 then
		strSQL2 = "select Prodid from Product(nolock) where Cat=21"
		If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
			IF NOT rsData2.eof then
				DO WHile NOT rsData2.eof
					strSQL= "Delete RECItem WHERE recID=" & intrecID &"AND ProdID=" & rsdata2("Prodid") & " AND LocationID="& LocationID 
					If NOT (dbExec(dbMain,strSQL)) Then
						Response.Write gstrMsg
						Response.end
					End If
					rsData2.MoveNext
				Loop
			END IF
		END IF

		intOpAir = request("optAir")
		Call SaveProd(dbMain,intOpAir,intRECID)
	END IF
end sub

Sub SaveChg4(dbMain)
	Dim intrecID,rsData,strSQL,rsData2,strSQL2,rsData3,strSQL3,strPrice,strComm,intrecItemID,LocationID
	Dim intProdID,strUPCHARGE,optAddSer,intOpAir,optDetail,optUPCHARGE,strTaxable,strTaxAmt,intVehMan,hdnProdID,newProdID
	intrecID =  request("intrecID")
	LocationID =  request("LocationID")
	strUPCHARGE =  request("strUPCHARGE")
    intVehMan =  request("cboVehMan") 
    hdnProdID =  request("hdnProdID")
    newProdID =  request("newProdID")

	strSQL= "	UPDATE REC Set " & _
			"	VehMan=" & intVehMan &"," & _
			"	Vehmod=" & request("cboVehmod") &"," & _
			"	VModel='" & request("strVModel") &"'," & _
			"	VehColor=" & request("cboVehColor")  &"," & _
			"	Notes='" & ltrim(request("txtNotes"))  &"'" & _
			"	WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If

    strSQL2 = "SELECT Product.Opt FROM Product(NOLOCK) WHERE (Product.cat = 3)  AND Prodid="& hdnProdID
	If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
		IF Not rsData2.eof then
            IF rsData2("Opt") = 1 then
	            strSQL2 = "SELECT RecItemID FROM dbo.RECITEM WHERE recID=" & intrecID &" AND LocationID="& LocationID &" AND Prodid in (SELECT ProductID FROM ProdOpt WHERE (OptID = "& hdnProdID &"))"
	            If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
		            IF Not rsData2.eof then
			            strSQL= "	Delete RECItem WHERE recID=" & intrecID &" AND LocationID="& LocationID &" AND Prodid in (SELECT ProductID FROM ProdOpt WHERE (OptID = "& hdnProdID &"))"
			            If NOT (dbExec(dbMain,strSQL)) Then
				            Response.Write gstrMsg
				            Response.end
			            End If
                    Else
			            strSQL= "	Delete RECItem WHERE recID=" & intrecID &" AND LocationID="& LocationID &" AND Prodid in (SELECT ProductID FROM ProdOpt WHERE (OptID = "& hdnProdID &"))"
			            If NOT (dbExec(dbMain,strSQL)) Then
				            Response.Write gstrMsg
				            Response.end
			            End If
			            strSQL = "Select Price,Comm From product with(nolock)  where ProdID=" & newProdID
			            If DBOpenRecordset(dbMain,rsData,strSQL) Then
				            IF NOT rsData.eof then
					            strPrice = nulltest(rsData("Price"))
					            strComm = nulltest(rsData("Comm"))
				            ELSE
					            strPrice = 0.0
					            strComm = 0.0
				            END IF
			            End If
			            strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem with(nolock)  where LocationID="& LocationID
			            If DBOpenRecordset(dbMain,rsData,strSQL) Then
				            intrecItemID = rsData("recItemID")
			            End If
			            strSQL= " Insert into RECItem(recID,LocationID,recItemID,Prodid,Price,Comm,qty)Values(" & _
					            intrecID & ", " & _
					            LocationID & ", " & _
					            intrecItemID & ", " & _
					            newProdID & "," & _
					            strPrice &"," & _
					            strComm &",1)" 
			            If NOT (dbExec(dbMain,strSQL)) Then
				            Response.Write gstrMsg
			            End If
                    end if
                end if
            else
	            strSQL2 = "SELECT RecItemID FROM dbo.RECITEM WHERE recID=" & intrecID &" AND Prodid="& hdnProdID & " AND LocationID="& LocationID
	            If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
		            IF Not rsData2.eof then
			            strSQL= "	Delete RECItem WHERE recID=" & intrecID &" AND Prodid="& hdnProdID & " AND LocationID="& LocationID
			            If NOT (dbExec(dbMain,strSQL)) Then
				            Response.Write gstrMsg
				            Response.end
			            End If
                    Else
			            strSQL= "	Delete RECItem WHERE recID=" & intrecID &" AND Prodid="& hdnProdID & " AND LocationID="& LocationID
			            If NOT (dbExec(dbMain,strSQL)) Then
				            Response.Write gstrMsg
				            Response.end
			            End If
					    strSQL = "SELECT distinct Product.Price, Product.Comm, Product.Taxable,LM_Locations.TaxRate"&_
					    " FROM Product(nolock), LM_Locations (nolock)"&_
					    " WHERE (Product.ProdID = "& hdnProdID &") AND (LM_Locations.LocationID="& LocationID & ")"
					    If DBOpenRecordset(dbMain,rsData,strSQL) Then
						    IF NOT rsData.eof then
							    IF rsData("Taxable")=1 then
								    strPrice = nulltest(rsData("Price"))
								    strComm = nulltest(rsData("Comm"))
								    strTaxable = 1
								    strTaxAmt = round((cdbl(rsData("Price"))*cdbl(rsData("TaxRate"))),2)
							    ELSE
								    strPrice = nulltest(rsData("Price"))
								    strComm = nulltest(rsData("Comm"))
								    strTaxable = 0
								    strTaxAmt = 0.0
							    END IF
						    ELSE
							    strPrice = 0.0
							    strComm = 0.0
							    strTaxable = 0
							    strTaxAmt = 0.0
						    END IF
					    End If
					    strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem with(nolock)  where LocationID="& LocationID
					    If DBOpenRecordset(dbMain,rsData,strSQL) Then
						    intrecItemID = rsData("recItemID")
					    End If
					    strSQL= " Insert into RECItem(recID,LocationID,recItemID,Prodid,Price,taxable,taxamt,Comm,qty)Values(" & _
							    intrecID & ", " & _
							    LocationID & ", " & _
							    intrecItemID & ", " & _
							    hdnProdID & "," & _
							    strPrice &"," & _
							    strTaxable &"," & _
							    strTaxAmt &"," & _
							    strComm &",1)" 
					    If NOT (dbExec(dbMain,strSQL)) Then
						    Response.Write gstrMsg
					    End If
                    end if
                end if

            end if
        end if
    end if


END Sub

Sub DeleteChg(dbMain)
	Dim intrecID,rsData,strSQL,LocationID
	intrecID =  request("intrecID")
	LocationID =  request("LocationID")
	strSQL= "	UPDATE REC Set datein='1/1/1900', dateout='1/1/1900', accamt=0.0, tax=0.0, gtotal=0.0, cashamt=0.0, chargeamt=0.0, cashback=0.0, cardtype=0, totalamt=0.0, CloseDte='1/1/1900',Status=70 WHERE recID=" & intrecID &" and LocationID= "& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
		Response.end
	End If
	strSQL= "	UPDATE RECItem set  Comm=0.0, Price=0.0 WHERE recID=" & intrecID &" and LocationID= "& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
		Response.end
	End If
	strSQL= "	Delete DetailComp WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
		Response.end
	End If
	strSQL= "	Delete GiftCardHist WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
		Response.end
	End If
END Sub

Function LoadVehical(db,var,var2)
	Dim strSQL,RS,strSel,temp,intType,blnDeleted,rsData2,strSQL2
	blnDeleted =False
	strSQL="SELECT vehical.vehnum,vehical.vModel,"&_
	"LM_ListItem2.ListDesc+' '+LM_ListItem.ListDesc+' '+LM_ListItem1.ListDesc AS Vehical,"&_
	" vehical.vehid"&_
	" FROM vehical (NOLOCK)"&_
	" INNER JOIN LM_ListItem(NOLOCK) ON vehical.make = LM_ListItem.ListValue AND LM_ListItem.ListType = 3"&_
	" INNER JOIN LM_ListItem LM_ListItem1(NOLOCK) ON vehical.model = LM_ListItem1.ListValue AND LM_ListItem1.ListType = 4"&_
	" INNER JOIN LM_ListItem LM_ListItem2(NOLOCK) ON vehical.Color = LM_ListItem2.ListValue AND LM_ListItem2.ListType = 5"&_
	" WHERE (vehical.ClientID = "& var &") order by vehical.vehnum "
	If dbOpenRecordSet(db,rs,strSQL) Then
		Do While Not RS.EOF
					If Trim(var2) = Trim(RS("vehid")) Then
						blnDeleted = true
						strSel = "selected" 
%>
<option value="<%=RS("vehid")%>" <%=strSel%>>#<%=RS("vehnum")%>&nbsp;<%=RS("Vehical")%></option>
<%
					Else
						strSel = ""
%>
<option value="<%=RS("vehid")%>" <%=strSel%>>#<%=RS("vehnum")%>&nbsp;<%=RS("Vehical")%></option>
<%
					End IF
			RS.MoveNext
		Loop
	End If
End Function

Function SaveProd(dbMain,intProdID,intRECID)
	Dim strSQL,rsData,strPrice,strComm,intrecItemID,strSumComm,strEstTime,strTaxable,strTaxAmt,LocationID
	LocationID =  request("LocationID")

	strSQL = "SELECT Product.Price, Product.Comm, Product.Taxable,LM_Locations.TaxRate"&_
	" FROM Product(nolock), LM_Locations(nolock)"&_
	" WHERE (Product.ProdID = "& intProdID &")  AND (LM_Locations.LocationID="& LocationID & ")"
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF NOT rsData.eof then
			IF rsData("Taxable")=1 then
				strPrice = nulltest(rsData("Price"))
				strComm = nulltest(rsData("Comm"))
				strTaxable = 1
				strTaxAmt = round((cdbl(rsData("Price"))*cdbl(rsData("TaxRate"))),2)
			ELSE
				strPrice = nulltest(rsData("Price"))
				strComm = nulltest(rsData("Comm"))
				strTaxable = 0
				strTaxAmt = 0.0
			END IF
		ELSE
			strPrice = 0.0
			strComm = 0.0
			strTaxable = 0
			strTaxAmt = 0.0
		END IF
	End If
	strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem with(nolock)  where LocationID="& LocationID
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		intrecItemID = rsData("recItemID")
	End If
	strSQL= " Insert into RECItem(recID,LocationID,recItemID,Prodid,Price,taxable,taxamt,Comm,qty)Values(" & _
			intrecID & ", " & _
			LocationID & ", " & _
			intrecItemID & ", " & _
			intProdID & "," & _
			strPrice &"," & _
			strTaxable &"," & _
			strTaxAmt &"," & _
			strComm &",1)" 
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If
	'Recalc Est. Time
	strSQL = "SELECT EstTime FROM Product (nolock) WHERE ProdID =" & intProdID
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF not rsData.eof then
		strEstTime = rsData("EstTime")
		ELse
		strEstTime = 0.0
		END IF
	End If
	strSQL= "	UPDATE REC Set " & _
			"	EstTime='" & dateadd("n",CDBL(strEstTime)*60, request("dtdatein")) &"'"  & _
			"	WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg


	END IF
	' Add kit comm here
	strSQL = "SELECT SUM(comm) as SumComm FROM RECITEM(nolock) WHERE recid =" & intrecID & " AND LocationID="& LocationID
	If DBOpenRecordset(dbMain,rsData,strSQL) Then
		IF not rsData.eof then
		strSumComm = rsData("SumComm")
		ELse
		strSumComm = 0.0
		END IF
	End If
	strSQL= "	UPDATE DetailComp Set " & _
			"	Comm=" & strSumComm & _
			"	WHERE recID=" & intrecID & " AND LocationID="& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If
End Function

%>
