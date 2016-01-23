<%@ Page Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false" 
CodeFile="frmShopHoldReason.aspx.vb" Inherits="frmShopHoldReason" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register assembly="MycustomDG" namespace="MycustomDG" tagprefix="Saifi" %>
<%@ Register src="UserControls/ctlBranchSelected.ascx" tagname="ctlBranchSelected" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
<script>
    //utility function to get the container of an element by tagname
    function GetParentByTagName(parentTagName, childElementObj) {
        var parent = childElementObj.parentNode;
        while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
            parent = parent.parentNode;
        }
        return parent;
    }

</script>
    <table style="width: 900px;">
        <tr>
            <td class="tableHeader">
                <asp:Label ID="Label2" runat="server" Text="Shop Setup >> Hold Reason"></asp:Label>
            </td>
        </tr>
        <uc1:ctlBranchSelected ID="ctlBranchSelected1" runat="server" Check1Shop="N" />
        <tr>
            <td >
                <cc1:CustomUpdateProgress ID="progress" runat="server"  ProgressImage="~/images/progress.gif" />
            </td>
        </tr>
        <tr>
            <td class="dvInnerHeader">
                Configuration
            </td>
        </tr>
        <tr>
            <td style="height: 5px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">
                            Master Hold Reason :
                        </td>
                        <td class="dvtCellInfo">
                            <asp:DropDownList ID="ddlMasterHoldReason" runat="server" Width="200px" AutoPostBack="true" ></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td  class="dvtCellLabel">
                            Hold Reason :
                        </td>
                        <td class="dvtCellInfo">
                            <asp:TextBox runat="server" ID="txt_hold_reason" class="TextView"
                            OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)"
                            Width="350px" Height="20px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="dvtCellLabel">
                            <asp:CheckBox runat="server" Text="Productive" ID="chk_hold_reason_productive" Enabled="false" >
                            </asp:CheckBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="dvtCellLabel">
                            <asp:CheckBox runat="server" Text="Active" ID="chk_hold_reason_active" Enabled="false" >
                            </asp:CheckBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="dvInnerHeader">
                Event
            </td>
        </tr>
        <tr>
            <td style="height: 5px">
                <table border="0" cellpadding="0" cellspacing="0" style="height: 77px; width: 411px">
                    <tr>
                        <td style="width: 117px; height: 27px" class="dvtCellLabel">
                            <asp:RadioButton ID="opt_now" runat="server" Text="Now" GroupName="Event" />
                        </td>
                        <td  style="width: 162px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 117px; height: 27px" class="dvtCellLabel">
                            <asp:RadioButton ID="opt_schedule" runat="server" Text="Schedule" GroupName="Event" Checked="true" />
                        </td>
                        <td class="style9" style="width: 162px">
                            <table style="width: 39%; height: 31px;">
                                <tr>
                                    <td style="width: 122px" class="dvtCellInfo">
                                        <asp:TextBox ID="txt_date" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="txt_date_CalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="txt_date" PopupButtonID="imgcalendar" Format="dd/MM/yyyy">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgcalendar" runat="server" ImageUrl="~/images/calender_icon_score.jpg" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 117px">
                            &nbsp;</td>
                        <td class="style9" style="width: 162px">
                            <asp:Button ID="cmd_save" runat="server"  Text="Save" />
                            <asp:Button ID="cmd_clear" runat="server" Text="Clear" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 5px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="dvInnerHeader">
                Reason
            </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: right;" class="dvtCellInfo">
                            <asp:Panel ID="Panel3" runat="server" Width="100%" Height="20px">
                                Shop : <asp:DropDownList ID="ddlSearchShop" runat="server" Width="150px"></asp:DropDownList>
                                <asp:TextBox ID="txt_search" runat="server" class="detailedViewTextBox" MaxLength="100"
                                    onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                    Width="150px" Height="20px"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="txt_search_TextBoxWatermarkExtender" 
                                    runat="server" Enabled="True" TargetControlID="txt_search" 
                                    WatermarkText="Search">
                                </asp:TextBoxWatermarkExtender>
                                <asp:ImageButton ID="CmdSearch" runat="server" Style="margin-left: 0px" Width="16px"
                                    ImageUrl="~/images/search_lense.png" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: right;" class="dvtCellInfo">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: center;">
                            <asp:Label runat="server" ID="lblNotFound" Text="Data not found." ForeColor="Red"
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 14px">
                             <Saifi:MyDg ID="dgvdetail" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                CssClass="Grid_inq" ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif"
                                ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif" ShowFirstAndLastImage="False"
                                ShowPreviousAndNextImage="False">
                                <AlternatingItemStyle CssClass="Grid_inqAltItem" />
                                <ItemStyle CssClass="Grid_inqItem" />
                                <PagerStyle Mode="NumericPages" CssClass="Grid_inqPager"></PagerStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Name Site" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" >
                                        <FooterTemplate >
                                            <asp:Label ID="Label3" runat="server" Font-Bold="true" ForeColor="Red" 
                                                Text="data not found"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="lbl_shop_name" runat="server" Style="text-align: left; width: 100%;"
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "shop_name_en")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Reason" ItemStyle-HorizontalAlign="left" ItemStyle-Width="250px">
                                         <ItemTemplate>
                                                <asp:Label ID="lbl_reason" runat="server" Style="text-align: center; width: 100%;"
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>'></asp:Label>
                                         </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Productivity" ItemStyle-HorizontalAlign="center" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Pro_name" runat="server" Style="text-align: left; width: 100%;" 
                                            Text='<%#DataBinder.Eval(Container.DataItem, "productive")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Active">
                                       <ItemTemplate>
                                            <asp:CheckBox ID="ChkActive" runat="server" AutoPostBack="false" Checked='<%#DataBinder.Eval(Container.DataItem, "active_status") = "1"%>'  Enabled="false"/>
                                       </ItemTemplate>
                                       <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle CssClass="Grid_inqHeader" />
                            </Saifi:MyDg>
                        </td>
                    </tr>
                </table>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

