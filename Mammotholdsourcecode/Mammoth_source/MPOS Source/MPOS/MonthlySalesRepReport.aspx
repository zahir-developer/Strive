﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonthlySalesRepReport.aspx.cs" Inherits="MPOS.MonthlySalesRepReport" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=11.1.17.503, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:TextBox ID="ServerName" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LocationDesc" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LocationID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LoginID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="Role" runat="server" Visible="false"></asp:TextBox>
   <telerik:ReportViewer ID="MonthlySalesReport" runat="server" ViewMode="PrintPreview" Width="100%" Height="1200px" BackColor="White">
        <typereportsource typename="MPOSReportLibrary.MonthlySales, MPOSReportLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null">
        </typereportsource>
    </telerik:ReportViewer>

</asp:Content>
