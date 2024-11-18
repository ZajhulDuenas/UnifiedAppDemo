using Microsoft.AspNetCore.Mvc;
using Models;

namespace Api.infrastructure.Api
{
    public static class ResponseExtends
    {
        public static IActionResult GetActionResult(this Response response)
        {
            if (response == null)
                return new BadRequestObjectResult("Sin respuesta");

            if (response.Success)
                return new OkObjectResult(response);
            else
                return new BadRequestObjectResult(response);
        }

        public static IActionResult GetCustomActionResult(this Response response)
       
        {
            if (response == null)
                return new BadRequestObjectResult("Sin respuesta");

            if (response.Success)
                return new OkObjectResult(response);
            else
                return new BadRequestObjectResult(response);
        }

        public static IActionResult GetActionResult<T>(this Response<T> response)
           where T : class
        {
            if (response == null)
                return new BadRequestObjectResult("Sin respuesta");

            if (response.Success)
                return new OkObjectResult(response);
            else
                return new BadRequestObjectResult(response);
        }

        public static IActionResult GetCustomActionResult<T>(this Response<T> response)
           where T : class
        {
            if (response == null)
                return new BadRequestObjectResult("Sin respuesta");

            if (response.Success)
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            else
                return new BadRequestObjectResult(response);
        }
    }
}
