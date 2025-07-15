using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using ExamSystem.Application.Services.Service;
using ExamSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }
        [HttpPost()]
        public IActionResult CreateExam([FromBody] ExamDto examDto, [FromQuery] int studentId, [FromQuery] int subjectId)
        {
            if (!ModelState.IsValid || examDto == null)
                return BadRequest(ModelState);
            _examService.CreateExam(examDto, studentId, subjectId);
            return Ok("Successfully created");
        }
        [HttpGet]
        public IActionResult GetExamHistoryByStudentId(int studentId)
        {
            if (studentId <= 0)
                return BadRequest("Invalid student ID.");
            var examHistory = _examService.GetExamHistoryByStudentId(studentId);
            if (examHistory == null || examHistory.Count == 0)
                return NotFound("No exam history found for the given student ID.");
            return Ok(examHistory);
        }
    }
}
