using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Comp586GroupProject.Data
{
    // dotnet ef: run from the project folder so appsettings.json is found; uses ConnectionStrings:MedicalDb.
    public sealed class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var connectionString =
                configuration.GetConnectionString("MedicalDb")
                ?? configuration.GetConnectionString("medicalmanagement")
                ?? Environment.GetEnvironmentVariable("ConnectionStrings__MedicalDb");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Set ConnectionStrings:MedicalDb in appsettings.json in this project directory, " +
                    "or set environment variable ConnectionStrings__MedicalDb, then run dotnet ef from that folder.");
            }

            var serverVersionString = configuration["Database:ServerVersion"] ?? "9.1.0-mysql";
            var serverVersion = ServerVersion.Parse(serverVersionString);

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseMySql(connectionString, serverVersion);

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
