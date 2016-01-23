<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VideoOnly.aspx.vb" Inherits="Video" %>
<%@ Register assembly="JW-FLV-Player-Control" namespace="JW_FLV_Player_Control" tagprefix="cc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style.css" rel="stylesheet" type="text/css" />
    <script src="Video/flowplayer-3.2.6.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">

    <center>
        <table border="0" class="formDialog shadow1 content">
 
            <tr>
                <td class="shoplist geen" width="380px"><strong><font>Agent</font><asp:Label ID="lblTest" 
                        runat="server"></asp:Label>
                    </strong>
                </td>
                <td class="shoplist" width="380px"><strong><font>Customer</font></strong>
                </td>
            </tr>
            <tr>
                <td class="shoplist" colspan="2" width="760px">
                                <cc:FlashPlayer ID="FP" runat="server" Height="305px" Width="760px"  AutoStart="true"  EnableViewState="False" />

                    <%--<%=strMediaDefault %>--%>

                </td>
            </tr>
        </table>
    </center>
   
    <div style="display:none">
    <asp:Label ID="lbl_shopid" runat="server"></asp:Label>
    <asp:Label ID="lbl_qid" runat="server"></asp:Label>
    <asp:Label ID="lbl_fdate" runat="server"></asp:Label>
    <asp:Label ID="lbl_ip" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
