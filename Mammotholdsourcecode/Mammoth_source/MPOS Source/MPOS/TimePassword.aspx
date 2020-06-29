<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TimePassword.aspx.cs" Inherits="MPOS.TimePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 646px;
            height: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="ServerName" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LocationDesc" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LocationID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LoginID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="Role" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="UserID" runat="server" Visible="false"></asp:TextBox>
    <br />
    <br />
    <div style="text-align: center;">
        <div style="text-align: center; height: 140px; width: 100%">
            <img id="piclogo" src="images/MammothLogo.gif" style="width: 151px; height: 120px">
        </div>
        <div style="text-align: center; height: 70px; width: 100%; vertical-align: middle">
            <asp:Label runat="server" class="control" Style="font: bold 18px arial; vertical-align: middle;" Text="Password:"></asp:Label>&nbsp;&nbsp;&nbsp;
            <asp:TextBox runat="server" ID="tPassword" size="8" Style="font: bold 28px arial; vertical-align: middle;" ></asp:TextBox>&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" Style="font: bold 18px arial; vertical-align: middle; width: 100px; height: 60px" OnClick="LoginBtnClicked" ID="logon" Text="Enter" />
        </div>
        <table style="width: 100%">
            <tr style="text-align: center">
                <td style="width: 25%">&nbsp;</td>
                <td style="width: 600px">

                    <table style="border-collapse: separate; padding: 10px;" border="0" class="auto-style1">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="button1" Text="1" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="button2" Text="2" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="button3" Text="3" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="button4" Text="4" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="button5" Text="5" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="button6" Text="6" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="button7" Text="7" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="button8" Text="8" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="button9" Text="9" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="button0" Text="0" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonQ" Text="Q" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonW" Text="W" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonE" Text="E" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonR" Text="R" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonT" Text="T" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonY" Text="Y" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonU" Text="U" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonI" Text="I" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonO" Text="O" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonP" Text="P" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonA" Text="A" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonS" Text="S" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonD" Text="D" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonF" Text="F" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonG" Text="G" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonH" Text="H" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonJ" Text="J" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonK" Text="K" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonL" Text="L" />
                            </td>
                            <td style="text-align: center">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonZ" Text="Z" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonX" Text="X" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonC" Text="C" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonV" Text="V" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonB" Text="B" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonN" Text="N" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="NumberBtn_Clicked" ID="buttonM" Text="M" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="DeleteBtn_Clicked" ID="buttonDel" Text="DEL" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button runat="server" Style="height: 60px; width: 60px; font: bold 18px arial; text-align: center" OnClick="ClearBtn_Clicked" ID="buttonClr" Text="CLR" />
                            </td>
                            <td style="text-align: center">&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 25%">&nbsp;</td>
            </tr>

        </table>
    </div>
</asp:Content>
