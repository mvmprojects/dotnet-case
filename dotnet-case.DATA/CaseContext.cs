﻿using dotnet_case.BL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace dotnet_case.DATA
{
    public class CaseContext : DbContext
    {
        public CaseContext(DbContextOptions<CaseContext> options)
           : base(options) {}
        public DbSet<ArtistModel> Artists { get; set; }
        public DbSet<AlbumModel> Albums { get; set; }
        public DbSet<TrackModel> Tracks { get; set; }
    }

    // when using a migration command, EF Tools will try to obtain the DbContext from the app's service provider.
    // however, the current .DATA project is a mere class library, and not an ASP.NET Core app. 
    // also, the connection string is kept in the .API project's appsettings.json file.

    // bypass normal tools behavior with the IDesignTimeDbContextFactory<TContext>
    // “If a class implementing this interface is found in either the same project as the derived DbContext 
    // or in the application’s startup project, the tools bypass the other ways of creating the DbContext and 
    // use the design-time factory instead.”

    // the following code needs:
    // -Microsoft.Extensions.Configuration.FileExtensions nuget package for SetBasePath
    // -Microsoft.Extensions.Configuration.Json nuget package for AddJsonFile
    // it then specifies the data provider as sql server and enters the conn. string as a parameter.
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CaseContext>
    {
        public CaseContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../dotnet-case.API/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<CaseContext>();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            builder.UseSqlServer(connectionString);
            return new CaseContext(builder.Options);
        }
    }
}
