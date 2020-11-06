using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_case.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS");
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
