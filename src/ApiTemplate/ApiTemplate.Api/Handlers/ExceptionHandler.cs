using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Serilog;

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
                
                Log.Logger.Warning(exceptionHandlerPathFeature.Error, error.Message);

                var responseErrors = JsonConvert.SerializeObject(error.Errors.Select(p => p.ErrorMessage));

                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(responseErrors);
            }
            else
            {
                Log.Logger.Error(exceptionHandlerPathFeature.Error, exceptionHandlerPathFeature.Error.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/text";

                await context.Response.WriteAsync("Ocorreu um erro inesperado");
            }
        }
    }
}
