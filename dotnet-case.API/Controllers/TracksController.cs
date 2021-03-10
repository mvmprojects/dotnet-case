using AutoMapper;
using dotnet_case.API.Dtos;
using dotnet_case.DOMAIN.Models;
using dotnet_case.DATA.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_case.BL.Services;

namespace dotnet_case.API.Controllers
{
    [Route("api/albums/{albumId}/tracks")]
    //[Route("api/[controller]")]
    // DONE look into replacing the route with "api/albums/{albumId}/tracks"
    // or even "api/artists/{artistId}/albums/{albumId}/tracks"
    // this will allow for the use of smaller Dto objects because the parent Id
    // is already in the URI, and so doesn't need to be added to the body of
    // every item in a list of child objects.
    [ApiController]
    public class TracksController : ControllerBase
    {
        //private readonly ICaseRepository _repo;
        private readonly ITrackService _service;
        private readonly IMapper _mapper;

        public TracksController(ITrackService service, IMapper mapper)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpOptions]
        public IActionResult GetTracksOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST,UPDATE,DELETE");
            return Ok();
        }

        [HttpGet(Name = "FindTracksByAlbumId")]
        public ActionResult<IEnumerable<TrackDto>> FindTracksByAlbumId(long albumId)
        {
            List<TrackModel> foundTracks = _service.FindTracksByAlbumId(albumId);
            List<TrackDto> trackDtos = _mapper.Map<List<TrackDto>>(foundTracks);
            return Ok(trackDtos);
        }

        [HttpPost]
        // consider creating a slimmed down TrackForCreationDto object
        public ActionResult<TrackDto> Create(TrackDto trackDto, [FromRoute] long albumId)
        {
            // note: param null check is unnecessary in modern ASP.NET 
            // as [ApiController] handles it, returning 400 Bad Request by itself

            TrackModel trackModel = _mapper.Map<TrackModel>(trackDto);

            trackModel.AlbumId = albumId;

            _service.CreateTrack(trackModel);
            _service.Save();
            var trackReturned = _mapper.Map<TrackDto>(trackModel);
            return CreatedAtAction("Create", trackReturned); // 201

            //// note: Kevin Dockx uses a void method for creation, repo.AddAuthor
            //// followed by a repo.Save() right in the controller, and uses CreatedAtRoute
            //return CreatedAtRoute("GetTrack",
            //    new { trackId = trackReturned.TrackId },
            //    trackReturned);
            //// finally, add (Name = "GetTrack") in [HttpGet]
        }

        [HttpPut("{trackId}")]
        public ActionResult<TrackDto> Update(TrackDto trackDto, [FromRoute] long albumId)
        {
            TrackModel trackModel = _mapper.Map<TrackModel>(trackDto);

            trackModel.AlbumId = albumId;

            _service.UpdateTrack(trackModel);
            _service.Save();

            return NoContent(); // 204
        }

        [HttpDelete("{trackId}")]
        public ActionResult<TrackDto> Delete([FromRoute] long trackId)
        {
            var trackModel = _service.GetTrack(trackId);

            if (trackModel != null)
            {
                _service.DeleteTrack(trackModel);
                _service.Save();

                return NoContent(); // 204
            } else return NotFound();
        }
    }
}
