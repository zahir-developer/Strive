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
<%
'********************************************************************
' Global Variables
'********************************************************************
Dim tempMID

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
Dim dbMain, intMenuLeft, intMenuTop, strLoginWelcome,LocationID
Dim rs, strSQL
If session("UserID") = "" Then
	Response.redirect "admLogin.asp"
End If

Set dbMain =  OpenConnection
If Request("LocID") <> "" then
	Call GetLocData(dbMain,Request("LocID"))
End If

    LocationID = Request("LocationID")


		Session("intDetails")=0
		Session("intWashes")=0
		Session("intWashers")=0
		Session("intDetailers")=0
		Session("intScore")=0


    strSQL =" SELECT isnull(SUM(1),0) AS cnt FROM REC(NOLOCK)"&_
    " INNER JOIN RECITEM(NOLOCK) ON  REC.recid = RECITEM.recId"&_
    " INNER JOIN Product ON RECITEM.ProdID = Product.ProdID"&_
    " WHERE (DATEPART(Month, REC.CloseDte) = '" & Month(date()) & "')"&_
    " AND (DATEPART(Day, REC.CloseDte) = '" & day(date()) & "')"&_
    " AND (DATEPART(Year, REC.CloseDte) = '" & year(date()) & "')"&_
    " AND (REC.Status >= 70) AND (Product.cat = 1)"
 
 
 	If DBOpenRecordset(dbMain,rs,strSQL) Then
    	IF NOT rs.EOF then
    		Session("intWashes") = rs("cnt")	
    	END IF
    END IF

    dim intUserID,Cdatetime,strTotalHrs,strHours,Caction,cnt2
    strTotalHrs = 0
    intUserID = 0
    cnt2 = 0
    strSQL =" SELECT TimeClock.UserID, TimeClock.Cdatetime, TimeClock.Caction "&_
	        " FROM TimeClock"&_
             " WHERE (DATEPART(Month, TimeClock.Cdatetime) = MONTH(GETDATE())) AND (DATEPART(Day, TimeClock.Cdatetime) = DAY(GETDATE()))"&_
            " AND (DATEPART(Year, TimeClock.Cdatetime) = YEAR(GETDATE()))  AND (CType <> 9)"&_
            " ORDER BY TimeClock.UserID, TimeClock.Cdatetime, TimeClock.Caction"
 	If DBOpenRecordset(dbMain,rs,strSQL) Then
    	IF NOT rs.EOF then
	        Do While Not RS.EOF 
               IF intUserID > 0 then
                    If rs("UserID") = intUserID then
                         IF rs("Caction") = 1 then
                            strHours = strHours + DateDiff("n",Cdatetime,rs("Cdatetime")) 
                        END IF 
                    else
                        IF Caction = 0 then
   				            strHours = strHours + DateDiff("n",Cdatetime,now())
                        end if
                    END IF
              END IF
                intUserID = rs("UserID")
                Caction = rs("Caction")
                Cdatetime =  rs("Cdatetime")
	        RS.MoveNext
	        Loop
    	END IF
    END IF
	strTotalHrs = round(strHours/60,2)

	IF Session("intWashes") >0 and strTotalHrs > 0 then
        Session("intScore")=round((Session("intWashes")/strTotalHrs)*100,0)
    end if




    strSQL =" SELECT isnull(SUM(1),0) AS cnt FROM REC(NOLOCK)"&_
    " INNER JOIN RECITEM(NOLOCK) ON  REC.recid = RECITEM.recId"&_
    " INNER JOIN Product ON RECITEM.ProdID = Product.ProdID"&_
    " WHERE (DATEPART(Month, REC.CloseDte) = '" & Month(date()) & "')"&_
    " AND (DATEPART(Day, REC.CloseDte) = '" & day(date()) & "')"&_
    " AND (DATEPART(Year, REC.CloseDte) = '" & year(date()) & "')"&_
    " AND (REC.Status >= 70) AND (Product.cat = 2)"
     
 	If DBOpenRecordset(dbMain,rs,strSQL) Then
    	IF NOT rs.EOF then
    		Session("intDetails") = rs("cnt")	
    	END IF
    END IF


    strSQL = "{call stp_Washers}"     
    IF not DBExec(dbMain, strSQL) then
    	Response.Write gstrmsg
    	Response.end
    END IF
    strSQL = "exec stp_Labor 2"     
    IF not DBExec(dbMain, strSQL) then
    	Response.Write gstrmsg
    	Response.end
    END IF
    strSQL =" SELECT washers,detailers,Labor FROM stats(NOLOCK)"
 	If DBOpenRecordset(dbMain,rs,strSQL) Then
    	IF NOT rs.EOF then
    		Session("intWashers") = rs("washers")	
    		Session("intDetailers") = rs("detailers")	
    		Session("intLabor") = rs("Labor")	
    	END IF
    END IF

