<%@ Page Language="VB" MasterPageFile="~/Template/ContentMasterPage.master" AutoEventWireup="false" CodeFile="frmAvgServiceTimeKPIBySkill.aspx.vb" Inherits="ReportCriteria_frmAvgServiceTimeKPIBySkill" %>

<%@ Register src="../FormControls/ctlByDate.ascx" tagname="ctlByDate" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table align="center" border="0" cellpadding="0" cellspacing="0" width="80%">
    <tr >
        <td>
            <table width="100%">
                <tr>
                    <th width="50%" height="23" id="Th1" style="color: #92b733"  align="left" >
                        Average Service Time comparing with KPI Report by Skill</th>
                    <th width="50%" class="title_page_current" id="screenName" >&nbsp;
                        
                    </th>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="section_header" colspan="4">
                        Search
                    </td>
                </tr>
                <tr><td colspan="4">&nbsp;</td></tr>
                <tr>
                    <td valign="top" align="right">Skill :&nbsp;</td>
                    <td colspan="3" align="left" >
                        <asp:CheckBox ID="chkAllShop" runat="server" AutoPostBack="true" Text="All Skill" />
                        <asp:CheckBoxList ID="chkSkillId" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Table">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr style="height:35px">
                    <td  align="right">Report By :&nbsp;</td>
                    <td colspan="3" align="left">
                        <asp:RadioButtonList ID="rdiReportBy" runat="server" RepeatDirection="Horizontal" RepeatColumns="6" RepeatLayout="Table" AutoPostBack="true" >
                            <%--<asp:ListItem Text="Time&nbsp;&nbsp;" Value="Time" Selected="True"></asp:ListItem>--%>
                            <asp:ListItem Text="Date&nbsp;&nbsp;" Value="Date" Selected="True" ></asp:ListItem>
                            <%--<asp:ListItem Text="Day&nbsp;&nbsp;" Value="Day" >&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Text="Week&nbsp;&nbsp;" Value="Week" ></asp:ListItem>
                            <asp:ListItem Text="Month&nbsp;&nbsp;" Value="Month" >&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Text="Year&nbsp;&nbsp;" Value="Year" >&nbsp;&nbsp;</asp:ListItem>--%>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <!-- For Criteria User Control -->
                <uc3:ctlByDate ID="ctlByDate1" runat="server"  />
                <tr style="height: 30px">
                    <td colspan="4" align="center">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                            ShowMessageBox="True" EnableTheming="True" 
                            HeaderText="กรุณากรอกข้อมูลให้ครบ!!" ShowSummary="False" />
                        &nbsp;
                        <asp:Button ID="btnSearch" CssClass="formDialog" runat="server" Text="Show Report" />
                    </td>
                </tr>
             </table>
        </td>
    </tr>
</table>
</asp:Content>