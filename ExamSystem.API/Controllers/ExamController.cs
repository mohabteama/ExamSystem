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
        public IActionResult CreateExam([FromBody] ExamDto examDto, [FromQuery] string studentId, [FromQuery] int subjectId)
        {
            if (!ModelState.IsValid || examDto == null)
                return BadRequest(ModelState);
            _examService.CreateExam(examDto, studentId, subjectId);
            return Ok("Successfully created");
        }
        //[HttpGet("{studentId}")]
        //public IActionResult GetExamHistoryByStudentId(string studentId)
        //{
        //    if (studentId == null)
        //        return BadRequest("Invalid student ID.");
        //    var examHistory = _examService.GetExamHistoryByStudentId(studentId);
        //    if (examHistory == null || examHistory.Count == 0)
        //        return NotFound("No exam history found for the given student ID.");
        //    return Ok(examHistory);
        //}
        //[HttpGet("results/{studentId}")]
        //public IActionResult GetStudentExamsByStudentId(string studentId)
        //{
        //    if (string.IsNullOrEmpty(studentId))
        //        return BadRequest("Invalid student ID.");
        //    var studentExams = _examService.GetStudentExamsByStudentId(studentId);
        //    if (studentExams == null || studentExams.Count == 0)
        //        return NotFound("No exams found for the given student ID.");
        //    return Ok(studentExams);
        //}
        [HttpPost("submit")]
        public async Task<ActionResult<ExamSubmissionResultDto>> SubmitExam(ExamSubmissionDto submission)
        {
            try
            {
                var result = await _examService.SubmitExamAsync(submission);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error during exam submission");
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Operation error during exam submission");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during exam submission");
                return StatusCode(500, "An error occurred while processing your submission");
            }
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
    }
}
