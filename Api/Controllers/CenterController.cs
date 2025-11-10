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
        public async Task<IActionResult> GetAllCenters()
        {
            throw new NotImplementedException();
        }

        [HttpPut("centers/{id:int}/active/{isActive:bool}")]
        public async Task<IActionResult> ChangeCenterActivity(int id, bool isActive)
        {
            throw new NotImplementedException();
        }
    }
}
