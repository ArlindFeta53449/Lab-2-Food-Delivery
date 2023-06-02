using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NotificationMicroservice.Controllers
{
    [Route("api/u/[controller]/[action]")]
    [ApiController]
    public class UserMicroserviceController : ControllerBase
    {
        public UserMicroserviceController()
        {
            
        }
        [HttpPost]
        public IActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # command Service");
            return Ok("Inbound test ok from UserMicroservice Controller");
        }
    }
}
