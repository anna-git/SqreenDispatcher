using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SqreenDispatcher
{
    public class AuthorizeSqreenAttribute : Attribute, IAuthorizationFilter
    {
        private string _secretKey= "542e0921e61c0ee5e8b7ac0be203d36707105404180af4908e1deefc9b53680d";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var services = context.HttpContext.RequestServices;

            var sqreenOptions = services.GetService(typeof(SqreenOptions));
            var success = context.HttpContext.Request.Headers.TryGetValue("X-Sqreen-Integrity", out Microsoft.Extensions.Primitives.StringValues keyv);
            if (success)
            {
                var key = keyv.ToArray()[0];
                var hmac256 = new HMACSHA256(Convert.FromBase64String(_secretKey));
                var keyBytes = Convert.FromBase64String(key);
                var hash = hmac256.ComputeHash(keyBytes);
                var res = Convert.ToBase64String(hash);


            }

            context.Result = new UnauthorizedObjectResult($"Sqreen authentication payload is missing or invalid");
            return;
        }

    }
}
