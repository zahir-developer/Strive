<%@ LANGUAGE="VBSCRIPT" %>
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

Dim dbMain, intClientID, strfname, strlname, straddr1, straddr2, strcity, strst, strzip, intType,_
    strPhone, strPhone2, strPhoneType, strPhone2Type, strEmail, txtNotes,txtRecNote,intStatus,strStartDT
Dim hdnFilterBy,hdnVehArr,hdnvehid,blAccount,strCurrentAmt,intCustAccID, intCType,intC_Corp,LocationID,LoginID
dim strScore,blNoEmail

Set dbMain =  OpenConnection

intClientID=Request("hdnClientID")
hdnFilterBy = Request("hdnFilterBy")
hdnVehArr = Request("hdnVehArr")
hdnvehid = Request("hdnvehid")
    LocationID = request("LocationID")
    LoginID = request("LoginID")

Select Case Request("FormAction")
	Case "btnCreate"
		Call CreateData(dbMain,intClientID)
	Case "btnSave"
		Call UpdateData(dbMain,intClientID)
	Case "SaveVeh"
		Call VehData(dbMain,hdnVehArr,intClientID)
	Case "DeleteVeh"
		Call DeleteVeh(dbMain,hdnvehid,intClientID)
	Case "btnDelete"
		Call DeleteData(dbMain,intClientID)
		Response.Redirect "ClientList.asp"
End Select


If intClientID="" Then 
	intClientID=0
ELSE
	Call GetClientInfo(dbMain,intClientID, strfname, strlname, straddr1, straddr2, strcity, strst, strzip, _
    strPhone, strPhone2, strPhoneType, strPhone2Type, strEmail, txtNotes,txtRecNote,intStatus,blAccount,_
    strCurrentAmt,intType,intCustAccID, intCType,strStartDT,intC_Corp,strScore,blNoEmail)
End If
IF len(trim(strCurrentAmt))=0 then
	strCurrentAmt = 0.0
end if

'********************************************************************
'HTML
'********************************************************************
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css">
<title></title>
</head>
<body class="pgbody" Onclick="SetDirty" onkeyup="SetDirty()">
<form method="POST" name="frmMain" action="ClientEdit.asp"> 
<input type="hidden" name="FormAction" tabindex="-2" />
<input type="hidden" name="hdnClientID" tabindex="-2" value="<%=intClientID%>">
<input type="hidden" name="intCustAccID" tabindex="-2" value="<%=intCustAccID%>">
<input type="hidden" name="intStatus" tabindex="-2" value="<%=intStatus%>">
<input type="hidden" name="hdnFilterBy" tabindex="-2" value="<%=hdnFilterBy%>">
<input type="hidden" name="hdnVehArr" tabindex="-2" value="<%=hdnVehArr%>">
<input type="hidden" name="blAccount" tabindex="-2" value="<%=blAccount%>">
<input type="hidden" name="strCurrentAmt" tabindex="-2" value="<%=strCurrentAmt%>">
<input type="hidden" name="intCType" tabindex="-2" value="<%=intCType%>">
<input type="hidden" name="intC_Corp" tabindex="-2" value="<%=intC_Corp%>">
<input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />

