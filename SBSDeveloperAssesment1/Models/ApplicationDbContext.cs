using Microsoft.EntityFrameworkCore;
using System;

namespace SBSDeveloperAssesment1.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Person> People { get; set; }
        public DbSet<Info> Info { get; set; }
    }
}
