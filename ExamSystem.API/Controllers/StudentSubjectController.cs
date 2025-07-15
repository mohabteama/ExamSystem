using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSubjectController : ControllerBase
    {
        private readonly IStudentSubjectService _studentSubjectService;
        public StudentSubjectController(IStudentSubjectService studentSubjectService)
        {
            _studentSubjectService = studentSubjectService;
        }
        [HttpPost]
        public IActionResult CreateStudentSubject([FromBody] StudentSubjectDto studentSubjectDto)
        {
            if (studentSubjectDto == null) return BadRequest();
            var result = _studentSubjectService.CreateSubject(studentSubjectDto);
            if (!result) return StatusCode(422, "Student subject already exists or error occurred");
            return StatusCode(201, "Successfully created");
        }
        [HttpGet]
        public IActionResult GetAllStudentSubjects()
        {
            var studentSubjects = _studentSubjectService.GetAllStudentSubject();
            if (studentSubjects == null || studentSubjects.Count == 0)
                return NotFound("No student subjects found.");
            return Ok(studentSubjects);
        }
    }
}
