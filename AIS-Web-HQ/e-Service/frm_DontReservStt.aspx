<%@ Page Title="" Language="VB" MasterPageFile="~/e_Service_MasterPage.master" AutoEventWireup="false"
    CodeFile="frm_DontReservStt.aspx.vb" Inherits="frm_DontReservStt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function mouseOverImage_confirm(){
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

        function mouseOutImage_confirm() {

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


        function mouseOverImage_check() {
            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_servicehistory = document.getElementById('<%= img_servicehistory.ClientID %>');

            if (txtPreferLang.value == 'Thai') {

                if (txtNetworkType.value == 'pre') {
                    img_servicehistory.src = "image/12call/btn_his_service2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_servicehistory.src = "image/gsm/btn_his_service2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_servicehistory.src = "image/corp/btn_his_service2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_servicehistory.src = "image/3g/btn_his_service2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_servicehistory.src = "image/3gpost/btn_his_service2.png";
                }
            } else {

                if (txtNetworkType.value == 'pre') {
                    img_servicehistory.src = "image/12call/btn_his_service_eng2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_servicehistory.src = "image/gsm/btn_his_service_eng2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_servicehistory.src = "image/corp/btn_his_service_eng2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_servicehistory.src = "image/3g/btn_his_service_eng2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_servicehistory.src = "image/3gpost/btn_his_service_eng2.png";
                }
            }


        }

        function mouseOutImage_check() {

            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_servicehistory = document.getElementById('<%= img_servicehistory.ClientID %>');

            if (txtPreferLang.value == 'Thai') {
                if (txtNetworkType.value == 'pre') {
                    img_servicehistory.src = "image/12call/btn_his_service.png";
                } else if (txtNetworkType.value == 'post') {
                    img_servicehistory.src = "image/gsm/btn_his_service.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_servicehistory.src = "image/corp/btn_his_service.png";
                } else if (txtNetworkType.value == '3g') {
                    img_servicehistory.src = "image/3g/btn_his_service.png";
                }
            } else {
                if (txtNetworkType.value == 'pre') {
                    img_servicehistory.src = "image/12call/btn_his_service_eng.png";
                } else if (txtNetworkType.value == 'post') {
                    img_servicehistory.src = "image/gsm/btn_his_service_eng.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_servicehistory.src = "image/corp/btn_his_service_eng.png";
                } else if (txtNetworkType.value == '3g') {
                    img_servicehistory.src = "image/3g/btn_his_service_eng.png";
                }
            }


        }

       

    </script>

    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <table style="width: 98%; padding-left: 20px;">
        <tr>
            <td colspan="3">
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 50px; text-align: center;" class="message">
                <asp:Label ID="lbl_message1" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 50px; text-align: center;" class="message">
                <asp:Label ID="lbl_message2" runat="server" ForeColor="#669900"></asp:Label>
                <asp:Label ID="lbl_message3" runat="server" ForeColor="#669900"></asp:Label>
                <asp:Label ID="lbl_message4" runat="server" ForeColor="#669900"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 30px; text-align: center; padding-right: 50px;" class="message">
                <asp:Label ID="lbl_message5" runat="server" Text="ขอบคุณที่ใช้บริการ" ForeColor="black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 162px">
                &nbsp;
            </td>
            <td style="width: 123px; text-align: center;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 98px">
                                    <asp:ImageButton ID="img_ok" runat="server" onmouseover="mouseOverImage_confirm()"
                                        onmouseout="mouseOutImage_confirm()" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="img_servicehistory" runat="server" onmouseover="mouseOverImage_check()"
                                        onmouseout="mouseOutImage_check()" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="width: 190px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 162px">
                &nbsp;
            </td>
            <td style="width: 123px">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <div style="display: none">
        <asp:TextBox ID="txtNetworkType" runat="server" Width="0px"></asp:TextBox>
        <asp:TextBox ID="txtPreferLang" runat="server" Width="0px"></asp:TextBox>
    </div>
</asp:Content>
