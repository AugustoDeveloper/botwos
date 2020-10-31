using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Botwos.Weather.Infrastructure.Persistence
{
    public class DbResponsesContextFactory : IDesignTimeDbContextFactory<DbResponsesContext>
    {
        public DbResponsesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbResponsesContext>();

            optionsBuilder.UseNpgsql(string.Empty);
            return new DbResponsesContext(optionsBuilder.Options);
        }
    }
}