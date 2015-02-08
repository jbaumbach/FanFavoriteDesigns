using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Data
{
    public class Address
    {
        private bool _taxable = false;
        private bool _domestic = true;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _companyName = string.Empty;
        private string _address1 = string.Empty;
        private string _address2 = string.Empty;
        private string _address3 = string.Empty;
        private string _city = string.Empty;
        private int _stateProvCode;
        private string _zipPostalCode = string.Empty;
        private string _countryCode = string.Empty;
        private string _phone = string.Empty;
        private string _stateProvAbbrev = string.Empty;
        private int _addressId = -1;
        private string _country = string.Empty;

        public enum AddrTypeCode
        {
            atcBilling = 1,
            atcShipping = 2
        }

        public bool Taxable
        {
            get { return _taxable; }
            set { _taxable = value; }
        }

        public bool Domestic
        {
            get { return _domestic; }
            set { _domestic = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        public string Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }

        public string Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }

        public string Address3
        {
            get { return _address3; }
            set { _address3 = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        /// <summary>
        /// The int code of the state/province from the DB.
        /// </summary>
        public int StateProvCode
        {
            get { return _stateProvCode; }
            set { _stateProvCode = value; }
        }

        /// <summary>
        /// State abbreviation, for use as a shortcut.  Use StateProvCode (int val from DB) whenever possible.
        /// </summary>
        public string StateProvAbbrev
        {
            get { return _stateProvAbbrev; }
            set { _stateProvAbbrev = value; }
        }

        public string ZipPostalCode
        {
            get { return _zipPostalCode; }
            set { _zipPostalCode = value; }
        }

        public string CountryCode
        {
            get { return _countryCode; }
            set { _countryCode = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public int AddressId
        {
            get { return _addressId; }
            set { _addressId = value; }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

    }
}
