using AutoMapper;
using dotnet_case.API.Dtos;
using dotnet_case.BL.Services;
using dotnet_case.DOMAIN.Models;
using dotnet_case.DATA.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_case.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        //private readonly ICaseRepository _repo;
        private readonly IArtistService _service;
        private readonly IMapper _mapper;

        public ArtistsController(IArtistService artistService, IMapper mapper)
        {
            _service = artistService ?? throw new ArgumentNullException(nameof(artistService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // basic GET: api/artists 
        [HttpGet]
        public IActionResult GetArtists() 
        {
            List<ArtistModel> data = _service.GetArtists();
            return Ok(_mapper.Map<List<ArtistDto>>(data));
        }

        // GET: api/artists/listwithcount (formerly api/artists/getlist)
        // Returns wrapper object that adds total list size as int (in its constructor).
        // Kevin Dockx recommends not to use verbs in your resource naming style.
        [HttpGet("listwithcount")]
        public async Task<IActionResult> GetListWithCountAsync()
        {
            Task<List<ArtistModel>> task = _service.GetArtistsAsync();
            // DONE: test to see if the mapper can actually tolerate a task instead of a list
            // UPDATE: not at all! don't put a Task straight into _mapper.Map()!
            List<ArtistModel> awaitedList = await task;
            List<ArtistDto> mappedList = _mapper.Map<List<ArtistDto>>(awaitedList);
            ArtistRequestWrapper wrapper = new ArtistRequestWrapper(mappedList);

            // optional alternative requires AutoMapper.QueryableExtensions
            // requires the ORM to expose IQueryable
            // https://docs.automapper.org/en/stable/Queryable-Extensions.html
            // var data = await _context.Artists.ProjectTo<ArtistDto>().ToListAsync();
            return Ok(wrapper);
        }

        // TODO see about replacing this with api/artists?filterby=name
        // Kevin Dockx recommends not to treat a content filter like a separate resource.
        // "byname" clearly isn't a resource and so should be passed via query string.
        // proper way of doing it involves a ResourceParams class that groups queries
        // so you don't have to do (string searchQuery, string extraQuery, (...)) etc.
        // checks will be needed in the repo method (or BL service method) like:
        // if (!string.IsNullOrWhiteSpace(artistsResourceParams.SearchQuery)) {...}

        // GET: api/artists/byname/{name} (should become api/artists?searchQuery=aName)
        // should get [FromQuery] ArtistsResourceParams resourceParams as only parameter
        [HttpGet("byname/{name}")]
        public IActionResult FindArtistByName(string name)
        {
            ArtistModel foundArtist = _service.FindArtistByName(name);
            return Ok(_mapper.Map<ArtistDto>(foundArtist));
        }

        [HttpOptions]
        public IActionResult GetArtistsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS");
            return Ok();
        }

        [HttpPost]
        public IActionResult Create() => StatusCode(501);

        [HttpPut]
        public ActionResult Update() => StatusCode(501);

        [HttpDelete]
        public ActionResult Delete() => StatusCode(501);
    }
}
