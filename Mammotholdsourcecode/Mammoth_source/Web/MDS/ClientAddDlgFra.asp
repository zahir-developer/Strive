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

Dim dbMain, strUPC,arrData,intClientID, strfname, strlname, straddr1, straddr2, strcity, strst, strzip,_
    strPhone, strPhone2, strPhoneType, strPhone2Type, strEmail,hdnArr,blNotFound
dim strSQL,rs,intvehid,intmake,intmodel,intColor,strVmodel,strTag,hdnClientID,LocationID,LoginID
Set dbMain =  OpenConnection

strUPC=Request("strUPC")
hdnClientID=Request("hdnClientID")
LocationID = request("LocationID")
LoginID = request("LoginID")


blNotFound = 0

IF LEN(Trim(hdnClientID)) >0 then
 hdnArr = split(hdnClientID,"|")
 intClientID = hdnArr(0)
 strUPC = hdnArr(1)
END IF
Select Case Request("FormAction")
	Case "btnSave"
		Call UpdateData(dbMain)
End Select


strSQL = "SELECT * FROM vehical (nolock) WHERE UPC='"& strUPC &"'"
If DBOpenRecordset(dbMain,rs,strSQL) Then
	If Not RS.EOF Then
		If intClientID="" OR isnull(intClientID) Then 
			intClientID  = RS("ClientID")
		END IF
		intvehid = RS("vehid")
		intmake = RS("make")
		intmodel = RS("model")
		intColor = RS("Color")
		strVmodel = RS("Vmodel")
		strTag = RS("Tag")
	ELSE
		intmake = 0
		intColor = 0
		blNotFound = 1
	END IF
END IF


If intClientID="" OR intClientID<3 Then 
	intClientID=0
ELSE
	strSQL="SELECT * FROM Client(Nolock) WHERE ClientID=" & intClientID 
	If DBOpenRecordset(dbMain,rs,strSQL) Then
		If Not RS.EOF Then
			strfname = RS("fname")
			strlname = RS("lname")
			straddr1 = RS("addr1")
			straddr2 = RS("addr2")
			strcity = RS("city")
			strst = RS("st")
			strzip = RS("zip")
			strPhone = RS("Phone")
			strPhone2 = RS("Phone2")
			strPhoneType = RS("PhoneType")
			strPhone2Type = RS("Phone2Type")
			strEmail = RS("Email")
		End If
	End If
	strSQL = "SELECT * FROM vehical (nolock) WHERE UPC='"& strUPC &"'"
	If DBOpenRecordset(dbMain,rs,strSQL) Then
		If Not RS.EOF Then
			intvehid = RS("vehid")
			intmake = RS("make")
			intmodel = RS("model")
			intColor = RS("Color")
			strVmodel = RS("Vmodel")
			strTag = RS("Tag")
		ELSE
			intmake = 0
			intColor = 0
		
		END IF
END IF


End If

%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css">
    <title></title>
