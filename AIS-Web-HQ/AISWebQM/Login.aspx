<%@ Page Language="VB" MasterPageFile="~/MasterLogin.master" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" title="" %>
<%@ MasterType  virtualPath="~/MasterLogin.master"%>
<%@ Register src="txtDate.ascx" tagname="txtDate" tagprefix="uc1" %>
<asp:Content ID="mainbody" ContentPlaceHolderID="Body" runat="server">
    <center>
<br />
            <br />
            <br />
            
            <table width="400" border="0" align="center" cellpadding="0" cellspacing="0" class="formDialog">
              <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td align="right">Username : </td>
                <td align="left">
                    <asp:TextBox ID="txtUsername" runat="server" Width="171px" MaxLength="50"></asp:TextBox>
                  </td>
              </tr>
              <tr>
                <td align="right">&nbsp;</td>
                <td align="left">&nbsp;</td>
              </tr>
              <tr>
                <td align="right">Password : </td>
                <td align="left">
                    <asp:TextBox ID="txtPassword" runat="server" Width="171px" MaxLength="50" 
                        TextMode="Password"></asp:TextBox>
                  </td>
              </tr>
              <tr>
                <td colspan="2" align="center" valign="middle" style="height: 16px">
                    &nbsp;</td>
              </tr>
              <tr>
                <td colspan="2" align="center" valign="middle">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btnGreen" 
                        Width="171px"/>
                  </td>
              </tr>
              <tr>
                <td colspan="2" align="center" valign="middle">
                    &nbsp;</td>
              </tr>
            </table>
           <asp:Label ID="lblConnInfo" runat="server" ForeColor="White"></asp:Label>
           <asp:Label ID="lblServerPath" runat="server" ForeColor="White"></asp:Label>
           <br />
            <br />
            <br />    
    </center>
</asp:Content>

