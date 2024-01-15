using ElasticsearchLogTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticsearchLogTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ElasticsearchService _elasticsearchService;

        public LogsController(ElasticsearchService elasticsearchService)
        {
            _elasticsearchService = elasticsearchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs(string query)
        {
            try
            {
                var logs = await _elasticsearchService.GetLogsAsync("logstash-*", query);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"İç sunucu hatası: {ex.Message}");
            }
        }
    }
}
