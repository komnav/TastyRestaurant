using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("AuthController")]
    public class AtuhController : Controller
    {
        [HttpPost("Token")]
        public IActionResult GenerateToken([FromBody] LoginRequest request, TokenGenerator tokenGenerator)
        {
            var access_token = tokenGenerator.GenerateToken(request.Email);
            return Ok(access_token);
        }
    }
}

