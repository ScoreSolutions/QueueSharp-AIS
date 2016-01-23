<%@ Page Title="" Language="VB" MasterPageFile="~/Template/ContentMasterPage.master" AutoEventWireup="false" CodeFile="frmSetFilterToShop.aspx.vb" Inherits="CSIWebApp_frmSetFilterToShop" %>

<%@ Register src="../UserControls/cmbComboBox.ascx" tagname="cmbComboBox" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td class="CssHead" align="left" width="100%" >Set Filter Template to shop</td>
        </tr>
        <tr>
            <td align="center" ><img src="../images/PageHeaderLine.png" alt="" width="100%" /></td>
        </tr>
        <tr><td align="left" >&nbsp;</td></tr>
        <tr style="height:30px" >
            <td align="left" class="CssSubHead"  >
                &nbsp;&nbsp;&nbsp;-กรุณาเลือก Filter Template และ Shop ที่ต้องการใช้งาน 
            </td>
        </tr>
        <tr><td align="left" >&nbsp;</td></tr>
        <tr  >
            <td align="left"  >
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                    <tr>
                        <td width="20%" class="Csslbl" align="right" >Filter Template&nbsp;&nbsp;</td>
                        <td >
                            <uc1:cmbComboBox ID="cmbFilterName" runat="server" Width="450px" IsNotNull="true" DefaultDisplay="เลือก" AutoPosBack="true" />
                        </td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr>
                        <td align="center" colspan="2" >
                            <asp:GridView ID="gvShopList" runat="server" AutoGenerateColumns="False" 
                                AllowSorting="true" CssClass="GridViewStyle" Width="70%" >
                                <RowStyle CssClass="RowStyle" />
                                <Columns> 
                                    <asp:TemplateField ShowHeader="false" >
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkH" runat="server" OnCheckedChanged="chkH_OnCheckedChanged" AutoPostBack="true" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id" >
                                        <ControlStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="shop_name" HeaderText="Shop Name" >
                                        <HeaderStyle HorizontalAlign="Center"  />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                                <PagerStyle CssClass="PagerStyle" />
                                <PagerSettings Visible="false" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr>
                        <td colspan="2" align="center" >
                            <asp:Button ID="btnSave" runat="server" CssClass="formDialog" Width="80px" Text="Save" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="formDialog" Width="80px" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td align="left" >&nbsp;</td></tr>
    </table>
</asp:Content>

