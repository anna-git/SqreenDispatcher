using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SqreenDispatcher
{
    public class AuthorizeSqreenAttribute : Attribute, IAuthorizationFilter
    {
        private string _secretKey = "1234";

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var services = context.HttpContext.RequestServices;

            var sqreenOptions = services.GetService(typeof(SqreenOptions));
            var success = context.HttpContext.Request.Headers.TryGetValue("X-Sqreen-Integrity", out Microsoft.Extensions.Primitives.StringValues keyv);

            byte[] ba = Encoding.UTF8.GetBytes(_secretKey);

            using var hmac = new HMACSHA256(ba);
            if (success)
            {
                
                var requestSignature = keyv.ToArray()[0];
                byte[] signatureBytes = await hmac.ComputeHashAsync(context.HttpContext.Request.Body);
                var requestSignatureBase64String =  BitConverter.ToString(signatureBytes).Replace("-", "").ToLower();

                var succes = requestSignature == requestSignatureBase64String;

            }

            context.Result = new UnauthorizedObjectResult($"Sqreen authentication payload is missing or invalid");
            return;
        }

        private static string ConvertToHex(string s)
        {
            byte[] ba = Encoding.UTF8.GetBytes(s);

            var hexString = BitConverter.ToString(ba);

            hexString = hexString.Replace("-", "");
            return hexString;
        }

    }
}
