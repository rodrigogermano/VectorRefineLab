using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace VectorRefineLab.Console.Infrastructure.Data;

public sealed class VectorRefineDbContextFactory : IDesignTimeDbContextFactory<VectorRefineDbContext>
{
    public VectorRefineDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var connectionString = configuration.GetConnectionString("SqlServer");

        var optionsBuilder = new DbContextOptionsBuilder<VectorRefineDbContext>();

        optionsBuilder.UseSqlServer(connectionString);

        return new VectorRefineDbContext(optionsBuilder.Options);
    }
}