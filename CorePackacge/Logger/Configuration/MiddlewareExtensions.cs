using CorePackacge.Logger.Abstract;
using CorePackacge.Logger.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CorePackacge.Logger.Configuration
{
    public static class MiddlewareExtensions
    {
        public static void UseCorePackageMiddlewares(this WebApplication app)
        {
            var loggerService = app.Services.GetRequiredService<ILoggerService>();
            app.UseMiddleware<AuditLogMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>(loggerService);
        }
    }
}
