<%@ Page Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false"
 CodeFile="frmShopGenerateTimeSlot.aspx.vb" Inherits="frmShopGenerateTimeSlot"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="MycustomDG" namespace="MycustomDG" tagprefix="Saifi" %>
<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>

<%@ Register src="UserControls/ctlBranchSelected.ascx" tagname="ctlBranchSelected" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
<script type="text/javascript">

    function ChangeDate() {
        var bt = document.getElementById("<%=btnTemp.ClientID%>");
        if (bt) {
            bt.click();
            return false;
        }
    }

</script>

    <table style="width:900px;">
        <tr>
            <td class="tableHeader">
                <asp:Label ID="lblScreenName" runat="server" Text="Shop Setup >> Appointment Slot"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 30px; text-align: center">
                <cc1:customupdateprogress id="progress" runat="server" progressimage="~/images/progress.gif"
                    text="Loading..." delay="1000" />
            </td>
        </tr>
                
       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
       <uc1:ctlBranchSelected ID="ctlBranchSelected1" runat="server" />
       </ContentTemplate>
        </asp:UpdatePanel>--%>
         <tr>
             <td>
                 <uc1:ctlBranchSelected ID="ctlBranchSelected1" runat="server" />
             </td>
        </tr>
        <tr>
            <td class="dvInnerHeader">Times Slot</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr><td>
            <table style="width: 100%" >
                <tr>
                    <td style="width: 525px">
                        <table style="width: 100%">
                            <tr>
                                <td>
                            <asp:Panel ID="myPanel" runat="server" GroupingText="Date Range" Width="100%"  >
                                <table>
                                    <tr>
                                        <td align="center"> 
                                        <asp:TextBox ID="txtDateFrom" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="txtDateFrom" PopupButtonID="imgcalendarF" Format="dd/MM/yyyy"  OnClientDateSelectionChanged="ChangeDate" >
                                        </asp:CalendarExtender>
                                        <asp:Button ID="btnTemp" runat="server" CssClass="zHidden"  />
                                        </td>
                                        <td><asp:ImageButton ID="imgcalendarF" runat="server" ImageUrl="~/images/calender_icon_score.jpg" /></td>
                                    </tr>
                                    <tr>
                                        <td >
                                        TO
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                        <asp:TextBox ID="txtDateTo" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                                         <asp:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="txtDateTo" PopupButtonID="imgcalendarT" Format="dd/MM/yyyy" OnClientDateSelectionChanged="ChangeDate">
                                        </asp:CalendarExtender>
                                        </td>
                                        <td><asp:ImageButton ID="imgcalendarT" runat="server" ImageUrl="~/images/calender_icon_score.jpg" /></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                            <asp:Panel ID="Panel2" runat="server" GroupingText="Recurrence Day" Width="100%"  >
                                <asp:CheckBoxList ID="chkRecurrenceDay" runat="server" RepeatColumns="2" 
                                    AutoPostBack="True" Width="200px">
                                </asp:CheckBoxList>
                            </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                            <asp:Panel ID="Panel3" runat="server" GroupingText="  " Width="100%"  >
                                <table >
                               
                                    <tr>
                                        <td  align="left">
                                        <asp:Label ID="lbl1" runat="server" Text="Your selected date range : " 
                                                Width="150px"></asp:Label>
                                        </td >
                                        <td align="left">
                                        <asp:Label ID="lblDateRange" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td  align="left" valign="top">
                                         <asp:Label ID="lbl2" runat="server" Text="Shop Operation Hour : " Width="150px"></asp:Label>
                                        </td>
                                        <td  align="left" valign="top">
                                            <asp:Label ID="lblShopOperationH" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td valign="top"  align="left">
                                        <asp:Label ID="lbl3" runat="server" Text="Recuring On : " Width="150px"></asp:Label>
                                        </td>
                                        <td  align="left">
                                        <asp:Label ID="lblRecuringOn" runat="server"></asp:Label>
                                        
                                        </td>
                                    </tr>
                                  
                                </table>
                            </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                            <Saifi:MyDg ID="gvService" runat="server" AllowPaging="false" 
                                AutoGenerateColumns="False" BorderStyle="None" CssClass="Grid_DetailItem" 
                                ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif" 
                                ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif" 
                                ShowFirstAndLastImage="False" ShowPreviousAndNextImage="False" Width="200px">
                                <AlternatingItemStyle CssClass="Grid_Detail" />
                                <ItemStyle CssClass="Grid_Detail" />
                                <PagerStyle CssClass="" Mode="NumericPages" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Services" ItemStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                            <asp:Label ID="lbl_item_name" runat="server" 
                                                Text='<%#DataBinder.Eval(Container.DataItem, "item_name")%>'></asp:Label>
                                            <asp:Label ID="lbl_id" runat="server" 
                                                Text='<%#DataBinder.Eval(Container.DataItem, "id")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" Width="200px" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle CssClass="Grid_DetailHeader" />
                            </Saifi:MyDg>
                        </td>
                </tr>
            </table>
            </td></tr>
        <tr><td>
                <asp:Panel ID="Panel1" runat="server" GroupingText="Slot Constraints" 
                Width="100%"  >
                    <table border="0" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td style="width: 138px; height: 27px" class="dvtCellLabel">
                                Start Time :</td>
                            <td  align="left" >
                                <asp:DropDownList ID="ddlStartTime" runat="server" Width="100px">
                                </asp:DropDownList>
                            </td>
                            <td class="dvtCellLabel">
                                End time :</td>
                            <td  align="left" >
                                <asp:DropDownList ID="ddlEndTime" runat="server" Width="100px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="dvtCellLabel"  align="left">No. of counter(s) :</td>
                            <td class="dvtCellInfo" align="left">
                                <asp:TextBox ID="txt_No_of_counter" runat="server" class="detailedViewTextBox" MaxLength="2"
                                    onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                    Width="150px" Height="20px"></asp:TextBox>
                                <asp:NumericUpDownExtender ID="txt_No_of_counter_NumericUpDownExtender" runat="server" Enabled="True"
                                    Maximum="10" Minimum="1" RefValues="" ServiceDownMethod="" ServiceDownPath="" 
                                    ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txt_No_of_counter"
                                    Width="60">
                                </asp:NumericUpDownExtender>
                            </td>
                            <td class="dvtCellLabel">Slot Time (mins) :</td>
                            <td class="dvtCellInfo" align="left" >
                                   <asp:TextBox ID="txt_Slot_Time" runat="server" class="TextView" 
                                        MaxLength="2" Height="20px" 
                                       OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)" 
                                       Width="150px"></asp:TextBox>
                                   <asp:NumericUpDownExtender ID="txt_Slot_Time_NumericUpDownExtender"  
                                       runat="server" Enabled="True" Maximum="120" Minimum="30" RefValues="30;60;90;120" 
                                       ServiceDownMethod="" ServiceDownPath="" ServiceUpMethod="" Tag="" 
                                       TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txt_Slot_Time" 
                                       Width="60" Step="30" >
                                   </asp:NumericUpDownExtender>
                                    Minute
                            </td>
                        </tr>
                        <tr>
                            <td class="dvtCellLabel"  align="left">Appointment before shop close in :</td>
                            <td class="dvtCellInfo" align="left" colspan="3">
                                <asp:TextBox ID="txtBeforeShopClose" runat="server" class="TextView" 
                                        MaxLength="2" Height="20px" 
                                       OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)" 
                                       Width="150px"></asp:TextBox>
                                   <asp:NumericUpDownExtender ID="numBeforeShopClose"  
                                       runat="server" Enabled="True" Maximum="120" Minimum="30" RefValues="30;60;90;120" 
                                       ServiceDownMethod="" ServiceDownPath="" ServiceUpMethod="" Tag="" 
                                       TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtBeforeShopClose" 
                                       Width="60" Step="30" >
                                   </asp:NumericUpDownExtender>
                                    Minute
                            </td>
                        </tr>
                        <tr><td colspan="4">&nbsp;</td></tr>
                        <tr><td colspan="4" align="center" >
                            <asp:Button ID="CmdGenSlot" runat="server" Text="Generate Slot" />
                        </td></tr>
                        
                    </table>
                </asp:Panel>
            </td></tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td class="dvtCellLabel_Header" align="center" >Appointment Slot</td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width:90%;">
                    <tr>
                        <td   style="width: 194px; padding-left:10px;">
                            <asp:Calendar ID="Calendar1" runat="server" SelectionMode="DayWeek" Visible="false" ></asp:Calendar>
                        </td>
                        <td valign="top" >
                            <asp:CheckBoxList ID="chkListTimeSlot" runat="server" RepeatColumns="5" RepeatDirection="Vertical" 
                                Width="500px" ></asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;</td>
                        <td style="width: 137px" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;</td>
                        <td style="width: 137px" >
                            &nbsp;</td>

                    </tr>
                    <tr>
                        <td colspan="2" align="center" >
                            <asp:Button ID="CmdSave" runat="server" Text="Save" />
                            <asp:Button ID="CmdClear" runat="server" Text="Clear" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

