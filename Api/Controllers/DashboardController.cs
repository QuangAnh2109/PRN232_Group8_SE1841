using Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Api.Constants;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly ITeacherDashboardService _teacherDashboardService;
        private readonly IDashboardService _dashboardService;

        public DashboardController(ITeacherDashboardService teacherDashboardService, IDashboardService dashboardService)
        {
            _teacherDashboardService = teacherDashboardService;
            _dashboardService = dashboardService;
        }

        [Authorize(Roles = DefaultValues.TeacherRole)]
        [HttpGet("teacher")]
        public async Task<IActionResult> GetTeacherDashboard()
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
                var dashboard = await _teacherDashboardService.GetDashboardAsync(userId, role);

                return Ok(new
                {
                    success = true,
                    data = dashboard,
                    message = "Dashboard data retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while loading dashboard data",
                    error = ex.Message
                });
            }
        }
        
        [Authorize(Roles = DefaultValues.AdminRole)]
        [HttpGet("admin")]
        public async Task<IActionResult> GetAdminDashboard()
        {
            return Ok(await _dashboardService.GetAdminDashboardAsync());
        }
    }
}
