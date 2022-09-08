using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CRUD.Models
{
    public class Admin : Person
    {
        public string? Role { get; set; }
    }
}