<div style="text-align: center">
<table width="768px">
	<tr colspan=5>
		<td align="right" class="control" >Client Number:</td>
		<% IF intClientID = 0 THEN %>
        <td align="left" class="control" ><b>NEW</b></td>
		<% Else %>
        <td align="left" class="control" ><b><%=intClientID%></b></td>
        <% END if %>
		<td align="right" class="control" >&nbsp;</td>
	</tr>
	<tr>
		<td align="right" class="control" >First Name:</td>
        <td align="left" class="control" ><input maxlength="20" size="20" type=text tabindex=1 DirtyCheck=TRUE name="strfname" value="<%=strfname%>">&nbsp; </td>
		<td align="right" class="control" >Phone 1:</td>
        <td align="left" class="control" ><input maxlength="14" size="14" type=text tabindex=8 DirtyCheck=TRUE name="strPhone" value="<%=strPhone%>">&nbsp; 
		<select tabindex="9" name="strPhoneType" DirtyCheck="TRUE" size="1">
				<% IF strPhoneType = "Work" then %>
				<option value="Cell">Cell</option>
				<option value="Home">Home</option>
				<option selected value="Work">Work</option>
				<% Else %>
					<% IF strPhoneType = "Home" then %>
					<option value="Cell">Cell</option>
					<option selected value="Home">Home</option>
					<option value="Work">Work</option>
					<% Else %>
					<option selected value="Cell">Cell</option>
					<option value="Home">Home</option>
					<option value="Work">Work</option>
					<% End If%>
				<% End If%>
			</select></td>
		<td align="right" class="control" >&nbsp;</td>
	</tr>
	<tr>
 		<td align="right" class="control" >Last Name:</td>
       <td align="left" class="control" ><input maxlength="20" size="20" type=text tabindex=2 DirtyCheck=TRUE name="strlname" value="<%=strlname%>">&nbsp; </td>
		<td align="right" class="control" >Phone 2:</td>
        <td align="left" class="control" ><input maxlength="14" size="14" type=text tabindex=10 DirtyCheck=TRUE name="strPhone2" value="<%=strPhone2%>">&nbsp; 
		<select tabindex="11" name="strPhone2Type" DirtyCheck="TRUE" size="1">
				<% IF strPhone2Type = "Cell" then %>
				<option selected value="Cell">Cell</option>
				<option value="Home">Home</option>
				<option  value="Work">Work</option>
				<% Else %>
					<% IF strPhone2Type = "Home" then %>
					<option value="Cell">Cell</option>
					<option selected value="Home">Home</option>
					<option value="Work">Work</option>
					<% Else %>
					<option  value="Cell">Cell</option>
					<option value="Home">Home</option>
					<option selected value="Work">Work</option>
					<% End If%>
				<% End If%>
			</select></td>
		<td align="right" class="control" >&nbsp;</td>
	</tr>
	<tr>
        <td align="right"  ><label class="control">Address:</label></td>
        <td><input tabindex="3" type="text" name="straddr1" size="40" DataType="text" DirtyCheck="TRUE" value="<%=straddr1%>"></td>
 			<td align="right" class="control" >Credit Account:</td>
		<td align="left">
			<INPUT name=chkAccount type=checkbox  DirtyCheck="TRUE"
				<%If blAccount Then%>
					checked
				<%End If%>>
		</td>
		<td align="right" class="control" >&nbsp;</td>

 	
 	</tr>

	

<% IF blAccount then %>

	<tr>
      <td align="right"  ><label class="control"> </label></td>
        <td><input tabindex="4" type="text" name="straddr2" size="40" DataType="text" DirtyCheck="TRUE" value="<%=straddr2%>"></td>
  		<td Valign="top" align="right" class="control" >Type:</td>
		<td align="left" class="control"  colspan=2> 
		<Select name="cboCType" tabindex=12 onChange="CheckC_CORP()" DirtyCheck=TRUE>
			<%Call LoadList(dbMain,8,intCtype)%>		
		</select>
<div align="left" id="C_Corp" style="visibility:hidden">
		<Select name="cboC_Corp" tabindex=13 DirtyCheck=TRUE>
			<option Value="0">Main Account</option>
			<%Call LoadCorpAcc(dbMain,intC_Corp)%>		
		</select>
</div>
    <div align="left" id="Monthly" style="visibility:hidden">
 		<label class="control">Start Date:</label>&nbsp;
        <input maxlength="20" size="20" type=text tabindex=50 DirtyCheck=TRUE name="strStartDT" value="<%=strStartDT%>"> 
</div>
		</td>
    </tr>
	<% END IF%>
	<tr>
		<td align="right"  ><label class="control">City:</label></td>
        <td><input tabindex="6" type="text" name="strCity" size="20" DataType="text" DirtyCheck="TRUE" value="<%=strCity%>">&nbsp;<label class="control">St:</label>
		<Select name="cboSt" tabindex=7 DirtyCheck=TRUE>
			<%Call LoadList(dbMain,-1,strSt)%>		
		</select>
        <input tabindex="5" type="text" name="strZip" onkeyup="CheckZip()" size="5" DataType="text" DirtyCheck="TRUE" value="<%=strZip%>"></td>
		<td align="right" class="control" >Status:</td>
		<td align="left" class="control" > 
		<Select name="cboStatus" tabindex=14 DirtyCheck=TRUE>
			<%Call LoadList(dbMain,9,intStatus)%>		
		</select>
		</td>
		<td align="right" class="control" >&nbsp;</td>
	</tr>
