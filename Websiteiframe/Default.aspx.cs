using Ffd.Data;

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    // protected Repeater colorListRepeater;

    protected void Page_Load(object sender, EventArgs e)
    {
        colorListRepeater.DataSource = DataManager.GetMaterials();
        colorListRepeater.DataBind();
    }

    protected void  lstColor_Bound(object sender, EventArgs e)
    {
        // System.Diagnostics.Debug.Write(e.Item.ToString());
    }
    protected void lstColor_DataBinding(object sender, EventArgs e)
    {

    }

    protected void colorListDataBound(object sender, RepeaterItemEventArgs e)
    {
    }
}
