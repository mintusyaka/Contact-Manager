using Microsoft.EntityFrameworkCore;
using Contact_Manager.Models;

namespace Contact_Manager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(
				new Contact { 
                    Id = 1,
                    Name = "Steven",
                    Birthday = new DateTime(2005, 10, 1),
                    Married = false,
                    Phone = "0938265605",
                    Salary = 1000.0M
                }
				);
		}

    }
}
