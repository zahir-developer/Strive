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

Dim dbMain, intdata,arrData,intClientID, strfname, strlname, straddr1, straddr2, strcity, strst, strzip, intCtype,_
    strPhone, strPhone2, strPhoneType, strPhone2Type, strEmail, txtNotes,intStatus
dim intmake,intmodel,intColor,strVmodel,LocationID,LoginID
Set dbMain =  OpenConnection

intdata=Request("intdata")
arrData = split(intdata,"|")
intClientID = arrData(0)
intmake = arrData(1)
strVmodel = arrData(2)
intmodel = arrData(3)
intColor = arrData(4)
    LocationID = request("LocationID")
    LoginID = request("LoginID")

Select Case Request("FormAction")
	Case "btnSave"
		Call UpdateData(dbMain,intClientID)
End Select


If intClientID="" OR intClientID<3 Then 
	intClientID=0
ELSE
	Call GetClientInfo(dbMain,intClientID, strfname, strlname, straddr1, straddr2, strcity, strst, strzip, intCtype,_
    strPhone, strPhone2, strPhoneType, strPhone2Type, strEmail, txtNotes,intStatus)
End If



%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css" />
    <title></title>
</head>
<body class="pgbody">
    <form method="POST" name="frmMain" action="NewClientDlgFra.asp">
        <div style="text-align:center">
            <input type="hidden" name="FormAction" tabindex="-2">
            <input type="hidden" name="intClientID" tabindex="-2" value="<%=intClientID%>">
            <input type="hidden" name="intStatus" tabindex="-2" value="<%=intStatus%>">
            <input type="hidden" name="strst" tabindex="-2" value="<%=strst%>">
            <input type="hidden" name="intdata" tabindex="-2" value="<%=intdata%>">
            <input type="hidden" name="intmake" tabindex="-2" value="<%=intmake%>">
            <input type="hidden" name="strVmodel" tabindex="-2" value="<%=strVmodel%>">
            <input type="hidden" name="intmodel" tabindex="-2" value="<%=intmodel%>">
            <input type="hidden" name="intColor" tabindex="-2" value="<%=intColor%>">
            <input type="hidden" name="LocationID" value="<%=LocationID%>" />
            <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
            <table align="center" border="0" width="500" cellspacing="0" cellpadding="0">
                <tr>
                    <td align="right" class="control" nowrap>First Name:</td>
                    <td align="left" class="control" nowrap>
                        <input maxlength="20" size="20" type="text" tabindex="1" dirtycheck="TRUE" name="strfname" value="<%=strfname%>">&nbsp; </td>
                    <td align="right" class="control" nowrap>Phone 1:</td>
                    <td align="left" class="control" nowrap>
                        <input maxlength="14" size="14" type="text" tabindex="8" dirtycheck="TRUE" name="strPhone" value="<%=strPhone%>">&nbsp; 
		<select tabindex="9" name="strPhoneType" dirtycheck="TRUE" size="1">
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
                    <td align="right" class="control" nowrap>Last Name:</td>
                    <td align="left" class="control" nowrap>
                        <input maxlength="20" size="20" type="text" tabindex="2" dirtycheck="TRUE" name="strlname" value="<%=strlname%>">&nbsp; </td>
                    <td align="right" class="control" nowrap>Phone 2:</td>
                    <td align="left" class="control" nowrap>
                        <input maxlength="14" size="14" type="text" tabindex="10" dirtycheck="TRUE" name="strPhone2" value="<%=strPhone2%>">&nbsp; 
		<select tabindex="11" name="strPhone2Type" dirtycheck="TRUE" size="1">
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
                        <input tabindex="3" type="text" name="straddr1" size="40" datatype="text" dirtycheck="TRUE" value="<%=straddr1%>"></td>
                    <td align="right" class="control" nowrap>Type:</td>
                    <td align="left" class="control" nowrap>
                        <select name="cboCType" tabindex="12" dirtycheck="TRUE">
                            <%Call LoadList(dbMain,8,intCtype)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <label class="control"></label>
                    </td>
                    <td>
                        <input tabindex="4" type="text" name="straddr2" size="40" datatype="text" dirtycheck="TRUE" value="<%=straddr2%>"></td>
                    <td align="right" class="control" nowrap>Status:</td>
                    <td align="left" class="control" nowrap>
                        <select name="cboStatus" tabindex="13" dirtycheck="TRUE">
                            <%Call LoadList(dbMain,9,intStatus)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <label class="control">City:</label></td>
                    <td colspan="3">
                        <input tabindex="6" type="text" name="strCity" size="20" datatype="text" dirtycheck="TRUE" value="<%=strCity%>">&nbsp;<label class="control">St:</label>
                        <select name="cboSt" tabindex="7" dirtycheck="TRUE">
                            <%Call LoadList(dbMain,-1,strSt)%>
                        </select><label class="control">Zip:</label>
                        <input tabindex="5" type="text" name="strZip" onkeyup="CheckZip()" size="5" datatype="text" dirtycheck="TRUE" value="<%=strZip%>"></td>
                </tr>
            </table>
            <table align="center" border="0" width="500" cellspacing="0" cellpadding="0">
                <tr>
                    <td align="right">
                        <label class="control">Email:</label></td>
                    <td>
                        <input tabindex="14" type="text" name="strEmail" size="80" datatype="text" dirtycheck="TRUE" value="<%=strEmail%>"></td>
                </tr>
                <tr>
                    <td align="right">
                        <label class="control">Notes:</label></td>
                    <td align="left" class="control" nowrap>
                        <textarea cols="60" rows="3" title="Notes ." name="txtNotes" dirtycheck="TRUE"><%=txtNotes%>
			</textarea>
                    </td>
                </tr>
            </table>
            <br>
            <table align="center" border="0" width="500" cellspacing="1" cellpadding="1">
                <tr>
                    <td align="center" colspan="3">
                        <button name="btnSave" class="button" style="width: 75" onclick="chnSave()">Save</button>&nbsp;&nbsp;&nbsp;&nbsp;
	  <button name="btnCancel" class="button" style="width: 75" onclick="Cancel()">Cancel</button>
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
End Sub

