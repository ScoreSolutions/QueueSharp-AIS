<%@ Page Title="" Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false" CodeFile="frmMSTService.aspx.vb" Inherits="frmMSTService" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
    <table style="width: 900px" border="0" cellpadding="0" cellspacing="0" >
        <tr>
            <td class="tableHeader">
                <asp:Label ID="Label2" runat="server" Text="Central Setup >> Service"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 30px; text-align: center">
                <cc1:customupdateprogress id="progress" runat="server" progressimage="~/images/progress.gif"  />
            </td>
        </tr>
        <tr>
            <td class="dvInnerHeader">
                Configuration
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:900px;">
                    <tr>
                        <td style="height: 27px; width: 201px;" class="dvtCellLabel">* Item Code :</td>
                        <td style="width: 19px" class="dvtCellInfo">
                            <asp:TextBox ID="txt_item_code" runat="server" class="detailedViewTextBox" MaxLength="20"
                                Width="150px" Height="20px"></asp:TextBox>
                        </td>
                        <td style="width: 196px; height: 27px" class="dvtCellLabel">&nbsp;</td>
                        <td class="dvtCellInfo" style="width: 150px;">&nbsp;</td>
                        <td class="dvtCellInfo" style="width: 150px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 27px; width: 201px;" class="dvtCellLabel">
                            * Item Name In English :
                        </td>
                        <td style="width: 19px" class="dvtCellInfo">
                            <asp:TextBox ID="txt_item_name_english" runat="server" class="detailedViewTextBox"
                                MaxLength="50" Width="150px" Height="20px"></asp:TextBox>
                        </td>
                        <td style="width: 196px; height: 27px" class="dvtCellLabel">
                            * Item Name In Thai :
                        </td>
                        <td class="dvtCellInfo" style="width: 150px;">
                            <asp:TextBox ID="txt_item_name_thai" runat="server" class="detailedViewTextBox" MaxLength="50"
                                Width="150px" Height="20px"></asp:TextBox>
                        </td>
                        <td class="dvtCellInfo" style="width: 150px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 27px; width: 201px;" class="dvtCellLabel">
                            * Appointment Queue No:
                        </td>
                        <td style="width: 19px" class="dvtCellInfo">
                            <table style="width: 80%;">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txt_appointment_queue_no_min" runat="server" class="detailedViewTextBox"
                                            MaxLength="3"  Width="60px" Height="20px"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txt_appointment_queue_no_min_FilteredTextBoxExtender"
                                            runat="server" Enabled="True" TargetControlID="txt_appointment_queue_no_min"
                                            FilterType="Numbers">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                    <td>-</td>
                                    <td>
                                        <asp:TextBox ID="txt_appointment_queue_no_max" runat="server" class="detailedViewTextBox"
                                            MaxLength="3" Width="60px" Height="20px"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txt_appointment_queue_no_max_FilteredTextBoxExtender"
                                            runat="server" Enabled="True" TargetControlID="txt_appointment_queue_no_max"
                                            FilterType="Numbers">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 196px; height: 27px" class="dvtCellLabel">
                            * Text Queue :
                        </td>
                        <td class="dvtCellInfo" colspan="2">
                            <asp:TextBox ID="txt_queue" runat="server" class="detailedViewTextBox" MaxLength="1"
                                 Width="50px" Height="20px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txt_queue_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="UppercaseLetters" TargetControlID="txt_queue">
                            </asp:FilteredTextBoxExtender>
                            &nbsp;
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="(Capital letter only ex. A-Z)"
                                Width="160px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 27px; width: 201px;" class="dvtCellLabel">
                            * Item Order :
                        </td>
                        <td style="width: 19px" class="dvtCellInfo">
                            <asp:TextBox ID="txt_item_order" runat="server" class="detailedViewTextBox" 
                                Width="150px" Height="20px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txt_item_order_FilteredTextBoxExtender" runat="server"
                                Enabled="True" TargetControlID="txt_item_order" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 196px" class="dvtCellLabel">
                             Color :
                        </td>
                        <td align="left">
                            <table style="width: 100%;" >
                                <tr>
                                    <td class="dvtCellInfo" valign="top">
                                        <asp:TextBox ID="txt_color" runat="server" class="detailedViewTextBox" Height="20px"
                                           OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)"
                                            Width="20px" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txt_colorcode" runat="server" BorderStyle="None" 
                                            BackColor="White" Width="60px" Enabled="false" ></asp:TextBox>
                                        <asp:ColorPickerExtender ID="txt_colorcode_ColorPickerExtender" runat="server" Enabled="True"
                                            TargetControlID="txt_colorcode" PopupButtonID="CmdColor_Picker" SampleControlID="txt_color" >
                                        </asp:ColorPickerExtender>
                                    </td>
                                    <td valign="top">
                                        <asp:ImageButton ID="CmdColor_Picker" runat="server" ImageUrl="~/images/color_picker.png" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 27px; width: 201px;" class="dvtCellLabel">
                            * Standard Handling time :
                        </td>
                        <td style="width: 19px" class="dvtCellInfo">
                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_standard_handing_time" runat="server" class="detailedViewTextBox"
                                        MaxLength="100" Width="150px" Height="20px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="txt_standard_handing_time_FilteredTextBoxExtender"
                                        runat="server" Enabled="True" TargetControlID="txt_standard_handing_time" FilterType="Numbers">
                                    </asp:FilteredTextBoxExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 196px; height: 27px" class="dvtCellLabel">
                            Min
                        </td>
                        <td style="width: 150px; height: 27px" class="dvtCellInfo">
                            <asp:CheckBox ID="chk_vasily" runat="server" Text="Vasily" Checked="true" />
                        </td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 201px;" class="dvtCellLabel">
                            * Standard Waiting time :
                        </td>
                        <td style="width: 19px" class="dvtCellInfo">
                            <asp:TextBox ID="txt_standard_waiting_time" runat="server" class="detailedViewTextBox"
                                MaxLength="100" Width="150px" Height="20px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txt_standard_waiting_time_FilteredTextBoxExtender"
                                runat="server" Enabled="True" TargetControlID="txt_standard_waiting_time" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 196px" class="dvtCellLabel">Min</td>
                        <td>
                                <asp:TextBox ID="txt_id" runat="server" BorderStyle="None" ForeColor="White" Visible="False" Text="0" ></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 201px; height: 27px" class="dvtCellLabel">
                             &nbsp;
                        </td>
                        <td style="width: 19px" class="dvtCellInfo">
                            <asp:CheckBox ID="chk_active" runat="server" Text="Active" Checked="true" />
                        </td>
                        <td style="width: 196px; height: 27px" class="dvtCellLabel">
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
            <td >
                <table>
                    <tr>
                        <td>
                        <asp:Button ID="CmdSave" runat="server" Text="Save" Width="60px" />
                        </td>
                        <td>
                        <asp:Button ID="CmdClear" runat="server" Text="Clear" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td colspan='2'>&nbsp;</td></tr>
        <tr><td colspan='2'>&nbsp;</td></tr>
        <tr>
            <td colspan='2'>
                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="height: 14px; width: 100%; text-align: right;" class="dvtCellInfo" align="right">
                            <asp:Panel ID="Panel3" runat="server" Width="100%" Height="20px" >
                                <asp:TextBox ID="txt_search" runat="server" class="detailedViewTextBox" MaxLength="100"
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
                       
                            <saifi:mydg id="dgvService" runat="server" allowpaging="true" autogeneratecolumns="False"
                                    cssclass="Grid_inq" imagefirst="/imgs/nav_left.gif" imagelast="/imgs/nav_right.gif"
                                    imagenext="/imgs/bulletr.gif" imageprevious="/imgs/bulletl.gif" showfirstandlastimage="False"
                                    showpreviousandnextimage="False" width="900px">
                                <AlternatingItemStyle CssClass="Grid_inqAltItem" />
                                <ItemStyle CssClass="Grid_inqItem" />
                                <PagerStyle Mode="NumericPages" CssClass="Grid_inqPager"></PagerStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Code" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_id" runat="server" Style="text-align: left; width: 100%;" Visible="false"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                            <asp:Label ID="lbl_item_code" runat="server" Style="text-align: center; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "item_code")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_item_name" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "item_name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Order" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_item_order" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "item_order")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Std. time" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_item_time" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "item_time")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="WT. time" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_item_wait" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "item_wait")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Text" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_txt_queue" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "txt_queue")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
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

