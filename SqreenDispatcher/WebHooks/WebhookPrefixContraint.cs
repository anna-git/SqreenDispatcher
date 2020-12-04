using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqreenDispatcher.WebHooks
{
    public class WebhookRoutePrefixConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (httpContext != null)
            {
                if (values.TryGetValue("prefix", out var value) && value is string actual)
                {
                    var options = httpContext.RequestServices.GetService(typeof(WebhookOptions)) as WebhookOptions;
                    // urls are case sensitive
                    var expected = options?.RoutePrefix;
                    return expected == actual;
                }
            }
            return false;
        }
    }
}
