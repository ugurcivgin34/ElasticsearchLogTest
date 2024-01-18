using ElasticsearchLogTest.Core.Logger;
using ElasticsearchLogTest.Exceptions;
using ElasticsearchLogTest.Model;
using Microsoft.AspNetCore.Mvc;


namespace ElasticsearchLogTest.Controllers
{
    [Route("api/Device/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILoggerService _loggerService;

        public TestController(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        [HttpGet("business")]
        public IActionResult TriggerBusinessException()
        {
            _loggerService.LogInformation("Device", new LogDetail
            {
                User = "Şerefsizzzzz",
                ActionType = "UserAction",
                Resource = "context.Request.Path",
                MethodName = "methodName",
                ModuleName = "moduleName"
            });
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
