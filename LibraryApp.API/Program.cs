using LibraryApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace LibraryApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // migrae database
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetService<LibraryAppDbContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {

                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured while migrating the database");
                }
            }

            // run app
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
