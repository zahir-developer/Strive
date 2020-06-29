<%@  language="VBSCRIPT" %>
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
Dim intRecItemID,hdnStatus,LocationID,LoginID,intRECID
intRECID = request("intRECID")
intRecItemID = request("intRecItemID")
LocationID = request("LocationID")
LoginID = request("LoginID")
%>
<html>
<head>
    <link rel="stylesheet" href="main.css" type="text/css">
    <title>Edit Item</title>
</head>
<body class="pgbody">
    <input type="hidden" name="intRecItemID" value="<%=intRecItemID%>" />
    <div style="text-align: center">
        <iframe name="fraMain" src="CashOutEditFra.asp?intRecItemID=<%=intRecItemID%>&intRECID=<%=intRECID%>&LocationID=<%=LocationID%>&LoginID=<%=LoginID%>" style="height:220px; width:350px" frameborder="0"></iframe>
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
