using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using dotnet_case.DATA;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace dotnet_case.API
{
    public class Program
    {

        //private static CaseContext context = new CaseContext();

        public static void Main(string[] args)
        {
            // old template code is just this line
            //CreateHostBuilder(args).Build().Run();

            // build, but don't start yet
            var host = CreateHostBuilder(args).Build();

            using (IServiceScope scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetService<CaseContext>();
                    // for demo purposes, delete the database & migrate on startup
                    context.Database.EnsureDeleted();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }

                try
                {
                    var context = services.GetRequiredService<CaseContext>();
                    DataLoader.Initialize(context);
                }
                catch (Exception)
                {

                    Console.WriteLine("An error occurred while seeding the database.");
                }
            }

            // actually run the web app now
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
