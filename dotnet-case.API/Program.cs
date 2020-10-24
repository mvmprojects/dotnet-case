using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using dotnet_case.DATA;
using dotnet_case.BL.Models;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_case.API
{
    public class Program
    {

        //private static CaseContext context = new CaseContext();

        public static void Main(string[] args)
        {
            // old template code
            //CreateHostBuilder(args).Build().Run();

            // build, but don't start yet
            var host = CreateHostBuilder(args).Build();

            // using statement for database
            //using (var scope = host.Services.CreateScope())
            //{
            //    try
            //    {
            //        var context = scope.ServiceProvider.GetService<CaseContext>();
            //        // for demo purposes, delete the database & migrate on startup
            //        //context.Database.EnsureDeleted();
            //        //context.Database.Migrate();
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(ex, "An error occurred while migrating the database.");
            //    }
            //}

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
