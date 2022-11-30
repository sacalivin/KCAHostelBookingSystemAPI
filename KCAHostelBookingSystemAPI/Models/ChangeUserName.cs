using System.ComponentModel.DataAnnotations;

namespace KCAHostelBookingSystemAPI.Models
{
    public class ChangeUserName
    {
        //[Required(ErrorMessage = "Current Email Required")]
        public string OldEmail { get; set; }
        [Required(ErrorMessage = "New Email Required")]
        public string NewEmail { get; set; }
        //[Required(ErrorMessage = "Token Required")]
        //public string Token { get; set; }
    }
}