</table>
<table width="768px">
	<tr>
		<td align="right"><label class="control">Email:</label></td>
        <td><input tabindex="15" type="text" name="strEmail" size="80" DataType="text" DirtyCheck="TRUE" value="<%=strEmail%>">&nbsp&nbsp
 			<label class="control">No Email:</label>&nbsp&nbsp
			<INPUT name=chkNoEmail type=checkbox  DirtyCheck="TRUE"
				<%If blNoEmail Then%>
					checked
				<%End If%>>
		<label class="control">Score:</label>&nbsp&nbsp
		<Select name="cboScore" tabindex=14 DirtyCheck=TRUE>
			<%Call LoadList(dbMain,17,strScore)%>		
		</select>
		</td>
	</tr>
	<tr>
		<td align="right"><label class="control">Notes:</label></td>
		<td align="left" class="control" >
			<Textarea tabindex="16" COLS="91" ROWS="3" Title="Notes ." name="txtNotes" DirtyCheck=TRUE><%=txtNotes%>
			</Textarea>
		</td>
	</tr>
	<tr>
		<td style=" text-align:right"><label class="control">Check Out Note:</label></td>
		<td align="left" class="control" >
			<Textarea tabindex="16" COLS="91" ROWS="3" Title="Check Out Notes ." name="txtRecNote" DirtyCheck=TRUE><%=txtRecNote%>
			</Textarea>
		</td>
	</tr>
</table>
<table class="tblcaption" cellspacing=0 cellpadding=0 width=768>
	<tr>
		<td align=center class="tdcaption" background="images/header.jpg" width=210>Vehical List</td>
		<td style=" text-align:right">			
		<% IF intClientID = 0 THEN %>
			<button  name="btnAddVeh" class="buttondead"  align="right" style="width:75" OnClick="AddVeh()">New</button>
		<% Else %>
			<button  name="btnAddVeh" class="button"  onmouseover="ButtonHigh()" onmouseout="ButtonLow()"  style="width:75px;text-align:center" OnClick="AddVeh()">New</button>
        <% END if %>



		</td>
	</tr>
</table>
<iframe Name="fraVeh"  style="height:150px; width:768px" frameborder="0"></iframe>
<% IF blAccount and (intCtype=3 or (intCtype=1 and Len(intC_Corp)>0)) then %>
	<br>
	<table class="tblcaption" cellspacing=0 cellpadding=0 width=768>
		<tr>
			<td class="tdcaption"  style=" text-align:center; background-image:url(images/header.jpg);width:210px">Account Info (last 20)</td>
			<td class="control" style="font:bold 18px arial;text-align:right"  >Balance:</td>
			<td class="control" ><label style="font:bold 18px arial;text-align:left" class="blkdata" ID="lblCurrentAmt" ></label></td>
			<td style=" text-align:right">			
				<button  name="btnAddAcc" style=" text-align:center" type="submit" OnClick="SubmitForm()">Add Activity</button>
			</td>
		</tr>
	</table>
	<iframe Name="fraAcc" src="admLoading.asp"  style="height:78px;width:768px; border-collapse:collapse" frameborder="0"></iframe>
<% ELSE %>
	<input type="hidden" name="intType" tabindex="-2" value="<%=intType%>">
<% END IF %>

<table border="0" width="768" cellspacing="0" cellpadding="0">
	<tr>
		<td width="100%"><div align="right">
			<button name="btnMonthly" tabindex="50" onmouseover="ButtonHigh()" onmouseout="ButtonLow()"  OnClick="SubmitForm()" >&nbsp;Statement&nbsp;</button>&nbsp;&nbsp;
			<button name="btnHistory" tabindex="51" onmouseover="ButtonHigh()" onmouseout="ButtonLow()"  OnClick="SubmitForm()" >&nbsp;History&nbsp;</button>&nbsp;&nbsp;
			<button name="btnDelete" tabindex="52" onmouseover="ButtonHigh()" onmouseout="ButtonLow()"  OnClick="SubmitForm()" >&nbsp;Delete&nbsp;</button>&nbsp;&nbsp;
			<button name="btnSave" tabindex="53" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" DirtyCheck=TRUE  OnClick="SubmitForm()" >&nbsp;Save&nbsp;</button>&nbsp;&nbsp;
			<button name="btnDone" width="90"  tabindex="54" onmouseover="ButtonHigh()" onmouseout="ButtonLow()"  OnClick="SubmitForm()" >&nbsp;Done&nbsp;</button>
		</div></td>
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

