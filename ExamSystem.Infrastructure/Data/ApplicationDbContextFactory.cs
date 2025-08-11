
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
               
                string projectDir = Directory.GetCurrentDirectory();

                
                string configPath = Path.Combine(projectDir, "appsettings.json");
                if (!File.Exists(configPath))
                {
                    
                    DirectoryInfo directory = new DirectoryInfo(projectDir);
                    while (directory != null && !File.Exists(Path.Combine(directory.FullName, "appsettings.json")))
                    {
                        directory = directory.Parent;
                    }

                    if (directory != null)
                    {
                        
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

               
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(projectDir)
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
                    .Build();

                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

                
                string connectionString = configuration.GetConnectionString("DefaultConnection")
                    ?? "Server=(localdb)\\mssqllocaldb;Database=ExamSystem;Trusted_Connection=True;MultipleActiveResultSets=true";

                optionsBuilder.UseSqlServer(connectionString);

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }
    }
}
