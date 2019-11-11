using RetailBay.WebShop.Models.Cart;
using System;
using System.Collections.Generic;

namespace RetailBay.WebShop.Models.Checkout
{
    public class IndexViewModel
    {
        public Guid SelectedAddressId { get; set; }
        public decimal ShippingPrice { get; set; }
        public AddressDTO ShippingAddress { get; set; }
        public IEnumerable<CartItemDTO> CartItems { get; set; }
    }
}
