using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_case.API.Dtos
{
    public class ArtistDto
    {
        // Kevin Dockx vouches for an extra DTO for creation purposes where 
        // the ID has been stripped away. This is because the client does 
        // not decide the id value anyway, so the incoming Post request's DTO
        // is inefficient because it always has one empty property.
        // I could add an ArtistCreationDto in the future.
        public long ArtistId { get; set; }
        public string Name { get; set; }
    }
}
