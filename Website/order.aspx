<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="order.aspx.cs" Inherits="_order" EnableViewState="false" %>
<%@ Register Src="~/controls/pageheader.ascx" TagPrefix="ffd" TagName="PageHeader" %>
<%@ Register Src="~/controls/globalnav.ascx" TagPrefix="ffd" TagName="GlobalNav" %>
<%@ Register Src="~/controls/secondarycontent.ascx" TagPrefix="ffd" TagName="SecondaryContent" %>
<%@ Register Src="~/controls/pagefooter.ascx" TagPrefix="ffd" TagName="PageFooter" %>
<%@ Register Src="~/controls/stickerform.ascx" TagPrefix="ffd" TagName="StickerForm" %>
<%@ Register Src="~/controls/ordersummary.ascx" TagPrefix="ffd" TagName="OrderSummary" %>
<%@ Register Src="~/controls/addressform.ascx" TagPrefix="ffd" TagName="AddressForm" %>
<%@ Import Namespace="Ffd.Presentation.Website" %>
<%@ Import Namespace="Ffd.Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Fan FavoriteDesigns</title>
    
    <style type="text/css">
	    @import url(<%=ContentServer %>/styles/default.css);
    </style>	
    
    <script type="text/javascript" src="<%=ContentServer %>/scripts/order.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div id="main">
        <div id="header" class="pageblock">
            <ffd:PageHeader ID="pageHeader" runat="server" />
        </div>
        
        <div id="nav" class="pageblock">
            <ffd:GlobalNav ID="globalNav" runat="server" />
        </div>
        
        <div id="content">
            <div id="primary" class="pageblock">
                
                <asp:MultiView ID="orderPageMultiview" runat="server" ActiveViewIndex="0">
                
                <asp:View ID="default" runat="server">
                <h2>Order Stickers</h2>
                
                <div class="order_column" id="order_login">
                    <p class="content_title">Login</p>
                    <p>Been here before?</p>
                    <asp:PlaceHolder ID="loginErrPlaceholder" runat="server" Visible="false">
                        <p class="validationMessage"><asp:Literal ID="loginErrMsg" runat="server"></asp:Literal></p>
                    </asp:PlaceHolder>
                    <fieldset>
                    <ol>
                    <li>
                        <label for="txtEmailBox">Email:</label>
                        <asp:TextBox ID="txtEmailBox" runat="server" CssClass="inputTextbox" MaxLength="50" Width="155px"></asp:TextBox>
                    </li>
                    
                    <li>
                        <label for="txtPasswordBox">Password:</label>
                        <asp:TextBox ID="txtPasswordBox" runat="server" TextMode="Password" CssClass="inputTextbox" MaxLength="20" Width="155px"></asp:TextBox>
                    </li>
                    
                    <li class="formSubmitButton">
                        <asp:ImageButton ID="btnLoginSubmit" runat="server" ImageUrl="images/btn-login.gif" AlternateText="[submit]" OnClick="btnLoginSubmit_OnClick" />
                    </li>
                    </ol>
                    </fieldset>
                </div>
                
                <div class="order_column" id="order_register">
                    <p class="content_title">Register</p>
                    <p>Create a free login account.&nbsp; We will NEVER sell your email address to anyone.</p>
                    <asp:PlaceHolder ID="registerErrPlaceholder" runat="server" Visible="false">
                        <p class="validationMessage"><asp:Literal ID="registerErrMsg" runat="server"></asp:Literal></p>
                    </asp:PlaceHolder>
                
                    <fieldset>
                    <ol>
                    <li>
                        <label for="txtEmailBoxR">Email:</label>
                        <asp:TextBox ID="txtEmailBoxR" runat="server" CssClass="inputTextbox" MaxLength="50" Width="155px"></asp:TextBox>
                    </li>
                    
                    <li>
                        <label for="txtPasswordBoxR">Password:</label>
                        <asp:TextBox ID="txtPasswordBoxR" runat="server" TextMode="Password" CssClass="inputTextbox" MaxLength="20" Width="155px"></asp:TextBox>
                    </li>
                    
                    <li>
                        <label for="txtPasswordBoxR2">Re-enter Password:</label>
                        <asp:TextBox ID="txtPasswordBoxR2" runat="server" TextMode="Password" CssClass="inputTextbox" MaxLength="20" Width="155px"></asp:TextBox>
                    </li>

                    <li>
                        <label for="txtFirstName">First Name:</label>
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="inputTextbox" MaxLength="20" Width="155px"></asp:TextBox>
                    </li>
                    
                    <li>
                        <label for="txtMI">MI:</label>
                        <asp:TextBox ID="txtMI" runat="server" CssClass="inputTextbox" MaxLength="1" Width="10px"></asp:TextBox>
                    </li>
                    
                    <li>
                        <label for="txtLastName">Last Name:</label>
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="inputTextbox" MaxLength="20" Width="155px"></asp:TextBox>
                    </li>

                    <li class="formSubmitButton">
                        <asp:ImageButton ID="btnRegisterSubmit" runat="server" ImageUrl="images/btn-register.gif" AlternateText="[submit]" OnClick="btnRegisterSubmit_OnClick" />
                    </li>
                    </ol>
                    </fieldset>
                </div>
                </asp:View>                
                
                <asp:View ID="enterViewOrder" runat="server">
                <h2>Enter and View Order</h2>
                <p><%=UserSession.CurrentUserSession.CurrentCustomer.FirstName %>, enter some items here.  You know the drill.</p>
                
                <div id="current_order">
                    <h2>Current Order Items</h2>
                    
                    <asp:MultiView ID="customerOrder" runat="server" ActiveViewIndex="0">
                    
                    <asp:View ID="orderNoItems" runat="server">
                    <p>No current items.</p>
                    </asp:View>
                    
                    <asp:View ID="orderHaveItems" runat="server">
                    
                    <ul class="sticker_gallery">
                    <li>
                        <ul class="sticker_gallery_items header">
                            <li class="cos_name">Name</li>
                            <li class="cos_jersey_number">Jersey #</li>
                            <li class="cos_jersey_type">Jersey Type</li>
                            <li class="cos_preview">Preview</li>
                            <li class="cos_quantity cos_numericCol">Quantity</li>
                            <li class="cos_cost cos_numericCol">Cost</li>
                            <li class="cos_extCost cos_numericCol">Total</li>
                            <li class="cos_action">Actions</li>
                        </ul>
                    </li>
                    <asp:Repeater ID="itemRepeater" runat="server">
                    <ItemTemplate>
                    <li>
                        <ul class="sticker_gallery_items sticker_order_item">
                            <li class="cos_name"><%# (Container.DataItem as OrderItem).PlayerSeason.JerseyName %></li>
                            <li class="cos_jersey_number"><%# (Container.DataItem as OrderItem).PlayerSeason.JerseyNumber %></li>
                            <li class="cos_jersey_type"><%# (Container.DataItem as OrderItem).PlayerSeason.TemplateCurrent.TemplateDescShort %></li>
                            <li class="cos_preview"><img src="<%# (Container.DataItem as OrderItem).ImageUrl %>" alt="Item Icon" width="40" height="40" /></li>
                            <li class="cos_quantity cos_numericCol"><%# (Container.DataItem as OrderItem).Quantity %></li>
                            <li class="cos_cost cos_numericCol"><%# (Container.DataItem as OrderItem).PriceDisplay %></li>
                            <li class="cos_extCost cos_numericCol"><%# (Container.DataItem as OrderItem).ExtendedPriceDisplay %></li>
                            <li class="cos_action">
                                <asp:LinkButton ID="removeItem" runat="server" Text="Remove" OnClick="modifyItem_Click" CommandArgument='<%# string.Format("rem-{0}", Container.ItemIndex) %>'></asp:LinkButton>
                                / <asp:LinkButton ID="editItem" runat="server" Text="Edit" OnClick="modifyItem_Click" CommandArgument='<%# string.Format("edt-{0}", Container.ItemIndex) %>'></asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                    </ItemTemplate>
                    </asp:Repeater>
                    
                    <li class="cos_subtotal">
                        <ul class="sticker_gallery_items">
                            <li class="cos_name">Total Stickers</li>
                            <li class="cos_jersey_number">&nbsp;</li>
                            <li class="cos_jersey_type">&nbsp;</li>
                            <li class="cos_preview">&nbsp;</li>
                            <li class="cos_quantity cos_numericCol"><asp:Literal ID="stickerTotal" runat="server">[qty]</asp:Literal></li>
                            <li class="cos_cost cos_numericCol">&nbsp;</li>
                            <li class="cos_extCost cos_numericCol">&nbsp;</li>
                            <li class="cos_action">&nbsp;</li>
                        </ul>
                    </li>
                    
                    <asp:Repeater ID="itemSummaryRepeater" runat="server">
                    <ItemTemplate>
                    <li>
                        <ul class="sticker_gallery_items">
                            <li class="cos_name"><%# (Container.DataItem as OrderItem).Description %></li>
                            <li class="cos_jersey_number">&nbsp;</li>
                            <li class="cos_jersey_type">&nbsp;</li>
                            <li class="cos_preview">&nbsp;</li>
                            <li class="cos_quantity cos_numericCol"><%# (Container.DataItem as OrderItem).Quantity %></li>
                            <li class="cos_cost cos_numericCol"><%# (Container.DataItem as OrderItem).PriceDisplay %></li>
                            <li class="cos_extCost cos_numericCol"><%# (Container.DataItem as OrderItem).ExtendedPriceDisplay %></li>
                            <li class="cos_action">&nbsp;</li>
                        </ul>
                    </li>
                    </ItemTemplate>
                    </asp:Repeater>
                    
                    <li class="cos_grandtotal">
                        <ul class="sticker_gallery_items">
                            <li class="cos_name">Total</li>
                            <li class="cos_jersey_number">&nbsp;</li>
                            <li class="cos_jersey_type">&nbsp;</li>
                            <li class="cos_preview">&nbsp;</li>
                            <li class="cos_quantity cos_numericCol">&nbsp;</li>
                            <li class="cos_cost cos_numericCol">&nbsp;</li>
                            <li class="cos_extCost cos_numericCol"><asp:Literal ID="grandTotal" runat="server">[total]</asp:Literal></li>
                            <li class="cos_action">&nbsp;</li>
                        </ul>
                    </li>
                    
                    </ul>
                    
                    </asp:View>
                    </asp:MultiView>
                
                </div>
                
                <div class="sticker_form sticker_form_order">
                <ffd:StickerForm ID="stickerForm" runat="server" DisplayMode="OrderScreen" />
                </div>
                
                <asp:PlaceHolder ID="btnDonePlaceholder" runat="server" Visible="false">
                <div class="done_entering_order">
                <asp:ImageButton ID="btnDoneOrder" runat="server" ImageUrl="images/btn-doneenteringorder.gif" AlternateText="[done]" OnClick="btnDoneOrder_OnClick" CssClass="done_button" />
                </div>
                </asp:PlaceHolder>
               
                </asp:View>
                
                <asp:View ID="addressForm" runat="server">
                
                <asp:PlaceHolder ID="errShippingPlaceholder" runat="server" Visible="false">
                    <p class="validationShippingMessage"><asp:Literal ID="errMsg" runat="server"></asp:Literal></p>
                </asp:PlaceHolder>
                
                <div class="address">
                <div class="address_column">
                <p class="content_title">Shipping Address</p>
                <p class="address_description">Please enter your shipping address below.</p>
                
                <ffd:AddressForm ID="shippingAddress" runat="server" />
                </div>
                
                <div class="copy_column">
                <img id="btnCopy" src="<%=ContentServer %>/images/btn-copy.gif" onclick="CopyOrderValues();" alt="Copy &gt;&gt;"/>
                </div>
                
                <div class="address_column">
                <p class="content_title">Billing Address</p>
                <p class="address_description">Please enter your billing address below.</p>
                
                <ffd:AddressForm ID="billingAddress" runat="server" />
                </div>
                </div>
                
                </asp:View>
                
                </asp:MultiView>
                
                
                
                
            </div>
            
            <div id="secondary" class="pageblock">
                <ffd:SecondaryContent ID="secondaryContent" runat="server" />
            </div>
        </div>
        <div id="footer" class="pageblock">
            <ffd:PageFooter ID="pageFooter" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
