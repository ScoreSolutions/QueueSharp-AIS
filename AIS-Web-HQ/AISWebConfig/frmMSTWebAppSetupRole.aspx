<%@ Page Title="" Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false"
    CodeFile="frmMSTWebAppSetupRole.aspx.vb" Inherits="frmMSTWebAppSetupRole" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
<%@ Register Src="~/UserControls/role.ascx" TagName="role" TagPrefix="r" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="Server">
    <table style="width: 900px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="tableHeader">
                <asp:Label ID="Label2" runat="server" Text="Web Application Setup >> Role"></asp:Label>
                
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
                <table border="0" cellpadding="0" cellspacing="0" style="width: 70%;">
                    <tr>
                        <td style="width: 122px; height: 27px" class="dvtCellLabel">
                            * Role Name :
                        </td>
                        <td class="dvtCellInfo">
                            <asp:TextBox ID="txt_rolename" runat="server" class="detailedViewTextBox" MaxLength="100"
                                onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                Width="150px" Height="20px" Style="margin-left: 0px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 122px; height: 27px" class="dvtCellLabel">
                            Description :
                        </td>
                        <td class="dvtCellInfo">
                            <asp:TextBox ID="txt_description" runat="server" class="detailedViewTextBox" MaxLength="100"
                                onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                Width="350px" TextMode="MultiLine" Rows="5" Style="margin-left: 0px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 122px; height: 27px" class="dvtCellLabel">
                            &nbsp;
                        </td>
                        <td class="dvtCellInfo">
                            <asp:CheckBox ID="chk_active" runat="server" Text="Active" Checked="true" />
                            <asp:TextBox ID="txt_id" runat="server" BorderStyle="None" ForeColor="White" Visible="False">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="CmdSave" runat="server" Text="Save" />
                            <asp:Button ID="CmdClear" runat="server" Text="Clear" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                        <asp:TabContainer ID="TabContainer1" runat="server" AutoPostBack="True" Width="896px"
                            EnableTheming="False" Style="margin-bottom: 49px" ActiveTabIndex="0" Height="400px" Enabled="false">
                            <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1" >
                                <HeaderTemplate>
                                    User
                                
                                
</HeaderTemplate>
                                

<ContentTemplate>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td align="left">
                                                <r:role ID="UserExist" runat="server"  Type="User" />
                                            </td>
                                            <td>
                                                <table>
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
                                            <td>
                                                <r:role ID="UserNotExist" runat="server" Type="User" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                
                                
</ContentTemplate>
                            

</asp:TabPanel>
                            <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                                <HeaderTemplate>
                                    ProgramID
                                
                                
</HeaderTemplate>
                                

<ContentTemplate>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <r:role ID="ProgramIDExist" runat="server"  Type="ProgramID" />
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnAddProgram" runat="server" Text="&lt;---" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnDeleteProgram" runat="server" Text="---&gt;" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <r:role ID="ProgramIDNotExist" runat="server" Type="ProgramID"  />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                
                                
</ContentTemplate>
                            

</asp:TabPanel>
                        </asp:TabContainer>
            </td>
        </tr>
        <tr>
            <td class="dvInnerHeader">
                Role
            </td>
        </tr>
        <tr>
            <td>
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
                                    <Saifi:MyDg ID="dgvRoleList" runat="server" AllowPaging="true" AutoGenerateColumns="False" CssClass="Grid_inq"
                                        ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif" ImageNext="/imgs/bulletr.gif"
                                        ImagePrevious="/imgs/bulletl.gif" ShowFirstAndLastImage="False" ShowPreviousAndNextImage="False"
                                        Width="900px">
                                        <AlternatingItemStyle CssClass="Grid_inqAltItem" />
                                        <ItemStyle CssClass="Grid_inqItem" />
                                        <PagerStyle Mode="NumericPages" CssClass="Grid_inqPager"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Role Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_id" runat="server" Style="text-align: left; width: 100%;" Visible="false"
                                                        Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                                    <asp:Label ID="lbl_role_name" runat="server" Style="text-align: left; width: 100%;"
                                                        Text='<%#DataBinder.Eval(Container.DataItem, "role_name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Description" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_role_desc" runat="server" Style="text-align: left; width: 100%;"
                                                        Text='<%#DataBinder.Eval(Container.DataItem, "role_desc")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkActie" runat="server" Enabled="false" Checked='<%#DataBinder.Eval(Container.DataItem, "active") = "Y" %>' />
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
