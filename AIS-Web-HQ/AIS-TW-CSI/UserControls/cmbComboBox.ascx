<%@ Control Language="VB" AutoEventWireup="false" CodeFile="cmbComboBox.ascx.vb" Inherits="UserControls_cmbComboBox" %>

<asp:DropDownList ID="cmbCombo" runat="server" CausesValidation="True" Width="100px" class="hyjack" onmouseover="showDropDownToolTip(this);" onmouseout="hideDropDownToolTip();" >
</asp:DropDownList>
<asp:Label ID="lblValidText" runat="server" ForeColor="Red" Text="*" ></asp:Label>
<div id="divDropDownToolTip" style="position:absolute;display:none;background:lightyellow;border:1px solid gray;padding:2px;" onMouseOut="hideDropDownToolTip()">
  <span id="informationText"></span>
 </div>


<script type="text/javascript">
function showDropDownToolTip(elementRef)
{
    if (elementRef.options[elementRef.selectedIndex].value == '0' || elementRef.options[elementRef.selectedIndex].value == '') {
        return;
    }
   
     // Set to information text...
     var informationSpanRef = document.getElementById('informationText');
     informationSpanRef.innerHTML = elementRef.options[elementRef.selectedIndex].text;


     var toolTipRef = document.getElementById('divDropDownToolTip');
     toolTipRef.style.top = window.event.clientY + 20;
     toolTipRef.style.left = window.event.clientX;
     toolTipRef.style.display = 'block';
}


function hideDropDownToolTip()
{
 document.getElementById('divDropDownToolTip').style.display = 'none';
}
</script>