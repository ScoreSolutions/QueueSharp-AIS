<%@ Page Title="" Language="VB" MasterPageFile="~/Template/ContentMasterPage.master" AutoEventWireup="false" CodeFile="frmWaitingTimeHandlingTimeBySkill.aspx.vb" Inherits="ReportCriteria_frmWaitingTimeHandlingTimeBySkill" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../FormControls/ctlByTime.ascx" tagname="ctlByTime" tagprefix="uc1" %>
<%@ Register src="../FormControls/ctlByWeek.ascx" tagname="ctlByWeek" tagprefix="uc2" %>
<%@ Register src="../FormControls/ctlByDate.ascx" tagname="ctlByDate" tagprefix="uc3" %>
<%@ Register src="../FormControls/ctlByMonth.ascx" tagname="ctlByMonth" tagprefix="uc5" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table align="center" border="0" cellpadding="0" cellspacing="0" width="80%">
    <tr align=center>
        <td>
            <table width="100%">
                <tr>
                    <th width="50%" height="23" id="Th1" style="color: #92b733"  align="left" >
                        Waiting Time and Handling Time by Skill Report</th>
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
                    <td valign="top" align="right">Shop :&nbsp;</td>
                    <td colspan="3" align="left" >
                        <asp:CheckBox ID="chkAllSkill" runat="server" AutoPostBack="true" Text="All Shop" />
                        <asp:CheckBoxList ID="chkSkillId" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Table">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr style="height:35px">
                    <td  align="right">Report By :&nbsp;</td>
                    <td colspan="3" align="left">
                        <asp:RadioButtonList ID="rdiReportBy" runat="server" RepeatDirection="Horizontal" RepeatColumns="6" RepeatLayout="Table" AutoPostBack="true" >
                            <asp:ListItem Selected="True" Text="Time&nbsp;&nbsp;" Value="Time"></asp:ListItem>
                            <asp:ListItem Text="Date&nbsp;&nbsp;" Value="Date"></asp:ListItem>
                            <asp:ListItem Text="Week&nbsp;&nbsp;" Value="Week"></asp:ListItem>
                            <asp:ListItem Text="Month&nbsp;&nbsp;" Value="Month">&nbsp;&nbsp;</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <!-- For Criteria User Control -->
                <uc1:ctlByTime ID="ctlByTime1" runat="server" />
                <uc2:ctlByWeek ID="ctlByWeek1" runat="server" Visible="false" />
                <uc3:ctlByDate ID="ctlByDate1" runat="server" Visible="false" />
                <uc5:ctlByMonth ID="ctlByMonth1" runat="server" Visible="false" />
                
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

