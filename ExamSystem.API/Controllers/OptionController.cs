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

            var success = _optionService.CreateOptions(optionDto, questionId);

            if (!success)
                return StatusCode(500, "Error creating option");

            return Ok(new { message = "Option created successfully" });
        }
    }
}
