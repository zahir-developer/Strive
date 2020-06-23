<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TimeSet.aspx.cs" Inherits="MPOS.TimeSet" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .UserInfo {
            height: 60px;
            width: 120px;
            font: bold 18px arial;
        }

        TD.header {
            BORDER-RIGHT: 1px ridge;
            PADDING-RIGHT: 0px;
            BORDER-TOP: 1px ridge;
            PADDING-LEFT: 0px;
            PADDING-BOTTOM: 0px;
            MARGIN: 0px;
            FONT: 10pt verdana;
            BORDER-LEFT: 1px ridge;
            COLOR: #000;
            PADDING-TOP: 0px;
            BORDER-BOTTOM: 1px ridge;
            BACKGROUND-COLOR: #ccc;
        }

        TD.Data {
            BORDER-RIGHT: #999 1px solid;
            PADDING-RIGHT: 0px;
            PADDING-LEFT: 0px;
            PADDING-BOTTOM: 0px;
            MARGIN: 0px;
            FONT: 8pt verdana;
            PADDING-TOP: 0px;
            BORDER-BOTTOM: #999 1px solid;
            BACKGROUND-COLOR: #ffc;
        }

        TH.Data {
            BORDER-RIGHT: #999 1px solid;
            PADDING-RIGHT: 0px;
            PADDING-LEFT: 0px;
            PADDING-BOTTOM: 0px;
            MARGIN: 0px;
            FONT: 8pt verdana;
            PADDING-TOP: 0px;
            BORDER-BOTTOM: #999 1px solid;
            BACKGROUND-COLOR: #ffc;
        }

        .control:visited {
            FONT-WEIGHT: normal;
            FONT-SIZE: 10pt;
            COLOR: #000;
            FONT-FAMILY: Verdana;
            TEXT-DECORATION: none;
        }

        .control:link {
            FONT-WEIGHT: normal;
            FONT-SIZE: 10pt;
            COLOR: #000;
            FONT-FAMILY: Verdana;
            TEXT-DECORATION: none;
        }

        .control:active {
            FONT-WEIGHT: normal;
            FONT-SIZE: 10pt;
            COLOR: #000;
            FONT-FAMILY: Verdana;
            TEXT-DECORATION: none;
        }

        .control {
            FONT-WEIGHT: normal;
            FONT-SIZE: 10pt;
            COLOR: #000;
            FONT-FAMILY: Verdana;
            TEXT-DECORATION: none;
        }

        .auto-style4 {
            height: 207px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
         <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
   <asp:TextBox ID="ServerName" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LocationDesc" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LocationID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LoginID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="Role" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="UserID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="ClockStatus" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="intClockType" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="StartOfWeek" runat="server" Visible="false"></asp:TextBox>

    <table style="width: 100%">
        <tr>
            <td style="width: 20%; vertical-align: top">
                <table style="width: 100%; text-align: right">
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div runat="server" id="ClockInOutBtnDisplay">
                                <asp:Button runat="server" Style="font: bold 18px arial; vertical-align: middle; width: 160px; height: 60px" OnClick="ClockInOutBtnClicked" ID="SetTimeBtn" Text="Clock In/Out" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div runat="server" id="TimeCardBtnDisplay">
                                <asp:Button runat="server" Style="font: bold 18px arial; vertical-align: middle; width: 160px; height: 60px" OnClick="TimeCardBtnClicked" ID="TimeCardBtn" Text="Time Card" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div runat="server" id="DetailButtonDisplay">
                                <asp:Button runat="server" Style="font: bold 18px arial; vertical-align: middle; width: 160px; height: 60px" OnClick="DetailBtnClicked" ID="DetailBtn" Text="Detail" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" Style="font: bold 18px arial; vertical-align: middle; width: 160px; height: 60px" OnClick="ReturnBtnClicked" ID="ReturnBtn" Text="Return" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 80%; text-align: left">
                <table style="width: 700px; text-align: center">
                    <tr style="text-align: center">
                        <td style="width: 48%; text-align: right">
                            <asp:Label runat="server" Text="Current User:" CssClass="UserInfo"></asp:Label>
                        </td>
                        <td style="width: 2%; text-align: right">&nbsp;
                        </td>
                        <td style="width: 50%; text-align: left">
                            <asp:Label runat="server" ID="txtCurrentUser" CssClass="UserInfo"></asp:Label>
                        </td>
                    </tr>
                    <tr style="text-align: center">
                        <td style="width: 48%; text-align: right">
                            <asp:Label runat="server" Text="Current Status:" CssClass="UserInfo"></asp:Label>
                        </td>
                        <td style="width: 2%; text-align: right">&nbsp;
                        </td>
                        <td style="width: 50%; text-align: left">
                            <asp:Label runat="server" ID="txtCurrentStatus" CssClass="UserInfo"></asp:Label>
                        </td>
                    </tr>
                    <tr style="text-align: center">
                        <td style="width: 48%; text-align: right">
                            <asp:Label runat="server" Text="Current Date/Time:" CssClass="UserInfo"></asp:Label>
                        </td>
                        <td style="width: 2%; text-align: right">&nbsp;
                        </td>
                        <td style="width: 50%; text-align: left">
                            <asp:Label runat="server" ID="txtCurrentTime" CssClass="UserInfo"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="TimeSetInfo" style="height: 300px; visibility: visible">
                                <table style="width: 100%">
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 200px">
                                            <asp:Label runat="server" Text="As:" ID="ClockTypeLabel" Style="font: bold 18px arial; vertical-align: middle; width: 200px; height: 60px"></asp:Label>
                                        </td>
                                        <td style="text-align: center; width: 300px">
                                            <asp:DropDownList ID="ClockType" runat="server" Style="font: bold 28px arial; vertical-align: middle;">
                                                <asp:ListItem Text="WA-Wash" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="MG-Manager" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="CA-Cashier" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="RN-Runner" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="GB-Greet Bay" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="FB-Finish Bay" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="DT-Detailer" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="UN-Unknown" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left; width: 200px;">
                                            <asp:Button runat="server" Style="font: bold 18px arial; vertical-align: middle; width: 120px; height: 60px" OnClick="ClockBtnClicked" ID="ClockBtn" Text="Clock In" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div runat="server" id="TimeCardInfo" style="background-color: antiquewhite" class="auto-style4">
                                <table style="width: 700px;" border="0" class="Data">
                                    <tr>
                                        <td style="text-align: right; white-space: nowrap; width: 45%" class="control">Pay Period:&nbsp;&nbsp;</td>
                                        <td style="text-align: left; white-space: nowrap" class="control">
                                            <label runat="server" class="control" id="dtSun1"></label>
                                            &nbsp;&nbsp;to&nbsp;&nbsp;
                                            <label runat="server" class="control" id="dtSat1"></label>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 700px" border="1" class="Data">
                                    <tr>
                                        <td colspan="12" class="Header" style="text-align: center; width: 700px">Time Card</td>
                                    </tr>
                                    <tr>
                                        <td class="Header" style="text-align: center; width: 50px">Day</td>
                                        <td class="Header" style="text-align: center; width: 90px">Date</td>
                                        <td class="Header" style="text-align: center; width: 100px">In</td>
                                        <td class="Header" style="text-align: center; width: 100px">Out</td>
                                        <td class="Header" style="text-align: center; width: 100px">In</td>
                                        <td class="Header" style="text-align: center; width: 100px">Out</td>
                                        <td class="Header" style="text-align: center; width: 100px">In</td>
                                        <td class="Header" style="text-align: center; width: 100px">Out</td>
                                        <td class="Header" style="text-align: center; width: 100px">In</td>
                                        <td class="Header" style="text-align: center; width: 100px">Out</td>
                                        <td class="Header" style="text-align: center; width: 80px">Daily</td>
                                        <td class="Header" style="text-align: center; width: 80px">Totals</td>
                                    </tr>
                                    <tr>
                                        <td class="Header" style="text-align: left; width: 50px">Sun</td>
                                        <td class="data" style="text-align: left; width: 90px">
                                            <label runat="server" class="control" id="dtSun"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSunI1"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSunO1"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSunI2"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSunO2"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSunI3"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSunO3"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSunI4"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSunO4"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 80px">
                                            <label runat="server" class="control" id="strSunDA"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 80px">
                                            <label runat="server" class="control" id="strSunTO"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Header" style="text-align: left; width: 50px">Mon</td>
                                        <td class="data" style="text-align: left; width: 90px">
                                            <label runat="server" class="control" id="dtMon"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strMonI1"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strMonO1"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strMonI2"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strMonO2"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strMonI3"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strMonO3"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strMonI4"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strMonO4"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 80px">
                                            <label runat="server" class="control" id="strMonDA"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 80px">
                                            <label runat="server" class="control" id="strMonTO"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Header" style="text-align: left; width: 50px">Tue</td>
                                        <td class="data" style="text-align: left; width: 50px">
                                            <label runat="server" class="control" id="dtTue"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strTueI1"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strTueO1"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strTueI2"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strTueO2"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strTueI3"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strTueO3"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strTueI4"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strTueO4"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 80px">
                                            <label runat="server" class="control" id="strTueDA"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 80px">
                                            <label runat="server" class="control" id="strTueTO"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Header" style="text-align: left; width: 50px">Wed</td>
                                        <td class="data" style="text-align: left; width: 90px">
                                            <label runat="server" class="control" id="dtWed"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strWedI1"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strWedO1"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strWedI2"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strWedO2"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strWedI3"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strWedO3"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strWedI4"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strWedO4"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 80px">
                                            <label runat="server" class="control" id="strWedDA"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 80px">
                                            <label runat="server" class="control" id="strWedTO"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Header" style="text-align: left; width: 50px">Thu</td>
                                        <td class="data" style="text-align: left; width: 90px">
                                            <label runat="server" class="control" id="dtThu"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strThuI1"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strThuO1"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strThuI2"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strThuO2"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strThuI3"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strThuO3"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strThuI4"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strThuO4"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 80px">
                                            <label runat="server" class="control" id="strThuDA"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 80px">
                                            <label runat="server" class="control" id="strThuTO"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Header" style="text-align: left; width: 50px">Fri</td>
                                        <td class="data" style="text-align: left; width: 90px">
                                            <label runat="server" class="control" id="dtFri"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strFriI1"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strFriO1"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strFriI2"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strFriO2"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strFriI3"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strFriO3"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strFriI4"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strFriO4"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 80px">
                                            <label runat="server" class="control" id="strFriDA"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 80px">
                                            <label runat="server" class="control" id="strFriTO"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Header" style="text-align: left; width: 50px">Sat</td>
                                        <td class="data" style="text-align: left; width: 90px">
                                            <label runat="server" class="control" id="dtSat"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSatI1"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSatO1"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSatI2"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSatO2"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSatI3"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSatO3"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSatI4"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 100px">
                                            <label runat="server" class="control" id="strSatO4"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 80px">
                                            <label runat="server" class="control" id="strSatDA"></label>
                                        </td>
                                        <td class="data" style="text-align: right; width: 80px">
                                            <label runat="server" class="control" id="strSatTO"></label>
                                        </td>
                                    </tr>

                                </table>




                            </div>
                                 <div runat="server" id="TimeDetailInfo" >
                                <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="-1" DataSourceID="SqlDataSource1" GridLines="Both" Height="150px" Width="700px">
                                <SortingSettings EnableSkinSortStyles="False" />
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                </ClientSettings>
                                <MasterTableView AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Bay" DataType="System.String" HeaderText="Bay" ItemStyle-Width="20px" UniqueName="Bay"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TimeIn" DataType="System.String" HeaderText="Time In" ItemStyle-Width="130px" UniqueName="TimeIn"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Client" DataType="System.String" HeaderText="Client" ItemStyle-Width="120px" UniqueName="Client"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Vehical" DataType="System.String" HeaderText="Vehical" ItemStyle-Width="150px" UniqueName="Vehical"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TimeOut" DataType="System.String" HeaderText="Est. Out" ItemStyle-Width="130px" UniqueName="TimeOut"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Service" DataType="System.String" HeaderText="Service" ItemStyle-Width="150px" UniqueName="Service"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommandType="StoredProcedure" SelectCommand="stp_TimeDetails">
                                <SelectParameters>
                                    <asp:FormParameter FormField="LocationID" Name="LocationID" Type="Int32" />
                                    <asp:FormParameter FormField="UserID" Name="UserID" Type="Int32" />
                                    <asp:FormParameter FormField="StartOfWeek" Name="StartOfWeek" Type="DateTime" />
                                </SelectParameters>
                            </asp:SqlDataSource>
  
                                     


                                </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>







</asp:Content>
