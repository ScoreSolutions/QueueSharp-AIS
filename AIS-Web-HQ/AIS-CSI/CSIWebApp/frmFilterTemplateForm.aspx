<%@ Page Title="" Language="VB" MasterPageFile="~/Template/ContentMasterPage.master"
    AutoEventWireup="false" CodeFile="frmFilterTemplateForm.aspx.vb" Inherits="CSIWebApp_frmFilterTemplateForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/txtAutoComplete.ascx" TagName="txtAutoComplete"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/cmbComboBox.ascx" TagName="cmbComboBox" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/txtDate.ascx" TagName="txtDate" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/txtTime.ascx" TagName="txtTime" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style3
        {
            width: 100%;
        }
        .style5
        {
            width: 302px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="CssHead" align="left" width="100%">
                Filter Template
            </td>
        </tr>
        <tr>
            <td align="center">
                <img src="../images/PageHeaderLine.png" alt="" width="100%" />
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr style="height: 30px">
            <td align="left" class="CssSubHead">
                &nbsp;&nbsp;&nbsp;-Information
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="15%" class="Csslbl" align="right">
                            Filter Name<font color="red">*</font>&nbsp;&nbsp;
                        </td>
                        <td width="85%">
                            <uc1:txtAutoComplete ID="txtFilterName" runat="server" Width="450px" IsNotNull="false" />
                            <asp:TextBox ID="txtID" runat="server" Text="0" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr style="height: 30px">
            <td align="left" class="CssSubHead">
                &nbsp;&nbsp;&nbsp;- Location
            </td>
        </tr>
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="35%">
                            &nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="25%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="Csslbl" colspan="4">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="15%" class="Csslbl" align="right">
                                        Default Template Code<font color="red">*</font>&nbsp;&nbsp;
                                    </td>
                                    <td width="85%">
                                        <uc1:txtAutoComplete ID="txtTemplateCode" runat="server" Width="100px" 
                                TextAlign="AlignCenter" TextKey="TextInt" AutoPosBack="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%" class="Csslbl" align="right">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td width="85%">
                                        <asp:RadioButtonList ID="rdiSurveyAllShop" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="N" Text="By Shop &nbsp;&nbsp;&nbsp;" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="Y" Text="All Shop"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:GridView ID="gvShopList" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                CssClass="GridViewStyle" Width="70%">
                                <RowStyle CssClass="RowStyle" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="false">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkH" runat="server" OnCheckedChanged="chkH_OnCheckedChanged" AutoPostBack="true" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id">
                                        <ControlStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="shop_code" HeaderText="Location Code">
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="shop_name" HeaderText="Location Name">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                     <asp:TemplateField ShowHeader="true" HeaderText="Template Code">
                                        <ItemTemplate>
                                            <uc1:txtAutoComplete ID="txtShTemplateCode" runat="server" Width="50px" TextKey="TextInt"
                                                Text='<%# Bind("sh_template_code") %>' TextAlign="AlignCenter" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        <FooterStyle Width="100px" Height="25px" HorizontalAlign="Center" VerticalAlign="Middle"
                                            CssClass="HeaderStyle" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblShTemplateCode" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="PagerStyle" />
                                <PagerSettings Visible="false" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr style="height: 30px">
            <td align="left" class="CssSubHead">
                &nbsp;&nbsp;&nbsp;-Filter
            </td>
        </tr>
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="15%">
                            &nbsp;
                        </td>
                        <td width="35%">
                            &nbsp;
                        </td>
                        <td width="15%">
                            &nbsp;
                        </td>
                        <td width="35%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td align="right" class="Csslbl">
                            Contact Class<font color="red">*</font>&nbsp;&nbsp;
                        </td>
                        <td colspan="3">
                            <table class="style3">
                                <tr>
                                    <td class="style5">
                                        <uc2:cmbComboBox ID="cmbContactClass" runat="server" Width="450px" IsNotNull="false"
                                            IsDefaultValue="false" />
                                    </td>
                                    <td style="padding-left: 20px">
                                        <asp:CheckBox ID="chkISBlankContactClass" runat="server" Text=" Blank" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td align="right" class="Csslbl">
                            Category<font color="red">*</font>&nbsp;&nbsp;
                        </td>
                        <td colspan="3">
                            <table class="style3">
                                <tr>
                                    <td class="style5">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <uc2:cmbComboBox ID="cmbCategory" runat="server" Width="450px" IsNotNull="false"
                                                    IsDefaultValue="false" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="padding-left: 20px">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:CheckBox ID="chkISBlankCategory" runat="server" Text=" Blank" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td align="right" class="Csslbl">
                            Nationality<font color="red">*</font>&nbsp;&nbsp;
                        </td>
                        <td colspan="3">
                            <table>
                                <tr>
                                    <td>
                                        <asp:CheckBoxList ID="chkNationality" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="THAI">THAILAND</asp:ListItem>
                                            <asp:ListItem Value="ENG">ENGLISH</asp:ListItem>
                                            <asp:ListItem>OTHER</asp:ListItem>
                                            <asp:ListItem>BLANK</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td align="right" class="Csslbl">
                            Network Type<font color="red">*</font>&nbsp;&nbsp;
                        </td>
                        <td colspan="3">
                            <uc2:cmbComboBox ID="cmbNetworkType" runat="server" Width="450px" IsNotNull="false"
                                IsDefaultValue="false" />
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td align="right" class="Csslbl" valign="top">
                            Segment&nbsp;&nbsp;
                        </td>
                        <td colspan="3">
                            <asp:GridView ID="gvSegment" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                CssClass="GridViewStyle" Width="70%" ShowFooter="false">
                                <RowStyle CssClass="RowStyle" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="false">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkH" runat="server" OnCheckedChanged="chkSegment_OnCheckedChanged"
                                                AutoPostBack="true" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="segment_name" HeaderText="Segment">
                                        <HeaderStyle HorizontalAlign="Center" />
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
                    <tr>
                        <td class="Csslbl" align="right" valign="top">
                            Service&nbsp;&nbsp;
                        </td>
                        <td colspan="3">
                            <asp:GridView ID="gvService" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                CssClass="GridViewStyle" Width="70%" ShowFooter="true">
                                <RowStyle CssClass="RowStyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Service">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblItemName" Text='<%# Bind("item_name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle CssClass="HeaderStyle" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id">
                                        <ControlStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <uc1:txtAutoComplete ID="txtTargetPercent" runat="server" Width="25px" TextKey="TextInt"
                                                Text='<%# Bind("target_percent") %>' TextAlign="AlignCenter" />
                                            %
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        <FooterStyle Width="100px" Height="25px" HorizontalAlign="Center" VerticalAlign="Middle"
                                            CssClass="HeaderStyle" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotTargetPer" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkH" runat="server" OnCheckedChanged="chkHService_OnCheckedChanged"
                                                AutoPostBack="true" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" Checked='<%# Eval("chk").toString() = "Y" %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        <FooterStyle CssClass="HeaderStyle" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="PagerStyle" />
                                <PagerSettings Visible="false" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <FooterStyle CssClass="HeaderStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr style="height: 45px;" valign="bottom">
                        <td>
                            &nbsp;
                        </td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnUSearch" runat="server" CssClass="formDialog" Width="80px" Text="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Csslbl" align="right" valign="top">
                            User Name&nbsp;&nbsp;
                        </td>
                        <td valign="top" colspan="3">
                            <div style="overflow: scroll; overflow-x: hidden; height: 120px; width: 80%; border-width: 1px;
                                border-style: inset; box-shadow: inset 1px 1px 5px gray; behavior: url('../Template/PIE.htc');">
                                <asp:GridView ID="gvUserName" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                    CssClass="GridViewStyle" Width="100%">
                                    <RowStyle CssClass="RowStyle" />
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgDel" OnClick="imgDelete_Click" ImageUrl="~/images/ico_delete.jpg"
                                                    runat="server" OnClientClick="return confirm('Are you sure?');" CommandArgument="<%# Bind('UQID') %>" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="shop_name" HeaderText="Location Name">
                                            <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="staff_name" HeaderText="Staff Name">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="shop_id">
                                            <ControlStyle CssClass="zHidden" />
                                            <FooterStyle CssClass="zHidden" />
                                            <HeaderStyle CssClass="zHidden" />
                                            <ItemStyle CssClass="zHidden" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="shop_name">
                                            <ControlStyle CssClass="zHidden" />
                                            <FooterStyle CssClass="zHidden" />
                                            <HeaderStyle CssClass="zHidden" />
                                            <ItemStyle CssClass="zHidden" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="user_id">
                                            <ControlStyle CssClass="zHidden" />
                                            <FooterStyle CssClass="zHidden" />
                                            <HeaderStyle CssClass="zHidden" />
                                            <ItemStyle CssClass="zHidden" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="username">
                                            <ControlStyle CssClass="zHidden" />
                                            <FooterStyle CssClass="zHidden" />
                                            <HeaderStyle CssClass="zHidden" />
                                            <ItemStyle CssClass="zHidden" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fname">
                                            <ControlStyle CssClass="zHidden" />
                                            <FooterStyle CssClass="zHidden" />
                                            <HeaderStyle CssClass="zHidden" />
                                            <ItemStyle CssClass="zHidden" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="lname">
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
                            </div>
                            <asp:Panel ID="Panel1" runat="server" Width="700px">
                                <table id="Table1" width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff"
                                    style="border: solid 0px 0px 0px 0px #ff0000" runat="server">
                                    <tr class="popHead">
                                        <td align="left" colspan="2">
                                            <asp:Label ID="lblHeader" runat="server" Text="Agent List "></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="imgClose" runat="server" ImageUrl="../Images/closewindows.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table border="0" cellpadding="0" cellspacing="0" width="90%" align="center">
                                                <tr>
                                                    <td colspan="2">
                                                        &nbsp
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="30%" align="right">
                                                        Shop :&nbsp;
                                                    </td>
                                                    <td width="70%" align="left">
                                                        <uc2:cmbComboBox ID="cmbSearchShop" runat="server" Width="300px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="30%" align="right">
                                                        Staff Name :&nbsp;
                                                    </td>
                                                    <td width="70%" align="left">
                                                        <uc1:txtAutoComplete ID="txtSearchStaffName" runat="server" Width="300px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="btnSearch" runat="server" CssClass="formDialog" Width="100px" Text="Search" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        &nbsp
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div style="overflow: scroll; overflow-x: hidden; height: 400px">
                                                            <asp:GridView ID="gvSearchStaffList" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                                CssClass="GridViewStyle" Width="100%">
                                                                <RowStyle CssClass="RowStyle" />
                                                                <Columns>
                                                                    <asp:TemplateField ShowHeader="false">
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkH" runat="server" OnCheckedChanged="chkSearchUserList_OnCheckedChanged"
                                                                                AutoPostBack="true" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chk" runat="server" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="30px" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="�ӴѺ" DataField="no">
                                                                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="shop_name" HeaderText="Location Name">
                                                                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="staff_name" HeaderText="Staff Name">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="shop_id">
                                                                        <ControlStyle CssClass="zHidden" />
                                                                        <FooterStyle CssClass="zHidden" />
                                                                        <HeaderStyle CssClass="zHidden" />
                                                                        <ItemStyle CssClass="zHidden" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="shop_name">
                                                                        <ControlStyle CssClass="zHidden" />
                                                                        <FooterStyle CssClass="zHidden" />
                                                                        <HeaderStyle CssClass="zHidden" />
                                                                        <ItemStyle CssClass="zHidden" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="user_id">
                                                                        <ControlStyle CssClass="zHidden" />
                                                                        <FooterStyle CssClass="zHidden" />
                                                                        <HeaderStyle CssClass="zHidden" />
                                                                        <ItemStyle CssClass="zHidden" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="username">
                                                                        <ControlStyle CssClass="zHidden" />
                                                                        <FooterStyle CssClass="zHidden" />
                                                                        <HeaderStyle CssClass="zHidden" />
                                                                        <ItemStyle CssClass="zHidden" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="fname">
                                                                        <ControlStyle CssClass="zHidden" />
                                                                        <FooterStyle CssClass="zHidden" />
                                                                        <HeaderStyle CssClass="zHidden" />
                                                                        <ItemStyle CssClass="zHidden" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="lname">
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
                                                <tr>
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:Button ID="btnSelect" runat="server" CssClass="formDialog" Width="100px" Text="+ Add" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnClose" runat="server" CssClass="formDialog" Width="80px" Text="Close" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        &nbsp;
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
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td class="Csslbl" align="right">
                            Period Date<font color="red">*</font>&nbsp;&nbsp;
                        </td>
                        <td>
                            <uc3:txtDate ID="txtPeriodDateFrom" runat="server" IsNotNull="false" />
                            <span class="Csslbl">To<font color="red">*</font>&nbsp;&nbsp;</span>
                            <uc3:txtDate ID="txtPeriodDateTo" runat="server" IsNotNull="false" />
                        </td>
                        <td class="Csslbl" align="right">
                            Period Time&nbsp;&nbsp;
                        </td>
                        <td>
                            <uc2:cmbComboBox ID="cmbTimeFrom" runat="server" Width="60px" IsNotNull="false" IsDefaultValue="false" />
                            <span class="Csslbl">To<font color="red">*</font>&nbsp;&nbsp;</span>
                            <uc2:cmbComboBox ID="cmbTimeTo" runat="server" Width="60px" IsNotNull="false" IsDefaultValue="false" />
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td class="Csslbl" align="right">
                            &nbsp;
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdiScheduleTypeDay" runat="server" RepeatColumns="2" RepeatDirection="Vertical"
                                RepeatLayout="Flow" AutoPostBack="true">
                                <asp:ListItem Text="Daily&nbsp;&nbsp;&nbsp;" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Fixed Date&nbsp;&nbsp;&nbsp;" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left" colspan="2">
                            <asp:CheckBoxList ID="chkScheduleDay" runat="server" RepeatDirection="Vertical" RepeatColumns="7"
                                RepeatLayout="Flow" Enabled="false">
                                <asp:ListItem Value="MON" Text="Mon&nbsp;&nbsp;"></asp:ListItem>
                                <asp:ListItem Value="TUE" Text="Tue&nbsp;&nbsp;"></asp:ListItem>
                                <asp:ListItem Value="WED" Text="Wed&nbsp;&nbsp;"></asp:ListItem>
                                <asp:ListItem Value="THU" Text="Thu&nbsp;&nbsp;"></asp:ListItem>
                                <asp:ListItem Value="FRI" Text="Fri&nbsp;&nbsp;"></asp:ListItem>
                                <asp:ListItem Value="SAT" Text="Sat&nbsp;&nbsp;"></asp:ListItem>
                                <asp:ListItem Value="SUN" Text="Sun&nbsp;&nbsp;"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="Csslbl">
                            Filter Time<font color="red">*</font>&nbsp;&nbsp;
                        </td>
                        <td align="left" class="Csslbl">
                            <asp:RadioButton ID="rdiAfterEndQueue" runat="server" GroupName="FileterTime" Text="After End Service"
                                Checked="true" AutoPostBack="true" />
                            <uc1:txtAutoComplete ID="txtAfterEndMin" runat="server" Width="40px" IsNotNull="false"
                                TextAlign="AlignCenter" TextKey="TextInt" />
                            Min
                        </td>
                        <td align="left" colspan="2" class="Csslbl">
                            <asp:RadioButton ID="rdiEveryTime" runat="server" GroupName="FileterTime" Text="Every"
                                AutoPostBack="true" />
                            <uc1:txtAutoComplete ID="txtEveryMin" runat="server" Width="40px" IsNotNull="false"
                                Enabled="false" TextAlign="AlignCenter" TextKey="TextInt" />
                            Min
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td class="Csslbl" align="right">
                            Target&nbsp;&nbsp;
                        </td>
                        <td valign="middle" class="Csslbl">
                            <uc1:txtAutoComplete ID="txtTarget" runat="server" Width="40px" TextAlign="AlignRight"
                                TextKey="TextInt" />
                            &nbsp;&nbsp; <span>Or&nbsp;&nbsp;</span>
                            <asp:CheckBox ID="chkUnlimited" runat="server" AutoPostBack="true" />
                            All Customer
                        </td>
                        <td align="right" class="Csslbl">
                           
                        </td>
                        <td align="left">
                           
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">

                <script language="javascript" type="text/javascript">
                    function CalTotTarget(lblTot) {
                        var i;
                        var tot = 0;
                        for (i = 0; i < document.forms[0].elements.length; i++) {
                            if ((document.forms[0].elements[i].type == 'text') &&
                                (document.forms[0].elements[i].name.indexOf('gvService') > -1) &&
                                (document.forms[0].elements[i].name.indexOf('txtTargetPercent') > -1)) {

                                if (document.forms[0].elements[i].value != "") {
                                    tot = parseFloat(tot) + parseFloat(document.forms[0].elements[i].value);
                                }
                            }
                        }
                        document.getElementById(lblTot).innerHTML = tot + " %";
                    }
                </script>

                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="15%">
                            &nbsp;
                        </td>
                        <td width="35%">
                            &nbsp;
                        </td>
                        <td width="15%">
                            &nbsp;
                        </td>
                        <td width="35%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="35%">
                            &nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="25%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr style="height: 30px">
            <td align="left" class="CssSubHead">
                &nbsp;&nbsp;&nbsp;- Auto Generate Siebel Activity
            </td>
        </tr>
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="15%">
                            &nbsp;
                        </td>
                        <td width="30%">
                            &nbsp;
                        </td>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td width="45%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left" class="Csslbl" colspan="3">
                            <asp:CheckBox ID="chkGenActAllSurvey" runat="server" Text=" Generate Siebel Activity for All Survey"
                                AutoPostBack="true" />
                            <uc1:txtAutoComplete ID="txtOwnerGenActAllSurvey" runat="server" Text="QISSYS" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            BKK
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="Csslbl">
                            Category&nbsp;&nbsp;
                        </td>
                        <td align="left" class="Csslbl" colspan="3">
                            <uc2:cmbComboBox ID="CmbCatGenActAllSurvey" runat="server" Width="650px" IsDefaultValue="false"
                                IsNotNull="false" AutoPosBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="Csslbl">
                            Sub Category&nbsp;&nbsp;
                        </td>
                        <td align="left" class="Csslbl" colspan="3" >
                            <uc2:cmbComboBox ID="CmbSubCatGenActAllSurvey" runat="server" Width="650px" IsDefaultValue="false"
                                IsNotNull="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            UPC
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="Csslbl">
                            Category&nbsp;&nbsp;
                        </td>
                        <td align="left" class="Csslbl" colspan="3">
                            <uc2:cmbComboBox ID="CmbCatGenActAllSurveyUPC" runat="server" Width="650px" IsDefaultValue="false"
                                IsNotNull="false" AutoPosBack="true" />
                        </td>
                    </tr>
                    <tr>
                        
                        <td align="right" class="Csslbl">
                            Sub Category&nbsp;&nbsp;
                        </td>
                        <td align="left" class="Csslbl" colspan="3">
                            <uc2:cmbComboBox ID="CmbSubCatGenActAllSurveyUPC" runat="server" Width="650px" IsDefaultValue="false"
                                IsNotNull="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left" class="Csslbl">
                            <asp:CheckBox ID="chkGenActResult3" runat="server" Text=' Generate Siebel Activity for Result "Poor" and "Leave Voice"'
                                AutoPostBack="true" />
                            <uc1:txtAutoComplete ID="txtOwnerGenActResult3" runat="server" Text="QAI" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            BKK
                        </td>
                        
                    </tr>
                    <tr>
                        <td align="right" class="Csslbl">
                            Category&nbsp;&nbsp;
                        </td>
                        <td align="left" class="Csslbl" colspan="3">
                            <uc2:cmbComboBox ID="CmbCatGenActResult3" runat="server" Width="650px" IsDefaultValue="false"
                                IsNotNull="false" AutoPosBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="Csslbl">
                            Sub Category&nbsp;&nbsp;
                        </td>
                        <td align="left" class="Csslbl" colspan="3">
                            <uc2:cmbComboBox ID="CmbSubCatGenActResult3" runat="server" Width="650px" IsDefaultValue="false"
                                IsNotNull="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            UPC
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="Csslbl">
                            Category&nbsp;&nbsp;
                        </td>
                        <td align="left" class="Csslbl" colspan="3">
                            <uc2:cmbComboBox ID="CmbCatGenActResult3UPC" runat="server" Width="650px" IsDefaultValue="false"
                                IsNotNull="false" AutoPosBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="Csslbl">
                            Sub Category&nbsp;&nbsp;
                        </td>
                        <td align="left" class="Csslbl" colspan="3">
                            <uc2:cmbComboBox ID="CmbSubCatGenActResult3UPC" runat="server" Width="650px" IsDefaultValue="false"
                                IsNotNull="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr style="height: 30px">
            <td align="left" class="CssSubHead">
                &nbsp;&nbsp;&nbsp;- Filter Status
            </td>
        </tr>
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="35%">
                            &nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="25%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:RadioButton ID="rdiStatusActive" runat="server" GroupName="FileterStatus" Text="Active"
                                Checked="true" CssClass="Csslbl" />
                        </td>
                        <td align="left" colspan="2">
                            <asp:RadioButton ID="rdiStatusHold" runat="server" GroupName="FileterStatus" Text="Inactive"
                                ForeColor="Red" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" CssClass="formDialog" Width="80px" Text="Save" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" CssClass="formDialog" Width="80px" Text="Cancel" />
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
