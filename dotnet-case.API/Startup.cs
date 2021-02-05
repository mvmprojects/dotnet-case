using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_case.BL.Services;
using dotnet_case.DATA;
using dotnet_case.DATA.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        readonly string SpecificOrigins = "_allowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy(name: SpecificOrigins,
                  policyBuilder =>
                  {
                      // testing with separate angular project
                      policyBuilder.WithOrigins("http://localhost:4200");
                  });
            });

            services.AddControllers(config =>
            {
                // if the following is 'false' (it is by default) then the api will return a default 
                // format if an unsupported media type is requested by a client. Kevin Dockx argues 
                // that it's better to turn down a request rather than lazily send json to a client 
                // that doesn't even want json. so we set this to true, to send 406 Not Acceptable.
                config.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters(); // Offer xml to clients asking for xml.
            // The reason the example uses this formatter type, XmlDataContractSerializer, instead
            // of just XmlSerializer, is so it can be used with types like DateTimeOffset. Most 
            // types in .NET and Core were not designed with the XmlSerializer in mind.
            // note that json remains the default format, and xml is only added as an option.

            // AddAutoMapper wants to know where you placed your Profiles, so if you ever move
            // the Profiles folder to another project then this line will fail.
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // repository and services
            services.AddScoped<ICaseRepository, CaseRepository>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<ITrackService, TrackService>();

            services.AddDbContext<CaseContext>
            // Connection string moved to appsettings.json 
            // Added rule to show real parameters in commands in logging.
            // For security reasons, do not use .EnableSensitiveDataLogging in a real application.
            (options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection"))
                .EnableSensitiveDataLogging()
                );

            // JSON options to help ignore reference loops?

            // seems you have to install a nuget package for the old JSON serializer used in 
            // older Core versions:
            // Microsoft.AspNetCore.Mvc.NewtonsoftJson
            // then do something like this:
            //services.AddControllers().AddNewtonsoftJson(o =>
            //{
            //    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(SpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
