using Api.DTO;
using Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Teacher")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                return Ok(new
                {
                    success = true,
                    data = students,
                    message = "Students retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while retrieving students",
                    error = ex.Message
                });
            }
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetStudentDetails(int id)
        {
            try
            {
                var studentDetails = await _studentService.GetStudentDetailsByIdAsync(id);
                return Ok(new
                {
                    success = true,
                    data = studentDetails,
                    message = "Student details retrieved successfully"
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
                    message = "An error occurred while retrieving student details",
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDTO createStudentDTO)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var teacherCenterId = await GetTeacherCenterIdAsync(userId);
                var createdStudent = await _studentService.CreateStudentAsync(createStudentDTO, teacherCenterId, userId);
                
                return CreatedAtAction(
                    nameof(GetStudentDetails),
                    new { id = createdStudent.Id },
                    new
                    {
                        success = true,
                        data = createdStudent,
                        message = "Student created successfully. Default password: Student@123"
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
                    message = "An error occurred while creating the student",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id);
                
                return Ok(new
                {
                    success = true,
                    message = "Student deleted successfully"
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
                    message = "An error occurred while deleting the student",
                    error = ex.Message
                });
            }
        }

        [HttpPut("active")]
        public async Task<IActionResult> UpdateStudentActiveStatus([FromBody] ActiveDTO<int> activeDTO)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                await _studentService.UpdateStudentActiveStatusAsync(activeDTO, userId);
                
                return Ok(new
                {
                    success = true,
                    message = $"Student active status updated to {(activeDTO.IsActive ? "active" : "inactive")}"
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
                    message = "An error occurred while updating student active status",
                    error = ex.Message
                });
            }
        }

        private async Task<int> GetTeacherCenterIdAsync(int teacherId)
        {
            return 1;
        }
    }
}
