using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;

using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;
        private readonly ILogger<ExamController> _logger;

        public ExamController(IExamService examService, ILogger<ExamController> logger)
        {
            _examService = examService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpPost()]
        public async Task<IActionResult> CreateRondomExam([FromQuery] string studentId, [FromQuery] int subjectId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            var result = await _examService.CreateRondomQuestions(studentId, subjectId);
            if (result == false)
            {
                return BadRequest("Failed to create exam");
            }
            return Ok("Successfully created");
        }
        [HttpGet("history")]
        public async Task<IActionResult> GetAllExamHistory(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string status = null)
        {
            try
            {
                var paginatedResult = await _examService.GetAllExamHistoryPagedAsync(pageNumber, pageSize, status);

                if (paginatedResult.Items == null || !paginatedResult.Items.Any())
                    return NotFound("No exam history found.");

                return Ok(paginatedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving exam history");
                return StatusCode(500, "An error occurred while retrieving exam history");
            }
        }
        [HttpGet("student/{studentId}/history")]
        public async Task<IActionResult> GetStudentExamHistory(
            string studentId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string status = null)
        {
            if (string.IsNullOrEmpty(studentId))
                return BadRequest("Invalid student ID.");

            try
            {
                var paginatedResult = await _examService.GetStudentExamHistoryPagedAsync(
                    studentId, pageNumber, pageSize, status);

                if (paginatedResult.Items == null || !paginatedResult.Items.Any())
                    return NotFound("No exam history found for the given student ID.");

                return Ok(paginatedResult);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving student exam history");
                return StatusCode(500, "An error occurred while retrieving student exam history");
            }
        }
        [HttpGet("exam/{examId}")]
        public async Task<IActionResult> GetExam(int examId)
        {
            var examDto = await _examService.GetExamWithQuestions(examId);
            if (examDto == null)
                return NotFound();

            return Ok(examDto);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateExam([FromQuery] string studentId, [FromQuery] int subjectId)
        {
            if (string.IsNullOrEmpty(studentId) || subjectId <= 0)
                return BadRequest("Invalid student ID or subject ID.");
            try
            {
                var examDto = await _examService.CreateExam(studentId, subjectId);
                return Ok(examDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating exam");
                return StatusCode(500, "An error occurred while creating the exam");
            }
        }
    }
}
