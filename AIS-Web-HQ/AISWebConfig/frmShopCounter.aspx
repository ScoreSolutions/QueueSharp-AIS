<%@ Page Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false"
    CodeFile="frmShopCounter.aspx.vb" Inherits="frmShopCounter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="Server">
    <table style="width: 900px;">
        <tr>
            <td class="tableHeader">
                <asp:Label ID="lblScreenName" runat="server" Text="Shop Setup >> Counter >>"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 30px; text-align: center">
                <cc1:CustomUpdateProgress ID="progress" runat="server" ProgressImage="~/images/progress.gif" />
            </td>
        </tr>
        <tr>
            <td class="dvInnerHeader">
                Configuration
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 60%;">
                    <tr>
                        <td style="width: 139px; height: 27px" class="dvtCellLabel">
                            * Counter Code :
                        </td>
                        <td style="width: 231px; height: 27px" class="dvtCellInfo">
                                    <asp:TextBox ID="txt_counter_code" runat="server" class="detailedViewTextBox" MaxLength="100"
                                        onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                        Width="150px" Height="20px"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="txt_counter_code_TextBoxWatermarkExtender" runat="server" 
                                        Enabled="True" TargetControlID="txt_counter_code" WatermarkText="Code">
                                    </asp:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">
                            * Counter Name :
                        </td>
                        <td style="width: 150px; height: 27px" class="dvtCellInfo">
                                    <asp:TextBox ID="txt_counter_name" runat="server" class="detailedViewTextBox" MaxLength="100"
                                        onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                        Width="150px" Height="20px" Style="margin-left: 0px"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="txt_counter_name_TextBoxWatermarkExtender" runat="server"
                                        Enabled="True" TargetControlID="txt_counter_name" WatermarkText="Name">
                                    </asp:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 139px; height: 27px" class="dvtCellLabel">
                            Unit Display ID :
                        </td>
                        <td style="width: 231px; height: 27px" class="dvtCellInfo">
                                    <asp:TextBox ID="txt_unit_display_id" runat="server" class="detailedViewTextBox"  MaxLength="3"
                                        onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                        OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)"
                                        Width="150px" Height="20px"></asp:TextBox>
                                    <asp:NumericUpDownExtender ID="txt_unit_display_id_NumericUpDownExtender" runat="server"
                                        Enabled="True" Maximum="999" Minimum="0" RefValues="" ServiceDownMethod="" ServiceDownPath=""
                                        ServiceUpMethod="" Tag="" TargetButtonDownID="" Width="60" TargetControlID="txt_unit_display_id">
                                    </asp:NumericUpDownExtender>
                        </td>
                        <td class="dvtCellLabel" style="width: 150px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 139px; height: 27px" class="dvtCellLabel">
                                    <asp:CheckBox ID="chk_Quick_Services" runat="server" Text="Quick Service" />
                        </td>
                        <td class="dvtCellLabel" style="width: 146px">
                                    <asp:CheckBox ID="chk_Speed_Lane" runat="server" Text="Speed Lane" />
                        </td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">
                        </td>
                        <td style="width: 216px; height: 27px" class="dvtCellLabel">
                                    <asp:TextBox ID="txt_id" runat="server" Text="0" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtShopID" runat="server" Text="0" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 139px; height: 27px" class="dvtCellLabel">
                                    <asp:CheckBox ID="chk_Back_Office" runat="server" Text="Back Office" />
                        </td>
                        <td class="dvtCellLabel" style="width: 146px">
                                    <asp:CheckBox ID="chk_Counter_Manager" runat="server" Text="Counter Manager" />
                        </td>
                        <td style="width: 216px; height: 27px" class="dvtCellLabel">
                            &nbsp;
                        </td>
                        <td style="width: 216px; height: 27px" class="dvtCellLabel">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 139px; height: 27px" class="dvtCellLabel">
                                    <asp:CheckBox ID="chk_Active" runat="server" Text="Active" Checked="True" />
                        </td>
                        <td class="style9" style="width: 146px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 100px;">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 500px; height: 5px;">
                    <tr>
                        <td class="Grid_TD_FirstRowOneColumn" style="text-align: center; width: 200px; height: 25px;">
                            Customer Type
                        </td>
                        <td class="Grid_TD_FirstRowOneColumn" style="text-align: center; width: 300px; height: 25px;">
                            Allow Other Customer if Available
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Verdana; font-size: 15px; padding-left: 10px; color: black;
                            border: solid 2px #9BBB59; height: 30px;" align="left" >
                            <asp:RadioButtonList ID="opt_custype" runat="server"  RepeatDirection="Vertical">
                            </asp:RadioButtonList>
                        </td>
                        <td style="font-family: Verdana; font-size: 15px; padding-left: 10px; color: black;
                            border: solid 2px #9BBB59; height: 30px;" align="left">
                            <asp:CheckBoxList ID="chk_allow" runat="server" >
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 30px;">
            </td>
        </tr>
        <tr>
            <td class="dvInnerHeader">
                Event
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 60%;">
                    <tr>
                        <td style="width: 102px; height: 27px" class="dvtCellLabel">
                                    <asp:RadioButton ID="opt_now" runat="server" Text="Now" GroupName="Event" />
                        </td>
                        <td style="width: 200px">
                        </td>
                        <td style="width: 0px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px; height: 27px" class="dvtCellLabel">
                            <asp:RadioButton ID="opt_schedule" Checked="true" runat="server" Text="Schedule" GroupName="Event" />
                        </td>
                        <td style="width: 200px">
                            <table style="width: 39%; height: 31px;">
                                <tr>
                                    <td>
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
                        <td style="width: 0px">
                        </td>
                    </tr>
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr>
                        <td style="width: 102px">
                            &nbsp;
                        </td>
                        <td style="width: 200px">
                                    <asp:Button ID="cmd_save" runat="server" Text="Save" Style="width: 46px" 
                                        Width="54px" />
                                    <asp:Button ID="cmd_clear" runat="server" Text="Clear" />
                        </td>
                        <td style="width: 0px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="height: 30px">
                            &nbsp;
                        </td>
                        <td style="height: 30px">
                            &nbsp;
                        </td>
                        <td style="height: 30px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="dvInnerHeader" colspan="3">
                            Counter
                        </td>
                    </tr>
                    <tr>
                        <td class="style9" colspan="3">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td style="height: 14px; width: 675px; text-align: right;" class="dvtCellInfo">
                                        <asp:Panel ID="Panel3" runat="server" Width="100%" Height="20px">
                                            <asp:TextBox ID="txt_search" runat="server" class="detailedViewTextBox" MaxLength="100"
                                                onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                                Width="150px" Height="20px"></asp:TextBox>
                                            <asp:TextBoxWatermarkExtender ID="txt_search_TextBoxWatermarkExtender" runat="server"
                                                Enabled="True" TargetControlID="txt_search" WatermarkText="Search">
                                            </asp:TextBoxWatermarkExtender>
                                            <asp:ImageButton ID="CmdSearch" runat="server" Style="margin-left: 0px; height: 16px;"
                                                Width="16px" ImageUrl="~/images/search_lense.png" />
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
                                    <td colspan="2" style="height: 14px">
                                        <Saifi:MyDg ID="dgvdetail" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CssClass="Grid_inq" ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif"
                                            ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif" ShowFirstAndLastImage="False"
                                            ShowPreviousAndNextImage="False">
                                            <AlternatingItemStyle CssClass="Grid_inqAltItem" />
                                            <ItemStyle CssClass="Grid_inqItem" />
                                            <PagerStyle Mode="NumericPages" CssClass="Grid_inqPager" Wrap="True"></PagerStyle>
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
                                                <asp:TemplateColumn HeaderText="Counter Code" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_id" runat="server" Style="text-align: center; width: 100%;" Visible="false"
                                                            Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                                        <asp:Label ID="lbl_counter_code" runat="server" Style="text-align: center; width: 100%;"
                                                            Text='<%#DataBinder.Eval(Container.DataItem, "counter_code")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Counter Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_counter_name" runat="server" Style="text-align: left; width: 100%;"
                                                            Text='<%#DataBinder.Eval(Container.DataItem, "counter_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkActive" runat="server" Checked='<%# Eval("active_status") = 1 %>'  Enabled="false"/>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit_images.jpg"
                                                CausesValidation="False" CommandName="Edit" />
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
                    <tr>
                        <td class="style9" colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
