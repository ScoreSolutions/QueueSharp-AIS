<%@ Page Language="VB" MasterPageFile="~/e_Service_MasterPage.master" AutoEventWireup="false"
    CodeFile="frm_Appointment_list.aspx.vb" Inherits="frm_Appointment_list" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <script language="javascript" type="text/javascript">
   function mouseOverImage_Tab() {
            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_tab2 = document.getElementById('<%= img_tab2.ClientID %>');

            if (txtPreferLang.value == 'Thai') {

                if (txtNetworkType.value == 'pre') {
                    img_tab2.src = "image/12call/tab2-green.png";
                } else if (txtNetworkType.value == 'post') {
                    img_tab2.src = "image/gsm/tab2-yellow.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_tab2.src = "image/corp/tab2-blue.png";
                } else if (txtNetworkType.value == '3g') {
                    img_tab2.src = "image/3g/tab2-green.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_tab2.src = "image/3gpost/tab2-green.png";
                }
            } else {

                if (txtNetworkType.value == 'pre') {
                    img_tab2.src = "image/12call/history-green.png";
                } else if (txtNetworkType.value == 'post') {
                    img_tab2.src = "image/gsm/history-yellow.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_tab2.src = "image/corp/history-blue.png";
                } else if (txtNetworkType.value == '3g') {
                    img_tab2.src = "image/3g/history-green.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_tab2.src = "image/3gpost/history-green.png";
                }
            }


        }

        function mouseOutImage_Tab() {

            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_tab2 = document.getElementById('<%= img_tab2.ClientID %>');

            if (txtPreferLang.value == 'Thai') {
                if (txtNetworkType.value == 'pre') {
                    img_tab2.src = "image/12call/tab2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_tab2.src = "image/gsm/tab2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_tab2.src = "image/corp/tab2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_tab2.src = "image/3g/tab2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_tab2.src = "image/3gpost/tab2.png";
                }
            } else {
                if (txtNetworkType.value == 'pre') {
                    img_tab2.src = "image/12call/history.png";
                } else if (txtNetworkType.value == 'post') {
                    img_tab2.src = "image/gsm/history.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_tab2.src = "image/corp/history.png";
                } else if (txtNetworkType.value == '3g') {
                    img_tab2.src = "image/3g/history.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_tab2.src = "image/3gpost/history.png";
                }
            }


        }

    </script>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 700px; height: 466px;">
        <tr>
            <td style="vertical-align: top;">
                <table style="width: 100%; height: 205px;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                            </asp:ToolkitScriptManager>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 350px;" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="padding-left: 10px; vertical-align: bottom; width: 50px;">
                                        <asp:ImageButton ID="img_tab1" runat="server" />
                                    </td>
                                    <td style="vertical-align: bottom; text-align: left;">
                                        <asp:ImageButton ID="img_tab2" runat="server"  onmouseover="mouseOverImage_Tab()"
                                            onmouseout="mouseOutImage_Tab()"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel5" runat="server" BackImageUrl="~/image/bg_2.png" Height="361px"
                                Width="683px">
                                <table style="width: 100%; height: 357px;" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 30px;">
                                            <asp:Label ID="lbl_message1" runat="server" Font-Bold="true" Font-Names="Tahoma"
                                                Font-Size="17px" ForeColor="Black"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 270px; vertical-align: top; padding-left: 30px;">
                                            <Saifi:MyDg ID="dgvdetail" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                                ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif" ImageNext="/imgs/bulletr.gif"
                                                ImagePrevious="/imgs/bulletl.gif" ShowFirstAndLastImage="False" ShowPreviousAndNextImage="False"
                                                Width="637px" CssClass="gridItem">
                                                <PagerStyle Mode="NumericPages" />
                                                <HeaderStyle CssClass="gridheader" />
                                                <Columns>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lbl_hd_tel" runat="server"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbl_tel" Text='<%#DataBinder.Eval(Container.DataItem, "tel")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lbl_hd_service" runat="server"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbl_service" Text='<%#DataBinder.Eval(Container.DataItem, "service")%>'
                                                                Visible="false"></asp:Label>
                                                            <asp:Label runat="server" ID="lbl_servicenm" Text='<%#DataBinder.Eval(Container.DataItem, "servicenm")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lbl_hd_shop" runat="server"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbl_shop" Text='<%#DataBinder.Eval(Container.DataItem, "shop")%>'></asp:Label>
                                                            <asp:Label runat="server" ID="lbl_shop_id" Text='<%#DataBinder.Eval(Container.DataItem, "shop_id")%>'
                                                                Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lbl_hd_date" runat="server"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbl_appdt" Text='<%#DataBinder.Eval(Container.DataItem, "appdt")%>'
                                                                Width="150px" Visible="false"></asp:Label>
                                                            <asp:Label runat="server" ID="lbl_date" Text='<%#DataBinder.Eval(Container.DataItem, "date")%>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lbl_hd_edit" runat="server"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="img_edit" runat="server" CommandName="Edit" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lbl_hd_cancel" runat="server"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="img_cancel" runat="server" CommandName="Cancel" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </Saifi:MyDg>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 30px;">
                                            <asp:Label ID="lbl_message2" runat="server" Font-Names="Tahoma" Font-Size="15px"
                                                ForeColor="#A3171E"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 30px;">
                                            <asp:Label ID="lbl_message3" runat="server" Font-Names="Tahoma" Font-Size="15px"
                                                ForeColor="#A3171E" Font-Italic="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
                                    <div style="display:none">
                                        <asp:TextBox ID="txtNetworkType" runat="server" Width="0px"></asp:TextBox>
                                         <asp:TextBox ID="txtPreferLang" runat="server" Width="0px"></asp:TextBox>
                                    </div>
</asp:Content>
