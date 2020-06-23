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

Dim dbMain,strSQL,rsData, intrecID,strSQL2,cnt
dim intQAvalue,lastQAType
Set dbMain =  OpenConnection

If Session("UserID") = "" Then
	Response.clear
	Response.redirect "CEDefault.asp"
End If


intrecID = Request("intrecID")

strSQL= " DELETE FROM RECQA WHERE  (RECQA.recid = "& intrecID &")  AND (RECQA.LocationID = "& Application("LocationID") &")"
If NOT (dbExec(dbMain,strSQL)) Then
    Response.Write gstrMsg
End If
cnt = 1

strSQL= " SELECT  ListDesc, ItemOrder"&_
        " FROM LM_ListItem with (nolock) WHERE (ListType = 19) AND (Active = 1) ORDER BY ItemOrder"
If DBOpenRecordset(dbMain,rsData,strSQL) Then
	IF not rsData.eof then
		DO WHile NOT rsData.eof
	        strSQL2= " Insert into RECQA(LocationID,recID,QADesc,QAOrder,QAType)Values(" & _
			        Application("LocationID") & ", " & _
			        intrecID & ", " & _
			        "'" & rsData("ListDesc") & "', " & _
			        rsData("ItemOrder") & ",0)" 
	        If NOT (dbExec(dbMain,strSQL2)) Then
		        Response.Write gstrMsg
	        End If
			cnt=cnt+1
			rsData.MoveNext
		Loop
	END IF
End If

strSQL= " SELECT Product.Descript FROM RECITEM with (nolock)"&_
        " INNER JOIN Product ON RECITEM.ProdID = Product.ProdID"&_
        " WHERE (RECITEM.recId = "& intrecID &") AND (RECITEM.LocationID = "& Application("LocationID") &") AND (Product.cat = 3)"
If DBOpenRecordset(dbMain,rsData,strSQL) Then
	IF not rsData.eof then
		DO WHile NOT rsData.eof
	        strSQL2= " Insert into RECQA(LocationID,recID,QADesc,QAOrder,QAType)Values(" & _
			        Application("LocationID") & ", " & _
			        intrecID & ", " & _
			        "'" & rsData("Descript") & "', " & _
			        cnt & ",1)" 
	        If NOT (dbExec(dbMain,strSQL2)) Then
		        Response.Write gstrMsg
	        End If
			cnt=cnt+1
			rsData.MoveNext
		Loop
	END IF
End If


IF Len(Request("Nextbutton"))>0 then
	Call DoNext(dbMain,intrecID,cnt)
End if
IF Len(Request("Backbutton"))>0 then
	Call DoBack(dbMain)
End if

'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<title>Mammoth Check Out</title>
<meta name="viewport" content="width=device-width" /> 
<link rel="stylesheet" href="main.css" type="text/css" />
<style type="text/css">
input.largerCheckbox
{
width: 30px;
height: 30px;
}
</style>
</head>
<body class="pgBody">
<form action="CEmenu2.asp" method="post" id="frmMain">
<input type="hidden" name="intRECID" value="<%=intRECID%>" />
<input type="hidden" name="FormAction" tabindex="-2" />
<div id="divMain" class="show">
<center>
    <table cellspacing="0" width="100%" class="data">
