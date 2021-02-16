﻿using AutoMapper;
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
        // consider changing this method to CreateTrackForAlbum, requiring an albumId
        public ActionResult<TrackDto> Create(TrackDto trackDto)
        {
            // note: param null check is unnecessary in modern ASP.NET 
            // as [ApiController] handles it, returning 400 Bad Request by itself

            TrackModel trackModel = _mapper.Map<TrackModel>(trackDto);

            _service.CreateTrack(trackModel);
            _service.Save();
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

            // UPDATE: Julie Lerman says the same thing. basically, EF Core will insert an 
            // object into the db and then immediately run a query to retrieve the ID that 
            // has just been generated by the db, and then use that to update the object 
            // in memory. just in case you want to do more work with it.
            
            // but both Lerman and Dockx are looking at a controller with closer access
            // to the db context. I'm looking at a BL service here. might want to dismantle
            // the BL service if it becomes an obstacle to using EF Core.
        }

        // patch might be a better option for tracks
        [HttpPut]
        public ActionResult<TrackDto> Update(TrackDto trackDto)
        {
            TrackModel trackModel = _mapper.Map<TrackModel>(trackDto);

            _service.UpdateTrack(trackModel);
            _service.Save();

            return NoContent(); // 204
        }

        [HttpDelete("{trackId}")]
        public ActionResult<TrackDto> Delete([FromRoute] long trackId) //(TrackDto trackDto)
        {
            Console.WriteLine("trackId is " + trackId);            
            //TrackModel trackModel = _mapper.Map<TrackModel>(trackDto);
            var trackModel = _service.GetTrack(trackId);
            Console.WriteLine("trackModel is " + trackModel.Name);

            if (trackModel != null)
            {
                _service.DeleteTrack(trackModel);
                _service.Save();

                return NoContent(); // 204
            } else return NotFound();
        }
    }
}
