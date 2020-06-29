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
       ' Response.Write("hey")
    'Response.End()
	Dim intUserID, strFirstName, strLastName, cboProfileID, cboRoleID,intMoreInfo,LocationID,LoginID
	Dim strPhone, strFax, strEmail, intLoginID, strPassword, strInitials, blnActive, strResult
	Dim strAddress,strCity,strState,strZip,strPayRate,strSickRate,strVacRate,strComRate,blTip
	Dim intcitizen,strAlienNo,dtAuthDate,rs2,strSQL2
	Dim dbMain, strSaveHappened, intLocationCount,strLoc,strSSNo,dtHire,dtBirth,dtLRT,strGender,strMStat,strSalery,intExempt
	Set dbMain =  OpenConnection
'Call CheckAccess(dbMain)



	intUserID=Request("UserID")
	strLoc = request("strLoc")
    LocationID = request("LocationID")
    LoginID = request("LoginID")

	If intUserId=0 then 
	cboProfileID=1


	End If
	strSaveHappened = "True"

	'intLocationCount = GetLocationCount(dbMain)
	Select Case Request("FormAction")

		Case "btnSave"
			If SaveData(dbMain,intUserID) Then
				strSaveHappened="True"
			Else
				strSaveHappened="False"
			End IF
			
		Case "btnDelete"
			Call DoDelete(dbmain, intUserID)
			Response.Redirect "admUsers.asp?LocationID="& LocationID & "&LoginID="& LoginID

		Case "btnDone"
				Response.Redirect  "admUsers.asp?LocationID="& LocationID & "&LoginID="& LoginID
		
	End Select



	If strSaveHappened = "True" Then
		If intUserID<>0 then
				'If intLocationCount = 1 Then
				'	Call GetUserProfile(dbMain,intUserID,cboProfileID)
				'End If
			Call GetUserInfo(dbMain, intUserID, strFirstName, strLastName, cboRoleID,_
						strPhone, strFax, strEmail, intLoginID, strPassword, strInitials, blnActive,_
						strAddress,strCity,strState,strZip,strPayRate,strSickRate,strVacRate,strComRate,_
						strSSNo,dtHire,dtBirth,dtLRT,strGender,strMStat,strSalery,intExempt,blTip,intcitizen,strAlienNo,dtAuthDate,LocationID,LoginID)
		ELSE
			strSalery = 0.00
			strPayRate = 0.00
			strSickRate = 0.00
			strVacRate = 0.00
			strComRate = 0.00
			intExempt = 1
			blTip = 1
			intcitizen = 0
			strAlienNo = ""
			strGender = "M"
			dtHire = date()
			strSQL2= "SELECT listvalue FROM LM_ListItem (NOLOCK) WHERE Listtype = -1 AND Listcode = 5"
			If DBOpenRecordset(dbMain,rs2,strSQL2) Then
				If NOT RS2.EOF Then
					strState = Trim(RS2("listvalue"))
				End If
			End If
			Set RS2 = Nothing
			'strSQL2= "SELECT listvalue FROM LM_ListItem (NOLOCK) WHERE Listtype = 11 AND Listcode = 4"
			'If DBOpenRecordset(dbMain,rs2,strSQL2) Then
			'	If NOT RS2.EOF Then
			'		strCity = Trim(RS2("listvalue"))
			'	End If
			'End If
			'Set RS2 = Nothing
			'strSQL2= "SELECT listvalue FROM LM_ListItem (NOLOCK) WHERE Listtype = 11 AND Listcode = 5"
			'If DBOpenRecordset(dbMain,rs2,strSQL2) Then
			'	If NOT RS2.EOF Then
			'		strZip = Trim(RS2("listvalue"))
			'	End If
			'End If
			'Set RS2 = Nothing
		End If
	Else
		strFirstName = Request("strFirstName")
		strLastName = Request("strLastName")
		cboRoleID = Request("cboRoleID")
		cboProfileID = Request("cboProfileID")
		strPhone = Request("strPhone")
		strFax = Request("strFax")
		strEmail = Request("strEmail")
		intLoginID = Request("intLoginID")
		strPassword = Request("strPassword")
		strInitials = Request("strInitials")
		blnActive = Request("rbActive")
		strAddress = Request("strAddress")
		strCity = Request("strCity")
		strState = Request("strState")
		strZip = Request("strZip")
		strPayRate = Request("strPayRate")
		strSickRate = Request("strSickRate")
		strVacRate = Request("strVacRate")
		strComRate = Request("strComRate")
		strSSNo = Request("strSSNo")
		dtHire = Request("dtHire")
		dtBirth = Request("dtBirth")
		dtLRT = Request("dtLRT")
		strGender = Request("strGender")
		strMStat = Request("strMStat")
		strSalery = Request("strSalery")
		intExempt = Request("intExempt")
		blTip = Request("blTip")
		intcitizen = Request("intcitizen")
		strAlienNo = Request("strAlienNo")
		dtAuthDate = Request("dtAuthDate")
	End if
