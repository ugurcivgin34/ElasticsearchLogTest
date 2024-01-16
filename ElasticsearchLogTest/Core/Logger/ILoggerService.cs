using ElasticsearchLogTest.Model;

namespace ElasticsearchLogTest.Core.Logger
{
    public interface ILoggerService
    {
        void LogInformation(LogDetail detail);
        void LogWarning(LogDetail detail);
        void LogError(LogDetail detail);
    }
}
