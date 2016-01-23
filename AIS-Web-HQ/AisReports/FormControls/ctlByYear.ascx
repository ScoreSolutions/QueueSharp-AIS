<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlByYear.ascx.vb" Inherits="FormControls_ctlByYear" %>
<%@ Register src="../UserControls/txtAutoComplete.ascx" tagname="txtAutoComplete" tagprefix="uc3" %>

    <tr style="height: 30px">
        <td align="right"  >Year From:&nbsp;</td>
        <td align="left" >
            <uc3:txtAutoComplete ID="txtYearFrom" runat="server" TextAlign="AlignRight" TextKey="TextInt" Width="50px" IsNotNull="true" />
            <font color='red'>ปี ค.ศ.</font> 
        </td>
        <td style="height: 29px" align="center" >Year To:&nbsp;</td>
        <td style="height: 29px" align="left">
            <uc3:txtAutoComplete ID="txtYearTo" runat="server" TextAlign="AlignRight" TextKey="TextInt" Width="50px" IsNotNull="true" />
            <font color='red'>ปี ค.ศ.</font> 
        </td>
    </tr>