strLoginWelcome = GetLoginWelcome(dbMain)

tempMID = ""

'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<title></title>
<link rel="stylesheet" href="main.css" type="text/css" />
</head>
<body class="menu">
    <Input type="hidden" name="LocationID" value="<%=LocationID%>" />

<table border="0" align="left" cellspacing="0" width="110%" cellpadding="0>"
<tr>
	<td>
		<table border="0" align="left" cellspacing="0" width="100%" cellpadding="1">
		<tr>
		<td class="menupad">&nbsp;
		<% Call BuildMenu(dbMain) %>
		&nbsp;&nbsp;&nbsp;&nbsp;
		</td>
		<td class="menupad" align="right">
		<img width="18" height="18" style="cursor:hand" title="Click here to view page Info." OnCLick="Showinfo()" src="images/info.ico">
		</td>
		<td class="menupad" align="right" width="10%">
		&nbsp;
		</td>
		</tr>
		</table>
	</td>
</tr>
<tr>
	<td>
		<Table width="100%" height=40 border=0>
			<tr>
				<td valign="center" align=left>
					&nbsp;&nbsp;<label  id=lblPageTitle class="pageTitle"></label>
				</td>
				<td valign="center" align="center">
					&nbsp;&nbsp;Washes &nbsp;<label id="lblWashes" class="data"><%=Session("intWashes")%></label>&nbsp;
				</td>
				<td valign="center" align="center">
					&nbsp;&nbsp;Wash-Emp&nbsp;<label id="lblWashers" class="data"><%=Session("intWashers")%></label>&nbsp;
				</td>
				<td valign="center" align="center">
					&nbsp;&nbsp;Details &nbsp;<label id="lblDetails"  class="data"><%=Session("intDetails")%></label>&nbsp;
				</td>
				<td valign="center" align="center">
					&nbsp;&nbsp;Score &nbsp;
                    
<% IF cdbl(Session("intScore")) < 74 then %>           
    <label class="reddata"><%=Session("intScore")%></label>&nbsp;
<% ELSEIF cdbl(Session("intScore")) >= 75 and cdbl(Session("intScore")) <= 79 then %>
    <label  class="YellowData"><%=Session("intScore")%></label>&nbsp;
<% ELSEIF cdbl(Session("intScore")) >= 80 and cdbl(Session("intScore")) <= 85 then %>
    <label  class="Data"><%=Session("intScore")%></label>&nbsp;
<% ELSE %>
    <label  class="GRNData"><%=Session("intScore")%></label>&nbsp;
