using Api.DTO;
using Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Teacher,Manager,Admin")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet("timesheet/{timesheetId}")]
        public async Task<IActionResult> GetTimesheetAttendance(int timesheetId)
        {
            try
            {
                var records = await _attendanceService.GetTimesheetAttendanceAsync(timesheetId);
                return Ok(new
                {
                    success = true,
                    data = records,
                    message = "Attendance records retrieved successfully"
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
                    message = "An error occurred while retrieving attendance",
                    error = ex.Message
                });
            }
        }

        [HttpPost("timesheet/{timesheetId}")]
        public async Task<IActionResult> UpsertAttendance(int timesheetId, [FromBody] AttendanceUpsertDTO upsertDTO)
        {
            try
            {
                if (upsertDTO.TimesheetId != timesheetId)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "TimesheetId mismatch"
                    });
                }

                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                await _attendanceService.UpsertAttendanceAsync(upsertDTO, userId);

                return Ok(new
                {
                    success = true,
                    message = "Attendance saved successfully"
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
                    message = "An error occurred while saving attendance",
                    error = ex.Message
                });
            }
        }
    }
}
