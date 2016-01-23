<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmReportPreview.aspx.vb" Inherits="CSIWebApp_frmReportPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css"href="../Template/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table  class="table_input">
            <tr>
                <td class="section_header">                
                    &nbsp;&nbsp; <asp:Label ID="lblReportName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" >
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td width="50%"><asp:Label ID="lblRowCount" runat="server"></asp:Label> </td>
                            <td width="50%" align="right" >
                                <asp:ImageButton ID="imgExport" runat="server" ImageUrl="~/images/ico_excel.jpg" />
                                <asp:LinkButton ID="likExport" runat="server" Text="Export" CssClass="Csslbl" ></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>                             
                    <asp:Label ID="lblReportDesc" runat="server"></asp:Label>
                </td>
           </tr>
            <tr>
                <td align="center" >
                    <asp:Label ID="lblerror" runat="server" Text="** Not Found **" Font-Bold="True" 
                        Font-Italic="False" Font-Names="Tahoma" Font-Size="Medium" ForeColor="#990000"></asp:Label>
                        <br /><br />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
