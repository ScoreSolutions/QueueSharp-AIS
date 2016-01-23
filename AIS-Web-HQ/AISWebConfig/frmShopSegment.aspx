<%@ Page Title="" Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false"
    CodeFile="frmShopSegment.aspx.vb" Inherits="frmShopSegment" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register Src="UserControls/ctlBranchSelected.ascx" TagName="ctlBranchSelected"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="Server">
    <table style="width: 900px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="tableHeader">
                <asp:Label ID="Label2" runat="server" Text="Shop Setup >> Segment"></asp:Label>
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
                <table style="width: 500px;">
                    <tr>
                        <td class="dvtCellLabel">
                            Customer Type :
                        </td>
                        <td style="width: 19px" class="dvtCellInfo">
                            <asp:DropDownList ID="ddlCustomerType" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">
                            &nbsp;
                            <asp:HiddenField ID="hidSegmentID" runat="server" Value="0" />
                        </td>
                    </tr>
                    <tr>
                        <td class="dvtCellLabel">
                            Segment:
                        </td>
                        <td class="dvtCellInfo" id="txt_Segment">
                            <asp:TextBox ID="txt_Segment" runat="server"></asp:TextBox>
                        </td>
                        <td class="dvtCellLabel">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 162px; height: 27px" class="dvtCellLabel">
                            &nbsp;
                        </td>
                        <td style="width: 19px" class="dvtCellInfo">
                            <asp:CheckBox ID="chk_active" runat="server" Text="Active" Checked="true" />
                        </td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            
            <td style="padding-left:150px">
                <asp:Button ID="CmdSave" runat="server" Text="Save" />
                <asp:Button ID="CmdClear" runat="server" Text="Clear" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="dvInnerHeader">
                Search Segment
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: Right;" class="dvtCellInfo">
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
                            <Saifi:MyDg ID="dgvService" runat="server" AllowPaging="True" PageSize="10" AutoGenerateColumns="False"
                                CssClass="Grid_inq" ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif"
                                ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif" ShowFirstAndLastImage="False"
                                ShowPreviousAndNextImage="False" Width="100%">
                                <AlternatingItemStyle CssClass="Grid_inqAltItem" />
                                <ItemStyle CssClass="Grid_inqItem" />
                                <PagerStyle Mode="NumericPages" CssClass="Grid_inqPager"></PagerStyle>
                                <Columns>
                                            <asp:TemplateColumn HeaderText="Shop" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                           
                                             <asp:Label ID="lbl_shop_id" runat="server" Style="text-align: center; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "shop_id")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_shop_name_en" runat="server" Style="text-align: center; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "shop_name_en")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Segment" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_id" runat="server" Style="text-align: left; width: 100%;" Visible="false"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                            <asp:Label ID="lbl_Segment" runat="server" Style="text-align: center; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "Segment")%>'></asp:Label>
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
                            </Saifi:MyDg>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
