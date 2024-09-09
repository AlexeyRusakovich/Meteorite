using Meteorite.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Meteorite.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeteoriteController : ControllerBase
    {
        public MeteoriteController()
        {
        }

        [HttpGet]
        public IActionResult GetMeteoritesFiltered([FromQuery] MeteoriteFilter filter)
        {
            return Ok();
        }
    }
}
