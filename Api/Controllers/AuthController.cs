using Interfaces.UserStory;
using Microsoft.AspNetCore.Mvc;
using Api.infrastructure.Api;
using Api.Models.Request;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ItokenUserStory tokenUserStory , IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration configuration= configuration;
        private readonly ItokenUserStory tokenUserStory = tokenUserStory;
       
        [HttpPost]
        [Route("GetToken")]
        public async Task<IActionResult> getToken(LoginRequest request)
        {
            if (request == null)
                return BadRequest("Parametro de entrada nulo");

            var response = await tokenUserStory.GetToken(configuration, request);
            return response.GetActionResult();
        }

    }
}
