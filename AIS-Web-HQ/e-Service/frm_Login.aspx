<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frm_login.aspx.vb" Inherits="frm_login" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
        .style14
        {
            height: 49px;
        }
        .style15
        {
            height: 377px;
        }
        .style16
        {
            height: 49px;
            width: 613px;
        }
        .style17
        {
            height: 377px;
            width: 613px;
        }
        .style18
        {
            width: 613px;
        }
        .style19
        {
            height: 49px;
            width: 257px;
        }
        .style20
        {
            height: 377px;
            width: 257px;
        }
        .style21
        {
            width: 257px;
        }
        .style3
        {
            height: 90px;
            width: 513px;
        }
        .style11
        {
            height: 249px;
            width: 131px;
        }
        .style13
        {
            width: 513px;
        }
        .style8
        {
            font-family: Tahoma;
            font-size: 15px;
            font-weight: bold;
            height: 30px;
            width: 163px;
        }
        .style9
        {
            height: 30px;
            width: 163px;
        }
        .style7
        {
            width: 234px;
        }
        .style2
        {
            height: 249px;
        }
        .style12
        {
            width: 131px;
        }
        .style23
        {
            height: 90px;
        }
        .style24
        {
            width: 118px;
        }
        .style25
        {
            width: 93px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <%--<div style="text-align: center">
    
        <table style="width:100%;">
            <tr>
                <td class="style19">
                </td>
                <td class="style16">
                        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        </asp:ToolkitScriptManager>
                    </td>
                <td class="style14">
                </td>
            </tr>
            <tr>
                <td class="style20">
                </td>
                <td class="style17">
    
        <asp:Panel ID="Panel1" runat="server" Height="360px" Width="683px" 
                        BackImageUrl="~/image/bg_2.png" style="margin-top: 0px">
            <table style="width:100%; height: 345px;" border="0" cellpadding="0" 
                cellspacing="0">
                <tr>
                    <td  style="padding-left:10px; padding-top:10px;" class="style23">
                        <asp:Panel ID="Panel2" runat="server" BackImageUrl="~/image/AIS-logo.jpg" 
                            Height="70px" Width="169px">
                        </asp:Panel>
                    </td>
                    <td class="style3">
                    </td>
                    <td class="style23">
                    </td>
                </tr>
                <tr>
                    <td class="style11">
                    </td>
                    <td style=" padding-top:20px; vertical-align:top;" >
                        <table style="width:100%;" border="0" cellpadding="0" cellspacing="0" >
                            <tr>
                                <td  class="style8" style="text-align:right; padding-right:10px; color:#669900;  ">
                                    Telephone Number :</td>
                                <td style="text-align: left; padding-left: 10px; color:#76933C;">
                                    <asp:TextBox ID="txt_tel" runat="server" class="detailedViewTextBox" onblur="this.className='detailedViewTextBox'"
                                        onfocus="this.className='detailedViewTextBoxOn'"></asp:TextBox>
                                   
                                    <asp:FilteredTextBoxExtender ID="txt_tel_FilteredTextBoxExtender" 
                                        runat="server" Enabled="True" TargetControlID="txt_tel" FilterType="Numbers">
                                    </asp:FilteredTextBoxExtender>
                                   
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style8" style="text-align:right; padding-right:5px; color:#669900;">
                                    Language :</td>
                                <td style="text-align: left; padding-left: 10px; color:#76933C;">
                                    <asp:DropDownList ID="dtplang" runat="server" class="detailedViewTextBox" onblur="this.className='detailedViewTextBox'"
                                        onfocus="this.className='detailedViewTextBoxOn'">
                                        
                                    </asp:DropDownList >
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style9">
                                    &nbsp;</td>
                                <td class="style7">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="0" cellspacing="0" width="300px"  >
                                <tr>
                                    <td style="text-align:right; padding-right:5px; width:200px;">
                                        <asp:ImageButton ID="img_login" runat="server" ImageUrl="~/image/btn_ok.png" />
                                    </td>
                               
                                    <td style="text-align:right; padding-right:5px;">
                                        <asp:ImageButton ID="img_cancel" runat="server" 
                                            ImageUrl="~/image/btn_close.png" />
                                    </td>
                            </tr>
                        </table>
                    </td>
                    <td class="style2">
                    </td>
                </tr>
                <tr>
                    <td class="style12">
                        &nbsp;</td>
                    <td class="style13">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
    
                </td>
                <td class="style15">
                </td>
            </tr>
            <tr>
                <td class="style21">
                    &nbsp;</td>
                <td class="style18">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>--%>
    </form>
</body>
</html>
