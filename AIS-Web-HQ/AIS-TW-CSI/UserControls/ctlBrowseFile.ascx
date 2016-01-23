<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlBrowseFile.ascx.vb" Inherits="UserControls_ctlBrowseFile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<table border="0" cellpadding="0" cellspacing="0" valign="top" >
    <tr>
        <td valign="top">
            <cc1:AsyncFileUpload ID="FileBrowse" runat="server" FailedValidation="False" UploaderStyle="Traditional" CssClass=""   />
        </td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td valign="top">
            <asp:Button ID="Button1" runat="server" Text="Upload"  Width="80px" Height="20px" />
        </td>
    </tr>
</table>

