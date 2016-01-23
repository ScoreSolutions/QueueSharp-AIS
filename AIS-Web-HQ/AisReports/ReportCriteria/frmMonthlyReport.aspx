<%@ Page Title="" Language="VB" MasterPageFile="~/Template/ContentMasterPage.master"
    AutoEventWireup="false" CodeFile="frmMonthlyReport.aspx.vb" Inherits="ReportCriteria_frmMonthlyReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../FormControls/ctlByMonth.ascx" TagName="ctlByMonth" TagPrefix="uc5" %>
<%@ Register Src="../UserControls/ddlServiceType.ascx" TagName="ddlServiceType" TagPrefix="ServiceType" %>
<%@ Register Src="../UserControls/ddlShop.ascx" TagName="ddlShop" TagPrefix="Shop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="80%">
        <tr align="center">
            <td>
                <table width="100%">
                    <tr>
                        <th width="50%" height="23" id="Th1" style="color: #92b733" align="left">
                             Monthly Performance Report by Network and Service Type of Shop BKK and UPC
                        </th>
                        <th width="50%" class="title_page_current" id="screenName">
                            &nbsp;
                        </th>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="section_header" colspan="4">
                            Search
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- For Criteria User Control -->
                    <uc5:ctlByMonth ID="ctlByMonth1" runat="server" Visible="true" />
                    <tr>
                        <td valign="top" align="right">
                            Network Type :&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlNetworkServie" runat="server">
                                <asp:ListItem>2G</asp:ListItem>
                                <asp:ListItem>3G</asp:ListItem>
                                <asp:ListItem>Other</asp:ListItem>
                                <asp:ListItem Value="Null">All</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                        <td valign="middle" align="right">
                            Regional :&nbsp;
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rdbLocation" runat="server" 
                                RepeatDirection="Horizontal" AutoPostBack="True">
                                <asp:ListItem Value="BKK">BKK</asp:ListItem>
                                <asp:ListItem Value="UPC">UPC</asp:ListItem>
                                <asp:ListItem Value="" Selected="True">All</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="right">
                            Service Type :&nbsp;
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rdbSeviceType" runat="server" 
                                RepeatDirection="Horizontal" AutoPostBack="True">
                                <asp:ListItem Value="1" Selected="True">By Service</asp:ListItem>
                                <asp:ListItem Value="2">All Service</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                   
                    <ServiceType:ddlServiceType ID="ddlServiceType" runat="server" Visible="True" />
                    <Shop:ddlShop ID="ddlShop" runat="server" Visible="False" />
                    <tr style="height: 30px">
                        <td colspan="4" align="center">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                EnableTheming="True" HeaderText="กรุณากรอกข้อมูลให้ครบ!!" ShowSummary="False" />
                            &nbsp;
                            <asp:Button ID="btnSearch" CssClass="formDialog" runat="server" Text="Show Report" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
