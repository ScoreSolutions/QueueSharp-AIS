<%@ Page Title="" Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false" CodeFile="frmMSTSkill.aspx.vb" Inherits="frmMSTSkill" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
<table style="width: 900px" border="0" cellpadding="0" cellspacing="0" >
        <tr>
            <td class="tableHeader">
                <asp:Label ID="Label2" runat="server" Text="Central Setup >> Skill"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 30px; text-align: center">
                <cc1:customupdateprogress id="progress" runat="server" progressimage="~/images/progress.gif"  />
            </td>
        </tr>
        <tr>
            <td class="dvInnerHeader">
                Configuration
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width:70%;">
                    <tr>
                        <td style="width: 122px; height: 27px" class="dvtCellLabel">* Skill :</td>
                        <td class="dvtCellInfo">
                            <asp:TextBox ID="txt_skill" runat="server" class="detailedViewTextBox" MaxLength="100"
                                onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                Width="150px" Height="20px" Style="margin-left: 0px"></asp:TextBox>
                        </td>
                        <td rowspan="6" style="width: 250px;" align="left" >
                            <div id="divService" style="overflow:scroll;overflow-x:hidden; height:150px;width:100%;border-width:1px;border-style: inset;box-shadow:inset 1px 1px 5px gray;behavior: url('../Template/PIE.htc');">
                                <asp:CheckBoxList ID="chkService" runat="server" >
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 122px; height: 27px" class="dvtCellLabel">&nbsp;</td>
                        <td align="left">
                            <asp:CheckBox ID="chk_appointment" runat="server" Text="Appointment" />
                            <asp:TextBox ID="txt_id" runat="server" BorderStyle="None" ForeColor="White" Visible="False">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 122px; height: 27px" class="dvtCellLabel">&nbsp;</td>
                        <td align="left"><asp:CheckBox ID="chk_active" runat="server" Text="Active" 
                                Checked="True" /></td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="CmdSave" runat="server" Text="Save" />
                            <asp:Button ID="CmdClear" runat="server" Text="Clear" />
                        </td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                </table>
            </td>
        </tr>
        <tr><td colspan='2'>&nbsp;</td></tr>
        <tr>
            <td colspan='2'>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: right;" class="dvtCellInfo">
                            <asp:Panel ID="Panel3" runat="server" Width="100%" Height="20px">
                                <asp:TextBox ID="txt_search" runat="server" class="detailedViewTextBox" MaxLength="100"
                                    onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                    Width="150px" Height="20px"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="txt_search_TextBoxWatermarkExtender" runat="server"
                                    Enabled="True" TargetControlID="txt_search" WatermarkText="Search">
                                </asp:TextBoxWatermarkExtender>
                                <asp:ImageButton ID="CmdSearch" runat="server" Style="margin-left: 0px" Width="16px"
                                    ImageUrl="~/images/search_lense.png" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: right;" class="dvtCellInfo">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: center;">
                            <asp:Label runat="server" ID="lblNotFound" Text="Data not found." ForeColor="Red"
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: right;" class="dvtCellInfo">
                            <saifi:mydg id="dgvSkillList" runat="server" allowpaging="true" autogeneratecolumns="False"
                                    cssclass="Grid_inq" imagefirst="/imgs/nav_left.gif" imagelast="/imgs/nav_right.gif"
                                    imagenext="/imgs/bulletr.gif" imageprevious="/imgs/bulletl.gif" showfirstandlastimage="False"
                                    showpreviousandnextimage="False" width="900px"  >
                                <AlternatingItemStyle CssClass="Grid_inqAltItem" />
                                <ItemStyle CssClass="Grid_inqItem" />
                                <PagerStyle  Mode="NumericPages" CssClass="Grid_inqPager"></PagerStyle>
                                <Columns>
                                   
                                   <asp:TemplateColumn HeaderText="Skill" ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-Width="200px">
                                        <ItemTemplate>
                                         <asp:Label ID="lbl_id" runat="server" Style="text-align: left; width: 100%;" Visible="false"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                            <asp:Label ID="lbl_skill" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "skill")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                    </asp:TemplateColumn>
                                   <asp:TemplateColumn HeaderText="Active" ItemStyle-HorizontalAlign="Center" 
                                        ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkActie" runat="server" Enabled="false" 
                                                
                                                Checked='<%#DataBinder.Eval(Container.DataItem, "active_status") = "1" %>' />
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit_images.jpg"
                                                CommandName="Edit" ToolTip="Edit" />
                                        
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle CssClass="Grid_inqHeader" />
                            </saifi:mydg>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td colspan='2'>&nbsp;</td></tr>
        <tr><td colspan='2'>&nbsp;</td></tr>
</table>
</asp:Content>

