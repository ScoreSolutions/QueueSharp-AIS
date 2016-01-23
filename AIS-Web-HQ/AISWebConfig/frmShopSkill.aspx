<%@ Page Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false" CodeFile="frmShopSkill.aspx.vb" Inherits="frmShopSkill" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="MycustomDG" namespace="MycustomDG" tagprefix="Saifi" %>
<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register src="UserControls/ctlBranchSelected.ascx" tagname="ctlBranchSelected" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
  
    <table style="width:900px;">
        <tr> 
            <td  class="tableHeader">
                <asp:Label ID="lblScreenName" runat="server" Text="Shop Setup >> Skill"></asp:Label>
            </td>
        </tr>
        <uc1:ctlBranchSelected ID="ctlBranchSelected1" runat="server" />
        <tr>
            <td style="height: 30px;  text-align:center">
                <cc1:CustomUpdateProgress ID="progress" runat="server"  ProgressImage="~/images/progress.gif" />
            </td>
        </tr>
        <tr>
            <td class="dvInnerHeader">Configuration</td>
        </tr>
        <tr>
            <td style="height: 23px">
                <table border="0" cellpadding="0" cellspacing="0" style="width:79%;">
                    <tr>
                        <td style="height: 27px" class="dvtCellLabel">* Master Skill :</td>
                        <td style="width: 19px" class="dvtCellInfo">
                            <asp:DropDownList ID="ddlMasterSkill" runat="server" Width="150px" AutoPostBack="true" >
                            </asp:DropDownList>
                        </td>
                        <td class="dvtCellLabel">&nbsp;</td>
                        <td class="dvtCellInfo" style="width: 150px;">&nbsp;</td>
                        <td class="dvtCellInfo" style="width: 150px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="dvtCellLabel"> * Skill :</td>
                        <td class="dvtCellInfo">
                            <asp:TextBox ID="txt_skill" runat="server" class="TextView" MaxLength="100"
                                OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)"
                                Width="150px" Height="20px" Style="margin-left: 0px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="dvtCellLabel">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:CheckBox ID="chk_appointment" runat="server" Text="Appointment" Enabled="false" />
                            <asp:TextBox ID="txt_id" runat="server" BorderStyle="None" ForeColor="White" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="dvtCellLabel">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:CheckBox ID="chk_active" runat="server" Text="Active" Enabled="false" />
                        </td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 23px; ">
                <Saifi:MyDg ID="dgvSkillItem" runat="server"  Width="265px" AllowPaging="false"
                    AutoGenerateColumns="False" ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif"
                    ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif" ShowFirstAndLastImage="False"
                    ShowPreviousAndNextImage="False" BorderStyle="None" 
                    CssClass="Grid_DetailItem">
                    <AlternatingItemStyle CssClass="Grid_Detail" />
                    <ItemStyle CssClass="Grid_Detail" />
                    <PagerStyle Mode="NumericPages" CssClass=""></PagerStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="* Service"  ItemStyle-Width="250px">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" /> 
                                <asp:Label ID="lbl_item_name" runat="server" 
                                    Text='<%#DataBinder.Eval(Container.DataItem, "item_name")%>'></asp:Label>
                                <asp:Label ID="lbl_id" runat="server" Visible="false" 
                                    Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left" Width="250px" />
                        </asp:TemplateColumn>
                    </Columns>
                    <HeaderStyle CssClass="Grid_DetailHeader" />
                </Saifi:MyDg>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td align="center" >
                <asp:Button ID="cmd_save" runat="server" Text="Save" />
                <asp:Button ID="cmd_clear" runat="server" style="margin-left: 0px"  Text="Clear" />
            </td>
        </tr>
        <tr>
            <td style="height: 23px">
                &nbsp;</td>
        </tr>
        
        <tr>
            <td class="dvInnerHeader">Skill</td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                    <tr>
                        <td style="height: 14px; width: 675px; text-align:right;"  class="dvtCellInfo" >
                            <asp:Panel ID="Panel3" runat="server" Width="100%" Height="20px">
                                Shop : <asp:DropDownList ID="ddlSearchShop" runat="server" Width="150px" ></asp:DropDownList>
                                <asp:TextBox ID="txt_search" runat="server" class="detailedViewTextBox" MaxLength="100"
                                    onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                    Width="150px" Height="20px"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="txt_search_TextBoxWatermarkExtender" runat="server"
                                    Enabled="True" TargetControlID="txt_search" WatermarkText="Search">
                                </asp:TextBoxWatermarkExtender>
                                <asp:ImageButton ID="CmdSearch" runat="server" style="margin-left: 0px" 
                                    Width="16px" ImageUrl="~/images/search_lense.png" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 14px">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: center;">
                            <asp:Label runat="server" ID="lblNotFound" Text="Data not found." ForeColor="Red"
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 14px">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <Saifi:MyDg ID="dgvdetail" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CssClass="Grid_inq" ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif" PageSize="10"
                                        ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif" ShowFirstAndLastImage="False"
                                        ShowPreviousAndNextImage="False">
                                        <AlternatingItemStyle CssClass="Grid_inqAltItem" />
                                        <ItemStyle CssClass="Grid_inqItem" />
                                        <PagerStyle Mode="NumericPages" CssClass="Grid_inqPager"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Name Site" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">
                                            <FooterTemplate >
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="true" ForeColor="Red" 
                                                        Text="data not found"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_shop_id" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "shop_id")%>'></asp:Label>
                                                    <asp:Label ID="lbl_shop_nm" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "shop_name_en")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:TemplateColumn>
                                            
                                            <asp:TemplateColumn HeaderText="Skill Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                                                <ItemTemplate>
                                                 <asp:Label ID="lbl_id" runat="server"  Visible="false"
                                                        Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                                    <asp:Label ID="lbl_skill" runat="server" Style="text-align: left; width: 100%;"
                                                        Text='<%#DataBinder.Eval(Container.DataItem, "skill")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkActie" runat="server" Enabled="false" Checked='<%# Eval("active_status")=1 %>'  />
                                                    <asp:ImageButton ID="btnEdit" runat="server"  CommandName="Edit"
                                                        ImageUrl="~/images/edit_images.jpg" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <HeaderStyle CssClass="Grid_inqHeader" />
                                    </Saifi:MyDg>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
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
</asp:Content>

