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

Dim dbMain, blnLoginFailed,hdnRecID,strSQL
Set dbMain =  OpenConnection

If Session("UserID") = "" Then
	Response.clear
	Response.redirect "CEDefault.asp"
End If
IF Len(Request("Logout"))>0 then
	Response.redirect "CEDefault.asp?loginid=&Password="
End if

IF Request("FormAction")="Checkout" then
	'Response.redirect "CEmenu2.asp?intrecID="&Request("hdnRecID")
	    strSQL= "	UPDATE Rec Set " & _
			    "	QABy=" & session("userid") &","& _
				" DateOUT='" & now() &"'"& _
			    "	WHERE recID=" & Request("hdnRecID") &_
				" AND LocationID=" & Application("LocationID") 
	    If NOT (dbExec(dbMain,strSQL)) Then
		    Response.Write gstrMsg
	    End If


	    strSQL= " exec stp_getCurrWaitTime " & Application("LocationID") 
	    If NOT (dbExec(dbMain,strSQL)) Then
		    Response.Write gstrMsg
	    End If



End if

strSQL= " update rec set esttime= dateadd(n,30,datein) WHERE  esttime is null AND (rec.LocationID = "& Application("LocationID") &")"
If NOT (dbExec(dbMain,strSQL)) Then
    Response.Write gstrMsg
End If




'********************************************************************
' HTML
'********************************************************************
%>
<html>
<head>
<meta name="viewport" content="width=device-width" /> 
<title>Mammoth Check Out</title>
<link rel="stylesheet" href="main.css" type="text/css" />
</head>
<body class="pgBody"  onload="JavaScript:timedRefresh(10000);">
<form action="CEmenu1.asp" method="post" name="frmMain">
<input type="hidden" name="FormAction" tabindex="-2" />
<input type="hidden" name="hdnRecID" tabindex="-2" />
<div style="text-align:center">
    <table style="width:100%; border-collapse:collapse" class="data">
<%

	DIM rsData,strSQL1,rsData1,cnt,strClass
    cnt = 0
	strSQL = " SELECT REC.recid, REC.datein, REC.esttime , REC.vehID,"&_
	        " ISNULL(CASE WHEN PATINDEX('%-%', client.fname) > 0 THEN LEFT(client.fname, PATINDEX('%-%', client.fname) - 1) ELSE client.fname END + ' ' + client.lname, 'DRIVE UP') AS clientname,"&_
	        " ISNULL(LM_ListItem.ListDesc, 'Unk') AS color, ISNULL(REC.vmodel, 'Unk') "&_
	        " AS model, ISNULL(LM_ListItem_1.ListDesc, 'Unk') AS make,"&_
	        " CASE WHEN PATINDEX('%(%', LM_ListItem_2.ListDesc) > 0 THEN substring( LM_ListItem_2.ListDesc, PATINDEX('%(%', LM_ListItem_2.ListDesc),3) ELSE '' END AS size, "&_
	        " Product.Descript, REC.Status,isnull((SELECT distinct isnull(RI2.recId,0) as Options FROM dbo.RECITEM RI2 INNER JOIN "&_
            " dbo.Product P2 ON RI2.ProdID = P2.ProdID WHERE (RI2.recId = Rec.Recid) AND ((p2.cat = 3) or (p2.cat = 22)) AND (RI2.ProdID <> 35)),0) as Options"&_
	        " FROM  REC "&_
            " INNER JOIN RECITEM ON REC.recid = RECITEM.recId AND (RECITEM.LocationID = "& Application("LocationID") &") "&_
            " INNER JOIN Product ON RECITEM.ProdID = Product.ProdID "&_
	        " LEFT OUTER JOIN LM_ListItem WITH (nolock)"&_
	        " INNER JOIN vehical ON LM_ListItem.ListType = 5"&_
	        " INNER JOIN LM_ListItem AS LM_ListItem_1 ON LM_ListItem_1.ListType = 3"&_
	        " INNER JOIN LM_ListItem AS LM_ListItem_2 ON LM_ListItem_2.ListType = 4 "&_
	        " AND vehical.model = LM_ListItem_2.ListValue ON REC.VehColor = LM_ListItem.ListValue "&_
	        " AND REC.VehMan = LM_ListItem_1.ListValue "&_
	        " AND REC.vehID = vehical.vehid "&_
	        " LEFT OUTER JOIN client ON REC.clientid = client.clientid"&_
	        " WHERE (REC.dateout IS NULL) AND (Product.cat = 1) AND (REC.LocationID = "& Application("LocationID") &") order by REC.recid"



	If dbopenrecordset(dbMain,rsData,strSQL)  Then
		IF NOT 	rsData.EOF then
			Do while Not rsData.EOF
    

            'IF Cnt = 2 then
            '        cnt = 0
            '    END IF
                If rsData("Options") = 0 then
                    strClass = "Data1"
                Else
                    strClass = "Data2"
                END IF
            
				%>
                <tr>
                    <td style="width:80%">
