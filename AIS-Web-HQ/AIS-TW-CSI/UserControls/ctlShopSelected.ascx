<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlShopSelected.ascx.vb" Inherits="UserControls_ctlShopSelected" %>
<style type="text/css">
    .style1
    {
        height: 26px;
    }
</style>
<asp:UpdatePanel ID="pnlUpdate1" runat="server" >
    <ContentTemplate>
      <%--  <table>
            <tr>
                <td width="45%">
                </td>
                <td width="10%">
                </td>
                <td width="45%">
                </td>
            </tr>
            <tr>
                <td>
                
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    
    --%>
    
    
    
    
    
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td  align="right" style="padding-right:20px">
                    <br />
                    <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label>
                    <asp:DropDownList ID="ddlRegion" Width="150px" runat="server" 
                        DataTextField="region_name_en" DataValueField="id" AutoPostBack="true">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            
            <tr>
                <td width="45%">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" class="dvtCellLabel" >
                                <asp:Label ID="lblHeaderLeft" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlPanelLeft" runat="server" Height="300px" ScrollBars="Auto" Width="400px">
                                    <asp:GridView ID="gvShopLeft" runat="server" AutoGenerateColumns="False" 
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
                                                <HeaderStyle Width="30%"  />
                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblHeaderTextCode" runat="server" Text="Location Code"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocationCode" runat="server" Text='<%# Bind("location_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ShowHeader="True"  HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle Width="56%"  />
                                                <ItemStyle HorizontalAlign="Left" Width="56%" />
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblHeaderTextName" runat="server" Text="Location Name"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocationName" runat="server" Text='<%# Bind("location_name_en") %>'></asp:Label>
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
                <td  style="width:10%; padding-top:80px; padding-right:20px" valign="top">
                 <asp:Button ID="btnAddUser" runat="server" Text="&lt;---" />
                  <asp:Button ID="btnDeleteUser" runat="server" Text="---&gt;"  />
                    <%--<table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr><td class="style1"></td></tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td>
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                              
                            </td>
                        </tr>
                    </table>--%>
                </td>
                <td width="45%">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" class="dvtCellLabel">
                                <asp:Label ID="lblHeaderRight" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlRight" runat="server" Height="300px" ScrollBars="Auto" Width="400px">
                                    <asp:GridView ID="gvShopRight" runat="server" AutoGenerateColumns="False" 
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
                                                <HeaderStyle Width="30%"  />
                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblHeaderTextCode" runat="server" Text="Location Code"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocationCode" runat="server" Text='<%# Bind("location_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ShowHeader="True"  HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle Width="56%"  />
                                                <ItemStyle HorizontalAlign="Left" Width="56%" />
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblHeaderText" runat="server" Text="Location Name"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocationName" runat="server" Text='<%# Bind("location_name_en") %>'></asp:Label>
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
    </ContentTemplate>
</asp:UpdatePanel>