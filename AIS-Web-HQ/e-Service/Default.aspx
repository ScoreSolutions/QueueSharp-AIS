<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Web Appointment V <%=globalFunction.GetVersion%></title>
    <script type="text/javascript">
function myFunction()
{
    document.getElementById("mobileparam").select();
}
function myFunction2() {
    document.getElementById("mobileNo").select();
}
function myFunction3() {
    document.getElementById("Type").select();
}
</script>
    <style type="text/css">
        #lang
        {
            width: 100px;
        }
    </style>
</head>
<body>
<center>
<form id="Form1" target="ifrm" method="post" action="frm_login.aspx" style="width:1024px;">
     	<input type="text" value="HWWQ+8Mw8pQvsXVTAIBpHxSvBo12nzwsh3gAqi4qDw8ynpHithI8CPtccvEfo91DahdpI+wLOizlQsy+TAm3RQ==" name="mobileparam" id="mobileparam" style="width:800px; font-size:medium" onclick="myFunction()" /><br />
        <br />
&nbsp;<input type="text" value="mobileNo" name="mobileNo" id="mobileNo" 
            style="width:285px; font-size:medium" onclick="myFunction2()" 
            maxlength="10" /><input 
            type="text" value="network Type" name="Type" id="Type" 
            style="width:312px; font-size:medium" onclick="myFunction3()" />
     	
     	<select id="lang" name="lang" >
     	    <option>th</option>
     	    <option>en</option>
     	</select>
   	&nbsp<br />
        <br />
        &nbsp
   	<input type="submit" size="font-size:xx-large;"/>
		<br /><br />
     

    </form>
    
    <iframe id="ifrm" name="ifrm"  width="780px" height="660px" visible="true" align="middle" frameborder="0" scrolling="no" title="e-service" src="frm_login.aspx"></iframe>
    <%--<asp:Button ID="btn" runat="server" Text="Test" />--%>
    </center>
</body>
</html>
