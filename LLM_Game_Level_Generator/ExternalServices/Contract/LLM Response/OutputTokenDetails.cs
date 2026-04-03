namespace ExternalServices.Contract.LLM_Response
{
    using System.Text.Json.Serialization;

    public class OutputTokenDetails
    {
        [JsonPropertyName("reasoning_tokens")]
        public int ReasoningTokens { get; set; }
    }
}
