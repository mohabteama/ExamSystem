using ExamSystem.API.Hubs;
using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;
        private readonly ILogger<ExamController> _logger;
        private readonly IHubContext<NotificationHub> _hubContext;

        public ExamController(IExamService examService, ILogger<ExamController> logger, IHubContext<NotificationHub> hubContext)
        {
            _examService = examService;
            _hubContext = hubContext; 
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Authorize]
        [HttpGet("student/history")]
        public async Task<IActionResult> GetStudentExamHistory(
          
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string status = null)
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
        public async Task<IActionResult> CreateExam([FromQuery] int subjectId)
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(studentId) || subjectId <= 0)
                return BadRequest("Invalid student ID or subject ID.");


            var examDto = await _examService.CreateExam(studentId, subjectId);
            return Ok(examDto);
        }
        [Authorize]
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitExam([FromBody] ExamSubmissionInputDto input)
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (input == null || string.IsNullOrEmpty(studentId) || input.ExamId <= 0)
                return BadRequest("Invalid submission data.");
            try
            {
                var result = await _examService.Submit(input);
                await _hubContext.Clients.All.SendAsync("ReceiveScore", new
                {
                    StudentId = result.StudentId,
                    Score = result.Score,
                    SubjectName = result.SubjectName,
                    ExamDate = result.ExamDate
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting exam");
                return StatusCode(500, "An error occurred while submitting the exam");
            }
        }
    }
}
