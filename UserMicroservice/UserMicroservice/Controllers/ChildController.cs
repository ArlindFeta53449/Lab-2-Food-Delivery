using Microsoft.AspNetCore.Mvc;
using Business.Services.ChildServices;
using Data.DTOs.ChildDtos;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChildController : ControllerBase
    {
        private readonly IChildService _childService;

        public ChildController(IChildService childService)
        {
            _childService = childService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChildDto>>> GetAllChildren()
        {
            var children = await _childService.GetAllChildrenAsync();
            return Ok(children);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChildDto>> GetChild(int id)
        {
            var child = await _childService.GetChildAsync(id);
            if (child == null)
            {
                return NotFound();
            }

            return Ok(child);
        }

        [HttpPost]
        public async Task<ActionResult<ChildDto>> CreateChild([FromBody] ChildCreateDto childCreateDto)
        {
            try
            {
                var createdChild = await _childService.CreateChildAsync(childCreateDto);
                return CreatedAtAction(nameof(GetChild), new { id = createdChild.Id }, createdChild);
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., book creation error
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChild(int id, [FromBody] ChildDto childDto)
        {
            if (id != childDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                var updatedChild = await _childService.UpdateChildAsync(id, childDto);
                return Ok(updatedChild);
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., book not found or update error
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChild(int id)
        {
            var deleted = await _childService.DeleteChildAsync(id);
            if (!deleted)
            {
                return NotFound("Child not found");
            }

            return Ok("Child deleted successfully");
        }

        [HttpGet("{difficulty}")]
        public async Task<ActionResult<IEnumerable<ChildDto>>> SearchChildrenByDifficulty(string difficulty)
        {
            var children = await _childService.SearchChildrenByDifficultyAsync(difficulty);
            return Ok(children);
        }


        [HttpGet("{parentName}")]
        public async Task<ActionResult<IEnumerable<ChildDto>>> SearchChildrenByParentName(string parentName)
        {
            var children = await _childService.SearchChildrenByParentAsync(parentName);
            return Ok(children);
        }
    }
}