Sub chnSave()
		frmMain.intdata.value  = frmMain.intClientID.value&"|"&document.frmmain.intmake.value&"|"&document.frmmain.strVmodel.value&"|"&document.frmmain.intmodel.value&"|"&document.frmmain.intColor.value
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
			frmMain.cboSt.value = strCitySTArr(1)
			frmMain.strPhone.focus
		END IF
	END IF
END Sub


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
Sub GetClientInfo(dbMain,intClientID, strfname, strlname, straddr1, straddr2, strcity, strst, strzip, intCtype,_
    strPhone, strPhone2, strPhoneType, strPhone2Type, strEmail, txtNotes,intStatus)
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
			intCtype = RS("Ctype")
			strPhone = RS("Phone")
			strPhone2 = RS("Phone2")
			strPhoneType = RS("PhoneType")
			strPhone2Type = RS("Phone2Type")
			strEmail = RS("Email")
			txtNotes = RS("Notes")
			intStatus = RS("Status")
		End If
	End If


	IF isnull(strst) or len(strst)=0 then
		strst = "GA"
	end if
	IF isnull(intCtype) or len(intCtype)=0 then
		intCtype = 0
	end if
	IF isnull(intStatus) or len(intStatus)=0 then
		intStatus = 0
	end if
End Sub


Sub UpdateData(dbMain,intClientID)
	Dim strSQL,rsData,MaxSQL,intvehid
	IF intClientID = 0 then
		MaxSQL = " Select ISNULL(Max(ClientID),0)+1 from Client (NOLOCK)"
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			intClientID=rsData(0)
		End if
		Set rsData = Nothing
		strSQL= "Insert into Client (LocationID,ClientID,fname,lname,addr1,addr2,city,st,zip,Ctype,"&_
			"Phone,Phone2,PhoneType,Phone2Type,Email,Notes,Status) Values ("&_
			request("LocationID") &","&_
			intClientID &","&_
			"'" & SQLReplace(Request("strfname")) &"',"&_
			"'" & SQLReplace(Request("strlname")) &"',"&_
			"'" & SQLReplace(Request("straddr1")) &"',"&_
			"'" & SQLReplace(Request("straddr2")) &"',"&_
			"'" & SQLReplace(Request("strcity")) &"',"&_
			"'" & Request("cbost") &"',"&_
			"'" & SQLReplace(Request("strzip")) &"',"&_
			Request("cboCtype") &","&_
			"'" & SQLReplace(Request("strPhone")) &"',"&_
			"'" & SQLReplace(Request("strPhone2")) &"',"&_
			"'" & SQLReplace(Request("strPhoneType")) &"',"&_
			"'" & SQLReplace(Request("strPhone2Type")) &"',"&_
			"'" & Request("strEmail") &"',"&_
			"'" & Request("txtNotes") &"',"&_
			Request("cboStatus") & ")"
		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
		MaxSQL = " Select ISNULL(Max(vehid),0)+1 from vehical"
		If dbOpenRecordSet(dbmain,rsData,MaxSQL) Then
			intvehid=rsData(0)
		End if
		Set rsData = Nothing
		strSQL= "Insert into vehical (LocationID,ClientID,vehid,Vehnum,Make,Vmodel,model,color,vyear) Values ("&_
			request("LocationID") &","&_
			intClientID &","&_
			intvehid &","&_
			"1,"&_
			request("intmake") &"," & _
			"'" & request("strVmodel") &"'," & _
			request("intmodel") &"," & _
			request("intColor") &"," & _
			"0)"
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
				"Ctype =" & Request("cboCtype") &","&_
				"Phone ='" & SQLReplace(Request("strPhone")) &"',"&_
				"Phone2 ='" & SQLReplace(Request("strPhone2")) &"',"&_
				"PhoneType ='" & SQLReplace(Request("strPhoneType")) &"',"&_
				"Phone2Type ='" & SQLReplace(Request("strPhone2Type")) &"',"&_
				"Email ='" & Request("strEmail") &"',"&_
				"Notes ='" & Request("txtNotes") &"',"&_
				"Status =" & Request("cboStatus") &_
				" Where ClientID =" & intClientID 

		IF NOT DBExec(dbMain, strSQL) then
			Response.Write gstrMsg
			Response.End
		END IF
	END IF
End Sub


%>
