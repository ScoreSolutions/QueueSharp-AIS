<%@ Page Title="" Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false" CodeFile="frmShopAutoForce.aspx.vb" Inherits="frmShopAutoForce" %>

<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
<style type="text/css">
.style1
{
	font-weight: bold; 
	font-size: 11px; 
	font-family: Arial, Helvetica, sans-serif;
	/*text-align:left;*/
	color: #666666;
	padding-left:10px;
	padding-right:10px;
	white-space:nowrap;
	font-family : Verdana;
	
}
</style>
<script type="text/javascript">

    function ChangeDate() {
        var bt = document.getElementById("<%=btnTemp.ClientID%>");
        if (bt) {
            bt.click();
            return false;
        }
    }

</script>

    <table style="width: 900px;">
        <tr>
            <td class="tableHeader">
                <asp:Label ID="lblScreenName" runat="server" 
                    Text="Shop Settup &gt;&gt; Auto Force"></asp:Label>
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
            <td>
            </td>
        </tr>
        <tr>
            <td >
               
               
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                           
                            <table >
                                <tr>
                                    <td class="style1" >
                                    <asp:Panel ID="myPanel" runat="server" GroupingText="Date Range"  Width="300px">
                                        <table>
                                            <tr>
                                                <td align="center"> 
                                                <asp:TextBox ID="txtDateFrom" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                                                <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="txtDateFrom" PopupButtonID="imgcalendarF" Format="dd/MM/yyyy"  OnClientDateSelectionChanged="ChangeDate" >
                                                </asp:CalendarExtender>
                                                <asp:Button ID="btnTemp" runat="server" CssClass="zHidden"  OnClick="btnTemp_Click" />
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
                                    <td  class="style1" align="left" >
                                    <asp:Panel ID="Panel2" runat="server" GroupingText="Recurrence Day"  Width="300px">
                                        
                                        
                                        <asp:CheckBoxList ID="chkRecurrenceDay" runat="server" RepeatColumns="2" 
                                            AutoPostBack="True" Width="200px">
                                        </asp:CheckBoxList>
                                      
                                    </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                        <td>
                        
                            <table style="width: 100%">
                                <tr>
                                    <td style="padding-top:5px" class="style1" align="left">
                                   
                                        <asp:Panel ID="Panel3" runat="server"  BorderColor="Gray" BorderWidth="1px" Width="500px" >
                                            <table >
                                            <tr><td><br /></td></tr>
                                                <tr>
                                                    <td align="right">
                                                    <asp:Label ID="lbl1" runat="server" Text="Your selected date range : " 
                                                            Width="200px"></asp:Label>
                                                    </td>
                                                    <td >
                                                    <asp:Label ID="lblDateRange" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td align="right">
                                                     <asp:Label ID="lbl2" runat="server" Text="Shop Operation Hour : " Width="200px"></asp:Label>
                                                    </td>
                                                    <td>
                                                    <asp:Label ID="lblShopOperationH" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td align="right" valign="top">
                                                    <asp:Label ID="lbl3" runat="server" Text="Recuring On : " Width="200px"></asp:Label>
                                                    </td>
                                                    <td>
                                                    <asp:Label ID="lblRecuringOn" runat="server"></asp:Label>
                                                    
                                                    </td>
                                                </tr>
                                                <tr><td> <br /></td></tr>
                                            </table>
                                            
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr class="style1" align="left">
                                    <td >
                                        <table>
                                            <tr>
                                                <td>
                                                &nbsp;<asp:Label ID="ibl7" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                        <asp:Label ID="lbl5" runat="server" Text="Auto Force Next Queue : "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAutoForce" runat="server" Height="20px" class="TextView"
                                                         OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)" ></asp:TextBox>
                                                    <asp:NumericUpDownExtender ID="numSecondAutoForce" runat="server"
                                                        Enabled="True" Maximum="10" Minimum="1"  RefValues="" 
                                                        ServiceDownMethod="" ServiceDownPath="" 
                                                        ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtAutoForce"
                                                        Width="60" >
                                                    </asp:NumericUpDownExtender>
                                                </td>
                                                <td>
                                                 <asp:Label id="lbl6" runat="server" Text="Second"></asp:Label>
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
        </tr>
      
        <tr>
            <td class="dvInnerHeader">
            <br />
            <br />
                Force Time Slot(S)</td>
        </tr>
        <tr align="left" class="style1">
            <td >
               <asp:CheckBoxList ID="chkListTimeSlot" runat="server" RepeatColumns="3" 
                    Width="500px" ></asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
            <br />
               <asp:Button ID="btnSave" runat="server" text="Save Slot"/> 
            </td>
        </tr>
    </table>
</asp:Content>

