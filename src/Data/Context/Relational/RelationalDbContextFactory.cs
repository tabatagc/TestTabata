using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CallCenterAgentManager.Data.Context.Relational
{
    public class RelationalDbContextFactory : IDesignTimeDbContextFactory<RelationalDbContext>
    {
        public RelationalDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RelationalDbContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("PostgreSqlConnection");

            optionsBuilder.UseNpgsql(connectionString);

            return new RelationalDbContext(optionsBuilder.Options);
        }
    }
}
