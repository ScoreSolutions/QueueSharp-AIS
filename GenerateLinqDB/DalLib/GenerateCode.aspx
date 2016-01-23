<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GenerateCode.aspx.vb" Inherits="DalLib.GenerateCode" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Generate</title>
    <link href="../Templates/BaseStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="updMain" runat="server">
            <ContentTemplate>
                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table border="0" width="900px" cellpadding="0" cellspacing="0" style="border-right: #0099ff 1px solid; border-top: #0099ff 1px solid; border-left: #0099ff 1px solid; border-bottom: #0099ff 1px solid; background-color:#E0FFFF">
                                <tr>
                                    <td style="height:25px; width:10px"></td>
                                    <td style="width:100px">Database Type</td>
                                    <td >
                                        <asp:RadioButtonList ID="rdiType" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="SQL">SQL Server</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="Oracle">Oracle</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td style="width:40px">&nbsp;</td>
                                    <td style="width:100px">Language</td>
                                    <td >
                                        <asp:RadioButtonList ID="rdiLang" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="VB">VB</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="C#">C#</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="right" style="width: 218px">
                                        &nbsp;
                                    </td>
                                    <td style="width:10px"></td>
                                </tr> 
                                <tr>
                                    <td >&nbsp;</td>
                                    <td colspan="6"><hr />
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtServerWatermark" runat="server" TargetControlID="txtServer" WatermarkCssClass="water"  >
                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtDatabaseWatermark" runat="server" TargetControlID="txtDatabase" WatermarkCssClass="water">
                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtUserIDWatermark" runat="server" TargetControlID="txtUserID" WatermarkCssClass="water">
                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtPasswordWatermark" runat="server" TargetControlID="txtPassword" WatermarkCssClass="water">
                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtTableWatermark" runat="server" TargetControlID="txtTable" WatermarkCssClass="water">
                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtNamespaceWatermark" runat="server" TargetControlID="txtNamespace" WatermarkCssClass="water">
                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtClassWatermark" runat="server" TargetControlID="txtClass" WatermarkCssClass="water">
                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                    </td>
                                    <td >&nbsp;</td>
                                </tr> 
                                <tr>
                                    <td style="height:25px;">&nbsp;</td>
                                    <td >Project Code</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtProjectCode" runat="server" Width="190px" CssClass="zTextbox">Proj</asp:TextBox>
                                    </td>
                                    <td >&nbsp;</td>
                                    <td >Database Name</td>
                                    <td >
                                        <asp:TextBox ID="txtDatabase" runat="server" Width="190px" CssClass="zTextbox">EXCISE_LAW</asp:TextBox></td>
                                    <td >&nbsp;</td>
                                    <td >&nbsp;</td>
                                </tr> 
                                <tr>
                                    <td style="height:25px;">&nbsp;</td>
                                    <td >Data Source</td>
                                    <td  align="left">
                                        <asp:TextBox ID="txtServer" runat="server" Width="190px" CssClass="zTextbox">ORCL</asp:TextBox>
                                    </td>
                                    <td >&nbsp;</td>
                                    <td >Password</td>
                                    <td >
                                        <asp:TextBox ID="txtPassword" runat="server" Width="190px" CssClass="zTextbox">password</asp:TextBox></td>
                                    <td >&nbsp;</td>
                                    <td >&nbsp;</td>
                                </tr> 
                                <tr>
                                    <td style="height:25px;"></td>
                                    <td >User ID</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtUserID" runat="server" Width="190px" CssClass="zTextbox">EXCISE_LAW</asp:TextBox>
                                    </td>
                                    <td >&nbsp;</td>
                                    <td >Table/View</td>
                                    <td >
                                        <asp:TextBox ID="txtTable" runat="server" Width="190px" CssClass="zTextbox">DISTRICT</asp:TextBox>
                                    </td>
                                    <td >&nbsp;</td>
                                    <td >&nbsp;</td>
                                </tr> 
                                <tr>
                                    <td >&nbsp</td>
                                    <td colspan="6"><hr /></td>
                                    <td >&nbsp</td>
                                </tr> 
                                <tr>
                                    <td style="height:5px; ">&nbsp;</td>
                                    <td >Namespace</td>
                                    <td colspan="2" >[Project].[DAL/Data].<asp:TextBox ID="txtNamespace" runat="server" Width="110px" CssClass="zTextbox">Illegal</asp:TextBox></td>
                                    <td >Class Name</td>
                                    <td ><asp:TextBox ID="txtClass" runat="server" Width="190px" CssClass="zTextbox">District</asp:TextBox></td>
                                    <td ><asp:Button ID="btnGenerateCode" runat="server" Text="Generate Code" Width="100px" /></td>
                                    <td >&nbsp;</td>
                                    <td >&nbsp;</td>
                                </tr> 
                                <tr>
                                    <td></td>
                                    <td ></td>
                                    <td ></td>
                                    <td ></td>
                                    <td ></td>
                                    <td ></td>
                                    <td ></td>
                                    <td ></td>
                                </tr> 
                            </table>
                        </td> 
                    </tr> 
                    <tr>
                        <td></td> 
                    </tr> 
                    <tr>
                        <td style="width:895px;">
                            <div id="up_container" >
                                <asp:UpdatePanel ID="updCode" runat="server">
                                    <ContentTemplate>
                                        Code DAL :
                                        <asp:TextBox ID="txtCodeDAL" runat="server" Height="215px" TextMode="MultiLine" Width="895px" Wrap="False" BackColor="#FFFFC0" ReadOnly="true" CssClass="zTextbox" BorderColor="#C04000" BorderStyle="Solid" BorderWidth="1px" ></asp:TextBox>
                                        <br />
                                        Code Data :
                                        <asp:TextBox ID="txtCodeData" runat="server" Height="215px" TextMode="MultiLine" Width="895px" Wrap="False" BackColor="#FFFFC0" ReadOnly="true" CssClass="zTextbox" BorderColor="#C04000" BorderStyle="Solid" BorderWidth="1px" ></asp:TextBox>
                                        <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnGenerateCode" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>  
                            <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="updCode" >
                                <Animations>
                                    <OnUpdating>
                                        <Sequence>
                                            <ScriptAction Script="var b = $find('animation'); b._originalHeight = b._element.offsetHeight;" />
                                            <Parallel duration="0">
                                                <EnableAction AnimationTarget="btnGenerateCode" Enabled="false" />
                                                <EnableAction AnimationTarget="rdbSql" Enabled="false" />
                                                <EnableAction AnimationTarget="rdbOracle" Enabled="false" />
                                                <EnableAction AnimationTarget="txtServer" Enabled="false" />
                                                <EnableAction AnimationTarget="txtDatabase" Enabled="false" />
                                                <EnableAction AnimationTarget="txtUserID" Enabled="false" />
                                                <EnableAction AnimationTarget="txtPassword" Enabled="false" />
                                                <EnableAction AnimationTarget="txtTable" Enabled="false" />
                                                <EnableAction AnimationTarget="txtNamespace" Enabled="false" />
                                                <EnableAction AnimationTarget="txtClass" Enabled="false" />
                                            </Parallel>
                                            <StyleAction Attribute="overflow" Value="hidden" /> 
                                           
                                            <%-- Do each of the selected effects --%>
                                            <Parallel duration="0" Fps="20">
                                                <Resize Height="0" /> 
                                            </Parallel>  
                                        </Sequence> 
                                    </OnUpdating>
                                    <OnUpdated>
                                        <Sequence>
                                            <%-- Do each of the selected effects --%>
                                            <Parallel duration=".25" Fps="30">
                                                <Condition ConditionScript="$get('effect_fade').checked">
                                                    <FadeIn AnimationTarget="up_container" minimumOpacity=".2" />
                                                </Condition>
                                                <Condition ConditionScript="$get('effect_collapse').checked">
                                                    <%-- Get the stored height --%>
                                                    <Resize HeightScript="$find('animation')._originalHeight" />
                                                </Condition>
                                                <Condition ConditionScript="$get('effect_color').checked">
                                                    <Color AnimationTarget="up_container" PropertyKey="backgroundColor"
                                                        StartValue="#FF0000" EndValue="#40669A" />
                                                </Condition>
                                            </Parallel>
                                            
                                            <%-- Enable all the controls --%>
                                            <Parallel duration="0">
                                                <EnableAction AnimationTarget="btnGenerateCode" Enabled="true" />
                                                <EnableAction AnimationTarget="rdbSql" Enabled="true" />
                                                <EnableAction AnimationTarget="rdbOracle" Enabled="true" />
                                                <EnableAction AnimationTarget="txtServer" Enabled="true" />
                                                <EnableAction AnimationTarget="txtDatabase" Enabled="true" />
                                                <EnableAction AnimationTarget="txtUserID" Enabled="true" />
                                                <EnableAction AnimationTarget="txtPassword" Enabled="true" />
                                                <EnableAction AnimationTarget="txtTable" Enabled="true" />
                                                <EnableAction AnimationTarget="txtNamespace" Enabled="true" />
                                                <EnableAction AnimationTarget="txtClass" Enabled="true" />
                                            </Parallel>                            
                                        </Sequence>
                                    </OnUpdated>
                                </Animations> 
                            </ajaxToolkit:UpdatePanelAnimationExtender>
                        </td> 
                    </tr>
                </table> 
            </ContentTemplate> 
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
