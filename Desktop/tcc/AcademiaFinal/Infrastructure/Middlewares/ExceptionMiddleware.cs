using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NexusGym.Infrastructure.ProblemDetails;

namespace NexusGym.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Acesso não autorizado");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                var problem = ProblemDetailsFactory.Unauthorized(ex.Message);

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(problem));
            }
            
            catch (BadHttpRequestException ex)
            {
                _logger.LogWarning(ex, "Requisição mal sucedida");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problem = ProblemDetailsFactory.BadRequest(ex.Message);

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(problem));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var detail = _env.IsDevelopment()
                    ? ex.Message
                    : "Ocorreu um erro inesperado";

                var problem = ProblemDetailsFactory.InternalServerError(detail);

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(problem));
            }
        }
    }
}
