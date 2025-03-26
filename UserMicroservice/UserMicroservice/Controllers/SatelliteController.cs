using Business.Services.SatelliteServices;
using Data.DTOs.SatelliteDtos;
using Microsoft.AspNetCore.Mvc;

namespace UserMicroservice.Controllers
{

        [Route("api/[controller]/[action]")]
        [ApiController]
        public class SatelliteController : ControllerBase
        {
            private readonly ISatelliteService _satelliteService;

            public SatelliteController(ISatelliteService satelliteService)
            {
                _satelliteService = satelliteService;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<SatelliteDto>>> GetAllSatellites()
            {
                var satellites = await _satelliteService.GetAllSatellitesAsync();
                return Ok(satellites);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<SatelliteDto>> GetSatellite(int id)
            {
                var satellite = await _satelliteService.GetSatelliteAsync(id);
                if (satellite == null)
                {
                    return NotFound();
                }

                return Ok(satellite);
            }

            [HttpPost]
            public async Task<ActionResult<SatelliteDto>> CreateSatellite([FromBody] SatelliteCreateDto satelliteCreateDto)
            {
                try
                {
                    var createdSatellite = await _satelliteService.CreateSatelliteAsync(satelliteCreateDto);
                    return CreatedAtAction(nameof(GetSatellite), new { id = createdSatellite.Id }, createdSatellite);
                }
                catch (Exception ex)
                {
                    // Handle exceptions, e.g., book creation error
                    return BadRequest(ex.Message);
                }
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateSatellite(int id, [FromBody] SatelliteDto satelliteDto)
            {
                if (id != satelliteDto.Id)
                {
                    return BadRequest("ID mismatch");
                }

                try
                {
                    var updatedSatellite = await _satelliteService.UpdateSatelliteAsync(id, satelliteDto);
                    return Ok(updatedSatellite);
                }
                catch (Exception ex)
                {
                    // Handle exceptions, e.g., book not found or update error
                    return NotFound(ex.Message);
                }
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteSatellite(int id)
            {
                var deleted = await _satelliteService.DeleteSatelliteAsync(id);
                if (!deleted)
                {
                    return NotFound("Satellite not found");
                }

                return Ok("Satellite deleted successfully");
            }



            [HttpGet("{planetName}")]
            public async Task<ActionResult<IEnumerable<SatelliteDto>>> SearchSatellitesByPlanetName(string planetName)
            {
                var satellites = await _satelliteService.SearchSatellitesByPlanetAsync(planetName);
                return Ok(satellites);
            }
        }
    }

