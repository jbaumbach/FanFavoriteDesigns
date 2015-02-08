<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="myaccount.aspx.cs" Inherits="_myaccount" %>
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
                <p>[your content here]</p>
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
