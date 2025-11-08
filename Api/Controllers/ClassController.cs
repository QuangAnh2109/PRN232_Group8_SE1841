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
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var userRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

                IEnumerable<ClassResponseDTO> classes;

                if (userRole == "Student")
                {
                    classes = await _classService.GetStudentClassesAsync(userId);
                }
                else
                {
                    classes = await _classService.GetAllClassesAsync();
                }

                return Ok(new
                {
                    success = true,
                    data = classes,
                    message = "Classes retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while retrieving classes",
                    error = ex.Message
                });
            }
        }

        [HttpGet("{id}/students")]
        public async Task<IActionResult> GetClassStudents(int id)
        {
            try
            {
                var students = await _classService.GetClassStudentsAsync(id);
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

        [HttpPost]
        [Authorize(Roles = "Teacher,Manager,Admin")]
        public async Task<IActionResult> CreateClass([FromBody] CreateClassDTO createClassDTO)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var createdClass = await _classService.CreateClassAsync(createClassDTO, userId);
                
                return CreatedAtAction(
                    nameof(GetAllClasses),
                    new { id = createdClass.Id },
                    new
                    {
                        success = true,
                        data = createdClass,
                        message = "Class created successfully"
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
                    message = "An error occurred while creating the class",
                    error = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher,Manager,Admin")]
        public async Task<IActionResult> UpdateClass(int id, [FromBody] UpdateClassDTO updateClassDTO)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var updatedClass = await _classService.UpdateClassAsync(id, updateClassDTO, userId);
                
                return Ok(new
                {
                    success = true,
                    data = updatedClass,
                    message = "Class updated successfully"
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
                    message = "An error occurred while updating the class",
                    error = ex.Message
                });
            }
        }

        [HttpPut("active")]
        [Authorize(Roles = "Teacher,Manager,Admin")]
        public async Task<IActionResult> UpdateClassActiveStatus([FromBody] ActiveDTO<int> activeDTO)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                await _classService.UpdateClassActiveStatusAsync(activeDTO, userId);
                
                return Ok(new
                {
                    success = true,
                    message = $"Class active status updated to {(activeDTO.IsActive ? "active" : "inactive")}"
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
                    message = "An error occurred while updating class active status",
                    error = ex.Message
                });
            }
        }
    }
}
