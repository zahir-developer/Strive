<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="MPOS.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:TextBox ID="ServerName" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LocationDesc" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LocationID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LoginID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="Role" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="RedirectUrl" runat="server" Visible="false"></asp:TextBox>
    <iframe name="body" style="width:100%; height:600px;"  seamless="seamless" src="<%=RedirectUrl.Text%>Sched.asp?LocationID=<%=LocationID.Text %>&LoginID=<%=LoginID.Text %>"></iframe>

</asp:Content>
