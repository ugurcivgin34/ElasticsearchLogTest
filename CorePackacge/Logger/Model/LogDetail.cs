namespace CorePackacge.Logger.Model
{
    public class LogDetail
    {
        public string User { get; set; }
        public string ActionType { get; set; } // Örneğin: "Create", "Update"
        public string Resource { get; set; } // Etkilenen kaynak, örneğin: "Device", "Order"
        public string MethodName { get; set; }
        public string Description { get; set; } // Yeni eklenecek açıklama
        public Dictionary<string, object> Parameters { get; set; }
    }
}
