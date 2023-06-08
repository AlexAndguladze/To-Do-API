using Newtonsoft.Json;
using ToDo.API.Infrastructure.Models;

namespace ToDo.API.Infrastructure.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        public readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            var error = new ApiError(context, ex);
            var result = JsonConvert.SerializeObject(error);

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.Status.Value;

            await context.Response.WriteAsync(result);
            await LogException(result);
        }

        private async Task LogException(string error)
        {
            var logInfo = $"{Environment.NewLine}---- Exception Log ----{Environment.NewLine}" + error;
            var completePath = Directory.GetCurrentDirectory() + @"\Infrastructure\Logging\Logs.txt";
            await File.AppendAllTextAsync(completePath, logInfo);
        }
    }
}
