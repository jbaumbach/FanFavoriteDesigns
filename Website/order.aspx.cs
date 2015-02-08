using Ffd.App.Core;
using Ffd.Presentation.Website;
using Ffd.Data;

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
using Ffd.Common;

public partial class _order : Ffd.Presentation.Website.BasePage
{

    private string _errMessage = string.Empty;

    /// <summary>
    /// Page load runs BEFORE the button click events run.  Just an FYI.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
            //
            // Initial load of page
            //
        if (UserSession.IsLoggedIn)
        {
            if (!IsPostBack)
            {
                orderPageMultiview.ActiveViewIndex = 1;

                ShowOrderItems();
            }
        }
        else
        {
            orderPageMultiview.ActiveViewIndex = 0;
        }

        //}

        AddContentServerToImageControlUrl("btnLoginSubmit");
        AddContentServerToImageControlUrl("btnRegisterSubmit");
        AddContentServerToImageControlUrl("btnDoneOrder");
        // AddContentServerToImageControlUrl("btnCopy");
    }

    private void ShowOrderItems()
    {
        if ((UserSession.CurrentUserSession.Order == null) || (UserSession.CurrentUserSession.Order.Items.Count == 0))
        {
            customerOrder.ActiveViewIndex = 0;
            btnDonePlaceholder.Visible = false;
        }
        else
        {
            customerOrder.ActiveViewIndex = 1;

            ApplicationManager.BuildOrderSummary(UserSession.CurrentUserSession.Order);

            itemRepeater.DataSource = ApplicationManager.GetOrderItemsOfType(UserSession.CurrentUserSession.Order, true);
            itemRepeater.DataBind();

            itemSummaryRepeater.DataSource = ApplicationManager.GetOrderItemsOfType(UserSession.CurrentUserSession.Order, false);
            itemSummaryRepeater.DataBind();

            stickerTotal.Text = UserSession.CurrentUserSession.Order.TotalProducts.ToString();

            grandTotal.Text = UserSession.CurrentUserSession.Order.TotalFormatted.ToString();

            // orderSummary.CurrentOrder = UserSession.CurrentUserSession.Order;

            btnDonePlaceholder.Visible = true;
        }
    }

    protected void btnLoginSubmit_OnClick(object sender, EventArgs e)
    {
        loginErrPlaceholder.Visible = true;
        loginErrMsg.Text = "Unable to log in user.  Please try again.";

        Customer existingCustomer = DataManager.GetCustomer(txtEmailBox.Text, txtPasswordBox.Text);

        if (existingCustomer != null)
        {
            UserSession session = new UserSession(existingCustomer);
            orderPageMultiview.ActiveViewIndex = 1;
        }
        else
        {
            loginErrPlaceholder.Visible = true;
            loginErrMsg.Text = "Either the email address is not recognized or the password is incorrect.  Please try again!";
        }
    }

    protected void btnRegisterSubmit_OnClick(object sender, EventArgs e)
    {
        Customer customer = GetCustomerFromUI();

        if (customer == null)
        {
            registerErrPlaceholder.Visible = true;
            registerErrMsg.Text = _errMessage;
        }
        else
        {
            if (DataManager.InsertCustomer(customer))
            {
                UserSession session = new UserSession(customer);
                orderPageMultiview.ActiveViewIndex = 1;
            }
            else
            {
                registerErrPlaceholder.Visible = true;
                registerErrMsg.Text = "Unable to create user account (internal error).  Please try again later.";
            }
        }
    }

    /// <summary>
    /// Event handler for when user is done entering their order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDoneOrder_OnClick(object sender, EventArgs e)
    {
        orderPageMultiview.ActiveViewIndex = 2;

        Customer currentCustomer = UserSession.CurrentUserSession.Order.Customer;

        shippingAddress.Customer = currentCustomer;
        shippingAddress.CurrentAddress = currentCustomer.ShippingAddress;

        billingAddress.CurrentAddress = currentCustomer.BillingAddress;
    }

    protected void modifyItem_Click(Object sender, EventArgs e)
    {
        Order order = UserSession.CurrentUserSession.Order;

        if (order != null)
        {
            string[] parameters = (sender as LinkButton).CommandArgument.Split('-');

            if (Functions.IsNumeric(parameters[1]))
            {
                switch (parameters[0])
                {
                    case "rem":
                        //
                        // Remove item
                        //
                        order.Items.RemoveAt(int.Parse(parameters[1]));
                        ShowOrderItems();
                        break;
                    case "edt":
                        //
                        // Edit item
                        //
                        stickerForm.CurrentProductItemJersey = (order.Items[int.Parse(parameters[1])] as ProductItemJersey);
                        break;
                }


            }
            else
            {
                // Something strange happened
            }
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
        if (UserSession.IsLoggedIn)
        {
            if (source == stickerForm)
            {
                //
                // The user clicked "SUBMIT" button to add a jersey
                //
                controls_stickerform thisStickerForm = (controls_stickerform)source;

                if (thisStickerForm.CurrentProductItemJersey != null)
                {
                    Order order = UserSession.CurrentUserSession.Order;

                    if (order == null)
                    {
                        //
                        // Create a new order
                        //
                        order = new Order(UserSession.CurrentUserSession.CurrentCustomer);
                        UserSession.CurrentUserSession.Order = order;
                    }

                    OrderItem item = new OrderItem(thisStickerForm.CurrentProductItemJersey.PlayerSeason);
                    item.Quantity = thisStickerForm.Quantity;

                    // Pretty ugly kludge here - prolly wanna store this in the DB at some point
                    item.ImageUrl = Functions.BuildUrlFromElements(WebConfig.ContentServer,
                        string.Format("images/myers-{0}-40.gif", item.PlayerSeason.TemplateCurrent.TemplateDescShort));

                    order.Items.Add(item);

                    //
                    // Clear out text boxes for next item entry
                    //
                    stickerForm.ClearUI();

                    ShowOrderItems();
                }
                else
                {
                    //previewErrPlaceholder.Visible = true;
                    //previewErrMsg.Text = thisStickerForm.ValidationErrorMessage;
                }
                // contentPics.ActiveViewIndex = 1;

            }

        }

        return base.OnBubbleEvent(source, args);
    }


    /// <summary>
    /// Gets a new customer from the UI
    /// </summary>
    /// <returns>null if something didn't work.</returns>
    private Customer GetCustomerFromUI()
    {
        Customer result = null;

        //
        // Validate our results
        //
        if (!Functions.IsValidEmailAddress(txtEmailBoxR.Text))
        {
            _errMessage = "Email address not valid.";
            return null;
        }
        
        if (txtPasswordBoxR.Text != txtPasswordBoxR2.Text)
        {
            _errMessage = "Passwords do not match.";
            return null;
        }

        if (txtPasswordBoxR.Text.Length < 3)
        {
            _errMessage = "Password must be at least 3 digits in length.";
            return null;
        }

        if (txtFirstName.Text.Length < 1)
        {
            _errMessage = "First name must be at least 1 letter.";
            return null;
        }

        if (txtLastName.Text.Length < 2)
        {
            _errMessage = "Last name must be at least 2 letters.";
            return null;
        }

        //
        // Check for existing user using that password
        //

        Customer existing = DataManager.GetCustomer(txtEmailBoxR.Text);

        if (existing != null)
        {
            _errMessage = "That email address already exists in our system.";
            return null;
        }

        result = new Customer();

        result.EmailAddress = txtEmailBoxR.Text;
        result.Password = txtPasswordBoxR.Text;
        result.FirstName = txtFirstName.Text;
        result.LastName = txtLastName.Text;
        result.MiddleInitial = txtMI.Text;

        return result;
    }
}
