<%@ Page Title="" Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false"
    CodeFile="frmShopAssignment.aspx.vb" Inherits="frmShopAssignment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MycustomDG" Namespace    ="MycustomDG" TagPrefix="Saifi" %>
<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="Server">
    <table style="width: 900px;">
        <tr>
            <td class="tableHeader">
                <asp:Label ID="lblScreenName" runat="server" Text="Shop Setup >> Assignment"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 30px; text-align: center">
                <cc1:CustomUpdateProgress ID="progress" runat="server" ProgressImage="~/images/progress.gif" />
            </td>
        </tr>
        <tr>
            <td class="dvInnerHeader">
                Configuration
            </td>
        </tr>
        <tr>
            <td style="height: 23px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 95%;">
                    <tr>
                        <td style="padding-left: 20px; width: 177px; vertical-align: top;">
                            <table style="width: 250px;" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="font-family: Verdana; font-size: 13px; color: #ffffff; height: 30px; background-color: #0066CC;
                                        text-align: center; font-weight: bold;">
                                        <asp:Label ID="Label3" runat="server" Text="Date"></asp:Label>
                                    </td>
                                    <td>&nbsp;&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td width="100%" >
                                                    <table >
                                                        <tr>
                                                            <td >
                                                                <asp:TextBox ID="txtAssignDateFrom" runat="server" Enabled="false"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                                    TargetControlID="txtAssignDateFrom" PopupButtonID="imgAssignDateFrom" Format="dd/MM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="imgAssignDateFrom" runat="server" ImageUrl="~/images/calender_icon_score.jpg" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    To
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100%">
                                                    <table >
                                                        <tr>
                                                            <td >
                                                                <asp:TextBox ID="txtAssignDateTo" runat="server" Enabled="false"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                                                    TargetControlID="txtAssignDateTo" PopupButtonID="imgAssignDateTo" Format="dd/MM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="imgAssignDateTo" runat="server" ImageUrl="~/images/calender_icon_score.jpg" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 250px; vertical-align: top;">
                            <table style="width: 250px;" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="font-family: Verdana; font-size: 13px; color: #ffffff; height: 30px; background-color: #0066CC;
                                        text-align: center; font-weight: bold; width: 75px; border-right: solid 1px #BEBEBE">
                                        <asp:Label ID="Label4" runat="server" Text="Initial" ></asp:Label>
                                    </td>
                                    <td style="font-family: Verdana; font-size: 13px; color: #ffffff; height: 30px; background-color: #0066CC;
                                        text-align: center; font-weight: bold; width: 150px;">
                                        <asp:Label ID="Label1" runat="server" Text="Service"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <Saifi:MyDg ID="dgvservice" runat="server" AutoGenerateColumns="False" ImageFirst="/imgs/nav_left.gif"
                                            ImageLast="/imgs/nav_right.gif" ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif"
                                            ShowFirstAndLastImage="False" ShowPreviousAndNextImage="False" ShowHeader="False"
                                            Width="250px" Height="50px" BorderColor="Black" BorderStyle="Solid" Font-Bold="False"
                                            Font-Names="Tahoma" Font-Size="12px" ForeColor="#000000" AllowPaging="false" >
                                            <ItemStyle Height="25px" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Initial" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_id" runat="server" Style="text-align: center;" Visible="false"
                                                            Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                                        <asp:Label ID="lbl_code" runat="server" Style="text-align: center; "
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="85px"></ItemStyle>
                                                    <HeaderStyle Width="85px" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Service" ItemStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_name" runat="server" Style="text-align: center; width: 150px;"
                                                            Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="150px"></ItemStyle>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </Saifi:MyDg>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td  style="vertical-align: top; padding-left:10px;">
                            <table style="width: 250px;" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="font-family: Verdana; font-size: 13px; color: #ffffff; height: 30px; background-color: #0066CC;
                                        text-align: center; font-weight: bold; width: 75px; border-right: solid 1px #BEBEBE">
                                        <asp:Label ID="Label2" runat="server" Text="Edit Data By Date" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" >
                                        <asp:Calendar runat="server" ID="cldViewEdit"></asp:Calendar>
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 177px">
                            &nbsp;
                        </td>
                        <td  style="width: 233px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px; vertical-align:top; height: 432px;">
                <asp:UpdatePanel ID="pnlGrdDynamic" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel6" runat="server" Width="100%" >
                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="vertical-align: top; height: 272px; padding-left: 10px;">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <Saifi:MyDg ID="GrdDynamic" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif" ImageNext="/imgs/bulletr.gif"
                                                        ImagePrevious="/imgs/bulletl.gif" ShowFirstAndLastImage="False"
                                                        ShowPreviousAndNextImage="False" AllowPaging="false" CssClass="gridassignmentitem" >
                                                        <HeaderStyle CssClass="gridassheader" />
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="User" ItemStyle-Width="150px" HeaderStyle-BackColor="#0066CC" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_userid" runat="server" Style="text-align: left;" Visible="false"
                                                                        Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_username" runat="server" Style="text-align: left; width: 150px;"
                                                                        Text='<%#DataBinder.Eval(Container.DataItem, "fullname")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle BackColor="#0066CC" Width="150px" />
                                                                <ItemStyle Width="150px" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Appointment" ItemStyle-Width="150px" HeaderStyle-BackColor="#0066CC">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Appointment" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Button ID="btn_Appointment" runat="server" text="Assign"  
                                                                        BorderStyle="None"  width="100px" onclick="btn_Appointment_Click"  />
                                                                </ItemTemplate>
                                                                <HeaderStyle BackColor="#0066CC" Width="100px" />
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Primary Active Service" ItemStyle-Width="100px" HeaderStyle-BackColor="#228B22">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="cbo_primary" runat="server" class="detailedViewTextBox" 
                                                                        MaxLength="100" onblur="this.className='detailedViewTextBox'" 
                                                                        onfocus="this.className='detailedViewTextBoxOn'" Width="100px"  
                                                                        AutoPostBack="true" onselectedindexchanged="cbo_primary_SelectedIndexChanged"
                                                                        >
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle BackColor="ForestGreen"   Width="100px"/>
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Secondary Active Service"  HeaderStyle-HorizontalAlign="Center"
                                                                HeaderStyle-BackColor="DarkOrange">
                                                                <ItemTemplate>
                                                                    <asp:CheckBoxList ID="chk_secondary_service" runat="server"  
                                                                        CellPadding="0" CellSpacing="0" 
                                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" 
                                                                        onselectedindexchanged="chk_secondary_service_SelectedIndexChanged" >
                                                                    </asp:CheckBoxList>
                                                                </ItemTemplate>
                                                                <HeaderStyle BackColor="DarkOrange"  />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </Saifi:MyDg>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 23px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 23px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="width: 200px; text-align: right;">
                            &nbsp;
                        </td>
                        <td style="width: 287px">
                            <asp:Button ID="cmd_save" runat="server" Text="Save" />
                            <asp:Button ID="cmd_clear" runat="server" Style="margin-left: 0px" Text="Clear" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
