using System.Text.Json.Serialization;

namespace Black.Bot.DTOs
{
    public class LeakCheckResult
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("found")]
        public long Found { get; set; }

        [JsonPropertyName("result")]
        public Result[] Result { get; set; }

        public bool NotFound => Error.ToLower() == "not found";
    }
}
