using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_case.API.Dtos
{
    public class AlbumDto
    {
        public long AlbumId { get; set; }
        public string Name { get; set; }
        public long ArtistId { get; set; }
        public string ArtistName { get; set; }
    }
}
