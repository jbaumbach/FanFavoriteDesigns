using Ffd.Presentation.Website;

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ffd.Presentation.Website
{
    public class BaseControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// Content server for the website
        /// </summary>
        public string ContentServer
        {
            get
            {
                return WebConfig.ContentServer;
            }
        }

        public static void AddContentServerToImageControlUrl(Image control)
        {
            if (!control.ImageUrl.Contains(WebConfig.ContentServer))
            {
                control.ImageUrl = Ffd.Common.Functions.BuildUrlFromElements(WebConfig.ContentServer, control.ImageUrl);
            }
        }
    }
}