<% End IF %>

				</td>
				<td valign="center" align="center">
					<label id="lblDetailers" style="visibility:hidden"><%=Session("intDetailers")%></label>&nbsp;
				</td>


		<% IF cdbl(Session("intLabor")) > 0.0 then %>
				<td valign="center" align="center">
					&nbsp;&nbsp;P/L&nbsp;<label id="lblLabor"  class="data"><%=Session("intLabor")%></label>&nbsp;
				</td>
		<% Else %>
				<td valign="center" align="center">
					&nbsp;&nbsp;P/L&nbsp;<label id="lblLabor"  class="reddata"><%=Session("intLabor")%></label>&nbsp;
				</td>
		<% END IF %>
				<td valign="center" align="right">
					<label class="User" ><%=Session("UserName")%></label><br>
					<label class="User" ><a class="lnkbody" title="Click here to change location" href="admLocationSel.asp?mode=change" ><%=Session("LocationDesc")%></a></label>
				</td>
				<td  align="right" width="10%">
				&nbsp;
				</td>
			</tr>
		</table>
	</tr>
</tr>
</table>
<a href="" name=lnkLauncher style="show:hide"></a>
<OBJECT classid="CLSID:F5131C24-E56D-11CF-B78A-444553540000" codeBase="activex/ikcntrls.cab#version=1,0,0,9" 
	height=1 id=objMenu style="LEFT: 0px; TOP: 0px" width=1 VIEWASTEXT>
	<PARAM NAME="_Version" VALUE="65536">
	<PARAM NAME="_ExtentX" VALUE="26">
	<PARAM NAME="_ExtentY" VALUE="26">
	<PARAM NAME="_StockProps" VALUE="0"></OBJECT>
</body>
</html>

<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<script type="text/vbscript" language="VBSCRIPT">
Option Explicit
Dim dbMain, strSQL, intMenuLeft, intMenuTop, obj 
sub window_onload
	Top.document.frames("alert").location.href = "admalert.asp"
	Top.document.frames("body").location.href = "<%=strLoginWelcome%>"
	Call window.setInterval("ReloadAlert", 60000,"vbscript")
end sub

Sub ReloadAlert()
	Top.document.frames("alert").location.Reload
End Sub

Sub WriteTitle (sTitle)
	lblPageTitle.InnerText = sTitle	
End Sub
Sub WriteWashesData (sWashes)
	lblWashes.InnerText = sWashes	
End Sub
Sub WriteWashersData (sWashers)
	lblWashers.InnerText = sWashers	
End Sub
Sub WriteDetailsData (sDetails)
	lblDetails.InnerText = sDetails	
End Sub

Sub WriteDetailersData (sDetailers)
	lblDetailers.InnerText = sDetailers	
End Sub



Sub HandleMenuLink
	Call HighlightMenuPad
	window.focus
End Sub
Sub HighLightMenuPad
	Dim objMenuPad, obj

	Set objMenuPad = window.event.srcElement
	For Each obj in Document.all
		If obj.ClassName = "MenuPadSel" Then
			obj.ClassName ="menupad"
		End If
	Next
	objMenuPad.ClassName ="MenuPadSel"

End Sub
Sub objMenu_OnClick(id)
Dim strURL, intWidth, intHeight, intLbsPos, intATPos
	strURL = objMenu.GetItemValue(id)

	If Left(strURL, 1) = "~" Then
		 intATPos = Instr(strURL, "@")
		 intLbsPos = Instr(strURL, "#")

		 intHeight = Mid(strURL, 2, intATPos -2)
		 intWidth = Mid(strURL, intATPos +1, intLbsPos - intATPos - 1)
		     strURL = Mid(strURL, intLbsPos +1)
			 Call mmshowmod(strURL, intHeight, intWidth)
			 
	Else
		lnkLauncher.href = Mid(strUrl,instr(strUrl,"#")+1)
		lnkLauncher.target = Mid(strUrl,2,Instr(strUrl,"#")-2)
		lnkLauncher.click()
	End If
End Sub

Sub ShowInfo()
	Dim arrUrl,strInfoUrl
	arrUrl = Split(Top.frames("Body").Location.PathName,"/")
	arrUrl(Ubound(arrUrl)) = "Info/Inf_" & ArrUrl(Ubound(arrUrl))
	strInfoUrl = Join(ArrUrl,"/")
	strInfoUrl = Replace(LCase(strInfoUrl),".asp",".htm")
	strInfoUrl="admInfoDlg.Asp?Url=" & strInfoUrl
	Call javamodal(strInfoUrl)
