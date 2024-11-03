using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using UserStories.login;
using Api.infrastructure.Api;


namespace WebApi.Controllers
{
    public class EmployersController(IemployeeUserStory employeeUser) : Controller
    {
        private readonly IemployeeUserStory employeeUser = employeeUser;


        /*
        [HttpPost]
        [Route("GetAllEmployees")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ShowListPolicy")] // aply custom Politic
        public async Task<IActionResult> Getlist()
        {
            
            var response = await employeeUser.GetEmployeList().ConfigureAwait(false);
            return response.GetActionResult();
            
        }
        */

        [HttpPost]
        [Route("GetEmployees")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ShowListPolicy")] // aply custom Politic
        public async Task<IActionResult> GetAlllist([FromBody] PaginationDto request)
        {
            if (request == null)
                return BadRequest("Parametro de entrada nulo");
           
            var response = await employeeUser.GetEmployeList(request).ConfigureAwait(false);
            return response.GetActionResult();

        }

        [HttpPost]
        [Route("GetEmployeById")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ShowListPolicy")] // aply custom Politic
        public async Task<IActionResult> GetlistById([FromBody] EmployeeIdDto request)
        {

            var response = await employeeUser.GetEmployeById(request).ConfigureAwait(false);
            return response.GetActionResult();

        }


        [HttpPost]
        [Route("AddEmployee")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CreatePolicy")] // aply custom Politic
        public async Task<IActionResult> Create([FromBody] EmployeeDto request)
        {
            
            if (request == null)
                return BadRequest("Parametro de entrada nulo");

            var response = await employeeUser.AddEmployee(request).ConfigureAwait(false);
            return response.GetCustomActionResult();

        }

        [HttpPost]
        [Route("DeleteEmployee")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "DeletePolicy")] // aply custom Politic
        public async Task<IActionResult> Delete([FromBody] EmployeeIdDto request)
        {

            if (request == null)
                return BadRequest("Parametro de entrada nulo");

            var response = await employeeUser.DeleteEmployee(request).ConfigureAwait(false);
            return response.GetCustomActionResult();

        }

        [HttpPost]
        [Route("ModifyEmployee")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "UpdatePolicy")] // aply custom Politic
        public async Task<IActionResult> Modify([FromBody] Employee2Dto request)
        {

            if (request == null)
                return BadRequest("Parametro de entrada nulo");

            var response = await employeeUser.ModifyEmployee(request).ConfigureAwait(false);
            return response.GetCustomActionResult();

        }

        [HttpGet]
        [Route("ExportEmployeeList")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ExportPolicy")] // aply custom Politic
        public async Task<IActionResult> Export()
        {

            var response = await employeeUser.ExportData().ConfigureAwait(false);

            if (response.StatusCode >= 400) return response.GetCustomActionResult();

            var fileStream = response.Payload;

            return File(fileStream.stream, fileStream.mimeType, fileStream.fileName);


        }

        [HttpPost]
        [Route("ImportEmployeeList")]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ImportPolicy")] // aply custom Politic
        public async Task<IActionResult> Import(IFormFile file)
        {

            if (file == null || file.Length == 0)
                return BadRequest("Parametro de entrada nulo");


            // Ejemplo de uso
            var allowedExtensions = new[] { ".xlsx", ".xls" };
            if (!IsValidFileExtension(file, allowedExtensions))
            {
                return BadRequest("Formato de archivo no válido.");
            }


            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0; // Asegúrate de establecer la posición en el inicio antes de leer

            var response = await employeeUser.ImportData(memoryStream).ConfigureAwait(false);

            return response.GetCustomActionResult();

            //return response.GetCustomActionResult();

        }

        private bool IsValidFileExtension(IFormFile file, string[] allowedExtensions)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(fileExtension);
        }
    }
}
