using System.ComponentModel.DataAnnotations;
namespace KCAHostelBookingSystemAPI.Models
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Username Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Current Password Required")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "New password Required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm new password Required")]
        public string ConfirmPassword { get; set;}
    }
    public class ForgotPassword
    {

        [Required(ErrorMessage = "New password Required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm new password Required")]
        public string ConfirmPassword { get; set; }
    }
}
