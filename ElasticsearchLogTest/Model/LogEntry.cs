using Nest;
using Newtonsoft.Json;

namespace ElasticsearchLogTest.Model
{
    public class LogEntry
    {
        [Date(Name = "@timestamp")]
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }

    }
}

