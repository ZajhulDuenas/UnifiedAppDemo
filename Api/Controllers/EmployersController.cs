using Interfaces.UserStory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using UserStories.login;
using infrastructure.Api;

namespace WebApi.Controllers
{
    public class EmployersController(IemployeeUserStory employeeUser) : Controller
    {
        private readonly IemployeeUserStory employeeUser = employeeUser;

        [HttpPost]
        [Route("GetEmployees")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ShowListPolicy")] // aply custom Politic
        public async Task<IActionResult> Getlist()
        {
            
            var response = await employeeUser.GetEmployeList().ConfigureAwait(false);
            return response.GetActionResult();
            
        }


        [HttpPost]
        [Route("AddEmployee")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CreatePolicy")] // aply custom Politic
        public async Task<IActionResult> Create(EmpleadoDto request)
        {
            /*
            if (request == null)
                return BadRequest("Parametro de entrada nulo");

            var response = await tokenUserStory.GetToken(configuration, request);
            return response.GetActionResult();
            */

            return Ok("Datos accesibles solo por el departamento de HR");

        }
    }
}
