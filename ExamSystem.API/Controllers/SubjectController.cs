using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost]
        public IActionResult CreateSubject([FromBody] CreateSubjectDto CreateSubjectDto)
        {
            if (CreateSubjectDto == null) return BadRequest();

            var result = _subjectService.CreateSubject(CreateSubjectDto);
            if (!result) return StatusCode(422, "Subject already exists or error occurred");

            return StatusCode(201, "Successfully created");
        }

        [HttpGet]
        public IActionResult GetAllSubjects()
        {
            var subjects = _subjectService.GetAllSubjects();
            if (subjects == null || subjects.Count == 0)
                return NotFound("No subjects found.");

            return Ok(subjects);
        }
        [HttpPut]
        public IActionResult UpdateSubject([FromBody] SubjectDto subjectDto, int SubjectId)
        {
            if (subjectDto == null)
                return BadRequest();

            var result = _subjectService.UpdateSubject(subjectDto, SubjectId);
            if (!result)
                return NotFound();

            return Ok("Successfully updated");
        }
        [HttpGet("{subjectId}")]
        public async Task<IActionResult> GetSubjectWithQuestions(int subjectId)
        {
            if (subjectId <= 0)
                return BadRequest("Invalid subject ID.");
            var subject = await _subjectService.GetSubjectWithQuestions(subjectId);
            if (subject == null)
                return NotFound("Subject not found.");
            return Ok(subject);
        }
    }
}
