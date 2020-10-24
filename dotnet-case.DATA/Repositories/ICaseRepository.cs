using System;
using System.Collections.Generic;
using System.Text;
using dotnet_case.BL.Models;

namespace dotnet_case.DATA.Repositories
{
    interface ICaseRepository
    {
        //void AddArtist(ArtistModel artist);
        void DeleteArtist(ArtistModel artist);
        void UpdateArtist(ArtistModel artist);
        bool ArtistExists(long ArtistId);
        bool Save();
    }
}
