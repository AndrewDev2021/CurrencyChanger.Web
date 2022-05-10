using CurrencyChangerWebProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace CurrencyChangerWebProject.Domain
{
    public class AppDbContext: DbContext
    {
        public DbSet<Registation> Users { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=CurrencyChangerDB;Username=postgres;Password=David25122001228");
        }
    }
}
