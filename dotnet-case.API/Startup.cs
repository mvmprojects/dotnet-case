using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_case.DATA;
using dotnet_case.DATA.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace dotnet_case.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // old template code is just this line
            //services.AddControllers();

            services.AddControllers(config =>
            {
                config.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters(); // Offer xml to clients asking for xml.
            // The reason the example uses this formatter type, XmlDataContractSerializer, instead
            // of just XmlSerializer, is so it can be used with types like DateTimeOffset. Most 
            // types in .NET and Core were not designed with the XmlSerializer in mind.

            // automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // repositories
            services.AddScoped<ICaseRepository, CaseRepository>();

            services.AddDbContext<CaseContext>
            //(options => { options.UseSqlServer(
            // @"Server=(localdb)\mssqllocaldb;Database=dotnet-caseDATA;Trusted_Connection=True;"); });

            // Connection string moved to appsettings.json
            (options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            // JSON options to avoid reference loops
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
            //{
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
