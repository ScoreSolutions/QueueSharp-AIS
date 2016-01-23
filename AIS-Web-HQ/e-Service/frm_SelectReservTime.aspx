<%@ Page Language="VB" MasterPageFile="~/e_Service_MasterPage.master" AutoEventWireup="false" CodeFile="frm_SelectReservTime.aspx.vb" Inherits="frm_SelectReservTime" title="Untitled Page" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 97%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                            </asp:ToolkitScriptManager>
                        </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                    <tr>
                        <td style="width: 126px">
                            <asp:ImageButton ID="img_tab1" runat="server" />
                        </td>
                        <td>
                            <asp:ImageButton ID="img_tab2" runat="server" style="margin-left: 0px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel5" runat="server" BackImageUrl="~/image/bg_2.png" 
                    Height="359px" Width="681px">
                    <table style="width:100%;">
                        <tr>
                            <td class="style1" style="width: 36px">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="lbl_message1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" style="width: 36px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style1" style="width: 36px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style1" style="width: 36px">
                                &nbsp;</td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" 
                                    style="width: 100%; height: 295px;">
                                    <tr>
                                        <td style="width: 73px">
                                            &nbsp;</td>
                                        <td style="width: 154px">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 73px">
                                            &nbsp;</td>
                                        <td style="width: 154px">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 73px">
                                            &nbsp;</td>
                                        <td style="width: 154px">
                                            <asp:ImageButton ID="img_return" runat="server" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="img_next" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

