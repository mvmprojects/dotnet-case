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
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly ICaseRepository _repo;
        private readonly IMapper _mapper;

        public ArtistsController(ICaseRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // basic GET: api/artists 
        [HttpGet]
        public IActionResult GetArtists() 
        {
            List<ArtistModel> data = _repo.GetArtists();
            return Ok(_mapper.Map<List<ArtistDto>>(data));
        }

        // GET: api/artists/listwithcount (formerly api/artists/getlist)
        // Returns wrapper object that adds total list size as int (in its constructor).
        // Kevin Dockx recommends not to use verbs in your resource naming style.
        [HttpGet("listwithcount")]
        public async Task<IActionResult> GetListWithCountAsync()
        {
            Task<List<ArtistModel>> task = _repo.GetArtistsAsync();
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
        // GET: api/artists/byname/{name} (should become api/artists?filterby=name)
        [HttpGet("byname/{name}")]
        public IActionResult FindAuthorByName(string name)
        {
            ArtistModel foundArtist = _repo.FindArtistByName(name);
            return Ok(_mapper.Map<ArtistDto>(foundArtist));
        }

        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS");
            return Ok();
        }

        [HttpPost]
        public IActionResult Create() => StatusCode(501);

        public IActionResult Update() => StatusCode(501);

        public IActionResult Delete() => StatusCode(501);
    }
}
