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
<%
Call Main
Sub Main
Dim dbmain,hdnUserID,hdnSheetID,strSQL,rs,strcardname,strSalery,strDTotal,LocationID,LoginID
Set dbMain =  OpenConnection


hdnSheetID = Request("hdnSheetID")
hdnUserID = Request("hdnUserID")
LocationID = request("LocationID")
LoginID = request("LoginID")

strSQL =" SELECT FirstName + ' ' + LastName AS cardname,salery"&_
	" FROM LM_Users"&_
	" WHERE LM_Users.userid ="& hdnUserID 
IF dbOpenStaticRecordset(dbMain, rs, strSQL) then   
	IF NOT rs.EOF then
		strcardname = rs("cardname")
		IF isnull(rs("salery")) then
		strSalery = 0.00
		ELSE
		strSalery = rs("salery")
		END IF
	END IF
END IF
Set rs = Nothing
strSQL = " Select Sum(ComAmt) as DTotal from DetailComp (NOLOCK)"&_
		",timeSheet (NOLOCK)"&_
		" WHERE (TimeSheet.SheetID = "& hdnSheetID &")"&_
		" AND (DetailComp.CdateTime BETWEEN TimeSheet.weekof AND DATEADD(day, 7, TimeSheet.weekof))"&_
			" AND (DetailComp.UserID = "& hdnUserID &") and (DetailComp.LocationID=" & LocationID &")"&_
            " AND (TimeSheet.LocationID=" & LocationID &")"
If dbOpenRecordSet(dbmain,rs,strSQL) Then
	IF NOT rs.eof then
		strDTotal=rs(0)
	ELSE
		strDTotal=0.0
	END IF
End if
Set rs = Nothing


'********************************************************************
'HTML
'********************************************************************
%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css">
    <title></title>
</head>
<body class="pgbody">
    <form method="post" name="admTimeDetailFra" action="admTimeDetailFra.asp">
        <input type="hidden" name="hdnUserID" tabindex="-2" value="<%=hdnUserID%>"/>
        <input type="hidden" name="hdnSheetID" tabindex="-2" value="<%=hdnSheetID%>"/>
        <input type="hidden" name="strSalery" tabindex="-2" value="<%=strSalery%>"/>
        <input type="hidden" name="strDTotal" tabindex="-2" value="<%=strDTotal%>"/>
        <input type="hidden" name="LocationID" value="<%=LocationID%>" />
        <input type="hidden" name="LoginID" value="<%=LoginID%>" />
        <% IF isnull(hdnUserID) or len(hdnUserID)=0 then %>
        <table style="width: 100%; border-collapse: collapse;" class="Data">
            <tr>
                <td colspan="11" style="text-align: center;" class="data">No records Selected.</td>
            </tr>
        </table>
        <% Else %>
        <table style="width: 100%; border-collapse: collapse;" width="950">
            <tr>
                <td style="text-align: center;" class="control"><b><%=strcardname%></b></td>
            </tr>
        </table>
        <table style="width: 100%; border-collapse: collapse;" class="Data">
            <tr>
                <td class="Header" style="text-align: center; width: 40px">&nbsp;</td>
                <td class="Header" style="text-align: center; width: 40px">&nbsp;</td>
                <td class="Header" style="text-align: center; width: 60px">Date</td>
                <td class="Header" style="text-align: center; width: 50px">#</td>
                <td class="Header" style="text-align: center; width: 220px">Detail Package</td>
                <td class="Header" style="text-align: center; width: 100px">Color</td>
                <td class="Header" style="text-align: center; width: 100px">Make</td>
                <td class="Header" style="text-align: center; width: 120px">Model</td>
                <td class="Header" style="text-align: center; width: 60px">%</td>
                <td class="Header" style="text-align: center; width: 60px">Amount</td>
            </tr>
            <%=DoDataRow()%>
        </table>
        <% END IF %>
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

