using DAL_CRUD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=SACALIVIN\\SQLEXPRESS;Initial Catalog=kcau_hostels_db;Integrated Security=True");
        //    }
        //    base.OnConfiguring(optionsBuilder);
        //}

        public DbSet<Hostel> Hostels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Armenity> Armenities { get; set;}  
        public DbSet<RentAlternative> RentAlternatives { get; set;}
        public DbSet<Book> Books { get; set; }

    }
}