<%
	strSQL = " SELECT REC.recid, REC.datein, REC.esttime, REC.vehID,"&_
	        " ISNULL(CASE WHEN PATINDEX('%-%', client.fname) > 0 THEN LEFT(client.fname, PATINDEX('%-%', client.fname) - 1) ELSE client.fname END + ' ' + client.lname, 'DRIVE UP') AS clientname,"&_
	        " ISNULL(LM_ListItem.ListDesc, 'Unk') AS color, ISNULL(vehical.vmodel, 'Unk') "&_
	        " AS model, ISNULL(LM_ListItem_1.ListDesc, 'Unk') AS make,"&_
	        " CASE WHEN PATINDEX('%(%', LM_ListItem_2.ListDesc) > 0 THEN substring( LM_ListItem_2.ListDesc, PATINDEX('%(%', LM_ListItem_2.ListDesc),3) ELSE '' END AS size, "&_
	        " Product.Descript"&_
	        " FROM  REC WITH (nolock) "&_
            " INNER JOIN RECITEM WITH (nolock) ON REC.recid = RECITEM.recId AND (RECITEM.LocationID = "& Application("LocationID") &")"&_
            " INNER JOIN Product WITH (nolock) ON RECITEM.ProdID = Product.ProdID "&_
	        " LEFT OUTER JOIN LM_ListItem WITH (nolock)"&_
	        " INNER JOIN vehical WITH (nolock) ON LM_ListItem.ListType = 5"&_
	        " INNER JOIN LM_ListItem AS LM_ListItem_1 WITH (nolock) ON LM_ListItem_1.ListType = 3"&_
	        " INNER JOIN LM_ListItem AS LM_ListItem_2 WITH (nolock) ON LM_ListItem_2.ListType = 4 "&_
	        " AND vehical.model = LM_ListItem_2.ListValue ON REC.VehColor = LM_ListItem.ListValue "&_
	        " AND REC.VehMan = LM_ListItem_1.ListValue "&_
	        " AND REC.vehID = vehical.vehid "&_
	        " LEFT OUTER JOIN client WITH (nolock) ON REC.clientid = client.clientid"&_
	        " WHERE (REC.recid="& intRECID &") AND (REC.LocationID = "& Application("LocationID") &")"
	If dbopenrecordset(dbMain,rsData,strSQL)  Then
		IF NOT rsData.EOF then
				%>
				<tr>
				    <td class="Data1" width="20%" align="left"><label class="control">&nbsp;<%=rsData("recid")%>&nbsp;</label></td>
				    <td class="Data1" width="80%"  align="left"><label class="control">&nbsp;<%=trim(rsData("clientname")) %></label></td>
				</tr>
				<tr>
				    <td class="Data0" width="20%" align="left"><label class="control">&nbsp;<%=formatdatetime(rsData("datein"),4)%>&nbsp;</label></td>
				    <td class="Data0" width="80%"  align="left"><label class="control">&nbsp;<%=trim(rsData("color"))%>&nbsp;<%=trim(rsData("make"))%>&nbsp;<%=trim(rsData("model"))%>&nbsp;<%=trim(rsData("size"))%></label></td>
				</tr>
				<tr>
				    <td class="Data1" width="20%" align="left"><label class="control">&nbsp;<%=formatdatetime(rsData("esttime"),4)%>&nbsp;</label></td>
				    <td class="Data1" width="80%"  align="left"><label class="control">&nbsp;<%=trim(rsData("Descript"))%></label></td>
				</tr>
                
				<%
		END IF
	END IF
	Set rsData = Nothing
%>
</table>
<table cellspacing="0" width="100%" class="data">
<%
	strSQL = " SELECT RECID, QADesc, QAvalue, QAOrder, QAType"&_
	         " FROM RECQA with (nolock) WHERE (RECID = "& intRECID &") AND (RECQA.LocationID = "& Application("LocationID") &") order by QAOrder"
	If dbopenrecordset(dbMain,rsData,strSQL)  Then
		IF NOT 	rsData.EOF then
			Do while Not rsData.EOF
                intQAvalue =rsData("QAvalue")

                IF rsData("QAType") = 1 and lastQAType = 0 then
				%>
	<tr>
<td colspan="2" align="center"><label class="data">*** Extra Services ***</label></td>
  	</tr>
              
                <% END IF %>
	<tr>

			<td><input name="checkbox<%=rsData("QAOrder")%>"  class="largerCheckbox" type="checkbox" 
				<%If len(intQAvalue) > 0 Then%>
					checked
				<%End If%> />
				<label class="data"><%=rsData("QADesc")%></label></td>


	</tr>
				<%
				lastQAType = rsData("QAType")
				rsData.MoveNext
			Loop	
		END IF
	END IF
	Set rsData = Nothing
%>
</table>
<table border="0" width="100%" cellspacing="1" cellpadding="1">
   <tr>
      <td align="center" colspan="3">
			<input type="submit" name="Backbutton" value="Cancel" />
			<input type="submit" name="Nextbutton" value="Next" />
		</td>
	</tr>		
</table>
</center>
</div>
</form>
</body>
</html>

<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>


<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
sub checkbrowser()
	If Instr(Request.ServerVariables("HTTP_User_Agent"),"Windows CE")= 0 then
		Response.Clear
		Response.Redirect "Default.asp"
	End If

End Sub

Sub DoBack(db)
	Response.Clear
	Response.Redirect "CEmenu1.asp"
End Sub


Sub DoNext(dbMain,intrecID,cnt)
 Dim strSQL,rsData,strval,intval
 Dim i
    For i = 1 to cnt
        strval = "checkbox"+cstr(i)
        IF len(trim(request(strval)))>0 then
            intval = 1
        ELSE
            intval = 0
        END IF
		strSQL= " UPDATE RECQA Set " & _
				" QAvalue=" & intval & _
				" WHERE recID=" & intrecID &_
				" AND LocationID=" & Application("LocationID") &_
				" AND QAOrder =" & i
		If NOT (dbExec(dbMain,strSQL)) Then
			Response.Write gstrMsg
		End If
   
    Next 

	Response.Clear
	Response.Redirect "CEmenu3.asp?intrecID="&intrecID
End Sub

%>

