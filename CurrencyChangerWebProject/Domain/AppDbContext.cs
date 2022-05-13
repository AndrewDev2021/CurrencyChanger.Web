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
    }
}
