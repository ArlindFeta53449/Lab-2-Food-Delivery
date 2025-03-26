using Business.Services.InterviewNotesServices;
using Data.DTOs.ContractDtos;
using Data.DTOs.InterviewNotesDtos;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InterviewNotesController : ControllerBase
    {
        private readonly IInterviewNotesService _interviewNotesService;

        public InterviewNotesController(IInterviewNotesService interviewNotesService)
        {
            _interviewNotesService = interviewNotesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterviewNotesDto>>> GetAllInterviewNotes()
        {
            var interviewNotes = await _interviewNotesService.GetAllInterviewNotesAsync();
            return Ok(interviewNotes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InterviewNotesDto>> GetInterviewNotes(int id)
        {
            var interviewNotes = await _interviewNotesService.GetInterviewNotesAsync(id);
            if (interviewNotes == null)
            {
                return NotFound();
            }

            return Ok(interviewNotes);
        }

        [HttpPost]
        public async Task<ActionResult<InterviewNotesDto>> CreateInterviewNotes([FromBody] InterviewNotesCreateDto interviewNotesCreateDto)
        {
            try
            {
                var createdInterviewNotes = await _interviewNotesService.CreateInterviewNotesAsync(interviewNotesCreateDto);
                return CreatedAtAction(nameof(GetInterviewNotes), new { id = createdInterviewNotes.Id }, createdInterviewNotes);
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., book creation error
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInterviewNotes(int id, [FromBody] InterviewNotesDto interviewNotesDto)
        {
            if (id != interviewNotesDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                var updatedInterviewNotes = await _interviewNotesService.UpdateInterviewNotesAsync(id, interviewNotesDto);
                return Ok(updatedInterviewNotes);
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., book not found or update error
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterviewNotes(int id)
        {
            var deleted = await _interviewNotesService.DeleteInterviewNotesAsync(id);
            if (!deleted)
            {
                return NotFound("InterviewNotes not found");
            }

            return Ok("InterviewNotes deleted successfully");
        }


        [HttpGet("{interviewId}")]
        public async Task<ActionResult<IEnumerable<InterviewNotesDto>>> SearchInterviewNotesByInterviewAsync(int interviewId)
        {
            var interviewNotes = await _interviewNotesService.SearchInterviewNotesByInterviewAsync(interviewId);
            return Ok(interviewNotes);
        }
    }
}
