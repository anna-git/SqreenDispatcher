using SqreenDispatcher.Services.Hmac;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace SqreenDispatcher.Tests
{
    public class VerifierTests
    {
        [Fact]
        public async System.Threading.Tasks.Task VerifySignatureOKAsync()
        {
            var secretKey = "1234";
            var content = "hello people!";
            byte[] byteArray = Encoding.UTF8.GetBytes(content);
            using var stream = new MemoryStream(byteArray);
            var signature = "27180575593528cde83d650d47f34f4ef7f825c091eddcd4f7c8637588f367cc";
            var correct = await Verifier.VerifySignatureAsync(secretKey, stream, signature);
            Assert.True(correct);

        }

        [Fact]
        public async System.Threading.Tasks.Task VerifySignatureKOAsync()
        {
            var secretKey = "1234";
            var content = "hello people!";
            byte[] byteArray = Encoding.UTF8.GetBytes(content);
            using var stream = new MemoryStream(byteArray);
            var signature = "37180575593528cde83d650d47f34f4ef7f825c091eddcd4f7c8637588f367cf";
            var correct = await Verifier.VerifySignatureAsync(secretKey, stream, signature);
            Assert.False(correct);

        }
    }
}
