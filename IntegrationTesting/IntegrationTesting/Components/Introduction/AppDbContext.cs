using Microsoft.EntityFrameworkCore;

namespace IntegrationTesting.Components.Introduction
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
    }
}