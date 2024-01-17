using CorePackacge.Logger.Abstract;
using CorePackacge.Logger.Exceptions;
using CorePackacge.Logger.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeviceModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceModuleApi : ControllerBase
    {
        private readonly ILoggerService _loggerService;

        public DeviceModuleApi(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        [HttpGet("business")]
        public IActionResult TriggerBusinessException()
        {
            throw new BusinessException("İş mantığı hatası oluştu.");
        }

        [HttpGet("validation")]
        public IActionResult TriggerValidationException()
        {
            LogDetail logDetail = new LogDetail();
            var errors = new Dictionary<string, string[]>
            {
                { "Field1", new string[] { "Field1 is invalid." } },
                { "Field2", new string[] { "Field2 is required." } }
            };
         

            return Ok();
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
