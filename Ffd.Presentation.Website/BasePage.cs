using Ffd.Presentation.Website;

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ffd.Presentation.Website
{
    public class BasePage : System.Web.UI.Page
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

        public void AddContentServerToImageControlUrl(string controlName)
        {
            Image control = FindControl(controlName) as Image;
            BaseControl.AddContentServerToImageControlUrl(control);
        }
    }
}
