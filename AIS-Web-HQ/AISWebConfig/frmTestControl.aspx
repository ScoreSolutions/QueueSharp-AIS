<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTestControl.aspx.vb" Inherits="frmTestControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    <div>
    


        <asp:TextBox ID="txt_Maximum_service" runat="server" ></asp:TextBox>
        <asp:NumericUpDownExtender ID="txt_Maximum_service_NumericUpDownExtender" runat="server"
            Enabled="True" Maximum="100" Minimum="0"  RefValues="" ServiceDownMethod="" ServiceDownPath="" 
            ServiceUpMethod="" Tag="1" Step="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txt_Maximum_service"
            Width="60">
        </asp:NumericUpDownExtender>
    </div>
    </form>
</body>
</html>
