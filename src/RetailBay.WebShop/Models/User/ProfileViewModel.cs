using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RetailBay.WebShop.Models.User
{
    public class ProfileViewModel
    {
        [DisplayName("User Name*")]
        [Required]
        public string UserName { get; set; }

        [DisplayName("Email*")]
        [Required]
        public string Email { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }
}
