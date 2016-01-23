<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlByMonth.ascx.vb" Inherits="FormControls_ctlByMonth" %>
<%@ Register src="../UserControls/txtDate.ascx" tagname="txtDate" tagprefix="uc1" %>
<%@ Register src="../UserControls/cmbComboBox.ascx" tagname="cmbComboBox" tagprefix="uc2" %>
<%@ Register src="../UserControls/txtAutoComplete.ascx" tagname="txtAutoComplete" tagprefix="uc3" %>

    <tr style="height: 30px">
        <td align="right" width="20%">Month :&nbsp;</td>
        <td  align="left" width="30%">
            <uc2:cmbComboBox ID="cmbMonthFrom" runat="server"  IsDefaultValue="false"  />
        </td>
        <td align="center" width="5%">To&nbsp;</td>
        <td  align="left" width="45%">
            <uc2:cmbComboBox ID="cmbMonthTo" runat="server"  IsDefaultValue="false"  />
        </td>
    </tr>
    <tr style="height: 30px">
        <td align="right"  >Year :&nbsp;</td>
        <td align="left" >
            <uc3:txtAutoComplete ID="txtYearFrom" runat="server" TextAlign="AlignRight" TextKey="TextInt" Width="50px" IsNotNull="true" />
            <font color='red'>ปี ค.ศ.</font> 
        </td>
        <td style="height: 29px" align="center" >To&nbsp;</td>
        <td style="height: 29px" align="left">
            <uc3:txtAutoComplete ID="txtYearTo" runat="server" TextAlign="AlignRight" TextKey="TextInt" Width="50px" IsNotNull="true" />
            <font color='red'>ปี ค.ศ.</font> 
        </td>
    </tr>