using AutoMapper;
using dotnet_case.API.Dtos;
using dotnet_case.BL.Models;
using dotnet_case.DATA.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_case.API.Controllers
{
    [Route("api/[controller]")]
    // TODO look into replacing this route with "api/albums/{albumId}/tracks"
    // or even "api/artists/{artistId}/albums/{albumId}/tracks"
    // this will allow for the use of smaller Dto objects because the parent Id
    // is already in the URI, and so doesn't need to be added to the body of
    // every item in a list of child objects.
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly ICaseRepository _repo;
        private readonly IMapper _mapper;

        public TracksController(ICaseRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpOptions]
        public IActionResult GetTracksOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST,UPDATE,DELETE");
            return Ok();
        }

        // not likely to be used unless we change the controller's route.
        // possibly to "api/albums/{albumId}/tracks"
        [HttpGet(Name = "GetTracks")]
        public IActionResult GetTracks()
        {
            return StatusCode(501);
        }

        // route deviates from suggested resource naming guidelines
        [HttpGet("byalbumid/{albumId}", Name = "FindTracksByAlbumId")]
        public IActionResult FindTracksByAlbumId(long albumId)
        {
            List<TrackModel> foundTracks = _repo.FindTracksByAlbumId(albumId);
            List<TrackDto> trackDtos = _mapper.Map<List<TrackDto>>(foundTracks);
            return Ok(trackDtos);
        }

        [HttpPost]
        // consider creating a slimmed down TrackForCreationDto object
        // consider changing this method to CreateTrackForAlbum, requiring an albumId
        public IActionResult Create(TrackDto trackDto)
        {
            // note: param null check is unnecessary in modern ASP.NET 
            // as [ApiController] handles it, returning 400 Bad Request by itself

            TrackModel trackModel = _mapper.Map<TrackModel>(trackDto);

            _repo.CreateTrack(trackModel);
            _repo.Save();
            var trackReturned = _mapper.Map<TrackDto>(trackModel);
            return CreatedAtRoute("FindTracksByAlbumId", trackReturned);

            //// note: Kevin Dockx uses a void method for creation, repo.AddAuthor
            //// followed by a repo.Save() right in the controller.
            //// At this point Dockx insists that the Id will be filled out in the 
            //// local variable. 
            //// But his demo app adds a Guid Id right in the repository method. I'm not 
            //// sure if this will work when I leave it to the database to decide the Id.
            //// Either way, he wraps things up with CreateAtRoute() which would look
            //// something like this when there is a TrackId available:
            //return CreatedAtRoute("GetTrack",
            //    new { trackId = trackReturned.TrackId },
            //    trackReturned);
            //// finally, add (Name = "GetTrack") in [HttpGet]
        }

        // patch might be a better option for tracks
        [HttpPut]
        public ActionResult Update()
        {
            // possible outcome:
            //return NoContent(); // 204
            return StatusCode(501);
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            // possible outcome:
            //return NoContent(); // 204
            return StatusCode(501);
        }
    }
}
