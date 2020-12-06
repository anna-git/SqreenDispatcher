using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqreenDispatcher.Services;
using SqreenDispatcher.Services.Model;

namespace SqreenDispatcher.Controllers
{
    [ApiController]
    [AuthorizeSqreen]
    [Route("[controller]")]
    public class SqreenController
    {
        private readonly Dispatcher _dispatcher;

        public SqreenController(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public string Index()
        {
            return "hello";
        }

        [HttpPost]
        public async void Alert(SqreenMessage[] messages)
        {
            await _dispatcher.Dispatch(messages);
        }
    }
}
