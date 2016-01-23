<%@ Page Title="" Language="VB" MasterPageFile="~/Template/ContentMasterPage.master" AutoEventWireup="false" CodeFile="twVerifyData.aspx.vb" Inherits="TWCSIWebApp_twVerifyData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../UserControls/txtAutoComplete.ascx" tagname="txtAutoComplete" tagprefix="uc1" %>
<%@ Register src="../UserControls/txtDate.ascx" tagname="txtDate" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/jscript">
        function OpenModalWindow(FilterID, ShopID, ServiceID, vDate) {
            var WinSettings = "center:yes;resizable:no;dialogHeight:500px";
            var MyArgs = window.showModalDialog("../TWCSIWebApp/twPopAddDataForSend.aspx?id=" + FilterID + '&ShopID='+ShopID + '&ServiceID=' + ServiceID + '&vDate=' + vDate, null, WinSettings);
        }

        function OpenResultWindow(FilterID, ShopID, ServiceID, vDate) {
            //alert("aaaa");
            var WinSettings = "center:yes;resizable:no;dialogHeight:450px;dialogWidth:750px;";
            var MyArgs = window.showModalDialog("../TWCSIWebApp/twPopSendResult.aspx?id=" + FilterID + '&ShopID=' + ShopID + '&ServiceID=' + ServiceID + '&vDate=' + vDate, null, WinSettings);

            if (MyArgs == true)
                location.reload(true);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td class="CssHead" align="left" width="100%" >Verify Data</td>
        </tr>
        <tr>
            <td align="center" ><img src="../images/PageHeaderLine.png" alt="" width="100%" /></td>
        </tr>
        <tr><td align="left" >&nbsp;</td></tr>
        <tr style="height:30px" >
            <td align="left" class="CssSubHead"  >
                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblTemplateName"  runat="server"></asp:Label>&nbsp;:&nbsp;
                <asp:Label ID="lblTarget"  runat="server"></asp:Label>
            </td>
        </tr>
        <tr><td align="left" >&nbsp;</td></tr>
        <tr >
            <td align="left"  >
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                    <tr>
                        <td width="15%" class="Csslbl" align="right" >Date&nbsp;&nbsp;</td>
                        <td width="20%" align="left" >
                            <asp:TextBox ID="txtID" runat="server" Text="0" Visible="false"></asp:TextBox>
                            <%--<asp:Label ID="lblRotate" runat="server" style="transform: rotate(180deg);" Text="Text 1" ></asp:Label>--%>
                            <uc2:txtDate ID="txtSearchDate" runat="server" IsNotNull="true" />&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td width="65%" rowspan="2" valign="middle" >
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="formDialog" Width="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Csslbl" align="right" >Location Code&nbsp;&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtSearchLocationCode" runat="server" Text="" Width="100px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr>
                        <td colspan="3"><asp:Label ID="lblDesc" runat="server"></asp:Label></td>
                    </tr>
                </table>
                 <center>
                <asp:Label runat="server" ID="lblNotFound" Text="Data not found." ForeColor="Red"
                                Visible="false"></asp:Label>
                 </center> 
            </td>
        </tr>
    </table>
</asp:Content>

