using Ffd.Data;
using Ffd.App.Core;

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

public partial class controls_ordersummary : Ffd.Presentation.Website.BaseControl
{
    private Order _currentOrder;

    public Order CurrentOrder
    {
        get { return _currentOrder; }
        set { _currentOrder = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
