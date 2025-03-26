using Business.Services.PlanetServices;
using Data.DTOs.PlanetDtos;
using Microsoft.AspNetCore.Mvc;

namespace UserMicroservice.Controllers
{
        [Route("api/[controller]/[action]")]
        [ApiController]
        public class PlanetController : ControllerBase
        {
            private readonly IPlanetService _planetService;

            public PlanetController(IPlanetService planetService)
            {
                _planetService = planetService;
            }


            [HttpGet]
            public async Task<ActionResult<IEnumerable<PlanetDto>>> GetAllPlanets()
            {
                var planets = await _planetService.GetAllPlanetsAsync();
                return Ok(planets);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<PlanetDto>> GetPlanet(int id)
            {
                var planet = await _planetService.GetPlanetAsync(id);
                if (planet == null)
                {
                    return NotFound();
                }

                return Ok(planet);
            }

            [HttpPost]
            public async Task<ActionResult<PlanetDto>> CreatePlanet([FromBody] PlanetCreateDto planetCreateDto)
            {
                var createdPlanet = await _planetService.CreatePlanetAsync(planetCreateDto);
                return CreatedAtAction(nameof(GetPlanet), new { id = createdPlanet.Id }, createdPlanet);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdatePlanet(int id, [FromBody] PlanetDto planetDto)
            {
                if (id != planetDto.Id)
                {
                    return BadRequest("ID mismatch");
                }

                try
                {
                    var updatedPlanet = await _planetService.UpdatePlanetAsync(id, planetDto);
                    return Ok(updatedPlanet);
                }
                catch (Exception ex)
                {
                    // Handle exceptions, e.g., author not found
                    return NotFound(ex.Message);
                }
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeletePlanet(int id)
            {
                var deleted = await _planetService.DeletePlanetAsync(id);
                if (!deleted)
                {
                    return NotFound("Planet not found");
                }

                return Ok("Planet deleted successfully");
            }


            [HttpGet("{name}")]
            public async Task<ActionResult<IEnumerable<PlanetDto>>> SearchPlanetsByName(string name)
            {
                var planets = await _planetService.SearchPlanetsByNameAsync(name);
                return Ok(planets);
            }
        }
    }
