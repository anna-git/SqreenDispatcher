using System;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SqreenDispatcher.Services.Model
{
    public class SqreenMessage
    {
        [JsonPropertyName("api_version")]
        public string? ApiVersion { get; set; }
        [JsonPropertyName("message_type")]

        public string? MessageType { get; set; }

        [JsonPropertyName("message_id")]

        public string? MessageId { get; set; }

        [JsonPropertyName("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonPropertyName("retry_count")]

        public int? RetryCount{ get; set; }

        public Message Message { get; set; }

    }

    public class Message
    {
        [JsonPropertyName("event_id")]

        public string? EventId { get; set; }
        [JsonPropertyName("risk_coefficient")]

        public int RiskCoefficient { get; set; }
        [JsonPropertyName("event_category")]

        public string? EventCategory { get; set; }
    }
}
