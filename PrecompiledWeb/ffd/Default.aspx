<%@ page language="C#" autoeventwireup="true" inherits="_Default, App_Web_1g0p7cpi" enableviewstate="false" %>
<%@ Register Src="~/controls/pageheader.ascx" TagPrefix="ffd" TagName="PageHeader" %>
<%@ Register Src="~/controls/globalnav.ascx" TagPrefix="ffd" TagName="GlobalNav" %>
<%@ Register Src="~/controls/secondarycontent.ascx" TagPrefix="ffd" TagName="SecondaryContent" %>
<%@ Register Src="~/controls/pagefooter.ascx" TagPrefix="ffd" TagName="PageFooter" %>
<%@ Register Src="~/controls/stickerform.ascx" TagPrefix="ffd" TagName="StickerForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
                <p class="content_title">Show support for your favorite athlete with our premium sports-themed stickers!</p>
                <p>These elegant designs are made from white, high-gloss 5-year vinyl and are created using the 
                latest, state-of-the-art technology.</p>
                
                <div id="content_primary_pics">
                    <asp:MultiView ID="contentPics" runat="server" ActiveViewIndex="0">
                    
                    <asp:View ID="default" runat="server">
                    <img src="<%=ContentServer %>/images/ford-football-small.jpg" alt="Ford hatchback marketing image" width="348" height="162" />
                    <img src="<%=ContentServer %>/images/greytruck-hockey-small.jpg" alt="Ford hatchback marketing image" width="348" height="189" />
                    </asp:View>
                    
                    <asp:View ID="userJerseyResults" runat="server">
                    <asp:Image ID="userJerseryResultsImg" runat="server" AlternateText="Your Jersey Results" Width="350" Height="350" />
                    </asp:View>
                    
                    </asp:MultiView>
                </div>
                
                <div id="content_primary_text">
                    Perfect for:
                    <ul>
                    <li>Little league sports</li>
                    <li>High-school and college teams</li>
                    <li>Recreational leagues</li>
                    </ul>
                    
                    <div id="preview_sticker" class="sticker_form">
                    <p class="content_title">Preview Your Sticker</p>
                    <asp:PlaceHolder ID="previewErrPlaceholder" runat="server" Visible="false">
                        <p class="validationMessage"><asp:Literal ID="previewErrMsg" runat="server"></asp:Literal></p>
                    </asp:PlaceHolder>
                    
                    <ffd:StickerForm ID="stickerForm" runat="server" ButtonImageUrl="images/btn-preview.gif" />
                    
                    </div>

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
