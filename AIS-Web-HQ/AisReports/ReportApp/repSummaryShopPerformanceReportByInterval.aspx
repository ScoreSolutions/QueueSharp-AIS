﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="repSummaryShopPerformanceReportByInterval.aspx.vb" Inherits="ReportApp_repSummaryShopPerformanceReportByInterval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AIS Report Version <%=Config.getReportVersion()%></title>
    <link rel="stylesheet" type="text/css"href="../Template/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" class="table_input">
            <tr>
                <td class="section_header">
                    <input id="hddMode" runat="server" type="hidden" />                 
                    &nbsp;&nbsp; Show Reports&nbsp; : Summary Shop Performance by Interval
                </td>
            </tr>
            <tr>
                <td align="left" bgcolor="White">
                    <asp:Button ID="btnExport" runat="server" Text="Export Excel" 
                        CssClass="formDialog"/>
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