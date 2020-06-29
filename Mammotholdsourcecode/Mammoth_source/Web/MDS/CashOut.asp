<%@  language="VBSCRIPT" %>
<%
'********************************************************************
Option Explicit
Response.Expires = 0
Response.Buffer = True
'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incDatabase.asp"-->
<!--#include file="incCommon.asp"-->
<%
'********************************************************************
' Global Variables
'********************************************************************
Dim Title
'********************************************************************
' Main
'********************************************************************
Call Main

'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
Sub Main

	Dim dbMain, intrecID,intTotal,intTax,intGTotal,intCashAmt,intGiftCardAmt,intChargeAmt,intCheckAmt,rsData
	Dim rs,strSQL,intCashBack,intCardType,intAccAmt,hdnValue,intTotalAmt,intBalance,hdnobj,intStatus
	Dim strChkPhone,strChkNo,strChkDL,intGiftCardID,intCustAccID,blAddItem,intVehAccID,intCorpAccID
	dim intReg,strEmail,blnNoEmail,blnCNXEmail,intClientID,txtRecNote,LocationID,LoginID,blnPrintReceipt
	Set dbMain =  OpenConnection


	intrecID = request("intrecID")
	LocationID = request("LocationID")
    LoginID = request("LoginID")
    blnPrintReceipt=request("blnPrintReceipt")
	intRecID = replace(Ucase(intrecID),"/L","")
	intreg= 1
	intStatus = request("intStatus")
	blnCNXEmail = request("blnCNXEmail")
	intClientID = request("intClientID")

    if len(trim(intClientID))=0 then
	    intClientID = 0
    END IF
    if len(trim(intStatus))=0 then
	    intStatus = 0
    END IF
    if len(trim(blnCNXEmail))=0 then
	    blnCNXEmail = 0
    END IF


	hdnValue = request("hdnValue")
	intTax = 0.0
	intTotal = 0.0
	intGTotal = 0.0
	intBalance = 0.0
		intCustAccID = 0
	blAddItem = "No"

Select Case Request("FormAction")
	Case "Srecbtn"
		Call SelectRec(dbMain,intrecID,intStatus,LocationID)
	Case "btnNew"
		Call NewRec(dbMain,intrecID,LocationID,LoginID)
	Case "btnAdd"
		Call AddItem(dbMain,hdnValue,LocationID)
		blAddItem = "Yes"
	Case "btnProcess"
		Call ProcessData(dbMain,intrecID,LocationID,LoginID)
        if blnPrintReceipt = 1 then     
		    Call PrintReceipt(dbMain,intrecID,intreg,LocationID,LoginID)
        end if
	Case "btnPrint"
		Call PrintReceipt(dbMain,intrecID,intreg,LocationID,LoginID)
	Case "btnDelete"
		Call DeleteReceipt(dbMain,intrecID,intreg,LocationID,LoginID)
	Case "btnRollBack"
		Call RollBackReceipt(dbMain,intrecID,intreg,LocationID,LoginID)
End Select
        blnPrintReceipt = 0

	IF len(trim(intRecID))=0 then
		intrecid=0
	    blnCNXEmail = 0
	END IF


IF not isnumeric(intRecID) then
    intRecID = FixNumber(intRecID)
	'response.write intRECID
	'response.end
END IF

	IF intRECID>0 then
		strSQL =" SELECT CashAmt,CheckAmt,ChargeAmt,GiftCardID,GiftCardAmt,cardType,CashBack,AccAmt,ChkPhone,ChkNo,ChkDL,Status,ClientID FROM REC (nolock) WHERE REC.RecID = "& intRECID &" AND REC.LocationID = "& LocationID 
	    IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
			IF NOT 	rs.EOF then
				intCashAmt = rs("CashAmt")
				intCheckAmt = rs("CheckAmt")
				intChargeAmt = rs("ChargeAmt")
				intCardType = rs("CardType")
				intCashBack = rs("CashBack")
				intAccAmt = rs("AccAmt")
				strChkPhone = rs("ChkPhone")
				strChkNo = rs("ChkNo")
				strChkDL = rs("ChkDL")
				intGiftCardAmt = rs("GiftCardAmt")
				intGiftCardID = rs("GiftCardID")
				intStatus = rs("Status")
                intClientID =  rs("ClientID")
			ELSE
				intCashAmt = 0.0
				intCheckAmt = 0.0
				intChargeAmt = 0.0
				intCashBack = 0.0
				intAccAmt = 0.0
				strChkPhone = ""
				strChkNo = ""
				strChkDL = ""
				intGiftCardAmt = 0.0
				intGiftCardID = ""
				intStatus = 0
                intClientID =  0
			END IF
		END IF
		Set RS = Nothing
		strSQL ="SELECT client.Ctype, client.Email, client.NoEmail, LTRIM(RTRIM(CAST(ISNULL(client.RecNote, '') AS varchar(1000)))) AS RecNote, "&_
		" REC.clientid, ISNULL(CustAcc.Type, 0) AS Type, client.C_Corp, ISNULL(CustAcc.Status, 0) AS status, "&_
		" CASE WHEN Ctype = 3 THEN (SELECT CustAccID FROM CustAcc WHERE (ClientID = REC.clientid)) "&_
		" When CType = 1 THEN (SELECT CustAccID FROM CustAcc WHERE (ClientID = client.C_Corp)) "&_
		" ELSE ISNULL(CustAcc.CustAccID, 0) END AS CustAccID "&_
		" FROM REC with (nolock) "&_
		" INNER JOIN client with (nolock) ON REC.clientid = client.clientid "&_
		" LEFT OUTER JOIN CustAcc with (nolock) ON client.clientid = CustAcc.ClientID AND REC.vehID = CustAcc.VehID"&_ 
		" WHERE (REC.recid = "& intRECID &") AND (REC.LocationID = "& LocationID &") AND (client.account = 1)"
	'Response.Write "==>"& strSQL &"<BR>"
		IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
			IF NOT 	rs.EOF then

				blnNoEmail =  rs("NoEmail")
				strEmail =  rs("Email")
				txtRecNote =  rs("RecNote")

				IF  rs("Ctype") = 1  then  ' Corp account
				intCustAccID = rs("CustAccID")
				ELSEIF  rs("Ctype") = 2 and rs("status")=1 then ' Monthly account
					intCustAccID = rs("CustAccID")
				elseIF  rs("Ctype") = 3  then  ' Comp account
					intCustAccID = rs("CustAccID")
				else
					intCustAccID = 0
				END IF
			END IF
		ELSE
			intCustAccID = 0
		END IF
		Set RS = Nothing
	ELSE
        blnNoEmail =  0
	    strEmail =  ""
		intCashAmt = 0.0
		intCheckAmt = 0.0
		intChargeAmt = 0.0
		intCashBack = 0.0
		intAccAmt = 0.0
		strChkPhone = ""
		strChkNo = ""
		strChkDL = ""
		intGiftCardAmt = 0.0
		intGiftCardID = ""
		intCustAccID = 0
	END IF

	IF intRECID>0 then

	    strSQL =" SELECT TXAmt FROM CustAccHist (nolock) WHERE TXRecID = "& intRECID &" AND TXLocationID = "& LocationID
	    IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
		    IF NOT 	rs.EOF then
			    intAccAmt = rs("TXAmt")*-1
		    END IF
	    END IF

	    Set RS = Nothing
	    strSQL =" SELECT TransAmt FROM GiftCardHist (nolock) WHERE RecID = "& intRECID &" and transtype ='Debit'"
	    IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
		    IF NOT 	rs.EOF then
			    intGiftCardAmt = rs("TransAmt")*-1
		    END IF
	    END IF
	    Set RS = Nothing
    END IF

	IF isnull(intGiftCardAmt) then
		intGiftCardAmt = 0.0
	END IF
	IF isnull(intCashAmt) then
		intCashAmt = 0.0
	END IF
	IF isnull(intCheckAmt) then
		intCheckAmt = 0.0
	END IF
	IF isnull(intChargeAmt) then
		intChargeAmt = 0.0
	END IF
	IF isnull(intCashBack) then
		intCashBack = 0.0
	END IF
	IF isnull(intAccAmt) then
		intAccAmt = 0.0
	END IF
	IF isnull(strChkPhone) then
		strChkPhone = ""
	END IF
	IF isnull(strChkNo) then
		strChkNo = ""
	END IF
	IF isnull(strChkDL) then
		strChkDL = ""
	END IF
	IF isnull(intGTotal) then
		intGTotal = 0.0
	END IF
	intTotalAmt = (intCashAmt+intChargeAmt+intAccAmt+intCheckAmt+intGiftCardAmt)
