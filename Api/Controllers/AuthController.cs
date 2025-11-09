using Api.Constants;
using Api.DTO;
using Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        private readonly IAccountService _accountService;

        public AuthController(ILogger<AuthController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterAccountDTO register)
        {
            await _accountService.RegisterAccountAsync(register);
            return Ok();
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetJwtToken([FromBody] LoginDTO login)
        {
            return Ok(await _accountService.GetJwtTokenAsync(login));
        }
        
        [Authorize]
        [HttpGet("token")]
        public async Task<IActionResult> GetAccessToken()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var lastModifiedTime = HttpContext.User.FindFirstValue(ClaimTypes.Version);

            if (userId == null || lastModifiedTime == null) 
                return BadRequest();
            else
                return Ok(await _accountService.GetAccessTokenAsync(int.Parse(userId), DateTime.Parse(lastModifiedTime)));
        }


    }
}