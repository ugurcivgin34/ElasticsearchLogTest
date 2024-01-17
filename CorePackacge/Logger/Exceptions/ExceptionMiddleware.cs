using CorePackacge.Logger.Abstract;
using CorePackacge.Logger.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CorePackacge.Logger.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public ExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var moduleName = context.Request.Headers["ModuleName"].FirstOrDefault() ?? "default";
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var logDetail = new LogDetail
                {
                    User = "Uğur Okan Çivgin",
                    MethodName = context.Request.Path,
                    ExceptionMessage = ex.Message,
                    ActionType = "Error",
                    Resource = context.Request.Path // Örnek olarak, talep edilen kaynağı kullanabilirsiniz
                };

                _loggerService.LogError(moduleName, logDetail);
                await HandleExceptionAsync(context, ex);
            }
        }


        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            switch (exception)
            {
                case ValidationException ve:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    await response.WriteAsync(new ErrorDetails(response.StatusCode, ve.Message, ve.Errors).ToString());
                    break;
                case BusinessException be:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    await response.WriteAsync(new ErrorDetails(response.StatusCode, be.Message).ToString());
                    break;
                case AuthorizationException ae:
                    response.StatusCode = StatusCodes.Status401Unauthorized;
                    await response.WriteAsync(new ErrorDetails(response.StatusCode, ae.Message).ToString());
                    break;
                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    await response.WriteAsync(new ErrorDetails(response.StatusCode, "Internal Server Error").ToString());
                    break;
            }
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; }
        public string Message { get; }
        public object Errors { get; }

        public ErrorDetails(int statusCode, string message, object errors = null)
        {
            StatusCode = statusCode;
            Message = message;
            Errors = errors;
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
