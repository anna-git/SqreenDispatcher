using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace SqreenDispatcher.Models
{
    public class Status : ActionFilterAttribute
    {
        private readonly HttpStatusCode statusCode;
            
        public Status(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.Result = new StatusCodeResult((int)statusCode);
        }
    }
}
