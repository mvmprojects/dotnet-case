using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using dotnet_case.BL.Models;

namespace dotnet_case.DATA.Repositories
{
    public interface ICaseRepository
    {
        // Artists

        //void AddArtist(ArtistModel artist);
        ArtistModel FindArtistByName(string artistName);
        List<ArtistModel> GetArtists();
        Task<List<ArtistModel>> GetArtistsAsync();
        void DeleteArtist(ArtistModel artist);
        void UpdateArtist(ArtistModel artist);
        bool ArtistExists(long artistId);

        // Albums

        List<AlbumModel> FindAlbumsByArtistId(long artistId);

        // Tracks

        void CreateTrack(TrackModel track);
        List<TrackModel> FindTracksByAlbumId(long albumId);
        void DeleteTrack(TrackModel track);
        void UpdateTrack(TrackModel track);

        bool Save();
    }
}
