<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frm_popup2.aspx.vb" Inherits="frm_popup2" MasterPageFile="~/e_Service_MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
        function mouseOverImage() {
            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_close = document.getElementById('<%= img_close.ClientID %>');

            if (txtPreferLang.value == 'Thai') {

                if (txtNetworkType.value == 'pre') {
                    img_close.src = "image/12call/btn_close2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_close.src = "image/gsm/btn_close2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_close.src = "image/corp/btn_close2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_close.src = "image/3g/btn_close2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_close.src = "image/3gpost/btn_close2.png";
                }
            } else {

                if (txtNetworkType.value == 'pre') {
                    img_close.src = "image/12call/btn_close_eng2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_close.src = "image/gsm/btn_close_eng2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_close.src = "image/corp/btn_close_eng2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_close.src = "image/3g/btn_close_eng2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_close.src = "image/3gpost/btn_close_eng2.png";
                }
            }


        }

        function mouseOutImage() {

            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_close = document.getElementById('<%= img_close.ClientID %>');

            if (txtPreferLang.value == 'Thai') {
                if (txtNetworkType.value == 'pre') {
                    img_close.src = "image/12call/btn_close.png";
                } else if (txtNetworkType.value == 'post') {
                    img_close.src = "image/gsm/btn_close.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_close.src = "image/corp/btn_close.png";
                } else if (txtNetworkType.value == '3g') {
                    img_close.src = "image/3g/btn_close.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_close.src = "image/3gpost/btn_close.png";
                }
            } else {
                if (txtNetworkType.value == 'pre') {
                    img_close.src = "image/12call/btn_close_eng.png";
                } else if (txtNetworkType.value == 'post') {
                    img_close.src = "image/gsm/btn_close_eng.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_close.src = "image/corp/btn_close_eng.png";
                } else if (txtNetworkType.value == '3g') {
                    img_close.src = "image/3g/btn_close_eng.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_close.src = "image/3gpost/btn_close_eng.png";
                }
            }


        }


      


    

    </script>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <div >
    </div>
        <asp:Panel ID="Panel1" runat="server" BackImageUrl="~/image/bg_3.png" 
            Height="307px" Width="619px">
            <table style="width:619px;" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td  style="width:30px;">
                        &nbsp;</td>
                    <td  style="width:300px;">
                        &nbsp;</td>
                    <td  style="width:30px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  style="width:30px;">
                        &nbsp;</td>
                    <td  style="width:300px;">
                        &nbsp;</td>
                    <td  style="width:30px;">
                        &nbsp;</td>
                </tr>
                <tr>
                   <td  style="width:30px;">
                        &nbsp;</td>
                    <td  style="width:300px; height:30px;">
                        &nbsp;</td>
                    <td  style="width:30px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  style="width:30px;">
                        &nbsp;</td>
                    <td  style="width:300px; height:200px;  vertical-align:top;">
                        <table style="width:619px;" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="text-align:center; height:30px; vertical-align:middle;"  class="label_green">
                                    <asp:Label ID="lbl_message1" runat="server"></asp:Label>
                                </td>
                                
                            </tr>
                            <tr>
                                <td style="text-align:center; height:30px; vertical-align:middle;"  class="label_green">
                                    <asp:Label ID="lbl_message2" runat="server" ForeColor="#A3171E"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center; height:30px; vertical-align:middle;">
                                    <asp:ImageButton ID="img_close" onmouseover="mouseOverImage()" 
                                        onmouseout="mouseOutImage()" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td  style="width:30px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  style="width:30px;">
                        &nbsp;</td>
                    <td  style="width:300px;">
                        &nbsp;</td>
                    <td  style="width:30px;">
                        &nbsp;</td>
                </tr>
            </table>
                       <div style="display:none">
                                        <asp:TextBox ID="txtNetworkType" runat="server" Width="0px"></asp:TextBox>
                                         <asp:TextBox ID="txtPreferLang" runat="server" Width="0px"></asp:TextBox>
                                    </div>
        </asp:Panel>
    

     </asp:Content>