'********************************************************************
' HTML
'********************************************************************
%>

<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 492px;
        }

        .auto-style2 {
            width: 67%;
        }

        .auto-style3 {
            width: 389px;
        }

        .auto-style4 {
            width: 428px;
        }
    </style>
</head>
<body class="pgbody">
    <form method="post" name="frmMain" action="Cashout.asp">
        <input type="hidden" name="FormAction" tabindex="-2" />
        <input type="hidden" name="hdnValue" tabindex="-2" value="<%=hdnValue%>" />
        <input type="hidden" name="hdnobj" tabindex="-2" value="<%=hdnobj%>" />
        <input type="hidden" name="intGTotal" tabindex="-2" value="<%=intGTotal%>" />
        <input type="hidden" name="intTax" tabindex="-2" value="<%=intTax%>" />
        <input type="hidden" name="intTotal" tabindex="-2" value="<%=intTotal%>" />
        <input type="hidden" name="intTotalAmt" tabindex="-2" value="<%=intTotalAmt%>" />
        <input type="hidden" name="intBalance" tabindex="-2" value="<%=intBalance%>" />
        <input type="hidden" name="intCashAmt" tabindex="-2" value="<%=intCashAmt%>" />
        <input type="hidden" name="intCheckAmt" tabindex="-2" value="<%=intCheckAmt%>" />
        <input type="hidden" name="intGiftCardAmt" tabindex="-2" value="<%=intGiftCardAmt%>" />
        <input type="hidden" name="strChkNo" tabindex="-2" value="<%=strChkNo%>" />
        <input type="hidden" name="strChkPhone" tabindex="-2" value="<%=strChkPhone%>" />
        <input type="hidden" name="strChkDL" tabindex="-2" value="<%=strChkDL%>" />
        <input type="hidden" name="intGiftCardID" tabindex="-2" value="<%=intGiftCardID%>" />
        <input type="hidden" name="intChargeAmt" tabindex="-2" value="<%=intChargeAmt%>" />
        <input type="hidden" name="intAccAmt" tabindex="-2" value="<%=intAccAmt%>" />
        <input type="hidden" name="intCustAccID" tabindex="-2" value="<%=intCustAccID%>" />
        <input type="hidden" name="intCardType" tabindex="-2" value="<%=intCardType%>" />
        <input type="hidden" name="intCashBack" tabindex="-2" value="<%=intCashBack%>" />
        <input type="hidden" name="blAddItem" tabindex="-2" value="<%=blAddItem%>" />
        <input type="hidden" name="intStatus" tabindex="-2" value="<%=intStatus%>" />
        <input type="hidden" name="intReg" tabindex="-2" value="<%=intReg%>" />
        <input type="hidden" name="strEmail" tabindex="-2" value="<%=strEmail%>" />
        <input type="hidden" name="blnNoEmail" tabindex="-2" value="<%=blnNoEmail%>" />
        <input type="hidden" name="blnCNXEmail" tabindex="-2" value="<%=blnCNXEmail%>" />
        <input type="hidden" name="intClientID" tabindex="-2" value="<%=intClientID%>" />
        <input type="hidden" name="txtRecNote" tabindex="-2" value="<%=txtRecNote%>" />
        <input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
        <input type="hidden" name="blnPrintReceipt" id="blnPrintReceipt" value="<%=blnPrintReceipt%>" />
        <div style="text-align:center">
             <%
    strSQL = "SELECT ProStat.Washes, ProStat.Washers, ProStat.Details, Case When ProStat.TotalHours = 0 OR ProStat.Washes=0 then 0 else  CAST(ROUND(ProStat.Washes / ProStat.TotalHours * 100, 0) AS int) end AS Score, isnull(ProStat.PerRev,0.0) as PerRev, stats.CurrentWaitTime FROM dbo.ProStat INNER JOIN dbo.stats ON dbo.ProStat.LocationID = dbo.stats.LocationID WHERE (ProStat.ProDate = '"& Right("00"+cstr(Month(date())),2)+Right("00"+cstr(Day(date())),2)+Right(cstr(year(date())),2) &"') AND (ProStat.LocationID = "& LocationID &")"
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof  Then
            %>
            <table style="width: 70%; height: 18px" border="0">
                <tr>
                    <td style="text-align: center; vertical-align: middle">&nbsp;&nbsp;Washes &nbsp;<label class="data"><%=rsData("Washes")%></label>&nbsp;
                    </td>
                    <td style="text-align: center; vertical-align: middle">&nbsp;&nbsp;Wash-Emp&nbsp;<label class="data"><%=rsData("Washers")%></label>&nbsp;
                    </td>
                    <td style="text-align: center; vertical-align: middle">&nbsp;&nbsp;Details &nbsp;<label class="data"><%=rsData("Details")%></label>&nbsp;
                    </td>
                    <td style="text-align: center; vertical-align: middle">&nbsp;&nbsp;Score &nbsp;
                    
                        <% IF cdbl(rsData("Score")) < 74 then %>
                        <label class="reddata"><%=rsData("Score")%></label>&nbsp;
                        <% ELSEIF cdbl(rsData("Score")) >= 75 and cdbl(Session("Score")) <= 79 then %>
                        <label class="YellowData"><%=rsData("Score")%></label>&nbsp;
                        <% ELSEIF cdbl(rsData("Score")) >= 80 and cdbl(Session("Score")) <= 85 then %>
                        <label class="Data"><%=rsData("Score")%></label>&nbsp;
                        <% ELSE %>
                        <label class="GRNData"><%=rsData("Score")%></label>&nbsp;
                        <% End IF %>

                    </td>


                    <% IF cdbl(rsData("PerRev")) > 0.0 then %>
                    <td style="text-align: center; vertical-align: middle">&nbsp;&nbsp;P/L&nbsp;<label class="data"><%=rsData("PerRev")%></label>&nbsp;
                    </td>
                    <% Else %>
                    <td style="text-align: center; vertical-align: middle">&nbsp;&nbsp;P/L&nbsp;<label class="reddata"><%=rsData("PerRev")%></label>&nbsp;
                    </td>
                    <% END IF %>
                    <td style="text-align: center; vertical-align: middle">&nbsp;&nbsp;Wait&nbsp;<label class="data"><%=rsData("CurrentWaitTime")%></label>
                        &nbsp;
                    </td>
                </tr>
            </table>



            <%
                end if
            end if
            %>

            
                       <table border="0" style="border-collapse: collapse;" class="auto-style2">
                <tr>
                    <td style="text-align:right" class="auto-style1">
                        <table border="0" style="width: 500px; border-collapse: collapse;">
                            <% IF intRecID > 0 then%>
                            <tr>
                                <td align="right">
                                    <input maxlength="8" size="8" type="hidden" tabindex="1" name="intRecID" value="<%=intRecID%>" />
                                    <label class="control" style="font: bold 18px arial; text-align:right;"><b><%=intRecID%></b>&nbsp;&nbsp;&nbsp;&nbsp;</label></td>
                                <td  style="text-align: left; white-space: nowrap;">
                                    <label class="control" style="font: bold 18px arial"><b>Ticket</b>&nbsp;&nbsp;</label></td>
                                <td colspan="2" style="text-align: center">
                                    <button class="buttondead" style="height: 40px; width: 100px; font: bold 18px arial" onclick="Srecbtn()">Select</button></td>
                                <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                                <td style="text-align:center">
                                    <button class="buttondead" style="height: 40px; width: 100px; font: bold 18px arial" onclick="Newbtn()">New</button></td>
                            </tr>
                            <tr>
                                <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td  style="text-align: left; white-space: nowrap;">
                                    <input type="text" size="10" name="tValue" onkeypress="Check4Enter()" onfocus="setobj(1)" style="font: bold 28px arial">&nbsp;</td>
                                <td  style="text-align: left; white-space: nowrap;">
                                    <label class="control" style="font: bold 18px arial"><b>Item</b></label></td>
                                <td colspan="2" style="text-align: left">
                                    <input type="text" size="3" name="cboQty" onfocus="setobj(2)" style="font: bold 28px arial">&nbsp;	</td>
                                <td  style="text-align: left; white-space: nowrap;">
                                    <label class="control" style="font: bold 18px arial"><b>&nbsp;Qty&nbsp;</b></label></td>
                                <td style="text-align:center">
                                    <button style="height: 40px; width: 100px; font: bold 18px arial" onclick="Addbtn()" name="btnAdd">Add</button></td>
                </tr>
                            <tr>
                                <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="5" style="text-align: left; white-space: nowrap;">
                                    <button class="button" style="height: 40px; width: 120px; font: bold 18px arial" onclick="Searchbtn()" id="btnSearch" name="btnSearch">Search</button></td>
                            </tr>
                            <% ELSE %>
                            <tr>
                                <td  style="text-align: left; white-space: nowrap;">
                                    <input maxlength="8" size="8" type="hidden" tabindex="1" name="intRecID" value="<%=intRecID%>" />
                                    <input maxlength="8" size="8" type="text" tabindex="1" onkeypress="Check4Enter()" style="font: bold 28px arial" name="tValue">&nbsp;
                                </td>
                                <td  style="text-align: left; white-space: nowrap;">
                                    <label class="control" style="font: bold 18px arial"><b>Ticket</b>&nbsp;&nbsp;</label></td>
                                <td style="text-align:center">
                                    <button class="button" style="height: 40px; width: 100px; font: bold 18px arial" onclick="Srecbtn()">Select</button></td>
                                <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                                <td style="text-align:center">
                                    <button class="button" style="height: 40px; width: 100px; font: bold 18px arial" onclick="Newbtn()">New</button></td>
                            </tr>
                            <tr>
                                <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                            </tr>

                            <tr>
                                <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                                <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                                <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                                <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                                <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                                <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="5"  style="text-align:left">
                                    <button class="button" style="height: 40px; width: 120px; font: bold 18px arial" onclick="Searchbtn()" id="btnSearch" name="btnSearch">Search</button></td>
                            </tr>
                            <% END IF %>
                            <tr>
                                 <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2"  style="text-align:center">
                                    <table style="width: 180" border="2" cellspacing="2" cellpadding="2">
                                        <tr>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button(1)" id="button1" name="button1">1</button>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button(2)" id="button2" name="button2">2</button>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button(3)" id="button3" name="button3">3</button>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button(4)" id="button4" name="button4">4</button>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button(5)" id="button5" name="button5">5</button>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button(6)" id="button6" name="button6">6</button>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button(7)" id="button7" name="button7">7</button>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button(8)" id="button8" name="button8">8</button>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button(9)" id="button9" name="button9">9</button>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button('.')" id="button10" name="button10">.</button>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button(0)" id="button0" name="button0">0</button>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button('.00')" id="button11" name="button11">.00</button>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button('1.00')" id="button1d" name="button16">$1</button>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button('5.00')" id="button5d" name="button12">$5</button>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button('10.00')" id="button10d" name="button0">$10</button>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button('20.00')" id="button20d" name="button13">$20</button>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button('50.00')" id="button50d" name="button14">$50</button>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button('100.00')" id="button100d" name="button15">$100</button>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="DeleteText()" id="buttonD" name="buttonD">BkSp</button>
                                            <td style="text-align:center">
                                                <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="button('-')">+/- </button>
                                                <td style="text-align:center">
                                                    <button style="height: 40px; width: 50px; font: bold 18px arial" onclick="ClearText()" id="buttonC" name="buttonC">CLR</button>
                                        </tr>
                                    </table>
                                </td>
                                <td align="center" colspan="3">
                                    <table style="width: 140" border="2" cellspacing="2" cellpadding="2">
                                        <tr>
                                            <td style="text-align:center">
                                                <button class="buttondead" style="height: 40px; width: 100px; font: bold 18px arial" onclick="Cashbtn()" name="btnCash">Cash</button>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center">
                                                <button class="button" style="height: 40px; width: 100px; font: bold 18px arial" onclick="Checkbtn()" name="btnCheck">Check</button>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center">
                                                <button class="button" style="height: 40px; width: 100px; font: bold 18px arial" onclick="Creditbtn()" name="btnCredit">Credit</button>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center">
                                                <button class="buttondead" style="height: 40px; width: 100px; font: bold 18px arial" onclick="Discountbtn()" name="btnDiscount">Discount</button>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center">
                                                <button class="button" style="height: 40px; width: 100px; font: bold 18px arial" onclick="GiftCardbtn()" name="btnGiftCard">Gift Card</button>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center">
                                                <button class="buttondead" style="height: 40px; width: 100px; font: bold 18px arial" onclick="Accountbtn()" name="btnAccount">Account</button>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center">
                                                <button class="button" style="height: 40px; width: 100px; font: bold 18px arial" onclick="Clearbtn()" name="btnClear">Clear</button>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
            </td>
            <td align="right" class="auto-style3">
                <table style="" width="350" border="0" cellspacing="0" cellpadding="0">
                    <table border="0" cellspacing="0" cellpadding="0" class="auto-style4">
                        <iframe align="center" name="fraMain" src="CashOutFra.asp?intRECID=<%=intRECID%>&LoginID=<%=LoginID%>&LocationID=<%=LocationID%>" height="300" width="350" frameborder="0"></iframe>
                        <tr>
                            <td>
                                <table border="0" width="180" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="text-align:center">
                                            <button class="buttonDEAD" style="height: 40px; width: 100px; font: bold 18px arial" onclick="ProcessBtn()" name="btnComplete">Process</button></td>
                                    </tr>
                                    <tr>
                                        <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:center">
                                            <button class="buttonDEAD" style="height: 40px; width: 100px; font: bold 18px arial" onclick="RollBackBtn()" name="btnRollBack">Roll Back</button></td>
                                    </tr>
                                    <tr>
                                        <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:center">
                                            <button class="buttonDEAD" style="height: 40px; width: 100px; font: bold 18px arial" onclick="DeleteBtn()" name="btnDelete">Delete</button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:center">
                                            <button class="buttonDEAD" style="height: 40px; width: 100px; font: bold 18px arial" onclick="PrintBtn()" name="btnPrint">Print</button></td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table border="0" width="100" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="control" style="text-align: right; font: bold 18px arial; white-space: nowrap;">Total:</td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; font: bold 18px arial; white-space: nowrap;">Tax:</td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; font: bold 18px arial; white-space: nowrap;">Grand Total:</td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; font: bold 18px arial; white-space: nowrap;">Cash:</td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; font: bold 18px arial; white-space: nowrap;">Check:</td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; font: bold 18px arial; white-space: nowrap;">Credit:</td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; font: bold 18px arial; white-space: nowrap;">Gift Card:</td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; font: bold 18px arial; white-space: nowrap;">Account:</td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; font: bold 18px arial; white-space: nowrap;">Total Paid:</td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; font: bold 18px arial; white-space: nowrap;">Balance Due:</td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; font: bold 18px arial; white-space: nowrap;">Cash Back:</td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table border="0" width="70" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="control" style="text-align: right; white-space: nowrap;">
                                            <label style="font: bold 18px arial" class="blkdata" id="lblTotal"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; white-space: nowrap;">
                                            <label style="font: bold 18px arial" class="blkdata" id="lblTax"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; white-space: nowrap;">
                                            <label style="font: bold 18px arial" class="reddata" id="lblGTotal"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; white-space: nowrap;">
                                            <label style="font: bold 18px arial" class="blkdata" id="lblCashAmt"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; white-space: nowrap;">
                                            <label style="font: bold 18px arial" class="blkdata" id="lblCheckAmt"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; white-space: nowrap;">
                                            <label style="font: bold 18px arial" class="blkdata" id="lblChargeAmt"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; white-space: nowrap;">
                                            <label style="font: bold 18px arial" class="blkdata" id="lblGiftCardAmt"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; white-space: nowrap;">
                                            <label style="font: bold 18px arial" class="blkdata" id="lblAccAmt"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; white-space: nowrap;">
                                            <label style="font: bold 18px arial" class="Grndata" id="lblTotalAmt"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; white-space: nowrap;">
                                            <label style="font: bold 18px arial" class="reddata" id="lblBalance"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="control" style="text-align: right; white-space: nowrap;">
                                            <label style="font: bold 18px arial" class="blkdata" id="lblCashBack"></label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table border="0" width="20" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td  style="text-align: left; white-space: nowrap;">&nbsp;</td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>

                </table>
            </td>
            </tr>
            </table>
    </div>
    </form>
