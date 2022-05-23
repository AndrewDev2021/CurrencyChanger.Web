using CurrencyExсhanger.Web.Model;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExсhanger.Web.Domain
{
    public sealed class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ExchangeHistory> ExchangeHistories { get; set; }
        public DbSet<ContactUs> ContactUsMessages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreatedAsync();
        }

        /*If you delete migrations , uncomment the function "OnModelCreating" and call
         "Add-migration [name]" and comment out back, then use "Update-database"*/


        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var adminRole = new Role()
            {
                Id = 1,
                Name = "admin"
            };

            var userRole = new Role()
            {
                Id = 2,
                Name = "user"
            };

            var admin = new User()
            {
                Id = 1,
                FirstName = "Bill",
                LastName = "Tomson",
                Email = "Example@gmail.com",
                Age = 33,
                Password = HashingService.GetHashString("12345Qwerty"),
                RoleId = adminRole.Id
            };

            modelBuilder.Entity<Role>()
                .HasData(new Role[] { adminRole, userRole });

            modelBuilder.Entity<User>()
                .HasData(admin);
        }*/
    }
}
