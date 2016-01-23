<%@ Page Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false" EnableEventValidation="false"
    CodeFile="frmMSTSoftwareSetupKiosk.aspx.vb" Inherits="frmMSTSoftwareSetupKiosk"  %>

<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="MycustomDG" namespace="MycustomDG" tagprefix="Saifi" %>
<%@ Register src="UserControls/ctlBrowseFile.ascx" tagname="ctlBrowseFile" tagprefix="uc1" %>
<%@ Register src="UserControls/ctlShopSelected.ascx" tagname="ctlShopSelected" tagprefix="uc2" %>
<%@ Register src="UserControls/ctlBrowseFileStream.ascx" tagname="ctlBrowseFileStream" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td class="tableHeader">
                <asp:Label ID="lblScreenName" runat="server" Text="Software Setup >> Kiosk"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td class="dvInnerHeader">
                Apply Configuration to Shop
            </td>
        </tr>
        <tr>
            <td>
                <uc2:ctlShopSelected ID="ctlShopSelected1" runat="server" HeaderLeft="Selected Shop"
                    HeaderRight="Unselected Shop" />
            </td>
        </tr>
        <tr>
            <td style="height: 19px" align="left" >
                <asp:TabContainer ID="TabContainer1" runat="server"   AutoPostBack="True"
                    Width="896px" EnableTheming="False" style="margin-bottom: 49px" 
                    ActiveTabIndex="0" >
                    <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                        <HeaderTemplate>
                            Hardware
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td class="dvInnerHeader">
                                        Configuration
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 216px; height: 27px" class="dvtCellLabel">
                                                    Soonest Arrival for Appointment :
                                                </td>
                                                <td style="width: 231px; height: 27px" class="dvtCellInfo" >
                                                    <asp:TextBox ID="txtSoonestArrivalAppointment" runat="server" Height="20px" class="TextView"
                                                         OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)" ></asp:TextBox>
                                                    <asp:NumericUpDownExtender ID="numSoonestArrivalAppointment" runat="server"
                                                        Enabled="True" Maximum="1200" Minimum="0"  RefValues="" 
                                                        ServiceDownMethod="" ServiceDownPath="" 
                                                        ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtSoonestArrivalAppointment"
                                                        Width="60" >
                                                    </asp:NumericUpDownExtender>
                                                    Minute
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dvtCellLabel">
                                                    Latest Arrival for Appointment :
                                                </td>
                                                <td class="dvtCellInfo" >
                                                    <asp:TextBox ID="txtLatestArrivalAppointment" runat="server" Height="20px" class="detailedViewTextBox"
                                                         onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'" 
                                                         OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)" ></asp:TextBox>
                                                    <asp:NumericUpDownExtender ID="numLatestArrivalAppointment" runat="server"
                                                        Enabled="True" Maximum="300" Minimum="0"  RefValues="" 
                                                        ServiceDownMethod="" ServiceDownPath="" 
                                                        ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtLatestArrivalAppointment"
                                                        Width="60" >
                                                    </asp:NumericUpDownExtender>
                                                    Minute
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dvtCellLabel">
                                                    Max Services Appointment per time :
                                                </td>
                                                <td class="dvtCellInfo" >
                                                    <asp:TextBox ID="txtMaxServiceAppointment" runat="server" Height="20px" class="detailedViewTextBox"
                                                         onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                                         OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)"  ></asp:TextBox>
                                                    <asp:NumericUpDownExtender ID="numMaxServiceAppointment" runat="server"
                                                        Enabled="True" Maximum="10" Minimum="0"  RefValues="" ServiceDownMethod="" ServiceDownPath="" 
                                                        ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtMaxServiceAppointment"
                                                        Width="60" >
                                                    </asp:NumericUpDownExtender>
                                                    Service
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dvtCellLabel">
                                                    Pre-booking Period :
                                                </td>
                                                <td class="dvtCellInfo" >
                                                    <asp:TextBox ID="txtPreBookingPeriod" runat="server" Height="20px" class="TextView"
                                                         OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)" ></asp:TextBox>
                                                    <asp:NumericUpDownExtender ID="numPreBookingPeriod" runat="server"
                                                        Enabled="True" Maximum="1000" Minimum="0"  RefValues="" ServiceDownMethod="" ServiceDownPath="" 
                                                        ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtPreBookingPeriod"
                                                        Width="60" Step="15" >
                                                    </asp:NumericUpDownExtender>
                                                    Minute
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dvtCellLabel">
                                                    Cancel before Reservation Time :
                                                </td>
                                                <td  class="dvtCellInfo" >
                                                    <asp:TextBox ID="txtCancelBeforeReservationTime" runat="server" Height="20px" class="TextView"
                                                         OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)" ></asp:TextBox>
                                                    <asp:NumericUpDownExtender ID="numCancelBeforeReservationTime" runat="server"
                                                        Enabled="True" Maximum="900" Minimum="0"  RefValues="" ServiceDownMethod="" ServiceDownPath="" 
                                                        ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtCancelBeforeReservationTime"
                                                        Width="60" Step="15" >
                                                    </asp:NumericUpDownExtender>
                                                    Minute
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dvtCellLabel">
                                                    No Print if W.T is more than(Minute) :
                                                </td>
                                                <td class="dvtCellInfo">
                                                    <asp:TextBox ID="txtNoPrint" runat="server" class="detailedViewTextBox" 
                                                        onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                                        OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)" 
                                                        Width="150px" Height="20px"></asp:TextBox>
                                                    <asp:NumericUpDownExtender ID="txt_No_Print_NumericUpDownExtender"
                                                        runat="server" Enabled="True" Maximum="100" Minimum="0" RefValues="" ServiceDownMethod=""
                                                        ServiceDownPath="" ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID=""
                                                        TargetControlID="txtNoPrint" Width="60">
                                                    </asp:NumericUpDownExtender>
                                                    Minute
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dvtCellLabel">
                                                    Maximum Services chosen per Time :
                                                </td>
                                                <td class="dvtCellInfo">
                                                    <asp:TextBox ID="txtMaximumServicePerTime" runat="server" class="detailedViewTextBox" 
                                                        onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                                        OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)" 
                                                        Width="150px" Height="20px"></asp:TextBox>
                                                    <asp:NumericUpDownExtender ID="numMaximumServicePerTime"
                                                        runat="server" Enabled="True" Maximum="100" Minimum="0" RefValues="" ServiceDownMethod=""
                                                        ServiceDownPath="" ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID=""
                                                        TargetControlID="txtMaximumServicePerTime" Width="60">
                                                    </asp:NumericUpDownExtender>
                                                    Service
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dvtCellLabel">
                                                    Refresh W.T every :
                                                </td>
                                                <td class="dvtCellInfo">
                                                    <asp:TextBox ID="txtRefreshWT" runat="server" class="detailedViewTextBox" 
                                                        onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                                        OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)" 
                                                        Width="150px" Height="20px"></asp:TextBox>
                                                    <asp:NumericUpDownExtender ID="numRefreshWT"
                                                        runat="server" Enabled="True" Maximum="100" Minimum="0" RefValues="" ServiceDownMethod=""
                                                        ServiceDownPath="" ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID=""
                                                        TargetControlID="txtRefreshWT" Width="60">
                                                    </asp:NumericUpDownExtender>
                                                    Second
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dvtCellLabel">
                                                    Show Video every :
                                                </td>
                                                <td class="dvtCellInfo" >
                                                    <asp:TextBox ID="txt_Show_Video_every" runat="server" class="detailedViewTextBox"
                                                        onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                                        OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)" 
                                                        Width="150px" Height="20px"></asp:TextBox>
                                                    <asp:NumericUpDownExtender ID="txt_Show_Video_every1_NumericUpDownExtender"
                                                        runat="server" Enabled="True" Maximum="100" Minimum="0" RefValues="" ServiceDownMethod=""
                                                        ServiceDownPath="" ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID=""
                                                        TargetControlID="txt_Show_Video_every" Width="60">
                                                    </asp:NumericUpDownExtender>
                                                    Minute
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dvtCellLabel"  >
                                                    Length of Mobile No :
                                                </td>
                                                <td class="dvtCellInfo">
                                                    <asp:TextBox ID="txt_Length_of_Mobile_No" runat="server" class="detailedViewTextBox"
                                                        onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                                        OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)" 
                                                        Width="150px" Height="20px"  ></asp:TextBox>
                                                    <asp:NumericUpDownExtender ID="txt_Length_of_Mobile_No_NumericUpDownExtender"
                                                        runat="server" Enabled="True" Maximum="12" Minimum="0" RefValues="" ServiceDownMethod=""
                                                        ServiceDownPath="" ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID=""
                                                        TargetControlID="txt_Length_of_Mobile_No" Width="60">
                                                    </asp:NumericUpDownExtender>
                                                    Digit
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dvtCellLabel" >
                                                    Allowable Mobile Prefix :
                                                </td>
                                                <td>
                                                    <table style="width: 50%;" >
                                                        <tr>
                                                            <td style="width: 40px; height: 27px" class="dvtCellInfo">
                                                                <asp:TextBox ID="txt_Allowable1" runat="server" Width="30px" Height="20px" class="detailedViewTextBox"
                                                                    MaxLength="2" onblur="this.className='detailedViewTextBox'" 
                                                                    onfocus="this.className='detailedViewTextBoxOn'"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txt_Allowable1_FilteredTextBoxExtender" 
                                                                    runat="server" Enabled="True" TargetControlID="txt_Allowable1" FilterType="Numbers">
                                                                </asp:FilteredTextBoxExtender>
                                                            </td>
                                                            <td style="width: 40px; height: 27px" class="dvtCellInfo">
                                                                <asp:TextBox ID="txt_Allowable2" runat="server" Width="30px" Height="20px" class="detailedViewTextBox"
                                                                    MaxLength="2" onblur="this.className='detailedViewTextBox'" 
                                                                    onfocus="this.className='detailedViewTextBoxOn'"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txt_Allowable2_FilteredTextBoxExtender" 
                                                                    runat="server" Enabled="True" TargetControlID="txt_Allowable2" FilterType="Numbers">
                                                                </asp:FilteredTextBoxExtender>
                                                            </td>
                                                            <td style="width: 40px; height: 27px" class="dvtCellInfo">
                                                                <asp:TextBox ID="txt_Allowable3" runat="server" Width="30px" Height="20px" class="detailedViewTextBox"
                                                                    MaxLength="2" onblur="this.className='detailedViewTextBox'" 
                                                                    onfocus="this.className='detailedViewTextBoxOn'"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txt_Allowable3_FilteredTextBoxExtender" 
                                                                    runat="server" Enabled="True" TargetControlID="txt_Allowable3" FilterType="Numbers">
                                                                </asp:FilteredTextBoxExtender>
                                                            </td>
                                                            <td style="width: 40px; height: 27px" class="dvtCellInfo">
                                                                <asp:TextBox ID="txt_Allowable4" runat="server" Width="30px" Height="20px" class="detailedViewTextBox"
                                                                    MaxLength="2" onblur="this.className='detailedViewTextBox'" 
                                                                    onfocus="this.className='detailedViewTextBoxOn'"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txt_Allowable4_FilteredTextBoxExtender" 
                                                                    runat="server" Enabled="True" TargetControlID="txt_Allowable4" FilterType="Numbers">
                                                                </asp:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                           
                                        </table>
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td class="dvInnerHeader">
                                        Video
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 24px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="dvtCellLabel" style="width: 161px; height: 27px">
                                                    Chosen Video :
                                                </td>
                                                <td class="dvtCellInfo" style="width: 150px; height: 27px">
                                                    <uc3:ctlBrowseFileStream ID="ctlBrowseFileVdo1" runat="server" 
                                                                    VisibleUploadButton="false" />
                                                </td>
                                                <td colspan="4">   
                                                      <asp:Label ID="lbl_upload4" runat="server"></asp:Label>
                                                      Frame Size : 320x240pixels  File Extension : *.wmv
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td></tr>
                                
                                 <tr>
                                    <td class="dvInnerHeader">
                                        Service Banner
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 24px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="dvtCellLabel" style="width: 161px; height: 27px">
                                                    Chosen Service Banner :
                                                </td>
                                                <td class="dvtCellInfo" style="width: 150px; height: 27px">
                                                    <uc1:ctlBrowseFile ID="ctlBrowseFileBranner" runat="server" VisibleUploadButton="false" />
                                                </td>
                                                <td colspan="4">   
                                                     <asp:Label ID="lbl_Upload_Branner" runat="server" ></asp:Label>
                                                     Image Size : 660x190pixels  File Extension : *.png
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td></tr>
                                
                                <tr>
                                    <td class="dvInnerHeader">
                                        Ticket
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 24px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="dvtCellLabel" style="width: 161px; height: 27px">
                                                    Header Thai :
                                                </td>
                                                <td class="dvtCellInfo" style="width: 150px; height: 27px">
                                                    <uc1:ctlBrowseFile ID="ctlBrowseFileTicketHeader" runat="server" VisibleUploadButton="false" />
                                                </td>
                                                <td colspan="4">   
                                                     <asp:Label ID="lbl_header" runat="server" ></asp:Label>
                                                     Image Size : 167x72pixels  File Extension : *.bmp
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dvtCellLabel" style="width: 161px; height: 27px">
                                                    Header Eng :
                                                </td>
                                                <td class="dvtCellInfo" style="width: 150px; height: 27px">
                                                    <uc1:ctlBrowseFile ID="ctlBrowseFileTicketHeaderEng" runat="server" VisibleUploadButton="false" />
                                                </td>
                                                <td colspan="4">   
                                                     <asp:Label ID="lbl_header_eng" runat="server" ></asp:Label>
                                                     Image Size : 167x72pixels  File Extension : *.bmp
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td></tr>
                                
                                
                                <tr>
                                    <td class="dvInnerHeader">
                                        Logo
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 24px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="dvtCellLabel" style="width: 161px; height: 27px">
                                                    Kiosk Logo :
                                                </td>
                                                <td class="dvtCellInfo" style="width: 150px; height: 27px">
                                                    <uc1:ctlBrowseFile ID="ctlBrowseFileKioskLogo" runat="server" VisibleUploadButton="false" />
                                                </td>
                                                <td colspan="4">   
                                                     <asp:Label ID="Label1" runat="server" ></asp:Label>
                                                     Image Size : 191x78pixels  File Extension : *.png
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dvtCellLabel" style="width: 161px; height: 27px">
                                                    Kiosk Elo Logo :
                                                </td>
                                                <td class="dvtCellInfo" style="width: 150px; height: 27px">
                                                    <uc1:ctlBrowseFile ID="ctlBrowseFileKioskEloLogo" runat="server" VisibleUploadButton="false" />
                                                </td>
                                                <td colspan="4">   
                                                     <asp:Label ID="Label2" runat="server" ></asp:Label>
                                                     Image Size : 322x140pixels  File Extension : *.jpg
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td></tr>
                                
                                
                                <tr>
                                    <td class="dvInnerHeader">
                                        Event
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 24px">
                                        <table style="width: 100%; height: 57px;">
                                            <tr style="display:none" >
                                                <td class="dvtCellLabel" style="width: 100px; height: 27px">
                                                            <asp:RadioButton ID="opt_now" runat="server" Text="Now" GroupName="Event" />
                                                </td>
                                                <td style="width: 260px; height: 27px;">
                                                </td>
                                                <td style="height: 27px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dvtCellLabel" style="width: 100px; height: 27px">
                                                            <asp:RadioButton ID="opt_schedule" runat="server" Text="Schedule" 
                                                                GroupName="Event" Checked="True"  />
                                                </td>
                                                <td style="width: 260px">
                                                    <table style="width: 39%; height: 31px;">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txt_date" runat="server" Enabled="False"></asp:TextBox>
                                                                <asp:CalendarExtender ID="txt_date_CalendarExtender" runat="server" Enabled="True"
                                                                    Format="dd/MM/yyyy" PopupButtonID="imgcalendar" TargetControlID="txt_date">
                                                                </asp:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="imgcalendar" runat="server" ImageUrl="~/images/calender_icon_score.jpg" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 260px">
                                                    <asp:Button ID="btn_save" runat="server" Text="Save" />
                                                    <asp:Button ID="btn_clear" runat="server" Text="Clear" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                        <HeaderTemplate>
                            View
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width:750px;"  class="table_view">
                                            <tr style="height:20px">
                                                <td colspan="11" align="center" >
                                                    <Saifi:MyDg ID="dgvdetail" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                        Width="850px" AllowPaging="True" ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif"
                                                        ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif" PageSize="200"
                                                        ShowFirstAndLastImage="False" ShowPreviousAndNextImage="False">
                                                        <Columns>
                                                            <asp:TemplateColumn SortExpression="Shop Size" HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                Shop Size<br/><br />
                                                                    <asp:DropDownList ID="ddshop_size" AutoPostBack="true" Width="100%" runat="server" OnSelectedIndexChanged="ddshop_size_SelectedIndexChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Shop_Size" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "shop_size")%>'
                                                                        Width="100%"></asp:Label></ItemTemplate>
                                                                <HeaderStyle Width="95px"/>
                                                                <ItemStyle HorizontalAlign="Center" Width="95px" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn SortExpression="shop_Name" HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                Shop Name<br /><br />
                                                                    <asp:DropDownList ID="ddshop_name" AutoPostBack="true" Width="100%" runat="server" OnSelectedIndexChanged="ddshop_name_SelectedIndexChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbl_shop_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "shop_Name")%>'
                                                                        Width="100%" OnClick="lbl_shop_Name_Click"> </asp:LinkButton><asp:Label ID="lbl_shop_code"
                                                                            runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "shop_code")%>' Visible="false"></asp:Label><asp:Label
                                                                                ID="lbl_id" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'
                                                                                Visible="false"></asp:Label></ItemTemplate>
                                                                <HeaderStyle Width="250px" />
                                                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn SortExpression="max_service" HeaderStyle-HorizontalAlign="Center">
                                                           
                                                                <HeaderTemplate>
                                                                 Maximum Service Chosen <br />Per Time (Service)<br />
                                                                    <asp:DropDownList ID="ddmax_service" AutoPostBack="true" Width="100%" runat="server"
                                                                         OnSelectedIndexChanged="ddmax_service_SelectedIndexChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_max_service" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "max_service")%>'
                                                                        Width="100%"></asp:Label></ItemTemplate>
                                                                <HeaderStyle Width="95px" />
                                                                <ItemStyle HorizontalAlign="Center" Width="95px" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn SortExpression="kiosk_wt_noprint" HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                No Print if W.T is <br />more than (Minute)<br />
                                                                    <asp:DropDownList ID="ddkiosk_wt_noprint" AutoPostBack="true" Width="100%" runat="server"
                                                                        OnSelectedIndexChanged="ddkiosk_wt_noprint_SelectedIndexChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_kiosk_wt_noprint" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kiosk_wt_noprint")%>'
                                                                        Width="100%"></asp:Label></ItemTemplate>
                                                                <HeaderStyle Width="95px" />
                                                                <ItemStyle HorizontalAlign="Center" Width="95px" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn SortExpression="ddkiosk_show_video" HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                Show video <br /> Every (Minute)<br />
                                                                    <asp:DropDownList ID="ddkiosk_show_video" AutoPostBack="true" Width="100%" runat="server"
                                                                        OnSelectedIndexChanged="ddkiosk_show_video_SelectedIndexChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_kiosk_show_video" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kiosk_show_video")%>'
                                                                        Width="100%"></asp:Label></ItemTemplate>
                                                                <HeaderStyle Width="95px" />
                                                                <ItemStyle HorizontalAlign="Center" Width="95px" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn SortExpression="kiosk_langth_mobile" HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                Length of <br />Mobile no (Digit)<br />
                                                                    <asp:DropDownList ID="ddkiosk_langth_mobile" AutoPostBack="true" Width="100%" runat="server"
                                                                       OnSelectedIndexChanged="ddkiosk_langth_mobile_SelectedIndexChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_kiosk_langth_mobile" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kiosk_langth_mobile")%>'
                                                                        Width="100%"></asp:Label></ItemTemplate>
                                                                <HeaderStyle Width="95px" />
                                                                <ItemStyle HorizontalAlign="Center" Width="95px" />
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                        <PagerStyle Mode="NumericPages" Visible="False" />
                                                    </Saifi:MyDg>
                                                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Text="User Cannot Authorize Shop" Visible="false"></asp:Label>
                                                </td>
                                                
                                            </tr>
                                        </table>
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
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
