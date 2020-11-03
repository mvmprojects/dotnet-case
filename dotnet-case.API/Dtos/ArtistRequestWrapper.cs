using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_case.API.Dtos
{
    public class ArtistRequestWrapper
    {
        public List<ArtistDto> results { get; set; }
        public int total { get; set; }

        public ArtistRequestWrapper(List<ArtistDto> artistDtos)
        {
            this.results = artistDtos;
            this.total = artistDtos.Count();
        }
    }
}
