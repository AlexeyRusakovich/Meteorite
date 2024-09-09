using Meteorite.Api.Interfaces;
using Meteorite.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Meteorite.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeteoriteController : ControllerBase
    {
        private readonly IMeteoriteRepository _meteoriteRepository;

        public MeteoriteController(IMeteoriteRepository meteoriteRepository)
        {
            _meteoriteRepository = meteoriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMeteoritesFiltered([FromQuery] MeteoriteFilter filter)
        {
            var result = await _meteoriteRepository.GetMeteoritesDataGrouped(filter);
            if (!result.Any())
                return NoContent();

            return Ok(result);
        }

        [HttpGet("dictionaries")]
        public async Task<IActionResult> GetMeteoritesDictionaries()
        {
            var result =  await _meteoriteRepository.GetMeteoritesDictionaries();
            return Ok(result);
        }
    }
}
