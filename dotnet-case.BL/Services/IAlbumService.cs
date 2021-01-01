using dotnet_case.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_case.BL.Services
{
    public interface IAlbumService
    {
        public List<AlbumModel> FindAlbumsByArtistId(long artistId);
    }
}
