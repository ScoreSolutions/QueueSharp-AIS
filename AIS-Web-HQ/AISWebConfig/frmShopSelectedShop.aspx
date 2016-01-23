<%@ Page Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false" CodeFile="frmShopSelectedShop.aspx.vb" Inherits="frmShopSelectedShop" title="Web Configuration Select Shop" %>
<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <table style="width: 900px" border="0" cellpadding="0" cellspacing="0" >
        <tr>
            <td class="tableHeader">
                <asp:Label ID="lblScreenName" runat="server" Text="Shop Setup >>"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 30px; text-align: center">
                <cc1:customupdateprogress id="progress" runat="server" progressimage="~/images/progress.gif"  />
            </td>
        </tr>
        <tr>
            <td align="center" >
                <asp:GridView ID="gvShopList" runat="server" AutoGenerateColumns="False" 
                     CssClass="shoplist" Width="50%" >
                    <Columns> 
                        <asp:BoundField DataField="no" HeaderText="#" >
                            <HeaderStyle Width="50px" HorizontalAlign="Center"  />
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField ShowHeader="True" HeaderText="Shop Code" >
                            <HeaderStyle Width="80px"  />
                            <ItemStyle HorizontalAlign="Center" Width="80px"  />
                            <ItemTemplate>
                                <asp:LinkButton ID="likShopAbb" runat="server" Text='<%# Bind("shop_abb") %>'
                                    CommandArgument='<%# Bind("id") %>' CommandName="Select" ToolTip="Select Shop"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="True" HeaderText="Shop Name" >
                            <HeaderStyle  />
                            <ItemStyle HorizontalAlign="Left"  />
                            <ItemTemplate>
                                <asp:LinkButton ID="likShopName" runat="server" Text='<%# Bind("shop_name_en") %>'
                                    CommandArgument='<%# Bind("id") %>' CommandName="Select" ToolTip="Select Shop"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="id" >
                            <ControlStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="formDialog" />
                    <AlternatingRowStyle BackColor="#DDDDDD" />
                </asp:GridView>
                
                <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Text="User Cannot Authorize Shop" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr><td>&nbsp;</td></tr>
   </table>
</asp:Content>

