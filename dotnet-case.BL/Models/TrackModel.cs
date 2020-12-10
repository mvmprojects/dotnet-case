using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_case.BL.Models
{
    public class TrackModel
    {
        [Key]
        public long TrackId { get; set; }
        [Required]
        public string Name { get; set; }
        // note: milliseconds
        public int Duration { get; set; }
        // reference navigation property
        public AlbumModel Album { get; set; }
        // foreign key
        public long AlbumId { get; set; }
    }
}
