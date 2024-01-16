namespace ElasticsearchLogTest.Exceptions
{
    public class ValidationException(string message, IDictionary<string, string[]> errors) : Exception(message)
    {
        public IDictionary<string, string[]> Errors { get; private set; } = errors;
    }

}
