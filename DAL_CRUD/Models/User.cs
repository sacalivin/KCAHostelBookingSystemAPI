﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CRUD.Models
{
    public class User : Person
    {
            
         [Required]
        public string? Compus { get; set; }
         [Required]
        public string? PhoneNumber { get; set; }
         [Required]
        public string? ParentFirstName { get; set; }
         [Required]
        public string? ParentLastName { get; set; }
         [Required]
        public string? ParentPhoneNumber { get; set; }
         
    }
}