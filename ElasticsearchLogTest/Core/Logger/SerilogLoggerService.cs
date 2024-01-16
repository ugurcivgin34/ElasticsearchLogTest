using ElasticsearchLogTest.Model;
using Serilog;

namespace ElasticsearchLogTest.Core.Logger
{
    public class SerilogLoggerService : ILoggerService
    {
        public void LogInformation(LogDetail detail)
        {
            Log.Information("User: {User}, MethodName: {MethodName}, Parameters: {Parameters}, ExceptionMessage: {ExceptionMessage}",
                            detail.User, detail.MethodName, detail.Parameters, detail.ExceptionMessage);
        }

        public void LogWarning(LogDetail detail)
        {
            Log.Warning("User: {User}, MethodName: {MethodName}, Parameters: {Parameters}, ExceptionMessage: {ExceptionMessage}",
                        detail.User, detail.MethodName, detail.Parameters, detail.ExceptionMessage);
        }

        public void LogError(LogDetail detail)
        {
            Log.Error("User: {User}, MethodName: {MethodName}, Parameters: {Parameters}, ExceptionMessage: {ExceptionMessage}",
                      detail.User, detail.MethodName, detail.Parameters, detail.ExceptionMessage);
        }
    }

}
