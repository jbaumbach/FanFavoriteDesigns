using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Data
{
    public class Order
    {
        public enum OrderStatusCode
        {
            oscCart = 10,
            oscCheckoutStep1 = 20,
            oscCheckoutStep2 = 30,
            oscCheckoutStep3 = 40,
            oscPendingPayment = 50,
            oscOrderFullfillment = 60,
            oscOrderPartiallyShipped = 70,
            oscOrderShipped = 80
        }

        private int _orderId;
        private Customer _customer = null;
        private OrderStatusCode _currentOrderStatusCode;
        private List<OrderItem> _items;
        private int _promoCode = -1;
        private string _shippingMethodDescShort;
        private decimal _total;
        private int _totalProducts;

        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        public OrderStatusCode CurrentOrderStatusCode
        {
            get { return _currentOrderStatusCode; }
            set { _currentOrderStatusCode = value; }
        }

        public List<OrderItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public int PromoCode
        {
            get { return _promoCode; }
            set { _promoCode = value; }
        }

        public string ShippingMethodDescShort
        {
            get { return _shippingMethodDescShort; }
            set { _shippingMethodDescShort = value; }
        }

        public decimal Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public string TotalFormatted
        {
            get { return string.Format("${0:0.00}", Total); }
        }

        public int TotalProducts
        {
            get { return _totalProducts; }
            set { _totalProducts = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customerId">The customer id</param>
        public Order(Customer customer)
        {
            _customer = customer;
            _items = new List<OrderItem>();
        }
    }
}
