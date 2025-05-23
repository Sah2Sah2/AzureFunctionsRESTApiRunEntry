using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RESTApiAzureFunctions.Data
{
    public class RunDbContextFactory : IDesignTimeDbContextFactory<RunDbContext>
    {
        public RunDbContext CreateDbContext(string[] args)
        {
            // Build config to read from local.settings.json or environment variables
            IConfigurationRoot configuration = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.json", optional: true)
     .AddJsonFile("local.settings.json", optional: true)
     .AddEnvironmentVariables()
     .Build();

            var connectionString = configuration["SqlConnectionString"]
                ?? configuration["Values:SqlConnectionString"];

            Console.WriteLine($"Using connection string: {connectionString}"); // Debug

            var optionsBuilder = new DbContextOptionsBuilder<RunDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new RunDbContext(optionsBuilder.Options);
        }
    }
}
