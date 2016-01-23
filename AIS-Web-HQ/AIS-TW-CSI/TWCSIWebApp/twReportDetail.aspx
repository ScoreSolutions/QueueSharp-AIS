<%@ Page Title="" Language="VB" MasterPageFile="~/Template/ContentMasterPage.master" AutoEventWireup="false" CodeFile="twReportDetail.aspx.vb" Inherits="TWCSIWebApp_twReportDetail" %>

<%@ Register src="../UserControls/txtDate.ascx" tagname="txtDate" tagprefix="uc1" %>
<%@ Register src="../UserControls/cmbComboBox.ascx" tagname="cmbComboBox" tagprefix="uc2" %>
<%@ Register src="../FormControls/ctlSelectFilterTemplate.ascx" tagname="ctlSelectFilterTemplate" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td class="CssHead" align="left" width="100%" >
                Detail Report
                <asp:Label ID="lblMessage" runat="server" Font-Bold="true" Font-Size="12pt" ForeColor="Red" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" ><img src="../images/PageHeaderLine.png" alt="" width="100%" /></td>
        </tr>
        <tr><td align="left" >&nbsp;</td></tr>
        <tr style="height:30px" >
            <td align="left" class="CssSubHead"  >
                &nbsp;&nbsp;&nbsp;เงื่อนไขการค้นหา
            </td>
        </tr>
        <tr><td align="left" >&nbsp;</td></tr>
        <tr>
            <td >
                <div class="DivBoxRaius"  >
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" >   
                        <tr>
                            <td width="20%" >&nbsp;</td>
                            <td width="30%">&nbsp;</td>
                            <td width="15%" >&nbsp;</td>
                            <td width="35%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" class="Csslbl" >Complete Date&nbsp;</td>
                            <td align="left" class="Csslbl" >
                                <uc1:txtDate ID="txtDateFrom" runat="server" /> - 
                                <uc1:txtDate ID="txtDateTo" runat="server" />
                            </td>
                            <td align="right" class="Csslbl" >Template&nbsp;</td>
                            <td align="left" rowspan="6" valign="top"  >
                                <uc3:ctlSelectFilterTemplate ID="ctlSelectFilterTemplate1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="Csslbl" >Order Type&nbsp;</td>
                            <td align="left" class="Csslbl" >
                                <uc2:cmbComboBox ID="cmbSFFOrderTypeID" runat="server" Width="220px" IsNotNull="false" DefaultDisplay="All" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="Csslbl" >Status&nbsp;</td>
                            <td align="left" class="Csslbl" colspan="2" >
                                <uc2:cmbComboBox ID="cmbStatus" runat="server" Width="220px" IsNotNull="false" />
                            </td>
                        </tr>
                         <tr>
                            <td align="right" class="Csslbl" >Network Type&nbsp;</td>
                            <td align="left" class="Csslbl" colspan="2" >
                               <uc2:cmbComboBox ID="cmbNetworkType" runat="server" Width="220px" IsNotNull="false"  />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="Csslbl" >Region&nbsp;</td>
                            <td align="left" class="Csslbl" >
                                <uc2:cmbComboBox ID="cmbRegion" runat="server" Width="220px" IsNotNull="false" AutoPosBack="true" DefaultDisplay="All" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="Csslbl" >Province&nbsp;</td>
                            <td align="left" class="Csslbl" >
                                <uc2:cmbComboBox ID="cmbProvince" runat="server" Width="220px" IsNotNull="false" AutoPosBack="true" DefaultDisplay="All" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="Csslbl" >Location&nbsp;</td>
                            <td align="left" class="Csslbl" colspan="3" >
                                <uc2:cmbComboBox ID="cmbLocationCode" runat="server" Width="610px"  IsNotNull="false" DefaultDisplay="All" />
                            </td>
                        </tr>
                        <tr><td colspan="4">&nbsp;</td></tr>
                        <tr><td colspan="4">&nbsp;</td></tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr><td align="left" >&nbsp;</td></tr>
        <tr>
            <td align="center" >
                <asp:Button ID="btnExportExcel" runat="server" Width="120px" CssClass="formDialog"
                    Text="Export" />
                &nbsp;<asp:Button ID="btnSearch" runat="server" Width="120px" CssClass="formDialog" Text="Search" />
                &nbsp;<asp:Button ID="btnSearch0" runat="server" Width="120px" CssClass="formDialog"
                    Text="Cancel" />
                </td>
        </tr>
        <tr><td align="left" >&nbsp;</td></tr>
        <tr><td align="left" >&nbsp;</td></tr>
     </table>
</asp:Content>

