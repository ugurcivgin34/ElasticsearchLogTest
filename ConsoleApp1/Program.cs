string timestampString = "2024-01-16T08:13:59.391Z";
DateTime timestamp = DateTime.ParseExact(timestampString, "yyyy-MM-ddTHH:mm:ss.fffZ", null, System.Globalization.DateTimeStyles.RoundtripKind);

Console.WriteLine($"timestamp: {timestamp}");