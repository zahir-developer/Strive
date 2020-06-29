<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TimeClockMaintenance.aspx.cs" Inherits="MPOS.TimeClockMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="~/Content/bootstrap.min.css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    <asp:TextBox ID="ServerName" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LocationDesc" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LocationID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="SheetID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="UserID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="WeekOf" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LoginID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="Role" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="RedirectUrl" runat="server" Visible="false"></asp:TextBox>

    <!--<iframe name="body" style="width:100%; height:600px;"    seamless="seamless" src="<%=RedirectUrl.Text%>AdmTime.asp?LocationID=<%=LocationID.Text %>&LoginID=<%=LoginID.Text %>"></iframe>-->

    <table style="width: 100%; padding:10px 10px 10px 10px ">
        <tr>
            <td style="width: 25%; text-align: center; vertical-align: top">
                <asp:Panel ID="Panel1" BorderStyle="None" HorizontalAlign="Center" runat="server">
                    <telerik:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource1" OnItemCommand="RadGrid1_ItemCommand">
                        <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                            <Selecting AllowRowSelect="true"></Selecting>
                            <Scrolling AllowScroll="false" UseStaticHeaders="True"></Scrolling>
                        </ClientSettings>
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView Width="100%"
                            DataKeyNames="SheetID, WeekOf" AutoGenerateColumns="false" AlternatingItemStyle-BackColor="#cccccc">
                            <Columns>
                                <telerik:GridBoundColumn AllowFiltering="false" DataField="WeekNo" UniqueName="WeekNo" HeaderText="Week #" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn AllowFiltering="false" DataField="WeekOf" UniqueName="WeekOf" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Week Of" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridDateTimeColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </asp:Panel>
            </td>
            <td style="width: 25%; text-align: center; vertical-align: central;">
                <asp:Panel ID="Panel2" BorderStyle="None" runat="server">
                    <asp:Label runat="server" ID="WeekOfLabel" Text=""></asp:Label>

                </asp:Panel>
            </td>
            <td style="width: 50%; text-align: center; vertical-align: top;">
                <asp:Panel ID="Panel3" BorderStyle="None" runat="server">
                <telerik:RadGrid ID="RadGrid2" Height="150px" runat="server" DataSourceID="SqlDataSource2" OnItemCommand="RadGrid2_ItemCommand">
                    <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true"></Selecting>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                    </ClientSettings>
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView Width="100%"
                        DataKeyNames="UserID, FirstName, LastName" AutoGenerateColumns="false" AlternatingItemStyle-BackColor="#cccccc">
                        <Columns>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="FirstName" UniqueName="FirstName" HeaderText="FirstName" HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="LastName" UniqueName="LastName" HeaderText="LastName" HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                    </asp:Panel>
            </td>
        </tr>
       <tr>
            <td colspan="3">
                <asp:Panel ID="Panel4" BorderStyle="None" Height="400px" runat="server">
                </asp:Panel>
            </td>

        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
        SelectCommand="tcm_weekSelect" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="LocationID" Name="LocationID" PropertyName="text"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
        SelectCommand="tcm_NameSelect" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="LocationID" Name="LocationID" PropertyName="text"
                Type="Int32" />
            <asp:ControlParameter ControlID="RadGrid1" DefaultValue="" Name="SheetID" PropertyName="SelectedValues['SheetID']"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