Sub Window_Onload()

	


		fraVeh.location.href = "ClientVehListFra.asp?hdnClientID=" & frmMain.hdnClientID.Value &"&LocationID="& document.all("LocationID").value&"&LoginID="& document.all("LoginID").value
	IF frmMain.blAccount.value  and  (document.all("intCType").value=3 OR (document.all("intCType").value=1 and Len(document.all("intC_Corp").value) > 0))  then
		document.all("lblCurrentAmt").innerText = formatcurrency(frmMain.strCurrentAmt.value,2)
		fraAcc.location.href = "CustAccHistFra.asp?intCustAccID=" & document.all("intCustAccID").Value &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	END IF

	if(document.getElementById("blAccount").value) then
		IF  document.all("intCType").value = 1 then
			document.getElementById("C_Corp").style.visibility="visible"
		ELSE
			document.getElementById("C_Corp").style.visibility="hidden"
		END IF

		IF  document.all("intCType").value = 2 then
			document.getElementById("Monthly").style.visibility="visible"
		ELSE
			document.getElementById("Monthly").style.visibility="hidden"
		END IF

	End If
End Sub



Sub Window_OnBeforeUnLoad
	Call dirtycheck
End Sub

Sub CheckC_CORP()





	IF  frmMain.cboCType.value = 1 then
		window.C_Corp.style.visibility="visible"
	ELSE
		window.C_Corp.style.visibility="hidden"
	END IF
	IF  frmMain.cboCType.value = 2 then
		window.Monthly.style.visibility="visible"
		IF isnull(frmMain.strStartDT.value) or  frmMain.strStartDT.value < "1/1/2000" then
		    frmMain.strStartDT.value = now()
		END IF
		
		
	ELSE
		window.Monthly.style.visibility="hidden"
	END IF


END Sub

Sub SubmitForm()
	Dim Answer
	window.event.CancelBubble=True
	window.event.ReturnValue=False

	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	Select Case window.event.srcElement.name
		Case "btnDone"
			location.href="ClientList.asp?hdnFilterBy=" & frmMain.hdnFilterBy.Value &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
		Case "btnSave"
			IF Validate Then
				Call ResetDirty
				frmMain.FormAction.value="btnSave"
				frmMain.Submit()
			END IF
		Case "btnDelete"
			Answer = MsgBox("Are you sure you want to Delete this Client " &"?"& chr(13) & chr(13) & " WARNING: A Client cannot be restored!",276,"Confirm Cancel")
			If Answer = 6 then
				Call ResetDirty
				frmMain.FormAction.value="btnDelete"
				frmMain.Submit()
			Else
				Exit Sub
			End if
 		Case "btnAddAcc"
			Dim strAct
				strAct= ShowModalDialog("CustAccActDlg.asp?intCustAccID=" & document.all("intCustAccID").Value & "&strCurrentAmt=" & document.all("strCurrentAmt").value& "&intClientID=" & document.all("hdnClientID").value&"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:350px;dialogheight:370px;")
				frmMain.Submit()
 		Case "btnHistory"
			Dim strhist
				strhist = ShowModalDialog("ClientHistoryDlg.asp?hdnClientID=" & document.all("hdnClientID").value&"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:650px;dialogheight:370px;")
				'frmMain.Submit()
 		Case "btnMonthly"
			Dim strMon
				strMon = ShowModalDialog("ClientMonthlyDlg.asp?intCustAccID=" & document.all("intCustAccID").value&"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:650px;dialogheight:370px;")
				'frmMain.Submit()
	End Select
End Sub

