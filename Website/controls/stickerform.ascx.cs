using Ffd.Data;
using Ffd.Common;

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

public partial class controls_stickerform : Ffd.Presentation.Website.BaseControl
{
    public enum Mode
    {
        PreviewScreen = 0,
        OrderScreen = 1
    }

    private ProductItemJersey _productItemJersey = null;
    private string _validationErrorMesssage;
    private int _quantity = -1;
    private Mode _displayMode = Mode.PreviewScreen;
    private string _buttonImageUrl = string.Empty;

    public ProductItemJersey CurrentProductItemJersey
    {
        get { return _productItemJersey; }
        set { _productItemJersey = value; }
    }

    public string ValidationErrorMessage
    {
        get { return _validationErrorMesssage; }
    }

    public int Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    public Mode DisplayMode
    {
        get { return _displayMode; }
        set { _displayMode = value; }
    }

    public string ButtonImageUrl
    {
        get { return _buttonImageUrl; }
        set { _buttonImageUrl = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (_displayMode == Mode.OrderScreen)
        {
            quantityBoxPlaceholder.Visible = true;
        }
    }

    /// <summary>
    /// Init runs before controls get their values from the form.  This is a good time to populate the controls.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            // If EnableViewState=true, controls remember stuff.  So, don't initialize things twice on postback.
        }

        // If EnableViewState=false, the controls "forget" their visibilities, list items, image url paths, etc.

        if (_buttonImageUrl != string.Empty)
        {
            btnPreviewSubmit.ImageUrl = _buttonImageUrl;
        }

        AddContentServerToImageControlUrl(btnPreviewSubmit);

        // Somehow the drop-down list remembers its index - even with viewstate set to off.  Nice.
        lstJerseyType.DataSource = DataManager.GetTemplates();
        lstJerseyType.DataBind();

        if (_productItemJersey != null)
        {
            // Preload UI elements here
        }

        base.OnInit(e);
    }

    protected void btnPreviewSubmit_OnClick(object sender, ImageClickEventArgs e)
    {
        if (Functions.IsNumeric(txtNumberBox.Text))
        {
            PlayerSeason playerSeason = new PlayerSeason(txtNameBox.Text, txtNumberBox.Text);

            //
            // Kludge to allow leading zero until this field is converted to a string everywhere
            //
            //if (number.ToString() != txtNumberBox.Text)
            //{
            //    playerSeason.PlayerNumberFormat = "{0:00}";
            //}

            Template template = DataManager.GetTemplate(lstJerseyType.SelectedValue);

            if (template != null)
            {
                bool valid = true;

                if (_displayMode == Mode.OrderScreen)
                {
                    if (Functions.IsNumeric(txtQuantity.Text) && (int.Parse(txtQuantity.Text) > 0))
                    {
                        _quantity = int.Parse(txtQuantity.Text);
                    }
                    else
                    {
                        _validationErrorMesssage = "Please enter a numeric quantity greater than zero.";
                        valid = false;
                    }
                }

                if (valid)
                {
                    //
                    // Success! 
                    //
                    _productItemJersey = new ProductItemJersey(playerSeason, template);
                }

            }
            else
            {
                _validationErrorMesssage = string.Format("Unable to determine jersey template from value \"{0}\".", lstJerseyType.SelectedValue);
            }
        }
        else
        {
            _validationErrorMesssage = string.Format("The jersey number (\"{0}\") must be numeric.", txtNumberBox.Text);
        }

        //
        // Send user's event to the parent page so it can do it's magic
        //
        base.RaiseBubbleEvent(this, (EventArgs) e);
    }

    public void ClearUI()
    {
        txtNameBox.Text = string.Empty;
        txtNumberBox.Text = string.Empty;
    }
}
