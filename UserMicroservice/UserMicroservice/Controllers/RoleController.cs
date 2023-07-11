using Business.Services.Roles;
using Business.Services.ZSyncDataServices.Http;
using Data.DTOs.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly INotificationDataClient _notificationDataClient;

        public RoleController(
            IRoleService roleService,
            INotificationDataClient notificationDataClient)
        {
            _roleService = roleService;
            _notificationDataClient = notificationDataClient;
        }

        [HttpGet]

        public IActionResult GetAllRoles()
        {
            var response =  _roleService.GetAll();
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public IActionResult GetRole(int id)
        {
            var response = _roleService.GetRole(id);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        public  IActionResult DeleteRole(int id)
        {
            var response = _roleService.DeleteRole(id);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPost]
        public IActionResult CreateRole(RoleCreateDto role)
        {
            var response =  _roleService.CreateRole(role);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut]
        public IActionResult EditRole(RoleDto role)
        {
            var response = _roleService.EditRole(role);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
