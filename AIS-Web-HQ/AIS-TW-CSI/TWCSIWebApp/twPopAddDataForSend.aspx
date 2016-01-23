<%@ Page Language="VB" AutoEventWireup="false" CodeFile="twPopAddDataForSend.aspx.vb" Inherits="TWCSIWebApp_twPopAddDataForSend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="overflow:scroll;overflow-x:hidden;height:500px" >
        <asp:Label ID="lblID" runat="server" Visible="false" ></asp:Label>
        <asp:Label ID="lblServiceID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblShopID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblMobileList" runat="server" ></asp:Label>
    </div>
    </form>
</body>
</html>
