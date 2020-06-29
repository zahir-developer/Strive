<%@  language="VBSCRIPT" %>
<%
'********************************************************************
' Name: 
'********************************************************************
Option Explicit
Response.Expires = 0
Response.Buffer = True

'********************************************************************
' Include Files
'********************************************************************
%>
<!--#include file="incDatabase.asp"-->
<%
'********************************************************************
' Global Variables
'********************************************************************
Dim Title

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
	Dim dbMain, intrecID,DetailCompID,rsData,strSQL,strComm,strComAmt,strComPer,LocationID,LoginID
	Dim hdnStatus,intAssigned,intKitID,strName,intUserID,strDescript
	Dim rsData2,strSQL2,rsData3,strSQL3,intProdID,intCnt,stropt,strVal,intl2
	Set dbMain =  OpenConnection

    LocationID = request("LocationID")
    LoginID = request("LoginID")

	if len(request("intrecID"))>0 then
		intrecID = request("intrecID")
	ELSE
		intrecID = 0 
	END IF

	IF Request("FormAction") = "SaveChg" then
		Call SaveChg(dbMain)
		hdnStatus = 0 
	ELSE
		strSQL =" SELECT Product.KitID, Product.ProdID"&_
				" FROM RECITEM(NOLOCK)"&_
				" INNER JOIN Product(NOLOCK) ON RECITEM.ProdID = Product.ProdID"&_
				" WHERE (RECITEM.recId = "&intrecID &") and Product.KIT = 1"
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			IF NOT rsData.eof then
				hdnStatus = 1 ' Yes Kits
				intKitID = rsData("ProdID")
			ELSE
				hdnStatus = 0 ' No Kits
				intKitID = 0
			END IF
		End If
		Set rsData = Nothing
		IF hdnStatus = 0 then
			strSQL = "Select count(*) From DetailComp(nolock) where RecID="&intrecID
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof then
					intAssigned = rsData(0)
					IF intAssigned > 1 then
						strComPer = 100/intAssigned

						strSQL = "SELECT SUM(comm) as sumComm FROM RECITEM(nolock) WHERE recid =" & intrecID
						If DBOpenRecordset(dbMain,rsData,strSQL) Then
							IF not rsData.eof then
							strComm = rsData("sumComm")
							ELse
							strComm = 0.0
							END IF
						End If
						strComAmt = strComm * strComPer*.01
						strSQL= "	UPDATE DetailComp Set " & _
								"	Comm=" & strComm  &"," & _
								"	ComAmt=" & strComAmt &"," & _
								"	ComPer=" & strComPer & _
								"	WHERE recID=" & intrecID
						If NOT (dbExec(dbMain,strSQL)) Then
							Response.Write gstrMsg
						End If
					END IF
				ELSE
					intAssigned = 0
				END IF
			End If
			Set rsData = Nothing
		END IF
	END IF



'--------------------------------------------------------------------
' Function: Main
'
' Purpose: Entry point for the page.
'--------------------------------------------------------------------
%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css" />
    <title></title>
</head>
<body class="pgbody">
    <form method="post" name="frmMain" action="MultiDetailByDlg.asp?intrecID=<%=intrecID%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>">
        <input type="hidden" name="intRECID" value="<%=intRECID%>" />
        <input type="hidden" name="hdnStatus" value="<%=hdnStatus%>" />
        <input type="hidden" name="intKitID" value="<%=intKitID%>" />
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" value="<%=LoginID%>" />
        <div style="text-align: center">
            <table border="0" style="width: 415px; border-collapse: collapse;">
                <tr>
                    <td style="text-align: center; white-space: nowrap;" class="control">% Split:&nbsp;&nbsp;		
			<input name="chkSplit" type="checkbox">
                    </td>
                </tr>
                <%
	strSQL3 = "SELECT Product.ProdID,Product.Descript,RECITEM.Comm "&_
			" FROM RECITEM(NOLOCK) INNER JOIN Product(NOLOCK) ON RECITEM.ProdID = Product.ProdID"&_
			" WHERE (RECITEM.recId = "& intrecID &") AND RECITEM.LocationID ="& LocationID
		If DBOpenRecordset(dbMain,rsData3,strSQL3) Then
			DO While NOT rsData3.eof 
			strDescript=rsdata3("Descript")
			strComm=rsdata3("Comm")
