using Ffd.Common;

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Ffd.Presentation.Website
{
    class FfdImageButton : ImageButton
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ImageUrl = Common.Functions.BuildUrlFromElements(WebConfig.ContentServer, ImageUrl);
        }
    }
}
