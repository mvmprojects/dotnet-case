using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_case.API.Dtos
{
    public class ArtistRequestWrapper
    {
        public List<ArtistDto> Results { get; set; }
        public int Total { get; set; }

        public ArtistRequestWrapper(List<ArtistDto> artistDtos)
        {
            this.Results = artistDtos;
            this.Total = artistDtos.Count();
        }
    }
}
