using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ffd.Data;

namespace Ffd.Presentation.Manager
{
    public partial class frmCustomer : FfdForm
    {
        private Customer _currentCustomer = new Customer();
        private DialogResult _result = DialogResult.Cancel;

        public frmCustomer(Customer customer) : this()
        {
            CurrentCustomer = customer;
        }

        public frmCustomer()
        {
            InitializeComponent();
            cmbSourceCode.DataSource = DataManager.GetLeadSourceCodes();
            cmbStatusCode.DataSource = DataManager.GetLeadStatusCodes();
        }

        public Customer CurrentCustomer
        {
            get 
            {
                SaveUIToCustomerObject();
                return _currentCustomer; 
            }
            set 
            { 
                _currentCustomer = value;
                LoadUIWithCustomerObject();
            }
        }


        public DialogResult Result
        {
            get { return _result; }
            set { _result = value; }
        }


        private void LoadUIWithCustomerObject()
        {
            if (_currentCustomer != null && tpBilling_ctlAddress != null && tpShipping_ctlAddress != null)
            {
                SetControlText(lblCustomerId, _currentCustomer.CustomerId);
                SetControlText(txtCompany, _currentCustomer.CompanyName);
                SetControlText(lblCreateDate, "");
                tpBilling_ctlAddress.CurrentAddress = _currentCustomer.BillingAddress;
                tpShipping_ctlAddress.CurrentAddress = _currentCustomer.ShippingAddress;

                if (_currentCustomer.GetType() == typeof(Lead))
                {
                    Lead lead = (Lead)_currentCustomer;

                    //cmbSourceCode.SelectedValue = lead.LeadSourceCode;
                    //cmbStatusCode.SelectedValue = lead.LeadStatusCodeCurrent;
                    chkIsSchool.Checked = lead.IsSchool;
                    chkIsAthletics.Checked = lead.IsAthletics;
                    SetControlText(txtEnrollment, lead.SchoolTotalEnrollment);
                    SetControlText(lblLeadDate, lead.LeadDate);
                }
            }
        }

        private void UpdateUIFromAddresses()
        {
            if (chkUpdateFromShipping.Checked)
            {
                txtCompany.Text = tpShipping_ctlAddress.CurrentAddress.CompanyName;
            }
        }

        private void SaveUIToCustomerObject()
        {
            if (_currentCustomer != null && tpBilling_ctlAddress != null && tpShipping_ctlAddress != null)
            {
                UpdateUIFromAddresses();
                _currentCustomer.CompanyName = GetControlText(txtCompany);
                _currentCustomer.BillingAddress = tpBilling_ctlAddress.CurrentAddress;
                _currentCustomer.ShippingAddress = tpShipping_ctlAddress.CurrentAddress;

                //if (_currentCustomer.GetType() == typeof(Lead))
                //{
                //    Lead lead = (Lead)_currentCustomer;

                //    cmbSourceCode.SelectedValue = lead.LeadSourceCode;
                //    cmbStatusCode.SelectedValue = lead.LeadStatusCodeCurrent;
                //    chkIsSchool.Checked = lead.IsSchool;
                //    chkIsAthletics.Checked = lead.IsAthletics;
                //    SetControlText(txtEnrollment, lead.SchoolTotalEnrollment);
                //    SetControlText(lblLeadDate, lead.LeadDate);
                //}

            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Result = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            CurrentCustomer = new Lead();
        }
    }
}