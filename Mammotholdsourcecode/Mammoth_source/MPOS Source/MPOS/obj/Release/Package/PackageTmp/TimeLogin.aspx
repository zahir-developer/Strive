<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TimeLogin.aspx.cs" Inherits="MPOS.TimeLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="ServerName" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LocationDesc" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LocationID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LoginID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="Role" runat="server" Visible="false"></asp:TextBox>
    <br />
    <br />
    <div style="text-align: center;">
        <div style="text-align: center; height: 140px; width: 100%">
            <img id="piclogo" src="images/MammothLogo.gif" style="width: 151px; height: 120px">
        </div>
        <div style="text-align: center; height: 70px; width: 100%; vertical-align: middle">
            <asp:Label runat="server" class="control" Style="font: bold 18px arial; vertical-align: middle;" Text="Login:"></asp:Label>&nbsp;&nbsp;&nbsp;
            <asp:TextBox runat="server" ID="tUserName" size="8" Style="font: bold 28px arial; vertical-align: middle" Visible="true"></asp:TextBox>&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" Style="font: bold 18px arial; vertical-align: middle; width:100px;height:60px" OnClick="LoginBtnClicked" ID="logon" Text="Enter" />
        </div>
        <table style="width:100%">
            <tr style="text-align: center">
                <td style="width:50%">&nbsp;</td>
                <td style="width:220px" >

                    <table style="width: 220px;border-collapse:separate;padding:10px;height:300px" border="0" >
                        <tr>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial;text-align:center" OnClick="NumberBtn_Clicked" ID="button1" Text="1" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial;text-align:center" OnClick="NumberBtn_Clicked" ID="button2" Text="2" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial;text-align:center" OnClick="NumberBtn_Clicked" ID="button3" Text="3" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial;text-align:center" OnClick="NumberBtn_Clicked" ID="button4" Text="4" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial;text-align:center" OnClick="NumberBtn_Clicked" ID="button5" Text="5" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial;text-align:center" OnClick="NumberBtn_Clicked" ID="button6" Text="6" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial;text-align:center" OnClick="NumberBtn_Clicked" ID="button7" Text="7" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial;text-align:center" OnClick="NumberBtn_Clicked" ID="button8" Text="8" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial;text-align:center" OnClick="NumberBtn_Clicked" ID="button9" Text="9" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial;text-align:center" OnClick="DeleteBtn_Clicked" ID="buttonD" Text="DEL" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial;text-align:center" OnClick="NumberBtn_Clicked" ID="button0" Text="0" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial;text-align:center" OnClick="ClearBtn_Clicked" ID="buttonC" Text="CLR" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width:50%">&nbsp;</td>
            </tr>

        </table>
    </div>

</asp:Content>
