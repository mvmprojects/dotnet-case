using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_case.DOMAIN.Models
{
    public class ArtistModel
    {
        // initialize list of Albums
        public ArtistModel()
        {
            Albums = new List<AlbumModel>();
        }
        [Key]
        public long ArtistId { get; set; }
        [Required]
        public string Name { get; set; }
        // collection navigation property
        public List<AlbumModel> Albums { get; set; }
    }
}
