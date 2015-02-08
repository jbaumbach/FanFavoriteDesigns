using Ffd.App.Core;
using Ffd.Common;
using Ffd.Data;
using Ffd.Presentation.Website;

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : Ffd.Presentation.Website.BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    /// <summary>
    /// Receives events from child controls.
    /// </summary>
    /// <param name="source">The control that generated the event.</param>
    /// <param name="args">Event arguments.</param>
    /// <returns></returns>
    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (source == stickerForm)
        {
            //
            // The user clicked "PREVIEW" button
            //
            controls_stickerform thisStickerForm = (controls_stickerform) source;

            if (thisStickerForm.CurrentProductItemJersey != null)
            {
                string imageUrl = string.Format("{0}?{1}&{2}&{3}", WebConfig.ImageGeneratorUrl,
                    string.Format("{0}={1}", QsKeys.Name, thisStickerForm.CurrentProductItemJersey.PlayerSeason.JerseyName),
                    string.Format("{0}={1}", QsKeys.Number, thisStickerForm.CurrentProductItemJersey.PlayerSeason.JerseyNumber),
                    string.Format("{0}={1}", QsKeys.Template, thisStickerForm.CurrentProductItemJersey.PlayerSeason.TemplateCurrent.TemplateDescShort));

                userJerseryResultsImg.ImageUrl = imageUrl;
            }
            else
            {
                previewErrPlaceholder.Visible = true;
                previewErrMsg.Text = thisStickerForm.ValidationErrorMessage;
            }
            contentPics.ActiveViewIndex = 1;

        }

        return base.OnBubbleEvent(source, args);
    }
}
