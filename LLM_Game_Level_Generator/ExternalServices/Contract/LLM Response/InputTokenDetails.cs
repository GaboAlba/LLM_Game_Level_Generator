namespace ExternalServices.Contract.LLM_Response
{
    using System.Text.Json.Serialization;

    public class InputTokenDetails
    {
        [JsonPropertyName("cached_tokens")]
        public int CachedTokens { get; set; }
    }
}
