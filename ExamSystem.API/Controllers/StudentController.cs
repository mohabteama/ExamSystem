using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using ExamSystem.Application.Services.Service;
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
        [HttpPost]
        public IActionResult CreateStudent([FromBody] StudentDto StudentDto)
        {
            if (StudentDto == null) return BadRequest();

            var result = _studentService.CreateStudent(StudentDto);
            if (!result) return StatusCode(422, "Subject already exists or error occurred");

            return StatusCode(201, "Successfully created");
        }
    }
}
