using Serilog.Context;
using Serilog;
using CorePackacge.Logger.Model;
using CorePackacge.Logger.Abstract;

namespace CorePackacge.Logger.SeriLogConcrete
{
    public class SerilogLoggerService : ILoggerService
    {
        public void LogInformation(string moduleName, LogDetail detail)
        {
            var indexName = $"{moduleName}-logs";

            using (LogContext.PushProperty("IndexName", indexName))
            {
                // 'Application' özelliği ekleyerek loglama yapın
                Log.ForContext("Application", "Information")
                   .Information("User: {User}, ActionType: {ActionType}, Resource: {Resource}, MethodName: {MethodName}, Parameters: {Parameters}",
                                detail.User, detail.ActionType, detail.Resource, detail.MethodName, detail.Parameters);
            }
        }

        public void LogWarning(string moduleName, LogDetail detail)
        {
            var indexName = $"{moduleName}-logs";

            // Serilog Context'ini kullanarak dinamik index ismi belirleme
            using (LogContext.PushProperty("IndexName", indexName))
            {
                Log.Warning("User: {User}, ActionType: {ActionType}, Resource: {Resource}, MethodName: {MethodName}, Parameters: {Parameters}",
                                detail.User, detail.ActionType, detail.Resource, detail.MethodName, detail.Parameters);
            }
        }

        public void LogError(string moduleName, LogDetail detail)
        {
            var indexName = $"{moduleName}-logs";

            // Serilog Context'ini kullanarak dinamik index ismi belirleme
            using (LogContext.PushProperty("IndexName", indexName))
            {
                Log.Error("User: {User}, ActionType: {ActionType}, Resource: {Resource}, MethodName: {MethodName}, Parameters: {Parameters}",
                                detail.User, detail.ActionType, detail.Resource, detail.MethodName, detail.Parameters);
            }
        }
    }
}
