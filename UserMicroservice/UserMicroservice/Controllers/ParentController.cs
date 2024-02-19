using Microsoft.AspNetCore.Mvc;
using Business.Services.ParentServices;
using Data.DTOs.ParentDtos;


namespace UserMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentService;

        public ParentController(IParentService parentService)
        {
            _parentService = parentService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParentDto>>> GetAllParents()
        {
            var parents = await _parentService.GetAllParentsAsync();
            return Ok(parents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParentDto>> GetParent(int id)
        {
            var parent = await _parentService.GetParentAsync(id);
            if (parent == null)
            {
                return NotFound();
            }

            return Ok(parent);
        }

        [HttpPost]
        public async Task<ActionResult<ParentDto>> CreateParent([FromBody] ParentCreateDto parentCreateDto)
        {
            var createdParent = await _parentService.CreateParentAsync(parentCreateDto);
            return CreatedAtAction(nameof(GetParent), new { id = createdParent.Id }, createdParent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParent(int id, [FromBody] ParentDto parentDto)
        {
            if (id != parentDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                var updatedParent = await _parentService.UpdateParentAsync(id, parentDto);
                return Ok(updatedParent);
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., author not found
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParent(int id)
        {
            var deleted = await _parentService.DeleteParentAsync(id);
            if (!deleted)
            {
                return NotFound("Parent not found");
            }

            return Ok("Parent deleted successfully");
        }


        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<ParentDto>>> SearchParentsByName(string name)
        {
            var parents = await _parentService.SearchParentsByNameAsync(name);
            return Ok(parents);
        }
    }
}
