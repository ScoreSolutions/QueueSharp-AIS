<%@ Page Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false"
    CodeFile="frmShopUser.aspx.vb" Inherits="frmShopUser" %>
<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="Server">
    <table style="width: 900px;">
        <tr>
            <td class="tableHeader">
                <asp:Label ID="lblScreenName" runat="server" Text="Shop Setup >> User"></asp:Label>
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
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="width: 83px; height: 27px" class="dvtCellLabel">
                            * User Code :
                        </td>
                        <td style="width: 84px" class="dvtCellInfo">
                            <asp:TextBox ID="txt_user_code" runat="server" class="detailedViewTextBox" MaxLength="100"
                                Width="150px" Height="20px"></asp:TextBox>
                        </td>
                        <td style="width: 87px" class="dvtCellLabel">
                            Title Name :
                        </td>
                        <td style="width: 84px" class="dvtCellInfo">
                            <asp:DropDownList ID="cbo_titlenm" runat="server"  Width="100px">
                            </asp:DropDownList >
                        </td>
                        <td rowspan="7" valign="top" >
                            <Saifi:MyDg ID="dgvSkill" runat="server" Width="250px" AllowPaging="false" AutoGenerateColumns="False"
                                ImagePrevious="/imgs/bulletl.gif" ShowFirstAndLastImage="False" ShowPreviousAndNextImage="False"
                                BorderStyle="None" CssClass="Grid_DetailItem">
                                <AlternatingItemStyle CssClass="Grid_Detail" />
                                <ItemStyle CssClass="Grid_Detail" />
                                <PagerStyle Mode="NumericPages" CssClass=""></PagerStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="* Skill" ItemStyle-Width="230px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                            <asp:Label ID="lbl_skill" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "skill")%>'></asp:Label>
                                            <asp:Label ID="lbl_id" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                            
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" Width="250px" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle CssClass="Grid_DetailHeader" />
                            </Saifi:MyDg>
                        </td>
                    </tr>
                    <tr>
                        <td height: 27px" class="dvtCellLabel">
                            * Name :</td>
                        <td  class="dvtCellInfo">
                            <asp:TextBox ID="txt_user_first_name" runat="server" class="detailedViewTextBox"
                                MaxLength="100" Width="150px" Height="20px"></asp:TextBox>
                        </td>
                        <td class="dvtCellLabel">
                            * Surname :</td>
                        <td  class="cellInfo">
                            <asp:TextBox ID="txt_user_last_name" runat="server" class="detailedViewTextBox" MaxLength="100"
                                Width="150px" Height="20px"></asp:TextBox>
                         </td>
                    </tr>
                    <tr>
                        <td class="dvtCellLabel">
                            * Group :</td>
                        <td style="width: 84px" class="dvtCellInfo">
                            <asp:DropDownList ID="cbo_user_group" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td class="dvtCellLabel">
                            * Position :
                        </td>
                        <td style="width: 84px" class="dvtCellInfo">
                            <asp:TextBox ID="txt_user_position" runat="server" class="detailedViewTextBox" MaxLength="100"
                                Width="150px" Height="20px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="dvtCellLabel">
                            * User Name :
                        </td>
                        <td class="cellInfo">
                            <asp:TextBox ID="txt_user_name" runat="server" class="detailedViewTextBox" MaxLength="100"
                                Width="150px" Height="20px"></asp:TextBox>
                        </td>
                        
                        <td class="dvtCellLabel">
                            * Password
                        </td>
                        <td class="cellInfo">
                            <asp:TextBox ID="txtPassword" runat="server" class="detailedViewTextBox" MaxLength="100"
                                Width="150px" Height="20px" TextMode="Password" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td >
                              &nbsp;</td>
                        <td class="cellInfo">
                            <asp:Button ID="btnLDAP" runat="server" Text="Check LDAP" />
                        </td>
                        <td >
                            &nbsp;</td>
                        <td  class="cellInfo" >
                            <asp:CheckBox ID="cbConLDAP" runat="server" Text="Check user in LDAP" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 83px; height: 27px" class="dvtCellLabel">
                              &nbsp;
                        </td>
                        <td class="cellInfo">
                            <asp:CheckBox ID="chk_active" runat="server" Text="Active" Checked="true" />
                        </td>
                        <td style="width: 87px; height: 27px;">
                            &nbsp;
                        </td>
                        <td style="height: 27px">
                            <asp:TextBox ID="txt_id"  runat="server" BorderStyle="None" ForeColor="White" Visible="False">0</asp:TextBox>
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
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="width: 105px; height: 27px" class="dvtCellLabel">
                            <asp:RadioButton ID="opt_now" runat="server" Text="Now" GroupName="Event" />
                        </td>
                        <td style="width: 234px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 105px; height: 27px" class="dvtCellLabel">

                            <asp:RadioButton ID="opt_schedule" runat="server" Text="Schedule" 
                                    GroupName="Event" Checked="true" />
                        </td>
                        <td style="width: 234px">
                            <table style="width: 39%; height: 31px;">
                                <tr>
                                    <td class="dvtCellInfo">
                                        <asp:TextBox ID="txt_date" runat="server" Enabled="false"></asp:TextBox>
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
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 105px">
                            &nbsp;
                        </td>
                        <td style="width: 234px">
                            <asp:Button ID="cmd_save" runat="server" Text="Save" />
                            <asp:Button ID="cmd_clear" runat="server" Style="margin-left: 0px" Text="Clear" />
                        </td>
                        <td>
                            &nbsp;
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
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: right;" class="dvtCellInfo">
                            <asp:Panel ID="Panel3" runat="server" Width="100%" Height="20px">
                                &nbsp;<asp:DropDownList ID="ddlSearchShop" runat="server" Width="150px" 
                                    Visible="False"></asp:DropDownList>
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
                            <Saifi:MyDg ID="dgvdetail" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                CssClass="Grid_inq" ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif"
                                ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif" ShowFirstAndLastImage="False"
                                ShowPreviousAndNextImage="False">
                                <AlternatingItemStyle CssClass="Grid_inqAltItem" />
                                <ItemStyle CssClass="Grid_inqItem" />
                                <PagerStyle Mode="NumericPages" CssClass="Grid_inqPager"></PagerStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Name Site" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                    <FooterTemplate >
                                            <asp:Label ID="Label3" runat="server" Font-Bold="true" ForeColor="Red" 
                                                Text="data not found"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_shop_id" runat="server" Visible="false"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "shop_id")%>' ></asp:Label>
                                            <asp:Label ID="lbl_shop_db_name" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "shop_name_en")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Group" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_group_name" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "group_name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="User Code" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                         <asp:Label ID="lbl_id" runat="server" Style="text-align: center; width: 100%;" Visible="false"
                                                                    Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                            <asp:Label ID="lbl_user_code" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "user_code")%>'></asp:Label>
                                                
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Position" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_position" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "position")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_User_Name" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "User_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkActive" runat="server" Checked='<%#Eval("active_status") = 1 %>'  Enabled="false"  />
                                            <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/images/edit_images.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle CssClass="Grid_inqHeader" />
                            </Saifi:MyDg>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 14px">
                            <asp:Label ID="lblShopID" runat="server" Visible="false" ></asp:Label>
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
