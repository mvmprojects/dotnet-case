using AutoMapper;
using dotnet_case.API.Dtos;
using dotnet_case.BL.Models;
using dotnet_case.DATA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_case.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        // TODO replace context field with ICaseRepository field
        private readonly CaseContext _context;
        private readonly IMapper _mapper;

        public ArtistsController(CaseContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // test GET: api/Artists 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetArtists() {
            
            var data = await _context.Artists.ToListAsync();
            return _mapper.Map<List<ArtistDto>>(data);

            // todo test me - requires AutoMapper.QueryableExtensions
            // requires the ORM to expose IQueryable
            // https://docs.automapper.org/en/stable/Queryable-Extensions.html
            // var data = await _context.Artists.ProjectTo<ArtistDto>().ToListAsync();
        }

        // TODO: GET for a wrapper object that adds total list size as int (in its constructor)
        // forward slash in the route template is not required
        [HttpGet("getlist")]
        public IActionResult GetList()
        {
            List<ArtistModel> artistList = _context.Artists.ToList();
            List<ArtistDto> mappedList = _mapper.Map<List<ArtistDto>>(artistList);
            ArtistRequestWrapper wrapper = new ArtistRequestWrapper(mappedList);
            return Ok(wrapper);
        }

        [HttpPost]
        public ActionResult Create()
        {
            return StatusCode(501);
        }

        public ActionResult Update()
        {
            return StatusCode(501);
        }

        public ActionResult Delete()
        {
            return StatusCode(501);
        }
    }
}
