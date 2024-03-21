using System.Net;
using System.Text.Json;
using Talabat.Errors;

namespace Talabat.MiddleWares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _host;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment host)
        {
            _next = next;
            _logger = logger;
            _host = host;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

            }catch (Exception ex)
            {
                context.Response.ContentType = "application/jason";
                context.Response.StatusCode= (int)HttpStatusCode.InternalServerError;
                var response =_host.IsDevelopment()?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString())
                    : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                var option = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json=JsonSerializer.Serialize(response, option);
               await context.Response.WriteAsync(json);
            }

        }
    }
}
