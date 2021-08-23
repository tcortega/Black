using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Black.Bot.DTOs
{
    public class Result
    {
        [JsonPropertyName("line")]
        public string Line { get; set; }

        [JsonPropertyName("sources")]
        public string[] Sources { get; set; }

        [JsonPropertyName("last_breach")]
        public string LastBreach { get; set; }
    }
}
