using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
namespace dotnetapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
            public DbSet<Feedback> Feedbacks { get; set; }

            public DbSet<User> Users { get; set; }

            public DbSet<Orphanage> Orphanages{ get; set; }

            public DbSet< Donation> Donations { get; set; }      
    }
}