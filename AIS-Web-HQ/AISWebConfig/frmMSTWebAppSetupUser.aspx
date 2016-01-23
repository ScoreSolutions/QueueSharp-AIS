<%@ Page Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false"
    CodeFile="frmMSTWebAppSetupUser.aspx.vb" Inherits="frmMSTWebAppSetupUser"  %>
<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="Server">
    <table style="width: 900px;">
        <tr>
            <td class="tableHeader">
                <asp:Label ID="lblScreenName" runat="server" Text="Web Applicaiton Setup >> User"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 30px;  text-align:center">
                            <cc1:CustomUpdateProgress ID="progress" runat="server"  ProgressImage="~/images/progress.gif" 
                                   />
            </td>
        </tr>
        <tr>
            <td class="dvInnerHeader">
                Configuration
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 80%;">
                    <tr>
                        <td style="width: 83px; height: 27px" class="dvtCellLabel">
                            * User Code :
                        </td>
                        <td style="width: 84px" class="dvtCellInfo">
                            <asp:TextBox ID="txt_user_code" runat="server" class="detailedViewTextBox" MaxLength="100"
                                onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                Width="150px" Height="20px"></asp:TextBox>
                            <asp:TextBoxWatermarkExtender ID="txt_user_code_TextBoxWatermarkExtender" runat="server"
                                Enabled="True" TargetControlID="txt_user_code" WatermarkText="Code">
                            </asp:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 87px" class="dvtCellLabel">
                            Title Name :
                        </td>
                        <td style="width: 84px" class="dvtCellInfo">
                            <asp:DropDownList ID="cbo_titlenm" runat="server" class="detailedViewTextBox" MaxLength="100"
                                onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'" Width="100px">
                            </asp:DropDownList >
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 83px; height: 27px" class="dvtCellLabel">
                            * Name :</td>
                        <td style="width: 84px" class="dvtCellInfo">
                            <asp:TextBox ID="txt_user_first_name" runat="server" class="detailedViewTextBox"
                                MaxLength="100" onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                Width="150px" Height="20px"></asp:TextBox>
                            <asp:TextBoxWatermarkExtender ID="txt_user_first_name_TextBoxWatermarkExtender" runat="server"
                                Enabled="True" TargetControlID="txt_user_first_name" WatermarkText="Name">
                            </asp:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 87px" class="dvtCellLabel">
                            * Surname :</td>
                        <td  class="cellInfo">
                            <asp:TextBox ID="txt_user_last_name" runat="server" class="detailedViewTextBox" MaxLength="100"
                                onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                Width="150px" Height="20px"></asp:TextBox>
                            <asp:TextBoxWatermarkExtender ID="txt_user_last_name_TextBoxWatermarkExtender" runat="server"
                                Enabled="True" TargetControlID="txt_user_last_name" WatermarkText="Sure Name">
                            </asp:TextBoxWatermarkExtender>
                         </td>
                    </tr>
                    <tr>
                        <td style="width: 83px; height: 27px" class="dvtCellLabel">
                            * Position :
                        </td>
                        <td style="width: 84px" class="dvtCellInfo">
                            <asp:TextBox ID="txt_user_position" runat="server" class="detailedViewTextBox" MaxLength="100"
                                onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                Width="150px" Height="20px"></asp:TextBox>
                            <asp:TextBoxWatermarkExtender ID="txt_user_position_TextBoxWatermarkExtender" runat="server"
                                Enabled="True" TargetControlID="txt_user_position" WatermarkText="Position">
                            </asp:TextBoxWatermarkExtender>
                        </td>
                        <td style="width: 87px" class="dvtCellLabel">
                            * User Name :
                        </td>
                        <td class="cellInfo">
                            <asp:TextBox ID="txt_user_name" runat="server" class="detailedViewTextBox" MaxLength="100"
                                onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                Width="150px" Height="20px"></asp:TextBox>
                            <asp:TextBoxWatermarkExtender ID="txt_user_name_TextBoxWatermarkExtender" runat="server"
                                Enabled="True" TargetControlID="txt_user_name" WatermarkText="User Name">
                            </asp:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 83px; height: 27px" class="dvtCellLabel">
                              &nbsp;
                        </td>
                        <td style="width: 84px; height: 27px;text-align:left">
                            <asp:CheckBox ID="chk_active" runat="server" Text="Active" Checked="true" />
                        </td>
                        <td style="width: 87px; height: 27px;">
                            &nbsp;
                        </td>
                        <td style="height: 27px">
                            <asp:TextBox ID="txt_id"  runat="server" BorderStyle="None" ForeColor="White" Visible="False">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td >
                              &nbsp;
                        </td>
                        <td align="center" colspan="3">
                            <asp:Button ID="cmd_save" runat="server" Text="Save" />
                            <asp:Button ID="cmd_clear" runat="server" Style="margin-left: 0px" Text="Clear" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td class="dvInnerHeader">
                User Authoried Shop
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                    <tr>
                        <td style="width:45%" >
                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                <tr>
                                    <td align="center" class="dvtCellLabel">
                                        <asp:Label ID="lblLeftHeader" runat="server" Text="Authroized Shop"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="panel1" runat="server" Height="400px" ScrollBars="Auto" Width="400px">
                                            <asp:GridView ID="gvUserLeft" runat="server"  AutoGenerateColumns="False" 
                                                CssClass="shoplist" Width="96%">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="#" 
                                                        ItemStyle-Width="10%">
                                                        <itemtemplate>
                                                            <asp:CheckBox ID="ChkSelect" runat="server" />
                                                             <asp:Label ID="lblId" runat="server" Text='<%# Bind("id") %>' style="display:none"></asp:Label>
                                                        </itemtemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <itemstyle horizontalalign="center" width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" ShowHeader="True"  HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderStyle Width="86%"  />
                                                        <ItemStyle HorizontalAlign="Left" Width="86%" />
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblHeaderText" runat="server" Text="Shop Name"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("shop_name_en") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="id">
                                                    <ControlStyle CssClass="zHidden" />
                                                    <FooterStyle CssClass="zHidden" />
                                                    <HeaderStyle CssClass="zHidden" />
                                                    <ItemStyle CssClass="zHidden" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="formDialog" />
                                                <AlternatingRowStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td  style="width:10%" valign="top" >
                            <table>
                                <tr><td>&nbsp;</td></tr>
                                <tr><td>&nbsp;</td></tr>
                                <tr><td>&nbsp;</td></tr>
                                <tr><td>&nbsp;</td></tr>
                                <tr><td>&nbsp;</td></tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnAddUser" runat="server" Text="&lt;---" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnDeleteUser" runat="server" Text="---&gt;"  />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width:45%" >
                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                <tr>
                                    <td align="center" class="dvtCellLabel">
                                        <asp:Label ID="lblRightHeader" runat="server" Text="Non Authrorized Shop"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="panel2" runat="server" Height="400px" ScrollBars="Auto" Width="400px">
                                            <asp:GridView ID="gvUserRight" runat="server" AutoGenerateColumns="False" 
                                                CssClass="shoplist" Width="96%">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="#" 
                                                        ItemStyle-Width="10%">
                                                        <itemtemplate>
                                                            <asp:CheckBox ID="ChkSelect" runat="server" />
                                                             <asp:Label ID="lblId" runat="server" Text='<%# Bind("id") %>' style="display:none"></asp:Label>
                                                        </itemtemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <itemstyle horizontalalign="center" width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" ShowHeader="True"  HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderStyle Width="86%"  />
                                                        <ItemStyle HorizontalAlign="Left" Width="86%" />
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblHeaderText" runat="server" Text="Shop Name"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("shop_name_en") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="id">
                                                    <ControlStyle CssClass="zHidden" />
                                                    <FooterStyle CssClass="zHidden" />
                                                    <HeaderStyle CssClass="zHidden" />
                                                    <ItemStyle CssClass="zHidden" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="formDialog" />
                                                <AlternatingRowStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </asp:Panel>
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
                User
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                    <tr>
                        <td style="height: 14px; width: 100%; text-align: right;" class="dvtCellInfo">
                            <asp:TextBox ID="txt_search" runat="server" class="detailedViewTextBox" MaxLength="100"
                                onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                Width="150px" Height="20px"></asp:TextBox>
                            <asp:TextBoxWatermarkExtender ID="txt_search_TextBoxWatermarkExtender" runat="server"
                                Enabled="True" TargetControlID="txt_search" WatermarkText="Search">
                            </asp:TextBoxWatermarkExtender>
                            <asp:ImageButton ID="CmdSearch" runat="server" Style="margin-left: 0px" Width="16px"
                                ImageUrl="~/images/search_lense.png" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 14px; text-align: right;" class="dvtCellInfo">
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
                        <td style="height: 14px;" align="center">
                            <Saifi:MyDg ID="dgvUserList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                CssClass="Grid_inq" ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif"
                                ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif" ShowFirstAndLastImage="False"
                                ShowPreviousAndNextImage="False" PageSize="10" Width="80%" >
                                <AlternatingItemStyle CssClass="Grid_inqAltItem" />
                                <ItemStyle CssClass="Grid_inqItem" />
                                <PagerStyle Mode="NumericPages" CssClass="Grid_inqPager"></PagerStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="User Code" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120px" >
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_id" runat="server"  Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                            <asp:Label ID="lbl_user_code" runat="server" Style="text-align: left;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "user_code")%>'></asp:Label>
                                                
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Position" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_position" runat="server" Style="text-align: left;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "position")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Name" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_User_Name" runat="server" Style="text-align: left;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "staff_name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkActie" runat="server" Enabled="false" Checked='<%#DataBinder.Eval(Container.DataItem, "active_status") = "1" %>' />
                                            <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit" ImageUrl="~/images/edit_images.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle CssClass="Grid_inqHeader" />
                            </Saifi:MyDg>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;
                        </td>
                    </tr>
                </table>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 23px">
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