IF len(trim(strComm))>0 then
                %>
                <tr>
                    <td>
                        <label style="white-space: nowrap;" class="control"><%=strDescript%></label></td>
                    <td style="text-align: left; white-space: nowrap;" class="control"><%=FormatCurrency(strComm,2)%></td>
                </tr>
                <%
END IF
			rsData3.MoveNext
			Loop
		End If
		Set rsData3 = Nothing


                %>

                <%  
		intCnt = 1
		strSQL = "SELECT DetailComp.UserID, LM_Users.FirstName + ' ' + LM_Users.LastName AS name"&_
				" FROM DetailComp(NOLOCK) INNER JOIN LM_Users(NOLOCK) ON DetailComp.UserID = LM_Users.UserID"&_
				" WHERE DetailComp.RecID = "&intrecID &" AND DetailComp.LocationID ="& LocationID
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			DO While NOT rsData.eof 

			strName=rsdata("name")
			intUserID=rsdata("UserID")
                %>
                <tr>
                    <td colspan="2" style="text-align: left; white-space: nowrap;" class="Header">Employee:&nbsp;<%=strName%></td>
                </tr>
                <%  
		strSQL2 = "SELECT Product.ProdID,Product.Descript, Product.Comm"&_
		" FROM ProdKit(NOLOCK) INNER JOIN Product(NOLOCK) ON ProdKit.ProductID = Product.ProdID"&_
		" WHERE ProdKit.KitID = "&intKitID
		intl2 = 1
		If DBOpenRecordset(dbMain,rsData2,strSQL2) Then
			DO While NOT rsData2.eof 

			intProdID=rsdata2("ProdID")
			stropt="opt"&CSTR(intl2)
			strDescript=rsdata2("Descript")
			strComm=rsdata2("Comm")
			strVal = CSTR(intProdID)&"|"&CSTR(intUserID)&"|"&strComm
                %>
                <tr>
                    <% If intCnt = 1 then%>
                    <td>
                        <input type="Radio" id="<%=stropt%>" name="<%=stropt%>" value="<%=strVal%>" checked>&nbsp;&nbsp;
		<% Else %>
                    <td>
                        <input type="Radio" id="<%=stropt%>" name="<%=stropt%>" value="<%=strVal%>">&nbsp;&nbsp;
		<% END IF %>
                        <label class="control" style="white-space: nowrap;"><%=strDescript%>-<%=stropt%>-<%=strVal%></label></td>
                    <td style="text-align: left; white-space: nowrap;" class="control"><%=FormatCurrency(strComm,2)%></td>
                </tr>
                <%
			rsData2.MoveNext
			intl2 = intl2+1
			Loop
		End If
		Set rsData2 = Nothing


			intCnt = intCnt+1
			rsData.MoveNext
			Loop
		End If
		Set rsData = Nothing
                %>
            </table>
            <table border="0" style="width: 415px; border-collapse: collapse;">
                <tr>
                    <td style="text-align: center;" colspan="3">
                        <button name="btnSave" class="button" style="width: 75px" onclick="SaveChg()">Save</button>&nbsp;&nbsp;&nbsp;&nbsp;
	                    <button name="btnDone" class="button" style="width: 75px" onclick="Done()">Done</button>&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <input type="hidden" name="FormAction" value="" />
            <input type="hidden" name="intl2" value="<%=intl2%>" />
        </div>
    </form>
</body>
</html>
<%

'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script language="VBSCRIPT">
Option Explicit

Sub Window_OnLoad()
	IF frmMain.hdnStatus.value=0 then
		window.close
	END IF

End Sub

Sub SaveChg()
	frmMain.FormAction.value="SaveChg"
	frmMain.submit()
End Sub
Sub done()
	window.close
End Sub

</script>

