using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CRUD.Models
{
    public class Hostel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required]
        public double RentalCost { get; set; }

        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }


        //public ICollection<Armenity>? Armenities { get; set; }
        public virtual ICollection<RentAlternative>? RentAlternatives { get; set; }
        public virtual ICollection<User>? Users { get; set; }
        public virtual ICollection<Book>? Books { get; set; }


    }
}
