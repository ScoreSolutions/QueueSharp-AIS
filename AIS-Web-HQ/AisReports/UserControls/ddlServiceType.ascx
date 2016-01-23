<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ddlServiceType.ascx.vb" Inherits="UserControls_ddlServiceType" %>
<tr style="height: 30px">
<td style="height: 28px" align="right">
   by Service Type :&nbsp;
</td>
<td style="height: 28px" align="left">
    <asp:DropDownList ID="ddlServiceType" runat="server" CausesValidation="True" Width="170px"
        class="hyjack" DataTextField="item_name" DataValueField="item_code">
    </asp:DropDownList>
</td>
</tr> 