namespace CorePackacge.Logger.Model
{
    public class LogDetail
    {
        public string? User { get; set; }
        public string? ActionType { get; set; } // İşlem tipi, örneğin: "Login", "UpdateProfile" vb.
        public string? Resource { get; set; } // Etkilenen kaynak, örneğin: "UserProfile", "Order" vb.
        public string? MethodName { get; set; }
        public string? ExceptionMessage { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
