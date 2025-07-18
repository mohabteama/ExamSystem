using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _studentService.GetAllStudents();
            if (students == null || students.Count == 0)
                return NotFound("No students found.");
            return Ok(students);
        }
        //[HttpPost]
        //public IActionResult CreateStudent([FromBody] StudentDto StudentDto)
        //{
        //    if (StudentDto == null) return BadRequest();

        //    var result = _studentService.CreateStudent(StudentDto);
        //    if (!result) return StatusCode(422, "Subject already exists or error occurred");

        //    return StatusCode(201, "Successfully created");
        //}
        [HttpPut("{studentId}/status/{isActive}")]
        public IActionResult UpdateStudentStatus(string studentId, bool isActive)
        {
            var result = _studentService.UpdateStudentStatus(studentId, isActive);
            if (!result) return NotFound("Student not found or error occurred");
            return Ok("Successfully updated student status");
        }
        [HttpGet("paginated")]
        public async Task<IActionResult> GetStudentsPaginated(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] bool? isActive = null)
        {
            try
            {
                var paginatedResult = await _studentService.GetStudentsPaginatedAsync(pageNumber, pageSize, isActive);
                return Ok(paginatedResult);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