End Sub

<% Call BuildMenuClick(dbMain) %>
</script>
<script type="text/javascript" language="JavaScript">
	function javamodal(strURL)
	{	
		window.status = "";
		dialogLeft = screen.availWidth - 350;
  		strFeatures = "dialogWidth=350px;dialogHeight=550px;dialogTop=0;dialogLeft=" + dialogLeft 
				  + ";center=no;help=no;maximize=yes;minimize=yes;resizable=yes" 

  		strResult = window.showModalDialog(strURL + "&Title=" + escape(document.all("lblPageTitle").innerHTML), "MyDialog",strFeatures);	
	}

	function mmshowmod(strURL, intHeight, intWidth)
	{	
		window.status = "";
  		strFeatures = "dialogWidth=" + intWidth + "px;dialogHeight=" + intHeight + "px;scrollbars=no;"
				  + "center=yes;border=thin;help=no;status=no" 
  		strResult = window.showModalDialog(strURL, "MyDialog", strFeatures);	
		if (strResult)
		{
			NewURL = strResult
			location.href = NewURL
		}
	}
</script>
<%
Call CloseConnection(dbMain)

End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Sub BuildMenu(db)

Dim strSQL, rs, strCaption, FTarget,intMID
If  Session("ProfileID") <> 0 Then
	strSQL =" SELECT * FROM LM_vuBuildMenu1 " & _ 
			" WHERE Profileid=" & Session("ProfileID") & _
			" ORDER BY DisplayOrder"
Else
	strSQL =" SELECT * FROM LM_vuBuildMenuSA " & _ 
			" WHERE PID = 0 " & _
			" ORDER BY DisplayOrder"
End If			
If DBOpenRecordset(db,rs,strSQL) Then
	Do While Not RS.EOF 
	If intMID <> CLong(rs("MID")) Then
		intMID = CLong(rs("MID"))
	strCaption = RS("Caption")

	If Clong(RS("MType")) = 1 Then %>
	<a href="<%=RS("URL")%><%Call CheckParam(CLong(RS("MID")), db)%>" onclick="HandleMenuLink()" name="M<%=RS("MID")%>" target="<%=RS("Target")%>" class="menupad"><%=strCaption%>
	</a>&nbsp;&nbsp;&nbsp;&nbsp;
	<%Else%>
	<a href="#!" name="M<%=RS("MID")%>" onclick="MenuClick()" class="menupad"><%=strCaption%></a>&nbsp;&nbsp;&nbsp;&nbsp;
 <% End If
	End If
	RS.MoveNext
	Loop
End If

End Sub

'******************************************************
'This Builds the Client Side Function Called Menu Click
'******************************************************

Sub BuildMenuClick(db)

Dim strSQL, rs, strCaption,intMID
If Clong(Session("ProfileID")) <> 0 Then
	strSQL =" SELECT * FROM LM_vuBuildMenu1 " & _
			" WHERE Profileid=" & Session("ProfileID") & _
			" ORDER BY DisplayOrder"
Else
	strSQL =" SELECT * FROM LM_vuTopLevelMenusSA " & _
			" ORDER BY DisplayOrder"
End If
If DBOpenRecordset(db,rs,strSQL) Then %>
	Sub MenuClick()
	Dim intMenuLeft, intMenuTop, objMenuPad, obj,intMID
		Set objMenuPad = window.event.srcElement
		Call HighLightMenuPad
		intMenuLeft = objMenuPad.offsetLeft
		intMenuTop = objMenuPad.offsetTop + 17 - document.body.scrollTop
			objMenu.RemoveAllItems
			Select Case objMenuPad.Name

	<%	Do While Not RS.EOF 
		If intMID <> CLong(rs("MID")) Then
			intMID = CLong(rs("MID")) %>
			Case "M<%=RS("MID")%>" 
				  <% Call BuildMenuItems(CLong(RS("MID")), tempMID, db,1)%>

	<% 
		End If
		RS.MoveNext
		Loop 
	%>
			End Select
		Window.Focus
		Call objMenu.Popup (intMenuLeft, intMenuTop)
	End Sub
