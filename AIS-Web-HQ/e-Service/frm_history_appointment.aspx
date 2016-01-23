<%@ Page Language="VB" MasterPageFile="~/e_Service_MasterPage.master" AutoEventWireup="false"
    CodeFile="frm_history_appointment.aspx.vb" Inherits="frm_history_appointment"
    Title="Untitled Page" %>

<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
      function mouseOverImage_Tab() {
            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_tab1 = document.getElementById('<%= img_tab1.ClientID %>');

            if (txtPreferLang.value == 'Thai') {

                if (txtNetworkType.value == 'pre') {
                    img_tab1.src = "image/12call/tab1.png";
                } else if (txtNetworkType.value == 'post') {
                    img_tab1.src = "image/gsm/tab1.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_tab1.src = "image/corp/tab1.png";
                } else if (txtNetworkType.value == '3g') {
                    img_tab1.src = "image/3g/tab1.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_tab1.src = "image/3gpost/tab1.png";
                }
            } else {

                if (txtNetworkType.value == 'pre') {
                    img_tab1.src = "image/12call/tab1_eng.png";
                } else if (txtNetworkType.value == 'post') {
                    img_tab1.src = "image/gsm/tab1_eng.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_tab1.src = "image/corp/tab1_eng.png";
                } else if (txtNetworkType.value == '3g') {
                    img_tab1.src = "image/3g/tab1_eng.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_tab1.src = "image/3gpost/tab1_eng.png";
                }
            }


        }

        function mouseOutImage_Tab() {
        
            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_tab1 = document.getElementById('<%= img_tab1.ClientID %>');

            if (txtPreferLang.value == 'Thai') {
                if (txtNetworkType.value == 'pre') {
                    img_tab1.src = "image/12call/tab1-gray.png";
                } else if (txtNetworkType.value == 'post') {
                    img_tab1.src = "image/gsm/tab1-gray.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_tab1.src = "image/corp/tab1-gray.png";
                } else if (txtNetworkType.value == '3g') {
                    img_tab1.src = "image/3g/tab1-gray.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_tab1.src = "image/3gpost/tab1-gray.png";
                }
            } else {
                if (txtNetworkType.value == 'pre') {
                    img_tab1.src = "image/12call/tab1_eng-gray.png";
                } else if (txtNetworkType.value == 'post') {
                    img_tab1.src = "image/gsm/tab1_eng-gray.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_tab1.src = "image/corp/tab1_eng-gray.png";
                } else if (txtNetworkType.value == '3g') {
                    img_tab1.src = "image/3g/tab1_eng-gray.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_tab1.src = "image/3gpost/tab1_eng-gray.png";
                }
                
            }


        }

    </script>

    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 700px; height: 466px;">
        <tr>
            <td style="vertical-align: top;">
                <table style="width: 100%; height: 392px;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                            </asp:ToolkitScriptManager>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 301px;">
                                <tr>
                                    <td style="padding-left: 10px; vertical-align: bottom; width: 50px;">
                                        <asp:ImageButton ID="img_tab1" runat="server" onmouseover="mouseOverImage_Tab()"
                                            onmouseout="mouseOutImage_Tab()" />
                                    </td>
                                    <td style="vertical-align: bottom; text-align: left;">
                                        <asp:ImageButton ID="img_tab2" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 17px">
                            <asp:Panel ID="Panel5" runat="server" BackImageUrl="~/image/bg_2.png" Height="360px"
                                Width="680px">
                                <table style="width: 98%;" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 17px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 17px">
                                            &nbsp;
                                        </td>
                                        <td class="label_black">
                                            <asp:Label ID="lbl_message1" runat="server" Font-Bold="true" Font-Names="Tahoma"
                                                Font-Size="17px" ForeColor="Black"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <div style="overflow: scroll; overflow-x: hidden; height: 300px;">
                                                <Saifi:MyDg ID="dgvdetail" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                                    ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif" ImageNext="/imgs/bulletr.gif"
                                                    ImagePrevious="/imgs/bulletl.gif" ShowFirstAndLastImage="False" ShowPreviousAndNextImage="False"
                                                    Width="95%" CssClass="gridItem">
                                                    <PagerStyle Mode="NumericPages" />
                                                    <HeaderStyle CssClass="gridheader" />
                                                    <Columns>
                                                        <asp:TemplateColumn>
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lbl_hd_tel" runat="server"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lbl_tel" Text='<%#DataBinder.Eval(Container.DataItem, "mobile_no")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lbl_hd_service" runat="server"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lbl_service" Text='<%#DataBinder.Eval(Container.DataItem, "service_name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lbl_hd_shop" runat="server"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lbl_shop" Text='<%#DataBinder.Eval(Container.DataItem, "shop_name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lbl_hd_date" runat="server"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lbl_date" Text='<%#DataBinder.Eval(Container.DataItem, "date_time")%>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lbl_hd_status" runat="server"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lbl_status" Text='<%#DataBinder.Eval(Container.DataItem, "status")%>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </Saifi:MyDg>
                                            </div>
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
    <div style="display: none">
        <asp:TextBox ID="txtNetworkType" runat="server" Width="0px"></asp:TextBox>
        <asp:TextBox ID="txtPreferLang" runat="server" Width="0px"></asp:TextBox>
    </div>
</asp:Content>
