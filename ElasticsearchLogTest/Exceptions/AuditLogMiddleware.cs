﻿using ElasticsearchLogTest.Core.Logger;
using ElasticsearchLogTest.Model;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace ElasticsearchLogTest.Exceptions
{
    public class AuditLogMiddleware(RequestDelegate next, ILoggerService loggerService)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILoggerService _loggerService = loggerService;

        public async Task InvokeAsync(HttpContext context)
        {
            var requestUrl = context.Request.Path.Value;
            var segments = requestUrl?.Split('/');
            var firstSegment = segments?[2];
            var moduleName = firstSegment ?? "-";

            var endpoint = context.GetEndpoint();
            var routeData = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

            var controllerName = routeData?.ControllerName;
            var actionName = routeData?.ActionName;
            var methodName = controllerName != null && actionName != null
                             ? $"{controllerName}.{actionName}"
                             : "-";

            var logDetail = new LogDetail
            {
                User ="uğur okan ",
                ActionType = $"{moduleName}Action",
                Resource = context.Request.Path,
                MethodName = methodName,
                ModuleName=moduleName,
            };

            // İsteği işleyin
            await _next(context);

            // Loglama işlemi
            _loggerService.LogInformation(moduleName, logDetail);
        }
    }


}
