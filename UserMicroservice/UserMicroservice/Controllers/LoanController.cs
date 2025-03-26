using Business.Services.LoanServices;
using Data.DTOs.LoanDtos;
using Microsoft.AspNetCore.Mvc;

namespace UserMicroservice.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetAllLoans()
        {
            var loans = await _loanService.GetAllLoansAsync();
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoanDto>> GetLoan(int id)
        {
            var loan = await _loanService.GetLoanAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            return Ok(loan);
        }

        [HttpPost]
        public async Task<ActionResult<LoanDto>> CreateLoan([FromBody] LoanCreateDto loanCreateDto)
        {
            try
            {
                var createdLoan = await _loanService.CreateLoanAsync(loanCreateDto);
                return CreatedAtAction(nameof(GetLoan), new { id = createdLoan.Id }, createdLoan);
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., book creation error
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoan(int id, [FromBody] LoanDto loanDto)
        {
            if (id != loanDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                var updatedLoan = await _loanService.UpdateLoanAsync(id, loanDto);
                return Ok(updatedLoan);
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., book not found or update error
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var deleted = await _loanService.DeleteLoanAsync(id);
            if (!deleted)
            {
                return NotFound("Loan not found");
            }

            return Ok("Loan deleted successfully");
        }



        [HttpGet("{costumerName}")]
        public async Task<ActionResult<IEnumerable<LoanDto>>> SearchLoansByCostumerName(string costumerName)
        {
            var loans = await _loanService.SearchLoansByCostumerAsync(costumerName);
            return Ok(loans);
        }
    }
}

