using Ffd.Data;

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class controls_addressform : Ffd.Presentation.Website.BaseControl
{
    private Address _currentAddress;
    private Customer _currentCustomer;

    public Address CurrentAddress
    {
        get { return _currentAddress; }
        set { _currentAddress = value; }
    }

    public Customer Customer
    {
        get { return _currentCustomer; }
        set { _currentCustomer = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnPreRender(EventArgs e)
    {
        SetItemsIntoDropdownWithExtraDefaultValue(lstState, DataManager.GetStates("USA"));

        if (_currentCustomer != null)
        {
            txtFName.Text = _currentCustomer.FirstName;
            txtLName.Text = _currentCustomer.LastName;
            txtCompanyName.Text = _currentCustomer.CompanyName;
            txtPhone.Text = _currentCustomer.PhoneDay;
        }

        if (_currentAddress != null)
        {
            //
            // Prepopulate the address fields
            //
            txtFName.Text = _currentAddress.FirstName;
            txtLName.Text = _currentAddress.LastName;
            txtCompanyName.Text = _currentAddress.CompanyName;
            txtAddr1.Text = _currentAddress.Address1;
            txtAddr2.Text = _currentAddress.Address2;
            txtCity.Text = _currentAddress.City;
            // lstState.SelectedValue = _currentAddress.StateProvCode;     // dunno if this is gonna work
            txtZip.Text = _currentAddress.ZipPostalCode;
            // no country yet
            txtPhone.Text = _currentAddress.Phone;
        }

        base.OnPreRender(e);
    }

    public static void SetItemsIntoDropdownWithExtraDefaultValue(DropDownList comboBox, ICollection dataObjects)
    {
        List<ListItem> comboItems = new List<ListItem>();

        if (dataObjects.Count > 1)
        {
            comboItems.Add(new ListItem("[select]"));
        }

        if (dataObjects is List<DictionaryEntry>)
        {
            foreach (DictionaryEntry de in dataObjects)
            {
                comboItems.Add(new ListItem(de.Value.ToString(), de.Key.ToString()));
            }

        }
        else
        {
            foreach (object item in dataObjects)
            {
                comboItems.Add(new ListItem(item.ToString()));
            }
            //comboBox.DataSource = comboItems;
            //comboBox.DataBind();
        }

        foreach (ListItem li in comboItems)
        {
            comboBox.Items.Add(li);
        }
    }


}
