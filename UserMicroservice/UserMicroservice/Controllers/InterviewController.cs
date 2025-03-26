using Microsoft.AspNetCore.Mvc;
using Business.Services.InterviewServices;
using Data.DTOs.InterviewDtos;
using Data.Entities;


namespace UserMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InterviewController : ControllerBase
    {
        private readonly IInterviewService _interviewService;

        public InterviewController(IInterviewService interviewService)
        {
            _interviewService = interviewService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterviewDto>>> GetAllInterviews()
        {
            var interviews = await _interviewService.GetAllInterviewsAsync();
            return Ok(interviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InterviewDto>> GetInterview(int id)
        {
            var interview = await _interviewService.GetInterviewAsync(id);
            if (interview == null)
            {
                return NotFound();
            }

            return Ok(interview);
        }

        [HttpPost]
        public async Task<ActionResult<InterviewDto>> CreateInterview([FromBody] InterviewCreateDto interviewCreateDto)
        {
            var createdInterview = await _interviewService.CreateInterviewAsync(interviewCreateDto);
            return CreatedAtAction(nameof(GetInterview), new { id = createdInterview.Id }, createdInterview);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInterview(int id, [FromBody] InterviewDto interviewDto)
        {
            if (id != interviewDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                var updatedInterview = await _interviewService.UpdateInterviewAsync(id, interviewDto);
                return Ok(updatedInterview);
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., author not found
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterview(int id)
        {
            var deleted = await _interviewService.DeleteInterviewAsync(id);
            if (!deleted)
            {
                return NotFound("Interview not found");
            }

            return Ok("Interview deleted successfully");
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<InterviewDto>>> SearchInterviewsById(int id)
        {
            var interviews = await _interviewService.SearchInterviewsByIdAsync(id);
            return Ok(interviews);
        }

        [HttpGet("{startDate}")]
        public async Task<ActionResult<IEnumerable<InterviewDto>>> SearchInterviewsByStartDate(DateTime startDate)
        {
            var interviews = await _interviewService.SearchInterviewsByStartDateAsync(startDate);
            return Ok(interviews);
        }


    }
}
