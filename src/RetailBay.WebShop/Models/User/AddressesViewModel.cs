using System;
using System.Collections.Generic;

namespace RetailBay.WebShop.Models.User
{
    public class AddressesViewModel
    {
        public IEnumerable<AddressDTO> ShippingAddresses { get; set; }
        public IEnumerable<AddressDTO> BillingAddresses { get; set; }

        public struct AddressDTO
        {
            public Guid Id { get; set; }
            public string ContactName { get; set; }
            public string Phone { get; set; }
            public string StreetAddress { get; set; }
            public string PostalCode { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
        }
    }
}
