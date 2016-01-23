<%@ Page Title="" Language="VB" MasterPageFile="~/Template/ContentMasterPage.master" AutoEventWireup="false" CodeFile="frmMonthlySummaryReport.aspx.vb" Inherits="ReportCriteria_frmMonthlySummaryReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../FormControls/ctlByMonth.ascx" tagname="ctlByMonth" tagprefix="uc5" %>
<%@ Register Src="../UserControls/ddlServiceType.ascx" TagName="ddlServiceType" TagPrefix="ServiceType" %>
<%@ Register Src="../UserControls/ddlShop.ascx" TagName="ddlShop" TagPrefix="Shop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style3
        {
            width: 69%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table align="center" border="0" cellpadding="0" cellspacing="0" width="80%">
    <tr align=center>
        <td>
            <table width="100%">
                <tr>
                    <th height="23" id="Th1" style="color: #92b733"  align="left" class="style3" >
                        Monthly Summary Report By Network Type All Service Type</th>
                    <th width="50%" class="title_page_current" id="screenName" >&nbsp;
                        
                    </th>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="section_header" colspan="4">
                        Search
                    </td>
                </tr>
                <tr><td colspan="4">&nbsp;</td></tr>

                <!-- For Criteria User Control -->
                <uc5:ctlbymonth ID="ctlByMonth1" runat="server" Visible="true" />
                
                          <tr>
                    <td valign="top" align="right">Network Type :&nbsp;</td>
                    <td align="left" >
                        <asp:DropDownList ID="ddlNetworkServie" runat="server">
                            <asp:ListItem>2G</asp:ListItem>
                            <asp:ListItem>3G</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                            <asp:ListItem Value="Null">All</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                
                          <tr>
                    <td valign="middle" align="right">Regional :&nbsp;</td>
                    <td align="left" >
                        <asp:RadioButtonList ID="rdbLocation" runat="server" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="BKK" >BKK</asp:ListItem>
                            <asp:ListItem Value="UPC">UPC</asp:ListItem>
                            <asp:ListItem Value="" Selected="True">All</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <ServiceType:ddlServiceType ID="ddlServiceType" runat="server" Visible="True" />
               <tr style="height: 30px">
                    <td colspan="4" align="center">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                            ShowMessageBox="True" EnableTheming="True" 
                            HeaderText="กรุณากรอกข้อมูลให้ครบ!!" ShowSummary="False" />
                        &nbsp;
                        <asp:Button ID="btnSearch" CssClass="formDialog" runat="server" Text="Show Report" />
                    </td>
                </tr>
             </table>
        </td>
    </tr>
</table>
</asp:Content>

