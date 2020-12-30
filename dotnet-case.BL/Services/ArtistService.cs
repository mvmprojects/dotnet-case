using dotnet_case.DOMAIN.Models;
using dotnet_case.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_case.BL.Services
{
    class ArtistService
    {
        // TODO update controller to use service layer instead of repository layer
        // note: this addition forced me to split up the solution into even more projects
        private readonly ICaseRepository _repo;

        public ArtistService(ICaseRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public ArtistModel FindArtistByName(string artistName)
        {
            return _repo.FindArtistByName(artistName);
        }
    }
}
