using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [HttpPost]
        public IActionResult CreateQuestion( [FromBody] QuestionDto questionDto, [FromQuery] int SubjectID)
        {
            if (!ModelState.IsValid || CreateQuestion == null)
                return BadRequest(ModelState);

            if (!_questionService.CreateQuestion(questionDto, SubjectID))
                return StatusCode(422, "Question already exists or error in creation");

            return Ok("Successfully created");
        }
    }
}
