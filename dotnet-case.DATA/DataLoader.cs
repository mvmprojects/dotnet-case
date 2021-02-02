using dotnet_case.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotnet_case.DATA
{
    public class DataLoader
    {
        public static void Initialize(CaseContext context)
        {
            if (!context.Artists.Any())
            {
                List<ArtistModel> artists = new List<ArtistModel>()
                {
                    new ArtistModel { Name = "Michael Jackson" },
                    new ArtistModel { Name = "Jimi Hendrix" },
                    new ArtistModel { Name = "Beyonce" }
                };

                context.Artists.AddRange(artists);

                // needs more stuff

                List<AlbumModel> albums = new List<AlbumModel>();

                foreach (var artist in artists)
                {
                    List<AlbumModel> albumsWithArtists = new List<AlbumModel> { 
                        new AlbumModel { Name = "Album A", Artist = artist },
                        new AlbumModel { Name = "Album B", Artist = artist }
                    };
                    albums.AddRange(albumsWithArtists);
                }

                context.Albums.AddRange(albums);

                List<TrackModel> tracks = new List<TrackModel>();

                foreach (var album in albums)
                {
                    List<TrackModel> trackModels = new List<TrackModel>
                    {
                        new TrackModel { Name = "Track A", Album = album, Duration = 300000 },
                        new TrackModel { Name = "Track B", Album = album, Duration = 360000 },
                        new TrackModel { Name = "Track C", Album = album, Duration = 270000 }
                    };
                    tracks.AddRange(trackModels);
                }

                context.Tracks.AddRange(tracks);

                context.SaveChanges();
            }
        }
    }
}
