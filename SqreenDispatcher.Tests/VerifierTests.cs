using SqreenDispatcher.Services.Hmac;
using System.IO;
using System.Text;
using Xunit;

namespace SqreenDispatcher.Tests
{
    public class VerifierTests
    {
        [Fact]
        public async System.Threading.Tasks.Task VerifySignatureOK()
        {
            var secretKey = "1234";
            var content = "[{\"message_id\": null, \"api_version\": \"2\", \"date_created\": \"2020-12-06T11:49:17.469469+00:00\", \"message_type\": \"test\", \"retry_count\": 0, \"message\": {\"application_name\": \"AppToMonitor\", \"environment\": \"development\", \"id\": \"33f60cb8a0c07344014826bc1683d84f\", \"event_category\": \"test\", \"event_kind\": \"test_kind\", \"risk\": 42, \"date_occurred\": \"2020-12-06T11:49:17.469798+00:00\", \"humanized_description\": \"\", \"url\": \"http://test\", \"ips\": [{\"address\": \"42.42.42.42\", \"date_resolved\": \"2020-12-06T11:49:17.469856+00:00\"}]}}]";
            byte[] byteArray = Encoding.UTF8.GetBytes(content);
            using var stream = new MemoryStream(byteArray);
            var signature = "ed1ff1cd76fcf50b3cf98742d9a7ec3a06e4445d886292a72bee045d4fee70ca";
            var correct = await Verifier.VerifySignature(secretKey, stream, signature);
            Assert.True(correct);

        }

        [Fact]
        public async System.Threading.Tasks.Task VerifySignatureKO()
        {
            var secretKey = "1234";
            var content = "[{\"message_id\": null, \"api_version\": \"2\", \"date_created\": \"2020-12-06T11:49:17.469469+00:00\", \"message_type\": \"test\", \"retry_count\": 0, \"message\": {\"application_name\": \"AppToMonitor\", \"environment\": \"development\", \"id\": \"33f60cb8a0c07344014826bc1683d84f\", \"event_category\": \"test\", \"event_kind\": \"test_kind\", \"risk\": 42, \"date_occurred\": \"2020-12-06T11:49:17.469798+00:00\", \"humanized_description\": \"\", \"url\": \"http://test\", \"ips\": [{\"address\": \"42.42.42.42\", \"date_resolved\": \"2020-12-06T11:49:17.469856+00:00\"}]}}]";
            byte[] byteArray = Encoding.UTF8.GetBytes(content);
            using var stream = new MemoryStream(byteArray);
            var signature = "cd1ff1cd76fcf50b3cf98742d9a7ec3a06e4445d886292a72bee045d4fee70c2";
            var correct = await Verifier.VerifySignature(secretKey, stream, signature);
            Assert.False(correct);

        }
    }
}