Sub Window_OnLoad()
	parent.document.all("lblDTotal").innerText = formatcurrency(cdbl(document.all("strDTotal").value),2)
	IF cdbl(document.all("strSalery").value) > 0.0 then
		parent.document.all("lblGTotal").innerText = formatCurrency(cdbl(parent.document.all("lblAdjAmt").innerText)+cdbl(parent.document.all("lblCollAmt").innerText)+cdbl(parent.document.all("lblUnifAmt").innerText) +cdbl(parent.document.all("lblTotal").innerText),2)
	ELSE
		parent.document.all("lblGTotal").innerText = formatCurrency(cdbl(parent.document.all("lblAdjAmt").innerText)+cdbl(parent.document.all("lblcollAmt").innerText)+cdbl(parent.document.all("lblUnifAmt").innerText) +cdbl(parent.document.all("lblTotal").innerText) + cdbl(parent.document.all("lblDTotal").innerText)+cdbl(parent.document.all("lblBonus").innerText),2)
	END IF
End Sub

Sub DeleteDc(hdnDetailCompID)
	Dim Answer,retDel
	window.event.cancelBubble = false
	Answer = MsgBox("Are you sure you want to Delete this Detail ?",276,"Confirm Cancel")
	If Answer = 6 then
		retDel= ShowModalDialog("admTimeDetailDelDlg.asp?intDetailCompID="& hdnDetailCompID &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:1px;dialogheight:1px;")
		parent.fraMain2.location.href = "admTimeDetailFra.asp?hdnSheetID=<%=Request("hdnSheetID")%>&hdnUserID=<%=Request("hdnUserID")%>&LocationID=<%=Request("LocationID")%>&LoginID=<%=Request("LoginID")%>"
	Else
		Exit Sub
	End if
	window.event.returnValue = False 
End Sub 

Sub EditRec(hdnRecID)
	Dim retDetailBy
	retDetailBy= ShowModalDialog("NewDetailByDlg.asp?intRecID="& hdnRecID &"&LocationID="& document.all("LocationID").value &"&LoginID="& document.all("LoginID").value ,"","dialogwidth:450px;dialogheight:300px;")
	'parent.location.href = "admTime.asp?hdnSheetID=<%=Request("hdnSheetID")%>&hdnUserID=<%=Request("hdnUserID")%> "
	parent.fraMain2.location.href = "admTimeDetailFra.asp?hdnSheetID=<%=Request("hdnSheetID")%>&hdnUserID=<%=Request("hdnUserID")%>&LocationID=<%=Request("LocationID")%>&LoginID=<%=Request("LoginID")%>"
End Sub 


</script>

<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
Function DoDataRow()
	Dim htmlDataRow,strDesc,hdnSheetID,LocationID
	Dim db,strSQL,rs,strLoc,intListCode,rowColor,hdnUserID
	Set db = OpenConnection

	hdnUserID = Request("hdnUserID")
	hdnSheetID = Request("hdnSheetID")
	LocationID = Request("LocationID")

               
	strSQL =" SELECT DetailComp.UserID, DetailComp.RecID,DetailComp.DetailCompID, "&_
			" DetailComp.CdateTime, DetailComp.ComAmt,DetailComp.ComPer, DetailComp.paid, "&_
			" Product.Descript, LM_ListItem.ListDesc AS make, "&_
			" LM_ListItem1.ListDesc AS model, LM_ListItem2.ListDesc AS color"&_
			" FROM Product"&_
			" INNER JOIN RECITEM ON Product.ProdID = RECITEM.ProdID"&_
			" AND (Product.Prodid = 6 OR Product.Prodid = 11 OR Product.Prodid = 12 OR Product.Prodid = 13 OR Product.Prodid = 14)"&_
			" FULL OUTER JOIN DetailComp"&_
			" INNER JOIN REC ON DetailComp.RecID = REC.recid ON RECITEM.recId = REC.recid and RECITEM.LocationID = REC.LocationID"&_
			" INNER JOIN LM_ListItem(Nolock) ON rec.vehMan = LM_ListItem.ListValue"&_
			" INNER JOIN LM_ListItem LM_ListItem1 ON rec.vehmod = LM_ListItem1.ListValue"&_
			" INNER JOIN LM_ListItem LM_ListItem2 ON rec.vehColor = LM_ListItem2.ListValue"&_
			" INNER JOIN TimeSheet ON DetailComp.LocationID = TimeSheet.LocationID "&_
			" WHERE (TimeSheet.SheetID = "& hdnSheetID &") and TimeSheet.LocationID=" & LocationID &" "&_
            " AND REC.LocationID=" & LocationID &" "&_
			" AND (DetailComp.CdateTime BETWEEN TimeSheet.weekof AND DATEADD(day, 7, TimeSheet.weekof))"&_
			" AND (DetailComp.UserID = "& hdnUserID &") and DetailComp.LocationID=" & LocationID &_
			" AND (LM_ListItem.ListType = 3)"&_
			" AND (LM_ListItem1.ListType = 4)"&_
			" AND (LM_ListItem2.ListType = 5)"&_
			" ORDER BY DetailComp.CdateTime"
'response.write strSQL
'    response.End	
    IF dbOpenStaticRecordset(db, rs, strSQL) then   
		IF NOT 	rs.EOF then
			Do while Not rs.EOF
				rowColor = "data"
				IF rs("paid") = 0 then
				htmlDataRow = htmlDataRow & "<tr><td width=40 class=header align=center onclick=""DeleteDC('"& rs("DetailCompID") &"')"" style=""cursor:hand"" ><u>Del</u></td>" 
				htmlDataRow = htmlDataRow & "<td width=40 class=header align=center onclick=""EditRec('"& rs("RECid") &"')"" style=""cursor:hand"" ><u>Edit</u></td>" 
				ELSE
				htmlDataRow = htmlDataRow & "<tr><td colspan=2 width=40 class=header align=center>Paid</td>" 
				END IF
				htmlDataRow = htmlDataRow & "<td  class="& rowColor &">&nbsp;" &  FormatDateTime(rs("Cdatetime"),vbshortdate) & "</td>" 
				htmlDataRow = htmlDataRow & "<td width=45 align=left class="& rowColor &">&nbsp;" & rs("RecID") & "</td>" 
				htmlDataRow = htmlDataRow & "<td  class="& rowColor &">&nbsp;" &  rs("Descript") & "</td>" 
				htmlDataRow = htmlDataRow & "<td  class="& rowColor &">&nbsp;" &  NullTest(rs("Color")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td  class="& rowColor &">&nbsp;" &  NullTest(rs("Make")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=left  class="& rowColor &">&nbsp;" & NullTest(rs("Model")) & "</td>" 
				htmlDataRow = htmlDataRow & "<td align=center class="& rowColor &">" & rs("ComPer") & "</td>"
				htmlDataRow = htmlDataRow & "<td align=center class="& rowColor &">" &formatCurrency(rs("ComAmt"),2) & "</td></tr>"
				rs.MoveNext
			Loop	
		ELSE
				htmlDataRow = "<tr><td colspan=11 align=""center"" Class=""data"">No records were found.</td></tr>"
		END IF
	ELSE
			htmlDataRow = "<tr><td colspan=11 align=""center"" Class=""data"">No records were found.</td></tr>"
	END IF
	DoDataRow = htmlDataRow
	Set RS = Nothing
	Call CloseConnection(db)
End Function


Function NullTest(var)
	If IsNull(var) then
		NullTest = "&nbsp;"
	Else
		If Trim(var) = "" Then
			NullTest ="&nbsp;"
		Else
			NullTest = var
		End If
	End If
End Function
Function NullZero(var)
	If IsNull(var) then
		NullZero = 0.0
	Else
		If Trim(var) = "" Then
			NullZero =0.0
		Else
			NullZero = var
		End If
	End If
End Function
%>
 

