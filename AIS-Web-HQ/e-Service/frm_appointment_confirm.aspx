<%@ Page Language="VB" MasterPageFile="~/e_Service_MasterPage.master" AutoEventWireup="false"
    CodeFile="frm_appointment_confirm.aspx.vb" Inherits="frm_appointment_confirm"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="jquery.min.js"></script>

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


        function mouseOverImage_confirm() {
            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_confirm = document.getElementById('<%= img_confirm.ClientID %>');

            if (txtPreferLang.value == 'Thai') {

                if (txtNetworkType.value == 'pre') {
                    img_confirm.src = "image/12call/btn_confirm2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_confirm.src = "image/gsm/btn_confirm2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_confirm.src = "image/corp/btn_confirm2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_confirm.src = "image/3g/btn_confirm2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_confirm.src = "image/3gpost/btn_confirm2.png";
                }
            } else {

                if (txtNetworkType.value == 'pre') {
                    img_confirm.src = "image/12call/btn_confirm_eng2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_confirm.src = "image/gsm/btn_confirm_eng2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_confirm.src = "image/corp/btn_confirm_eng2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_confirm.src = "image/3g/btn_confirm_eng2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_confirm.src = "image/3gpost/btn_confirm_eng2.png";
                }
            }


        }

        function mouseOutImage_confirm() {

            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_confirm = document.getElementById('<%= img_confirm.ClientID %>');

            if (txtPreferLang.value == 'Thai') {
                if (txtNetworkType.value == 'pre') {
                    img_confirm.src = "image/12call/btn_confirm.png";
                } else if (txtNetworkType.value == 'post') {
                    img_confirm.src = "image/gsm/btn_confirm.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_confirm.src = "image/corp/btn_confirm.png";
                } else if (txtNetworkType.value == '3g') {
                    img_confirm.src = "image/3g/btn_confirm.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_confirm.src = "image/3gpost/btn_confirm.png";
                }
            } else {
                if (txtNetworkType.value == 'pre') {
                    img_confirm.src = "image/12call/btn_confirm_eng.png";
                } else if (txtNetworkType.value == 'post') {
                    img_confirm.src = "image/gsm/btn_confirm_eng.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_confirm.src = "image/corp/btn_confirm_eng.png";
                } else if (txtNetworkType.value == '3g') {
                    img_confirm.src = "image/3g/btn_confirm_eng.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_confirm.src = "image/3gpost/btn_confirm_eng.png";
                }
            }
        }


        function HiddenButton() {
            var txt_email = $('#<%=img_confirm.ClientID %>');

            if (txt_email.value != '' && Page_ClientValidate("Next") != false) {
                $('#<%=img_confirm.ClientID %>').hide();
            }


        }

        function checkBack() {
            var txtCheck = document.getElementById('<%= txtCheck.ClientID %>');
            txtCheck.value = 'Back';
            window.location.href("frm_AddAppointment.aspx");
            return false;
        }

        function checkNext() {
            var txtCheck = document.getElementById('<%= txtCheck.ClientID %>');
            txtCheck.value = 'Next';

         
        }

        function validateEmail() {
            var txt_email = document.getElementById('<%= txt_email.ClientID %>');
            var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

            if (re.test(txt_email.value) == false && txt_email.value != '') {
                alert('Invalid Mail Format');
                return false;
            } else {
            HiddenButton();
                return true;
            }
           

           
        } 
       

    </script>

    <link href="StyleSheet.css" rel="stylesheet" type="text/css"></link>
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
                                        <asp:ImageButton ID="img_tab1" runat="server" Visible="False" />
                                    </td>
                                    <td style="vertical-align: bottom; text-align: left;">
                                        <asp:ImageButton ID="img_tab2" runat="server" Visible="False" />
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
                                        <table style="width: 100%; height: 357px;" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 33px; height: 18px">
                                                </td>
                                                <td style="height: 18px">
                                                </td>
                                                <td style="height: 18px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 33px; height: 60px;" class="style1">
                                                    &nbsp;
                                                </td>
                                                <td style="height: 60px">
                                                    <table style="width: 655px;" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="height: 15px; width: 655px;">
                                                                <asp:Label ID="lbl_prefix" runat="server" Width="70px" CssClass="label_green"></asp:Label>
                                                                <asp:Label ID="lbl_name" runat="server" Width="150px" CssClass="label_red"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 15px; width: 655px;">
                                                                <asp:Label ID="lbl_number" runat="server" CssClass="label_green" Width="116px"></asp:Label>
                                                                <asp:Label ID="lbl_phone_number" runat="server" CssClass="label_red" Width="140px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="height: 60px">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td class="label_green" style="height: 15px;">
                                                    <asp:Label ID="lbl_message1" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 33px" class="style1">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 59px">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_service1" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 59px">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_service2" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 59px">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_service3" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 33px" class="style1">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 600px;">
                                                        <tr>
                                                            <td class="label_green1" style="height: 15px;">
                                                                <asp:Label ID="lbl_message2" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="label_red1" style="height: 15px;">
                                                                <asp:Label ID="lbl_shop_nm" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="label_green1" style="height: 15px;">
                                                                <asp:Label ID="lbl_message3" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="label_red1" style="height: 15px;">
                                                                <asp:Label ID="lbl_appointment_day" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="label_green1" style="height: 15px;">
                                                                <asp:Label ID="lbl_message4" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="label_red1" style="height: 15px;">
                                                                <asp:Label ID="lbl_appointment_dt" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="label_green1" style="height: 15px;">
                                                                <asp:Label ID="lbl_message5" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="label_red1" style="height: 15px;">
                                                                <asp:Label ID="lbl_appointment_tm" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 33px" class="style1">
                                                    &nbsp;
                                                </td>
                                                <td class="label_green" style="height: 15px;">
                                                    <asp:Label ID="lbl_message6" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 33px" class="style1">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                        <tr>
                                                            <td class="label_green" style="height: 15px;">
                                                                <asp:Label ID="lbl_message7" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 144px">
                                                                <asp:TextBox ID="txt_email" AutoCompleteType="Email" runat="server" Width="235px"></asp:TextBox>
                                                            </td>
                                                            <td class="label_red" style="height: 15px;">
                                                                <asp:Label ID="Label7" runat="server" Text="*"></asp:Label>
                                                            </td>
                                                            <td class="label_green" style="height: 15px;">
                                                                <asp:Label ID="lbl_message8" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label_green" style="height: 15px;">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 144px">
                                                                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                    ControlToValidate="txt_email" Display="Dynamic"
                                                                    ErrorMessage="Invalid Email Format" ValidationGroup="Next" Enabled="False"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td class="label_green" style="height: 15px;">
                                                                &nbsp;
                                                            </td>
                                                            <td class="label_green" style="height: 15px;">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label_green" style="height: 15px;">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 144px">
                                                                &nbsp;
                                                            </td>
                                                            <td class="label_green" style="height: 15px;">
                                                                &nbsp;
                                                            </td>
                                                            <td class="label_green" style="height: 15px;">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 94px">
                                                                &nbsp;
                                                                </td>
                                                            <td style="width: 144px">
                                                                <asp:ImageButton ID="img_return" runat="server" onmouseover="mouseOverImage_return()"
                                                                    onmouseout="mouseOutImage_return()" onclientclick="checkBack();"  
                                                                     />
                                                               
                                                            </td>
                                                            <td style="width: 7px">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="img_confirm" runat="server" onmouseover="mouseOverImage_confirm()"
                                                                    onmouseout="mouseOutImage_confirm()" 
                                                                    OnClientClick=" return validateEmail();" ValidationGroup="Next"  
                                                                   />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 33px" class="style1">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    </td>
                                                <td>
                                                    &nbsp;
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
    <div style="display: none">
        <asp:TextBox ID="txtNetworkType" runat="server" Width="0px"></asp:TextBox>
        <asp:TextBox ID="txtPreferLang" runat="server" Width="0px"></asp:TextBox>
         <asp:TextBox ID="txtCheck" runat="server"></asp:TextBox>
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
            var txt_email = document.getElementById('<%= txt_email.ClientID %>');
            var txtCheck = document.getElementById('<%= txtCheck.ClientID %>');
           var txt_email = document.getElementById('<%= txt_email.ClientID %>');
            var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;


            if (txt_email.value != '' && Page_ClientValidate("Next") != false && re.test(txt_email.value) != false && txtCheck.value !='Back') {

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



        }

        $('form').live("submit", function() {

           // ShowProgress();

        });

    </script>

</asp:Content>
