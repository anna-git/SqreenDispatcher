using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SqreenDispatcher
{
    public class AuthorizeSqreenAttribute : Attribute, IAuthorizationFilter
    {

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var services = context.HttpContext.RequestServices;
            var sqreenOptions = (SqreenOptions) services.GetService(typeof(SqreenOptions));
            var secretKey = sqreenOptions!.SecretKey;
            var signatureExists = context.HttpContext.Request.Headers.TryGetValue("X-Sqreen-Integrity", out Microsoft.Extensions.Primitives.StringValues keyv);
            context.HttpContext.Request.EnableBuffering();
            byte[] ba = Encoding.UTF8.GetBytes(secretKey);

            using var hmac = new HMACSHA256(ba);
            if (signatureExists)
            {
                var requestSignature = keyv.ToArray()[0].Trim();
                byte[] signatureBytes = await hmac.ComputeHashAsync(context.HttpContext.Request.Body);
                var reconstructedRequestSignature =  BitConverter.ToString(signatureBytes).Replace("-", "").ToLower();

                var succes = requestSignature == reconstructedRequestSignature;
                if (succes)
                {
                    context.HttpContext.Request.Body.Position = 0;
                    return;
                }
            }
            
            context.Result = new UnauthorizedObjectResult($"Sqreen authentication payload is missing or invalid");
            return;
        }
    }
}
