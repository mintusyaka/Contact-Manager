using Microsoft.EntityFrameworkCore;
using Contact_Manager.Models;

namespace Contact_Manager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(
				new Contact { 
                    Id = 1,
                    Phone = "0938265605",
                    Operator = OperatorType.Kyivstar,
                    Description = ""
                }
				);

            modelBuilder.Entity<User>()
                .HasOne(e => e.Contact)
                .WithOne(e => e.User)
                .HasForeignKey<Contact>(e => e.UserId)
                .IsRequired();
		}

    }
}
