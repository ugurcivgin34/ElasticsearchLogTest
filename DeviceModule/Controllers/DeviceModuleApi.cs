using CorePackacge.Logger.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeviceModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceModuleApi : ControllerBase
    {
        private readonly CorePackacge.Logger.Abstract.ILoggerService _loggerService;

        public DeviceModuleApi(CorePackacge.Logger.Abstract.ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        [HttpGet("business")]
        public IActionResult TriggerBusinessException()
        {
            var logDetails = new LogDetail
            {
                User = "xx",
                ActionType = "Create",
                Resource = "Device",
                MethodName = "AddDevice",
                Description = "Adding a new device",
                Parameters = new Dictionary<string, object>
    {
        {"DeviceId", 123},
        {"DeviceName", "New Device"}
                }
            };

            _loggerService.LogError("DeviceModule", logDetails);
            return Ok();
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

        //[HttpGet("authorization")]
        //public IActionResult TriggerAuthorizationException()
        //{
        //}

        //[HttpGet("general")]
        //public IActionResult TriggerGeneralException()
        //{
        //}
    }
}
