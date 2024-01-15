using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ElasticsearchLogTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Log.Information("Get işlemi başlatıldı.");

            try
            {
                // Burada iş mantığınızı uygulayın...
                // Örnek olarak bir hata oluşturalım
                throw new InvalidOperationException("Başarıyla çalıştı");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Get işlemi sırasında hata oluştu.");
                return StatusCode(500, "İç Sunucu Hatası");
            }
        }
    }
}
