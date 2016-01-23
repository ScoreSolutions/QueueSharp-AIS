<%@ Page Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false" EnableEventValidation="false"
    CodeFile="frmMSTWebAppSetupAppointment.aspx.vb" Inherits="frmMSTWebAppSetupAppointment"  %>

<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="MycustomDG" namespace="MycustomDG" tagprefix="Saifi" %>
<%@ Register src="UserControls/ctlBrowseFile.ascx" tagname="ctlBrowseFile" tagprefix="uc1" %>
<%@ Register src="UserControls/ctlShopSelected.ascx" tagname="ctlShopSelected" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td class="tableHeader">
                <asp:Label ID="lblScreenName" runat="server" Text="Web Applicaiton Setup >> Appointment"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 19px" align="left" >
                <table style="width: 100%;">
                    <tr>
                        <td class="dvInnerHeader">
                            Configuration
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 216px; height: 27px" class="dvtCellLabel">
                                        Minimum Appointment Hour :
                                    </td>
                                    <td style="width: 231px; height: 27px" class="dvtCellInfo" colspan="2" >
                                        <asp:TextBox ID="txtMinAppointmentHour" runat="server" Height="20px" class="detailedViewTextBox"
                                             onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                             OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)" ></asp:TextBox>
                                        <asp:NumericUpDownExtender ID="numMinAppointmentHour" runat="server"
                                            Enabled="True" Maximum="6" Minimum="1"  RefValues="" 
                                            ServiceDownMethod="" ServiceDownPath="" 
                                            ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtMinAppointmentHour"
                                            Width="60" >
                                        </asp:NumericUpDownExtender>
                                        Hour
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dvtCellLabel">
                                        Maximum Appointment Day :
                                    </td>
                                    <td class="dvtCellInfo" colspan="2" >
                                        <asp:TextBox ID="txtMaxAppointmentDay" runat="server" Height="20px" class="detailedViewTextBox"
                                             onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                             OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)"  ></asp:TextBox>
                                        <asp:NumericUpDownExtender ID="numMaxAppointmentDay" runat="server"
                                            Enabled="True" Maximum="7" Minimum="0"  RefValues="" 
                                            ServiceDownMethod="" ServiceDownPath="" 
                                            ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtMaxAppointmentDay"
                                            Width="60" >
                                        </asp:NumericUpDownExtender>
                                        Day
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dvtCellLabel">
                                        Maximum Edit Appointment Hour :
                                    </td>
                                    <td class="dvtCellInfo" colspan="2" >
                                        <asp:TextBox ID="txtMaxEditAppointmentHour" runat="server" Height="20px" class="detailedViewTextBox"
                                             onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                             OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)"  ></asp:TextBox>
                                        <asp:NumericUpDownExtender ID="numMaxEditAppointmentHour" runat="server"
                                            Enabled="True" Maximum="6" Minimum="1"  RefValues="" ServiceDownMethod="" ServiceDownPath="" 
                                            ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtMaxEditAppointmentHour"
                                            Width="60" >
                                        </asp:NumericUpDownExtender>
                                        Hour
                                    </td>
                                </tr>
                                <tr style="display:none" >
                                    <td class="dvtCellLabel">
                                        Maximum Appointment Service :
                                    </td>
                                    <td class="dvtCellInfo" colspan="2" >
                                        <asp:TextBox ID="txtMaxAppointmentService" runat="server" Height="20px" class="detailedViewTextBox"
                                             onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                             OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)"  ></asp:TextBox>
                                        <asp:NumericUpDownExtender ID="numMaxAppointmentService" runat="server"
                                            Enabled="True" Maximum="6" Minimum="1"  RefValues="" ServiceDownMethod="" ServiceDownPath="" 
                                            ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtMaxAppointmentService"
                                            Width="60" >
                                        </asp:NumericUpDownExtender>
                                        Hour
                                    </td>
                                </tr>
                                <tr  >
                                    <td class="dvtCellLabel">
                                        SMS and Email Alert Time :
                                    </td>
                                    <td class="dvtCellInfo" colspan="2" >
                                        <asp:DropDownList ID="ddlSmsTimeFrom" runat="server" Width="100px">
                                            <asp:ListItem Text="08:00" Value="08:00"></asp:ListItem>
                                            <asp:ListItem Text="09:00" Value="09:00"></asp:ListItem>
                                            <asp:ListItem Text="10:00" Value="10:00"></asp:ListItem>
                                            <asp:ListItem Text="11:00" Value="11:00"></asp:ListItem>
                                            <asp:ListItem Text="12:00" Value="12:00"></asp:ListItem>
                                            <asp:ListItem Text="13:00" Value="13:00"></asp:ListItem>
                                            <asp:ListItem Text="14:00" Value="14:00"></asp:ListItem>
                                            <asp:ListItem Text="15:00" Value="15:00"></asp:ListItem>
                                            <asp:ListItem Text="16:00" Value="16:00"></asp:ListItem>
                                            <asp:ListItem Text="17:00" Value="17:00"></asp:ListItem>
                                            <asp:ListItem Text="18:00" Value="18:00"></asp:ListItem>
                                            <asp:ListItem Text="19:00" Value="19:00"></asp:ListItem>
                                            <asp:ListItem Text="20:00" Value="20:00"></asp:ListItem>
                                            <asp:ListItem Text="21:00" Value="21:00"></asp:ListItem>
                                            <asp:ListItem Text="22:00" Value="22:00"></asp:ListItem>
                                        </asp:DropDownList>
                                        -
                                        <asp:DropDownList ID="ddlSmsTimeTo" runat="server" Width="100px">
                                            <asp:ListItem Text="08:00" Value="08:00"></asp:ListItem>
                                            <asp:ListItem Text="09:00" Value="09:00"></asp:ListItem>
                                            <asp:ListItem Text="10:00" Value="10:00"></asp:ListItem>
                                            <asp:ListItem Text="11:00" Value="11:00"></asp:ListItem>
                                            <asp:ListItem Text="12:00" Value="12:00"></asp:ListItem>
                                            <asp:ListItem Text="13:00" Value="13:00"></asp:ListItem>
                                            <asp:ListItem Text="14:00" Value="14:00"></asp:ListItem>
                                            <asp:ListItem Text="15:00" Value="15:00"></asp:ListItem>
                                            <asp:ListItem Text="16:00" Value="16:00"></asp:ListItem>
                                            <asp:ListItem Text="17:00" Value="17:00"></asp:ListItem>
                                            <asp:ListItem Text="18:00" Value="18:00"></asp:ListItem>
                                            <asp:ListItem Text="19:00" Value="19:00"></asp:ListItem>
                                            <asp:ListItem Text="20:00" Value="20:00"></asp:ListItem>
                                            <asp:ListItem Text="21:00" Value="21:00"></asp:ListItem>
                                            <asp:ListItem Text="22:00" Value="22:00"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dvtCellLabel" colspan="3">
                                        If No show by appointment
                                        <asp:TextBox ID="txtAppointmentNoShowQty" runat="server" Height="20px" class="detailedViewTextBox"
                                             onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                             OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)" ></asp:TextBox>
                                        <asp:NumericUpDownExtender ID="numAppointmentNoShowQty" runat="server"
                                            Enabled="True" Maximum="50" Minimum="1"  RefValues="" ServiceDownMethod="" ServiceDownPath="" 
                                            ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtAppointmentNoShowQty"
                                            Width="60"  >
                                        </asp:NumericUpDownExtender>
                                        Time(s)   
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        within
                                        <asp:TextBox ID="txtAppointmentWithinDay" runat="server" Height="20px" class="detailedViewTextBox"
                                             onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                             OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)" ></asp:TextBox>
                                        <asp:NumericUpDownExtender ID="numAppointmentWithinDay" runat="server"
                                            Enabled="True" Maximum="300" Minimum="0"  RefValues="" ServiceDownMethod="" ServiceDownPath="" 
                                            ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtAppointmentWithinDay"
                                            Width="60"  >
                                        </asp:NumericUpDownExtender>
                                        day(s)
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        then the customer cannot pre-book for the next 
                                        <asp:TextBox ID="txtNoBookDay" runat="server" Height="20px" class="detailedViewTextBox"
                                             onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                             OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)" ></asp:TextBox>
                                        <asp:NumericUpDownExtender ID="numNoBookDay" runat="server"
                                            Enabled="True" Maximum="300" Minimum="0"  RefValues="" ServiceDownMethod="" ServiceDownPath="" 
                                            ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtNoBookDay"
                                            Width="60"  >
                                        </asp:NumericUpDownExtender>
                                        day(s)
                                    </td>
                                </tr>
                                <tr><td colspan="3">&nbsp;</td></tr>
                                <tr>
                                    <td style="width: 100px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 260px">
                                        <asp:Button ID="btn_save" runat="server" Text="Save" />
                                        <asp:Button ID="btn_clear" runat="server" Text="Clear" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr><td colspan="3">&nbsp;</td></tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
