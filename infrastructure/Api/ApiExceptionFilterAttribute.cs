using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace infrastructure.Api
{
#pragma warning disable CA1019 // Definir descriptores de acceso para los argumentos de atributo

#pragma warning disable S3993 // Custom attributes should be marked with "System.AttributeUsageAttribute"

    public sealed class ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> looger, IConfiguration configuration) : ExceptionFilterAttribute
#pragma warning restore S3993 // Custom attributes should be marked with "System.AttributeUsageAttribute"
#pragma warning restore CA1019 // Definir descriptores de acceso para los argumentos de atributo
    {
        private readonly ILogger<ApiExceptionFilterAttribute> looger = looger;
        private readonly IConfiguration configuration = configuration;

        public override void OnException(ExceptionContext context)
        {
            if (context == null)
                return;

            var id = Guid.NewGuid();
            var exception = new ApiException($"ID:{id} ", context.Exception);
            string msg = $"ID:{id}";

            looger.LogError(exception, message: msg);

            if (!string.IsNullOrEmpty(configuration["ShowErros"]) && configuration["ShowErros"] == "true")
                msg = context.Exception.ToString();

            var response = new Response
            {
                Message = $"Error {msg}",
                StatusCode = -500,
                Success = false,
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
