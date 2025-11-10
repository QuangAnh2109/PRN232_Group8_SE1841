using Api.Constants;
using Api.DTO;
using Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = DefaultValues.AdminRole)]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUser([FromQuery] int page = DefaultValues.DefaultPage,[FromQuery] int limit = DefaultValues.DefaultLimit)
        {
            return Ok(await _userService.GetAllUsersWithPaginationAsync(page, limit));
        }

        [Authorize(Roles = DefaultValues.AdminRole)]
        [HttpPut("users/{id:int}/active/{isActive:bool}")]
        public async Task<IActionResult> ChangeUserActivity(int id, bool isActive)
        {
            await _userService.ChangeUserActivityAsync(id, isActive);
            return Ok();
        }

        [Authorize(Roles = DefaultValues.AdminRole)]
        [HttpGet("users/{id:int}/details")]
        public async Task<IActionResult> GetUserDetails(int id)
        {
            return Ok(await _userService.GetUserDetailsByIdAsync(id));
        }
    }
}