<%
End If
Response.Write gStrmsg
End Sub

'***************************************************
'This builds the pop-up menu items for the main menu
'***************************************************

Sub BuildMenuItems(iMID, tempMID, db, iLevel)
Dim strSQL, rs, strURL, intHeight, intWidth, strModalURL,tmpMID,intMID
 
tmpMID =tempMID
IF Clong(session("ProfileID")) <> 0 then
	strSQL =" SELECT * FROM LM_vuBuildMenu" & iLevel + 1 & _ 
			" WHERE Profileid=" & session("ProfileID") & _
			" AND Pid=" & iMID & _
			" ORDER BY DisplayOrder"
Else
	strSQL =" SELECT * FROM LM_vuBuildMenuSA " & _ 
			" WHERE Pid=" & iMID & _
			" ORDER BY DisplayOrder"
End If

If DBOpenRecordset(db,rs,strSQL) Then

Do While Not rs.EOF
If intMID <> CLong(rs("MID")) Then
	intMID = CLong(rs("MID"))
IF Clong(RS("CallType")) = 1 Then
	intHeight = RS("DlgHeight")
    strModalURL = RS("URL")
	intWidth = RS("DlgWidth")
	strURL = "~" & intHeight & "@" & intWidth & "#" & strModalURL & RS("Param")
Else
	strURL = "@" & rs("Target") & "#"  & RS("URL") & RS("Param")
End If 
	If Clong(RS("MType")) <> 0 Then 
	tmpMID = "" %>
		call objMenu.AddItem ("<%=RS("MID")%>", "<%=RS("Caption")%>", "<%=strURL%>", "<%=iMID%>")
  <%Else%>
		call objMenu.AddItem ("<%=RS("MID")%>", "<%=RS("Caption")%>", "", "<%=tempMID%>")
		<%	tmpMID = RS("MID")
		Call BuildMenuItems(CLong(RS("MID")),tmpMID, db, ilevel + 1)
	End If
End If
	RS.MoveNext
Loop
RS.Close
End If
End Sub

'****************************************************************
'If the menu items has Parameters his will add parameters to URL 
'****************************************************************

Function CheckParam(intMID, db)
Dim  rs, strSQL, strParam
Exit Function
strSQL = "SELECT * FROM LM_MenuParam WHERE MID =" & intMID
If DBOpenRecordset(db,rs,strSQL) Then
	strParam = ""
	Do While Not RS.EOF 
		strParam = strParam + RS("ParamName") + "=" + RS("ParamValue") + "&"
		RS.MoveNext
	Loop
End If
If strParam <> "" Then
	strParam = Left(strParam, Len(strParam) -1)
	strParam = "?" + strParam
	
End If
End Function

Function GetLoginWelcome(db)
	Dim strSQL,rs
	strSQL ="	SELECT	URL " _
		+	"	FROM	LM_URL" _
		+	"	WHERE URLID = 1"
	If dbOpenRecordSet(db,rs,strSQL) Then
		If Not RS.EOF Then
		 GetLoginWelcome = rs("URL")
		End If
	End If
	Set rs = NOTHING
End Function


Sub GetLocdata(db,iLocID)
	Dim strSQL,rs
	strSQL=" Select * FROM LM_vuUserLocation WHERE Userid=" & Session("UserID") & " AND Locationid= " & iLocID
	If DBOpenRecordset(db,rs,strSQL) Then
		Session("ProfileID") = Clong(rs("ProfileID"))
		Session("LocationID") = CLong(rs("LocationID"))
		Session("LocationDesc") =rs("LocationDesc")
	End If
	
End Sub
%>

