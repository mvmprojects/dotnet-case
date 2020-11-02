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

        // GET: api/Artists 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetArtists() {
            
            var data = await _context.Artists.ToListAsync();
            return _mapper.Map<List<ArtistDto>>(data);
        }
    }
}
