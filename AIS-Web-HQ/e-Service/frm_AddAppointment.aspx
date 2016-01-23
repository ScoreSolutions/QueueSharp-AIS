<%@ Page Title="" Language="VB" MasterPageFile="~/e_Service_MasterPage.master" AutoEventWireup="false"
    CodeFile="frm_AddAppointment.aspx.vb" Inherits="frm_AddAppointment" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
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


        function mouseOverImage_next() {
            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_next = document.getElementById('<%= img_next.ClientID %>');

            if (txtPreferLang.value == 'Thai') {

                if (txtNetworkType.value == 'pre') {
                    img_next.src = "image/12call/btn_next2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_next.src = "image/gsm/btn_next2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_next.src = "image/corp/btn_next2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_next.src = "image/3g/btn_next2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_next.src = "image/3gpost/btn_next2.png";
                }
            } else {

                if (txtNetworkType.value == 'pre') {
                    img_next.src = "image/12call/btn_next_eng2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_next.src = "image/gsm/btn_next_eng2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_next.src = "image/corp/btn_next_eng2.png";
                } else if (txtNetworkType.value == '3g') {
                    img_next.src = "image/3g/btn_next_eng2.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_next.src = "image/3gpost/btn_next_eng2.png";
                }
            }


        }

        function mouseOutImage_next() {

            var txtNetworkType = document.getElementById('<%= txtNetworkType.ClientID %>');
            var txtPreferLang = document.getElementById('<%= txtPreferLang.ClientID %>');
            var img_next = document.getElementById('<%= img_next.ClientID %>');

            if (txtPreferLang.value == 'Thai') {
                if (txtNetworkType.value == 'pre') {
                    img_next.src = "image/12call/btn_next.png";
                } else if (txtNetworkType.value == 'post') {
                    img_next.src = "image/gsm/btn_next.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_next.src = "image/corp/btn_next.png";
                } else if (txtNetworkType.value == '3g') {
                    img_next.src = "image/3g/btn_next.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_next.src = "image/3gpost/btn_next.png";
                }
            } else {
                if (txtNetworkType.value == 'pre') {
                    img_next.src = "image/12call/btn_next_eng.png";
                } else if (txtNetworkType.value == 'post') {
                    img_next.src = "image/gsm/btn_next_eng.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_next.src = "image/corp/btn_next_eng.png";
                } else if (txtNetworkType.value == '3g') {
                    img_next.src = "image/3g/btn_next_eng.png";
                } else if (txtNetworkType.value == '3gpost') {
                    img_next.src = "image/3gpost/btn_next_eng.png";
                }
            }


        }

    </script>

    <script type="text/javascript" src="<%= ResolveUrl("~/Bin/jquery.min.js")%>"></script>

    <style type="text/css">
        .RowStyle
        {
            height: 25px;
        }
        .AlternateRowStyle
        {
            height: 25px;
        }
    </style>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 680px; height: 520px;">
        <tr>
            <td style="vertical-align: top;">
                <table style="width: 100%; height: 205px;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="height: 30px">
                            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                            </asp:ToolkitScriptManager>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 350px;" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="padding-left: 10px; vertical-align: bottom; width: 50px;">
                                        <asp:ImageButton ID="img_tab1" runat="server" Visible="False" />
                                        <span id="tab1_span" runat="server" class="tab1"><a href='#' id="imgTab1" runat="server">
                                        </a></span>
                                    </td>
                                    <td style="vertical-align: bottom; text-align: left;">
                                        <asp:ImageButton ID="img_tab2" runat="server" Visible="False" />
                                        <span id="tab2_span" runat="server" class="tab2"><a href='#' id="imgTab2" runat="server">
                                        </a></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                       <%-- <asp:Panel ID="Panel7" runat="server" BackImageUrl="~/image/bg_2.png" Height="359px"--%>
                            <asp:Panel ID="Panel7" runat="server" BackImageUrl="~/image/bg_2.png" Height="359px"
                                Width="681px">

                                        <table style="width: 100%; height: 357px;" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="label_header1_green" style="height: 15px;">
                                                    <asp:Label ID="lbl_message1" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 5px; vertical-align: top; height: 254px;">
                                                    <div >
                                                        
                                                            <asp:UpdatePanel ID="updatepanel2" runat="server">
                                                                 <ContentTemplate>
                                                                    <asp:Panel ID="Pnl1" runat="server" ScrollBars="Vertical" Height="250px" Width="100%">
                                                                        
                                                                        <table border="0" cellpadding="0" cellspacing="0" class="tebleAppointment" style="width: 100%;
                                                                            height: 290px;">
                                                                            <tr>
                                                                                <td id="td1" runat="server" class="tebleAppointment_header">
                                                                                    <asp:Label ID="lbl_time" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td id="td2" runat="server" class="tebleAppointment_header" style="width: 81px;">
                                                                                    <asp:Label ID="lbl_day1" runat="server" Style="width: 81px;"></asp:Label>
                                                                                    <br />
                                                                                    <asp:Label ID="lbl_date1" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbl_dateorginal1" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td id="td3" runat="server" class="tebleAppointment_header" style="width: 81px;">
                                                                                    <asp:Label ID="lbl_day2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                    <br />
                                                                                    <asp:Label ID="lbl_date2" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbl_dateorginal2" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td id="td4" runat="server" class="tebleAppointment_header" style="width: 81px;">
                                                                                    <asp:Label ID="lbl_day3" runat="server" Style="width: 81px;"></asp:Label>
                                                                                    <br />
                                                                                    <asp:Label ID="lbl_date3" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbl_dateorginal3" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td id="td5" runat="server" class="tebleAppointment_header" style="width: 81px;">
                                                                                    <asp:Label ID="lbl_day4" runat="server" Style="width: 81px;"></asp:Label>
                                                                                    <br />
                                                                                    <asp:Label ID="lbl_date4" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbl_dateorginal4" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td id="td6" runat="server" class="tebleAppointment_header" style="width: 81px;">
                                                                                    <asp:Label ID="lbl_day5" runat="server" Style="width: 81px;"></asp:Label>
                                                                                    <br />
                                                                                    <asp:Label ID="lbl_date5" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbl_dateorginal5" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td id="td7" runat="server" class="tebleAppointment_header" style="width: 81px;">
                                                                                    <asp:Label ID="lbl_day6" runat="server" Style="width: 81px;"></asp:Label>
                                                                                    <br />
                                                                                    <asp:Label ID="lbl_date6" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbl_dateorginal6" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td id="td8" runat="server" class="tebleAppointment_header" style="width: 81px;">
                                                                                    <asp:Label ID="lbl_day7" runat="server" Style="width: 81px;"></asp:Label>
                                                                                    <br />
                                                                                    <asp:Label ID="lbl_date7" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbl_dateorginal7" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="8" style="height: 250px; vertical-align: top;">
                                                                                    
                                                                                        <Saifi:MyDg ID="dgvdetail" runat="server" AutoGenerateColumns="False" ImageFirst="/imgs/nav_left.gif"
                                                                                            ImageLast="/imgs/nav_right.gif" PageSize="50" ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif"
                                                                                            ShowFirstAndLastImage="False" ShowHeader="False" ShowPreviousAndNextImage="False"
                                                                                            AllowPaging="false" Width="100%" CssClass="gridItem">
                                                                                            <EditItemStyle CssClass="RowStyle" />
                                                                                            <AlternatingItemStyle CssClass="RowStyle" />
                                                                                            <ItemStyle CssClass="RowStyle" />
                                                                                            <PagerStyle Mode="NumericPages" />
                                                                                            <Columns>
                                                                                                <asp:TemplateColumn HeaderText="time">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label runat="server" ID="lbl_time" Text='<%#DataBinder.Eval(Container.DataItem, "time")%>'
                                                                                                            Width="75px" BorderStyle="None"> </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn HeaderText="day1">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Button ID="btn_day1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "day1")%>'
                                                                                                            Width="79px" Height="25px" Font-Size="11px" BorderStyle="None" OnClick="MyButtonClick1" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn HeaderText="day2">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Button ID="btn_day2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "day2")%>'
                                                                                                            Width="78px" Height="25px" Font-Size="11px" BorderStyle="None" OnClick="MyButtonClick2" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn HeaderText="day3">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Button ID="btn_day3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "day3")%>'
                                                                                                            Width="78px" Height="25px" Font-Size="11px" BorderStyle="None" OnClick="MyButtonClick3" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn HeaderText="day4">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Button ID="btn_day4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "day4")%>'
                                                                                                            Width="78px" Height="25px" Font-Size="11px" BorderStyle="None" OnClick="MyButtonClick4" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn HeaderText="day5">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Button ID="btn_day5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "day5")%>'
                                                                                                            Width="78px" Height="25px" Font-Size="11px" BorderStyle="None" OnClick="MyButtonClick5" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn HeaderText="day6">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Button ID="btn_day6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "day6")%>'
                                                                                                            Width="78px" Height="25px" Font-Size="11px" BorderStyle="None" OnClick="MyButtonClick6" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:TemplateColumn HeaderText="day7">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Button ID="btn_day7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "day7")%>'
                                                                                                            Width="78px" Height="25px" Font-Size="11px" BorderStyle="None" OnClick="MyButtonClick7" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateColumn>
                                                                                            </Columns>
                                                                                        </Saifi:MyDg>
                                                                                        
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                            </ContentTemplate>
                                                       </asp:UpdatePanel>
                                                                    
                                                                    
                                                                    
                                                                    <asp:Panel ID="Pnl2" runat="server" Height="250px" Width="100%">
                                                                        <table border="0" cellpadding="0" cellspacing="0" class="tebleAppointment" style="width: 100%;
                                                                            height: 290px;">
                                                                           
                                                                         <tr>
                                                                                <td id="td16" runat="server" class="tebleAppointment_header">
                                                                                    <asp:Label ID="lbl_time2" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td id="td17" runat="server" class="tebleAppointment_header" style="width: 81px;">
                                                                                    <asp:Label ID="lbl_day1_2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                    <br />
                                                                                    <asp:Label ID="lbl_date1_2" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbl_dateorginal1_2" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td id="td18" runat="server" class="tebleAppointment_header" style="width: 81px;">
                                                                                    <asp:Label ID="lbl_day2_2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                    <br />
                                                                                    <asp:Label ID="lbl_date2_2" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbl_dateorginal2_2" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td id="td19" runat="server" class="tebleAppointment_header" style="width: 81px;">
                                                                                    <asp:Label ID="lbl_day3_2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                    <br />
                                                                                    <asp:Label ID="lbl_date3_2" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbl_dateorginal3_2" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td id="td20" runat="server" class="tebleAppointment_header" style="width: 81px;">
                                                                                    <asp:Label ID="lbl_day4_2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                    <br />
                                                                                    <asp:Label ID="lbl_date4_2" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbl_dateorginal4_2" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td id="td21" runat="server" class="tebleAppointment_header" style="width: 81px;">
                                                                                    <asp:Label ID="lbl_day5_2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                    <br />
                                                                                    <asp:Label ID="lbl_date5_2" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbl_dateorginal5_2" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td id="td22" runat="server" class="tebleAppointment_header" style="width: 81px;">
                                                                                    <asp:Label ID="lbl_day6_2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                    <br />
                                                                                    <asp:Label ID="lbl_date6_2" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbl_dateorginal6_2" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td id="td23" runat="server" class="tebleAppointment_header" style="width: 81px;">
                                                                                    <asp:Label ID="lbl_day7_2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                    <br />
                                                                                    <asp:Label ID="lbl_date7_2" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbl_dateorginal7_2" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="8" style="height: 250px; vertical-align: top;">
                                                                                    <Saifi:MyDg ID="dgvdetail_2" runat="server" AutoGenerateColumns="False" ImageFirst="/imgs/nav_left.gif"
                                                                                        ImageLast="/imgs/nav_right.gif" ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif"
                                                                                        ShowFirstAndLastImage="False" ShowHeader="False" ShowPreviousAndNextImage="False"
                                                                                        AllowPaging="false" Width="100%" CssClass="gridItem" PageSize="50">
                                                                                        <EditItemStyle CssClass="RowStyle" />
                                                                                        <AlternatingItemStyle CssClass="RowStyle" />
                                                                                        <ItemStyle CssClass="RowStyle" />
                                                                                        <PagerStyle Mode="NumericPages" />
                                                                                        <Columns>
                                                                                            <asp:TemplateColumn HeaderText="time">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label runat="server" ID="lbl_time2" Text='<%#DataBinder.Eval(Container.DataItem, "time")%>'
                                                                                                        Width="75px" BorderStyle="None"> </asp:Label></ItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn HeaderText="day1">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Button ID="btn_day1_2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "day1")%>'
                                                                                                        Width="78px" Height="25px" Font-Size="11px" BorderStyle="None" OnClick="MyButtonClick1_2" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn HeaderText="day2">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Button ID="btn_day2_2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "day2")%>'
                                                                                                        Width="78px" Height="25px" Font-Size="11px" BorderStyle="None" OnClick="MyButtonClick2_2" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn HeaderText="day3">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Button ID="btn_day3_2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "day3")%>'
                                                                                                        Width="78px" Height="25px" Font-Size="11px" BorderStyle="None" OnClick="MyButtonClick3_2" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn HeaderText="day4">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Button ID="btn_day4_2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "day4")%>'
                                                                                                        Width="78px" Height="25px" Font-Size="11px" BorderStyle="None" OnClick="MyButtonClick4_2" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn HeaderText="day5">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Button ID="btn_day5_2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "day5")%>'
                                                                                                        Width="78px" Height="25px" Font-Size="11px" BorderStyle="None" OnClick="MyButtonClick5_2" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn HeaderText="day6">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Button ID="btn_day6_2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "day6")%>'
                                                                                                        Width="78px" Height="25px" Font-Size="11px" BorderStyle="None" OnClick="MyButtonClick6_2" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn HeaderText="day7">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Button ID="btn_day7_2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "day7")%>'
                                                                                                        Width="78px" Height="25px" Font-Size="11px" BorderStyle="None" OnClick="MyButtonClick7_2" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                        </Columns>
                                                                                    </Saifi:MyDg>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                        <div id="Next" style="height: 30px;">
                                                            <table width="100%" border="0">
                                                                <tr>
                                                                    <td width="10%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="left" width="10%">
                                                                        <asp:Button ID="btnPrev" runat="server" Text="<<" />
                                                                    </td>
                                                                          <td width="60%" align="center"  >
                                                                        &nbsp;<asp:Button ID="btnNotAvaliable" runat="server" BackColor="#948A54" Width="25px" Enabled="false" Height="15px"  />
                                                                &nbsp;<asp:Label ID="lblNotAvaliable" runat="server" Text="Not Avaliable"></asp:Label>
                                                                <asp:Button ID="btnAvaliable" runat="server" BackColor="White"  Width="25px" Enabled="false" Height="15px"    />
                                                                &nbsp;<asp:Label ID="lblAvaliable" runat="server" Text="Avaliable"></asp:Label></td>
                                                                    </td>
                                                                    <td align="right" width="10%">
                                                                        <asp:Button ID="btnNext" runat="server" Text=">>" />
                                                                    </td>
                                                                    <td width="10%">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 30px;">
                                                    <table style="width: 80%;" border="0" cellpadding="0" cellspacing="0">
                                       
                                                        <tr>
                                                            <td style="width: 224px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 123px">
                                                                <asp:ImageButton ID="img_return" runat="server" 
                                                                    onmouseout="mouseOutImage_return()" onmouseover="mouseOverImage_return()" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="img_next" runat="server" onmouseout="mouseOutImage_next()" 
                                                                    onmouseover="mouseOverImage_next()" />
                                                            </td>
                                                            <td style="display: none">
                                                                <%--                                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/image/12call/btn_close.png"
                                                                    onmouseover="this.src='image/12call/btn_close_eng.png';" onmouseout="this.src='image/12call/btn_close.png';" />--%>
                                                            </td>
                                                        </tr>
                                                    </table>
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
