using ExamSystem.Application.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashBoardService;
        public DashBoardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }
        [HttpGet]
        public IActionResult GetDashBoard()
        {
            var dashboardData = _dashBoardService.GetDashBoardData();
            if (dashboardData == null)
                return NotFound("No data available for the dashboard.");
            return Ok(dashboardData);
        }
    }
}
