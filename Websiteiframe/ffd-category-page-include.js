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
   
function Preview(name, number, template) {
    var cmdParams = "?name=" + escape(name.toUpperCase()) + "&number=" + escape(number) + "&template=" + escape(template);
    var imgUrl = "http://lavamantis.dyndns.org:8090/getimage.ashx" + cmdParams;
    $("table.prod-detail td.prod-detail-lt a img").attr("src", imgUrl);
    // alert(cmdParams);
}

function IsBlank(value) {
    return ((value == null) || (value == ""));
}

function HaveParams(name, number, template) {
    return ((!IsBlank(name) || !IsBlank(number)) && !IsBlank(template));
}

$(document).ready(function(){

    var name = $(".personalization-answers input:eq(0)").val();
    var number = $(".personalization-answers input:eq(1)").val();
    var template = "";

    var h1str = $(".product-detail h1").html();
    var h1words = h1str.split(" ");
    if (h1words.length > 2) {
        template = h1words[1];
    }            

    if (HaveParams(name, number, template)) {
        Preview(name, number, template);
    } else {
        name = unescape(getUrlParam("name"));
        number = unescape(getUrlParam("number"));

        if (HaveParams(name, number, template)) {
            $(".personalization-answers input:eq(0)").val(name.toUpperCase());
            $(".personalization-answers input:eq(1)").val(number);
            Preview(name, number, template);
        }
    }
        
    $("table.prod-detail td.prod-detail-lt a img").css("visibility", "visible");

 });
