
<%@ Page Title="" Language="VB" MasterPageFile="~/Template/ContentMasterPage.master"
    AutoEventWireup="false" CodeFile="twLocation.aspx.vb" Inherits="TWCSIWebApp_twLocation" %>
<%@ Register src="../UserControls/PageControl.ascx" tagname="PageControl" tagprefix="uc2" %>
<%@ Register src="../UserControls/ctlBrowseFile.ascx" tagname="ctlBrowseFile" tagprefix="uc5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="CssHead" align="left" width="100%">
                Location
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
                <asp:Button ID="btnUpload" runat="server" Text="Upload File" CssClass="formDialog" Width="80px"  />
                <asp:Button ID="btnAddLocation" runat="server" Text="Add Location" CssClass="formDialog" Width="90px" />
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="V1" runat="server">
                        <table " >
                            <tr style="height: 30px">
                                <td class="Csslbl" align="right"  width="20%"  >
                                    Attach File&nbsp;&nbsp;
                                </td>
                                <td  width="80%">
                                    <uc5:ctlBrowseFile ID="ctlLocationList" runat="server" VisibleUploadButton="true"
                                        Width="400px" />
                                </td>
                            </tr>
                            <tr style="height: 135px">
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="V2" runat="server">
                        <table width="100%">
                            <tr>
                                <td align="right" class="Csslbl">
                                    <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>&nbsp;<asp:Label
                                        ID="lblLocation_code" runat="server" Text="Location Code"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLocation_Code" runat="server" 
                                    Width="200px" MaxLength="50"></asp:TextBox>
                                    <asp:Label ID="lblDisplayLocatioCode" runat="server" 
                                        OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)"
                                    BackColor="GrayText" Visible="False" Width="200px" Height="18px"></asp:Label>
                                    <asp:TextBox ID="txtID" runat="server" Text="0" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="Csslbl">
                                    <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>&nbsp;<asp:Label
                                        ID="lblLocation_Name" runat="server" Text="Location Name"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLocation_Name" runat="server" Width="200px" MaxLength="255"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="Csslbl">
                                    <asp:Label ID="lblLocation_Segment" runat="server" Text="Location Segment"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSegment" runat="server" Height="22px" Width="205px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="Csslbl">
                                    <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>&nbsp;<asp:Label
                                        ID="lblRegion_Code" runat="server" Text="Region Code"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRegion" runat="server" Height="22px" Width="205px" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="Csslbl">
                                    <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>&nbsp;<asp:Label
                                        ID="lblProvince_Code" runat="server" Text="Province Code"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProvince" runat="server" Height="22px" Width="205px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="Csslbl">
                                    <asp:Label ID="lblLocation_Type" runat="server" Text="Location Type"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlType" runat="server" Height="22px" Width="205px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="Csslbl">
                                    &nbsp;</td>
                                <td align="left">
                                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" Text="Active" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="Csslbl">
                                </td>
                                <td align="left" style="padding-left: 20px">
                                    <asp:Button ID="btnSave" runat="server" CssClass="formDialog" Text="Save" 
                                        Width="80px" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="formDialog" Text="Cancel" 
                                        Width="80px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="Csslbl">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>

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
                <asp:GridView ID="gvLocationList" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                    CssClass="GridViewStyle" Width="100%" AllowPaging="true" ShowHeader="true" >
                    <RowStyle CssClass="RowStyle" />
                    <EmptyDataTemplate  >
                       
                    </EmptyDataTemplate>
                    <EmptyDataRowStyle HorizontalAlign="Center" ForeColor="Red" />
                    <Columns>
                        <asp:BoundField DataField="no" HeaderText="No">
                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="location_code" HeaderText="Location" SortExpression="location_code">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="province_code" HeaderText="Province" SortExpression="province_code">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="region_code" HeaderText="Region" SortExpression="region_code">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="location_name_th" HeaderText="Location Name" SortExpression="location_name_th">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="location_type" HeaderText="Location Type" SortExpression="location_type">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField ShowHeader="false">
                            <HeaderTemplate>
                                Manage
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" runat="server" OnClick="imgEdit_Click" ImageUrl="~/images/ico_edit.jpg"
                                    ToolTip="Edit" CommandArgument="<%# Bind('id') %>"></asp:ImageButton>
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
                <asp:TextBox ID="txtSortField" runat="server" Visible="False" Width="15px" ></asp:TextBox><asp:TextBox ID="txtSortDir" runat="server" Visible="False" Width="15px" Text="DESC" ></asp:TextBox></td></tr></table></asp:Content>
