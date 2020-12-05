using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SqreenDispatcher.Services.Hmac;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SqreenDispatcher
{
    public class AuthorizeSqreenAttribute : Attribute, IAuthorizationFilter
    {

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var services = context.HttpContext.RequestServices;
            var sqreenOptions = (SqreenOptions)services.GetService(typeof(SqreenOptions));
            var signatureExists = context.HttpContext.Request.Headers.TryGetValue("X-Sqreen-Integrity", out Microsoft.Extensions.Primitives.StringValues keyv);
            context.HttpContext.Request.EnableBuffering();
            
            if (signatureExists && keyv.Any())
            {
                var result = await Verifier.VerifySignatureAsync(sqreenOptions!.SecretKey, context.HttpContext.Request.Body, keyv[0]);
                context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                if (result) return;
            }
            context.Result = new UnauthorizedObjectResult($"Sqreen authentication payload is missing or invalid");
            return;
        }


    }
}
