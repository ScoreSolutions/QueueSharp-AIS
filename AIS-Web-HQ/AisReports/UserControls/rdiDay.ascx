<%@ Control Language="VB" AutoEventWireup="false" CodeFile="rdiDay.ascx.vb" Inherits="UserControls_rdiDay" %>

       <asp:CheckBoxList ID="rdiday" runat="server" CausesValidation="True" Width="100px" RepeatDirection="Horizontal" class="hyjack">
            <asp:ListItem Value="1">Sunday&nbsp;&nbsp;</asp:ListItem>
            <asp:ListItem Value="2">Monday&nbsp;&nbsp;</asp:ListItem>
            <asp:ListItem Value="3">Tuesday&nbsp;&nbsp;</asp:ListItem>
            <asp:ListItem Value="4">Wednesday&nbsp;&nbsp;</asp:ListItem>
            <asp:ListItem Value="5">Thursday&nbsp;&nbsp;</asp:ListItem>
            <asp:ListItem Value="6">Friday&nbsp;&nbsp;</asp:ListItem>
            <asp:ListItem Value="7">Saturday&nbsp;&nbsp;</asp:ListItem>
        </asp:CheckBoxList>
            <asp:Label ID="lblValidText" runat="server" ForeColor="Red" 
    Text="*" ></asp:Label>

