using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SqreenDispatcher.Controllers
{
    [ApiController, Route("{prefix:webhookRoutePrefix}/[controller]")]
    public abstract class ResponseHandler<TRequest, TResponse>
    {
        [HttpPost, Route("")]
        public abstract Task<TResponse> Handle([FromBody] TRequest request);
    }
}
