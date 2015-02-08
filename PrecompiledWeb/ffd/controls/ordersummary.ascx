<%@ control language="C#" autoeventwireup="true" inherits="controls_ordersummary, App_Web_udte8pc-" %>

<asp:Repeater ID="itemRepeater" runat="server">
<HeaderTemplate>
<table>
<tr class="header">
    <td class="os_description">Description</td>
    <td>Price</td>
    <td>Quantity</td>
    <td>Total</td>
</tr>
</HeaderTemplate>

<ItemTemplate>
<tr class="header">
    <td><asp:Literal ID="description" runat="server">description</asp:Literal></td>
    <td><asp:Literal ID="priceEach" runat="server">priceEach</asp:Literal></td>
    <td><asp:Literal ID="quantity" runat="server">quantity</asp:Literal></td>
    <td><asp:Literal ID="total" runat="server">total</asp:Literal></td>
</tr>
</ItemTemplate>

<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>

