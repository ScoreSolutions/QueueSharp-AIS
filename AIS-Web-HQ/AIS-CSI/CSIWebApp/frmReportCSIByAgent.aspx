<%@ Page Title="" Language="VB" MasterPageFile="~/Template/ContentMasterPage.master" AutoEventWireup="false" CodeFile="frmReportCSIByAgent.aspx.vb" Inherits="CSIWebApp_frmReportCSIByAgent" %>
<%@ Register src="../UserControls/txtDate.ascx" tagname="txtDate" tagprefix="uc1" %>
<%@ Register src="../UserControls/cmbComboBox.ascx" tagname="cmbComboBox" tagprefix="uc2" %>
<%@ Register src="../FormControls/ctlSelectFilterTemplate.ascx" tagname="ctlSelectFilterTemplate" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td class="CssHead" align="left" width="100%" >
                <asp:Label ID="lblReportName" runat="server" Text="CSI By Agent Report"></asp:Label>
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
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                        <td width="5%">
                        </td>
                            <td width="45%" valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="50%">
                                            &nbsp;
                                        </td>
                                        <td width="50%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Csslbl">
                                            Period Date&nbsp;
                                        </td>
                                        <td align="left" class="Csslbl">
                                            <uc1:txtDate ID="txtDateFrom" runat="server" />
                                            -
                                            <uc1:txtDate ID="txtDateTo" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Csslbl">
                                            Service&nbsp;
                                        </td>
                                        <td align="left" class="Csslbl">
                                            <uc2:cmbComboBox ID="cmbServiceID" runat="server" Width="220px" IsNotNull="false"
                                                DefaultDisplay="Select All" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Csslbl">
                                            Status&nbsp;
                                        </td>
                                        <td align="left" >
                                            <asp:Label ID="lblStatus" runat="server" Text="Complete"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Csslbl">
                                            Network Type&nbsp;
                                        </td>
                                        <td align="left" class="Csslbl" colspan="2">
                                            <uc2:cmbComboBox ID="cmbNetworkType" runat="server" Width="220px" IsNotNull="false" />
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                            <td width="45%" valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="50%">
                                            &nbsp;
                                        </td>
                                        <td width="50%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Csslbl">
                                            Location&nbsp;
                                        </td>
                                        <td align="left" class="Csslbl">
                                            <uc2:cmbComboBox ID="cmbShopID" runat="server" Width="220px" IsNotNull="false" AutoPosBack="true"
                                                DefaultDisplay="Select All" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Csslbl">
                                            Agent&nbsp;
                                        </td>
                                        <td align="left" class="Csslbl">
                                            <uc2:cmbComboBox ID="cmbShopUserID" runat="server" Width="220px" IsNotNull="false"
                                                DefaultValue="" DefaultDisplay="Select All" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Csslbl" valign="top">
                                            Template&nbsp;
                                        </td>
                                        <td align="left" rowspan="4">
                                            <uc3:ctlSelectFilterTemplate ID="ctlSelectFilterTemplate1" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="5%"></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                       
                    </table>
                </div>
            </td>
        </tr>
        <tr><td align="left" >&nbsp;</td></tr>
        <tr>
            <td align="center" >
                <asp:Button ID="btnExportExcel" runat="server" Width="120px" 
                    CssClass="formDialog" Text="Export" />
                &nbsp;
                <asp:Button ID="btnSearch" runat="server" Width="120px" CssClass="formDialog" Text="Search" />
                &nbsp; <asp:Button ID="btnSearch0" runat="server" Width="120px" CssClass="formDialog"
                    Text="Cancel" />
                &nbsp;&nbsp;
                </td>
        </tr>
        <tr><td align="left" >&nbsp;</td></tr>
        <tr><td align="left" >&nbsp;</td></tr>
       
        <tr>
            <td align="left" ><asp:Label ID="lblDesc" runat="server" ></asp:Label></td>
        </tr>
     </table>
</asp:Content>

