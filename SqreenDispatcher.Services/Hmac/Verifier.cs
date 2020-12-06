using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SqreenDispatcher.Services.Hmac
{
    public class Verifier
    {
        public static async Task<bool> VerifySignature(string secretKey, Stream content, string supposedSignature)
        {
            byte[] ba = Encoding.UTF8.GetBytes(secretKey);
            var hmac = new HMACSHA256(ba);
            byte[] signatureBytes = await hmac.ComputeHashAsync(content);
            var reconstructedRequestSignature = BitConverter.ToString(signatureBytes).Replace("-", "").ToLower();

            return supposedSignature == reconstructedRequestSignature;
        }
    }
}
