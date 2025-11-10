using Api.Constants;
using Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CenterController : ControllerBase
    {
        private readonly ILogger<CenterController> _logger;
        private readonly ICenterService _centerService;

        public CenterController(ILogger<CenterController> logger, ICenterService centerService)
        {
            _logger = logger;
            _centerService = centerService;
        }

        [HttpGet("centers")]
        public async Task<IActionResult> GetAllCenters([FromQuery] int page = DefaultValues.DefaultPage, [FromQuery] int limit = DefaultValues.DefaultLimit)
        {
            return Ok(await _centerService.GetAllCentersWithPaginationAsync(page, limit));
        }

        [HttpPut("centers/{id:int}/active/{isActive:bool}")]
        public async Task<IActionResult> ChangeCenterActivity(int id, bool isActive)
        {
            await _centerService.ChangeCenterActivityAsync(id, isActive);
            return Ok();
        }

        [HttpGet("centers/{id:int}/details")]
        public async Task<IActionResult> GetCenterDetailsById(int id)
        {
            return Ok(await _centerService.GetCenterDetailsByIdAsync(id));
        }
    }
}
