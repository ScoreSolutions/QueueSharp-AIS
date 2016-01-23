<%@ Page Title="" Language="VB" MasterPageFile="~/e_Service_MasterPage.master" AutoEventWireup="false"
    CodeFile="frm_AddAppointmentNew.aspx.vb" Inherits="frm_AddAppointmentNew" %>

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
                }
            } else {

                if (txtNetworkType.value == 'pre') {
                    img_return.src = "image/12call/btn_back_eng2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_return.src = "image/gsm/btn_back_eng2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_return.src = "image/corp/btn_back_eng2.png";
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
                }
            } else {
                if (txtNetworkType.value == 'pre') {
                    img_return.src = "image/12call/btn_back_eng.png";
                } else if (txtNetworkType.value == 'post') {
                    img_return.src = "image/gsm/btn_back_eng.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_return.src = "image/corp/btn_back_eng.png";
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
                }
            } else {

                if (txtNetworkType.value == 'pre') {
                    img_next.src = "image/12call/btn_next_eng2.png";
                } else if (txtNetworkType.value == 'post') {
                    img_next.src = "image/gsm/btn_next_eng2.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_next.src = "image/corp/btn_next_eng2.png";
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
                }
            } else {
                if (txtNetworkType.value == 'pre') {
                    img_next.src = "image/12call/btn_next_eng.png";
                } else if (txtNetworkType.value == 'post') {
                    img_next.src = "image/gsm/btn_next_eng.png";
                } else if (txtNetworkType.value == 'corp') {
                    img_next.src = "image/corp/btn_next_eng.png";
                }
            }


        }

    </script>

    <script type="text/javascript" src="<%= ResolveUrl("~/Bin/jquery.min.js")%>"></script>

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
                            <asp:Panel ID="Panel7" runat="server" BackImageUrl="~/image/bg_2.png" Height="359px"
                                Width="681px">
                                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                    <ContentTemplate>
                                        <table style="width: 100%; height: 357px;" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="height: 15px;">
                                                    <asp:Label ID="lbl_message1" runat="server"></asp:Label>
                                                    <asp:Label ID="lbl_time" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 5px; vertical-align: top; height: 254px;">
                                                    <div>
                                                        <asp:Panel ScrollBars="Vertical" runat="server" Height="250px" Width="670px">
                                                            <asp:UpdatePanel ID="updatepanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Panel ID="Pnl1" runat="server" Height="250px" Width="100%">
                                                                        <table border="0" cellpadding="0" cellspacing="0" class="tebleAppointment" style="width: 100%;
                                                                            height: 290px;">
                                                                            <tr>
                                                                                <td colspan="8" style="height: 250px; vertical-align: top;">
                                                                                    <table style="width: 100%">
                                                                                        <tr>
                                                                                            <td  id="td1" runat="server" width="15%">
                                                                                                <asp:Label ID="lbl_day1" runat="server" Style="width: 81px;"></asp:Label>
                                                                                                <br />
                                                                                                <asp:Label ID="lbl_date1" runat="server"></asp:Label>
                                                                                                <asp:Label ID="lbl_dateorginal1" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                         
                                                                                              <td>
                                                                                                  <asp:DataList ID="DataList1" runat="server" BorderStyle="None" 
                                                                                                      RepeatColumns="9" RepeatDirection="Horizontal" ShowFooter="False" 
                                                                                                      ShowHeader="False">
                                                                                                      <ItemStyle BorderStyle="None" />
                                                                                                      <ItemTemplate>
                                                                                                          <asp:Button ID="Button1" runat="server" Text='<%# bind("Time") %>' />
                                                                                                      </ItemTemplate>
                                                                                                  </asp:DataList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td  id="td2" runat="server">
                                                                                                <asp:Label ID="lbl_day2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                                <br />
                                                                                                <asp:Label ID="lbl_dateorginal2" runat="server" Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_date2" runat="server"></asp:Label>
                                                                                            </td>
                                                                                          <td>
                                                                                              <asp:DataList ID="DataList2" runat="server" BorderStyle="None" 
                                                                                                  RepeatColumns="9" RepeatDirection="Horizontal" ShowFooter="False" 
                                                                                                  ShowHeader="False">
                                                                                                  <ItemStyle BorderStyle="None" />
                                                                                                  <ItemTemplate>
                                                                                                      <asp:Button ID="Button2" runat="server" Text='<%# bind("Time") %>' />
                                                                                                  </ItemTemplate>
                                                                                              </asp:DataList>
                                                                                          </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td  id="td3" runat="server">
                                                                                                <asp:Label ID="lbl_day3" runat="server" Style="width: 81px;"></asp:Label>
                                                                                                <br />
                                                                                                <asp:Label ID="lbl_date3" runat="server"></asp:Label>
                                                                                                <asp:Label ID="lbl_dateorginal3" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                           <td>
                                                                                               <asp:DataList ID="DataList3" runat="server" BorderStyle="None" 
                                                                                                   RepeatColumns="9" RepeatDirection="Horizontal" ShowFooter="False" 
                                                                                                   ShowHeader="False">
                                                                                                   <ItemStyle BorderStyle="None" />
                                                                                                   <ItemTemplate>
                                                                                                       <asp:Button ID="Button2" runat="server" Text='<%# bind("Time") %>' />
                                                                                                   </ItemTemplate>
                                                                                               </asp:DataList>
                                                                                          </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td  id="td4" runat="server">
                                                                                                <asp:Label ID="lbl_day4" runat="server" Style="width: 81px;"></asp:Label>
                                                                                                <br />
                                                                                                <asp:Label ID="lbl_date4" runat="server"></asp:Label>
                                                                                                <asp:Label ID="lbl_dateorginal4" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        <td>
                                                                                            <asp:DataList ID="DataList4" runat="server" BorderStyle="None" 
                                                                                                RepeatColumns="9" RepeatDirection="Horizontal" ShowFooter="False" 
                                                                                                ShowHeader="False">
                                                                                                <ItemStyle BorderStyle="None" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:Button ID="Button3" runat="server" Text='<%# bind("Time") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:DataList>
                                                                                          </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td  id="td5" runat="server">
                                                                                                <asp:Label ID="lbl_day5" runat="server" Style="width: 81px;"></asp:Label>
                                                                                                <br />
                                                                                                <asp:Label ID="lbl_date5" runat="server"></asp:Label>
                                                                                                 <asp:Label ID="lbl_dateorginal5" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                           <td>
                                                                                               <asp:DataList ID="DataList5" runat="server" BorderStyle="None" 
                                                                                                   RepeatColumns="9" RepeatDirection="Horizontal" ShowFooter="False" 
                                                                                                   ShowHeader="False">
                                                                                                   <ItemStyle BorderStyle="None" />
                                                                                                   <ItemTemplate>
                                                                                                       <asp:Button ID="Button3" runat="server" Text='<%# bind("Time") %>' />
                                                                                                   </ItemTemplate>
                                                                                               </asp:DataList>
                                                                                          </td>
                                                                                        </tr>
                                                                                      
                                                                                        
                                                                                        <tr>
                                                                                            <td  id="td6" runat="server">
                                                                                                <asp:Label ID="lbl_day6" runat="server" Style="width: 81px;"></asp:Label>
                                                                                                <br />
                                                                                                <asp:Label ID="lbl_date6" runat="server"></asp:Label>
                                                                                                <asp:Label ID="lbl_dateorginal6" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                          <td>
                                                                                              <asp:DataList ID="DataList6" runat="server" BorderStyle="None" 
                                                                                                  RepeatColumns="9" RepeatDirection="Horizontal" ShowFooter="False" 
                                                                                                  ShowHeader="False">
                                                                                                  <ItemStyle BorderStyle="None" />
                                                                                                  <ItemTemplate>
                                                                                                      <asp:Button ID="Button4" runat="server" Text='<%# bind("Time") %>' />
                                                                                                  </ItemTemplate>
                                                                                              </asp:DataList>
                                                                                          </td>
                                                                                        </tr>
                                              
                                                                                        <tr>
                                                                                            <td  id="td7" runat="server">
                                                                                                <asp:Label ID="lbl_day7" runat="server" Style="width: 81px;"></asp:Label>
                                                                                                <br />
                                                                                                <asp:Label ID="lbl_date7" runat="server"></asp:Label>
                                                                                                <asp:Label ID="lbl_dateorginal7" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                         <td>
                                                                                          </td>
                                                                                        </tr>
                                                                                    
                                                                                        <tr>
                                                                                            <td  id="td8" runat="server" >
                                                                                                <asp:Label ID="lbl_day1_2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                                <br />
                                                                                                                <asp:Label ID="lbl_date1_2" runat="server"></asp:Label>
                                                                                                <asp:Label ID="lbl_dateorginal1_2" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                             <td>
                                                                                          </td>
                                                                                        </tr>
                                                                              
                                                                                        <tr>
                                                                                            <td  id="td9" runat="server">
                                                                                                <asp:Label ID="lbl_day2_2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                                <br />
                                                                                                   <asp:Label ID="lbl_date2_2" runat="server"></asp:Label>
                                                                                                <asp:Label ID="lbl_dateorginal2_2" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                          </td>
                                                                                        </tr>
  
                                                                                        <tr>
                                                                                            <td  id="td10" runat="server">
                                                                                                <asp:Label ID="lbl_day3_2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                                <br />
                                                                                                 <asp:Label ID="lbl_date3_2" runat="server"></asp:Label>
                                                                                                <asp:Label ID="lbl_dateorginal3_2" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                          <td>
                                                                                          </td>
                                                                                        </tr>
                                                                                   
                                                                                        <tr>
                                                                                            <td  id="td11" runat="server">
                                                                                                <asp:Label ID="lbl_day4_2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                                <br />
                                                                                                <asp:Label ID="lbl_date4_2" runat="server"></asp:Label>
                                                                                                <asp:Label ID="lbl_dateorginal4_2" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                         <td>
                                                                                          </td>
                                                                                        </tr>
                                                                               
                                                                                        <tr>
                                                                                            <td  id="td12" runat="server">
                                                                                                <asp:Label ID="lbl_day5_2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                                <br />
                                                                                                 <asp:Label ID="lbl_date5_2" runat="server"></asp:Label>
                                                                                                <asp:Label ID="lbl_dateorginal5_2" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                          </td>
                                                                                        </tr>
                                                                                  
                                                                                        <tr>
                                                                                            <td  id="td13" runat="server">
                                                                                                <asp:Label ID="lbl_day6_2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                                <br />
                                                                                                  <asp:Label ID="lbl_date6_2" runat="server"></asp:Label>
                                                                                                <asp:Label ID="lbl_dateorginal6_2" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                             <td>
                                                                                          </td>
                                                                                        </tr>
                                                                                   
                                                                                        <tr>
                                                                                            <td  id="td14" runat="server">
                                                                                                <asp:Label ID="lbl_day7_2" runat="server" Style="width: 81px;"></asp:Label>
                                                                                                <br />
                                                                                                  <asp:Label ID="lbl_date7_2" runat="server"></asp:Label>
                                                                                                <asp:Label ID="lbl_dateorginal7_2" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                          </td>
                                                                                        </tr>
                                                                                
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </asp:Panel>
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
                                                                <asp:ImageButton ID="img_return" runat="server" onmouseover="mouseOverImage_return()"
                                                                    onmouseout="mouseOutImage_return()" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="img_next" runat="server" onmouseover="mouseOverImage_next()"
                                                                    onmouseout="mouseOutImage_next()" />
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
    </div>
</asp:Content>
