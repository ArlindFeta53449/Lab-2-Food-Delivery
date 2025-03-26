using Business.Services.ContractServices;
using Data.DTOs.ContractDtos;
using Microsoft.AspNetCore.Mvc;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractDto>>> GetAllContracts()
        {
            var contracts = await _contractService.GetAllContractsAsync();
            return Ok(contracts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContractDto>> GetContract(int id)
        {
            var contract = await _contractService.GetContractAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            return Ok(contract);
        }

        [HttpPost]
        public async Task<ActionResult<ContractDto>> CreateContract([FromBody] ContractCreateDto contractCreateDto)
        {
            try
            {
                var createdContract = await _contractService.CreateContractAsync(contractCreateDto);
                return CreatedAtAction(nameof(GetContract), new { id = createdContract.Id }, createdContract);
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., book creation error
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(int id, [FromBody] ContractDto contractDto)
        {
            if (id != contractDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                var updatedContract = await _contractService.UpdateContractAsync(id, contractDto);
                return Ok(updatedContract);
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., book not found or update error
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(int id)
        {
            var deleted = await _contractService.DeleteContractAsync(id);
            if (!deleted)
            {
                return NotFound("Contract not found");
            }

            return Ok("Contract deleted successfully");
        }

        [HttpGet("{startDate}")]
        public async Task<ActionResult<IEnumerable<ContractDto>>> SearchContractsByStartDate(DateTime startDate)
        {
            var contracts = await _contractService.SearchContractsByStartDateAsync(startDate);
            return Ok(contracts);
        }


        [HttpGet("{employeeName}")]
        public async Task<ActionResult<IEnumerable<ContractDto>>> SearchContractsByEmployeeName(string employeeName)
        {
            var contracts = await _contractService.SearchContractsByEmployeeAsync(employeeName);
            return Ok(contracts);
        }
    }
}
