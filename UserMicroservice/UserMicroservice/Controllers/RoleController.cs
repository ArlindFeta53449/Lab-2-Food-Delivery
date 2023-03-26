using Business.Services.Roles;
using Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]

        public IActionResult GetAllRoles()
        {
            var roles = _roleService.GetAll();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public IActionResult GetRole(int id)
        {
            var role = _roleService.GetRole(id);
            return Ok(role);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
           _roleService.DeleteRole(id);
            return Ok("Roli u fshi me sukses");
        }
        [HttpPost]
        public IActionResult CreateRole(RoleCreateDto role)
        {
            var result = _roleService.CreateRole(role);
            return Ok(result);
        }
        [HttpPut]
        public IActionResult EditRole(RoleDto role)
        {
            var result = _roleService.EditRole(role);
            return Ok(result);
        }
    }
}
