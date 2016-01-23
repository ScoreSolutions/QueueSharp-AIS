<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="vdo.aspx.vb" Inherits="vdo" title="Service Detail" %>
<%@ MasterType  virtualPath="~/MasterPage.master"%>
<%@ Register assembly="JW-FLV-Player-Control" namespace="JW_FLV_Player_Control" tagprefix="cc" %>
<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server" >
    <div>
   <table border="0" class="formDialogOrange" width="518"><tr><td>
     <asp:Label ID="lblShop" runat="server" Text="" CssClass="geen" Font-Bold="true"></asp:Label><br />
    <asp:Label ID="lblDate" runat="server" Text="" Font-Bold="true"></asp:Label>  
   </td></tr></table>

    <asp:GridView ID="gvQ" runat="server" AutoGenerateColumns="False" 
        CssClass="shoplist" HorizontalAlign="Center" Width="518px">
        <HeaderStyle CssClass="formDialog" />
        <RowStyle CssClass="formDialogOrange" />
        <Columns>
            <asp:BoundField DataField="queue_no" HeaderText="Queue" />
            <asp:BoundField DataField="agent" HeaderText="Agent" />
            <asp:BoundField DataField="item_name" HeaderText="Service" />
            <asp:BoundField HeaderText="Mobile No." DataField="customer_id" />
            <asp:BoundField DataField="start" HeaderText="Start Time" />
            <asp:BoundField DataField="end" HeaderText="End Time" />
        </Columns>
        <HeaderStyle Wrap="False" />
    </asp:GridView>
<center><br /><br />
<div id="dvExternal" runat="server">

        <table border="0" class="formDialog shadow1 content">
        <tr>
        <td class="shoplist geen" width="380px"><strong><font>Agent</font></strong></td>
        <td class="shoplist" width="380px"><strong><font>Customer</font></strong></td>
        </tr>
        <tr>
        <td class="shoplist" colspan="2" width="760px">
        <cc:FlashPlayer ID="FP" runat="server" Height="305px" Width="760px"  AutoStart="true"  EnableViewState="False" />
        </td>
        </tr>
        </table>
        </div>


  
    
    </center>
    </div>
    <div id="dvInternal" runat="server">
     <IFRAME id="frame1" runat="server" Height="395px" Width="790px" scrolling="no" frameborder="0" marginheight="0" marginwidth="0"   ></IFRAME>
    </div>
    
           

</asp:Content>



