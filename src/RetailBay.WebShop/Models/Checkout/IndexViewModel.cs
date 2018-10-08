using System;
using System.Collections.Generic;
using RetailBay.Core.Entities.Identity;

namespace RetailBay.WebShop.Models.Checkout
{
    public class IndexViewModel
    {
        public Guid SelectedAddressId { get; set; }
        public IEnumerable<Address> ShippingAddresses { get; set; }
    }
}
