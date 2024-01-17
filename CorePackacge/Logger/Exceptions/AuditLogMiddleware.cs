using CorePackacge.Logger.Abstract;
using CorePackacge.Logger.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace CorePackacge.Logger.Exceptions
{
    public class AuditLogMiddleware(RequestDelegate next, ILoggerService loggerService)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILoggerService _loggerService = loggerService;

        public async Task InvokeAsync(HttpContext context)
        {
            var moduleName = context.Request.Headers["ModuleName"].FirstOrDefault() ?? "default";
            var endpoint = context.GetEndpoint();
            var routeData = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

            var controllerName = routeData?.ControllerName;
            var actionName = routeData?.ActionName;
            var methodName = controllerName != null && actionName != null
                             ? $"{controllerName}.{actionName}"
                             : "-";

            var logDetail = new LogDetail
            {
                User = "uğur okan ",
                ActionType = "UserAction",
                Resource = context.Request.Path,
                MethodName = methodName,
                Parameters = [] // Burada gerekli parametreleri ekleyebilirsiniz
            };

            // İsteği işleyin
            await _next(context);

            // Loglama işlemi
            _loggerService.LogInformation(moduleName, logDetail);
        }
    }

}
