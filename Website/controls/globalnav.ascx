<%@ Control Language="C#" AutoEventWireup="true" CodeFile="globalnav.ascx.cs" Inherits="controls_globalnav" %>

<ul>
    <li><asp:HyperLink ID="home" runat="server" NavigateUrl="~/default.aspx">Home</asp:HyperLink></li>
    <li><asp:HyperLink ID="stickerGallery" runat="server" NavigateUrl="~/stickergallery.aspx">Sticker Gallery</asp:HyperLink></li>
    <li><asp:HyperLink ID="order" runat="server" NavigateUrl="~/order.aspx">Order</asp:HyperLink></li>
    <li><asp:HyperLink ID="myaccount" runat="server" NavigateUrl="~/myaccount.aspx">My Account</asp:HyperLink></li>
    <li><asp:HyperLink ID="login" runat="server" NavigateUrl="~/login.aspx">Login</asp:HyperLink></li>
</ul>