Sub AddVeh()
	Dim strFilt,strVehArr
	IF frmMain.hdnClientID.Value = 0 then
		msgbox "You must save the Client before adding vehicals!"
		Exit Sub
	ELSE
		strVehArr = ShowModalDialog("ClientVehEditDlg.asp?hdnClientID=" & frmMain.hdnClientID.Value & "&hdnvehid=0"&"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,,"center:1;dialogleft:210px;dialogtop:150px; dialogwidth:450px;dialogheight:350px;")
		If Len(strVehArr) = 0 Then 
			window.event.ReturnValue = False
		Else
			Call ResetDirty
			frmMain.hdnVehArr.value = strVehArr

			frmMain.FormAction.value="SaveVeh"
			frmMain.Submit()
		End If
	END IF
End Sub

Sub CheckZip()
	Dim strZipArr,strCitySTArr
	IF Len(trim(frmMain.strZip.value)) = 5 then
		strZipArr = ShowModalDialog("ClientZipDlg.asp?hdnZip=" & trim(frmMain.strZip.Value),,"center:1;dialogleft:200px;dialogtop:200px; dialogwidth:200px;dialogheight:200px;")
		IF LEN(TRIM(strZipArr))>3 then
			strCitySTArr = Split(strZipArr,"|")
			frmMain.strZip.value = trim(frmMain.strZip.Value)
			frmMain.strCity.value = strCitySTArr(0)
			frmMain.strst.value = strCitySTArr(1)
			frmMain.cboSt.value = strCitySTArr(1)
			frmMain.strPhone.focus
		END IF
	END IF
END Sub

Function Validate
	Dim valMSG
	valMSG = " "
	Validate = True
End Function

</script>
<%
	Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Sub GetClientInfo(dbMain,intClientID, strfname, strlname, straddr1, straddr2, strcity, strst, strzip, _
    strPhone, strPhone2, strPhoneType, strPhone2Type, strEmail, txtNotes,txtRecNote,intStatus,blAccount,_
    strCurrentAmt,intType,intCustAccID, intCType,strStartDT,intC_Corp,strScore,blNoEmail)
	Dim strSQL, RS 
	strSQL="SELECT client.*,CustAcc.CurrentAmt,CustAcc.Type,CustAcc.CustAccID FROM client(nolock)"&_
	" LEFT OUTER JOIN CustAcc (nolock) ON client.ClientID = CustAcc.ClientID"&_
	" WHERE client.ClientID = " & intClientID 
	If DBOpenRecordset(dbMain,rs,strSQL) Then
		If Not RS.EOF Then
	
			strfname = RS("fname")
			strlname = RS("lname")
			straddr1 = RS("addr1")
			straddr2 = RS("addr2")
			strcity = RS("city")
			strst = RS("st")
			strzip = RS("zip")
			intCtype = RS("Ctype")
			strStartDT = RS("StartDT")
			intC_Corp = RS("C_Corp")
			blAccount = RS("Account")
			strPhone = RS("Phone")
			strPhone2 = RS("Phone2")
			strPhoneType = RS("PhoneType")
			strPhone2Type = RS("Phone2Type")
			strEmail = RS("Email")
			txtNotes = RS("Notes")
			txtRecNote = RS("RecNote")
			intStatus = RS("Status")
			strScore = RS("Score")
			blNoEmail = RS("NoEmail")
		End If
	End If

	IF isnull(strst) or len(trim(strst))=0 then
		strst = "GA"
	end if
'	IF isnull(intType) or len(intType)=0 then
'		intType = 0
'	end if
	'IF isnull(intC_Corp) or len(intC_Corp)=0 then
	'	intC_Corp = 0
	'end if
	IF isnull(intStatus) or len(intStatus)=0 then
		intStatus = 0
	end if
	IF isnull(blNoEmail) or len(blNoEmail)=0 then
		blNoEmail = 0
	end if
	IF isnull(strScore) or len(strScore)=0 then
		strScore = "N"
	end if
    if intCtype = 2 or intCtype=3 then
	    strSQL="SELECT isnull(CustAcc.CurrentAmt,0.0) as CurrentAmt,CustAcc.Type,CustAcc.CustAccID FROM CustAcc (nolock) WHERE CustAcc.ClientID = " & intClientID 
	    If DBOpenRecordset(dbMain,rs,strSQL) Then
		    If Not RS.EOF Then
			    strCurrentAmt = RS("CurrentAmt")
			    intType = RS("Type")
			    intCustAccID = RS("CustAccID")
		    End If
	    End If
    end if
    if intCtype = 1  then
	    strSQL="SELECT isnull(CustAcc.CurrentAmt,0.0) as CurrentAmt,CustAcc.Type,CustAcc.CustAccID FROM CustAcc (nolock) WHERE CustAcc.ClientID = " & intC_Corp 
	    If DBOpenRecordset(dbMain,rs,strSQL) Then
		    If Not RS.EOF Then
			    strCurrentAmt = RS("CurrentAmt")
			    intType = 1
			    intCustAccID = RS("CustAccID")
		    End If
	    End If
    end if


End Sub

Sub VehData(dbMain,hdnVehArr,intClientID)
	Dim strSQL,rsData,arrFilterBy,strSQL2,intvehid
	arrFilterBy = Split(hdnVehArr,"|")

'    response.write hdnVehArr &"<br/>"

    if len(trim(arrFilterBy(3)))>0 then
        '** has UPC
        strSQL = " Select clientid,vehid from vehical (NOLOCK) where (ltrim(rtrim(upc))='"& rtrim(ltrim(arrFilterBy(3)))&"')"
	    If dbOpenRecordSet(dbmain,rsData,strSQL) Then
		    IF not rsData.eof then
                intvehid = rsData("vehid")
			    strSQL2= "UPDATE vehical  set "&_
					"ClientID =" & intClientID 
                if len(trim(ucase(arrFilterBy(4))))> 0 then
					strSQL2=strSQL2&",TAG ='" & ucase(arrFilterBy(4)) &"'"
                end if
                if len(trim(arrFilterBy(5)))> 0 then
					strSQL2=strSQL2&",Make =" & arrFilterBy(5) 
                end if
                if len(trim(arrFilterBy(6)))> 0 then
					strSQL2=strSQL2&",Vmodel ='" & arrFilterBy(6) &"'"
                end if
                if len(trim(arrFilterBy(7) ))> 0 then
					strSQL2=strSQL2&",model =" & arrFilterBy(7) 
                end if
                if len(trim(arrFilterBy(8)))> 0 then
					strSQL2=strSQL2&",color =" & arrFilterBy(8) 
                end if
                if len(trim(arrFilterBy(9)))> 0 then
					strSQL2=strSQL2&",MonthlyCharge =" & arrFilterBy(9)
                end if
                strSQL2=strSQL2& " Where (upc ='"&arrFilterBy(3)&"')"
'        response.write strSQL2 &"<br/>"
			    IF NOT DBExec(dbMain, strSQL2) then
			        Response.Write gstrMsg
			        Response.End
			    END IF
            else
                ' ** No UPC in DB
                intvehid = arrFilterBy(1)
                strSQL = " Select * from vehical (NOLOCK) where vehid="&arrFilterBy(1)
	            If dbOpenRecordSet(dbmain,rsData,strSQL) Then
		            IF rsData.eof then
			                strSQL2= "Insert into vehical (LocationID,ClientID,vehid,Vehnum,UPC,TAG,Make,Vmodel,model,color,MonthlyCharge) Values ("&_
				                request("LocationID") &","&_
				                intClientID &","&_
				                arrFilterBy(1) &","&_
				                arrFilterBy(2) &","&_
				                "'" & arrFilterBy(3) &"',"&_
				                "'" & arrFilterBy(4) &"',"&_
				                arrFilterBy(5) &","&_
				                "'" & ucase(arrFilterBy(6)) &"',"&_
				                arrFilterBy(7) &","&_
				                arrFilterBy(8) &","&_
				                arrFilterBy(9) &")"
'        response.write strSQL2 &"<br/>"
			                IF NOT DBExec(dbMain, strSQL2) then
				                Response.Write gstrMsg
				                Response.End
			                END IF

		            ELSE
			            strSQL2= "UPDATE vehical  set "&_
					            "ClientID =" & intClientID &","&_
					            "UPC ='" & arrFilterBy(3) &"',"&_
					            "TAG ='" & ucase(arrFilterBy(4)) &"',"&_
					            "Make =" & arrFilterBy(5) &","&_
					            "Vmodel ='" & arrFilterBy(6) &"',"&_
					            "model =" & arrFilterBy(7) &","&_
					            "color =" & arrFilterBy(8) &","&_
					            "MonthlyCharge =" & arrFilterBy(9) &_
					            " Where vehid =" & arrFilterBy(1) 
'    response.write strSQL2 &"<br/>"
			            IF NOT DBExec(dbMain, strSQL2) then
				            Response.Write gstrMsg
				            Response.End
			            END IF
		            END IF
	            End if
            end if
        end if
		strSQL2= "UPDATE REC set ClientID =" & intClientID &" Where (vehid ="&intvehid&")"
		IF NOT DBExec(dbMain, strSQL2) then
			Response.Write gstrMsg
			Response.End
		END IF
    
    end if

'    response.End

End Sub


Sub UpdateData(dbMain,intClientID)
	Dim strSQL,rsData,MaxSQL,blAccount,intCustACCID,rsData2,intCustAccTID
	Dim intC_Corp,intType,intCtype,blNoEmail

	IF   request("chkAccount") = "on" then
		blAccount = 1
	ELSE
		blAccount = 0
	END IF
	IF   request("chkNoEmail") = "on" then
		blNoEmail = 1
	ELSE
		blNoEmail = 0
	END IF
	 intC_Corp = Request("cboC_Corp")
	 IF len(trim(intC_Corp))=0 then
		intC_Corp = 0
	 END IF
	 intCtype = Request("cboCtype")
	 IF len(trim(intCtype))=0 then
		intCtype = 0
	 END IF


	IF intClientID=0 then
		MaxSQL = " Select ISNULL(Max(ClientID),0)+1 from Client (NOLOCK) "
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			intClientID=rsData(0)
		End if
		Set rsData = Nothing
		strSQL= "Insert into Client (LocationID,ClientID,fname,lname,addr1,addr2,city,st,zip,Ctype,StartDT,C_Corp,Account,"&_
			"Phone,Phone2,PhoneType,Phone2Type,Email,Score,NoEmail,Notes,recNote,Status) Values ("&_
			request("LocationID") &","&_
			intClientID &","&_
			"'" & SQLReplace(Request("strfname")) &"',"&_
			"'" & SQLReplace(Request("strlname")) &"',"&_
			"'" & SQLReplace(Request("straddr1")) &"',"&_
			"'" & SQLReplace(Request("straddr2")) &"',"&_
			"'" & SQLReplace(Request("strcity")) &"',"&_
			"'" & Request("cbost") &"',"&_
			"'" & SQLReplace(Request("strzip")) &"',"&_
			intCtype &","&_
			"'" & Request("strStartDT") &"',"&_
			intC_Corp &","&_
			blAccount &","&_
			"'" & SQLReplace(Request("strPhone")) &"',"&_
			"'" & SQLReplace(Request("strPhone2")) &"',"&_
			"'" & SQLReplace(Request("strPhoneType")) &"',"&_
			"'" & SQLReplace(Request("strPhone2Type")) &"',"&_
			"'" & Request("strEmail") &"',"&_
			"'" & Request("cboScore") &"',"&_
			blNoEmail &","&_
			"'" & Request("txtNotes") &"',"&_
			"'" & Request("txtRecNote") &"',"&_
			Request("cboStatus") & ")"
		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
		MaxSQL = " Select ISNULL(Max(CustAccID),0)+1 from CustAcc (NOLOCK) "
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			intCustAccID=rsData(0)
		End if
		Set rsData = Nothing


		strSQL= "Insert into CustAcc (CustAccID, LocationID, ClientID,  ActiveDte, LastUpdate, LastUpdateBy, Type, Status, MonthlyCharge) Values ("&_
			intCustAccID &","&_
			request("LocationID") &","&_
			intClientID &","&_
			"'" & Request("strStartDT") &"',"&_
			"'" & Request("strStartDT") &"',"&_
			"'" & request("LoginID") &"',"&_
			intCtype &","&_
			"1,0.0)"
		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF



	ELSE
		strSQL= "UPDATE Client  set "&_
				"fname ='" & SQLReplace(Request("strfname")) &"',"&_
				"lname ='" & SQLReplace(Request("strlname")) &"',"&_
				"addr1 ='" & SQLReplace(Request("straddr1")) &"',"&_
				"addr2 ='" & SQLReplace(Request("straddr2")) &"',"&_
				"city ='" & SQLReplace(Request("strcity")) &"',"&_
				"st ='" & Request("Cbost") &"',"&_
				"zip ='" & SQLReplace(Request("strzip")) &"',"&_
				"Ctype =" & intCtype &","&_
				"StartDT ='" & Request("strStartDT") &"',"&_
				"C_Corp =" & intC_Corp &","&_
				"Account =" & blAccount &","&_
				"Phone ='" & SQLReplace(Request("strPhone")) &"',"&_
				"Phone2 ='" & SQLReplace(Request("strPhone2")) &"',"&_
				"PhoneType ='" & SQLReplace(Request("strPhoneType")) &"',"&_
				"Phone2Type ='" & SQLReplace(Request("strPhone2Type")) &"',"&_
				"Email ='" & Request("strEmail") &"',"&_
				"NoEmail =" & blNoEmail  &","&_
				"Score ='" & Request("cboScore") &"',"&_
				"Notes ='" & Request("txtNotes") &"',"&_
				"RecNote ='" & Request("txtRecNote") &"',"&_
				"Status =" & Request("cboStatus") &_
				" Where ClientID =" & intClientID 

		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
	END IF
	intType = Request("CboType")
	IF len(trim(intType))=0 then
		intType = 1
	END IF
	MaxSQL = " Select CustAccID from CustAcc (NOLOCK) where ClientID=" & intClientID  	
    If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
		IF NOT rsData.EOF then
			intCustACCID=rsData(0)
			IF blAccount = 1 then
				strSQL= "UPDATE CustAcc  set "&_
						"Type =" & intType  &_
						" Where CustACCID =" &  intCustACCID  &" and LocationID = "& request("LocationID")
				IF NOT DBExec(dbMain, strSQL) then
					Response.Write gstrMsg
					Response.End
				END IF
			ELSE
				strSQL= "UPDATE CustAcc  set "&_
						"Status = 0 Where CustACCID =" &  intCustACCID &" and LocationID = "& request("LocationID")
				IF NOT DBExec(dbMain, strSQL) then
					Response.Write gstrMsg
					Response.End
				END IF
			END IF
		ELSE
			IF blAccount = 1 then
				MaxSQL = " Select ISNULL(Max(CustAccID),0)+1 from CustAcc (NOLOCK)"
				If dbOpenRecordSet(dbmain,rsData2,MaxSQL) Then
					intCustACCID=rsData2(0)
				End if
				strSQL= "Insert into CustAcc (LocationID,ClientID,CustACCID,CurrentAmt,ActiveDte,LastUpdate,LastUpdateBy,Status,Type) Values ("&_
					 request("LocationID") &","&_
					intClientID &","&_
					intCustACCID &","&_
					0.0 &","&_
					"'" & date() &"',"&_
					"'" & date() &"',"&_
					 request("LoginID")  &","&_
					"1,"&_
					"1)"
				IF NOT DBExec(dbMain, strSQL) then
					Response.Write gstrMsg
					Response.End
				END IF
				Set rsData2 = Nothing
				MaxSQL = " Select ISNULL(Max(CustAccTID),0)+1 from CustAccHist (NOLOCK)"
				If dbOpenRecordSet(dbmain,rsData2,MaxSQL) Then
					intCustAccTID=rsData2(0)
				End if
				strSQL= "Insert into CustAccHist (txCustid,txLocationID,CustACCID,CustAccTID,TXDte,TXUser,TXAmt,TXType,Archive) Values ("&_
				intClientID &","&_
				 request("LocationID") &","&_
				intCustACCID &","&_
				intCustAccTID &","&_
				"'" & date() &"',"&_
				 request("LoginID") &","&_
				"0.0,"&_
				"'Created',0)"
				IF NOT DBExec(dbMain, strSQL) then
					Response.Write gstrMsg
					Response.End
				END IF
				Set rsData2 = Nothing
			END IF
		END IF
	End if
	Set rsData = Nothing
	Response.Redirect "ClientEdit.asp?hdnClientID=" & intClientID &"&LocationID="& request("LocationID") &"&LoginID="&request("LoginID") 
End Sub

Sub DeleteData(dbMain, intClientID)
	Dim strSQL,rs
	strSQL=" DELETE Vehical WHERE ClientID=" & intClientID 
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
	strSQL=" DELETE Client WHERE ClientID=" & intClientID
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
	Response.Redirect "ClientList.asp?LocationID="& request("LocationID") &"&LoginID="&request("LoginID") 
End Sub

Sub DeleteVeh(dbMain, intvehid,intClientID)
	Dim strSQL,rs
	strSQL=" DELETE Vehical WHERE vehid=" & intvehid
	IF NOT DBExec(dbMain, strSQL) then
		Response.Write gstrMsg
		Response.End
	END IF
	Response.Redirect "ClientEdit.asp?hdnClientID=" & intClientID &"&LocationID="& request("LocationID") &"&LoginID="&request("LoginID") 
End Sub
Function LoadCorpAcc(db,var)
	Dim strSQL,RS,strSel,temp,intType,blnDeleted,rsData2,strSQL2
	blnDeleted =False
	strSQL="SELECT ClientID,Fname"&_
	" FROM Client (NOLOCK)"&_
	" WHERE account = 1 AND Ctype = 1 and C_Corp=0 "
	If dbOpenRecordSet(db,rs,strSQL) Then
		Do While Not RS.EOF
					If Trim(var) = Trim(RS("ClientID")) Then
						blnDeleted = true
						strSel = "selected" 
						%>
						<option Value="<%=RS("ClientID")%>" <%=strSel%>>&nbsp;<%=RS("Fname")%></option>
						<%
					Else
						strSel = ""
						%>
						<option Value="<%=RS("ClientID")%>" <%=strSel%>>&nbsp;<%=RS("Fname")%></option>
						<%
					End IF
			RS.MoveNext
		Loop
	End If
End Function

%>
