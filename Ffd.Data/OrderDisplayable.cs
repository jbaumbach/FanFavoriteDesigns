using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Data
{
    /// <summary>
    /// Class to generate a displayable order, used for binding to a grid or repeater.  As of now, 
    /// only 1 item is supported!
    /// </summary>
    public class OrderDisplayable
    {
        public enum DisplayModes
        {
            /// <summary>
            /// Default output
            /// </summary>
            All = 0,

            /// <summary>
            /// If this is a grid preview display.
            /// </summary>
            GridPreview = 1
        }

        private Order _order;
        private int _index = 0;
        private DisplayModes _displayMode;


        public Order Order
        {
            get { return _order; }
            set { _order = value; }
        }

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        public DisplayModes DisplayMode
        {
            get { return _displayMode; }
            set { _displayMode = value; }
        }
	
        public string OrderId 
        { 
            // get { return _order.OrderId; }
            get { return _index == 0 ? _order.OrderId.ToString() : "  \"  "; }
        }

        public string ShippingFirstName
        {
            get { return _index == 0 ? _order.Customer.ShippingAddress.FirstName : string.Empty; }
        }

        public string ShippingLastName
        {
            get { return _index == 0 ? _order.Customer.ShippingAddress.LastName : string.Empty; }
        }

        public string ShippingCity
        {
            get { return _index == 0 ? _order.Customer.ShippingAddress.City : string.Empty; }
        }

        public string ShippingState
        {
            get { return _index == 0 ? _order.Customer.ShippingAddress.StateProvAbbrev : string.Empty; }
        }

        public string ShippingZip
        {
            get { return _index == 0 ? _order.Customer.ShippingAddress.ZipPostalCode : string.Empty; }
        }

        public string ShippingCountry
        {
            get { return _index == 0 ? _order.Customer.ShippingAddress.CountryCode : string.Empty; }
        }

        public string NumberOfItems
        {
            get { return _index == 0 ? _order.Items.Count.ToString() : ""; }
        }

        public string ItemDescriptionExternal
        {
            get { return _order.Items[_index].DescriptionExternal; }
        }

        public int ItemTemplateId
        {
            get { return _order.Items[_index].PlayerSeason.TemplateCurrent.TemplateId; }
        }

        public Template ItemTemplate
        {
            get { return _order.Items[_index].PlayerSeason.TemplateCurrent; }
        }

        public string ItemTemplateDescriptionShort
        {
            get { return _order.Items[_index].PlayerSeason.TemplateCurrent.TemplateDescShort; }
        }

        public PlayerSeason ItemPlayerSeason
        {
            get { return _order.Items[_index].PlayerSeason; }
        }

        public int ItemQuantity
        {
            get { return _order.Items[_index].Quantity; }
        }

        public string ItemJerseyName
        {
            get { return _order.Items[_index].PlayerSeason.JerseyName; }
        }

        public string ItemJerseyNumber
        {
            get { return _order.Items[_index].PlayerSeason.JerseyNumber; }
        }

        public string ItemColor
        {
            get { return _order.Items[_index].Material.ToString(); }
        }

        public Material ItemMaterial
        {
            get { return _order.Items[_index].Material; }
        }

        public OrderDisplayable(Order order)
        {
            _order = order;
        }

        public OrderDisplayable(Order order, int itemIndex) : this(order)
        {
            _index = itemIndex;
        }
    }
}
