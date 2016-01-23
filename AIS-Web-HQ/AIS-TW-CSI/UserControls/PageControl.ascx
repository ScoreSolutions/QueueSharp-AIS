<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PageControl.ascx.vb" Inherits="UserControls_PageControl" %>

<table cellpadding="0" cellspacing="0" border="0" width="100%" id="TABLE1" style="background-color: #f5f5f5">
    <tr >
        <td style="padding-top:3px; padding-bottom:3px; width:300px" >
            <asp:LinkButton ID="lnbBack" runat="server" ForeColor="Black" >[<]</asp:LinkButton>
             <asp:Label ID="Label3" runat="server"  >Page</asp:Label>
            
            <asp:DropDownList ID="cmbPage" runat="server" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="cmbPage_SelectedIndexChanged">
            </asp:DropDownList>
          <asp:Label ID="Label2" runat="server" >From</asp:Label>
            
            <asp:Label ID="lblTotalPage" runat="server" ></asp:Label>
        <asp:Label ID="Label1" runat="server" >Page(s)</asp:Label>
            <asp:LinkButton ID="lnbNext" runat="server" ForeColor="Black" >[>]</asp:LinkButton>
        </td>
        <td style="padding-top:3px; padding-bottom:3px; padding-right:5px" align="right" >
            <asp:Label ID="lblSummary" runat="server" ></asp:Label>
            <asp:TextBox ID="txtPageIndex" runat="server" Visible="false" />
        </td>
    </tr>
</table>
