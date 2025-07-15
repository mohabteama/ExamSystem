using ExamSystem.Application.DTO;
using ExamSystem.Application.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController:ControllerBase
    {
        private readonly IOptionService _optionService;
        public OptionController(IOptionService optionService)
        {
            _optionService = optionService;
        }
        [HttpPost]
        public IActionResult CreateOption([FromBody] OptionDto optionDto, [FromQuery] int questionId)
        {
            if (!ModelState.IsValid || optionDto == null)
                return BadRequest(ModelState);
            _optionService.CreateOptions(optionDto, questionId);
            return Ok("Successfully created");
        }
    }
}
