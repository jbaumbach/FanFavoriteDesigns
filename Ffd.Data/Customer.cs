using Ffd.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Ffd.Data
{
    public class Customer : PersonName
    {
        private int _customerId = -1;
        private string _emailAddress;
        private string _password;
        private Address _billingAddress;
        private Address _shippingAddress;
        private string _companyName;
        private string _phoneDay;
        private DateTime _leadDate = DateTime.MinValue;
        private string _createBy = "webserver";

        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        public string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public Address BillingAddress
        {
            get { return _billingAddress; }
            set { _billingAddress = value; }
        }

        public Address ShippingAddress
        {
            get { return _shippingAddress; }
            set { _shippingAddress = value; }
        }

        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        public string PhoneDay
        {
            get { return _phoneDay; }
            set { _phoneDay = value; }
        }

        public DateTime LeadDate
        {
            get { return _leadDate; }
            set { _leadDate = value; }
        }

        /// <summary>
        /// Not used yet (usually customer inserted by 'webserver' user)
        /// </summary>
        public string CreateBy
        {
            get { return _createBy; }
            set { _createBy = value; }
        }
        
        /// <summary>
        /// Hack to simulate casting from a customer to one of the subclasses of Customer, like Lead.
        /// Essentially does a shallow copy of all the properties.  Be careful, it might not work with complex properties.  See 
        /// remarks for examples.
        /// </summary>
        /// <remarks>
        ///     Customer cust = [get customer object];
        ///     Lead result = new Lead();
        ///     Object workingLead = (Object)result;
        ///     cust.Upcast(ref workingLead);
        ///     result = (Lead)workingLead;
        /// </remarks>
        /// <param name="destination">An object that you've casted the destination into. Then you can cast this object into your final destination.</param>
        public void Upcast(ref Object destination)
        {
            Type sourceType = this.GetType();
            Type destinationType = destination.GetType();

            foreach (PropertyInfo sourceProperty in sourceType.GetProperties())
            {
                PropertyInfo destinationProperty = destinationType.GetProperty(sourceProperty.Name);
                if (destinationProperty != null)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(this, null), null);
                }
           }
        }

        /// <summary>
        /// Return a more developer friendly description that shows up in the debugging windows.  Good times.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (!Functions.IsEmptyString(this.BuildFullName()) && !Functions.IsEmptyString(this.EmailAddress))
            {
                return string.Format("name: \"{0}\" - email: \"{1}\"", this.BuildFullName(), this.EmailAddress);
            }
            else if (!Functions.IsEmptyString(this.CompanyName))
            {
                return this.CompanyName;
            }
            else
            {
                return this.GetType().ToString();
            }
        }

    }
}
