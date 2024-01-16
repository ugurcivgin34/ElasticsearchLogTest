using Microsoft.AspNetCore.Mvc;
using ElasticsearchLogTest.Services;
using System.Threading.Tasks;

namespace ElasticsearchLogTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogsController(ElasticsearchService elasticsearchService) : ControllerBase
    {
        private readonly ElasticsearchService _elasticsearchService = elasticsearchService;

        [HttpGet]
        public async Task<IActionResult> GetLogs(string query)
        {
            var logs = await _elasticsearchService.GetLogsAsync("logstash-*", query);
            return Ok(logs);
        }
    }
}
