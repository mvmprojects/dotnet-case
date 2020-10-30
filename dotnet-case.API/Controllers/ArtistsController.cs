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
        // TODO replace with ICaseRepository field
        private readonly CaseContext _context;
        // TODO add automapper

        public ArtistsController(CaseContext context)
        {
            _context = context ?? throw new ArgumentNullException();
            // TODO add mapper parameter
        }

        // GET: api/Artists 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistModel>>> GetArtists() {
            return await _context.Artists.ToListAsync();
        }
    }
}
