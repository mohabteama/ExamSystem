using ExamSystem.Application.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize]
        [HttpGet("{examId}")]
        public async Task<IActionResult> GetExamResult(int examId)
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _examResultService.GetExamResultAsync(studentId, examId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
    
}
