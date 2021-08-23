using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Black.Bot.DTOs
{
    public class LeakCheckResult
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("found")]
        public long Found { get; set; }

        [JsonPropertyName("result")]
        public Result[] Result { get; set; }
    }
}
