using CurrencyExсhanger.Web.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace CurrencyExсhanger.Web.Domain
{
    public class AppDbContext: DbContext
    {
        public DbSet<Registation> Users { get; set; }
        public DbSet<ExchangeHistory> ExchangeHistories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreatedAsync();
        }
    }
}
