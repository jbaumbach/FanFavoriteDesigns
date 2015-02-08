<%@ control language="C#" autoeventwireup="true" inherits="controls_stickerform, App_Web_udte8pc-" %>

<fieldset>
<ol>
<li>
    <label for="txtNameBox">Name:</label>
    <asp:TextBox ID="txtNameBox" runat="server" CssClass="inputTextbox" MaxLength="13"></asp:TextBox>
</li>

<li>
    <label for="txtNumberBox">Number:</label>
    <asp:TextBox ID="txtNumberBox" runat="server" CssClass="inputTextbox" MaxLength="2" Width="20"></asp:TextBox>
</li>

<li>
    <label for="lstJerseyType">Type:</label>
    <asp:DropDownList ID="lstJerseyType" runat="server" CssClass="inputListbox">
    </asp:DropDownList>
</li>

<asp:PlaceHolder ID="quantityBoxPlaceholder" runat="server" Visible="false">
<li>
    <label for="txtQuantity">Quantity:</label>
    <asp:TextBox ID="txtQuantity" runat="server" CssClass="inputTextbox" MaxLength="3" Width="30"></asp:TextBox>
</li>
</asp:PlaceHolder>

<li class="formSubmitButton">
    <asp:ImageButton ID="btnPreviewSubmit" runat="server" ImageUrl="images/btn-enter.gif" AlternateText="[submit]" OnClick="btnPreviewSubmit_OnClick" />
</li>
</ol>
</fieldset>
