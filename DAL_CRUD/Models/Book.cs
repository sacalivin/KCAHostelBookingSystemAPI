using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CRUD.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Request { get; set; }

        [Required]
        public string? ModeOfPayment { get; set; }
        [Required]
        public DateOnly CheckinDate { get; set; }
        [Required]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        // 
        [Required]
        public int? RentAlternativeId { get; set; }
        [Required]
        public int HostelId { get; set; }



    }
}
