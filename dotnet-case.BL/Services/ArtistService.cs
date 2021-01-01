using dotnet_case.DOMAIN.Models;
using dotnet_case.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_case.BL.Services
{
    public class ArtistService : IArtistService
    {
        // note: there currently isn't any business logic, so this BL layer doesn't do much.
        // it just demonstrates the principle of separating business logic from controllers.

        private readonly ICaseRepository _repo;

        public ArtistService(ICaseRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public List<ArtistModel> GetArtists()
        {
            return _repo.GetArtists();
        }

        public Task<List<ArtistModel>> GetArtistsAsync()
        {
            return _repo.GetArtistsAsync();
        }

        public ArtistModel FindArtistByName(string artistName)
        {
            return _repo.FindArtistByName(artistName);
        }
    }
}
