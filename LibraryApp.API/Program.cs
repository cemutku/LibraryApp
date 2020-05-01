using LibraryApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace LibraryApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Info("Initializing application...");
                var host = CreateHostBuilder(args).Build();

                // migrate database
                logger.Info("Initializing migration...");
                using (var scope = host.Services.CreateScope())
                {
                    try
                    {
                        var context = scope.ServiceProvider.GetService<LibraryAppDbContext>();
                        context.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "An error occured while migrating the database");
                    }
                }

                // run app
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Application stopped due to exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog();
    }
}
