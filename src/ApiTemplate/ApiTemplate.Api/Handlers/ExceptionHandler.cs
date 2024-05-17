using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ApiTemplate.Api.Handlers
{
    public static class ExceptionHandler
    {
        public static async Task HandleError(HttpContext context) 
        {
            var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is ValidationException)
            {
                var error = (ValidationException)exceptionHandlerPathFeature.Error;

                var responseErrors = JsonConvert.SerializeObject(error.Errors.Select(p => p.ErrorMessage));

                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(responseErrors);
            }
        }
    }
}
