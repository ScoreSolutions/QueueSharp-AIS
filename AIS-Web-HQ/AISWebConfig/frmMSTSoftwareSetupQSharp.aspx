<%@ Page Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false" EnableEventValidation="false"
    CodeFile="frmMSTSoftwareSetupQSharp.aspx.vb" Inherits="frmMSTSoftwareSetupQSharp" %>

<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="MycustomDG" namespace="MycustomDG" tagprefix="Saifi" %>
<%@ Register src="UserControls/ctlBrowseFile.ascx" tagname="ctlBrowseFile" tagprefix="uc1" %>
<%@ Register src="UserControls/ctlShopSelected.ascx" tagname="ctlShopSelected" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td class="tableHeader">
                <asp:Label ID="lblScreenName" runat="server" Text="Software Setup >> Queue Sharp"></asp:Label>
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
                                                    Refresh Data every :
                                                </td>
                                                <td class="dvtCellInfo" >
                                                    <asp:TextBox ID="txtRefreshDataSec" runat="server" Height="20px" class="detailedViewTextBox"
                                                         onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                                         OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)"  ></asp:TextBox>
                                                    <asp:NumericUpDownExtender ID="numRefreshDataSec" runat="server"
                                                        Enabled="True" Maximum="300" Minimum="0"  RefValues="" 
                                                        ServiceDownMethod="" ServiceDownPath="" 
                                                        ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtRefreshDataSec"
                                                        Width="60" >
                                                    </asp:NumericUpDownExtender>
                                                    Second
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dvtCellLabel">
                                                    &nbsp;
                                                </td>
                                                <td class="dvtCellInfo"  >
                                                    <asp:CheckBox ID="chkUseLdap" runat="server" Text="Use LDAP to login for all user" Checked="true" />
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
                                                            <asp:RadioButton ID="opt_schedule" runat="server" Text="Schedule" GroupName="Event" Checked="true"  />
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
                                        <table border="0" cellpadding="0" cellspacing="0"  style="width:500px;"  class="table_view">
                                            <tr style="height:20px">
                                                <td  colspan="3" align="center">
                                                    <Saifi:MyDg ID="dgvdetail" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                        Width="100%" AllowPaging="True" ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif"
                                                        ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif" PageSize="200"
                                                        ShowFirstAndLastImage="False" ShowPreviousAndNextImage="False">
                                                        <Columns>
                                                        <asp:TemplateColumn SortExpression="Shop Size" HeaderStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate  >
                                                            Shop Size<br />
                                                                <asp:DropDownList ID="ddshop_size" AutoPostBack="true" Width="100%" runat="server"
                                                                    OnSelectedIndexChanged="ddshop_size_SelectedIndexChanged" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Shop_Size" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "shop_size")%>' Width="100px">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="100px" />
                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                        </asp:TemplateColumn>
                                                            <asp:TemplateColumn SortExpression="shop_Name" HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                Shop Name<br />
                                                                    <asp:DropDownList ID="ddshop_name" AutoPostBack="true" Width="100%" runat="server"
                                                                         OnSelectedIndexChanged="ddshop_name_SelectedIndexChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbl_shop_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "shop_Name")%>'
                                                                        Width="290px" OnClick="lbl_shop_Name_Click"> </asp:LinkButton><asp:Label ID="lbl_shop_code"
                                                                            runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "shop_code")%>' Visible="false"></asp:Label><asp:Label
                                                                                ID="lbl_id" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'
                                                                                Visible="false"></asp:Label></ItemTemplate>
                                                                <HeaderStyle Width="290px" />
                                                                <ItemStyle HorizontalAlign="Left" Width="290px" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn SortExpression="refresh_data" HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                Refresh Data Every (Second)<br />
                                                                    <asp:DropDownList ID="ddrefresh_data" AutoPostBack="true" Width="100%" runat="server"
                                                                        OnSelectedIndexChanged="ddrefresh_data_SelectedIndexChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_refresh_data" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "refresh_data")%>'
                                                                        Width="100px"></asp:Label></ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
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
