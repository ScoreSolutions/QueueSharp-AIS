<%@ Page Title="" Language="VB" MasterPageFile="~/Template/ContentMasterPage.master" AutoEventWireup="false" CodeFile="frmFilterBacklist.aspx.vb" Inherits="CSIWebApp_frmFilterBacklist" %>
<%@ Register src="../UserControls/txtAutoComplete.ascx" tagname="txtAutoComplete" tagprefix="uc1" %>
<%@ Register src="../UserControls/ctlBrowseFile.ascx" tagname="ctlBrowseFile" tagprefix="uc5" %>
<%@ Register src="../UserControls/PageControl.ascx" tagname="PageControl" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td class="CssHead" align="left" width="100%" colspan="2" >
                Filter Blacklist
                <asp:Label ID="lblMessage" runat="server" Font-Bold="true" Font-Size="12pt" ForeColor="Red" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" ><img src="../images/PageHeaderLine.png" alt="" width="100%" /></td>
        </tr>
        <tr><td align="left" colspan="2" >&nbsp;</td></tr>
        <tr>
            <td colspan="2">
                <table cellpadding="0" cellspacing="0" width="100%" style="border:2px solid #339933" class="DivBoxRaius">
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr style="height:30px" >
                        <td class="Csslbl" align="right"  width="20%" >Attach File&nbsp;&nbsp;</td>
                        <td width="80%" >
                            <uc5:ctlBrowseFile ID="ctlBlackList" runat="server" VisibleUploadButton="true" Width="350px"  />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" ><uc2:PageControl ID="pcFile" runat="server" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" >
                            <asp:GridView ID="gvBlackListFile" runat="server" AutoGenerateColumns="False" AllowPaging="true" AllowSorting="true" 
                                CssClass="GridViewStyle" Width="95%"  ShowFooter="false" >
                                <RowStyle CssClass="RowStyle" />
                                <Columns> 
                                    <asp:TemplateField ShowHeader="false" >
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgDel" OnClick="imgDeleteFile_Click" ImageUrl="~/images/ico_delete.jpg"  runat="server" OnClientClick="return confirm('Are you sure?');" CommandArgument="<%# Bind('id') %>" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="no" HeaderText="No" >
                                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="file_name" HeaderText="File Name" SortExpression="file_name" >
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                                <PagerStyle CssClass="PagerStyle" />
                                <PagerSettings Visible="false" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                            <asp:TextBox ID="txtFileSortField" runat="server" Visible="false" Width="50px"></asp:TextBox>
                            <asp:TextBox ID="txtFileSortDir" runat="server" Visible="false" Width="50px" Text="DESC" ></asp:TextBox>    
                        </td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                </table>
            </td>
        </tr>
        <tr><td colspan="2">&nbsp;</td></tr>
        
        <tbody id="SearchMobile" runat="server">
            <tr>
                <td align="center" colspan="2" ><img src="../images/PageHeaderLine.png" alt="" width="100%" /></td>
            </tr>
            <tr><td align="left" colspan="2" >&nbsp;</td></tr>
            <tr>
                <td colspan="2">
                    <table cellpadding="0" cellspacing="0" width="100%" style="border:2px solid #339933" class="DivBoxRaius">
                        <tr><td colspan="2">&nbsp;</td></tr>
                        <tr style="height:30px" >
                            <td class="Csslbl" align="right" valign="middle" width="20%" >Search Mobile&nbsp;</td>
                            <td valign="top" width="80%" align="left" class="Csslbl" >
                                <uc1:txtAutoComplete ID="txtSearchMobile" runat="server" Width="100px" />&nbsp;&nbsp;&nbsp;
                                File Name&nbsp;
                                <uc1:txtAutoComplete ID="txtSearchFileName" runat="server" Width="150px" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnSearchMobile" runat="server" Text="Search" CssClass="formDialog" Width="80px" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnDuplicate" runat="server" Text="Duplicate" CssClass="formDialog" Width="80px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left" ><uc2:PageControl ID="pcMobile" runat="server" Visible="false" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" >
                                <asp:GridView ID="gvMobileList" runat="server" AutoGenerateColumns="False" AllowPaging="true" AllowSorting="true" 
                                    CssClass="GridViewStyle" Width="95%"  ShowFooter="false" >
                                    <RowStyle CssClass="RowStyle" />
                                    <Columns> 
                                        <asp:TemplateField ShowHeader="false" >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgDel" OnClick="imgDeleteMobile_Click" ImageUrl="~/images/ico_delete.jpg"  runat="server" OnClientClick="return confirm('Are you sure?');" CommandArgument="<%# Bind('id') %>" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="no" HeaderText="No" >
                                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mobile_no" HeaderText="Mobile No" SortExpression="mobile_no" >
                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="file_name" HeaderText="File Name" SortExpression="file_name" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="create_date" HeaderText="Update Date" SortExpression="create_date" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="create_by" HeaderText="User" SortExpression="create_by" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle CssClass="PagerStyle" />
                                    <PagerSettings Visible="false" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                </asp:GridView>    
                                <asp:TextBox ID="txtMobileSortField" runat="server" Visible="false" Width="50px"></asp:TextBox>
                                <asp:TextBox ID="txtMobileSortDir" runat="server" Visible="false" Width="50px" Text="DESC" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr><td colspan="2">&nbsp;</td></tr>
                    </table>
                </td>
            </tr>
            <tr><td colspan="2">&nbsp;</td></tr>
        </tbody>
    </table>
</asp:Content>

