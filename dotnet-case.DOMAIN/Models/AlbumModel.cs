using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_case.DOMAIN.Models
{
    public class AlbumModel
    {
        // initialize list of Tracks
        public AlbumModel()
        {
            Tracks = new List<TrackModel>();
        }
        [Key]
        public long AlbumId { get; set; }
        [Required]
        public string Name { get; set; }
        // collection navigation property
        public List<TrackModel> Tracks { get; set; }
        // reference navigation property
        public ArtistModel Artist { get; set; }
        // foreign key
        public long ArtistId { get; set; }
    }
}
