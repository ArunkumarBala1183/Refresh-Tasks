using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[Controller]")]
    [ApiController]
    public class CheckValidationController : ControllerBase
    {
        [HttpGet("GetUsername")]
        public IActionResult GetUserName([FromForm] string username)
        {
            return Ok(username);
        }
    }
}