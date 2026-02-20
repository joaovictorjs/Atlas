using Microsoft.EntityFrameworkCore;

namespace Atlas.Infrastructure.Data
{
    public class AtlasDbContext(DbContextOptions<AtlasDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AtlasDbContext).Assembly);
        }
    }
}
