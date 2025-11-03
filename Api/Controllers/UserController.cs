using Api.DTO;
using Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> ChangeUserActivity([FromBody] Models.User user)
        {
            throw new NotImplementedException();
        }

        [HttpPut("users/active")]
        public async Task<IActionResult> CreateUser([FromBody] IEnumerable<int> userId)
        {
            throw new NotImplementedException();
        }

    }
}
