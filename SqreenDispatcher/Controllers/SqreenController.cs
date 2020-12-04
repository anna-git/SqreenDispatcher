using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqreenDispatcher.Services.Model;

namespace SqreenDispatcher.Controllers
{
    [ApiController]
    [AuthorizeSqreen]
    [Route("[controller]")]
    public class SqreenController
    {
        private readonly ILogger<SqreenController> _logger;

        public SqreenController(ILogger<SqreenController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Index()
        {
            return "hello";
        }

        [HttpPost]
        public void Alert(SqreenMessage[] messages)
        {

        }


    }
}
