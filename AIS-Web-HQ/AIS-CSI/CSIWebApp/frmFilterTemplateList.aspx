<%@ Page Title="" Language="VB" MasterPageFile="~/Template/ContentMasterPage.master" EnableEventValidation="false" AutoEventWireup="false" CodeFile="frmFilterTemplateList.aspx.vb" Inherits="CSIWebApp_frmFilterTemplateList" %>
<%@ Register src="../UserControls/txtAutoComplete.ascx" tagname="txtAutoComplete" tagprefix="uc1" %>
<%@ Register src="../UserControls/PageControl.ascx" tagname="PageControl" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td style="color:#88B526;font-size:16pt" align="left" >Filter Template</td>
        </tr>
        <tr>
            <td align="center" ><img src="../images/PageHeaderLine.png" alt="" width="100%" /></td>
        </tr>
        <tr>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" style="border:2px solid #339933" class="DivBoxRaius" >
                    <tr >
                        <td align="left" class="CssSubHead" >Search</td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="90%" align="center" >
                                <tr><td colspan="2">&nbsp</td></tr>
                                <tr>
                                    <td width="30%" align="right" >Filter Name :&nbsp;</td>
                                    <td width="70%" align="left" >
                                        <uc1:txtAutoComplete ID="txtSearchFilterName" runat="server" Width="300px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">Template Code :&nbsp;</td>
                                    <td align="left">
                                        <uc1:txtAutoComplete ID="txtSearchTemplateCode" runat="server" Width="150px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">Status :&nbsp;</td>
                                    <td align="left">
                                        <asp:RadioButtonList runat="server" ID="rdiStatus" RepeatColumns="3" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Active &nbsp;&nbsp;&nbsp;" Value="Y" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Hold &nbsp;&nbsp;&nbsp;" Value="N" ></asp:ListItem>
                                            <asp:ListItem Text="All &nbsp;&nbsp;&nbsp;" Value="0" ></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td align="left">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="formDialog" Width="100px" Text="Search" />
                                    </td>
                                </tr>
                                <tr><td colspan="2">&nbsp</td></tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td align="left">&nbsp;</td></tr>
        <tr>
            <td align="right" >
                <asp:Button ID="btnAddNew" runat="server" Text="Add New" Width="100px" CssClass="formDialog" />
            </td>
        </tr>
        <tr><td align="left">&nbsp;</td></tr>
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lblNotFound" Text="Data not found." ForeColor="Red"
                    Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
           
                <uc2:PageControl ID="PageControl1" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
            
                <asp:GridView ID="gvFilterList" runat="server" AutoGenerateColumns="False" 
                    AllowSorting="true" CssClass="GridViewStyle" Width="100%" AllowPaging="true"  >
                    <RowStyle CssClass="RowStyle" />
                    <Columns> 
                        <asp:BoundField DataField="no" HeaderText="#" >
                            <HeaderStyle Width="30px" HorizontalAlign="Center"  />
                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="filter_name" HeaderText="Filter Name" SortExpression="filter_name" >
                            <HeaderStyle HorizontalAlign="Center"  />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="template_code" HeaderText="Template<br />Code" SortExpression="template_code" HtmlEncode="false" >
                            <HeaderStyle Width="50px" HorizontalAlign="Center"  />
                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="target" HeaderText="Target" SortExpression="target" >
                            <HeaderStyle Width="50px" HorizontalAlign="Center"  />
                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="status_name" HeaderText="Status" SortExpression="status_name" >
                            <HeaderStyle Width="50px" HorizontalAlign="Center"  />
                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="period_date" HeaderText="Period Date" SortExpression="preiod_datefrom" >
                            <HeaderStyle Width="50px" HorizontalAlign="Center"  />
                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField ShowHeader="false" >
                            <HeaderTemplate>Manage</HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgVerify" runat="server" OnClick="imgVerify_Click" ImageUrl="~/images/ico_verify_data.jpg" ToolTip="Verify Data" CommandArgument="<%# Bind('id') %>" ></asp:ImageButton>
                                <asp:ImageButton ID="imgEdit" runat="server" OnClick="imgEdit_Click" ImageUrl="~/images/ico_edit.jpg" ToolTip="Edit" CommandArgument="<%# Bind('id') %>" ></asp:ImageButton>
                                <asp:ImageButton ID="imgCopy" runat="server" OnClick="imgCopy_Click" ImageUrl="~/images/ico_copy.jpg" ToolTip="Duplicate" CommandArgument="<%# Bind('id') %>" ></asp:ImageButton>
                                <asp:ImageButton ID="imgDelete" runat="server" OnClick="imgDelete_Click" ImageUrl="~/images/ico_delete.jpg" ToolTip="Delete" CommandArgument="<%# Bind('id') %>" OnClientClick="return confirm('Are you sure?');" ></asp:ImageButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="id" >
                            <ControlStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle CssClass="PagerStyle" />
                    <PagerSettings Visible="false" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
                <asp:TextBox ID="txtSortField" runat="server" Visible="False" Width="15px" ></asp:TextBox>
                <asp:TextBox ID="txtSortDir" runat="server" Visible="False" Width="15px" Text="DESC" ></asp:TextBox>
            </td>
        </tr>
        <tr style="background-color:#E7EDDA; height:25px;vertical-align:middle"  >
            <td align="left" >&nbsp;&nbsp;&nbsp;
                <img src="../images/ico_edit.jpg" alt="Edit" />Edit&nbsp;&nbsp;
                <img src="../images/ico_copy.jpg" alt="Duplicate" />Duplicate&nbsp;&nbsp;
                <img src="../images/ico_verify_data.jpg" alt="Verify Data" />Verify Data&nbsp;&nbsp;
                <img src="../images/ico_delete.jpg" alt="Delete" />Delete&nbsp;&nbsp;
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
    </table>
</asp:Content>

