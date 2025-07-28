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
        public IActionResult CreateQuestion( [FromBody] CreateQuestionDto CreateQuestionDto, [FromQuery] int SubjectID)
        {
            if (!ModelState.IsValid || CreateQuestionDto == null)
                return BadRequest(ModelState);

            // Additional validation for the Text property
            if (string.IsNullOrEmpty(CreateQuestionDto.question))
                return BadRequest("Question text cannot be empty");

            if (!_questionService.CreateQuestion(CreateQuestionDto, SubjectID))
                return StatusCode(422, "Question already exists or error in creation");

            return Ok("Successfully created");
        }
    }
}
