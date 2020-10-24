using dotnet_case.BL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace dotnet_case.DATA
{
    public class CaseContext : DbContext
    {
        public CaseContext(DbContextOptions<CaseContext> options)
           : base(options)
        {
        }

        public DbSet<ArtistModel> Artists { get; set; }
        public DbSet<AlbumModel> Albums { get; set; }
        public DbSet<TrackModel> Tracks { get; set; }

        //// According to Julie Lerman, EF Core expects you to explicitly specify 
        //// a data provider and connection string. She adds it here in the context class.
        //// However, she's not plugging EF into a web API. Her tutorial = just a console app.

        //// Override virtual method OnConfiguring...
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Access UseSqlServer, which expects a connection string.
        //    // UseSqlServer is available because we installed 
        //    // Microsoft.EntityFrameworkCore.SqlServer
        //    optionsBuilder.UseSqlServer(
        //        "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = dotnet-caseDATA");
        //    //base.OnConfiguring(optionsBuilder); // apparently not needed
        //}
    }
}
