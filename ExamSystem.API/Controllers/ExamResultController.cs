using ExamSystem.Application.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamResultController : ControllerBase
    {
        private readonly IExamResultService _examResultService;
        public ExamResultController(IExamResultService examResultService)
        {
            _examResultService = examResultService;
        }
        [HttpGet("{studentId}/{examId}")]
        public async Task<IActionResult> GetExamResult(string studentId, int examId)
        {
            var result = await _examResultService.GetExamResultAsync(studentId, examId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
    
}