<table  style="width:100%; border-collapse:collapse" >
    				<tr>
				    <td class="<%=strClass%>" style="width:20%; text-align:left"><label class="control" >&nbsp;<%=rsData("recid")%>&nbsp;</label></td>
				    <td class="<%=strClass%>" style="width:80%; text-align:left"><label class="control" >&nbsp;<%=trim(rsData("clientname")) %></label></td>
				</tr>
				<tr>
				    <td class="<%=strClass%>" style="width:20%; text-align:left"><label class="control" >&nbsp;<%=formatdatetime(rsData("datein"),4)%>&nbsp;</label></td>
				    <td class="<%=strClass%>" style="width:80%; text-align:left"><label class="control" >&nbsp;<%=trim(rsData("color"))%>&nbsp;<%=trim(rsData("make"))%>&nbsp;<%=trim(rsData("model"))%>&nbsp;<%=trim(rsData("size"))%></label></td>
				</tr>
				<tr>
				    <td class="<%=strClass%>" style="width:20%; text-align:left"><label class="control" >&nbsp;<%=formatdatetime(rsData("esttime"),4)%>&nbsp;</label></td>
				    <td class="<%=strClass%>" style="width:80%; text-align:left"><label class="control" >&nbsp;<%=trim(rsData("Descript"))%></label></td>
				</tr>


</table>

                    </td>
   
                    <% if rsData("Status") = 70 then %>
                    <td class="<%=strClass%>" style="text-align:center; background-color:green; color:yellow;height:44px; width:20%"><b><a href="CEmenu1.asp?hdnRecID=<%=rsData("recid")%>&FormAction=Checkout"><Label OnClick="checkout('<%=rsData("recid")%>')">Check Out</Label></a></b></td>
                    <% else %>
                    <td class="<%=strClass%>" style="text-align:center; background-color:red; color:black;height:44px; width:20%"><b><a href="CEmenu1.asp?hdnRecID=<%=rsData("recid")%>&FormAction=Checkout"><Label OnClick="checkout('<%=rsData("recid")%>')">Check Out</Label></a></b></td>
                    <% end if %>

                </tr>
        <tr>
            <td style="height:3px; background-color:blue" colspan="2">
                
            </td>
        </tr>
				<%
				'cnt = cnt+1
				rsData.MoveNext
			Loop	
		END IF
	END IF
	Set rsData = Nothing

%>

</table>
<hr />
<table  style="width:100%; border-collapse:collapse" class="data">
   <tr>
     <td style="text-align:center">
			<input type="submit" name="Logout" value="Logout" />
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
<script type="text/vbscript">

Option Explicit


Sub checkout(recID)
	document.all("hdnRecID").value =  recID
	document.all("FormAction").value =  "Checkout"
	frmMain.Submit()
End Sub

</script>
<script type="text/JavaScript">
    function timedRefresh(timeoutPeriod) {
        setTimeout("window.location=window.location;", timeoutPeriod);
        //setTimeout("location.reload(true);", timeoutPeriod);
        //setTimeout("window.opener.document.forms[0].submit();", timeoutPeriod);
        
        //window.resizeTo(100, 300);
    }
</script>

<%

Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
%>

