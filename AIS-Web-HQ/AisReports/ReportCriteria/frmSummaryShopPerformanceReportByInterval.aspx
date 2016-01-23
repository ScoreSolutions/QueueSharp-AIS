<%@ Page Title="" Language="VB" MasterPageFile="~/Template/ContentMasterPage.master" AutoEventWireup="false" CodeFile="frmSummaryShopPerformanceReportByInterval.aspx.vb" Inherits="ReportCriteria_frmSummaryShopPerformanceReportByInterval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../FormControls/ctlByDate.ascx" tagname="ctlByDate" tagprefix="uc3" %>
<%@ Register src="../UserControls/cmbComboBox.ascx" tagname="cmbComboBox" tagprefix="uc2" %>
<%@ Register src="../UserControls/ddlServiceType.ascx" tagname="ddlServiceType" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table align="center" border="0" cellpadding="0" cellspacing="0" width="80%">
    <tr align=center>
        <td>
            <table width="100%">
                <tr>
                    <th width="50%" height="23" id="Th1" style="color: #92b733"  align="left" >
                        Summary Shop Performance Report by Interval</th>
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
                <tr style="height:30px" >
                    <td align="right">Location :&nbsp;</td>
                    <td colspan="3" align="left" >
                        <asp:RadioButtonList ID="rdiLocation" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" AutoPostBack="true" >
                            <asp:ListItem Text="BKK&nbsp;&nbsp;" Value="BKK" ></asp:ListItem>
                            <asp:ListItem Text="UPC&nbsp;&nbsp;" Value="UPC" ></asp:ListItem>
                            <asp:ListItem Text="ALL" Value="ALL" Selected="True" ></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr style="height:30px" >
                    <td align="right">Shop Name :&nbsp;</td>
                    <td colspan="3" align="left" >
                        <asp:CheckBoxList ID="chkShopId" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Table">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                
                <!-- For Criteria User Control -->
                <uc3:ctlByDate ID="ctlByDate1" runat="server" />
                <tr style="height:30px" >
                    <td align="right">Interval Time :&nbsp;</td>
                    <td colspan="3" align="left" >
                        <uc2:cmbComboBox ID="cmbIntervalMinute" runat="server" AutoPosBack="false" IsDefaultValue="false" IsNotNull="false"  />
                        Minute
                    </td>
                </tr>
                <tr style="height:30px" >
                    <td align="right">Network Type :&nbsp;</td>
                    <td colspan="3" align="left" >
                        <asp:RadioButtonList ID="rdiNetworkType" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" >
                            <asp:ListItem Text="2G&nbsp;&nbsp;" Value="2G" ></asp:ListItem>
                            <asp:ListItem Text="3G&nbsp;&nbsp;" Value="3G" ></asp:ListItem>
                            <asp:ListItem Text="Other&nbsp;&nbsp;" Value="OTHER" ></asp:ListItem>
                            <asp:ListItem Text="All" Value="ALL" Selected="True" ></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr style="height:30px" >
                    <td align="right">Customer Segment :&nbsp;</td>
                    <td colspan="3" align="left" >
                        <asp:RadioButtonList ID="rdiSegment" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" >
                            <asp:ListItem Text="Mass&nbsp;&nbsp;" Value="MASS" ></asp:ListItem>
                            <asp:ListItem Text="Serenade&nbsp;&nbsp;" Value="SERENADE" ></asp:ListItem>
                            <asp:ListItem Text="All" Value="ALL" Selected="True" ></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <uc1:ddlServiceType ID="ddlServiceType1" runat="server" Width="150px" DataValueField="id" />
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

