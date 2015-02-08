using Ffd.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Ffd.Presentation.Manager
{
    public partial class AddressControl : FfdUserControl
    {
        /// <summary>
        /// Set this to a new address.  Vis Studio access all the properties and it doesn't like
        /// null being returned.  Strange things start happening.
        /// </summary>
        private Address _currentAddress = new Address();

        /// <summary>
        /// Gets or sets the current address for this control.  Will not return null.
        /// </summary>
        public Address CurrentAddress
        {
            get 
            {
                SaveAddressUI();
                return _currentAddress; 
            }
            set 
            { 
                _currentAddress = value; 
                LoadAddressUI();
            }
        }

        /// <summary>
        /// Constructor with an address as a parameter.
        /// </summary>
        /// <param name="address"></param>
        public AddressControl(Address address) : this()
        {
            CurrentAddress = address;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AddressControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load the UI of the form with the values from the local address object.
        /// </summary>
        private void LoadAddressUI()
        {
            if (_currentAddress != null)
            {
                SetControlText(txtFName, _currentAddress.FirstName);
                SetControlText(txtLName, _currentAddress.LastName);
                SetControlText(txtCompanyName, _currentAddress.CompanyName);
                SetControlText(txtAddress1, _currentAddress.Address1);
                SetControlText(txtAddress2, _currentAddress.Address2);
                SetControlText(txtCity, _currentAddress.City);
                SetControlText(txtState, _currentAddress.StateProvAbbrev);
                SetControlText(txtZip, _currentAddress.ZipPostalCode);
                SetControlText(lblAddrId, _currentAddress.AddressId);
            }
        }

        /// <summary>
        /// Save the UI fields into the local address object.
        /// </summary>
        private void SaveAddressUI()
        {
            if (_currentAddress != null)
            {
                _currentAddress.FirstName = GetControlText(txtFName);
                _currentAddress.LastName = GetControlText(txtLName);
                _currentAddress.CompanyName = GetControlText(txtCompanyName);
                _currentAddress.Address1 = GetControlText(txtAddress1);
                _currentAddress.Address2 = GetControlText(txtAddress2);
                _currentAddress.City = GetControlText(txtCity);
                _currentAddress.StateProvAbbrev = GetControlText(txtState);
                _currentAddress.ZipPostalCode = GetControlText(txtZip);
            }
        }

        private void lnkGoogleIt_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //
            // Open browser w/google search
            // 
            string search = GetControlText(txtCompanyName).Trim() + " address " + GetControlText(txtState).Trim() + " " + GetControlText(txtZip);
            search = search.Replace(" ", "+");
            string url = string.Format("http://www.google.com/search?hl=en&q={0}", search);
            System.Diagnostics.Process.Start(url);
        }
    }
}
