<%@ page language="C#" autoeventwireup="true" inherits="_stickergallery, App_Web_1g0p7cpi" %>
<%@ Register Src="~/controls/pageheader.ascx" TagPrefix="ffd" TagName="PageHeader" %>
<%@ Register Src="~/controls/globalnav.ascx" TagPrefix="ffd" TagName="GlobalNav" %>
<%@ Register Src="~/controls/secondarycontent.ascx" TagPrefix="ffd" TagName="SecondaryContent" %>
<%@ Register Src="~/controls/pagefooter.ascx" TagPrefix="ffd" TagName="PageFooter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Fan FavoriteDesigns</title>
    
    <style type="text/css">
	    @import url(<%=ContentServer %>/styles/default.css);
    </style>	
    
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
                <h2>Sticker Gallery</h2>
                <p>We have stickers for a wide range of sports!</p>
                
                <div>
                <ul class="sticker_gallery">
                    <li>
                        <ul class="sticker_gallery_items header">
                            <li class="sgi_sport">Sport</li>
                            <li class="sgi_color">Color</li>
                            <li class="sgi_size">Size</li>
                            <li class="sgi_price">Price</li>
                            <li class="sgi_preview">Preview</li>
                        </ul>
                    </li>
                    <li>
                        <ul class="sticker_gallery_items">
                            <li class="sgi_sport">Hockey</li>
                            <li class="sgi_color">White</li>
                            <li class="sgi_size">6 3/4" x 6"</li>
                            <li class="sgi_price">$5.75</li>
                            <li class="sgi_preview"><asp:Image ID="galleryImageHockey" runat="server" ImageUrl="images/myers-hockey-180.gif" Height="180" Width="180" /></li>
                        </ul>
                    </li>
                    <li>
                        <ul class="sticker_gallery_items">
                            <li class="sgi_sport">Football</li>
                            <li class="sgi_color">White</li>
                            <li class="sgi_size">6 3/4" x 6"</li>
                            <li class="sgi_price">$5.75</li>
                            <li class="sgi_preview">
                                <asp:Image ID="galleryImageFootball" runat="server" ImageUrl="images/myers-football-180.gif" Height="180" Width="180" /></li>
                        </ul>
                    </li>
                    <li>
                        <ul class="sticker_gallery_items">
                            <li class="sgi_sport">Baseball</li>
                            <li class="sgi_color">White</li>
                            <li class="sgi_size">6 3/4" x 6"</li>
                            <li class="sgi_price">$5.75</li>
                            <li class="sgi_preview">
                                <asp:Image ID="galleryImageBaseball" runat="server" ImageUrl="images/myers-Baseball-180.gif" Height="180" Width="180" /></li>
                        </ul>
                    </li>
                    <li>
                        <ul class="sticker_gallery_items">
                            <li class="sgi_sport">Basketball</li>
                            <li class="sgi_color">White</li>
                            <li class="sgi_size">6 3/4" x 6"</li>
                            <li class="sgi_price">$5.75</li>
                            <li class="sgi_preview">
                                <asp:Image ID="galleryImageBasketball" runat="server" ImageUrl="images/myers-Basketball-180.gif" Height="180" Width="180" /></li>
                        </ul>
                    </li>
                </ul>
                </div>
                
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
