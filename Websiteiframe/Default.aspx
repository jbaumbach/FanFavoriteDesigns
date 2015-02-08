<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Import Namespace="Ffd.Data" %>
<%@ Import Namespace="Ffd.App.Core" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>iframe test page</title>
    <script type="text/javascript" src="http://www.ergocentricsoftware.com/ffd/scripts/jquery-1.2.2.min.js"></script>
    
    <style type="text/css">
    body {
    background:black;
    color:white;
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>This is the iframe test page</h1>
    <p>The javascript in this container page is duplicated on the FFD website to support the iframe (which runs on the Ergo website).</p>
    </div>
    <!--*****************************************************************************************************-->



    
        
    <script type="text/javascript">
    function getUrlParam( name )
    {
        name = name.replace(/[\[]/,"\\\[").replace(/[\]]/,"\\\]");
        var regexS = "[\\?&]"+name+"=([^&#]*)";
        var regex = new RegExp( regexS );
        var results = regex.exec( window.location.href );
        if( results == null )
            return "";
        else
            return results[1];
    } 
    
    function IsBlank(value) {
        return ((value == null) || (value == ""));
    }

    function HaveParams(name, number, template) {
        return ((!IsBlank(name) || !IsBlank(number)) && !IsBlank(template));
    }

    var name = getUrlParam("name");
    var number = getUrlParam("number");
    var template = getUrlParam("template");

    // This is the frame source on the ergo server.
    // var frameSrc = "http://www.ergocentricsoftware.com/ffd/iframe.html";
    
    // This is the frame source on the local server.  Do dev on this, then copy changes to iframe.html and 
    // upload to ergo server.
    var frameSrc = "http://localhost/iframe/iframe-local.html";
    
    if (HaveParams(name, number, template))
    {
        frameSrc += "?name=" + name + "&number=" + number + "&template=" + template;
    }
    
    
    
    
    
    
    //*****************************************************************************************************
    
    document.write('<iframe height="400" frameborder="0" width="600" scrolling="no" src="' + frameSrc + '"></iframe>');
    
    document.write('<h3>Change this in the source code:</h3><p>iframe source: <b>' + frameSrc + '</b></p>');
    </script>
    
    <%--<iframe height="400" frameborder="0" width="600" scrolling="no" src="iframe.html?name=hello&number=22&template=Football"></iframe>--%>
    <p>Debug info from the local getimage.ashx file:</p>
    <iframe style="background-color:White;" width="500px" height="200px" scrolling="yes" frameborder="1" src="http://localhost/ffd/getimage.ashx?debug=1"></iframe>
    
    <br />
    <br />
    Colors from the database:<br />
    <asp:DropDownList ID="lstColor" runat="server" DataSourceID="materialsColors" DataTextField="MfgColorDesc" DataValueField="RGBColorHex" OnDataBound="lstColor_Bound" DataTextFormatString="Color: {0}">
    </asp:DropDownList>
    <asp:ObjectDataSource ID="materialsColors" runat="server" SelectMethod="GetMaterials" TypeName="Ffd.Data.DataManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="materialTypeCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br /><br />
    <asp:Repeater ID="colorListRepeater" runat="server" OnItemDataBound="colorListDataBound">
    <HeaderTemplate><select id="lstColor" name="lstColor">
    </HeaderTemplate>
    
    <ItemTemplate>  <option value="<%# ((Material) Container.DataItem).RGBColorHex  %>" style="background-color:#<%# ((Material) Container.DataItem).RGBColorHex  %>; color:<%# ApplicationManager.ColorBrightness(((Material) Container.DataItem).RGBColorHex) < 80 ? "#fff" : "#000" %>;"><%# ((Material) Container.DataItem).MfgColorDesc  %></option>
    </ItemTemplate>
    
    <FooterTemplate></select>
    </FooterTemplate>
    </asp:Repeater>
    
    </form>
</body>
</html>
