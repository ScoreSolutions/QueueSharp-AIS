<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="qm.aspx.vb" Inherits="QM" title="Quality Monitoring" %>
<%@ MasterType  virtualPath="~/MasterPage.master"%>
<asp:Content ID="mainbody" ContentPlaceHolderID="Body" runat="server">
      <div id="lvShop" runat="server"></div>
      
      
      
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
                    <asp:LinkButton ID="likShopName" runat="server" Text='<%# Bind("shop_name") %>'
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
</asp:Content>

