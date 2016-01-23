﻿<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlByDay.ascx.vb" Inherits="FormControls_ctlByDay" %>
<%@ Register src="../UserControls/txtDate.ascx" tagname="txtDate" tagprefix="uc1" %>
<%@ Register src="../UserControls/cmbComboBox.ascx" tagname="cmbComboBox" tagprefix="uc2" %>
<%@ Register src="../UserControls/txtAutoComplete.ascx" tagname="txtAutoComplete" tagprefix="uc3" %>
<%@ Register Src="~/UserControls/rdiDay.ascx" tagname="rdiDay" tagprefix="uc4" %>

    <tr style="height: 30px">
    <td align="right"  >Day :&nbsp;</td>
        <td align="left">
            <uc4:rdiDay ID="rdiDay" runat="server" IsDefaultValue="false"  />
        </td>
        </tr>
        <tr style="height: 30px"><td align="right" width="20%">Week :&nbsp;</td>
        <td  align="left" width="30%">
            <uc2:cmbComboBox ID="cmbWeekFrom" runat="server" IsDefaultValue="false"  />
        </td>
        <td align="center" width="5%">To&nbsp;</td>
        <td  align="left" width="45%">
            <uc2:cmbComboBox ID="cmbWeekTo" runat="server"  IsDefaultValue="false"  />
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