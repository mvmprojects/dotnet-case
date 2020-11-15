using AutoMapper;
using dotnet_case.DATA.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_case.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly ICaseRepository _repo;
        private readonly IMapper _mapper;

        public AlbumsController(ICaseRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS");
            return Ok();
        }

        [HttpGet]
        // consider changing controller route to force usage of parent Id in URI
        // api/artists/{artistId}/albums
        // even though this means we'll need to change the front-end project
        public IActionResult GetAlbumsForArtist(long artistId)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Create()
        {
            return StatusCode(501);
        }

        [HttpPut]
        public ActionResult Update()
        {
            return StatusCode(501);
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return StatusCode(501);
        }
    }
}
