using Ffd.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Ffd.Presentation.Website
{
    public class UserSession
    {
        private const string USER_SESSION = "user_session";

        private Customer _currentCustomer;
        private Order _currentOrder = null;

        public Customer CurrentCustomer
        {
            get { return _currentCustomer; }
            set 
            { 
                _currentCustomer = value;
                SaveToSession();
            }
        }

        public Order Order
        {
            get { return _currentOrder; }
            set 
            { 
                _currentOrder = value;
                SaveToSession();
            }
        }


        public static bool IsLoggedIn
        {
            get
            {
                return (CurrentUserSession != null);
            }
        }

        public static UserSession CurrentUserSession
        {
            get
            {
                HttpSessionState state = HttpContext.Current.Session;

                if (state[USER_SESSION] != null)
                {
                    return state[USER_SESSION] as UserSession;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Saves current property values to the session state 
        /// </summary>
        private void SaveToSession()
        {
            HttpSessionState state = HttpContext.Current.Session;
            state[USER_SESSION] = this;
        }

        /// <summary>
        /// Constructor.  This constructor inserts the object instance itself into the session variables 
        /// after setting the Customer property to the passed constructor.
        /// </summary>
        public UserSession(Customer customer)
        {
            _currentCustomer = customer;
            SaveToSession();
        }


    }
}
