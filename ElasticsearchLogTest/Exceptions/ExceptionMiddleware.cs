using ElasticsearchLogTest.Core.Logger;
using Newtonsoft.Json;
using ElasticsearchLogTest.Model;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace ElasticsearchLogTest.Exceptions
{
    public class ExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILoggerService _loggerService = loggerService;

        public async Task InvokeAsync(HttpContext context)
        {
            var requestUrl = context.Request.Path.Value;
            var segments = requestUrl.Split('/');
            var firstSegment = segments[2];
            var moduleName = firstSegment ?? "default";

            var endpoint = context.GetEndpoint();
            var routeData = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

            var controllerName = routeData?.ControllerName;
            var actionName = routeData?.ActionName;
            var methodName = controllerName != null && actionName != null
                             ? $"{controllerName}.{actionName}"
                             : "-";

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var logDetail = new LogDetail
                {
                    User = "Uğur Okan Çivgin",
                    MethodName = methodName,
                    ExceptionMessage = ex.Message,
                    ActionType = $"{moduleName}Action",
                    ModuleName = moduleName,
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
