using Ffd.Common;
using Ffd.Presentation.Website;

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _stickergallery : Ffd.Presentation.Website.BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //Image galleryImage = FindControl("galleryImage") as Image;
        //galleryImage.ImageUrl = Ffd.Common.Functions.BuildUrlFromElements(WebConfig.ContentServer, galleryImage.ImageUrl);

        if (!IsPostBack)
        {
            AddContentServerToImageControlUrl("galleryImageHockey");
            AddContentServerToImageControlUrl("galleryImageFootball");
            AddContentServerToImageControlUrl("galleryImageBaseball");
            AddContentServerToImageControlUrl("galleryImageBasketball");
        }
    }
}
