<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="QMUser.aspx.vb" Inherits="QMUser" title="Home" %>
<%@ MasterType  virtualPath="~/MasterPage.master"%>

<asp:Content ID="mainbody" ContentPlaceHolderID="Body" runat="server">
    <center>
        <table width="518" border="0" align="center" cellpadding="0" cellspacing="0" 
            class="formDialog shoplist">
          <tr>
            <td width="100">&nbsp;</td>
            <td class="style4">&nbsp;</td>
          </tr>
          <tr>
            <td width="100" align="right" nowrap="nowrap">Username : </td>
            <td align="left" nowrap="nowrap">
                                <asp:TextBox ID="txtUsername" runat="server" Width="157px" 
                    MaxLength="50"></asp:TextBox>
                              <asp:Button ID="btnCheck" runat="server" CssClass="btnGreen" 
                    Height="22px" Text="Check" />
                                <asp:Label ID="lblInfo" runat="server" Text="Info" Visible="False" CssClass="formDialogOrange"></asp:Label>
              </td>
            
          </tr>
          <!--
          <tr>
            <td align="right"  nowrap="nowrap" width="100">Password : </td>
            <td align="left"  nowrap="nowrap">
                                <asp:TextBox ID="txtPassword" runat="server" Width="157px" MaxLength="50" 
                                    TextMode="Password"></asp:TextBox>
                              </td>
            
          </tr>
          -->
          <tr>
            <td align="right" width="100">&nbsp;</td>
            <td align="left">
                <asp:CheckBox ID="chkAdmin" runat="server" Text="Admin" />
              </td>
            
          </tr>
          <tr>
            <td colspan="2" align="center" valign="middle">
                &nbsp;</td>
          </tr>
        </table>    
        <br />
        <asp:GridView ID="gvShop" runat="server" AutoGenerateColumns="False" 
            CssClass="shoplist" Width="518px">
            <Columns>
                <asp:BoundField DataField="shop_name" HeaderText="Shop" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Can view Others">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkViewOthers" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblShop" runat="server" Text='<%#Bind("shop_id")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="formDialog" />
            <AlternatingRowStyle BackColor="#DDDDDD" />
        </asp:GridView>
        <br />
                <asp:Button ID="btnSave" runat="server" CssClass="btnGreen" Height="22px" 
                    Text="Save" Width="72px" />
              <br />
        
    </center>
</asp:Content>

<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
    .style3
    {
        width: 9px;
    }
        .style4
        {
            height: 16px;
        }
        .style5
        {
            width: 9px;
            height: 16px;
        }
    </style>




</asp:Content>


