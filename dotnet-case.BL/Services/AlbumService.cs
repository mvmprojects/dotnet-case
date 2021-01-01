using dotnet_case.DATA.Repositories;
using dotnet_case.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_case.BL.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly ICaseRepository _repo;

        public AlbumService(ICaseRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public List<AlbumModel> FindAlbumsByArtistId(long artistId)
        {
            return _repo.FindAlbumsByArtistId(artistId);
        }
    }
}
