using dotnet_case.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotnet_case.DATA
{
    public class DataLoader
    {
        // otherwise known as a "Data Seeder"

        public static void Initialize(CaseContext context)
        {
            if (!context.Artists.Any())
            {
                List<ArtistModel> artists = new List<ArtistModel>()
                {
                    new ArtistModel { Name = "Michael Jackson" },
                    new ArtistModel { Name = "Jimi Hendrix" }
                };

                context.Artists.AddRange(artists);
                context.SaveChanges();
            }
        }
    }
}
