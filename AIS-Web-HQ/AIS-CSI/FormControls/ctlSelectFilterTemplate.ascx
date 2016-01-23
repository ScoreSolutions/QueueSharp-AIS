<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlSelectFilterTemplate.ascx.vb" Inherits="FormControls_ctlSelectFilterTemplate" %>
<%@ Register src="../UserControls/txtAutoComplete.ascx" tagname="txtAutoComplete" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Button ID="btnSearch" runat="server" Text="Select Template" Width="150px" CssClass="formDialog"  />


    <asp:GridView ID="gvSelectFilter" runat="server" AutoGenerateColumns="False" 
        AllowSorting="true" CssClass="GridViewStyle" Width="100%" >
        <RowStyle CssClass="RowStyle" />
        <Columns> 
            <asp:TemplateField ShowHeader="false" >
                <ItemTemplate>
                    <asp:ImageButton ID="imgDel" OnClick="imgDelete_Click" ImageUrl="~/images/ico_delete.jpg"  runat="server" OnClientClick="return confirm('Are you sure?');" CommandArgument="<%# Bind('id') %>" />
                </ItemTemplate>
                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
            <asp:BoundField DataField="filter_name" HeaderText="Filter Name" >
                <HeaderStyle Width="150px" HorizontalAlign="Center"  />
                <ItemStyle Width="150px" HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="id" >
                <ControlStyle CssClass="zHidden" />
                <FooterStyle CssClass="zHidden" />
                <HeaderStyle CssClass="zHidden" />
                <ItemStyle CssClass="zHidden" />
            </asp:BoundField>
        </Columns>
        <PagerStyle CssClass="PagerStyle" />
        <PagerSettings Visible="false" />
        <HeaderStyle CssClass="HeaderStyle" />
        <AlternatingRowStyle CssClass="AltRowStyle" />
    </asp:GridView>


<div style="overflow:scroll;overflow-x:hidden; height:80px;width:80%;border-width:1px;border-style: inset;box-shadow:inset 1px 1px 5px gray;behavior: url('../Template/PIE.htc');">
</div>





<asp:Panel ID="Panel1" runat="server" Width="700px" >
    <table id="Table1" width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff"
        style="border: solid 0px 0px 0px 0px #ff0000" runat="server">
        <tr class="popHead">
            <td align="left" colspan="2" >
                <asp:Label ID="lblHeader" runat="server" Text="Filter Template List " ></asp:Label>
            </td>
            <td align="right" >
                <asp:ImageButton ID="imgClose" runat="server" ImageUrl="../Images/closewindows.png" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table border="0" cellpadding="0" cellspacing="0" width="90%" align="center" >
                    <tr><td colspan="2">&nbsp</td></tr>
                    <tr>
                        <td width="30%" align="right" >Filter Name :&nbsp;</td>
                        <td width="70%" align="left" >
                            <uc1:txtAutoComplete ID="txtSearchFilterName" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td align="left">
                            <asp:Button ID="btnPopSearch" runat="server" CssClass="formDialog" Width="100px" Text="Search" />
                        </td>
                    </tr>
                    <tr><td colspan="2">&nbsp</td></tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" Visible="false" ForeColor="Red" ></asp:Label>
                            <div style="overflow:scroll;overflow-x:hidden;height:400px"> 
                                <asp:GridView ID="gvSearchTemplateList"  runat="server" AutoGenerateColumns="False" 
                                 CssClass="GridViewStyle"  Width="100%"  >
                                    <RowStyle CssClass="RowStyle" />
                                    <Columns> 
                                        <asp:TemplateField ShowHeader="false" >
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkH" runat="server" OnCheckedChanged="chkSearchFilterList_OnCheckedChanged" AutoPostBack="true" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="ลำดับ" DataField="no" >
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="filter_name" HeaderText="Filter Name" >
                                            <HeaderStyle Width="150px" HorizontalAlign="Center"  />
                                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="template_code" HeaderText="Template Code" >
                                            <HeaderStyle Width="100px" HorizontalAlign="Center"  />
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="target" HeaderText="Target" >
                                            <HeaderStyle Width="50px" HorizontalAlign="Center"  />
                                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="period_date" HeaderText="Period Date" >
                                            <HeaderStyle Width="50px" HorizontalAlign="Center"  />
                                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id" >
                                            <ControlStyle CssClass="zHidden" />
                                            <FooterStyle CssClass="zHidden" />
                                            <HeaderStyle CssClass="zHidden" />
                                            <ItemStyle CssClass="zHidden" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                </asp:GridView>
                                <asp:TextBox ID="txtSortField" runat="server" Visible="False" Width="15px"></asp:TextBox>
                                <asp:TextBox ID="txtSortDir" runat="server" Visible="False" Width="15px"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr>
                        <td colspan="2" align="center" >
                            <asp:Button ID="btnSelect" runat="server" CssClass="formDialog" Width="100px" Text="Select" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnClose" runat="server" CssClass="formDialog" Width="80px" Text="Close" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;
                            <asp:Label ID="lblSearchDateFrom" runat="server" CssClass="zHidden" ></asp:Label>
                            <asp:Label ID="lblSearchDateTo" runat="server" CssClass="zHidden"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Button ID="Button1" runat="server" Text="Button" Width="0px" CssClass="zHidden" />
<cc1:ModalPopupExtender ID="zPop" runat="server" PopupControlID="Panel1" TargetControlID="Button1"
    BackgroundCssClass="modalBackground" DropShadow="true">
</cc1:ModalPopupExtender>