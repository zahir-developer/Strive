<%@ LANGUAGE="VBSCRIPT" %>
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
<%
'********************************************************************
' Global Variables
'********************************************************************
Dim Title,intAccAmt,intRecID,intCustAccID,LocationID,LoginID

intAccAmt=Request("intAccAmt")
intRecID=Request("intRecID")
intCustAccID=Request("intCustAccID")
LocationID = request("LocationID")
LoginID = request("LoginID")
%>
<html>
<head>
<link REL="stylesheet" HREF="main.css" TYPE="text/css"/>
<Title>Account Info</title>
</head>
<body class="pgbody">
<input type="hidden" name="intCustAccID" tabindex="-2" value="<%=intCustAccID%>"/>
<input type="hidden" name="intAccAmt" tabindex="-2" value="<%=intAccAmt%>"/>
<input type="hidden" name="intRecID" tabindex="-2" value="<%=intRecID%>"/>
<input type="hidden" name="LocationID" id="LocationID" value="<%=LocationID%>" />
<input type="hidden" name="LoginID" id="LoginID" value="<%=LoginID%>" />
<div style="text-align:center">

<iframe Name="fraMain" src="CashOutAccDlgFra.asp?intAccAmt=<%=intAccAmt%>&intCustAccID=<%=intCustAccID%>&intRecID=<%=intRecID%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>"   style="height:400px; width:100%" ></iframe>
</div>
</body>
</html>
<%
'********************************************************************
' Client-Side Functions
'********************************************************************
%>
<%
'********************************************************************
' Server-Side Functions
'********************************************************************
%>
