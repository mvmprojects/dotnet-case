using dotnet_case.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_case.BL.Services
{
    public interface IArtistService
    {
        public List<ArtistModel> GetArtists();
        public Task<List<ArtistModel>> GetArtistsAsync();
        public ArtistModel FindArtistByName(string artistName);
    }
}
