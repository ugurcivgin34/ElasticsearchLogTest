namespace ElasticsearchLogTest.Model
{
    public class LogDetail
    {
        public string User { get; set; }
        public string MethodName { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public string ExceptionMessage { get; set; } // Hata durumları için
    }

}
