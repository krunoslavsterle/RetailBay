using System.ComponentModel.DataAnnotations;

namespace RetailBay.WebShop.Models.User
{
    public class NewAddressViewModel
    {
        [Required]
        public string ContactName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
