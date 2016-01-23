<%@ Page Language="VB" MasterPageFile="~/Template/ContentMasterPage.master" AutoEventWireup="false" CodeFile="frmActivity.aspx.vb" Inherits="CSIWebApp_frmActivity" title="" %>
<%@ Register src="../UserControls/PageControl.ascx" tagname="PageControl" tagprefix="uc2" %>
<%@ Register src="../UserControls/ctlBrowseFile.ascx" tagname="ctlBrowseFile" tagprefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="CssHead" align="left" width="100%">
                Siebel Cat/Subcat 
            </td>
        </tr>
        <tr>
            <td align="center">
                <img src="../images/PageHeaderLine.png" alt="" width="100%" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                
               
                        <table " >
                            <tr style="height: 30px">
                                <td class="Csslbl" align="right"  width="20%"  >
                                    Attach File&nbsp;&nbsp;
                                </td>
                                <td  width="80%">
                                    <uc5:ctlBrowseFile ID="ctlActivityList" runat="server" VisibleUploadButton="true"
                                        Width="400px" />
                                </td>
                            </tr>
                           
                        </table>
                    

            </td>
        </tr>
    </table>
    
    <table width="100%">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td align="right" style="width:90%"> 
                        <asp:TextBox ID="txtSearch" runat="server" Width="168px"></asp:TextBox>
                        </td>
                        <td  align="left" style="width:10%">
                         <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="formDialog" Width="80px"  />   
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
     <tr>
            <td align="left">
                <uc2:PageControl ID="PageControl1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvCategoryList" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                    CssClass="GridViewStyle" Width="100%" AllowPaging="true" ShowHeader="true" >
                    <RowStyle CssClass="RowStyle" />
                    <EmptyDataTemplate  >
                      <%--  Data not found.--%>
                    </EmptyDataTemplate>
                    <EmptyDataRowStyle HorizontalAlign="Center" ForeColor="Red" />
                    <Columns>
                        <asp:BoundField DataField="no" HeaderText="No">
                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Category_Name" HeaderText="Category" SortExpression="Category_Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="subCategory_Name" HeaderText="Sub Category" SortExpression="subCategory_Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField ShowHeader="false">
                            <HeaderTemplate>
                                Delete
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDelete" runat="server" OnClick="imgDelete_Click" ImageUrl="~/images/ico_delete.jpg"
                                    ToolTip="Delete" CommandArgument="<%# Bind('id') %>" OnClientClick="return confirm('Are you sure?');">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                            <ItemStyle HorizontalAlign="Left" Width="40px"/>
                        </asp:TemplateField>
                        <asp:BoundField DataField="id">
                            <ControlStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle CssClass="PagerStyle" />
                    <PagerSettings Visible="false" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
                <center>
                <asp:Label runat="server" ID="lblNotFound" Text="Data not found." ForeColor="Red"
                                Visible="false"></asp:Label>
                 </center>              
                <asp:TextBox ID="txtSortField" runat="server" Visible="False" Width="15px" ></asp:TextBox><asp:TextBox ID="txtSortDir" runat="server" Visible="False" Width="15px" Text="DESC" ></asp:TextBox></td></tr></table>
</asp:Content>

