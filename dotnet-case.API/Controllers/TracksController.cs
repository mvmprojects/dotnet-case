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
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST,UPDATE,DELETE");
            return Ok();
        }

        [HttpPost]
        public IActionResult Create()
        {
            return StatusCode(501);
        }

        public IActionResult Update()
        {
            return StatusCode(501);
        }

        public IActionResult Delete()
        {
            return StatusCode(501);
        }
    }
}
