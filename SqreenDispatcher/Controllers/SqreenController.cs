using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SqreenDispatcher.Controllers
{
    public class SqreenController
    {
        private readonly ILogger<SqreenController> _logger;

        public SqreenController(ILogger<SqreenController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public void Alert(object test)
        {

        }


    }
}
