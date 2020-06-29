<%@  language="VBSCRIPT" %>
<%
'********************************************************************
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
	Dim dbMain,strSQL,rsData,strImage,dDate,LocationID,LoginID
    
    Set dbMain =  OpenConnection
	dDate = date()
    LocationID = request("LocationID")
    LoginID = request("LoginID")

    'Response.Write("<br>")
    'Response.Write("Location Id is..."+LocationID)
     'Response.Write("LoginID Id is..."+LoginID)
   ' Response.End()

'********************************************************************
' HTML
'********************************************************************<%

%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css" />
    <title></title>
</head>
<body class="pgbody">
    <form action="wash.asp" method="get" id="form1" name="form1">
        <div style="text-align: center">
            <input type="hidden" name="dDate" value="<%=dDate%>" />
            <input type="hidden" name="LocationID" value="<%=LocationID%>" />
            <input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
            <%
    strSQL = "SELECT ProStat.Washes, ProStat.Washers, ProStat.Details, Case When ProStat.TotalHours = 0 OR ProStat.Washes=0 then 0 else  CAST(ROUND(ProStat.Washes / ProStat.TotalHours * 100, 0) AS int) end AS Score, isnull(ProStat.PerRev,0.0) as PerRev, stats.CurrentWaitTime FROM dbo.ProStat INNER JOIN dbo.stats ON dbo.ProStat.LocationID = dbo.stats.LocationID WHERE (ProStat.ProDate = '"& Right("00"+cstr(Month(date())),2)+Right("00"+cstr(Day(date())),2)+Right(cstr(year(date())),2) &"') AND (ProStat.LocationID = "& LocationID &")"
			
                'Response.write("db open starts<br>")
                
                
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
                else
                'Response.write("zero records")
                end if
            end if
            %>



            <!--<iframe name="Washfra1" src="admLoading.asp"></iframe>-->
            <iframe id="Washfra1" name="Washfra1" src="admLoading.asp" style="height: 400px ;width: 100%"></iframe>
            <!--		<td>
<iframe name="Washfra2" src="admLoading.asp" scrolling="yes" height="500px" width="276" frameborder="0"></iframe>
		</td>
		<td>
<iframe name="Washfra3" src="admLoading.asp" scrolling="yes" height="500px" width="276" frameborder="0"></iframe>
		</td>-->
        </div>

    </form>
</body>
</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>

<script type="text/VBScript">
Option Explicit
Sub Window_Onload()
	Washfra1.location.href = "WashFra1.asp?dtDate="& form1.dDate.value &"&LocationID=" & form1.LocationID.value &"&LoginID=" & form1.LoginID.value
	'Washfra2.location.href = "Washfra2.asp?dtDate="& form1.dDate.value &"&LocationID=" & form1.LocationID.value &"&LoginID=" & form1.LoginID.value
	'Washfra3.location.href = "Washfra3.asp?dtDate="& form1.dDate.value &"&LocationID=" & form1.LocationID.value &"&LoginID=" & form1.LoginID.value
End Sub 


</script>

<script type="text/javascript">
    window.onload = function () {
        var url = "/WashFra1.asp?dtDate=" + form1.dDate.value + "&LocationID=" + form1.LocationID.value + "&LoginID=" + form1.LoginID.value;
        document.getElementById("Washfra1").src = url;
       }

</script>
<%
Call CloseConnection(dbMain)
End Sub
'********************************************************************
' Server-Side Functions
'********************************************************************
%>