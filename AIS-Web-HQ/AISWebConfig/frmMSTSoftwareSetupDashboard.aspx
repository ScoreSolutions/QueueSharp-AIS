<%@ Page Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false" EnableEventValidation="false"
CodeFile="frmMSTSoftwareSetupDashboard.aspx.vb" Inherits="frmMSTSoftwareSetupDashboard" title="Untitled Page" %>

<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="MycustomDG" namespace="MycustomDG" tagprefix="Saifi" %>
<%@ Register src="UserControls/ctlBrowseFile.ascx" tagname="ctlBrowseFile" tagprefix="uc3" %>
<%@ Register src="UserControls/ctlShopSelected.ascx" tagname="ctlShopSelected" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
 <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td class="tableHeader">
                <asp:Label ID="lblScreenName" runat="server" Text="Software Setup >> Dashboard"></asp:Label>
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
                                                <td class="dvtCellLabel" style="width: 183px">
                                                    Logo :
                                                </td>
                                                <td class="dvtCellInfo">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                           
                                                            <td>
                                                                <uc3:ctlBrowseFile ID="ctlBrowseFileLogo" runat="server" VisibleUploadButton="false" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                Image Size : 1772x724pixels  File Extension : *.png
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            
                                             
                                            
                                        </table>
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td></tr>
                                <tr>
                                    <td class="dvInnerHeader">
                                        Apply Configuration to Shop
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <uc2:ctlShopSelected ID="ctlShopSelected1" runat="server" HeaderLeft="Selected Shop" HeaderRight="Unselected Shop" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dvInnerHeader">
                                        Event
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 24px">
                                        <table style="width: 100%; height: 57px;">
                                            <tr style="display:none">
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
                    
       
                    
                </asp:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>

