<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ddlShop.ascx.vb" Inherits="UserControls_ddlShop" %>
<tr style="height: 30px">
<td style="height: 28px" align="right">
    by Shop :&nbsp;
</td>
    <td style="height: 28px" align="left">
        <asp:DropDownList ID="ddlShop" runat="server" CausesValidation="True" Width="170px"
            class="hyjack" DataTextField="item_name" DataValueField="item_code">
        </asp:DropDownList>
    </td>
</tr>