</body>
</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/VBSCRIPT">
Option Explicit

Sub window_onload()
    dim txtRecNote

	document.all("intTotalAmt").value = cdbl(document.all("intGiftCardAmt").value)+cdbl(document.all("intCashAmt").value)+cdbl(document.all("intCheckAmt").value)+cdbl(document.all("intChargeAmt").value)+cdbl(document.all("intAccAmt").value)
	document.all("lblTotal").innerText = formatcurrency(document.all("intTotal").value,2)
	document.all("lblTax").innerText = formatcurrency(document.all("intTax").value,2)
	document.all("lblGTotal").innerText = formatcurrency(document.all("intGTotal").value,2)
	document.all("lblGiftCardAmt").innerText = formatcurrency(document.all("intGiftCardAmt").value,2)
	document.all("lblCashAmt").innerText = formatcurrency(document.all("intCashAmt").value,2)
	document.all("lblCheckAmt").innerText = formatcurrency(document.all("intCheckAmt").value,2)
	document.all("lblChargeAmt").innerText = formatcurrency(document.all("intChargeAmt").value,2)
	document.all("lblCashBack").innerText = formatcurrency(document.all("intCashBack").value,2)
	document.all("lblAccAmt").innerText = formatcurrency(document.all("intAccAmt").value,2)
	document.all("lblTotalamt").innerText = formatcurrency(document.all("intTotalAmt").value,2)
	document.all("intBalance").value = cdbl(document.all("intTotalAmt").value)-cdbl(document.all("lblGTotal").innerText)
	document.all("lblBalance").innerText = formatcurrency(document.all("intBalance").value,2)

	IF document.all("intStatus").value => 70 then
		document.All("btnCash").ClassName="buttondead"
		document.All("btnCheck").ClassName="buttondead"
		document.All("btnDiscount").ClassName="buttondead"
		document.All("btnCredit").ClassName="buttondead"
		document.All("btnGiftCard").ClassName="buttondead"
		document.All("btnAccount").ClassName="buttondead"
		document.All("btnSearch").ClassName="buttondead"
		document.All("btnAdd").ClassName="buttondead"
		document.All("btnPrint").ClassName="button"
		document.All("btnRollBack").ClassName="button"
		document.All("btnDelete").ClassName="button"
		IF round(cdbl(document.all("intTotalAmt").value),2) >= round(cdbl(document.all("intGTotal").value),2) then
			document.all("intCashBack").value = round(cdbl(document.all("intTotalAmt").value) - cdbl(document.all("intGTotal").value),2)
			document.all("lblCashBack").innerText = formatcurrency(document.all("intCashBack").value,2)
		ELSE
			document.all("intCashBack").value = 0.0
			document.all("lblCashBack").innerText = formatcurrency(document.all("intCashBack").value,2)
		END IF


	ELSE
		IF document.all("intRecID").value > 0 then
			document.All("btnCash").ClassName="button"
			'document.All("btnCheck").ClassName="button"
			document.All("btnDiscount").ClassName="button"
			IF document.all("intCustAccID").value > 0 then
				document.All("btnAccount").ClassName="buttonRed"
			END IF
		END IF
		IF round(cdbl(document.all("intTotalAmt").value),2) >= round(cdbl(document.all("intGTotal").value),2) then
			document.all("intCashBack").value = round(cdbl(document.all("intTotalAmt").value) - cdbl(document.all("intGTotal").value),2)
			document.all("lblCashBack").innerText = formatcurrency(document.all("intCashBack").value,2)
			document.All("btnComplete").ClassName="button"
		ELSE
			document.all("intCashBack").value = 0.0
			document.all("lblCashBack").innerText = formatcurrency(document.all("intCashBack").value,2)
			document.All("btnComplete").ClassName="buttondead"
		END IF
	END IF
	IF document.all("blAddItem").value = "Yes" then
		Dim retDel,hdnArr,hdnValue
		hdnValue = document.all("hdnValue").value
		hdnArr = split(hdnValue,"|")
		'msgbox hdnArr(1)
		IF hdnArr(1) = "178" then
			retDel= ShowModalDialog("CashOutEditDlg.asp?intRecItemID="& hdnArr(2)  &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:400px;dialogheight:300px;")
			fraMain.frmMain.submit()
		END IF
	END IF
    IF (document.all("intClientID").value > 2) and (len(trim(document.all("strEmail").value))=0) and (document.all("blnNoEmail").value = "False") and (document.all("blnCNXEmail").value = 0) then 
		Dim retEmail
        
			retEmail= ShowModalDialog("CashOutEmailDlg.asp?intClientID="& document.all("intClientID").value  ,"","dialogwidth:450px;dialogheight:200px;")
            IF len(trim(retEmail))>0 then
                document.all("blnCNXEmail").value = 1
            END IF
 
    END IF
    
    txtRecNote =""
    txtRecNote = trim(replace(trim(document.frmMain.txtRecNote.value),"	",""))

    If Len(txtRecNote) > 5 then
    
       ' msgbox txtRecNote
    end if
	document.frmMain.tValue.focus()
	document.frmMain.tValue.select()
End Sub


Sub button(Val)
	IF document.frmMain.hdnObj.value = 1 then
		IF instr(1,val,"-")>0 then
			IF instr(1,document.frmMain.tValue.value,"-")>0 then
				document.frmMain.tValue.value = replace(document.frmMain.tValue.value,"-","")
			ELSE 
				document.frmMain.tValue.value = trim(cstr(trim(Val))&trim(document.frmMain.tValue.value))
			END IF
		ELSE
			IF instr(1,val,".")>0 then
				IF instr(1,document.frmMain.tValue.value,".")>0 then
					'msgbox cstr(cdbl(document.frmMain.tValue.value) + cdbl(Val))
					IF instr(1,Val,".")>0 and LEN(TRIM(Val))>3 then
						document.frmMain.tValue.value = formatnumber((cdbl(document.frmMain.tValue.value) + cdbl(Val)),2)
					ELSE
						document.frmMain.tValue.value = trim(trim(document.frmMain.tValue.value) & cstr(trim(Val)))
					END IF
				ELSE
					document.frmMain.tValue.value = trim(trim(document.frmMain.tValue.value) & cstr(trim(Val)))
				END IF
			ELSE
				document.frmMain.tValue.value = trim(trim(document.frmMain.tValue.value) & cstr(trim(Val)))
			END IF
		END IF
	ELSE
		IF instr(1,val,"-")=0 and instr(1,val,".")=0 then
			document.frmMain.cboQty.value = trim(trim(document.frmMain.cboQty.value) & cstr(trim(Val)))
		END IF
	END IF
End Sub

sub SetObj(val)
	document.frmMain.hdnObj.value = val
End Sub

Sub Check4Enter()
	If window.event.keycode = 13 Then
		window.event.cancelbubble = True
		window.event.returnvalue = FALSE
		call Srecbtn()
	End If
End Sub

Sub DeleteText()
	IF document.frmMain.hdnObj.value = 1 then
		IF len(trim(document.frmMain.tValue.value))>0 then
			document.frmMain.tValue.value = left(trim(document.frmMain.tValue.value),len(trim(document.frmMain.tValue.value))-1)
		END IF
	ELSE
		IF len(trim(document.frmMain.cboQty.value))>0 then
			document.frmMain.cboQty.value = left(trim(document.frmMain.cboQty.value),len(trim(document.frmMain.cboQty.value))-1)
		END IF
	END IF
End Sub

Sub ClearText()
	IF document.frmMain.hdnObj.value = 1 then
		document.ALL("tValue").value = ""
		document.ALL("tValue").innerText = ""	
	ELSE
		document.ALL("cboQty").value = ""
		document.ALL("cboQty").innerText = ""	
	END IF
End Sub

Sub Cashbtn()
	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	IF len(trim(document.frmMain.tValue.value))>0 then
		document.all("intCashAmt").value=trim(document.frmMain.tValue.value)
		document.all("lblCashAmt").innerText = formatcurrency(document.all("intCashAmt").value,2)
		document.all("intTotalAmt").value = cdbl(document.all("intGiftCardAmt").value)+cdbl(document.all("intCashAmt").value)+ cdbl(document.all("intCheckAmt").value)+cdbl(document.all("intChargeAmt").value)+cdbl(document.all("intAccAmt").value)
		document.all("lblTotalamt").innerText = formatcurrency(document.all("intTotalAmt").value,2)
		document.all("intBalance").value = cdbl(document.all("intTotalAmt").value)-cdbl(document.all("lblGTotal").innerText)
		document.all("lblBalance").innerText = formatcurrency(document.all("intBalance").value,2)
		IF round(cdbl(document.all("intTotalAmt").value),2) >= round(cdbl(document.all("intGTotal").value),2) then
			document.all("intCashBack").value = Round(cdbl(document.all("intTotalAmt").value) - cdbl(document.all("intGTotal").value),2)
			document.all("lblCashBack").innerText = formatcurrency(document.all("intCashBack").value,2)
			document.All("btnComplete").ClassName="button"
		ELSE
			document.all("intCashBack").value = 0.0
			document.all("lblCashBack").innerText = formatcurrency(document.all("intCashBack").value,2)
			document.All("btnComplete").ClassName="buttondead"
		END IF
	document.frmMain.tValue.value = ""
	end if
End Sub

Sub Creditbtn()
	Dim retCard,intChargeAmt,arrData

	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	intChargeAmt = round((cdbl(document.all("intGTotal").value) - cdbl(document.all("intTotalAmt").value)),2)
	IF intChargeAmt > 0 then
		retCard= ShowModalDialog("CashOutCardDlg.asp?intChargeAmt=" & intChargeAmt ,"","dialogwidth:350px;dialogheight:600px;")
		IF len(trim(retCard)) > 0 then
			arrData = Split(retCard,"|")
				
				
			document.all("intCardType").value = cint(arrData(0))
			intChargeAmt = arrData(1)
			document.all("intChargeAmt").value = intChargeAmt
			document.all("lblChargeAmt").innerText = formatcurrency(intChargeAmt,2)
			document.all("intTotalAmt").value = cdbl(document.all("intGiftCardAmt").value)+cdbl(document.all("intCashAmt").value)+cdbl(document.all("intCheckAmt").value)+cdbl(document.all("intChargeAmt").value)+cdbl(document.all("intAccAmt").value)
			document.all("lblTotalamt").innerText = formatcurrency(document.all("intTotalAmt").value,2)
		    document.all("intBalance").value = cdbl(document.all("intTotalAmt").value)-cdbl(document.all("lblGTotal").innerText)
		    document.all("lblBalance").innerText = formatcurrency(document.all("intBalance").value,2)
			IF round(cdbl(document.all("intTotalAmt").value),2) >= round(cdbl(document.all("intGTotal").value),2) then
				document.all("intCashBack").value = round(cdbl(document.all("intTotalAmt").value) - cdbl(document.all("intGTotal").value),2)
				document.all("lblCashBack").innerText = formatcurrency(document.all("intCashBack").value,2)
				document.All("btnComplete").ClassName="button"
			ELSE
				document.all("intCashBack").value = 0.0
				document.all("lblCashBack").innerText = formatcurrency(document.all("intCashBack").value,2)
				document.All("btnComplete").ClassName="buttondead"
			END IF
			document.frmMain.tValue.value = ""
		ELSE
			EXIT SUB
		END IF
	END IF
End Sub

Sub GiftCardbtn()
	Dim GiftCard,intGiftCardAmt,arrgfData

	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	intGiftCardAmt = round((cdbl(document.all("intGTotal").value) - cdbl(document.all("intTotalAmt").value)),2)
	IF intGiftCardAmt > 0 then
		GiftCard= ShowModalDialog("CashOutGiftCardDlg.asp?intGiftCardAmt=" & intGiftCardAmt &"&intREcID="& document.all("intrecID").value &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value  ,"","dialogwidth:350px;dialogheight:450px;")
		IF len(trim(GiftCard)) > 0 then
			arrgfData = Split(GiftCard,"|")
			document.all("intGiftCardID").value = trim(arrgfData(0))
			intGiftCardAmt = arrgfData(1)
			document.all("intGiftCardAmt").value = intGiftCardAmt
			document.all("lblGiftCardAmt").innerText = formatcurrency(intGiftCardAmt,2)
			document.all("intTotalAmt").value = cdbl(document.all("intGiftCardAmt").value)+cdbl(document.all("intCashAmt").value)+cdbl(document.all("intCheckAmt").value)+cdbl(document.all("intChargeAmt").value)+cdbl(document.all("intAccAmt").value)
			document.all("lblTotalAmt").innerText = formatcurrency(document.all("intTotalAmt").value,2)
			document.all("intBalance").value = cdbl(document.all("intTotalAmt").value)-cdbl(document.all("lblGTotal").innerText)
			document.all("lblBalance").innerText = formatcurrency(document.all("intBalance").value,2)
			IF round(cdbl(document.all("intTotalAmt").value),2) >= round(cdbl(document.all("intGTotal").value),2) then
				document.all("intCashBack").value = round(cdbl(document.all("intTotalAmt").value) - cdbl(document.all("intGTotal").value),2)
				document.all("lblCashBack").innerText = formatcurrency(document.all("intCashBack").value,2)
				document.All("btnComplete").ClassName="button"
			ELSE
				document.all("intCashBack").value = 0.0
				document.all("lblCashBack").innerText = formatcurrency(document.all("intCashBack").value,2)
				document.All("btnComplete").ClassName="buttondead"
			END IF
			document.frmMain.tValue.value = ""
		ELSE
			EXIT SUB
		END IF
	END IF
End Sub

Sub Checkbtn()
	Dim retCard,intCheckAmt,arrData
	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	intCheckAmt = round((cdbl(document.all("intGTotal").value) - cdbl(document.all("intTotalAmt").value)),2)
	IF intCheckAmt > 0 then
		retCard= ShowModalDialog("CashOutCheckDlg.asp?intCheckAmt=" & intCheckAmt ,"","dialogwidth:450px;dialogheight:500px;")
		IF len(trim(retCard)) > 0 then
			arrData = Split(retCard,"|")
			intCheckAmt = arrData(0)
			document.all("strChkNo").value = arrData(1)
			document.all("strChkPhone").value = arrData(2)
			document.all("strChkDL").value = arrData(3)
			document.all("intCheckAmt").value = intCheckAmt
			document.all("lblCheckAmt").innerText = formatcurrency(intCheckAmt,2)
			document.all("intTotalAmt").value = cdbl(document.all("intGiftCardAmt").value)+cdbl(document.all("intCashAmt").value)+cdbl(document.all("intCheckAmt").value)+cdbl(document.all("intChargeAmt").value)+cdbl(document.all("intAccAmt").value)
			document.all("lblTotalamt").innerText = formatcurrency(document.all("intTotalAmt").value,2)
		document.all("intBalance").value = cdbl(document.all("intTotalAmt").value)-cdbl(document.all("lblGTotal").innerText)
		document.all("lblBalance").innerText = formatcurrency(document.all("intBalance").value,2)
			IF round(cdbl(document.all("intTotalAmt").value),2) >= round(cdbl(document.all("intGTotal").value),2) then
				document.all("intCashBack").value = round(cdbl(document.all("intTotalAmt").value) - cdbl(document.all("intGTotal").value),2)
				document.all("lblCashBack").innerText = formatcurrency(document.all("intCashBack").value,2)
				document.All("btnComplete").ClassName="button"
			ELSE
				document.all("intCashBack").value = 0.0
				document.all("lblCashBack").innerText = formatcurrency(document.all("intCashBack").value,2)
				document.All("btnComplete").ClassName="buttondead"
			END IF
			document.frmMain.tValue.value = ""
		ELSE
			EXIT SUB
		END IF
	END IF
End Sub


Sub Discountbtn()
	Dim retDisc
	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	retDisc= ShowModalDialog("CashOutDiscDlg.asp" ,"","dialogwidth:350px;dialogheight:450px;")
	IF len(trim(retDisc)) > 0 then
		document.frmMain.hdnValue.value = "2|"&retDisc
		'msgbox retDisc
        frmMain.FormAction.value="btnAdd"
		frmMain.Submit()
	ELSE
		EXIT SUB
	END IF
End Sub

Sub PrintBtn()
	frmMain.FormAction.value="btnPrint"
	frmMain.Submit()
End Sub

Sub DeleteBtn()
	Dim Answer
	window.event.cancelBubble = false
	Answer = MsgBox("Are you sure you want to Delete this Ticket?",276,"Confirm Delete")
	If Answer = 6 then
		frmMain.FormAction.value="btnDelete"
		frmMain.submit()
	Else
		Exit Sub
	End if
End Sub

Sub RollBackBtn()
	Dim Answer
	window.event.cancelBubble = false
	Answer = MsgBox("Are you sure you want to Roll Back this Ticket?",276,"Confirm Roll Back")
	If Answer = 6 then
		frmMain.FormAction.value="btnRollBack"
		frmMain.submit()
	Else
		Exit Sub
	End if
End Sub

Sub Accountbtn()
	Dim AccAmt,intCustAccID,intAccAmt
	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	intAccAmt = round((cdbl(document.all("intGTotal").value) - cdbl(document.all("intTotalAmt").value)),2)
	IF intAccAmt > 0 then
		AccAmt= ShowModalDialog("CashOutAccDlg.asp?intAccAmt=" & intAccAmt &"&intCustAccID="& document.all("intCustAccID").value &"&intRecID="& document.all("intrecID").value &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:500px;dialogheight:450px;")
		IF len(trim(AccAmt)) > 0 then
			intAccAmt = AccAmt
			document.all("intAccAmt").value = intAccAmt
			document.all("lblAccAmt").innerText = formatcurrency(intAccAmt,2)
			document.all("intTotalAmt").value = cdbl(document.all("intGiftCardAmt").value)+cdbl(document.all("intCashAmt").value)+cdbl(document.all("intCheckAmt").value)+cdbl(document.all("intChargeAmt").value)+cdbl(document.all("intAccAmt").value)
			document.all("lblTotalamt").innerText = formatcurrency(document.all("intTotalAmt").value,2)
		document.all("intBalance").value = cdbl(document.all("intTotalAmt").value)-cdbl(document.all("lblGTotal").innerText)
		document.all("lblBalance").innerText = formatcurrency(document.all("intBalance").value,2)
			IF round(cdbl(document.all("intTotalAmt").value),2) >= round(cdbl(document.all("intGTotal").value),2) then
				document.all("intCashBack").value = round(cdbl(document.all("intTotalAmt").value) - cdbl(document.all("intGTotal").value),2)
				document.all("lblCashBack").innerText = formatcurrency(document.all("intCashBack").value,2)
				document.All("btnComplete").ClassName="button"
			ELSE
				document.all("intCashBack").value = 0.0
				document.all("lblCashBack").innerText = formatcurrency(document.all("intCashBack").value,2)
				document.All("btnComplete").ClassName="buttondead"
			END IF
			document.frmMain.tValue.value = ""
		ELSE
			EXIT SUB
		END IF
	END IF
End Sub
Sub Addbtn()
	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	IF len(trim(document.frmMain.tValue.value))>0 then
		IF isnumeric(document.frmMain.tValue.value) then
			document.frmMain.hdnValue.value = "0|"&trim(document.frmMain.tValue.value)
			frmMain.FormAction.value="btnAdd"
			frmMain.Submit()
		ELSE
			document.frmMain.hdnValue.value = "1|"&trim(document.frmMain.hdnValue.value)
			frmMain.FormAction.value="btnAdd"
			frmMain.Submit()
		END IF
	END IF
End Sub
Sub Newbtn()
	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
		frmMain.FormAction.value="btnNew"
		frmMain.Submit()
End Sub

Sub Srecbtn()
	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	IF len(trim(document.frmMain.tValue.value))>0 then
		document.frmMain.intRecID.value = trim(document.frmMain.tValue.value)
		frmMain.FormAction.value="Srecbtn"
		frmMain.Submit()
	END IF

End Sub


Sub Searchbtn()
	Dim retItem,ItemArr
	'intdata = frmMain.intClientID.value&"|"&document.frmmain.cboVehMan.value&"|"&document.frmmain.strVModel.value&"|"&document.frmmain.cboVehMod.value&"|"&document.frmmain.cboVehColor.value
	retItem= ShowModalDialog("CashOutSelItemDlg.asp?LocationID="& document.all("LocationID").value ,"","dialogwidth:600px;dialogheight:350px;")
	IF len(trim(retItem))>0 then
		ItemArr = split(retItem,"|")
		IF ItemArr(2) = 1 then
			document.frmMain.tValue.value = ItemArr(1)
			document.frmMain.hdnValue.value = ItemArr(0)
			document.frmMain.hdnValue.value = "1|"&trim(document.frmMain.hdnValue.value)
			frmMain.FormAction.value="btnAdd"
			frmMain.Submit()
		ELSE
			document.frmMain.tValue.value = ItemArr(1)
			document.frmMain.hdnValue.value = ItemArr(0)
		END IF
	END IF
End Sub
Sub ProcessBtn()
	Dim Answer
	window.event.cancelBubble = false
	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
    IF document.all("intChargeAmt").value = 0 then
    	    Answer = MsgBox("Does the customer need a Receipt?",276,"Need Reciept?")
	    If Answer = 6 then
            frmMain.blnPrintReceipt.value=1
		    frmMain.FormAction.value="btnProcess"
		    frmMain.Submit()
	    Else
            frmMain.blnPrintReceipt.value=0
            frmMain.FormAction.value="btnProcess"
		    frmMain.Submit()
	    End if
    else
        frmMain.blnPrintReceipt.value=0
        frmMain.FormAction.value="btnProcess"
		frmMain.Submit()
    end if
End Sub

Sub ClearBtn()
		frmMain.Submit()
End Sub


</script>


<%

End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Sub PrintReceipt(dbMain,intRecID,intreg,LocationID,LoginID)
	Dim strSQL
			strSQL= " Insert into PrintREC(LocationID,RecID,PrinterName)Values(" & _
				LocationID & ", " & _
				intrecID & ", " & _
				"'Receipt1') " 
		If NOT (dbExec(dbMain,strSQL)) Then
			Response.Write gstrMsg
			Response.end
		END IF

	Response.Redirect "CashOut.asp?intreg="& intreg &"&intRecID=0&LocationID="& LocationID &"&LoginID="& LoginID
End Sub

Sub RollBackReceipt(dbMain,intRecID,intreg,LocationID,LoginID)
	Dim strSQL,rs,intCustAccID,strTxAmt,strNewAmt,strCurrentAmt
	strSQL= "	UPDATE REC Set " & _
			"	CashAmt = 0,"  & _
			"	GiftCardAmt = 0,"  & _
			"	ChargeAmt =  0,"  & _
			"	CheckAmt =  0,"  & _
			"	ChkNo =  '',"  & _
			"	ChkPhone =  '',"  & _
			"	ChkDL =  '',"  & _
			"	GiftCardID =  '',"  & _
			"	CardType =0,"  & _
			"	CashBack = 0,"  & _
			"	AccAmt = 0,"  & _
			"	GTotal = 0,"  & _
			"	TotalAmt =0,"  & _
			"   CashoutID= 0," & _
			"   CloseDte= ''," & _
			"   Status= 0," & _
			"	Tax = 0" & _
			"	WHERE recID=" & intrecID  &" and LocationID= "& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	END IF

	strSQL= "	Delete DetailComp WHERE recID=" & intrecID  &" and LocationID= "& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
		Response.end
	End If
	strSQL= "	Delete GiftCardHist WHERE recID=" & intrecID  &" and LocationID= "& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
		Response.end
	End If
	strSQL =" Select CustAccID,TxAmt from CustAccHist (nolock) where TXrecid="& intRecID  &" and TXLocationID= "& LocationID
	If DBOpenRecordset(dbMain,rs,strSQL) Then
		If Not RS.EOF Then
			intCustAccID = rs("CustAccID")
			strTxAmt = rs("TxAmt")
			
			strSQL= "	Delete CustAccHist WHERE txrecID=" & intrecID  &" and TXLocationID= "& LocationID
			If NOT (dbExec(dbMain,strSQL)) Then
				Response.Write gstrMsg
				Response.end
			End If
			strSQL="SELECT CurrentAmt FROM CustAcc(Nolock) WHERE CustAccID=" & intCustAccID 
			If DBOpenRecordset(dbMain,rs,strSQL) Then
				If Not RS.EOF Then
					strCurrentAmt = rs("CurrentAmt")
				END IF
			END IF
			strNewAmt = strCurrentAmt - strTxAmt
			strSQL= "Update CustAcc SET "&_
				" CurrentAmt=" & strNewAmt &","&_
				" LastUpdate='" & date() &"',"&_
				" LastUpdateBy=" & LoginID &_
				" Where CustAccID ="& intCustAccID 
			IF NOT DBExec(dbMain, strSQL) then
				Response.Write gstrMsg
				Response.End
			END IF
		End If
	End If
	Set RS = Nothing

	Response.Redirect "CashOut.asp?intreg="& intreg &"&intRecID=0&LocationID="& LocationID &"&LoginID="& LoginID
End Sub

Sub DeleteReceipt(dbMain,intRecID,intreg,LocationID,LoginID)
	Dim strSQL,rs,intCustAccID,strTxAmt,strNewAmt,strCurrentAmt
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
	strSQL= "	Delete DetailComp WHERE recID=" & intrecID &" and LocationID= "& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
		Response.end
	End If
	strSQL= "	Delete GiftCardHist WHERE recID=" & intrecID &" and LocationID= "& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
		Response.end
	End If

	strSQL =" Select CustAccID,TxAmt from CustAccHist (nolock) where TXrecid="& intRecID &" and TXLocationID = "& LocationID
	If DBOpenRecordset(dbMain,rs,strSQL) Then
		If Not RS.EOF Then
			intCustAccID = rs("CustAccID")
			strTxAmt = rs("TxAmt")
			
			strSQL= "	Delete CustAccHist WHERE txrecID=" & intrecID &" and TXLocationID= "& LocationID
			If NOT (dbExec(dbMain,strSQL)) Then
				Response.Write gstrMsg
				Response.end
			End If
			strSQL="SELECT CurrentAmt FROM CustAcc(Nolock) WHERE CustAccID=" & intCustAccID 
			If DBOpenRecordset(dbMain,rs,strSQL) Then
				If Not RS.EOF Then
					strCurrentAmt = rs("CurrentAmt")
				END IF
			END IF
			strNewAmt = strCurrentAmt - strTxAmt
			strSQL= "Update CustAcc SET "&_
				" CurrentAmt=" & strNewAmt &","&_
				" LastUpdate='" & date() &"',"&_
				" LastUpdateBy=" & LoginID &_
				" Where CustAccID ="& intCustAccID 
			IF NOT DBExec(dbMain, strSQL) then
				Response.Write gstrMsg
				Response.End
			END IF
		End If
	End If
	Set RS = Nothing
	Response.Redirect "CashOut.asp?intreg="& intreg &"&intRecID=0&LocationID="& LocationID &"&LoginID="& LoginID
End Sub

Sub SelectRec(dbMain,intRecID,intStatus,LocationID)
	Dim rs,strSQL
	strSQL =" Select recID,status from REC (nolock) where recid="& intRecID  &" and LocationID ="& LocationID
	If DBOpenRecordset(dbMain,rs,strSQL) Then
		If Not RS.EOF Then
			intRecID = RS("recID")
			intStatus =  RS("status")
		ELSE
			intRecID = 0
			intStatus =  0
		End If
	End If
	Set RS = Nothing
END sub


Sub NewRec(dbMain,intRecID,LocationID,LoginID)
	Dim rsData,strSQL,intLine
		strSQL = "Select recID=IsNull(Max(recID),0) + 1 From Rec where LocationID = "& LocationID
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			intrecID = rsData("recID")
		End If
		strSQL= " Insert into REC(recID,LocationID,ClientID,vehid, VehMan, VehMod, VehColor,"&_
				" datein, SalesRep,line)Values(" & _
				intrecID & ", " & _
				LocationID & ", " & _
				2 & ", " & _
				0 & ", " & _
				0 & "," & _
				0 & ", " & _
				0 & ", " & _
				"'"& date() &"'," & _
				LoginID & "," & _
				7 & ") " 
		If NOT (dbExec(dbMain,strSQL)) Then
			Response.Write gstrMsg
		End If
END sub


Sub AddItem(dbMain,hdnValue,LocationID)
	Dim rsData,strSQL,intRecID,intrecItemID,intProdID,strPrice,strComm,strTaxable,strTaxAmt
	Dim intQTY,intUPC,dArr,intValue,rsData2,strSQL2,strService
	intRecID = request("intrecID")
	intQTY = request("cboQty") 
	if len(trim(intQTY))= 0 then
	intQTY = 1
	END IF
	dArr = split(trim(hdnValue),"|")
	intUPC = dArr(0)
	intValue =  dArr(1)
	Select Case intUPC
		Case 0	' UPC
			strSQL = "SELECT Product.ProdID,Product.Price, Product.Comm, Product.Taxable,LM_Locations.TaxRate "&_
			" FROM Product(nolock), LM_Locations(nolock)"&_
			" WHERE (ltrim(Product.bcode) = '"& trim(intValue) &"') AND (LM_Locations.LocationID = "& LocationID &")"
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof then
					IF rsData("Taxable")=1 then
						intProdID = rsData("ProdID")
						strPrice = nulltest(rsData("Price"))* cdbl(intQTY)
						strComm = nulltest(rsData("Comm"))
						strTaxable = 1
						strTaxAmt = round((cdbl(rsData("Price"))* cdbl(intQTY) *cdbl(rsData("TaxRate"))),2)
					ELSE
						intProdID = rsData("ProdID")
						strPrice = nulltest(rsData("Price"))* cdbl(intQTY)
						strComm = nulltest(rsData("Comm"))
						strTaxable = 0
						strTaxAmt = 0.0
					END IF
				ELSE
					Exit Sub
				END IF
			End If
		Case 1	' Prod ID
			strSQL = "SELECT Product.ProdID,Product.Price, Product.Comm, Product.Taxable,LM_Locations.TaxRate "&_
			" FROM Product(nolock), LM_Locations(nolock)"&_
			" WHERE (ltrim(Product.ProdID) = '"& trim(intValue) &"') AND (LM_Locations.LocationID = "& LocationID &")"
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof then
					IF rsData("Taxable")=1 then
						intProdID = rsData("ProdID")
						strPrice = nulltest(rsData("Price"))* cdbl(intQTY)
						strComm = nulltest(rsData("Comm"))
						strTaxable = 1
						strTaxAmt = round((cdbl(rsData("Price"))* cdbl(intQTY) *cdbl(rsData("TaxRate"))),2)
					ELSE
						intProdID = rsData("ProdID")
						strPrice = nulltest(rsData("Price"))* cdbl(intQTY)
						strComm = nulltest(rsData("Comm"))
						strTaxable = 0
						strTaxAmt = 0.0
					END IF
				ELSE
					Exit Sub
				END IF
			End If
		Case 2 ' Discount
			strSQL = "SELECT Product.ProdID,Product.PerAdj,Product.Price, Product.Comm, Product.Taxable,LM_Locations.TaxRate "&_
			" FROM Product(nolock), LM_Locations(nolock)"&_
			" WHERE (ltrim(Product.ProdID) = '"& trim(intValue) &"') AND (LM_Locations.LocationID = "& LocationID &")"
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof then
					IF rsData("PerAdj")=1 then
						strSQL2=" SELECT SUM(RECITEM.Price) as Service FROM RECITEM (nolock)"&_
								" INNER JOIN Product(nolock) ON RECITEM.ProdID = Product.ProdID"&_
								" AND ((CAT = 1 OR CAT = 9 OR CAT = 13 OR CAT = 14 OR CAT = 15 OR CAT = 16)"&_
								" OR  (CAT = 2 OR CAT = 10 OR CAT = 17 OR CAT = 18 OR CAT = 19 OR CAT = 20))"&_
								" WHERE (RECITEM.recId = "& intRecID & ") and (RECITEM.LocationID = "& LocationID & ")"
						If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
							IF NOT rsData2.eof then
								strService = rsData2("Service")
							END IF
						END IF
						IF strService > 0 then
							intProdID = rsData("ProdID")
							strPrice = strService *(cdbl(nulltest(rsData("Price")))*0.01)
							strComm = nulltest(rsData("Comm"))
							strTaxable = 0
							strTaxAmt = 0.0
						ELSE
							exit sub
						END IF
					ELSE
						IF rsData("Taxable")=1 then
							intProdID = rsData("ProdID")
							strPrice = nulltest(rsData("Price"))* cdbl(intQTY)
							strComm = nulltest(rsData("Comm"))
							strTaxable = 1
							strTaxAmt = round((cdbl(rsData("Price"))* cdbl(intQTY) *cdbl(rsData("TaxRate"))),2)
						ELSE
							intProdID = rsData("ProdID")
							strPrice = nulltest(rsData("Price"))* cdbl(intQTY)
							strComm = nulltest(rsData("Comm"))
							strTaxable = 0
							strTaxAmt = 0.0
						END IF
					END IF
				ELSE
					Exit Sub
				END IF
			End If
	END Select	
	strSQL = "Select recItemID=IsNull(Max(recItemID),0) + 1 From RecItem"
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
			strComm &"," &_
			intQTY &")" 
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	End If
	hdnValue = hdnValue+"|"+cstr(intrecItemID)
END sub

Sub ProcessData(dbMain,intRecID,LocationID,LoginID)
	Dim strSQL,intCashAmt,intGiftCardAmt,intChargeAmt,intCardType,intCashBack,intAccAmt,intGTotal
	Dim intCheckAmt,strChkNo,strChkPhone,strChkDL,intGiftCardID
	Dim intTotal,intTax,rs,intPrice,MaxSQL,intGiftCardTID,intReg
	
	
	intCashAmt = request("intCashAmt") 
	intChargeAmt = request("intChargeAmt") 
	intCashBack = request("intCashBack")
	intAccAmt = request("intAccAmt") 
	intCheckAmt = request("intCheckAmt") 
	intGiftCardAmt = request("intGiftCardAmt") 
	strChkNo = request("strChkNo") 
	strChkPhone = request("strChkPhone") 
	strChkDL = request("strChkDL") 
	intGTotal = request("intGTotal") 
	intTotal = request("intTotal") 
	intTax = request("intTax") 
	intCardType = request("intCardType") 
	intGiftCardID = request("intGiftCardID") 
	intReg = request("intReg") 
	IF len(trim(intCardType))=0 then
		intCardType = 0
	END IF
	strSQL= "	UPDATE REC Set " & _
			"	CashAmt = " & intCashAmt  &","  & _
			"	GiftCardAmt = " & intGiftCardAmt  &","  & _
			"	ChargeAmt =  " & intChargeAmt  &","  & _
			"	CheckAmt =  " & intCheckAmt  &","  & _
			"	ChkNo =  '" & strChkNo  &"',"  & _
			"	ChkPhone =  '" & strChkPhone  &"',"  & _
			"	ChkDL =  '" & strChkDL  &"',"  & _
			"	GiftCardID =  '" & intGiftCardID  &"',"  & _
			"	CardType =" &  intCardType  &","  & _
			"	CashBack = " & intCashBack &","  & _
			"	AccAmt = " & intAccAmt  &","  & _
			"	GTotal = " & intGTotal &","  & _
			"	TotalAmt = " & intTotal  &","  & _
			"   CashoutID= " & LoginID & "," & _
			"   CloseDte= '"& date() &"'," & _
			"   Status= 70," & _
			"   Reg= " & intReg  &","  & _
			"	Tax = " & intTax & _
			"	WHERE recID=" & intrecID & " and LocationID = "& LocationID
	If NOT (dbExec(dbMain,strSQL)) Then
		Response.Write gstrMsg
	END IF

	strSQL="SELECT RECITEM.GiftCardID, RECITEM.Price"&_
	" FROM RECITEM (nolock) "&_
	" WHERE RECITEM.RecID =" & intRecID & " and LocationID = "& LocationID &_
	" AND LEN(LTRIM(RECITEM.GiftCardID))>0 "

	If DBOpenRecordset(dbMain,rs,strSQL) Then
		If Not RS.EOF Then
			intPrice = RS("Price")
			intGiftCardID = RS("GiftCardID")

			MaxSQL = " Select ISNULL(Max(GiftCardTID),0)+1 from GiftCardHist (NOLOCK)"
			If dbOpenRecordSet(dbmain,RS,MaxSQL) Then
				intGiftCardTID=RS(0)
			End if
			Set RS = Nothing
			strSQL= "Insert into GiftCardHist (GiftCardTID,GiftCardID,TransDte,TransType,TransAmt,TransUserID,RecID,LocationID) Values ("&_
				intGiftCardTID &","&_
				"'" & intGiftCardID &"',"&_
				"'" & Date() &"',"&_
				"'Purchased',"&_
				intPrice &","& LoginID &","& intRecID &","& LocationID &")"
			IF NOT DBExec(dbMain, strSQL) then
				Response.Write gstrMsg
				Response.End
			END IF
		End If
	END IF
END sub

Function FixNumber(var)
    Dim newNum,i
    i=0
    Do While i <= len(var)
        if isnumeric(Right(Left(var,i),1)) then 
            'response.write Right(Left(var,i),1) &"<br />"
            newNum = newNum+Right(Left(var,i),1)
        end if
    i=i+1
    loop
    FixNumber = newNum
end Function

%>
