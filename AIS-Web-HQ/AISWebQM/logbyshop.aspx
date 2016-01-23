<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="logbyshop.aspx.vb" Inherits="logbyshop" Title="View Agent" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Src="txtDate.ascx" TagName="txtDate" TagPrefix="uc1" %>
<asp:Content ID="mainbody" ContentPlaceHolderID="Body" runat="server">

    <asp:HiddenField ID="HiddenField_ShopWebQM_IP" runat="server" />
    <asp:HiddenField ID="HiddenField_DcWebQM_IP" runat="server" />
    <asp:HiddenField ID="HiddenField_User_IP" runat="server" />
    <uc:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true"
        ID="ScriptManager1" ScriptMode="Debug" CombineScripts="false" />
    <table border="0" width="100%">
        <tr>
            <td width="30" valign="top">
                <div runat="server" id="cal" visible="false">
                    <table cellspacing="0" cellpadding="0" width="20%" border="0">
                        <tbody>
                            <tr>
                                <td align="left" bgcolor="#EFF5C5">
                                    &nbsp;
                                </td>
                                <td align="right" bgcolor="#EFF5C5">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </td>
            <td valign="top" align="center">
                <asp:Panel ID="pnlSearch" DefaultButton="btnSearch" runat="server">
                    <table border="0" class="formDialog" width="518">
                        <tr>
                            <td nowrap="nowrap" valign="top" align="center" colspan="6" class="formDialogOrange">
                                <asp:Label ID="lblShopName" runat="server" Text="Shop" ForeColor="#009900" Font-Bold="true"></asp:Label><br />
                                <asp:Label ID="lblDate" runat="server" Text="Date" Font-Bold="True" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" valign="top" align="left" colspan="6">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" valign="top" align="right">
                                Start Date:
                            </td>
                            <td valign="top" nowrap="nowrap" align="left" colspan="3" width="150">
                                <uc1:txtDate ID="txtDateF" runat="server" DefaultCurrentDate="true" />
                            </td>
                            <td nowrap="nowrap" valign="top" align="right">
                                End Date:
                            </td>
                            <td valign="top" nowrap="nowrap">
                                <uc1:txtDate ID="txtDateT" runat="server" DefaultCurrentDate="true" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                Agent:
                            </td>
                            <td align="left" colspan="3">
                                <asp:DropDownList ID="ddlAgent" runat="server" TabIndex="9">
                                </asp:DropDownList>
                            </td>
                            <td nowrap="nowrap" align="right">
                                Queue No.:
                            </td>
                            <td>
                                <asp:TextBox ID="tbxQ" runat="server"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                Service:
                            </td>
                            <td align="left" colspan="3">
                                <asp:DropDownList ID="ddlService" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td nowrap="nowrap" align="right">
                                Mobile:
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="tbxMobile" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                Time - From:
                            </td>
                            <td align="left" width="80">
                                <asp:DropDownList ID="ddlFrom" runat="server" Width="70px">
                                    <asp:ListItem Value="0">-- All --</asp:ListItem>
                                    <asp:ListItem Value="09:00">09:00</asp:ListItem>
                                    <asp:ListItem Value="10:00">10:00</asp:ListItem>
                                    <asp:ListItem Value="11:00">11:00</asp:ListItem>
                                    <asp:ListItem Value="12:00">12:00</asp:ListItem>
                                    <asp:ListItem Value="13:00">13:00</asp:ListItem>
                                    <asp:ListItem Value="14:00">14:00</asp:ListItem>
                                    <asp:ListItem Value="15:00">15:00</asp:ListItem>
                                    <asp:ListItem Value="16:00">16:00</asp:ListItem>
                                    <asp:ListItem Value="17:00">17:00</asp:ListItem>
                                    <asp:ListItem Value="18:00">18:00</asp:ListItem>
                                    <asp:ListItem Value="19:00">19:00</asp:ListItem>
                                    <asp:ListItem Value="20:00">20:00</asp:ListItem>
                                    <asp:ListItem Value="21:00">21:00</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="20">
                                To:
                            </td>
                            <td align="left" class="style5">
                                <asp:DropDownList ID="ddlTo" runat="server" Width="70px">
                                    <asp:ListItem Value="0">-- All --</asp:ListItem>
                                    <asp:ListItem Value="09:00">09:00</asp:ListItem>
                                    <asp:ListItem Value="10:00">10:00</asp:ListItem>
                                    <asp:ListItem Value="11:00">11:00</asp:ListItem>
                                    <asp:ListItem Value="12:00">12:00</asp:ListItem>
                                    <asp:ListItem Value="13:00">13:00</asp:ListItem>
                                    <asp:ListItem Value="14:00">14:00</asp:ListItem>
                                    <asp:ListItem Value="15:00">15:00</asp:ListItem>
                                    <asp:ListItem Value="16:00">16:00</asp:ListItem>
                                    <asp:ListItem Value="17:00">17:00</asp:ListItem>
                                    <asp:ListItem Value="18:00">18:00</asp:ListItem>
                                    <asp:ListItem Value="19:00">19:00</asp:ListItem>
                                    <asp:ListItem Value="20:00">20:00</asp:ListItem>
                                    <asp:ListItem Value="21:00">21:00</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td nowrap="nowrap" align="right">
                                Counter:
                            </td>
                            <td nowrap="nowrap" align="left">
                                <asp:DropDownList ID="ddlCounterName" runat="server" Width="150">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="center" colspan="6" class="style3">
                                <asp:DropDownList ID="ddlPage" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="center" colspan="6">
                                <asp:Button ID="btnSearch" runat="server" BackColor="#8DB667" Font-Bold="True" ForeColor="White"
                                    Text="Search" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:GridView ID="gvAgent" runat="server" AutoGenerateColumns="False" CssClass="shadow1 content"
        HorizontalAlign="Center" AllowPaging="True" Width="90%" AllowSorting="True">
        <EmptyDataRowStyle CssClass="formDialogOrange" HorizontalAlign="Center" />
        <EmptyDataTemplate>
            No Data Found.
        </EmptyDataTemplate>
        <PagerSettings Mode="NumericFirstLast" />
        <Columns>
            <asp:BoundField DataField="no" HeaderText="No" ItemStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                <HeaderStyle ForeColor="#8DB667" CssClass="HeaderBorderLeft" />
            </asp:BoundField>
            <asp:BoundField DataField="agent" SortExpression="agent" HeaderText="Agent" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
            </asp:BoundField>
                 <asp:BoundField DataField="counter_name" SortExpression="counter_name" HeaderText="Counter">
            </asp:BoundField>
            <asp:BoundField DataField="mobile_no" HeaderText="Mobile" SortExpression="mobile_no" />
            <asp:BoundField DataField="service_date" HeaderText="Date" SortExpression="service_date"
                DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
            <asp:BoundField DataField="time" SortExpression="time" HeaderText="Time" />
            <asp:BoundField DataField="item_name" SortExpression="item_name" HeaderText="Service Type" />
            <asp:HyperLinkField DataNavigateUrlFields="id,file_date,id" Visible="false" DataNavigateUrlFormatString="vdo.aspx?qid={0}&amp;fdate={1}&amp;rnd={2}"
                DataTextField="queue_no" HeaderText="Queue No." SortExpression="queue_no" />
            <asp:TemplateField HeaderText="Queue No." SortExpression="queue_no">
                <ItemTemplate>
                    <asp:HyperLink ID="hplShowVideo" runat="server" Text='<%# bind("queue_no") %>' Style="cursor: pointer;"
                        ForeColor="#339933"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="VDO File" SortExpression="qm_status_name">
                <ItemTemplate>
                    <asp:Label ID="lblIsFile" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
       
        </Columns>
        <PagerStyle CssClass="formDialogOrange" HorizontalAlign="center" />
        <HeaderStyle CssClass="formDialog" />
        <AlternatingRowStyle BackColor="#DDDDDD" />
    </asp:GridView>
    <asp:TextBox runat="server" ID="txtSortDir" autocomplete="off" Visible="False" />
    <asp:TextBox runat="server" ID="txtSortField" autocomplete="off" Visible="False" />
    <br />
    <asp:GridView ID="gvAgentNew" runat="server" AutoGenerateColumns="False" Width="80%"
        AllowPaging="True" GridLines="Both">
        <PagerSettings Mode="NumericFirstLast" />
        <EmptyDataRowStyle CssClass="formDialogOrange" HorizontalAlign="Center" />
        <EmptyDataTemplate>
            No Data Found.
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="No" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" CssClass="formDialog"></ItemStyle>
            </asp:TemplateField>
            <asp:BoundField DataField="shop_name" HeaderText="Shop" ItemStyle-HorizontalAlign="Left">
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="queue_no" HeaderText="Queue No." ItemStyle-HorizontalAlign="center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="customer_id" HeaderText="Mobile No." ItemStyle-HorizontalAlign="center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="agent" HeaderText="Agent" ItemStyle-HorizontalAlign="Left">
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="item_code" HeaderText="Service Type" ItemStyle-HorizontalAlign="center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="service_date" HeaderText="Service Date" ItemStyle-HorizontalAlign="center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="start_time" HeaderText="Start Time" ItemStyle-HorizontalAlign="center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="end_time" HeaderText="End Time" ItemStyle-HorizontalAlign="center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="shop_id,tb_counter_queue_id,fdate" DataNavigateUrlFormatString="./vdo.aspx?shopid={0}&amp;qid={1}&amp;fdate={2}"
                HeaderText="Video" Text="View" ItemStyle-HorizontalAlign="center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:HyperLinkField>
        </Columns>
        <PagerStyle CssClass="formDialogOrange" HorizontalAlign="center" />
        <HeaderStyle CssClass="formDialog" />
        <AlternatingRowStyle BackColor="#DDDDDD" />
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style3
        {
            height: 24px;
        }
        .style5
        {
            width: 29px;
        }
    </style>
</asp:Content>
