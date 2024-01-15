using ElasticsearchLogTest.Model;
using Nest;

namespace ElasticsearchLogTest.Services
{
    public class ElasticsearchService
    {
        private readonly ElasticClient _client;

        public ElasticsearchService(Uri elasticsearchUrl)
        {
            var settings = new ConnectionSettings(elasticsearchUrl);
            _client = new ElasticClient(settings);
        }

        public async Task<IEnumerable<LogEntry>> GetLogsAsync(string index, string query, int size = 10)
        {
            var searchResponse = await _client.SearchAsync<LogEntry>(s => s
                .Index(index)
                .Query(q => q.QueryString(d => d.Query(query)))
                .Size(size));

            if (!searchResponse.IsValid)
            {
                // Hata işleme
                throw new Exception("Elasticsearch sorgusunda hata oluştu.");
            }

            return searchResponse.Documents;
        }
    }
}
