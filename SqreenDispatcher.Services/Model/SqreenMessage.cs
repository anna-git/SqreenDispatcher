using System;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SqreenDispatcher.Services.Model
{
    public class SqreenMessage
    {
        [JsonPropertyName("message_id")]
        public string? Id { get; set; }
        [JsonPropertyName("api_version")]
        public string? ApiVersion { get; set; }
        [JsonPropertyName("message_type")]

        public string? Type { get; set; }


        [JsonPropertyName("date_created")]
        public DateTime? DateCreated { get; set; }

        [JsonPropertyName("retry_count")]

        public int? RetryCount{ get; set; }

        public Message? Message { get; set; }

    }

    public class Message
    {
        [JsonPropertyName("event_id")]
        public string? EventId { get; set; }

        [JsonPropertyName("risk_coefficient")]
        public int? RiskCoefficient { get; set; }

        [JsonPropertyName("event_category")]
        public string? EventCategory { get; set; }

        [JsonPropertyName("event_url")]
        public string? EventUrl { get; set; }
        [JsonPropertyName("humanized_description")]

        public string? HumanizedDescription { get; set; }

        public string?  Url { get; set; }
        public string? Id { get; set; }
    }
}
