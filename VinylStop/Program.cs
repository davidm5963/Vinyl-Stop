using System;
using VinylStop.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace VinylStop
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                // Retrieve your DbContext isntance
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                    var serviceProvider = services.GetRequiredService<IServiceProvider>();
                    var configuration = services.GetRequiredService<IConfiguration>();
                    DbInitializer.CreateRoles(serviceProvider, configuration).Wait();
                    DbInitializer.SeedData(dbContext);

                }
                catch (Exception exception)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(exception, "An error occurred while creating roles");
                }
            }
            host.Run();
        }


            public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
