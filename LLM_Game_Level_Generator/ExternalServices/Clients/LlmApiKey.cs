namespace ExternalServices.Clients
{
    using ExternalServices.Contract;

    using System.Text.Json.Serialization;

    public class LlmApiKey
    {
        /// <summary>
        /// Gets or sets the 
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LLMProviders.Providers Provider { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public required string Key { get; set; }
    }
}
