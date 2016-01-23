<%@ Page Language="VB" AutoEventWireup="false" EnableEventValidation="false"  CodeFile="frmPopSendResult.aspx.vb" Inherits="CSIWebApp_frmPopAddDataForSend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail</title>
    <link rel="stylesheet" type="text/css" href="../Template/style.css" />
    <link rel="stylesheet" type="text/css" href="../Template/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="overflow:scroll;overflow-x:hidden;height:400px" >
        <asp:Label ID="lblID" runat="server" Visible="false" ></asp:Label>
        <asp:Label ID="lblServiceID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblShopID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label>
        <center>
            <asp:GridView ID="gvResultList" runat="server" AutoGenerateColumns="False" 
                CssClass="GridViewStyle" Width="95%" >
                <RowStyle CssClass="RowStyle" />
                <Columns> 
                    <asp:BoundField DataField="no" HeaderText="#" >
                        <HeaderStyle Width="30px" HorizontalAlign="Center"  />
                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="mobile_no" HeaderText="Mobile" >
                        <HeaderStyle Width="50px" HorizontalAlign="Center"  />
                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="customer_name" HeaderText="Customer Name" >
                        <HeaderStyle  HorizontalAlign="Center"  />
                        <ItemStyle  HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="filter_name" HeaderText="Filter Name" >
                        <HeaderStyle Width="100px" HorizontalAlign="Center"  />
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="template_code" HeaderText="Template Code" >
                        <HeaderStyle Width="100px" HorizontalAlign="Center"  />
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="item_code" HeaderText="Service" >
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="send_time" HeaderText="Send Time" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HtmlEncode="False" >
                        <HeaderStyle Width="100px" HorizontalAlign="Center"  />
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="result_time" HeaderText="Result Time" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HtmlEncode="False" >
                        <HeaderStyle Width="100px" HorizontalAlign="Center"  />
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="result" HeaderText="Result" >
                        <HeaderStyle Width="80px" HorizontalAlign="Center"  />
                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="id" >
                        <ControlStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                    </asp:BoundField>
                </Columns>
                <PagerStyle CssClass="PagerStyle" />
                <PagerSettings Visible="false" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
            </asp:GridView><br />
        </center>
    </div>
    <center><asp:Label ID="lblShowResult" runat="server" ></asp:Label></center>
    </form>
</body>
</html>
