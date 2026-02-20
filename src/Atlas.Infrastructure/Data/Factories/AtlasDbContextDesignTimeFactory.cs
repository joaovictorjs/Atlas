using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Atlas.Infrastructure.Data.Factories
{
    internal class AtlasDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AtlasDbContext>
    {
        public AtlasDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AtlasDbContext>();
            builder.UseNpgsql();
            var context = new AtlasDbContext(builder.Options);
            return context;
        }
    }
}
