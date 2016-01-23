<%@ Page Title="" Language="VB" MasterPageFile="~/Template/ContentMasterPage.master" AutoEventWireup="false" CodeFile="frmWelcomePage.aspx.vb" Inherits="ReportApp_frmWelcomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">
        function SaveTransLog(TransDesc, LoginHisID) {
            var pageUrl = '<%=ResolveUrl("~/Template/AjaxScript.asmx")%>';
            $.ajax({
                type: "POST",
                url: pageUrl + "/SaveTransLog",
                data: "{'TransDesc':'" + TransDesc + "','LoginHisID':'" + LoginHisID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    return true;
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
        <asp:Label ID="lblWelcome" runat="server" Font-Size="X-Large" ForeColor="Gray"></asp:Label>
        <br /><br />
        <asp:Label ID="lblReportList" runat="server" ></asp:Label>
        <asp:Button ID="btnTest" runat="server" Text="Test" Visible="false"  />
    </center>
</asp:Content>

