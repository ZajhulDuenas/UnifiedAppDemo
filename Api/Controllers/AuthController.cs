using Interfaces.UserStory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using infrastructure.Api;

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
        public async Task<IActionResult> getToken(LoginDto request)
        {
            if (request == null)
                return BadRequest("Parametro de entrada nulo");

            var response = await tokenUserStory.GetToken(configuration, request);
            return response.GetActionResult();
        }

    }
}
