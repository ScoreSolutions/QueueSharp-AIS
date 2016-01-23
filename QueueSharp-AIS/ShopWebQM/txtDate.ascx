<%@ Control Language="VB" AutoEventWireup="false" CodeFile="txtDate.ascx.vb" Inherits="UserControls_txtDate" %>
<table>
<tr>
    <td align =left >
        <asp:TextBox ID="TextBox1" runat="server" Width="80" CssClass="Csslbl"></asp:TextBox>
    </td>
    <td align =left >
        <a id="likA" runat="server" style="text-decoration:none" >
            <img src="~/images/calendarIcon.gif" width="19" height="19" border="0" 
            id="ImageButton1" runat="server" 
    style="vertical-align:baseline;cursor:pointer;" />
        </a>
    </td>
    <td align =left >
        <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
    </td>
    <td align =left >
        <asp:Label ID="lblValidText" runat="server" Text="" ForeColor="Red"></asp:Label>
    </td>
</tr>
</table>

