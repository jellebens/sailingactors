using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SailingActors.Api.Controllers
{
    [Route("health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("live")]
        public IActionResult live()
        {
            return Ok();
        }

        [HttpGet("ready")]
        public IActionResult ready()
        {
            return Ok();
        }
    }
}
