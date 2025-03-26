using Microsoft.AspNetCore.Mvc;
using Business.Services.EmployeeServices;
using Data.DTOs.EmployeeDtos;
using Data.Entities;


namespace UserMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] EmployeeCreateDto employeeCreateDto)
        {
            var createdEmployee = await _employeeService.CreateEmployeeAsync(employeeCreateDto);
            return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.Id }, createdEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (id != employeeDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employeeDto);
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., author not found
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleted = await _employeeService.DeleteEmployeeAsync(id);
            if (!deleted)
            {
                return NotFound("Employee not found");
            }

            return Ok("Employee deleted successfully");
        }


        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> SearchEmployeesByName(string name)
        {
            var employees = await _employeeService.SearchEmployeesByNameAsync(name);
            return Ok(employees);
        }
    }
}