</head>
<body class="pgbody">
    <form method="POST" name="frmMain" action="ClientAddDlgFra.asp">
        <div style="text-align: center">
            <input type="hidden" name="FormAction" tabindex="-2" value>
            <input type="hidden" name="blNotFound" tabindex="-2" value="<%=blNotFound%>">
            <input type="hidden" name="intClientID" tabindex="-2" value="<%=intClientID%>">
            <input type="hidden" name="hdnClientID" tabindex="-2" value="<%=hdnClientID%>">
            <input type="hidden" name="intvehid" tabindex="-2" value="<%=intvehid%>">
            <input type="hidden" name="strst" tabindex="-2" value="<%=strst%>">
            <input type="hidden" name="strCity" tabindex="-2" value="<%=strCity%>">
            <input type="hidden" name="strUPC" tabindex="-2" value="<%=strUPC%>">
            <input type="hidden" name="LocationID" value="<%=LocationID%>" />
            <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
            <table align="center" border="0" width="500" cellspacing="0" cellpadding="0">
                <tr>
                    <td align="right" class="control" nowrap>First Name:</td>
                    <td align="left" class="control" nowrap>
                        <input maxlength="20" size="20" type="text" tabindex="1" dirtycheck="TRUE" name="strfname" value="<%=strfname%>">&nbsp; </td>
                    <td align="left" class="control" nowrap>
                        <button align="Center" class="button" onclick="SelClient()" style="width: 120" id="button1" name="button1">Select</button></td>
                </tr>
                <tr>
                    <td align="right" class="control" nowrap>Last Name:</td>
                    <td align="left" class="control" nowrap>
                        <input maxlength="20" size="20" type="text" tabindex="2" dirtycheck="TRUE" name="strlname" value="<%=strlname%>">&nbsp; </td>
                </tr>
                <tr>
                    <td align="right" class="control" nowrap>Phone 1:</td>
                    <td align="left" class="control" nowrap>
                        <input maxlength="14" size="14" type="text" tabindex="3" dirtycheck="TRUE" name="strPhone" value="<%=strPhone%>">&nbsp; 
		<select tabindex="4" name="strPhoneType" dirtycheck="TRUE" size="1">
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
                </tr>
                <tr>
                    <td align="right" class="control" nowrap>Phone 2:</td>
                    <td align="left" class="control" nowrap>
                        <input maxlength="14" size="14" type="text" tabindex="5" dirtycheck="TRUE" name="strPhone2" value="<%=strPhone2%>">&nbsp; 
		<select tabindex="6" name="strPhone2Type" dirtycheck="TRUE" size="1">
            <% IF strPhone2Type = "Cell" then %>
            <option selected value="Cell">Cell</option>
            <option value="Home">Home</option>
            <option value="Work">Work</option>
            <% Else %>
            <% IF strPhone2Type = "Home" then %>
            <option value="Cell">Cell</option>
            <option selected value="Home">Home</option>
            <option value="Work">Work</option>
            <% Else %>
            <option value="Cell">Cell</option>
            <option value="Home">Home</option>
            <option selected value="Work">Work</option>
            <% End If%>
            <% End If%>
        </select></td>
                </tr>
                <tr>
                    <td align="right">
                        <label class="control">Address:</label></td>
                    <td>
                        <input tabindex="7" type="text" name="straddr1" size="40" datatype="text" dirtycheck="TRUE" value="<%=straddr1%>"></td>
                </tr>
                <tr>
                    <td align="right">
                        <label class="control"></label>
                    </td>
                    <td>
                        <input tabindex="8" type="text" name="straddr2" size="40" datatype="text" dirtycheck="TRUE" value="<%=straddr2%>"></td>
                </tr>
                <tr>
                    <td align="right">
                        <label class="control">Zip:</label></td>
                    <td>
                        <input tabindex="9" type="text" name="strZip" onkeyup="CheckZip()" size="5" datatype="text" dirtycheck="TRUE" value="<%=strZip%>"></td>
                </tr>
            </table>
            <table align="center" border="0" width="500" cellspacing="0" cellpadding="0">
                <tr>
                    <td align="right">
                        <label class="control">Email:</label></td>
                    <td>
                        <input tabindex="10" type="text" name="strEmail" size="80" datatype="text" dirtycheck="TRUE" value="<%=strEmail%>"></td>
                </tr>
            </table>
            <table align="center" border="0" width="500" cellspacing="0" cellpadding="0">
                <tr>
                    <td align="center" class="control" nowrap>Make:</td>
                    <td align="center" class="control" nowrap>Model:</td>
                    <td align="center" class="control" nowrap>Color:</td>
                    <td align="center" class="control" nowrap>Tag:</td>
                    <td align="center" class="control" nowrap>Upcharge:</td>
                </tr>
                <tr>
                    <td align="left" class="control" nowrap>
                        <select name="cbomake" tabindex="11">
                            <%Call LoadListA(dbMain,3,intmake)%>
                        </select></td>
                    <td align="left" class="control" nowrap>
                        <input maxlength="40" size="20" type="text" tabindex="12" dirtycheck="TRUE" name="strVModel" value="<%=strVModel%>"></td>
                    </td>
		<td align="left" class="control" nowrap>
            <select name="cboColor" tabindex="13">
                <%Call LoadListA(dbMain,5,intColor)%>
            </select></td>
                    <td align="left" class="control" nowrap>
                        <input maxlength="40" size="10" type="text" tabindex="14" dirtycheck="TRUE" name="strTag" value="<%=strTag%>"></td>
                    </td>
		<td align="left" class="control" nowrap>
            <select name="cbomodel" tabindex="15">
                <%Call LoadList(dbMain,4,intmodel)%>
            </select></td>
                </tr>
            </table>
            <br>
            <table align="center" border="0" width="500" cellspacing="1" cellpadding="1">
                <tr>
                    <td align="center" colspan="3">
                        <button tabindex="16" name="btnSave" class="button" style="width: 75" onclick="chnSave()">Save</button>&nbsp;&nbsp;&nbsp;&nbsp;
	  <button tabindex="17" name="btnCancel" class="button" style="width: 75" onclick="Cancel()">Cancel</button>
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
<script language="VBSCRIPT">
Option Explicit


Sub Window_OnLoad()
	IF frmMain.blNotFound.value = 1 then
		msgbox "Barcode Was not found add Info"
		'window.returnValue = 0
		'window.close
		
	END IF
End Sub

Sub chnSave()
		frmMain.FormAction.value="btnSave"
		frmMain.submit()
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
			frmMain.strEmail.focus
		END IF
	END IF
END Sub

Sub SelClient()
	Dim retClient,intdata
	'intdata = frmMain.intClientID.value&"|"&document.frmmain.cboVehMan.value&"|"&document.frmmain.strVModel.value&"|"&document.frmmain.cboVehMod.value&"|"&document.frmmain.cboVehColor.value
	retClient= ShowModalDialog("SelClientDlg.asp" ,"","dialogwidth:600px;dialogheight:350px;")
	IF retClient > 2 and len(trim(retClient))>0 then
		frmMain.hdnClientID.value=retClient+"|"+frmMain.strUPC.value
		frmMain.submit()
	END IF
