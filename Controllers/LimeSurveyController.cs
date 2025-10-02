using LimeSurveyAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LimeSurveyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LimeSurveyController : ControllerBase
    {
        private readonly ILimeSurveyService _limeSurveyService;

        public LimeSurveyController(ILimeSurveyService limeSurveyService)
        {
            _limeSurveyService = limeSurveyService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromQuery] string userName, [FromQuery] string password)
        {
            var session = await _limeSurveyService.GetSessionKeyAsync(userName, password);
            return Ok(new { session });
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsersAsync([FromQuery] string sessionKey)
        {
            var users = await _limeSurveyService.GetUsersAsync(sessionKey);
            return Ok(users);
        }

        [HttpGet("surveys")]
        public async Task<IActionResult> ListSurveys([FromQuery] string sessionKey, [FromQuery] string? username = null)
        {
            var result = await _limeSurveyService.ListSurveysAsync(sessionKey, username);
            return Ok(result);
        }
    }
}
