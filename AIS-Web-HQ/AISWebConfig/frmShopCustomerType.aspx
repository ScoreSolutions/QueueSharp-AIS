<%@ Page Title="" Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false" CodeFile="frmShopCustomerType.aspx.vb" Inherits="frmShopCustomerType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register src="UserControls/ctlBranchSelected.ascx" tagname="ctlBranchSelected" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
<table style="width: 900px;">
    <tr>
        <td class="tableHeader">
            <asp:Label ID="lblScreenName" runat="server" Text="Shop Setup >> Customer Type "></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="height: 30px; text-align: center">
            <cc1:CustomUpdateProgress ID="progress" runat="server" ProgressImage="~/images/progress.gif" />
        </td>
    </tr>
    <tr>
            <td>
                 <uc1:ctlBranchSelected ID="ctlBranchSelected1" runat="server" />
            </td>
        </tr>
    <tr>
        <td class="dvInnerHeader">
            Configuration
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
            <td>
                <table style="width:700px;">
                    <tr>
                        <td style="height: 27px" class="dvtCellLabel">* Master Customer Type :</td>
                        <td class="dvtCellInfo">
                            <asp:DropDownList ID="ddlMasterCustomerType" runat="server" Width="150px" 
                                AutoPostBack="true" >
                            </asp:DropDownList>
                        </td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">&nbsp;
                            <asp:HiddenField ID="hidShopCustomerTypeID" runat="server" Value="0" />
                            </td>
                        <td class="dvtCellInfo" style="width: 150px;">&nbsp;</td>
                        <td class="dvtCellInfo" style="width: 150px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="dvtCellLabel">Code :</td>
                        <td class="dvtCellInfo">
                            <asp:TextBox ID="txt_code" runat="server" class="TextView" MaxLength="20"
                                OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)"
                                Width="150px" Height="20px" ></asp:TextBox>
                        </td>
                        <td class="dvtCellLabel">&nbsp;</td>
                        <td class="dvtCellInfo" >&nbsp;</td>
                        <td class="dvtCellInfo" >&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 27px" class="dvtCellLabel">
                            Name :
                        </td>
                        <td style="width: 76px" class="dvtCellInfo">
                            <asp:TextBox ID="txt_name" runat="server" class="TextView"  
                                OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)"
                                Width="150px" Height="20px" ></asp:TextBox>
                        </td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">
                            &nbsp;</td>
                        <td class="dvtCellInfo" style="width: 150px;">
                            &nbsp;</td>
                        <td class="dvtCellInfo" style="width: 150px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 27px" class="dvtCellLabel">
                            Queue No:
                        </td>
                        <td style="width: 76px" class="dvtCellInfo">
                            <table style="width: 80%;">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txt_queue_no_min" runat="server"  MaxLength="3"  
                                         Width="60px" Height="20px"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txt_queue_no_min_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" TargetControlID="txt_queue_no_min" FilterType="Numbers">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                    <td>-</td>
                                    <td>
                                        <asp:TextBox ID="txt_queue_no_max" runat="server"  MaxLength="3" 
                                            
                                            Width="60px" Height="20px"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="txt_queue_no_max_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" TargetControlID="txt_queue_no_max" FilterType="Numbers">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">
                            Text Queue :
                        </td>
                        <td class="dvtCellInfo" colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txt_queue" runat="server" class="TextView" MaxLength="1"
                                        OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)"
                                        Width="50px" Height="20px"  ></asp:TextBox>
                                    </td>
                                    <td>
                                       
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 27px" class="dvtCellLabel">
                            &nbsp;</td>
                        <td class="dvtCellInfo">
                            <asp:CheckBox ID="chk_IsAppointment" runat="server" Text="Appointment" 
                                Enabled="False" CausesValidation="True" />
                        </td>
                        <td style="width: 46px" class="dvtCellLabel">
                           Color :
                        </td>
                        <td>
                            <table style="width: 80%;">
                                <tr>
                                    <td class="dvtCellInfo">
                                        <asp:TextBox ID="txt_color" runat="server" class="detailedViewTextBox" Height="20px"
                                            OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)" ReadOnly="True"
                                            Width="20px"></asp:TextBox>
                                        <asp:TextBox ID="txt_colorcode" runat="server" BorderStyle="None" 
                                            BackColor="White" Width="52px" Enabled="false" ></asp:TextBox>
                                        <asp:ColorPickerExtender ID="txt_colorcode_ColorPickerExtender" runat="server" Enabled="True"
                                            TargetControlID="txt_colorcode" PopupButtonID="CmdColor_Picker" SampleControlID="txt_color" >
                                        </asp:ColorPickerExtender>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="CmdColor_Picker" runat="server" ImageUrl="~/images/color_picker.png" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 27px" class="dvtCellLabel">
                            &nbsp;</td>
                        <td class="dvtCellInfo">
                                <asp:CheckBox ID="chk_IsDefaultCustomerType" runat="server" 
                                    Text="Default Customer Type" Enabled="False" />
                        </td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">
                            &nbsp;</td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">&nbsp;</td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 162px;" class="dvtCellLabel">
                            &nbsp;</td>
                        <td class="dvtCellInfo">
                            <asp:CheckBox ID="chk_active" runat="server" Text="Active" Checked="true" Enabled="false" />
                        </td>
                        <td style="width: 46px" class="dvtCellLabel">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 162px; height: 27px" class="dvtCellLabel">
                             &nbsp;
                        </td>
                        <td class="dvtCellInfo">
                            &nbsp;</td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">
                            &nbsp;
                        </td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">
                            &nbsp;
                        </td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="CmdSave" runat="server" Text="Save" />
                <asp:Button ID="CmdClear" runat="server" Text="Clear" />
            </td>
        </tr>
        <tr><td colspan='2'>&nbsp;</td></tr>
        <tr><td colspan='2'>&nbsp;</td></tr>
        <tr>
            <td class="dvInnerHeader">
                Search Customer Type</td>
        </tr>
        <tr>
            <td colspan='2'>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: right;" class="dvtCellInfo">
                            <asp:Panel ID="Panel3" runat="server" Width="100%" Height="20px">
                                Shop : <asp:DropDownList ID="ddlSearchShop" runat="server" Width="150px" ></asp:DropDownList>
                                <asp:TextBox ID="txt_search" runat="server" class="detailedViewTextBox" MaxLength="100"
                                    onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                    Width="150px" Height="20px"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="txt_search_TextBoxWatermarkExtender" runat="server"
                                    Enabled="True" TargetControlID="txt_search" WatermarkText="Search">
                                </asp:TextBoxWatermarkExtender>
                                <asp:ImageButton ID="CmdSearch" runat="server" Style="margin-left: 0px" Width="16px"
                                    ImageUrl="~/images/search_lense.png" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: right;" class="dvtCellInfo">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: center;">
                            <asp:Label runat="server" ID="lblNotFound" Text="Data not found." ForeColor="Red"
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: right;" class="dvtCellInfo">
                            <saifi:mydg id="dgvCustomerType" runat="server" allowpaging="True" PageSize="10" autogeneratecolumns="False"
                                    cssclass="Grid_inq" imagefirst="/imgs/nav_left.gif" imagelast="/imgs/nav_right.gif"
                                    imagenext="/imgs/bulletr.gif" imageprevious="/imgs/bulletl.gif" showfirstandlastimage="False"
                                    showpreviousandnextimage="False" width="900px">
                                <AlternatingItemStyle CssClass="Grid_inqAltItem" />
                                <ItemStyle CssClass="Grid_inqItem" />
                                <PagerStyle Mode="NumericPages" CssClass="Grid_inqPager"></PagerStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Name Site" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">
                                        <FooterTemplate>
                                            <asp:Label ID="Label3" runat="server" Font-Bold="true" ForeColor="Red" Text="data not found"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_shop_id" runat="server" Style="text-align: left; width: 100%;"
                                                Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "shop_id")%>'></asp:Label>
                                            <asp:Label ID="lbl_shop_name" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "shop_name_en")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Code" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_id" runat="server" Style="text-align: left; width: 100%;" Visible="false"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                            <asp:Label ID="lbl_item_code" runat="server" Style="text-align: center; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "customertype_code")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_item_name" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "customertype_name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                    </asp:TemplateColumn>
                                  
                                    <asp:TemplateColumn HeaderText="Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkActie" runat="server" Enabled="false" Checked='<%#DataBinder.Eval(Container.DataItem, "active_status") = "1" %>' />
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit_images.jpg"
                                                CommandName="Edit" ToolTip="Edit" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle CssClass="Grid_inqHeader" />
                            </saifi:mydg>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td colspan='2'>&nbsp;</td></tr>
        <tr><td colspan='2'>&nbsp;</td></tr>
</table>
</asp:Content>

