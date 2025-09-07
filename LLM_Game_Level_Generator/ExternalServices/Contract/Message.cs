namespace ExternalServices.Contract
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Message object for the LLM
    /// </summary>
    public record Message
    {
        /// <summary>
        /// The role of the LLM Message
        /// </summary>
        [JsonPropertyName("role")]
        public required string Role
        {
            get;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Role is null");
                }

                value = value.ToLower();

                if (value != "system" &&
                    value != "user" &&
                    value != "assistant")
                {
                    throw new ArgumentException($"Role {value} is not valid. It must either system, user, or assistant");
                }

                field = value;
            }
        }

        /// <summary>
        /// The content of the Prompt
        /// </summary>
        [JsonPropertyName("content")]
        public required string Content { get; set; }
    }
}