End Sub


Sub Cancel()
	window.returnValue = frmMain.intClientID.value
	window.close
End Sub

</script>

<%
	Call CloseConnection(dbMain)
End Sub

'********************************************************************
' Server-Side Functions
'********************************************************************
Sub GetClientInfo(dbMain,intClientID, strfname, strlname, straddr1, straddr2, strcity, strst, strzip,_
    strPhone, strPhone2, strPhoneType, strPhone2Type, strEmail)
	Dim strSQL, RS 
	strSQL="SELECT * FROM Client(Nolock) WHERE ClientID=" & intClientID 
	If DBOpenRecordset(dbMain,rs,strSQL) Then
		If Not RS.EOF Then
	
			strfname = RS("fname")
			strlname = RS("lname")
			straddr1 = RS("addr1")
			straddr2 = RS("addr2")
			strcity = RS("city")
			strst = RS("st")
			strzip = RS("zip")
			strPhone = RS("Phone")
			strPhone2 = RS("Phone2")
			strPhoneType = RS("PhoneType")
			strPhone2Type = RS("Phone2Type")
			strEmail = RS("Email")
		End If
	End If
End Sub


Sub UpdateData(dbMain)
	Dim strSQL,rsData,MaxSQL,intvehid,intClientID,intVehnum,LocationID,LoginID
	intClientID = request("intClientID")
    LocationID = request("LocationID")
    LoginID = request("LoginID")
	IF intClientID = 0 then
		MaxSQL = " Select ISNULL(Max(ClientID),0)+1 from Client (NOLOCK)"
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			intClientID=rsData(0)
		End if
		Set rsData = Nothing
		strSQL= "Insert into Client (LocationID,ClientID,fname,lname,addr1,addr2,city,st,zip,"&_
			"Phone,Phone2,PhoneType,Phone2Type,Email,status,ctype) Values ("&_
			LocationID &","&_
			intClientID &","&_
			"'" & SQLReplace(Request("strfname")) &"',"&_
			"'" & SQLReplace(Request("strlname")) &"',"&_
			"'" & SQLReplace(Request("straddr1")) &"',"&_
			"'" & SQLReplace(Request("straddr2")) &"',"&_
			"'" & SQLReplace(Request("strcity")) &"',"&_
			"'" & Request("cbost") &"',"&_
			"'" & SQLReplace(Request("strzip")) &"',"&_
			"'" & SQLReplace(Request("strPhone")) &"',"&_
			"'" & SQLReplace(Request("strPhone2")) &"',"&_
			"'" & SQLReplace(Request("strPhoneType")) &"',"&_
			"'" & SQLReplace(Request("strPhone2Type")) &"',"&_
			"'" & Request("strEmail") &"',0,0)"
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
				"Phone ='" & SQLReplace(Request("strPhone")) &"',"&_
				"Phone2 ='" & SQLReplace(Request("strPhone2")) &"',"&_
				"PhoneType ='" & SQLReplace(Request("strPhoneType")) &"',"&_
				"Phone2Type ='" & SQLReplace(Request("strPhone2Type")) &"',"&_
				"Email ='" & Request("strEmail") &"'"&_
				" Where ClientID =" & intClientID 

		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
	END IF
	IF LEN(Trim(Request("intvehid"))) > 0 then
		strSQL= "UPDATE vehical  set "&_
				" ClientID ="& intClientID &","&_
				" Make ="& request("cbomake")  &","&_
				" Vmodel ='" & request("strVmodel") &"',"&_
				" model ="& request("cbomodel")  &","&_
				" color ="& request("cboColor")  &","&_
				" tag ='"&  request("strTag")  &"'"&_
				" Where UPC='"& Request("strUPC") & "'"
		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
	ELSE
		MaxSQL = " Select ISNULL(Max(vehid),0)+1 from vehical (NOLOCK)"
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			intvehid=rsData(0)
		End if
		Set rsData = Nothing
		MaxSQL = " Select ISNULL(Max(Vehnum),0)+1 from vehical (NOLOCK) where ClientID="&intClientID
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			intVehnum=rsData(0)
		End if
		Set rsData = Nothing
		strSQL= "Insert into vehical (ClientID,vehid,UPC,Vehnum,Make,Vmodel,model,color,tag,vyear) Values ("&_
			intClientID &","&_
			intvehid &","&_
			"'" & request("strUPC") &"'," & _
			intVehnum & ","&_
			request("cbomake") &"," & _
			"'" & request("strVmodel") &"'," & _
			request("cbomodel") &"," & _
			request("cboColor") &"," & _
			"'" & request("strTag") &"'," & _
			"0)"
		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
	END IF
End Sub


%>
