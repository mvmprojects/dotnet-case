using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using dotnet_case.BL.Models;

namespace dotnet_case.DATA.Repositories
{
    public interface ICaseRepository
    {
        //void AddArtist(ArtistModel artist);
        ArtistModel FindByName(string artistName);
        List<ArtistModel> GetArtists();
        Task<List<ArtistModel>> GetArtistsAsync();
        void DeleteArtist(ArtistModel artist);
        void UpdateArtist(ArtistModel artist);
        bool ArtistExists(long ArtistId);
        bool Save();
    }
}
