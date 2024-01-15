using Newtonsoft.Json;

namespace ElasticsearchLogTest.Model
{
    public class LogEntry
    {
        [JsonProperty("@timestamp")]
        public string Timestamp { get; set; } // List<string> yerine string

        public string Level { get; set; }
        public string Message { get; set; } // List<string> yerine string

        [JsonProperty("exceptions")]
        public List<ExceptionDetail> Exceptions { get; set; }

        public class ExceptionDetail
        {
            [JsonProperty("ClassName.raw")]
            public string ClassNameRaw { get; set; } // List<string> yerine string

            [JsonProperty("StackTraceString")]
            public string StackTraceString { get; set; } // List<string> yerine string

            // Diğer exception detayları...
        }
    }
}