<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Sub SaveChg(dbMain)
	dim strSQL,rsData,intrecID,intAssigned,strComPer,strComm,strComAmt,intUserID,strtComm,LocationID,LoginID
	intrecID = request("intrecID")
	LocationID = request("LocationID")
	LoginID = request("LoginID")
	IF request("chkSplit") = "on" then
		strSQL = "Select count(*) From DetailComp(nolock) where RecID="&intrecID &" AND LocationID="& LocationID
		If DBOpenRecordset(dbMain,rsData,strSQL) Then
			IF NOT rsData.eof then
				intAssigned = rsData(0)
				IF intAssigned > 1 then
					strComPer = 100/intAssigned

					strSQL = "SELECT SUM(comm) as sumComm FROM RECITEM(nolock) WHERE recid =" & intrecID &" AND LocationID="& LocationID
					If DBOpenRecordset(dbMain,rsData,strSQL) Then
						IF not rsData.eof then
						strComm = rsData("sumComm")
						ELse
						strComm = 0.0
						END IF
					End If
					strComAmt = strComm * strComPer*.01
					strSQL= "	UPDATE DetailComp Set " & _
							"	Comm=" & strComm  &"," & _
							"	ComAmt=" &  FormatCurrency(round(strComAmt,1),2) &"," & _
							"	ComPer=" & strComPer & _
							"	WHERE recID=" & intrecID &" AND LocationID="& LocationID
					If NOT (dbExec(dbMain,strSQL)) Then
						Response.Write gstrMsg
					End If
				END IF
			END IF
		End If
		Set rsData = Nothing
	ELSE
		Dim stropt,cnt,strArr,intcnt
			strSQL = "Select count(*) From DetailComp(nolock) where RecID="&intrecID &" AND LocationID="& LocationID
			If DBOpenRecordset(dbMain,rsData,strSQL) Then
				IF NOT rsData.eof then
					intAssigned = rsData(0)
					IF intAssigned > 1 then
						strComPer = 100/intAssigned
						strSQL = "SELECT SUM(comm) as sumComm FROM RECITEM(nolock) WHERE recid =" & intrecID &" AND LocationID="& LocationID
						If DBOpenRecordset(dbMain,rsData,strSQL) Then
							IF not rsData.eof then
							strtComm = rsData("sumComm")
							ELse
							strtComm = 0.0
							END IF
						End If

						strSQL = "SELECT RECITEM.Comm as sumComm "&_
								" FROM RECITEM(NOLOCK) INNER JOIN Product(NOLOCK) ON RECITEM.ProdID = Product.ProdID"&_
								" WHERE (RECITEM.recId = "& intrecID &")  AND RECITEM.LocationID="& LocationID & " AND Product.Descript = 'Large Vehicle'"
						If DBOpenRecordset(dbMain,rsData,strSQL) Then
							IF not rsData.eof then
							strComm = rsData("sumComm")
							ELse
							strComm = 0.0
							END IF
						End If
						strComAmt = strComm * strComPer*.01
						strComPer = abs(((strtComm-strComm)/strtComm)-1)*100
						strSQL= "	UPDATE DetailComp Set " & _
								"	Comm=" & strComm  &"," & _
								"	ComAmt=" &  FormatCurrency(round(strComAmt,2),2) &"," & _
								"	ComPer=" & strComPer & _
								"	WHERE recID=" & intrecID &" AND LocationID="& LocationID
						If NOT (dbExec(dbMain,strSQL)) Then
							Response.Write gstrMsg
						End If
					END IF
				END IF
			End If
			Set rsData = Nothing
			intcnt = request("intl2")-1
			For Cnt = 1 to intcnt
				stropt="opt"&CSTR(cnt)
				strArr = Split(request(stropt),"|")
				intUserID = strArr(1)
				strComAmt = strArr(2)
					strSQL = "SELECT ComAmt as sumComm FROM DetailComp(nolock) WHERE recid =" & intrecID & " and UserID=" & intUserID &" AND LocationID="& LocationID
					If DBOpenRecordset(dbMain,rsData,strSQL) Then
						IF not rsData.eof then
						strComm = rsData("sumComm")+strComAmt
						ELse
						strComm = strComAmt
						END IF
					End If

					strComPer = abs(((strtComm-strComm)/strtComm)-1)*100

					strSQL= "	UPDATE DetailComp Set " & _
						"	Comm=" & strComm  &"," & _
						"	ComAmt=" &  FormatCurrency(round(strComm,2),2)  &"," & _
						"	ComPer=" & strComPer & _
						"	WHERE recID=" & intrecID &_
						"	and UserID=" & intUserID &" AND LocationID="& LocationID
					If NOT (dbExec(dbMain,strSQL)) Then
						Response.Write gstrMsg
					End If
			Next
	END IF
'Response.End
END SUB
%>
