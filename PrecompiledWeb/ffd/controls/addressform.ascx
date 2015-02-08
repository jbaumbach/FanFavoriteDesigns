<%@ control language="C#" autoeventwireup="true" inherits="controls_addressform, App_Web_udte8pc-" %>


<fieldset>
<ol>
<li>
    <label for="txtFName">First Name:</label>
    <asp:TextBox ID="txtFName" runat="server" CssClass="inputTextbox" MaxLength="50" Width="155px"></asp:TextBox>
</li>

<li>
    <label for="txtLName">Last Name:</label>
    <asp:TextBox ID="txtLName" runat="server" CssClass="inputTextbox" MaxLength="20" Width="155px"></asp:TextBox>
</li>

<li>
    <label for="txtCompanyName">Company Name:</label>
    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="inputTextbox" MaxLength="20" Width="155px"></asp:TextBox>
</li>

<li>
    <label for="txtAddr1">Address line 1:</label>
    <asp:TextBox ID="txtAddr1" runat="server" CssClass="inputTextbox" MaxLength="20" Width="155px"></asp:TextBox>
</li>

<li>
    <label for="txtAddr2">Address line 2:</label>
    <asp:TextBox ID="txtAddr2" runat="server" CssClass="inputTextbox" MaxLength="20" Width="155px"></asp:TextBox>
</li>

<li>
    <label for="txtCity">City:</label>
    <asp:TextBox ID="txtCity" runat="server" CssClass="inputTextbox" MaxLength="20" Width="155px"></asp:TextBox>
</li>

<li>
    <label for="lstState">State:</label>
    <asp:DropDownList ID="lstState" runat="server" ></asp:DropDownList>
</li>

<li>
    <label for="txtZip">Zip/Postal:</label>
    <asp:TextBox ID="txtZip" runat="server" CssClass="inputTextbox" MaxLength="20" Width="155px"></asp:TextBox>
</li>

<li>
    <label>Country:</label>
    <span>United States</span>
</li>

<li>
    <label for="txtPhone">Phone:</label>
    <asp:TextBox ID="txtPhone" runat="server" CssClass="inputTextbox" MaxLength="20" Width="155px"></asp:TextBox>
</li>

</ol>
</fieldset>
