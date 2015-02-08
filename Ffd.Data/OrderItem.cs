using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Data
{
    public class OrderItem : ProductItemJersey
    {

        public enum OrderItemTypeCode
        {
            oitcProduct = 10,
            oitcShipping = 20,
            oitcSalesTax = 30,
            oitcVolumeDiscount = 40,
            oitcPromoCodeDisc = 50,
            oitcWholesaleDisc = 60
        }

        private int _orderId;
        private int _orderItemNumber;
        private int _quantity = 0;
        private string _imageUrl;
        private decimal _price = 0;
        // private string _description;
        private OrderItemTypeCode _currentOrderItemTypeCode;
        private string _descriptionExternal;

        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public int OrderItemNumber
        {
            get { return _orderItemNumber; }
            set { _orderItemNumber = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }

        public string PriceDisplay
        {
            get
            {
                return string.Format("${0:0.00}", Price);
            }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }


            // Hmmmmm... could link to template - but what if I change the price?  Then all the past
            // invoices would be incorrect (e.g. permanently irretrievable).  I'm making the 
            // decision to write the price to the order here.  It's non-normalized, but a lot safer 
            // and more flexible.

            //get
            //{
            //    if (_currentOrderItemTypeCode == OrderItemTypeCode.oitcProduct)
            //    {
            //        return this.PlayerSeason.ToString();
            //    }
            //    else
            //    {
            //        return _price;
            //    }
            //}
            //set
            //{
            //    if (_currentOrderItemTypeCode == OrderItemTypeCode.oitcProduct)
            //    {
            //        throw new ApplicationException("Attempt to set price for a product - not good.");
            //    }
            //    else
            //    {
            //        _price = value;
            //    }
            //}
        }

        public string ExtendedPriceDisplay
        {
            get
            {
                return string.Format("${0:0.00}", ExtendedPrice);
            }
        }

        public decimal ExtendedPrice
        {
            get
            {
                return _quantity * _price;
            }
        }

        public string Description
        {
            get 
            {
                if (_currentOrderItemTypeCode == OrderItemTypeCode.oitcProduct)
                {
                    return this.PlayerSeason.ToString();
                }
                else
                {
                    return GetTypeCodeString(_currentOrderItemTypeCode);
                }
            }
            set
            {
                throw new ApplicationException("Attempt to set description for a product - not good.");

                //if (_currentOrderItemTypeCode == OrderItemTypeCode.oitcProduct)
                //{
                //    throw new ApplicationException("Attempt to set description for a product - not good.");
                //}
                //else
                //{
                //    _description = value;
                //}
            }
        }

        public OrderItemTypeCode CurrentOrderItemTypeCode
        {
            get { return _currentOrderItemTypeCode; }
            set { _currentOrderItemTypeCode = value; }
        }

        public string DescriptionExternal
        {
            get { return _descriptionExternal; }
            set { _descriptionExternal = value; }
        }
	



        public static string GetTypeCodeString(OrderItemTypeCode type)
        {
            switch (type)
            {
                case OrderItemTypeCode.oitcPromoCodeDisc:
                    return "Promo Code";
                case OrderItemTypeCode.oitcSalesTax:
                    return "Sales Tax";
                case OrderItemTypeCode.oitcShipping:
                    return "Shipping";
                case OrderItemTypeCode.oitcVolumeDiscount:
                    return "Volume Discount";
                case OrderItemTypeCode.oitcWholesaleDisc:
                    return "Wholesale Disc.";
                default:
                    throw new ApplicationException(string.Format("Unknown order item type code specified: \"{0}\"", type));
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="playerSeason"></param>
        public OrderItem(PlayerSeason playerSeason)
        {
            this.PlayerSeason = playerSeason;
            _currentOrderItemTypeCode = OrderItemTypeCode.oitcProduct;
            _price = playerSeason.TemplateCurrent.MSRP;
        }

        /// <summary>
        /// Constructor for summary items
        /// </summary>
        /// <param name="typeCode"></param>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        public OrderItem(OrderItemTypeCode typeCode, int quantity, decimal price)
        {
            _currentOrderItemTypeCode = typeCode;
            _quantity = quantity;
            _price = price;
        }
    }
}
