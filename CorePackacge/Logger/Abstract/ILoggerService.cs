using CorePackacge.Logger.Model;

namespace CorePackacge.Logger.Abstract
{
    public interface ILoggerService
    {
        void LogInformation(string moduleName, LogDetail detail);
        void LogWarning(string moduleName, LogDetail detail);
        void LogError(string moduleName, LogDetail detail);
    }
}
