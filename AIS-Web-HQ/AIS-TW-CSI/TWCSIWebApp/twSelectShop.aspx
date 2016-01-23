<%@ Page Language="VB" AutoEventWireup="false" CodeFile="twSelectShop.aspx.vb" Inherits="TWCSIWebApp_twSelectShop" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="x-ua-compatible" content="IE=9" >
    <%--<style type="text/css">
        .style3
        {
            width: 100%;
        }
        .style4
        {
            color: #8CB927;
            text-decoration: none;
            padding: 1px;
            font-style: normal;
            font-variant: normal;
            font-size: 12px;
            line-height: normal;
            font-family: Tahoma;
            width: 600px;
            height: 25px;
        }
        .style5
        {
            width: 60%;
            height: 25px;
        }
        .style7
        {
            height: 25px;
        }
        .style8
        {
            color: #8CB927;
            text-decoration: none;
            padding: 1px;
            font-style: normal;
            font-variant: normal;
            font-size: 12px;
            line-height: normal;
            font-family: Tahoma;
            width: 464px;
        }
        .style9
        {
            width: 464px;
        }
    </style>--%>
    <link rel="stylesheet" type="text/css" href="../Template/style.css" />
    <link rel="stylesheet" type="text/css" href="../Template/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="Scriptmanager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" >
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table  border="0" >
                                <tr>
                                    <td align="right" class="Csslbl">
                                        <asp:Label ID="lblRegion" runat="server" Text="Region" ></asp:Label>
                                    </td>
                                    <td style="padding-left:10px" >
                                        <asp:DropDownList ID="ddlRegion" runat="server" Width="179px" 
                                            AutoPostBack="true" class="hyjack">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Csslbl">
                                        <asp:Label ID="lblProvince" runat="server" Text="Province" ></asp:Label>
                                    </td>
                                    <td style="padding-left:10px">
                                        <asp:DropDownList ID="ddlProvince" runat="server" Width="178px" class="hyjack">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Csslbl">
                                        <asp:Label ID="lblSegment" runat="server" Text="Location Type" ></asp:Label>
                                    </td>
                                    <td style="padding-left:10px">
                                        <asp:DropDownList ID="ddlType" runat="server" Width="178px" class="hyjack">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Csslbl">
                                        <asp:Label ID="lblLocationCode" runat="server" Text="Location Code" ></asp:Label>
                                    </td>
                                    <td style="padding-left:10px">
                                        <asp:TextBox ID="txtLocationCode" runat="server" Width="176px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Csslbl">
                                        <asp:Label ID="lblLocationName" runat="server" Text="Location Name" ></asp:Label>
                                    </td>
                                    <td style="padding-left:10px">
                                        <asp:TextBox ID="txtLocationName" runat="server" Width="176px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                 <td  align="center">
                                 </td>
                                    <td  align="center">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="formDialog" Text="Search" 
                                            Width="55px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 80%; padding-right: 20px" valign="top" align="center">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:Panel ID="pnlRight" runat="server" Height="400px" ScrollBars="Auto"  
                                            Width="730px">
                                            <asp:GridView ID="gvShop" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                                Width="96%" >
                                                <RowStyle CssClass="RowStyle" />
                                                <Columns>
                                                
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="#" ItemStyle-Width="30">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="ChkSelectAll" runat="server" OnCheckedChanged="ChkSelectAll_CheckedChanged" AutoPostBack="true" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChkSelect" runat="server" />
                                                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("id") %>' Style="display: none"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="center" Width="30" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="no" HeaderText="No">
                                                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                     <asp:TemplateField HeaderText="" ShowHeader="True" HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderStyle Width="60" />
                                                        <ItemStyle HorizontalAlign="Left" Width="60" />
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblHeaderTextCode" runat="server" Text="Location"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLocationCode" runat="server" Text='<%# Bind("location_code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Province" ShowHeader="True" HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderStyle Width="60" />
                                                        <ItemStyle HorizontalAlign="Left" Width="60" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProvinceCode" runat="server" Text='<%# Bind("province_code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Region" ShowHeader="True" HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderStyle Width="60" />
                                                        <ItemStyle HorizontalAlign="Left" Width="60"  />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRegionCode" runat="server" Text='<%# Bind("region_code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" ShowHeader="True" HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderStyle Width="56%" />
                                                        <ItemStyle HorizontalAlign="Left" Width="56%" />
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblHeaderText" runat="server" Text="Location Name"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLocationName" runat="server" Text='<%# Bind("location_name_en") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location Type" ShowHeader="True" HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderStyle Width="80" />
                                                        <ItemStyle HorizontalAlign="Left" Width="80" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLocationType" runat="server" Text='<%# Bind("location_type") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Segment" ShowHeader="True" HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderStyle Width="80" />
                                                        <ItemStyle HorizontalAlign="Left" Width="80" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLocationSegment" runat="server" Text='<%# Bind("location_segment") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                               <PagerStyle CssClass="PagerStyle" />
                                                <PagerSettings Visible="false" />
                                                <HeaderStyle CssClass="HeaderStyle" />
                                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                            </asp:GridView>
                                            <asp:Label ID="lblerror" runat="server" Text="Data not found" ForeColor="Red" Visible="false" Font-Bold="true"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <br />
                            <asp:Button ID="btnAddUser" runat="server" Text="OK" Width="55px" CssClass="formDialog"/>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
