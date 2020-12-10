using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_case.API.Dtos
{
    public class TrackDto
    {
        public long TrackId { get; set; }
        public string Name { get; set; }
        //public long AlbumId { get; set; }
        //public string AlbumName { get; set; }
        //public long ArtistId { get; set; }
        //public string ArtistName { get; set; }
        //public int Duration { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
    }
}
