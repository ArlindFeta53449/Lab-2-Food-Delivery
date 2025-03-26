using Business.Services.CostumerServices;
using Data.DTOs.CostumerDtos;
using Microsoft.AspNetCore.Mvc;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CostumerController : ControllerBase
    {
        private readonly ICostumerService _costumerService;

        public CostumerController(ICostumerService costumerService)
        {
            _costumerService = costumerService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CostumerDto>>> GetAllCostumers()
        {
            var costumers = await _costumerService.GetAllCostumersAsync();
            return Ok(costumers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CostumerDto>> GetCostumer(int id)
        {
            var costumer = await _costumerService.GetCostumerAsync(id);
            if (costumer == null)
            {
                return NotFound();
            }

            return Ok(costumer);
        }

        [HttpPost]
        public async Task<ActionResult<CostumerDto>> CreateCostumer([FromBody] CostumerCreateDto costumerCreateDto)
        {
            var createdCostumer = await _costumerService.CreateCostumerAsync(costumerCreateDto);
            return CreatedAtAction(nameof(GetCostumer), new { id = createdCostumer.Id }, createdCostumer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCostumer(int id, [FromBody] CostumerDto costumerDto)
        {
            if (id != costumerDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                var updatedCostumer = await _costumerService.UpdateCostumerAsync(id, costumerDto);
                return Ok(updatedCostumer);
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., author not found
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCostumer(int id)
        {
            var deleted = await _costumerService.DeleteCostumerAsync(id);
            if (!deleted)
            {
                return NotFound("Costumer not found");
            }

            return Ok("Costumer deleted successfully");
        }


        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<CostumerDto>>> SearchCostumersByName(string name)
        {
            var costumers = await _costumerService.SearchCostumersByNameAsync(name);
            return Ok(costumers);
        }
    }
}
