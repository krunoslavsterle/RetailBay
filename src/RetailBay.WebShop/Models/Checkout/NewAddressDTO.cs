using System.ComponentModel.DataAnnotations;

namespace RetailBay.WebShop.Models.Checkout
{
    public class NewAddressDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

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
