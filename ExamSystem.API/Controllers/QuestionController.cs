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
        public IActionResult CreateQuestion([FromBody] CreateQuestionDto createQuestionDto, [FromQuery] int subjectId)
        {
            if (!ModelState.IsValid || createQuestionDto == null)
                return BadRequest(ModelState);

            if (string.IsNullOrEmpty(createQuestionDto.question))
                return BadRequest("Question text cannot be empty");

            var createdQuestion = _questionService.CreateQuestion(createQuestionDto, subjectId);

            if (createdQuestion == null)
                return StatusCode(422, "Question already exists or error in creation");

            return Ok(new
            {
                Message = "Successfully created",
                QuestionId = createdQuestion.Id,
                QuestionText = createdQuestion.question
            });
        }


    }
}
