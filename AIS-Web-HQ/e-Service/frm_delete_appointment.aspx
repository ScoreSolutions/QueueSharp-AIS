<%@ Page Language="VB" MasterPageFile="~/e_Service_MasterPage.master" AutoEventWireup="false"
    CodeFile="frm_delete_appointment.aspx.vb" Inherits="frm_delete_appointment" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <script type="text/javascript" src="jquery.min.js"></script>
    <script language="javascript" type="text/javascript">
        function mouseOverImage_return() {
            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_return_no_arrow = document.getElementById('<%= img_return_no_arrow.ClientID %>');
            if (txtPreferLang.value == 'Thai') {

                if (txtNetworkType.value == 'pre') {
                    img_return_no_arrow.src = "image/12call/btn_return_no_arrow2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_return_no_arrow.src = "image/gsm/btn_return_no_arrow2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_return_no_arrow.src = "image/corp/btn_return_no_arrow2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_return_no_arrow.src = "image/3g/btn_return_no_arrow2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_return_no_arrow.src = "image/3gpost/btn_return_no_arrow2.png";
                }
            } else {

                if (txtNetworkType.value == 'pre') {
                    img_return_no_arrow.src = "image/12call/btn_return_no_arrow_eng2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_return_no_arrow.src = "image/gsm/btn_return_no_arrow_eng2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_return_no_arrow.src = "image/corp/btn_return_no_arrow_eng2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_return_no_arrow.src = "image/3g/btn_return_no_arrow_eng2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_return_no_arrow.src = "image/3gpost/btn_return_no_arrow_eng2.png";
                }
            }


        }

        function mouseOutImage_return() {

            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_return_no_arrow = document.getElementById('<%= img_return_no_arrow.ClientID %>');

            if (txtPreferLang.value == 'Thai') {
                if (txtNetworkType.value == 'pre') {
                    img_return_no_arrow.src = "image/12call/btn_return_no_arrow.png";
                } else if (txtNetworkType.value == 'post') {
                    img_return_no_arrow.src = "image/gsm/btn_return_no_arrow.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_return_no_arrow.src = "image/corp/btn_return_no_arrow.png";
                } else if (txtNetworkType.value == '3g') {
                    img_return_no_arrow.src = "image/3g/btn_return_no_arrow.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_return_no_arrow.src = "image/3gpost/btn_return_no_arrow.png";
                }
            } else {
                if (txtNetworkType.value == 'pre') {
                    img_return_no_arrow.src = "image/12call/btn_return_no_arrow_eng.png";
                } else if (txtNetworkType.value == 'post') {
                    img_return_no_arrow.src = "image/gsm/btn_return_no_arrow_eng.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_return_no_arrow.src = "image/corp/btn_return_no_arrow_eng.png";
                } else if (txtNetworkType.value == '3g') {
                    img_return_no_arrow.src = "image/3g/btn_return_no_arrow_eng.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_return_no_arrow.src = "image/3gpost/btn_return_no_arrow_eng.png";
                }
            }


        }


        function mouseOverImage_confirm() {
            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_confirm_reject = document.getElementById('<%= img_confirm_reject.ClientID %>');

            if (txtPreferLang.value == 'Thai') {

                if (txtNetworkType.value == 'pre') {
                    img_confirm_reject.src = "image/12call/btn_confirm_reject2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_confirm_reject.src = "image/gsm/btn_confirm_reject2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_confirm_reject.src = "image/corp/btn_confirm_reject2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_confirm_reject.src = "image/3g/btn_confirm_reject2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_confirm_reject.src = "image/3gpost/btn_confirm_reject2.png";
                }
            } else {

                if (txtNetworkType.value == 'pre') {
                    img_confirm_reject.src = "image/12call/btn_confirm_eng2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_confirm_reject.src = "image/gsm/btn_confirm_eng2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_confirm_reject.src = "image/corp/btn_confirm_eng2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_confirm_reject.src = "image/3g/btn_confirm_eng2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_confirm_reject.src = "image/3gpost/btn_confirm_eng2.png";
                }
            }


        }

        function mouseOutImage_confirm() {

            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_confirm_reject = document.getElementById('<%= img_confirm_reject.ClientID %>');

            if (txtPreferLang.value == 'Thai') {
                if (txtNetworkType.value == 'pre') {
                    img_confirm_reject.src = "image/12call/btn_confirm_reject.png";
                } else if (txtNetworkType.value == 'post') {
                    img_confirm_reject.src = "image/gsm/btn_confirm_reject.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_confirm_reject.src = "image/corp/btn_confirm_reject.png";
                } else if (txtNetworkType.value == '3g') {
                    img_confirm_reject.src = "image/3g/btn_confirm_reject.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_confirm_reject.src = "image/3gpost/btn_confirm_reject.png";
                }
            } else {
                if (txtNetworkType.value == 'pre') {
                    img_confirm_reject.src = "image/12call/btn_confirm_eng.png";
                } else if (txtNetworkType.value == 'post') {
                    img_confirm_reject.src = "image/gsm/btn_confirm_eng.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_confirm_reject.src = "image/corp/btn_confirm_eng.png";
                } else if (txtNetworkType.value == '3g') {
                    img_confirm_reject.src = "image/3g/btn_confirm_eng.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_confirm_reject.src = "image/3gpost/btn_confirm_eng.png";
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

        function HiddenButton() {
            $('#<%=img_confirm_reject.ClientID %>').hide();
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
                                Width="681px" Style="margin-right: 0px">
                                <table style="width: 100%; height: 357px;" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="2" style="height: 10px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="height: 30px; padding-left: 5px; width: 200px;">
                                            <asp:Panel ID="Panel3" runat="server" CssClass="table_header" Width="661px">
                                                <asp:Label ID="lbl_message1" runat="server"></asp:Label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20px; height: 30px;">
                                            &nbsp;
                                        </td>
                                        <td style="height: 30px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20px; height: 30px;">
                                            &nbsp;
                                        </td>
                                        <td style="height: 30px">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                <tr>
                                                    <td style="width: 100px; height: 25px;">
                                                        &nbsp;
                                                    </td>
                                                    <td class="label_green" style="height: 25px">
                                                        <asp:Label ID="lbl_message2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                            <tr>
                                                                <td class="label_black" style="width: 240px; height: 30px;">
                                                                    <asp:Label ID="lbl_message3" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="label_red1" style="width: 221px; height: 30px;" colspan="4">
                                                                    <asp:Label ID="lbl_service" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="label_black" style="width: 240px; height: 30px;">
                                                                    <asp:Label ID="lbl_message4" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="label_red1" style="width: 221px; height: 30px;" colspan="4">
                                                                    <asp:Label ID="lbl_shopnm" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="label_black" style="width: 240px; height: 30px;">
                                                                    <asp:Label ID="lbl_message5" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="label_red1" style="width: 221px; height: 20px;">
                                                                    <asp:Label ID="lbl_day" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="label_black" style="width: 69px; height: 30px;">
                                                                    <asp:Label ID="lbl_message6" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="label_red1" style="width: 239px; height: 30px;">
                                                                    <asp:Label ID="lbl_date" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="label_black" style="width: 114px; height: 20px;">
                                                                    <asp:Label ID="lbl_message7" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="label_red1" style="width: 277px; height: 30px;">
                                                                    <asp:Label ID="lbl_time" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 75%;">
                                                            <tr>
                                                                <td style="width: 45px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 83px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 145px">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 45px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 83px">
                                                                    &nbsp;
                                                                    <asp:ImageButton ID="img_return_no_arrow" runat="server" onmouseover="mouseOverImage_return()"
                                                                        onmouseout="mouseOutImage_return()" />
                                                                </td>
                                                                <td style="width: 145px">
                                                                    &nbsp;
                                                                    <asp:ImageButton ID="img_confirm_reject" runat="server" onmouseover="mouseOverImage_confirm()"
                                                                        onmouseout="mouseOutImage_confirm()" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 45px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 83px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 145px">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
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
    <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <img src="image/ajax-loader2.gif" alt="" />
    </div>
    
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 400px;
            height: 250px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>

    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function() {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 4 - loading[0].offsetHeight / 4, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }

        $('form').live("submit", function() {
            //ShowProgress();
        });
    </script>
</asp:Content>
