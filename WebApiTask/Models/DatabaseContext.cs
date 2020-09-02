using Microsoft.EntityFrameworkCore;

namespace WebApiTask.Models
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<VersionModel> Versions { get; set; }
        public DbSet<BuildingModel> Buildings { get; set; }
    }
}