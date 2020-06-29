<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WeeklySales.aspx.cs" Inherits="MPOS.WeeklySales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:TextBox ID="ServerName" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LocationDesc" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LocationID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LoginID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="Role" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="RedirectUrl" runat="server" Visible="false"></asp:TextBox>
     <iframe name="body" style="width:100%; height:600px; " scrolling="no" seamless="seamless" src="<%=RedirectUrl.Text%>rptWeekly.asp?LocationID=<%=LocationID.Text %>"></iframe>
</asp:Content>
