
namespace ExamSystem.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;

    namespace ExamSystem.Infrastructure.Data
    {
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                // Find the project directory
                string projectDir = Directory.GetCurrentDirectory();

                // Handle the case where the current directory is not the project directory
                string configPath = Path.Combine(projectDir, "appsettings.json");
                if (!File.Exists(configPath))
                {
                    // Try moving up to the solution directory and then to the Web/API project
                    DirectoryInfo directory = new DirectoryInfo(projectDir);
                    while (directory != null && !File.Exists(Path.Combine(directory.FullName, "appsettings.json")))
                    {
                        directory = directory.Parent;
                    }

                    if (directory != null)
                    {
                        // Try to find the API/Web project that contains appsettings.json
                        foreach (var dir in Directory.GetDirectories(directory.FullName))
                        {
                            if (File.Exists(Path.Combine(dir, "appsettings.json")))
                            {
                                projectDir = dir;
                                break;
                            }
                        }
                    }
                }

                // Build configuration
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(projectDir)
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
                    .Build();

                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

                // Hard-code the connection string as a fallback if not found in configuration
                string connectionString = configuration.GetConnectionString("DefaultConnection")
                    ?? "Server=(localdb)\\mssqllocaldb;Database=ExamSystem;Trusted_Connection=True;MultipleActiveResultSets=true";

                optionsBuilder.UseSqlServer(connectionString);

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }
    }
}
