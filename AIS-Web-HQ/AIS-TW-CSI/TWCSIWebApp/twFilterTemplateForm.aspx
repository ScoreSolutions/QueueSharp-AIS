<%@ Page Title="" Language="VB" MasterPageFile="~/Template/ContentMasterPage.master"
    AutoEventWireup="false" CodeFile="twFilterTemplateForm.aspx.vb" Inherits="TWCSIWebApp_twFilterTemplateForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/txtAutoComplete.ascx" TagName="txtAutoComplete"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/cmbComboBox.ascx" TagName="cmbComboBox" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/txtDate.ascx" TagName="txtDate" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/txtTime.ascx" TagName="txtTime" TagPrefix="uc4" %>
<%--<%@ Register Src="../UserControls/ctlShopSelected.ascx" TagName="Location" TagPrefix="l" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function showModal(page, width, height, scroll) {
            showModalDialog(page, "", "dialogWidth:" + width + "px; dialogHeight:" + height + "px;help:no;status:no;center:yes;scroll:" + scroll);
        }      
    </script>

    <style type="text/css">
        .style3
        {
            width: 100%;
        }
        .style4
        {
            width: 155px;
        }
    </style>

    <script type="text/javascript" language="javascript">

        function isNumberKey(evt, txt) {
            var charCode = (evt.charCode) ? evt.which : event.keyCode


            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;
            else {
                var input = document.getElementById(txt).value;
                var len = document.getElementById(txt).value.length;
                var index = document.getElementById(txt).value.indexOf('.');

                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0 || index == 0) {
                    var CharAfterdot = (len + 1) - index;
                    if (CharAfterdot > 3) {

                        return false;
                    }
                }
                if (charCode == 46 && input.split('.').length > 1) {
                    return false;
                }
            }
            return true;
        } 
    </script>

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
                            <uc1:txtAutoComplete ID="txtFilterName" runat="server" Width="450px" />
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
                <%--<l:Location ID="cLocation" runat="server"  />--%>
                <table border="0" cellpadding="0" cellspacing="0" width="500px">
                    <tr>
                        <td width="15%" class="Csslbl" align="right">
                        </td>
                        <td width="85%" align="right">
                            <asp:Button ID="btnAddLocation" CssClass="formDialog" runat="server" Width="109px"
                                Text="Add Location" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlRight" runat="server" Height="400px" ScrollBars="Auto" Width="730px"
                                BorderWidth="1">
                                <asp:GridView ID="GvLocation" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                    Width="100%">
                                    <RowStyle CssClass="RowStyle" />
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgDelLocation" OnClick="imgDeleteLocation_Click" ImageUrl="~/images/ico_delete.jpg"
                                                    runat="server" OnClientClick="return confirm('Are you sure?');" CommandArgument="<%# Bind('id') %>" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="30" />
                                            <ItemStyle HorizontalAlign="Center" Width="30" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="no" HeaderText="No">
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Location_code" HeaderText="Location">
                                            <HeaderStyle Width="50" HorizontalAlign="Center" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="province_code" HeaderText="Province">
                                            <HeaderStyle Width="50" HorizontalAlign="Center" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="region_code" HeaderText="Region">
                                            <HeaderStyle Width="50" HorizontalAlign="Center" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Location_name_en" HeaderText="Location Name">
                                            <HeaderStyle Width="150" HorizontalAlign="Center" />
                                            <ItemStyle Width="150" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Location_type" HeaderText="Location Type">
                                            <HeaderStyle Width="50" HorizontalAlign="Center" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Location_segment" HeaderText="Segment">
                                            <HeaderStyle Width="150" HorizontalAlign="Center" />
                                            <ItemStyle Width="150" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle CssClass="PagerStyle" />
                                    <PagerSettings Visible="false" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                </asp:GridView>
                                <asp:Label ID="lblerror" runat="server" Text="Data not found" ForeColor="Red" Visible="false"
                                    Font-Bold="true"></asp:Label>
                            </asp:Panel>
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
                            <uc2:cmbComboBox ID="cmbNetworkType" runat="server" Width="450px" IsDefaultValue="false"
                                IsNotNull="false" />
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
                            Order Type&nbsp;&nbsp;
                        </td>
                        <td colspan="3">
                            <table class="style3">
                                <tr>
                                    <td align="right" class="style4">
                                        <asp:Label ID="lblPayment" runat="server" Text="Payment"></asp:Label>
                                    </td>
                                    <td style="padding-left: 20px">
                                        <uc1:txtAutoComplete ID="txtPaymentPer" runat="server" Width="68px" TextKey="TextInt">
                                        </uc1:txtAutoComplete>
                                        <asp:Label ID="Label2" runat="server" Text="%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="style4">
                                        <asp:CheckBox ID="chkAllSFF" runat="server" AutoPostBack="True" Checked="True" Text="All SFF Order Type" />
                                    </td>
                                    <td style="padding-left: 20px">
                                        <uc1:txtAutoComplete ID="txtAllSFFPer" runat="server" Width="68px" TextKey="TextInt">
                                        </uc1:txtAutoComplete>
                                        <asp:Label ID="Label3" runat="server" Text="%"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="Csslbl" align="right" valign="top">
                            &nbsp;&nbsp;
                        </td>
                        <td colspan="3">
                            <asp:GridView ID="gvService" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                CssClass="GridViewStyle" Width="70%" ShowFooter="true">
                                <RowStyle CssClass="RowStyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Order Type">
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
                                </Columns>
                                <PagerStyle CssClass="PagerStyle" />
                                <PagerSettings Visible="false" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <FooterStyle CssClass="HeaderStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td class="Csslbl" align="right">
                            Complete Date<font color="red">*</font>&nbsp;&nbsp;
                        </td>
                        <td>
                            <uc3:txtDate ID="txtPeriodDateFrom" runat="server" />
                            <span class="Csslbl">To<font color="red">*</font></span>
                            <uc3:txtDate ID="txtPeriodDateTo" runat="server" />
                        </td>
                        <td class="Csslbl" align="right">
                            Complete Time<font color="red">*</font>&nbsp;&nbsp;
                        </td>
                        <td>
                            <uc2:cmbComboBox ID="cmbTimeFrom" runat="server" Width="60px" IsNotNull="false" IsDefaultValue="false" />
                            <span class="Csslbl">To<font color="red">*</font></span>
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
                            Template Code<font color="red">*</font>&nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <uc1:txtAutoComplete ID="txtTemplateCode" runat="server" Width="100px" TextAlign="AlignCenter"
                                TextKey="TextInt" />
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

                        var paymentPer;
                        paymentPer = document.getElementById("<%= txtPaymentPer.ClientID %>").value;
                        if (document.getElementById("<%= txtPaymentPer.ClientID %>").value == '') {
                            paymentPer = 0;
                        }

                        var sffPer;
                        sffPer = document.getElementById("<%= txtAllSFFPer.ClientID %>").value;
                        if (document.getElementById("<%= txtAllSFFPer.ClientID %>").value == '') {
                            sffPer = 0;
                        }

                        tot = parseFloat(tot) + parseFloat(paymentPer) + parseFloat(sffPer);
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
                        <td align="left" class="Csslbl">
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
                        <td align="left" class="Csslbl"  colspan="3">
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
                        <td align="left" class="Csslbl" colspan="3" >
                            <uc2:cmbComboBox ID="CmbCatGenActAllSurveyUPC" runat="server" Width="650px" IsDefaultValue="false"
                                IsNotNull="false" AutoPosBack="true" />
                        </td>
                    </tr>
                    <tr>
                    
                        <td align="right" class="Csslbl">
                            Sub Category&nbsp;&nbsp;
                        </td>
                        <td align="left" class="Csslbl"  colspan="3" >
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
                        <td align="left" class="Csslbl" colspan="3" >
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
