using Api.DTO;
using Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimesheetController : ControllerBase
    {
        private readonly ITimesheetService _timesheetService;

        public TimesheetController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTimesheets([FromQuery] int? classId)
        {
            try
            {
                var timesheets = await _timesheetService.GetTimesheetsAsync(classId);
                return Ok(new
                {
                    success = true,
                    data = timesheets,
                    message = "Timesheets retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while retrieving timesheets",
                    error = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimesheet(int id)
        {
            try
            {
                var timesheet = await _timesheetService.GetTimesheetByIdAsync(id);
                return Ok(new
                {
                    success = true,
                    data = timesheet,
                    message = "Timesheet retrieved successfully"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while retrieving the timesheet",
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Teacher,Manager,Admin")]
        public async Task<IActionResult> CreateTimesheet([FromBody] CreateTimesheetDTO createTimesheetDTO)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var timesheet = await _timesheetService.CreateTimesheetAsync(createTimesheetDTO, userId);

                return CreatedAtAction(nameof(GetTimesheet), new { id = timesheet.Id }, new
                {
                    success = true,
                    data = timesheet,
                    message = "Timesheet created successfully"
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while creating the timesheet",
                    error = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher,Manager,Admin")]
        public async Task<IActionResult> UpdateTimesheet(int id, [FromBody] UpdateTimesheetDTO updateTimesheetDTO)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var timesheet = await _timesheetService.UpdateTimesheetAsync(id, updateTimesheetDTO, userId);

                return Ok(new
                {
                    success = true,
                    data = timesheet,
                    message = "Timesheet updated successfully"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while updating the timesheet",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher,Manager,Admin")]
        public async Task<IActionResult> DeleteTimesheet(int id)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                await _timesheetService.DeleteTimesheetAsync(id, userId);

                return Ok(new
                {
                    success = true,
                    message = "Timesheet deleted successfully"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while deleting the timesheet",
                    error = ex.Message
                });
            }
        }
    }
}
