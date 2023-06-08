using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToDo.Application.CustomExceptions;

namespace ToDo.API.Infrastructure.Models
{
    public class ApiError : ProblemDetails
    {
        public const string UnhandledErrorCode = "UnhandledError";
        private HttpContext _httpContext;
        private Exception _exception;

        public LogLevel LogLevel { get; set; }
        public string Code { get; set; }

        public string TraceId
        {
            get
            {
                if (Extensions.TryGetValue("TraceId", out var traceId))
                {
                    return (string)traceId;
                }
                return null;
            }
            set => Extensions["TraceId"] = value;
        }
        public ApiError(HttpContext httpContext, Exception exception)
        {
            _httpContext = httpContext;
            _exception = exception;

            TraceId = httpContext.TraceIdentifier;

            Code = UnhandledErrorCode;
            Status = (int)HttpStatusCode.InternalServerError;
            Title = exception.Message;
            LogLevel = LogLevel.Error;
            Instance = httpContext.Request.Path;

            HandleException((dynamic)exception);
        }
        public void HandleException(ItemNotFoundException exception)
        {
            Code = exception.code;
            Status = (int)HttpStatusCode.NotFound;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
            Title = exception.Message;
            LogLevel = LogLevel.Information;
        }
        public void HandleException(IncorrectUsernameOrPasswordException exception)
        {
            Code = exception.code;
            Status = (int)HttpStatusCode.NotFound;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
            Title = exception.Message;
            LogLevel = LogLevel.Information;
        }
        public void HandleException(Exception exception)
        {

        }
    }
}
