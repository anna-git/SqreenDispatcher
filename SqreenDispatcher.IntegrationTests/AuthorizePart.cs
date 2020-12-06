using GeekLearning.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using SqreenDispatcher.Services.Model;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using GeekLearning.Email;
using Xunit;

namespace SqreenDispatcher.IntegrationTests
{
    public class AuthorizePart :  IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public AuthorizePart(WebApplicationFactory<Startup> factory)
        {

            _factory = factory.WithWebHostBuilder(builder => {builder.UseEnvironment("Tests");
                builder.ConfigureServices(i => { i.AddInMemoryEmail(); });
            });

        }


        [Fact]
        public async Task TestAuthorizeBlock()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("/sqreen", new SqreenMessage().Yield());

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("Sqreen authentication payload is missing or invalid", content);
            
        }

        [Fact]
        public async Task TestAuthorizeOk()
        {
            var client = _factory.CreateClient();

            client.DefaultRequestHeaders.Add("X-Sqreen-Integrity", "5768234ff20d732ca1d63bdcde02a4c7a2a68ccbf1ffbe4c35345c6ef594d1aa"); 
            var response = await client.PostAsync("/sqreen", new StringContent("[{\"message_id\": null, \"api_version\": \"2\", \"date_created\": \"2020-12-06T13:01:52.270108+00:00\", \"message_type\": \"security_event\", \"retry_count\": 0, \"message\": {\"risk_coefficient\": 25, \"event_category\": \"http_error\", \"event_kind\": \"waf\", \"application_id\": \"5fca38cd4d2bfc001e0b60d4\", \"application_name\": \"AppToMonitor\", \"environment\": \"development\", \"date_occurred\": \"2020-12-04T14:28:38.585000+00:00\", \"event_id\": \"5fca47967d034b000f043d57\", \"event_url\": \"https://my.sqreen.com/application/5fca38cd4d2bfc001e0b60d4/events/5fca47967d034b000f043d57\", \"humanized_description\": \"Attack tentative from 172.17.0.1\", \"ips\": [{\"address\": \"172.17.0.1\", \"is_tor\": false, \"geo\": {}, \"date_resolved\": \"2020-12-04T14:28:38.771000+00:00\"}]}}]", Encoding.UTF8, "application/json"));


            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
