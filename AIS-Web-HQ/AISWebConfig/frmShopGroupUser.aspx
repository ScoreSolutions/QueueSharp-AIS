<%@ Page Title="" Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false" CodeFile="frmShopGroupUser.aspx.vb" Inherits="frmShopGroupUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register src="UserControls/ctlBranchSelected.ascx" tagname="ctlBranchSelected" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
<table style="width: 900px;">
    <tr>
        <td class="tableHeader">
            <asp:Label ID="lblScreenName" runat="server" Text="Shop Setup >> Group User"></asp:Label>
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
            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                <tr>
                    <td style="height: 30px;width:30px" class="dvtCellLabel">* Group Code :</td>
                    <td  class="dvtCellInfo">
                        <asp:TextBox ID="txtGroupCode" runat="server" class="detailedViewTextBox" MaxLength="100"
                            onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                            Width="200px" Height="20px"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="txtGroupCodeTextBoxWatermarkExtender" runat="server"
                            Enabled="True" TargetControlID="txtGroupCode" WatermarkText="Group Code">
                        </asp:TextBoxWatermarkExtender>
                        <asp:HiddenField ID="hidGroiupUserID" runat="server" Value="0" />
                    </td>
                </tr>
                <tr>
                    <td class="dvtCellLabel"> * Group Name :</td>
                    <td class="dvtCellInfo">
                        <asp:TextBox ID="txtGroupName" runat="server" class="detailedViewTextBox" MaxLength="100"
                            onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                            Width="200px" Height="20px"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="txtGroupNameTextBoxWatermarkExtender" runat="server"
                            Enabled="True" TargetControlID="txtGroupName" WatermarkText="Group Name">
                        </asp:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td class="dvtCellLabel">
                        &nbsp;
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chk_active" runat="server" Text="Active" Checked="true"  />
                    </td>
                </tr>
                <tr><td colspan="2">&nbsp;</td></tr>
                <tr><td colspan="2">&nbsp;</td></tr>
                <tr>
                    <td colspan="2">
                        <Saifi:MyDg ID="dgvMenu" runat="server"  Width="265px" AllowPaging="false"
                            AutoGenerateColumns="False" ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif"
                            ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif" ShowFirstAndLastImage="False"
                            ShowPreviousAndNextImage="False" BorderStyle="None" 
                            CssClass="Grid_DetailItem">
                            <AlternatingItemStyle CssClass="Grid_Detail" />
                            <ItemStyle CssClass="Grid_Detail" />
                            <PagerStyle Mode="NumericPages" CssClass=""></PagerStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="Menu Type" >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" /> 
                                        <asp:Label ID="lbl_menu_type" runat="server" 
                                            Text='<%#DataBinder.Eval(Container.DataItem, "menu_type")%>'></asp:Label>
                                        <asp:Label ID="lbl_id" runat="server" Visible="false" 
                                            Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" Width="150px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn  HeaderText="Menu Name" >
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_menu_name" runat="server" 
                                            Text='<%#DataBinder.Eval(Container.DataItem, "menu_name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" Width="150px" />
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle CssClass="Grid_DetailHeader"   />
                        </Saifi:MyDg>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" >
            <asp:Button ID="CmdSave" runat="server" Text="Save" />
            <asp:Button ID="CmdClear" runat="server" Text="Clear" />
        </td>
    </tr>
    <tr><td colspan='2'>&nbsp;</td></tr>
    <tr><td colspan='2'>&nbsp;</td></tr>
    <tr>
        <td class="dvInnerHeader">
            Search Group User
        </td>
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
                        <saifi:mydg id="dgvGroupUser" runat="server" allowpaging="True" PageSize="10" autogeneratecolumns="False"
                                cssclass="Grid_inq" imagefirst="/imgs/nav_left.gif" imagelast="/imgs/nav_right.gif"
                                imagenext="/imgs/bulletr.gif" imageprevious="/imgs/bulletl.gif" showfirstandlastimage="False"
                                showpreviousandnextimage="False" width="900px">
                            <AlternatingItemStyle CssClass="Grid_inqAltItem" />
                            <ItemStyle CssClass="Grid_inqItem" />
                            <PagerStyle Mode="NumericPages" CssClass="Grid_inqPager"></PagerStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="Shop Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">
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
                                <asp:TemplateColumn HeaderText="Group Code" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_id" runat="server" Style="text-align: left; width: 100%;" Visible="false"
                                            Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                        <asp:Label ID="lbl_group_code" runat="server" Style="text-align: center; width: 100%;"
                                            Text='<%#DataBinder.Eval(Container.DataItem, "group_code")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Group Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_group_name" runat="server" Style="text-align: left; width: 100%;"
                                            Text='<%#DataBinder.Eval(Container.DataItem, "group_name")%>'></asp:Label>
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

