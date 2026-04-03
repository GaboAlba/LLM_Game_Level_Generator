namespace ExternalServices.Contract.LLM_Response
{
    using System.Text.Json.Serialization;

    public class LLMUsage
    {
        [JsonPropertyName("input_tokens")]
        public int InputTokens { get; set; }

        [JsonPropertyName("input_tokens_details")]
        public InputTokenDetails InputTokenDetails { get; set; }

        [JsonPropertyName("output_tokens")]
        public int OutputTokens { get; set; }

        [JsonPropertyName("output_tokens_details")]
        public OutputTokenDetails OutputTokenDetails { get; set; }

        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }
}
