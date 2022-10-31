using System.ComponentModel.DataAnnotations;

namespace KCAHostelBookingSystemAPI.Auth
{
    public class Register
    {

            [Required(ErrorMessage = "First Name is required")]
            public string? Firstname { get; set; }

            [Required(ErrorMessage = "last Name is required")]
            public string? Lastname { get; set; } 

            [EmailAddress]
            [Required(ErrorMessage = "Email is required")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "Password is required")]
            public string? Password { get; set; }
        [Required(ErrorMessage = "HostelId is required")]
        public int HostelId { get;  set; }
    }
}
