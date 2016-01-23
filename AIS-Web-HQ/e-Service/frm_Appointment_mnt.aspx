<%@ Page Title="" Language="VB" MasterPageFile="~/e_Service_MasterPage.master" AutoEventWireup="false"
    EnableEventValidation="false" CodeFile="frm_Appointment_mnt.aspx.vb" Inherits="frm_Appointment_mnt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function mouseOverImage_return() {
            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_return = document.getElementById('<%= img_return.ClientID %>');
            if (txtPreferLang.value == 'Thai') {

                if (txtNetworkType.value == 'pre') {
                    img_return.src = "image/12call/btn_back2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_return.src = "image/gsm/btn_back2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_return.src = "image/corp/btn_back2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_return.src = "image/3g/btn_back2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_return.src = "image/3gpost/btn_back2.png";
                }
            } else {

                if (txtNetworkType.value == 'pre') {
                    img_return.src = "image/12call/btn_back_eng2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_return.src = "image/gsm/btn_back_eng2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_return.src = "image/corp/btn_back_eng2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_return.src = "image/3g/btn_back_eng2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_return.src = "image/3gpost/btn_back_eng2.png";
                }
            }


        }

        function mouseOutImage_return() {

            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_return = document.getElementById('<%= img_return.ClientID %>');

            if (txtPreferLang.value == 'Thai') {
                if (txtNetworkType.value == 'pre') {
                    img_return.src = "image/12call/btn_back.png";
                } else if (txtNetworkType.value == 'post') {
                    img_return.src = "image/gsm/btn_back.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_return.src = "image/corp/btn_back.png";
                } else if (txtNetworkType.value == '3g') {
                    img_return.src = "image/3g/btn_back.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_return.src = "image/3gpost/btn_back.png";
                }
                
            } else {
                if (txtNetworkType.value == 'pre') {
                    img_return.src = "image/12call/btn_back_eng.png";
                } else if (txtNetworkType.value == 'post') {
                    img_return.src = "image/gsm/btn_back_eng.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_return.src = "image/corp/btn_back_eng.png";
                } else if (txtNetworkType.value == '3g') {
                    img_return.src = "image/3g/btn_back_eng.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_return.src = "image/3gpost/btn_back_eng.png";
                }
            }


        }

        function mouseOverImage() {
            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_ok = document.getElementById('<%= img_ok.ClientID %>');

            if (txtPreferLang.value == 'Thai') {

                if (txtNetworkType.value == 'pre') {
                    img_ok.src = "image/12call/btn_ok2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_ok.src = "image/gsm/btn_ok2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_ok.src = "image/corp/btn_ok2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_ok.src = "image/3g/btn_ok2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_ok.src = "image/3gpost/btn_ok2.png";
                }
            } else {

                if (txtNetworkType.value == 'pre') {
                    img_ok.src = "image/12call/btn_ok_eng2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_ok.src = "image/gsm/btn_ok_eng2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_ok.src = "image/corp/btn_ok_eng2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_ok.src = "image/3g/btn_ok_eng2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_ok.src = "image/3gpost/btn_ok_eng2.png";
                }
            }


        }

        function mouseOutImage() {

            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_ok = document.getElementById('<%= img_ok.ClientID %>');

            if (txtPreferLang.value == 'Thai') {
                if (txtNetworkType.value == 'pre') {
                    img_ok.src = "image/12call/btn_ok.png";
                } else if (txtNetworkType.value == 'post') {
                    img_ok.src = "image/gsm/btn_ok.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_ok.src = "image/corp/btn_ok.png";
                } else if (txtNetworkType.value == '3g') {
                    img_ok.src = "image/3g/btn_ok.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_ok.src = "image/3gpost/btn_ok.png";
                }
            } else {
                if (txtNetworkType.value == 'pre') {
                    img_ok.src = "image/12call/btn_ok_eng.png";
                } else if (txtNetworkType.value == 'post') {
                    img_ok.src = "image/gsm/btn_ok_eng.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_ok.src = "image/corp/btn_ok_eng.png";
                } else if (txtNetworkType.value == '3g') {
                    img_ok.src = "image/3g/btn_ok_eng.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_ok.src = "image/3gpost/btn_ok_eng.png";
                }
            }


        }


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
                                        <asp:ImageButton ID="img_tab2" runat="server" onmouseover="mouseOverImage_Tab()"
                                            onmouseout="mouseOutImage_Tab()" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel7" runat="server" BackImageUrl="~/image/bg_2.png" Height="359px"
                                Width="681px">
                                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                    <ContentTemplate>
                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 4px; padding-right: 4px;">
                                                    <asp:Panel ID="Panel2" runat="server" CssClass="table_header">
                                                        <asp:Label ID="lbl_headder1" runat="server"></asp:Label>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 313px; padding-left: 5px;" class="label_green">
                                                                <asp:Panel ID="Panel9" runat="server">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                        <tr>
                                                                            <td class="label_green" style="width: 80px">
                                                                                <asp:Label ID="lbl_service1" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="dropdrown">
                                                                                <asp:DropDownList ID="dtpservice1" runat="server" AutoPostBack="True" CssClass="detailedViewTextBox"
                                                                                    Width="200px">
                                                                                </asp:DropDownList>
                                                                                <asp:Label ID="lblService1Default" runat="server" Visible="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label_green" style="width: 80px">
                                                                                <asp:Label ID="lbl_service2" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="dropdrown">
                                                                                <asp:DropDownList ID="dtpservice2" runat="server" AutoPostBack="True" CssClass="detailedViewTextBox"
                                                                                    Width="200px">
                                                                                </asp:DropDownList>
                                                                                <asp:Label ID="lblService2Default" runat="server" Visible="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label_green" style="width: 80px">
                                                                                <asp:Label ID="lbl_service3" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="dropdrown">
                                                                                <asp:DropDownList ID="dtpservice3" runat="server" AutoPostBack="True" CssClass="detailedViewTextBox"
                                                                                    Width="200px">
                                                                                </asp:DropDownList>
                                                                                <asp:Label ID="lblService3Default" runat="server" Visible="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                            <td>
                                                                <asp:Panel ID="Panel8" runat="server" Width="336px" Visible="False">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                        <tr>
                                                                            <td class="label_green" style="padding-left: 10px;">
                                                                                <asp:Label ID="lbl_type" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="dropdrown">
                                                                                <asp:DropDownList ID="dtptype1" runat="server" AutoPostBack="True" CssClass="detailedViewTextBox"
                                                                                    Width="200px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 80px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td class="dropdrown">
                                                                                <asp:DropDownList ID="dtptype2" runat="server" AutoPostBack="True" CssClass="detailedViewTextBox"
                                                                                    Width="200px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 80px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td class="dropdrown">
                                                                                <asp:DropDownList ID="dtptype3" runat="server" AutoPostBack="True" CssClass="detailedViewTextBox"
                                                                                    Width="200px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 4px; padding-right: 4px;">
                                                    <asp:Panel ID="Panel3" runat="server" CssClass="table_header">
                                                        <asp:Label ID="lbl_headder2" runat="server"></asp:Label>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;<table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 122px; padding-left: 10px;" class="label_green">
                                                                <asp:Label ID="lbl_region" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="dropdrown">
                                                                <asp:DropDownList ID="dtpregion" runat="server" Width="300px" CssClass="detailedViewTextBox"
                                                                    AutoPostBack="True">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblRegionDefault" runat="server" Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 122px; padding-left: 10px;" class="label_green">
                                                                <asp:Label ID="lbl_branch" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="dropdrown">
                                                                <asp:DropDownList ID="dtpbranch" runat="server" Width="300px" CssClass="detailedViewTextBox"
                                                                    AutoPostBack="True">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblBranchDefault" runat="server" Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label_green" style="width: 122px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="padding-left: 50px;">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label_green" style="width: 122px" align="right">
                                                                <asp:ImageButton ID="img_return" runat="server" Visible="false" onmouseover="mouseOverImage_return()" onmouseout="mouseOutImage_return()"  />
                                                            </td>
                                                            <td style="padding-left: 50px;">
                                                                <asp:ImageButton ID="img_ok" runat="server" onmouseover="mouseOverImage()" onmouseout="mouseOutImage()" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
        
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