'If  intUserID > 0 Then 
'	Call GetMoreInfo(dbMain,intMoreInfo)
'End If
'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css" />
    <title>Edit User</title>
    <meta http-equiv="x-ua-compatible" content="IE=10">
</head>
<body class="pgbody" onkeyup="SetDirty()" onclick="SetDirty()">
    <form method="POST" name="frmMain" action="admUserEdit.asp">
        <input type="hidden" tabindex="-1" name="FormAction" value="" />
        <input type="hidden" tabindex="-1" name="hdnLocationIDs" value="" />
        <input type="hidden" tabindex="-1" name="UserID" value="<%=intUserID%>" />
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
        <div style="text-align: center">

            <table style="width: 769px; border-collapse: collapse;">
                <tr>
                    <td style="text-align: right">
                        <label class="control">First Name:</label></td>
                    <td>
                        <input tabindex="1" type="text" name="strFirstName" size="40" datatype="r" dirtycheck="TRUE" value="<%=strFirstName%>"></td>
                    <td style="text-align: right">
                        <label class="control">Login ID:</label></td>
                    <td>
                        <input tabindex="5" type="text" name="intLoginID" size="30" datatype="r" dirtycheck="TRUE" value="<%=intLoginID%>" maxlength="16"></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <label class="control">Last Name:</label></td>
                    <td>
                        <input tabindex="2" type="text" name="strLastName" size="40" datatype="r" dirtycheck="TRUE" value="<%=strLastName%>"></td>
                    <td style="text-align: right">
                        <label class="control">Password:</label></td>
                    <td>
                        <input tabindex="6" type="password" name="strPassword" size="30" dirtycheck="TRUE" value="<%=strPassword%>" maxlength="16"></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <label class="control">Address:</label></td>
                    <td>
                        <input tabindex="2" type="text" name="strAddress" size="40" datatype="r" dirtycheck="TRUE" value="<%=strAddress%>"></td>
                    <td style="text-align: right">
                        <label class="control">Confirm Password:</label></td>
                    <td>
                        <input tabindex="7" type="password" name="strCPassword" size="30" dirtycheck="TRUE" value="<%=strPassword%>" maxlength="16"></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <label class="control">City:</label></td>
                    <td>
                        <input tabindex="2" type="text" name="strCity" size="20" datatype="r" dirtycheck="TRUE" value="<%=strCity%>">&nbsp;<label class="control">St:</label>
                        <input tabindex="2" type="text" name="strState" size="2" datatype="r" dirtycheck="TRUE" value="<%=strState%>">&nbsp;<label class="control">Zip:</label>
                        <input tabindex="2" type="text" name="strZip" size="5" datatype="r" dirtycheck="TRUE" value="<%=strZip%>"></td>
                    <td style="text-align: right">
                        <label class="control">Status:</label></td>
                    <td>
                        <input tabindex="3" type="radio" dirtycheck="TRUE" id="IDrbActive1" <%=IIF (blnActive<>0, " Checked ", "" )%> name="rbActive" value="1">
                        <label class="control" for="IDrbActive1">Active</label>&nbsp;
			<input tabindex="3" type="radio" dirtycheck="TRUE" id="IDrbActive2" <%=IIF(blnActive=0, " Checked ", "" )%> name="rbActive" value="0">
                        <label class="control" for="IDrbActive2">Inactive</label></td>
                </tr>
            </table>
            <table style="width: 769px; border-collapse: collapse; height: 30px" border="0">
                <tr>
                    <td style="text-align: right">
                        <label class="control">Default Position:</label></td>
                    <td>
                        <select tabindex="4" name="cboRoleID" dirtycheck="TRUE" size="1">
                            <% Call LoadList(dbMain,6,cboRoleID)%>
                        </select>
                    </td>
                    <td style="text-align: right">
                        <label class="control">Initials:</label></td>
                    <td>
                        <input tabindex="11" type="text" name="strInitials" size="4" dirtycheck="TRUE" value="<%=strInitials%>"></td>
                    <td style="text-align: right">
                        <label class="control">Gender:</label></td>
                    <td>
                        <select tabindex="4" name="strGender" dirtycheck="TRUE" size="1">
                            <% IF strGender = "M" then %>
                            <option selected value="M">M</option>
                            <option value="F">F</option>
                            <% Else %>
                            <option value="M">M</option>
                            <option selected value="F">F</option>
                            <% End If%>
                        </select></td>
                    <td style="text-align: right">
                        <label class="control">Marital:</label></td>
                    <td>
                        <select tabindex="4" name="strMStat" dirtycheck="TRUE" size="1">
                            <% IF strMStat = "M" then %>
                            <option selected value="M">M</option>
                            <option value="S">S</option>
                            <% Else %>
                            <option value="M">M</option>
                            <option selected value="S">S</option>
                            <% End If%>
                        </select></td>
                    <td style="text-align: right">
                        <label class="control">Exemptions:</label></td>
                    <td>
                        <input tabindex="11" type="text" name="intExempt" size="4" dirtycheck="TRUE" value="<%=intExempt%>"></td>
                </tr>
            </table>
            <table style="width: 769px; border-collapse: collapse; height: 30px">
                <tr>
                    <td style="text-align: right">
                        <label class="control">Hire Date:</label></td>
                    <td>
                        <input tabindex="11" type="date" name="dtHire" size="10" dirtycheck="TRUE" value="<%=dtHire%>"></td>
                    <td style="text-align: right">
                        <label class="control">Birth Date:</label></td>
                    <td>
                        <input tabindex="11" type="date" name="dtBirth" size="10" dirtycheck="TRUE" value="<%=dtBirth%>"></td>
                    <td style="text-align: right">
                        <label class="control">Leave/Raise/Term Date:</label></td>
                    <td>
                        <input tabindex="11" type="date" name="dtLRT" size="10" dirtycheck="TRUE" value="<%=dtLRT%>"></td>
                </tr>
            </table>
            <table style="width: 769px; border-collapse: collapse; height: 30px">
                <tr>
                    <td style="text-align: right">
                        <label class="control">Phone:</label></td>
                    <td>
                        <input tabindex="8" type="text" name="strPhone" size="14" dirtycheck="TRUE" value="<%=strPhone%>"></td>
                    <td style="text-align: right">
                        <label class="control">Cell:</label></td>
                    <td>
                        <input tabindex="8" type="text" name="strFax" size="14" dirtycheck="TRUE" value="<%=strFax%>"></td>
                    <td style="text-align: right">
                        <label class="control">Email:</label></td>
                    <td>
                        <input tabindex="10" type="text" name="strEmail" size="40" dirtycheck="TRUE" value="<%=strEmail%>"></td>
                    <td style="text-align: right">
                        <label class="control">SS #:</label></td>
                    <td>
                        <input tabindex="11" type="text" name="strSSNo" size="11" dirtycheck="TRUE" value="<%=strSSNo%>"></td>
                </tr>
            </table>
            <table style="width: 769px; border-collapse: collapse; height: 30px">
                <tr>
                    <td style="text-align: right">
                        <label class="control">Hourly:</label></td>
                    <td>
                        <input tabindex="9" type="text" name="strPayRate" size="10" dirtycheck="TRUE" value="<%=strPayRate%>"></td>
                    <td style="text-align: right" class="control" nowrap>Tip:</td>
                    <td align="left">
                        <input name="chkTip" type="checkbox" dirtycheck="TRUE"
                            <%If blTip Then%>
                            checked
                            <%End If%>>
                    </td>
                    <td>
                        <input tabindex="3" type="radio" dirtycheck="TRUE" id="IDrbcitizen1" <%=IIF (intcitizen=0, " Checked ", "" )%> name="rbcitizen" value="0">
                        <label class="control" for="IDrbcitizen1">A citizen or national of the United States</label>
                    &nbsp;
                </tr>
                <tr>
                    <td style="text-align: right">
                        <label class="control">Salary:</label></td>
                    <td>
                        <input tabindex="9" type="text" name="strSalery" size="10" dirtycheck="TRUE" value="<%=strSalery%>"></td>
                    <td style="text-align: right">
                        <label class="control">Sick :</label></td>
                    <td>
                        <input tabindex="9" type="text" name="strSickRate" size="10" dirtycheck="TRUE" value="<%=strSickRate%>"></td>
                    <td>
                        <input tabindex="3" type="radio" dirtycheck="TRUE" id="IDrbcitizen2" <%=IIF(intcitizen=1, " Checked ", "" )%> name="rbcitizen" value="1">
                        <label class="control" for="IDrbcitizen2">A Lawful Permanent Resident (Alien #) A </label>
                        <input type="text" name="strAlienNo" size="10" dirtycheck="TRUE" value="<%=strAlienNo%>"></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <label class="control">Commission :</label></td>
                    <td>
                        <input tabindex="9" type="text" name="strComRate" size="10" dirtycheck="TRUE" value="<%=strComRate%>"></td>
                    <td style="text-align: right">
                        <label class="control">Vacation :</label></td>
                    <td>
                        <input tabindex="11" type="text" name="strVacRate" size="10" dirtycheck="TRUE" value="<%=strVacRate%>"></td>
                    <td>
                        <input tabindex="3" type="radio" dirtycheck="TRUE" id="IDrbcitizen3" <%=IIF(intcitizen=2, " Checked ", "" )%> name="rbcitizen" value="2">
                        <label class="control" for="IDrbcitizen3">A Alien authorized to work until </label>
                        <input type="date" size="10" name="dtAuthDate" dirtycheck="TRUE" value="<%=dtAuthDate%>"></td>
                </tr>





            </table>
            <br>
            <table style="width: 769px; border-collapse: collapse; height: 30px">
                <tr>
                     <td style="text-align: right">
                        <button name="btnNew" tabindex="12" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" style="width: 75px" onclick="SubmitForm()">New</button>
                        <button name="btnCopy" tabindex="13" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" style="width: 75px" onclick="SubmitForm()">Copy</button>
                        <button name="btnDelete" tabindex="14" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" style="width: 75px" onclick="SubmitForm()">Delete</button>
                        <button name="btnSave" tabindex="15" class="buttondead" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" dirtycheck="TRUE" style="width: 75px" onclick="SubmitForm()">Save</button>
                        <button name="btnDone" tabindex="16" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" style="width: 75px" onclick="SubmitForm()">Done</button>
                    </td>
                </tr>
            </table>
            <table style="width: 769px; border-collapse: collapse;">
                <tr>
                    <td width="15" class="tabgap">&nbsp;</td>
                    <td class="tabselect" style="text-align: center; white-space:nowrap;"><a name="Uniforms" href="#1" class="control" onclick="ChangeTab()">Uniforms.</a> </td>
                    <td class="tabgap">&nbsp;</td>
                    <td class="tab" style="text-align: center; white-space:nowrap;"><a name="Collision" href="#1" class="control" onclick="ChangeTab()">Collision.</a></td>
                    <td class="tabgap">&nbsp;</td>
                    <td class="tab"  style="text-align: center; white-space:nowrap;"><a name="Docs" href="#1" class="control" onclick="ChangeTab()">Documents.</a></td>
                    <td class="tabgap">&nbsp;</td>
                    <td style="width: 300px" class="tabgap">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td class="tabgap">&nbsp;</td>
                    <td style="width: 300px" class="tabgap">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td class="tabgap">&nbsp;</td>
                    <td style="width: 300px" class="tabgap">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td class="tabgap">&nbsp;</td>
                    <td style="width: 300px" class="tabgap">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td class="tabgap">&nbsp;</td>
                    <td style="width: 300px" class="tabgap">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td style="text-align: right">&nbsp;
		                <button name="btnAddDocs" style="text-align: center; width:140px;display:none" class="button" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" onclick="SubmitForm()" value="yes">Add Documents.</button>
                        <button name="btnCollision" style="text-align: center; width:140px;display:none" class="button" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" onclick="SubmitForm()" value="yes">Add Collision.</button>
                        <button name="btnUniforms" style="text-align: center; width:140px;" class="button" onmouseover="ButtonHigh()" onmouseout="ButtonLow()" onclick="SubmitForm()" value="yes">Add Items.</button>
                    </td>
                </tr>
            </table>
            <iframe name="fraMain2" src="admLoading.asp" scrolling="yes" height="100" width="768" frameborder="0"></iframe>
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
Dim Validate,obj

Sub Window_Onload()
	Dim Obj
			Uniforms.ParentElement.className = "tabSelect"	
			Collision.ParentElement.className = "tab"
			Docs.ParentElement.className = "tab"
			frmMain.btnUniforms.style.Display = "inline"
			frmMain.btnCollision.style.Display = "none"
			frmMain.btnAddDocs.style.Display = "none"
				fraMain2.location.href = "admUniformsFra.asp?intUserID=" & frmMain.UserID.Value&"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	If  "<%=intUserID%>" = 0 Then 
			document.frmMain.btnCopy.className="buttondead"
	End If
	
End Sub

Sub ChangeTab
	If UCase(window.event.srcElement.ClassName) = "LNKDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If
	If UCase(window.event.srcElement.parentElement.className) = "TABSELECT" Then  exit sub
	Select Case window.event.srcElement.name
		Case "Uniforms"
			Uniforms.ParentElement.className = "tabSelect"	
			Collision.ParentElement.className = "tab"
			Docs.ParentElement.className = "tab"
				frmMain.btnAddDocs.style.Display = "none"
				frmMain.btnUniforms.style.Display = "inline"
				frmMain.btnCollision.style.Display = "none"
				fraMain2.location.href = "admUniformsFra.asp?intUserID=" & frmMain.UserID.Value&"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
			
		Case "Collision"
			Uniforms.ParentElement.className = "tab"	
			Collision.ParentElement.className = "tabSelect"
			Docs.ParentElement.className = "tab"
				frmMain.btnAddDocs.style.Display = "none"
				frmMain.btnUniforms.style.Display = "none"
				frmMain.btnCollision.style.Display = "inline"
				fraMain2.location.href = "admCollisionFra.asp?intUserID=" & frmMain.UserID.Value&"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value

		Case "Docs"
			Uniforms.ParentElement.className = "tab"	
			Collision.ParentElement.className = "tab"
			Docs.ParentElement.className = "tabSelect"
				frmMain.btnUniforms.style.Display = "none"
				frmMain.btnCollision.style.Display = "none"
				frmMain.btnAddDocs.style.Display = "inline"
				fraMain2.location.href = "admDocListFra.asp?intUserID=" & frmMain.UserID.Value&"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
	End Select
	window.focus
End Sub


Sub SubmitForm()
	
	window.event.CancelBubble=True
	window.event.ReturnValue=False

	If UCase(window.event.srcElement.ClassName) = "BUTTONDEAD" Then
		window.Event.ReturnValue = FALSE
		Exit Sub
	End If

	Select Case window.event.srcElement.name
		Case "btnCopy"
			frmMain.UserID.Value=0
			frmMain.intLoginID.Value = ""
			frmMain.strPassword.Value = ""
			frmMain.strCPassword.Value = ""
			MsgBox "Please make appropriate changes!", ,"Mammoth - User Copied"
			
		Case "btnSave"
			If Len(Trim(frmMain.intLoginID.Value)) < 4 Then
				MsgBox "The login for a user must be atleast 4 characters long!", ,"Mammoth Error"
				Exit sub
			End If
			If Len(Trim(frmMain.strPassword.Value)) < 4 Then
				MsgBox "The password for a user must be atleast 4 characters long!", ,"Mammoth Error"
				Exit Sub
			End If
			If ValdataType Then
				Call ResetDirty
				Dim LocAssignItemCount
					frmMain.FormAction.value="btnSave"
					frmMain.Submit()
			End If
			
		Case "btnNew"
			location.href="admUserEdit.asp?UserID=0&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value

		Case "btnDelete"
			Dim DeleteAnswer
			DeleteAnswer = MsgBox("Are you sure you want to delete the user " & frmMain.strFirstName.value & " " & frmMain.strLastName.value & "'?", 36, "Confirm Delete")
			If DeleteAnswer = 6 Then
				frmMain.FormAction.value="btnDelete"
				frmMain.Submit()
			End If
		Case "btnDone"
				location.href="admusers.asp?LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
		Case "btnCollision"
			Dim retCollision
			retCollision= ShowModalDialog("admNewColl.asp?intUserID=<%=intUserID%>&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:460px;dialogheight:260px;")
			fraMain2.location.href = "admCollisionFra.asp?intUserID=" & frmMain.UserID.Value&"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
		
		Case "btnAddDocs"
			Dim strFilt
			strFilt = ShowModalDialog("admUserDocDlg.asp?intUserID=<%=intUserID%> &LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value  ,,"center:1;dialogwidth:320px;dialogheight:180px;")
			fraMain2.location.href = "admDocListFra.asp?intUserID=" & frmMain.UserID.Value &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value
		
		Case "btnUniforms"
			Dim retUniforms
			retUniforms= ShowModalDialog("admNewUnif.asp?intUserID=<%=intUserID%>&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:460px;dialogheight:260px;")
			fraMain2.location.href = "admUniformsFra.asp?intUserID=" & frmMain.UserID.Value &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value




	End Select
End Sub

Sub btnAdd_Onclick()
Dim strValue
strValue = ShowModalDialog("admUsersLocationsDlg.asp?UserID=<%=intUserID%>&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value,,"center:1;dialogwidth:470px;dialogheight:340px;")
If Len(strValue) > 1 Then	
	fraMain.frmMain.formaction.value="AddLocation"
	fraMain.frmMain.hdnValue.value = strValue
	Call ResetDirty
	fraMain.frmMain.submit
	window.event.returnValue = False
Else
	window.event.returnValue = False
End If
End Sub


Sub Window_OnBeforeUnLoad
	Call dirtycheck
End Sub



</script>

<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Sub GetUserInfo(db, intUserID, strFirstName, strLastName, cboRoleID,_
				strPhone, strFax, strEmail, intLoginID, strPassword, strInitials, blnActive,_
				strAddress,strCity,strState,strZip,strPayRate,strSickRate,strVacRate,strComRate,_
				strSSNo,dtHire,dtBirth,dtLRT,strGender,strMStat,strSalery,intExempt,blTip,intcitizen,strAlienNo,dtAuthDate,LocationID,LoginID)

	Dim strSQL, RS, strSQL2, RS2

	strSQL= "Select * from LM_Users where UserID=" & intUserID &" and LocationID=" & LocationID
		'Response.Write strsql
	If DBOpenRecordset(db,rs,strSQL) Then
		If NOT RS.EOF Then
			strFirstName=Trim(RS("FirstName"))
			strLastName=Trim(RS("LastName"))
			cboRoleID=Clong(RS("RoleID"))
			strPhone=Trim(RS("Phone"))
			strFax=Trim(RS("Fax"))
			strEmail=Trim(RS("Email"))
			blnActive=Cbool(RS("Active"))
			intLoginID=RS("LoginID")
			strPassword=Trim(RS("Password"))
			strInitials=Trim(RS("Initials"))
			strAddress = Trim(RS("Address"))
			strCity = Trim(RS("City"))
			strState = Trim(RS("State"))
			strZip = Trim(RS("Zip"))
			strPayRate = Trim(RS("PayRate"))
			strSickRate = Trim(RS("SickRate"))
			strVacRate = Trim(RS("VacRate"))
			strComRate = Trim(RS("ComRate"))
			strSSNo = Trim(RS("SSNo"))
			dtHire = Trim(RS("Hire"))
			dtBirth = Trim(RS("Birth"))
			dtLRT = Trim(RS("LRT"))
			strGender = Trim(RS("Gender"))
			strMStat = Trim(RS("MStat"))
			strSalery = Trim(RS("Salery"))
			intExempt = Trim(RS("Exempt"))
			blTip = Trim(RS("Tip"))
			intcitizen = Trim(RS("citizen"))
			strAlienNo = Trim(RS("AlienNo"))
			dtAuthDate = Trim(RS("AuthDate"))
		ELSE
			strPayRate = 0.00
			strSickRate = 0.00
			strVacRate = 0.00
			strComRate = 0.00
			intExempt = 1
			blTip = 1
			intcitizen = 0
			strSQL2= "SELECT listvalue FROM LM_ListItem (NOLOCK) WHERE Listtype = 11 AND Listcode = 3"
			If DBOpenRecordset(db,rs2,strSQL2) Then
				If NOT RS2.EOF Then
					strState = Trim(RS2("listvalue"))
				End If
			End If
			Set RS2 = Nothing
		End If
	End If
	Set RS = Nothing
	IF dtAuthDate = "1/1/1900" then
		dtAuthDate = ""
	END IF
	IF dtHire = "1/1/1900" then
		dtHire = ""
	END IF
	IF dtLRT = "1/1/1900" then
		dtLRT = ""
	END IF
	IF len(intcitizen) = 0 or isnull(intcitizen) then
	intcitizen = 0
	end if


	IF len(intExempt) = 0 or isnull(intExempt) then
	intExempt = 0
	end if
End Sub



Function SaveData(db,iUserID)

	SaveData=True
	Dim strSQL, RS,blTip,intcitizen,strLoginID,strPassword
	If Request("strCPassword") <> Request("strPassword") Then %>
<script language="VBscript">
		MsgBox "Your confirm password is different from your password. Please try again!", 48  ,"Mammoth - Password Mismatch"
</script>
<%
		SaveData = False
		Exit Function
	ElseIf LoginIDDupe(db,Request("UserID"),Request("intLoginID")) Then %>
<script language="VBscript">
		MsgBox "Your LoginID is already in use by another user." & chr(13) & "Please enter a different LoginID then click Save again!", 48  ,"Mammoth - LoginID Dupe"
</script>
<%
		SaveData = False
		Exit Function
	End If 
	IF   request("chkTip") = "on" then
		blTip = 1
	ELSE
		blTip = 0
	END IF
	
	intcitizen = request("rbcitizen")

	'Response.Write request("rbcitizen")
	'Response.End
	If Request("rbActive") = 0 then
        strLoginID =" "
        strPassword = null
    else
        strLoginID = Request("intLoginID")
        strPassword = Request("strPassword")
    end if




	If iUserID=0 then 
		Dim MaxSQL,ID
		MaxSQL = " Select Max(UserID) + 1 as MaxUserID from LM_User where LocationID=" & request("LocationID")
		Set RS = Nothing
	    
	
		strSQL= "{ Call LM_UserInsert('" & SQLReplace(Request("strFirstName")) & "', " & _
			"'" & SQLReplace(Request("strLastName")) & "', " & _
				  NullTest(Request("cboRoleID")) & ","  & _
			"'" & SQLReplace(Request("strPhone")) & "', " & _
			"'" & SQLReplace(Request("strEmail")) & "', " & _			
			NullTest(Request("rbActive")) & "," & _
			"'" & strLoginID & "', " & _
			"'" & strPassword & "', " & _				  
			"'" & SQLReplace(Request("strInitials")) & "', " & _
			"'" & SQLReplace(Request("strAddress")) & "', " & _
			"'" & SQLReplace(Request("strCity")) & "', " & _
			"'" & SQLReplace(Request("strState")) & "', " & _
			"'" & SQLReplace(Request("strZip")) & "', " & _
			"'" & SQLReplace(Request("strPayRate")) & "', " & _
			"'" & SQLReplace(Request("strSickRate")) & "', " & _
			"'" & SQLReplace(Request("strVacRate")) & "', " & _
			"'" & SQLReplace(Request("strComRate")) & "', " & _
			"'" & SQLReplace(Request("strSSNo")) & "', " & _
			"'" & SQLReplace(Request("dtHire")) & "', " & _
			"'" & SQLReplace(Request("dtBirth")) & "', " & _
			"'" & SQLReplace(Request("dtLRT")) & "', " & _
			"'" & SQLReplace(Request("strGender")) & "', " & _
			"'" & SQLReplace(Request("strMStat")) & "', " & _
			"'" & SQLReplace(Request("strSalery")) & "', " & _
			blTip & ", " & _
			intcitizen & ", " & _
			"'" & SQLReplace(Request("strAlienNo")) & "', " & _
			"'" & SQLReplace(Request("dtAuthDate")) & "', " & _
			SQLReplace(Request("intExempt")) & ", " & _
			"'" & SQLReplace(Request("strFax")) & "',~|~," &_
            request("LocationID") &")} "
			
	
	Call dbInsertMax(db,rs,MaxSQL,strSQL,iUserID)	
			
	Else

			strSQL = "{ Call LM_UserUpdate('" & SQLReplace(Request("strFirstName")) & "', " & _
			"'" & SQLReplace(Request("strLastName")) & "'," & _
				 NullTest(Request("cboRoleID")) & ","  & _
			"'" & SQLReplace(Request("strPhone")) & "', " & _
			"'" & SQLReplace(Request("strEmail")) & "', " & _
			    SQLReplace(Request("rbActive")) & "," & _
			"'" & strLoginID & "', " & _
			"'" & strPassword & "', " & _
			"'" & SQLReplace(Request("strInitials")) & "', " & _
			"'" & SQLReplace(Request("strAddress")) & "', " & _
			"'" & SQLReplace(Request("strCity")) & "', " & _
			"'" & SQLReplace(Request("strState")) & "', " & _
			"'" & SQLReplace(Request("strZip")) & "', " & _
			"'" & SQLReplace(Request("strPayRate")) & "', " & _
			"'" & SQLReplace(Request("strSickRate")) & "', " & _
			"'" & SQLReplace(Request("strVacRate")) & "', " & _
			"'" & SQLReplace(Request("strComRate")) & "', " & _
			"'" & SQLReplace(Request("strSSNo")) & "', " & _
			"'" & SQLReplace(Request("dtHire")) & "', " & _
			"'" & SQLReplace(Request("dtBirth")) & "', " & _
			"'" & SQLReplace(Request("dtLRT")) & "', " & _
			"'" & SQLReplace(Request("strGender")) & "', " & _
			"'" & SQLReplace(Request("strMStat")) & "', " & _
			"'" & SQLReplace(Request("strSalery")) & "', " & _
			blTip & ", " & _
			intcitizen & ", " & _
			"'" & SQLReplace(Request("strAlienNo")) & "', " & _
			"'" & SQLReplace(Request("dtAuthDate")) & "', " & _
			SQLReplace(Request("intExempt")) & ", " & _
			"'" & SQLReplace(Request("strFax")) & "'," & _
            iUserID & ","&_
             request("LocationID") & ")}"
        
			If Not(DBExec(db, strSQL)) Then
				Response.Write gStrMsg
			End If
		End If
		'If intLocationCount = 1 Then
		'	Call SaveUser(db, iUserID)
		'End If

End Function

Function LoginIDDupe(dbMain, iUserID, iLoginID)
	'Response.Write "UserID = " & iUserID & "  " & "LoginID = " & iLoginID & " "
	LoginIDDupe = False
	If iLoginID="" OR iLoginID="0" Then
		LoginIDDupe = False
	Else
		Dim strSQL, RS, NumberOfRecords

		strSQL="SELECT Count(UserID) AS NumRec FROM LM_Users WHERE LoginID='" & iLoginID & "' AND UserID <>" & iUserID & " AND LocationID=" & request("LocationID")
		'Response.Write strSQL
		If DBOpenRecordset(dbMain,rs,strSQL) Then
			If Not RS.EOF Then
				NumberOfRecords=Clong(RS("NumRec"))
			End If
		End If
		
		If NumberOfRecords = 0 Then
			LoginIDDupe = False
		Else
			LoginIDDupe = True
		End If
		
	End If
		
		Set RS = Nothing

End Function


Sub DoDelete(db,iUserID)
	
	Dim strSQL, RS

	strSQL = "Delete FROM LM_Users Where UserID=" & iUserID & " AND LocationID=" & request("LocationID")
	Call DBExec(db, strSQL)

End Sub

'Sub GetMoreInfo(db,intMoreInfo)
'	Dim strSQL,rsData
'	
'	strSQL = " Select ListValue FROM LM_ListItem WHERE ListType=-5 AND ListCode=2 "
'	
'	If dbOpenRecordset(db,rsData,strSQL) Then
'		IF not rsData.eof then
'			intMoreInfo = rsData("ListValue")
'		END IF
'	End If
    '
'End Sub


%>
