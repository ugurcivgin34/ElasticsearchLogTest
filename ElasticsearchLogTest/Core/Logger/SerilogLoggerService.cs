using ElasticsearchLogTest.Model;
using Serilog;
using Serilog.Context;

namespace ElasticsearchLogTest.Core.Logger
{
    public class SerilogLoggerService : ILoggerService
    {
        public void LogInformation(LogDetail detail)
        {
            var indexName = $"{detail.ModuleName}-logs";

            using (LogContext.PushProperty("IndexName", indexName))
            {
                Log.Information("User: {User}, ActionType: {ActionType}, Resource: {Resource}, MethodName: {MethodName}, ModuleName: {ModuleName}",
                                detail.User, detail.ActionType, detail.Resource, detail.MethodName,detail.ModuleName);
            }
        }

        public void LogWarning(LogDetail detail)
        {
            var indexName = $"{detail.ModuleName}-logs";


            using (LogContext.PushProperty("IndexName", indexName))
            {
                Log.Warning("User: {User}, ActionType: {ActionType}, Resource: {Resource}, MethodName: {MethodName}, ModuleName: {ModuleName}",
                                detail.User, detail.ActionType, detail.Resource, detail.MethodName, detail.ModuleName);
            }
        }

        public void LogError( LogDetail detail)
        {
            var indexName = $"{detail.ModuleName}-logs";


            using (LogContext.PushProperty("IndexName", indexName))
            {
                Log.Error("User: {User}, ActionType: {ActionType}, Resource: {Resource}, MethodName: {MethodName}, ModuleName: {ModuleName}",
                                detail.User, detail.ActionType, detail.Resource, detail.MethodName, detail.ModuleName);
            }
        }
    }

}
