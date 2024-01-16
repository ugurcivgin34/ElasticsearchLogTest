using ElasticsearchLogTest.Core.Logger;
using ElasticsearchLogTest.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ElasticsearchLogTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TestController : ControllerBase
    {
        
        [HttpGet("business")]
        public IActionResult TriggerBusinessException()
        {
            throw new BusinessException("İş mantığı hatası oluştu.");
        }

        [HttpGet("validation")]
        public IActionResult TriggerValidationException()
        {
            var errors = new Dictionary<string, string[]>
            {
                { "Field1", new string[] { "Field1 is invalid." } },
                { "Field2", new string[] { "Field2 is required." } }
            };
            throw new ValidationException("Validasyon hatası oluştu.", errors);
        }

        [HttpGet("authorization")]
        public IActionResult TriggerAuthorizationException()
        {
            throw new AuthorizationException("Yetkilendirme hatası oluştu.");
        }

        [HttpGet("general")]
        public IActionResult TriggerGeneralException()
        {
            throw new Exception("Genel bir hata oluştu.");
        }
    }
}
