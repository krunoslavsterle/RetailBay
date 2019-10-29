using System;
using System.Collections.Generic;
using RetailBay.Core.Entities.Identity;
using RetailBay.WebShop.Models.Cart;

namespace RetailBay.WebShop.Models.Checkout
{
    public class IndexViewModel
    {
        public Guid SelectedAddressId { get; set; }
        public decimal ShippingPrice { get; set; }
        public IEnumerable<Address> ShippingAddresses { get; set; }
        public IEnumerable<CartItemDTO> CartItems { get; set; }
    }
}
