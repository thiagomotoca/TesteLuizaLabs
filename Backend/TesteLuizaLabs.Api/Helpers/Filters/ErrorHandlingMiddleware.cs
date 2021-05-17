using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TesteLuizaLabs.Lib.Excecao;

namespace TesteLuizaLabs.Api.Helpers.Filters
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            if (ex is DadoNaoEncontratoException) code = HttpStatusCode.NotFound;
            else if (ex is DadoInvalidoException) code = HttpStatusCode.BadRequest;
            else if (ex is DadoDuplicadoException) code = HttpStatusCode.Conflict;

            var result = JsonSerializer.Serialize(new { message = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
